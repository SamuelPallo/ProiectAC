using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using Microsoft.VisualBasic.PowerPacks;
using System.Windows.Forms;

namespace ProiectAC
{
    public partial class Simulator : Form
    {
        private Asamblor asamblor;

        struct label
        {
            public string name;
            public int address;
        };

        struct procedure
        {
            public string name;
            public int address;
        };

        // declarations
        #region declaratii

        static label[] labels = new label[50];
        static procedure[] procedures = new procedure[50];

        static byte[] RAM = new byte[65536];
        static UInt16 IR = 0;
        static UInt16 FLAG = 0;
        static UInt16 IVR = 0;
        static UInt16 T = 0;
        static UInt16 SP = 65534;
        static UInt16 MDR = 0;
        static UInt16 ADR = 0;
        static UInt16 PC = 0;
        static UInt16 PCMax = 0; // PC Maxim, in functie de cate instructiuni sunt in fisier
        static UInt16[] R = new UInt16[16];
        static UInt16 reg = 0;
        static UInt16 g = 0;
        static UInt16 clasa;
        static UInt16 IndexValoare;

        static UInt64[] MPM = new UInt64[200];
        static UInt64 MIR = 0;
        static UInt16 MAR = 0;
        static UInt16 SBUS = 0;
        static UInt16 DBUS = 0;
        static UInt16 RBUS = 0;
        static UInt16 Carry = 0;

        static int Step = 0;

        static UInt16 bit = 0;

        static string registru;
        static string valoareImediata;
        static string valImediataBinar;
        static string IM = "00";
        static string AD = "01";
        static string AI = "10";
        static string AX = "11";

        static int contor;
        static int adresaSalt;
        static int modAdresare;
        static int offset;
        static string valIndex;
        static string valIndexSursa;
        static string valIndexDest;
        static string offsetString;

        static Dictionary<string, string> base1 = new Dictionary<string, string>();
        static Dictionary<string, string> base2 = new Dictionary<string, string>();
        static Dictionary<string, string> base3 = new Dictionary<string, string>();
        static Dictionary<string, string> base4 = new Dictionary<string, string>();
        static Dictionary<string, string> registers = new Dictionary<string, string>() { 
            {"R0","0000"},{"R1","0001"},{"R2","0010"},{"R3","0011"},{"R4","0100"},{"R5","0101"},{"R6","0110"},{"R7","0111"},
            {"R8","1000"},{"R9","1001"},{"R10","1010"},{"R11","1011"},{"R12","1100"},{"R13","1101"},{"R14","1110"},{"R15","1111"}
        };
        Dictionary<string, string> registreIndex = new Dictionary<string, string>();
        static Dictionary<UInt16, byte> mem = new Dictionary<UInt16, byte>();
 

        List<string> microcod = new List<string>();

        static string str, firstStr, opcode, numeOpcode = null;
        static string codif;
        static int nbLabels = 0;
        static int nbProcedures = 0;

        static bool isb1 = false;
        static bool isb2 = false;
        static bool isb3 = false;
        static bool isb4 = false;
        static bool isValImm = false;
        static bool isIndexSrc = false;
        static bool isIndexDest = false;
        static bool isError = false;
        static bool isJMP = false;
        static bool isCALL = false;
        static bool isPUSHRi = false;
        static bool isPOPRi = false;
        static bool asmFileOpened = false;
        bool microCodeOpened = false;
        static bool isStep;
        static bool isINT;
        static Simulator simulator;
        //Form2 memory;

        #endregion

        public Simulator()
        {
            InitializeComponent();
            simulator = this;
            asamblor = new Asamblor();
        }

