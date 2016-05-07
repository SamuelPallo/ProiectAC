using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProiectAC
{
    public partial class MicroArhitecture : Form
    {
        Simulator sim;

        public MicroArhitecture(Simulator sim)
        {
            InitializeComponent();
            this.sim = sim;
        }

        #region Graphics

        public void PdIROffs()
        {
            IRlabel.BackColor = Color.Red;
            PdIRsline.BorderColor = Color.Red;
            SBUSline.FillColor = Color.Red;
            SBUSlabel.ForeColor = Color.Red;
        }

        public void PdIROffd()
        {
            IRlabel.BackColor = Color.Red;
            PdIRdline.BorderColor = Color.Red;
            DBUSline.FillColor = Color.Red;
            DBUSlabel.ForeColor = Color.Red;
        }

        public void PdFLAGs()
        {
            FLAGlabel.BackColor = Color.Red;
            PdFLAGsline.BorderColor = Color.Red;
            SBUSline.FillColor = Color.Red;
            SBUSlabel.ForeColor = Color.Red;
        }

        public void PdFLAGd()
        {
            FLAGlabel.BackColor = Color.Red;
            PdFLAGdline.BorderColor = Color.Red;
            DBUSline.FillColor = Color.Red;
            DBUSlabel.ForeColor = Color.Red;
        }


        public void PdSPs()
        {
            SPlabel.BackColor = Color.Red;
            PdSPsline.BorderColor = Color.Red;
            SBUSline.FillColor = Color.Red;
            SBUSlabel.ForeColor = Color.Red;
        }

        public void PdSPd()
        {
            SPlabel.BackColor = Color.Red;
            PdSPdline.BorderColor = Color.Red;
            DBUSline.FillColor = Color.Red;
            DBUSlabel.ForeColor = Color.Red;
        }

        public void PdTs()
        {
            PdTslabel.Text = "PdTs";
            Tlabel.BackColor = Color.Red;
            PdTsline.BorderColor = Color.Red;
            SBUSline.FillColor = Color.Red;
            SBUSlabel.ForeColor = Color.Red;
        }

        public void PdTd()
        {
            PdTdlabel.Text = "PdTd";
            Tlabel.BackColor = Color.Red;
            PdTdline.BorderColor = Color.Red;
            DBUSline.FillColor = Color.Red;
            DBUSlabel.ForeColor = Color.Red;
        }

        public void PdnTs()
        {
            PdTslabel.Text = "PdnTs";
            Tlabel.BackColor = Color.Red;
            PdTsline.BorderColor = Color.Red;
            SBUSline.FillColor = Color.Red;
            SBUSlabel.ForeColor = Color.Red;
        }

        public void PdnTd()
        {
            PdTdlabel.Text = "PdnTd";
            Tlabel.BackColor = Color.Red;
            PdTdline.BorderColor = Color.Red;
            DBUSline.FillColor = Color.Red;
            DBUSlabel.ForeColor = Color.Red;
        }

        public void PdPCs()
        {
            PClabel.BackColor = Color.Red;
            PdPCsline.BorderColor = Color.Red;
            SBUSline.FillColor = Color.Red;
            SBUSlabel.ForeColor = Color.Red;
        }

        public void PdPCd()
        {
            PClabel.BackColor = Color.Red;
            PdPCdline.BorderColor = Color.Red;
            DBUSline.FillColor = Color.Red;
            DBUSlabel.ForeColor = Color.Red;
        }

        public void PdIVRs()
        {
            IVRlabel.BackColor = Color.Red;
            PdIVRsline.BorderColor = Color.Red;
            SBUSline.FillColor = Color.Red;
            SBUSlabel.ForeColor = Color.Red;
        }

        public void PdIVRd()
        {
            IVRlabel.BackColor = Color.Red;
            PdIVRdline.BorderColor = Color.Red;
            DBUSline.FillColor = Color.Red;
            DBUSlabel.ForeColor = Color.Red;
        }

        public void PdADRs()
        {
            ADRlabel.BackColor = Color.Red;
            PdADRsline1.BorderColor = Color.Red;
            PdADRsline2.BorderColor = Color.Red;
            SBUSline.FillColor = Color.Red;
            SBUSlabel.ForeColor = Color.Red;
        }

        public void PdADRd()
        {
            ADRlabel.BackColor = Color.Red;
            PdADRdline.BorderColor = Color.Red;
            DBUSline.FillColor = Color.Red;
            DBUSlabel.ForeColor = Color.Red;
        }

        public void PdMDRs()
        {
            MDRlabel.BackColor = Color.Red;
            PdMDRsline1.BorderColor = Color.Red;
            PdMDRsline2.BorderColor = Color.Red;
            SBUSline.FillColor = Color.Red;
            SBUSlabel.ForeColor = Color.Red;
        }

        public void PdMDRd()
        {
            MDRlabel.BackColor = Color.Red;
            PdMDRdline.BorderColor = Color.Red;
            DBUSline.FillColor = Color.Red;
            DBUSlabel.ForeColor = Color.Red;
        }

        public void PdRg0()
        {
            R0label.BackColor = Color.Red;
        }

        public void PdRg1()
        {
            R1label.BackColor = Color.Red;
        }

        public void PdRg2()
        {
            R2label.BackColor = Color.Red;
        }

        public void PdRg3()
        {
            R3label.BackColor = Color.Red;
        }

        public void PdRg4()
        {
            R4label.BackColor = Color.Red;
        }

        public void PdRg5()
        {
            R5label.BackColor = Color.Red;
        }

        public void PdRg6()
        {
            R6label.BackColor = Color.Red;
        }

        public void PdRg7()
        {
            R7label.BackColor = Color.Red;
        }

        public void PdRg8()
        {
            R8label.BackColor = Color.Red;
        }

        public void PdRg9()
        {
            R9label.BackColor = Color.Red;
        }

        public void PdRg10()
        {
            R10label.BackColor = Color.Red;
        }

        public void PdRg11()
        {
            R11label.BackColor = Color.Red;
        }

        public void PdRg12()
        {
            R12label.BackColor = Color.Red;
        }

        public void PdRg13()
        {
            R13label.BackColor = Color.Red;
        }

        public void PdRg14()
        {
            R14label.BackColor = Color.Red;
        }

        public void PdRg15()
        {
            R15label.BackColor = Color.Red;
        }

        public void PdRgs()
        {
            PdRGsline.BorderColor = Color.Red;
            SBUSline.FillColor = Color.Red;
            SBUSlabel.ForeColor = Color.Red;
        }

        public void PdRgd()
        {
            PdRGdline.BorderColor = Color.Red;
            DBUSline.FillColor = Color.Red;
            DBUSlabel.ForeColor = Color.Red;
        }

        public void Pd0s()
        {
            Pd0sline.BorderColor = Color.Red;
            zerolabel.BackColor = Color.Red;
            SBUSline.FillColor = Color.Red;
            SBUSlabel.ForeColor = Color.Red;
        }

        public void Pd0d()
        {
            Pd0dline.BorderColor = Color.Red;
            zerolabel.BackColor = Color.Red;
            DBUSline.FillColor = Color.Red;
            DBUSlabel.ForeColor = Color.Red;
        }

        public void Pdm1s()
        {
            Pdminussline.BorderColor = Color.Red;
            minusunulabel.BackColor = Color.Red;
            SBUSline.FillColor = Color.Red;
            SBUSlabel.ForeColor = Color.Red;
        }

        public void Pd1s()
        {
            Pd1sline.BorderColor = Color.Red;
            unulabel.BackColor = Color.Red;
            SBUSline.FillColor = Color.Red;
            SBUSlabel.ForeColor = Color.Red;
        }

        public void ActivateCIN()
        {
            CINlabel.BackColor = Color.Red;
            CINline.BorderColor = Color.Red;
        }

        public void Alu()
        {
            ALUline1.BorderColor = Color.Red;
            ALUline2.BorderColor = Color.Red;
            PdAluline.BorderColor = Color.Red;
            RBUSShape1.FillColor = Color.Red;
            RBUSShape2.FillColor = Color.Red;
            RBUSShape3.FillColor = Color.Red;
            RBUSlabel.ForeColor = Color.Red;
        }

        public void PmFLAG()
        {
            PmFLAGline.BorderColor = Color.Red;
            PmFLAGline1.BorderColor = Color.Red;
            FLAGlabel.BackColor = Color.Red;
        }

        public void PmSP()
        {
            PmSPline.BorderColor = Color.Red;
            SPlabel.BackColor = Color.Red;
        }

        public void PmT()
        {
            PmTline.BorderColor = Color.Red;
            Tlabel.BackColor = Color.Red;
        }

        public void PmPC()
        {
            PmPCline.BorderColor = Color.Red;
            PClabel.BackColor = Color.Red;
        }

        public void PmIVR()
        {
            PmIVRline.BorderColor = Color.Red;
            IVRlabel.BackColor = Color.Red;
        }

        public void PmADR()
        {
            PmADRline.BorderColor = Color.Red;
            ADRlabel.BackColor = Color.Red;
        }

        public void PmMDR()
        {
            PmMDRline1.BorderColor = Color.Red;
            PmMDRline2.BorderColor = Color.Red;
            MDRlabel.BackColor = Color.Red;
        }

        public void PdCond()
        {
            PdCondline1.BorderColor = Color.Red;
            PdCondline2.BorderColor = Color.Red;
            PdCondline3.BorderColor = Color.Red;
            PmFLAGline1.BorderColor = Color.Red;
            FLAGlabel.BackColor = Color.Red;
        }

        public void SPinc()
        {
            SPlabel.BackColor = Color.Red;
        }

        public void SPdec()
        {
            SPlabel.BackColor = Color.Red;
        }

        public void PCinc()
        {
            PClabel.BackColor = Color.Red;
        }

        public void Ifch()
        {
            PdADRsline2.BorderColor = Color.Red;
            ADRline1.BorderColor = Color.Red;
            ADRline2.BorderColor = Color.Red;
            DOline1.BorderColor = Color.Red;
            DOline5.BorderColor = Color.Red;
            DOline6.BorderColor = Color.Red;
            IRlabel.BackColor = Color.Red;
        }

        public void Read()
        {
            PdADRsline2.BorderColor = Color.Red;
            ADRline1.BorderColor = Color.Red;
            ADRline2.BorderColor = Color.Red;
            DOline1.BorderColor = Color.Red;
            DOline2.BorderColor = Color.Red;
            DOline3.BorderColor = Color.Red;
            PmMDRline2.BorderColor = Color.Red;
            PmMDRline1.BorderColor = Color.Red;
            MDRlabel.BackColor = Color.Red;
        }

        public void Write()
        {
            PdADRsline2.BorderColor = Color.Red;
            ADRline1.BorderColor = Color.Red;
            ADRline2.BorderColor = Color.Red;
            PdMDRsline2.BorderColor = Color.Red;
            DataInline1.BorderColor = Color.Red;
            DataInline2.BorderColor = Color.Red;
            MemShape.FillColor = Color.Red;
        }
        #endregion


        public void resetGraphics()
        {
            MemShape.FillColor = SystemColors.InactiveCaption;
            SBUSline.FillColor = Color.SteelBlue;
            DBUSline.FillColor = Color.SteelBlue;
            RBUSShape1.FillColor = Color.SteelBlue;
            RBUSShape2.FillColor = Color.SteelBlue;
            RBUSShape3.FillColor = Color.SteelBlue;
            PmPCline.BorderColor = Color.RoyalBlue;
            PClabel.BackColor = SystemColors.ActiveCaption;
            PdPCdline.BorderColor = Color.RoyalBlue;
            PdPCsline.BorderColor = Color.RoyalBlue;
            PmIVRline.BorderColor = Color.RoyalBlue;
            IVRlabel.BackColor = SystemColors.ActiveCaption;
            PdIRdline.BorderColor = Color.RoyalBlue;
            PdIVRsline.BorderColor = Color.RoyalBlue;
            PmTline.BorderColor = Color.RoyalBlue;
            Tlabel.BackColor = SystemColors.ActiveCaption;
            PdTsline.BorderColor = Color.RoyalBlue;
            PdTdline.BorderColor = Color.RoyalBlue;
            PmSPline.BorderColor = Color.RoyalBlue;
            SPlabel.BackColor = SystemColors.ActiveCaption;
            PdSPsline.BorderColor = Color.RoyalBlue;
            PdSPdline.BorderColor = Color.RoyalBlue;
            PdFLAGdline.BorderColor = Color.RoyalBlue;
            PdFLAGsline.BorderColor = Color.RoyalBlue;
            FLAGlabel.BackColor = SystemColors.ActiveCaption;
            PmFLAGline.BorderColor = Color.RoyalBlue;
            PmFLAGline1.BorderColor = Color.RoyalBlue;
            PdCondline1.BorderColor = Color.RoyalBlue;
            PdCondline2.BorderColor = Color.RoyalBlue;
            PdCondline3.BorderColor = Color.RoyalBlue;
            PdAluline.BorderColor = Color.RoyalBlue;
            ALUlabel.Text = "NONE";
            ALUline1.BorderColor = Color.DarkOrange;
            ALUline2.BorderColor = Color.DarkOrange;
            CINline.BorderColor = Color.RoyalBlue;
            CINlabel.BackColor = SystemColors.ActiveCaption;
            unulabel.BackColor = SystemColors.ActiveCaption;
            minusunulabel.BackColor = SystemColors.ActiveCaption;
            zerolabel.BackColor = SystemColors.ActiveCaption;
            Pd1sline.BorderColor = Color.RoyalBlue;
            Pdminussline.BorderColor = Color.RoyalBlue;
            Pd0dline.BorderColor = Color.RoyalBlue;
            Pd0sline.BorderColor = Color.RoyalBlue;
            PdRGsline.BorderColor = Color.RoyalBlue;
            PdRGdline.BorderColor = Color.RoyalBlue;
            PmRGline.BorderColor = Color.RoyalBlue;
            R0label.BackColor = SystemColors.ActiveCaption;
            R1label.BackColor = SystemColors.ActiveCaption;
            R2label.BackColor = SystemColors.ActiveCaption;
            R3label.BackColor = SystemColors.ActiveCaption;
            R4label.BackColor = SystemColors.ActiveCaption;
            R5label.BackColor = SystemColors.ActiveCaption;
            R6label.BackColor = SystemColors.ActiveCaption;
            R7label.BackColor = SystemColors.ActiveCaption;
            R8label.BackColor = SystemColors.ActiveCaption;
            R9label.BackColor = SystemColors.ActiveCaption;
            R10label.BackColor = SystemColors.ActiveCaption;
            R11label.BackColor = SystemColors.ActiveCaption;
            R12label.BackColor = SystemColors.ActiveCaption;
            R13label.BackColor = SystemColors.ActiveCaption;
            R14label.BackColor = SystemColors.ActiveCaption;
            R15label.BackColor = SystemColors.ActiveCaption;
            PdTslabel.Text = "PdTs";
            PdIRdline.BorderColor = Color.RoyalBlue;
            PdIRsline.BorderColor = Color.RoyalBlue;
            IRlabel.BackColor = SystemColors.ActiveCaption;
            PdADRsline1.BorderColor = Color.RoyalBlue;
            PdADRsline2.BorderColor = Color.RoyalBlue;
            PdADRdline.BorderColor = Color.RoyalBlue;
            ADRline1.BorderColor = Color.RoyalBlue;
            ADRline2.BorderColor = Color.RoyalBlue;
            PmADRline.BorderColor = Color.RoyalBlue;
            ADRlabel.BackColor = SystemColors.ActiveCaption;
            PdMDRsline1.BorderColor = Color.RoyalBlue;
            PdMDRsline2.BorderColor = Color.RoyalBlue;
            PdMDRdline.BorderColor = Color.RoyalBlue;
            DataInline1.BorderColor = Color.RoyalBlue;
            DataInline2.BorderColor = Color.RoyalBlue;
            MDRlabel.BackColor = SystemColors.ActiveCaption;
            PmMDRline1.BorderColor = Color.RoyalBlue;
            PmMDRline2.BorderColor = Color.RoyalBlue;
            DOline1.BorderColor = Color.RoyalBlue;
            DOline2.BorderColor = Color.RoyalBlue;
            DOline3.BorderColor = Color.RoyalBlue;
            DOline4.BorderColor = Color.RoyalBlue;
            DOline5.BorderColor = Color.RoyalBlue;
            DOline6.BorderColor = Color.RoyalBlue;
            SBUSlabel.ForeColor = Color.RoyalBlue;
            DBUSlabel.ForeColor = Color.RoyalBlue;
            RBUSlabel.ForeColor = Color.RoyalBlue;
        }

        public void resetRegisters()
        {
            PCtext.Text = "0000000000000000";
            IRtext.Text = "0000000000000000";
            SPtext.Text = sim.Convert_Binary(65534.ToString(), 16);
            ADRtext.Text = "0000000000000000";
            FLAGtext.Text = "0000000000000000";
            Ttext.Text = "0000000000000000";
            IVRtext.Text = "0000000000000000";
            MDRtext.Text = "0000000000000000";
            R0text.Text = "0000000000000000";
            R1text.Text = "0000000000000000";
            R2text.Text = "0000000000000000";
            R3text.Text = "0000000000000000";
            R4text.Text = "0000000000000000";
            R5text.Text = "0000000000000000";
            R6text.Text = "0000000000000000";
            R7text.Text = "0000000000000000";
            R8text.Text = "0000000000000000";
            R9text.Text = "0000000000000000";
            R10text.Text = "0000000000000000";
            R11text.Text = "0000000000000000";
            R12text.Text = "0000000000000000";
            R13text.Text = "0000000000000000";
            R14text.Text = "0000000000000000";
            R15text.Text = "0000000000000000";
        }

        private void closeArhitectureButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
