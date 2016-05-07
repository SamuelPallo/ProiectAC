using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ProiectAC
{
    class Microcode
    {
        Simulator simulator;
        String lineFile;
        bool microCodeOpened = false;

        List<string> microcode;

        public Microcode(Simulator sim) {
            this.simulator = sim;
            this.microcode = new List<string>();
        }

        //create and open microcode
        public void OpenFile()
        {
            loadMicroCodeText();
            createMicroCodeBinaryFile();
            openMicroCode();
            simulator.resetContor();
        }

        //read txt file with microcode and create binary file
        private void createMicroCodeBinaryFile()
        {
            string fout = "microproprogram_emulare.bin";
            FileStream fs1 = new FileStream(fout, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs1);

            StreamReader sr = new StreamReader("microprogram_emulare_binar.txt");

            while ((lineFile = sr.ReadLine()) != null)
            {
                bw.Write(Convert.ToInt64(lineFile, 2));
            }

            sr.Close();
            bw.Close();
        }

        //open microcode binary file and load the microcode into MPM
        private void openMicroCode()
        {
            string fin = "microproprogram_emulare.bin";
            FileStream fs2 = new FileStream(fin, FileMode.Open);
            BinaryReader bw = new BinaryReader(fs2);

            int counter = 0;
            int pos = 0;
            Int64 length = (Int64)bw.BaseStream.Length;
            while (pos < length)
            {
                simulator.addValueMPM(counter, bw.ReadUInt64());
                pos += sizeof(UInt64);
                counter++;
            }

            microCodeOpened = true;
            MessageBox.Show("Microcode file opened with succes!");
            bw.Close();
        }

        private void loadMicroCodeText()
        {
            StreamReader sr = new StreamReader("microprogram_emulare_pasi.txt");

            while ((lineFile = sr.ReadLine()) != null)
            {
                microcode.Add(lineFile);
            }
            simulator.microcodeBox.DataSource = microcode;

            sr.Close();

        }

        public bool IsMicrocodeOpened()
        {
            return microCodeOpened;
        }
    }
}