        private void ReadInstructionsEncodings() {
            int numberBase1 = 0, numberBase2 = 0, numberBase3 = 0, numberBase4 = 0, ct = 1;
            string firstLine, opcodeName, lineFile;

            StreamReader encodingFile = new StreamReader("encodingsInputFile.txt");
            char[] separators = { ' ' };
            string[] values = null;
            
            firstLine = encodingFile.ReadLine();
            if (firstLine != null) {
                values = firstLine.Split(separators);
                numberBase1 = Convert.ToInt16(values[0]);
                numberBase2 = Convert.ToInt16(values[1]);
                numberBase3 = Convert.ToInt16(values[2]);
                numberBase4 = Convert.ToInt16(values[3]);
            }
            while ((lineFile = encodingFile.ReadLine()) != null) {
                values = lineFile.Split(separators); 

                if (ct <= numberBase1) {
                    opcodeName = values[0];
                    opcode = values[1];
                    base1.Add(values[0], values[1]);
                } else
                    if (ct <= (numberBase1 + numberBase2)) {
                    opcodeName = values[0];
                    opcode = values[1];
                    base2.Add(values[0], values[1]);
                } else
                        if (ct <= (numberBase1 + numberBase2 + numberBase3)) {
                    opcodeName = values[0];
                    opcode = values[1];
                    base3.Add(opcodeName, opcode);
                } else {
                    if (values[1] == "PC" || values[1] == "FLAG") {
                        opcodeName = values[0] + " " + values[1];
                        opcode = values[2];
                        base4.Add(opcodeName, opcode);
                    }
                    else {
                        opcodeName = values[0];
                        opcode = values[1];
                        base4.Add(opcodeName, opcode);
                    }
                }
                ct++;
            }
           foreach (KeyValuePair<UInt16, byte> line in mem)
           {
                mem.Remove(line.Key);
           }
            encodingFile.Close();
        }


        private void assembleFile_Click(object sender, EventArgs e)
        {
            asamblor.assembleFile(asmFileOpened);
        }

        class Asamblor
        {
            List<string> prelines;
            List<string> instructions;

            bool compiledOk;

            public Asamblor()
            {
                prelines = new List<string>();
                instructions = new List<string>();
            }
            
            public void openAsmFile() {
                contor = 1;
                simulator.instrBox.Text = "";

                StreamReader sr = new StreamReader("program.asm");
                while ((str = sr.ReadLine()) != null)
                {
                    simulator.instrBox.Items.Add(contor + ". " + str);
                    prelines.Add(str);
                    contor++;
                }

                PCMax -= 2;
                asmFileOpened = true;
                sr.Close();
            }

            //precompile and compile asm file
            public void assembleFile(bool asmFileOpened)
            {
                if (asmFileOpened)
                {
                    preCompile(prelines);
                    compile(instructions);
                    if (compiledOk)
                    {

                        MessageBox.Show("Compile process completed with succes!");
                        loadInstructionsIntoRAM();
                    }
                    else
                    {
                        MessageBox.Show("Correct the mistakes and open again the asm file!");
                        prelines.Clear();
                        instructions.Clear();
                        simulator.assembleBox.Items.Clear();
                        simulator.instrBox.Items.Clear();
                    }
                }
                else
                    MessageBox.Show("You opened no asm file! Open a file and try again!!");
            }

            //remove whitespaces and save labels 
            private void preCompile(List<string> prelines)
            {
                PCMax = 0;
                for (int i = 0; i < prelines.Count; i++)
                {
                    //eliminam comentariile
                    int k = prelines[i].IndexOf(";");
                    if (k != -1)
                        prelines[i] = prelines[i].Remove(k);
                    prelines[i] = prelines[i].ToUpper();
                    prelines[i] = prelines[i].Trim(); // remove whitespace from the beginning or ending of the string


                    if (prelines[i].Contains(".DATA") == false && prelines[i].Contains(".CODE") == false && prelines[i].Contains("END") == false)
                    {
                        //salvam etichetele si proc si calc adresele lor
                        if (prelines[i].Contains("ET") == true && prelines[i].Contains(":") == true)
                        {
                            //daca avem eticheta
                            k = prelines[i].IndexOf(":");
                            if (k != -1)
                                prelines[i] = prelines[i].Remove(k);

                            labels[nbLabels].name = prelines[i];
                            labels[nbLabels++].address = PCMax;
                        }
                        else
                            if (prelines[i].Contains("PROC") == true)
                            {
                                //daca avem procedura
                                k = prelines[i].IndexOf(" ");
                                if (k != -1)
                                {
                                    if (prelines[i].Substring(k + 1) != "ENDP")
                                    {
                                        prelines[i] = prelines[i].Substring(0, k);
                                        procedures[nbProcedures].name = prelines[i];
                                        procedures[nbProcedures].address = PCMax;
                                        nbProcedures++;
                                    }
                                }
                            }
                            else
                            {
                                if (prelines[i].Contains("START:") == true)
                                {
                                    k = prelines[i].IndexOf(":");
                                    if (k != -1)
                                        prelines[i] = prelines[i].Remove(k);

                                    labels[nbLabels].name = prelines[i];
                                    labels[nbLabels++].address = PCMax;

                                }
                                else
                                {
                                    //daca nu e eticheta sau procedura sau endp pe rand
                                    PCMax += 2;
                                    instructions.Add(prelines[i]);

                                    isb1 = isb2 = isb3 = isb4 = false;
                                    isJMP = isCALL = false;
                                    opcode = checkInstrType(prelines[i]);

                                    if (isb1)
                                    {
                                        string operandSursa = prelines[i].Substring(prelines[i].IndexOf(",") + 1);
                                        checkMA(operandSursa);

                                        if (modAdresare == 0 || modAdresare == 3)
                                            PCMax += 2;
                                        string s1 = prelines[i].Substring(0, prelines[i].IndexOf(","));
                                        string operandDestinatie = s1.Substring(s1.IndexOf(" ") + 1);
                                        checkMA(operandDestinatie);
                                        if (modAdresare == 3)
                                            PCMax += 2;
                                    }

                                    if (isb2)
                                    {
                                        checkMA(prelines[i].Substring(prelines[i].IndexOf(" ") + 1));
                                        if (isJMP || isCALL)
                                            PCMax += 2;
                                        if (modAdresare == 3)
                                            PCMax += 2;
                                    }
                                }
                            }
                    }
                }
            }

