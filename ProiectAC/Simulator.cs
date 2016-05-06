using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using Microsoft.VisualBasic.PowerPacks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ProiectAC
{
    [Serializable]
    [ComVisibleAttribute(true)]
    public partial class Simulator : Form
    {
        private Asamblor asamblor;

        // declarations
        #region declaratii

        
        byte[] RAM = new byte[65536];
        static UInt16 IR = 0;
        static UInt16 FLAG = 0;
        static UInt16 IVR = 0;
        static UInt16 T = 0;
        static UInt16 SP = 65534;
        static UInt16 MDR = 0;
        static UInt16 ADR = 0;
        static UInt16 PC = 0;
        UInt16 PCMax = 0; // PC Maxim, in functie de cate instructiuni sunt in fisier
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


        Dictionary<string, string> registreIndex = new Dictionary<string, string>();
        Dictionary<UInt16, byte> mem = new Dictionary<UInt16, byte>();
 
        List<string> microcod = new List<string>();

        bool microCodeOpened = false;
        static bool isStep;
        static bool isINT;
        static Simulator simulator;
        //Form2 memory;

        #endregion

        public Simulator()
        {
            InitializeComponent();
            asamblor = new Asamblor(this);
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
            this.mem.Add((UInt16)counter, line);
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
    }
}
