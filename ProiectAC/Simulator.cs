using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProiectAC
{
    public partial class Simulator : Form
    {


        // declarations
        #region declaratii

        Asamblor asamblor;
        Microcode microcode;
        MicroArhitecture arhitecture;
        Sequencer sequencer;
        Memory memory;

        byte[] RAM = new byte[65536];
        UInt16 PCMax = 0; // PC Maxim, in functie de cate instructiuni sunt in fisier

        UInt64[] MPM = new UInt64[200];

        Dictionary<UInt16, byte> mem = new Dictionary<UInt16, byte>();

        #endregion

        public Simulator()
        {
            InitializeComponent();
            arhitecture = new MicroArhitecture(this);
            arhitecture.Hide();
            asamblor = new Asamblor(this);
            microcode = new Microcode(this);
            sequencer = new Sequencer(this, this.arhitecture);
        }

        public void setPCMax(ushort value) {
            this.PCMax = value;
        }

        public ushort getPCMax()
        {
            return PCMax;
        }

        public void risePCMax(ushort value)
        {
            this.PCMax += value;
        }

        public void subbPCMax(ushort value)
        {
            this.PCMax -= value;
        }

        public void addLineRAM(int contor, byte line) {
            this.RAM[contor] = line;
        }

        public byte getLineRAM(int contor)
        {
            return RAM[contor];
        }

        public void addLineMem(int counter, byte line) {
            try {
                this.mem.Add((UInt16)counter, line);
            } catch (ArgumentException) {
                this.mem[(UInt16)counter] = line;
            }
        }

        public void removeLineMem(KeyValuePair<UInt16, byte> line)
        {
            this.mem.Remove(line.Key);
        }

#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required
        public new Dictionary<UInt16, byte> getEntireMem
#pragma warning restore CS0109 // Member does not hide an inherited member; new keyword is not required
        {
            get
            {
                return this.mem;
            }
        }

        private void assembleFile_Click(object sender, EventArgs e)
        {
            asamblor.assembleFile();
        }

        private void openAsmFile_Click(object sender, EventArgs e)
        {
            asamblor.openAsmFile();
        }

        private void openMicrocodeButton_Click(object sender, EventArgs e)
        {
            microcode.OpenFile();
        }

        public void addValueMPM(int contor, ulong value)
        {
            this.MPM[contor] = value;
        }

        public ulong getValueMPM(int contor) {
            return MPM[contor];
        }

        //convert a string into binary of nr bits 
        //returns a string containing the bits
        public string Convert_Binary(string st, int nr)
        {
            Int64 adresa = Convert.ToInt64(st);
            Int64 n = adresa;
            Int64 reg = 0;
            string lineFile = "";
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
                lineFile += Convert.ToString(reg);
            }

            for (int i = 0; i < nr; i++)
            {
                str_reg += lineFile.Substring(nr - 1 - i, 1);
            }
            return str_reg;
        }


        private void showArhitectureButton_Click(object sender, EventArgs e)
        {
            arhitecture.Show();
        }

        //reinitialize registers
        private void resetButton_Click(object sender, EventArgs e)
        {
            arhitecture.resetGraphics();
            sequencer.resetRegisters();
            this.resetContor();
        }

        private void showMemoryButton_Click(object sender, EventArgs e)
        {
            memory = new Memory();
            memory.Show();

            foreach (KeyValuePair<UInt16, byte> line in mem)   //adaug instructiunile in listbox de memorie
            {
                memory.listBoxMem.Items.Add(line.Key + "     " + Convert_Binary(line.Value.ToString(), 8));
            }
        }

        private void stepButton_Click(object sender, EventArgs e)
        {
            if (asamblor.IsCompiled() && microcode.IsMicrocodeOpened())
            {
                sequencer.setIsStep(true);
                sequencer.runSimulatorStep();
            }
            else
            {
                MessageBox.Show("Something went wrong! Please reload the process!");
            }
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            if (asamblor.IsCompiled() && microcode.IsMicrocodeOpened())
            {
                if (!sequencer.itIsStep())
                {
                    this.resetContor();
                    sequencer.runSimulator();
                }
                else MessageBox.Show("You cannot run the simulator to the end while running step by step!");
            }
            else
            {
                MessageBox.Show("Something went wrong! Please reload the process!");
            }
        }

        public void resetContor() {
            asamblor.resetContor();
        }
    }
}