            //compile asm file(instructions) and write codification in binary file
            private void compile(List<string> instructions)
            {
                StreamWriter fisIesire = new StreamWriter("output.txt");

                string fout = "codificare.bin";
                FileStream fs = new FileStream(fout, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);

                PCMax = 0;
                compiledOk = true;

                foreach (string instr in instructions)
                {
                    isb1 = isb2 = isb3 = isb4 = false;
                    isError = false;
                    isValImm = isIndexDest = isIndexSrc = false;
                    isJMP = isCALL = isPUSHRi = isPOPRi = false;
                    codif = valoareImediata = valImediataBinar = valIndex = valIndexSursa = valIndexDest = null;

                    opcode = checkInstrType(instr);

                    if (opcode != null)
                    {
                        codif = opcode;
                    }
                    else
                    {
                        compiledOk = false;
                        MessageBox.Show("Invalid instruction: " + instr);
                        break;
                    }

                    #region clasa base1
                    //clasa base1
                    if (isb1)
                    {
                        string operandSursa = instr.Substring(instr.IndexOf(",") + 1);
                        checkMA(operandSursa); // mod adresare operand sursa

                        switch (modAdresare)
                        {
                            case 0:
                                {
                                    if (operandSursa.Contains("ET") == true || operandSursa.Contains("START") == true)
                                    {
                                        compiledOk = false;
                                        MessageBox.Show("Invalid instruction: " + instr);
                                    }
                                    else
                                    {
                                        codif += IM;
                                        isValImm = true;
                                        valImediataBinar = Convert_Binary(valoareImediata, 16);
                                        codif += "0000";
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    bool regGasit = false;
                                    codif += AD;
                                    foreach (KeyValuePair<string, string> r in registers)
                                    {
                                        if (r.Key == registru)
                                        {
                                            regGasit = true;
                                            codif += r.Value;
                                        }
                                    }
                                    if (!regGasit)
                                    {
                                        compiledOk = false;
                                        MessageBox.Show("Invalid source register in instruction: " + instr);
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    bool regGasit = false;
                                    codif += AI;
                                    foreach (KeyValuePair<string, string> r in registers)
                                    {
                                        if (r.Key == registru)
                                        {
                                            regGasit = true;
                                            codif += r.Value;
                                        }
                                    }
                                    if (!regGasit)
                                    {
                                        compiledOk = false;
                                        MessageBox.Show("Invalid source register in instruction: " + instr);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    bool regGasit = false;
                                    codif += AX;
                                    foreach (KeyValuePair<string, string> r in registers)
                                    {
                                        if (r.Key == registru)
                                        {
                                            regGasit = true;
                                            codif += r.Value;
                                        }
                                    }
                                    if (!regGasit)
                                    {
                                        compiledOk = false;
                                        MessageBox.Show("Invalid source register in instruction: " + instr);
                                    }
                                    if (Convert.ToInt32(valIndex) >= -32767 && Convert.ToInt32(valIndex) <= 32767)
                                    {
                                        isIndexSrc = true;
                                        valIndexSursa = Convert_Binary(valIndex, 16);
                                    }
                                    else
                                    {
                                        compiledOk = false;
                                        MessageBox.Show("Index not in range (-32767,+32767) in instruction: " + instr);
                                    }
                                    break;
                                }
                            default:
                                {
                                    compiledOk = false;
                                    MessageBox.Show("Invalid instruction: " + instr);
                                    break;
                                }
                        }

                        string s1 = instr.Substring(0, instr.IndexOf(","));
                        string operandDestinatie = s1.Substring(s1.IndexOf(" ") + 1);
                        checkMA(operandDestinatie); //mod adresare operand destinatie

                        switch (modAdresare)
                        {
                            case 1:
                                {
                                    bool regGasit = false;
                                    codif += AD;
                                    foreach (KeyValuePair<string, string> r in registers)
                                    {
                                        if (r.Key == registru)
                                        {
                                            codif += r.Value;
                                            regGasit = true;
                                        }
                                    }
                                    if (!regGasit)
                                    {
                                        compiledOk = false;
                                        MessageBox.Show("Invalid destination register in instruction: " + instr);
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    bool regGasit = false;
                                    codif += AI;
                                    foreach (KeyValuePair<string, string> r in registers)
                                    {
                                        if (r.Key == registru)
                                        {
                                            codif += r.Value;
                                            regGasit = true;
                                        }
                                    }
                                    if (!regGasit)
                                    {
                                        compiledOk = false;
                                        MessageBox.Show("Invalid destination register in instruction: " + instr);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    bool regGasit = false;
                                    codif += AX;
                                    foreach (KeyValuePair<string, string> r in registers)
                                    {
                                        if (r.Key == registru)
                                        {
                                            codif += r.Value;
                                            regGasit = true;
                                        }
                                    }
                                    if (!regGasit)
                                    {
                                        compiledOk = false;
                                        MessageBox.Show("Invalid destination register in instruction: " + instr);
                                    }
                                    if (Convert.ToInt32(valIndex) >= -32767 && Convert.ToInt32(valIndex) <= 32767)
                                    {
                                        isIndexDest = true;
                                        valIndexDest = Convert_Binary(valIndex, 16);
                                    }
                                    else
                                    {
                                        compiledOk = false;
                                        MessageBox.Show("Index not in range (-32767,+32767) in instruction: " + instr);
                                    }
                                    break;
                                }
                            default:
                                {
                                    compiledOk = false;
                                    MessageBox.Show("Invalid instruction: " + instr);
                                    break;
                                }
                        }
                        if (compiledOk)
                        {
                            fisIesire.WriteLine(codif);
                            bw.Write(Convert.ToInt16(codif, 2));
                            // assembleBox.Items.Add(codif);
                            if (isValImm)
                            {
                                fisIesire.WriteLine(valImediataBinar);
                                bw.Write(Convert.ToInt16(valImediataBinar, 2));
                                PCMax += 2;
                            }
                            if (isIndexSrc)
                            {
                                fisIesire.WriteLine(valIndexSursa);
                                bw.Write(Convert.ToInt16(valIndexSursa, 2));
                                PCMax += 2;
                            }
                            if (isIndexDest)
                            {
                                fisIesire.WriteLine(valIndexDest);
                                bw.Write(Convert.ToInt16(valIndexDest, 2));
                                PCMax += 2;
                            }
                        }
                        else
                            break;
                    }
                    #endregion

                    #region clasa base2
                    //clasa base2
                    if (isb2)
                    {
                        checkMA(instr.Substring(instr.IndexOf(" ") + 1)); //mod adresare destinatie

                        switch (modAdresare)
                        {
                            case 0:
                                {
                                    if (isJMP || isCALL)
                                    {
                                        codif += IM;
                                        isValImm = true;
                                        valImediataBinar = Convert_Binary(valoareImediata, 16);
                                        codif += "0000";
                                    }
                                    else
                                    {
                                        compiledOk = false;
                                        MessageBox.Show("Immediate value not allowed in base2 instruction: " + instr);
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    if (!isJMP && !isCALL)
                                    {
                                        codif += AD;
                                        bool regGasit = false;
                                        foreach (KeyValuePair<string, string> r in registers)
                                        {
                                            if (r.Key == registru)
                                            {
                                                codif += r.Value;
                                                regGasit = true;
                                            }
                                        }
                                        if (!regGasit)
                                        {
                                            compiledOk = false;
                                            MessageBox.Show("Invalid destination register in instruction: " + instr);
                                        }
                                    }
                                    else
                                    {
                                        compiledOk = false;
                                        MessageBox.Show("JMP or CALL incorrect: " + instr);
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    if (!isPUSHRi && !isPOPRi)
                                    {
                                        bool regGasit = false;
                                        codif += AI;
                                        foreach (KeyValuePair<string, string> r in registers)
                                        {
                                            if (r.Key == registru)
                                            {
                                                codif += r.Value;
                                                regGasit = true;
                                            }
                                        }
                                        if (!regGasit)
                                        {
                                            compiledOk = false;
                                            MessageBox.Show("Invalid destination register in instruction: " + instr);
                                        }
                                    }
                                    else
                                    {
                                        compiledOk = false;
                                        MessageBox.Show("PUSH Ri or POP Ri incorrect: " + instr);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (!isPUSHRi && !isPOPRi)
                                    {
                                        bool regGasit = false;
                                        codif += AX;
                                        foreach (KeyValuePair<string, string> r in registers)
                                        {
                                            if (r.Key == registru)
                                            {
                                                codif += r.Value;
                                                regGasit = true;
                                            }
                                        }
                                        if (!regGasit)
                                        {
                                            compiledOk = false;
                                            MessageBox.Show("Invalid destination register in instruction: " + instr);
                                        }
                                        if (Convert.ToInt32(valIndex) >= -32767 && Convert.ToInt32(valIndex) <= 32767)
                                        {
                                            isIndexDest = true;
                                            valIndexDest = Convert_Binary(valIndex, 16);
                                        }
                                        else
                                        {
                                            compiledOk = false;
                                            MessageBox.Show("Index not in range (-32767,+32767) in instruction: " + instr);
                                        }
                                    }
                                    else
                                    {
                                        compiledOk = false;
                                        MessageBox.Show("PUSH Ri or POP Ri incorrect: " + instr);
                                    }
                                    break;
                                }
                            default:
                                {
                                    compiledOk = false;
                                    MessageBox.Show("Invalid instruction: " + instr);
                                    break;
                                }
                        }
                        if (compiledOk)
                        {
                            fisIesire.WriteLine(codif);
                            bw.Write(Convert.ToInt16(codif, 2));
                            // assembleBox.Items.Add(codif);
                            if (isValImm)
                            {
                                fisIesire.WriteLine(valImediataBinar);
                                bw.Write(Convert.ToInt16(valImediataBinar, 2));
                                PCMax += 2;
                            }
                            if (isIndexDest)
                            {
                                fisIesire.WriteLine(valIndexDest);
                                bw.Write(Convert.ToInt16(valIndexDest, 2));
                                PCMax += 2;
                            }
                        }
                        else
                            break;
                    }
                    #endregion

                    #region clasa base3
                    //clasa base3
                    if (isb3)
                    {
                        checkMA(instr.Substring(instr.IndexOf(" ") + 1)); //mod adresare

                        if (modAdresare == 0)
                        {
                            int val = Convert.ToInt16(valoareImediata);
                            offset = val - (PCMax + 2);
                            if ((offset >= -127) && (offset <= 127))
                            {
                                offsetString = Convert_Binary(offset.ToString(), 8);
                            }
                            else
                            {
                                compiledOk = false;
                                MessageBox.Show("Jump address to high. It is not in range (-127,127)! on instruction: " + instr);
                            }
                        }
                        else
                        {
                            compiledOk = false;
                            MessageBox.Show("Invalid base3 instruction: " + instr);
                        }
                        if (compiledOk)
                        {
                            codif += offsetString;
                            fisIesire.WriteLine(codif);
                            bw.Write(Convert.ToInt16(codif, 2));
                            // assembleBox.Items.Add(codif);
                        }
                        else
                            break;
                    }
                    #endregion

                    #region clasa base4
                    if (isb4)
                    {
                        fisIesire.WriteLine(codif);
                        bw.Write(Convert.ToInt16(codif, 2));
                        // assembleBox.Items.Add(codif);
                    }
                    #endregion

                    if (compiledOk)
                    {
                        fisIesire.Flush();
                        PCMax += 2;
                    }
                    else
                        break;
                }
                fisIesire.Close();
                bw.Close();
            }

            //check what instruction type has current instruction processed
            private string checkInstrType(string instr)
            {
                foreach (KeyValuePair<string, string> t4 in base4) // clasa base4?
                {
                    if (t4.Key == instr)
                    {
                        isb4 = true;
                        return t4.Value; // return opcode-ul instructiunii
                    }
                }

                char[] separators = { ' ' };
                string[] values = instr.Split(separators);

                foreach (KeyValuePair<string, string> t1 in base1) //clasa base1?
                {
                    if (t1.Key == values[0])
                    {
                        isb1 = true;
                        return t1.Value;
                    }
                }

                foreach (KeyValuePair<string, string> t2 in base2) //clasa base2?
                {
                    if (t2.Key == values[0])
                    {
                        isb2 = true;
                        if (values[0] == "JMP")
                            isJMP = true;
                        if (values[0] == "CALL")
                            isCALL = true;
                        if (values[0] == "PUSH")
                            isPUSHRi = true;
                        if (values[0] == "POP")
                            isPOPRi = true;
                        return t2.Value;
                    }
                }
                foreach (KeyValuePair<string, string> t3 in base3) // clasa base3?
                {
                    if (t3.Key == values[0])
                    {
                        isb3 = true;
                        return t3.Value;
                    }
                }
                return null;
            }

            //check the address mode of the current instruction
            private void checkMA(string s)
            {
                if ((s.Length >= 2 && s.Substring(0, 2) == "ET") || (s.Length >= 5 && s.Substring(0, 5) == "START"))
                {
                    modAdresare = 0;
                    foreach (label l in labels)
                    {
                        if (l.name == s)
                        {
                            valoareImediata = l.address.ToString();
                        }
                    }
                }
                else
                {
                    if (s.IndexOf("R") != -1) //adresare AD,AI,AX
                    {
                        if (s.IndexOf(")") == -1)
                        {//adresare AD
                            modAdresare = 1;
                            registru = s;
                        }
                        else
                        {//AI sau AX
                            if (s.IndexOf("(") == 0)
                            {//AI
                                modAdresare = 2;
                                s = s.Remove(s.IndexOf(")"));
                                registru = s.Substring(1);
                            }
                            else
                            {//AX
                                modAdresare = 3;
                                valIndex = s.Substring(0, s.IndexOf("("));
                                s = s.Substring(s.IndexOf("(") + 1);
                                registru = s.Substring(0, s.IndexOf(")"));
                            }
                        }
                    }
                    else
                    {//AM
                        modAdresare = 0;
                        valoareImediata = s;
                    }
                }
            }

            //convert a string into binary of nr bits 
            //returns a string containing the bits
            private string Convert_Binary(string st, int nr)
            {
                Int64 adresa = Convert.ToInt64(st);
                Int64 n = adresa;
                Int64 reg = 0;
                string str = "";
                string str_reg = "";

                if (n < 0)
                {
                    n = (-1) * n - 1;
                }
                for (int i = 0; i < nr; i++)
                {
                    reg = n % 2;
                    if (adresa < 0)
                    {
                        if (reg == 0)
                            reg = 1;
                        else
                        {
                            if (reg == 1)
                                reg = 0;
                        }
                    }
                    n = n / 2;
                    str += Convert.ToString(reg);
                }

                for (int i = 0; i < nr; i++)
                {
                    str_reg += str.Substring(nr - 1 - i, 1);
                }
                return str_reg;
            }

            //load instructions into RAM MEMORY after compilation
            private void loadInstructionsIntoRAM()
            {
                string fin = "codificare.bin";
                FileStream fs = new FileStream(fin, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);

                int counter = 0;
                int pos = 0;
                byte length = (byte)br.BaseStream.Length;
                while (pos < length)
                {
                    RAM[counter] = br.ReadByte();
                    mem.Add((UInt16)counter, RAM[counter]);
                    pos += sizeof(byte);
                    counter++;
                }

                br.Close();
            }
        }

        private void openAsmFile_Click(object sender, EventArgs e)
        {
            asamblor.openAsmFile();
        }
    }
}
