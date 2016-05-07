using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProiectAC
{
    class Sequencer
    {
        Simulator simulator;
        MicroArhitecture arhitecture;

        UInt16 bit = 0;

        UInt16[] R = new UInt16[16];

        UInt64 MIR = 0;
        UInt16 MAR = 0;
        UInt16 SBUS = 0;
        UInt16 DBUS = 0;
        UInt16 RBUS = 0;
        UInt16 Carry = 0;

        UInt16 IR = 0;
        UInt16 FLAG = 0;
        UInt16 IVR = 0;
        UInt16 T = 0;
        UInt16 SP = 65534;
        UInt16 MDR = 0;
        UInt16 ADR = 0;
        UInt16 PC = 0;

        UInt16 reg = 0;
        UInt16 g = 0;
        UInt16 IndexValoare;
        UInt16 clasa;

        int Step = 0;

        bool isINT, isStep;

        public Sequencer(Simulator sim, MicroArhitecture arhi) {
            this.simulator = sim;
            this.arhitecture = arhi;
        }

        //run simulator - one single step
        public void runSimulatorStep()
        {
            Step++;
            if (PC <= simulator.getPCMax())
            {
                switch (Step)
                {
                    case 1:
                        {
                            MIR = simulator.getValueMPM(MAR);
                            simulator.microcodeBox.SetSelected(MAR, true);
                            DecodifSBUS();
                        }
                        break;
                    case 2:
                        DecodifDBUS();
                        break;
                    case 3:
                        DecodifALU();
                        break;
                    case 4:
                        DecodifDestRBUS();
                        break;
                    case 5:
                        DecodifOtherOp();
                        break;
                    case 6:
                        TestIfchReadWrite();
                        break;
                    case 7:
                        {
                            g = TestG();
                            if (g == 1) // MAR = ADRESA_SALT + INDEX
                            {
                                UInt16 Index;
                                // selectie index
                                #region SelectieIndex
                                switch ((MIR & 0x000000000000E00) >> 9)
                                {
                                    case 0x0: //INDEX0 
                                        MAR = (UInt16)(MIR & 0x0000000000FF);
                                        break;
                                    case 0x1: //INDEX1
                                        UInt16 cl = getCl();
                                        switch (cl)
                                        {
                                            case 0:
                                                MAR = (UInt16)(MIR & 0x00000000000000FF);
                                                break;
                                            case 1:
                                                MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + 0x1);
                                                break;
                                            case 2:
                                                MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + 0x3);
                                                break;
                                            case 3:
                                                MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + 0x2);
                                                break;
                                        }
                                        break;
                                    case 0x2: //INDEX2
                                        IndexValoare = (UInt16)((IR & 0x0C00) >> 9);
                                        MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + IndexValoare);
                                        break;
                                    case 0x3: //INDEX3
                                        IndexValoare = (UInt16)((IR & 0x0030) >> 3);
                                        MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + IndexValoare);
                                        break;
                                    case 0x4: //INDEX4
                                        IndexValoare = (UInt16)((IR & 0x7000) >> 11);
                                        MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + IndexValoare);
                                        break;
                                    case 0x5: //INDEX5
                                        IndexValoare = (UInt16)((IR & 0x07C0) >> 5);
                                        MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + IndexValoare);
                                        break;
                                    case 0x6: //INDEX6
                                        IndexValoare = (UInt16)((IR & 0x1F00) >> 7);
                                        MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + IndexValoare);
                                        break;
                                    case 0x7: //INDEX7
                                        IndexValoare = (UInt16)((IR & 0x001F) << 1);
                                        MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + IndexValoare);
                                        break;
                                    default:
                                        break;
                                }
                                #endregion
                            }
                            else
                            { // MAR = MAR + 1
                                MAR = (UInt16)(MAR + 0x1);
                            }
                            //MARtext.Text = Convert_Binary(MAR.ToString(), 16);
                            SBUS = 0;
                            DBUS = 0;
                            RBUS = 0;
                            Step = 0;
                            arhitecture.resetGraphics();
                        }
                        break;
                    default:
                        break;
                }
            }
            else
                MessageBox.Show("The program was successfully simulated!");
        }

        //run sumulator to the end
        public void runSimulator()
        {
            while (PC <= simulator.getPCMax())
            {
                MIR = simulator.getValueMPM(MAR);
                simulator.microcodeBox.SetSelected(MAR, true);
                //MIRtext.Text = Convert_Binary(MIR.ToString(), 64);
                DecodifSBUS();
                DecodifDBUS();
                DecodifALU();
                DecodifDestRBUS();
                DecodifOtherOp();

                g = TestG();
                if (g == 1) // MAR = ADRESA_SALT + INDEX
                {
                    UInt16 Index;
                    // selectie index
                    #region SelectieIndex
                    switch ((MIR & 0x000000000000E00) >> 9)
                    {
                        case 0x0: //INDEX0 
                            MAR = (UInt16)(MIR & 0x0000000000FF);
                            break;
                        case 0x1: //INDEX1
                            int cl = getCl();
                            switch (cl)
                            {
                                case 0:
                                    MAR = (UInt16)(MIR & 0x00000000000000FF);
                                    break;
                                case 1:
                                    MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + 0x1);
                                    break;
                                case 2:
                                    MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + 0x3);
                                    break;
                                case 3:
                                    MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + 0x2);
                                    break;
                            }
                            break;
                        case 0x2: //INDEX2
                            IndexValoare = (UInt16)((IR & 0x0C00) >> 9);
                            MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + IndexValoare);
                            break;
                        case 0x3: //INDEX3
                            IndexValoare = (UInt16)((IR & 0x0030) >> 3);
                            MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + IndexValoare);
                            break;
                        case 0x4: //INDEX4
                            IndexValoare = (UInt16)((IR & 0x7000) >> 11);
                            MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + IndexValoare);
                            break;
                        case 0x5: //INDEX5
                            IndexValoare = (UInt16)((IR & 0x07C0) >> 5);
                            MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + IndexValoare);
                            break;
                        case 0x6: //INDEX6
                            IndexValoare = (UInt16)((IR & 0x1F00) >> 7);
                            MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + IndexValoare);
                            break;
                        case 0x7: //INDEX7
                            IndexValoare = (UInt16)((IR & 0x001F) << 1);
                            MAR = (UInt16)((UInt16)(MIR & 0x00000000000000FF) + IndexValoare);
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                else
                { // MAR = MAR + 1
                    MAR = (UInt16)(MAR + 0x1);
                }
                //MARtext.Text = Convert_Binary(MAR.ToString(), 16);

                TestIfchReadWrite();

                SBUS = 0;
                DBUS = 0;
                RBUS = 0;
                arhitecture.resetGraphics();
            }
            MessageBox.Show("The program was successfully simulated!");
        }

        #region Decodificatoare

        //decodificator camp Sursa SBUS
        private void DecodifSBUS()
        {
            UInt16 campSbus = (UInt16)((MIR & 0x0000007800000000) >> 35);
            switch (campSbus)
            {
                case 0x1: //PdIR[Off]s
                    arhitecture.PdIROffs();
                    SBUS = (UInt16)(IR & 0x00FF);
                    arhitecture.SBUSlabel.Text = simulator.Convert_Binary(SBUS.ToString(), 16);
                    break;

                case 0x2: //PdFLAGs		
                    arhitecture.PdFLAGs();
                    SBUS = (UInt16)(FLAG);
                    arhitecture.SBUSlabel.Text = simulator.Convert_Binary(SBUS.ToString(), 16);
                    break;

                case 0x3: //PdSPs
                    arhitecture.PdSPs();
                    SBUS = (UInt16)(SP);
                    arhitecture.SBUSlabel.Text = simulator.Convert_Binary(SBUS.ToString(), 16);
                    break;

                case 0x4: //PdTs
                    arhitecture.PdTs();
                    SBUS = (UInt16)(T);
                    arhitecture.SBUSlabel.Text = simulator.Convert_Binary(SBUS.ToString(), 16);
                    break;

                case 0x5: //PdnTs	
                    arhitecture.PdnTs();
                    UInt16 t = T;
                    SBUS = (UInt16)(~t);
                    arhitecture.SBUSlabel.Text = simulator.Convert_Binary(SBUS.ToString(), 16);
                    break;

                case 0x6: //PdPCs	
                    arhitecture.PdPCs();
                    SBUS = (UInt16)(PC);
                    arhitecture.SBUSlabel.Text = simulator.Convert_Binary(SBUS.ToString(), 16);
                    break;

                case 0x7: //PdIVRs	
                    arhitecture.PdIVRs();
                    SBUS = (UInt16)(IVR);
                    arhitecture.SBUSlabel.Text = simulator.Convert_Binary(SBUS.ToString(), 16);
                    break;

                case 0x8: //PdADRs		
                    arhitecture.PdADRs();
                    SBUS = (UInt16)(ADR);
                    arhitecture.SBUSlabel.Text = simulator.Convert_Binary(SBUS.ToString(), 16);
                    break;

                case 0x9: //PdMDRs	
                    arhitecture.PdMDRs();
                    SBUS = (UInt16)(MDR);
                    arhitecture.SBUSlabel.Text = simulator.Convert_Binary(SBUS.ToString(), 16);
                    break;

                case 0xA: //PdRGs		
                    int nr_reg = (IR & 0x03C0) >> 6; //preiau reg din camp RS din IR
                    
                    SBUS = (UInt16)(R[nr_reg]);
                    #region registrii
                    switch (nr_reg)
                    {
                        case 0x1:
                            arhitecture.PdRg1();
                            break;
                        case 0x2:
                            arhitecture.PdRg2();
                            break;
                        case 0x3:
                            arhitecture.PdRg3();
                            break;
                        case 0x4:
                            arhitecture.PdRg4();
                            break;
                        case 0x5:
                            arhitecture.PdRg5();
                            break;
                        case 0x6:
                            arhitecture.PdRg6();
                            break;
                        case 0x7:
                            arhitecture.PdRg7();
                            break;
                        case 0x8:
                            arhitecture.PdRg8();
                            break;
                        case 0x9:
                            arhitecture.PdRg9();
                            break;
                        case 0xA:
                            arhitecture.PdRg10();
                            break;
                        case 0xB:
                            arhitecture.PdRg11();
                            break;
                        case 0xC:
                            arhitecture.PdRg12();
                            break;
                        case 0xD:
                            arhitecture.PdRg13();
                            break;
                        case 0xE:
                            arhitecture.PdRg14();
                            break;
                        case 0xF:
                            arhitecture.PdRg15();
                            break;
                        default:
                            arhitecture.PdRg0();
                            break;
                    }
                    #endregion
                    arhitecture.PdRgs();
                    arhitecture.SBUSlabel.Text = simulator.Convert_Binary(SBUS.ToString(), 16);
                    break;

                case 0xB: //Pd0s
                    arhitecture.Pd0s();
                    SBUS = 0;
                    arhitecture.SBUSlabel.Text = simulator.Convert_Binary(SBUS.ToString(), 16);
                    break;

                case 0xC: //Pd-1s
                    arhitecture.Pdm1s();
                    Int16 v = -1;
                    SBUS = (UInt16)v;
                    arhitecture.SBUSlabel.Text = simulator.Convert_Binary(SBUS.ToString(), 16);
                    break;

                case 0xD: //Pd1s
                    arhitecture.Pd1s();
                    SBUS = (UInt16)(1);
                    arhitecture.SBUSlabel.Text = simulator.Convert_Binary(SBUS.ToString(), 16);
                    break;

                default:
                    break;
            }
        }

        //decodificator camp Sursa DBUS
        private void DecodifDBUS()
        {
            UInt16 campDbus = (UInt16)((MIR & 0x0000000780000000) >> 31);
            switch (campDbus)
            {
                case 0x1: //PdIR[Off]d
                    arhitecture.PdIROffd();
                    DBUS = (UInt16)(IR & 0x00FF);
                    arhitecture.DBUSlabel.Text = simulator.Convert_Binary(DBUS.ToString(), 16);
                    break;

                case 0x2: //PdFLAGd
                    arhitecture.PdFLAGd();
                    DBUS = (UInt16)(FLAG);
                    arhitecture.DBUSlabel.Text = simulator.Convert_Binary(DBUS.ToString(), 16);
                    break;

                case 0x3: //PdSPd
                    arhitecture.PdSPd();
                    DBUS = (UInt16)(SP);
                    arhitecture.DBUSlabel.Text = simulator.Convert_Binary(DBUS.ToString(), 16);
                    break;

                case 0x4: //PdTd
                    arhitecture.PdTd();
                    DBUS = (UInt16)(T);
                    arhitecture.DBUSlabel.Text = simulator.Convert_Binary(DBUS.ToString(), 16);
                    break;

                case 0x5: //PdnTd
                    arhitecture.PdnTd();
                    UInt16 t = T;
                    DBUS = (UInt16)(~t);
                    arhitecture.DBUSlabel.Text = simulator.Convert_Binary(DBUS.ToString(), 16);
                    break;

                case 0x6: //PdPCd
                    arhitecture.PdPCd();
                    DBUS = (UInt16)(PC);
                    arhitecture.DBUSlabel.Text = simulator.Convert_Binary(DBUS.ToString(), 16);
                    break;

                case 0x7: //PdIVRd
                    arhitecture.PdIVRd();
                    DBUS = (UInt16)(IVR);
                    arhitecture.DBUSlabel.Text = simulator.Convert_Binary(DBUS.ToString(), 16);
                    break;

                case 0x8: //PdADRd
                    arhitecture.PdADRd();
                    DBUS = (UInt16)(ADR);
                    arhitecture.DBUSlabel.Text = simulator.Convert_Binary(DBUS.ToString(), 16);
                    break;

                case 0x9: //PdMDRd
                    arhitecture.PdMDRd();
                    DBUS = (UInt16)(MDR);
                    arhitecture.DBUSlabel.Text = simulator.Convert_Binary(DBUS.ToString(), 16);
                    break;

                case 0xA: //PdRGd
                    int nr_reg = IR & 0x000F; //preiau reg din camp RS din IR
                    DBUS = Convert.ToUInt16(R[nr_reg]);
                    #region registrii
                    switch (nr_reg)
                    {
                        case 0x1:
                            arhitecture.PdRg1();
                            break;
                        case 0x2:
                            arhitecture.PdRg2();
                            break;
                        case 0x3:
                            arhitecture.PdRg3();
                            break;
                        case 0x4:
                            arhitecture.PdRg4();
                            break;
                        case 0x5:
                            arhitecture.PdRg5();
                            break;
                        case 0x6:
                            arhitecture.PdRg6();
                            break;
                        case 0x7:
                            arhitecture.PdRg7();
                            break;
                        case 0x8:
                            arhitecture.PdRg8();
                            break;
                        case 0x9:
                            arhitecture.PdRg9();
                            break;
                        case 0xA:
                            arhitecture.PdRg10();
                            break;
                        case 0xB:
                            arhitecture.PdRg11();
                            break;
                        case 0xC:
                            arhitecture.PdRg12();
                            break;
                        case 0xD:
                            arhitecture.PdRg13();
                            break;
                        case 0xE:
                            arhitecture.PdRg14();
                            break;
                        case 0xF:
                            arhitecture.PdRg15();
                            break;
                        default:
                            arhitecture.PdRg0();
                            break;
                    }
                    #endregion
                    arhitecture.PdRgd();
                    arhitecture.DBUSlabel.Text = simulator.Convert_Binary(DBUS.ToString(), 16);
                    break;

                case 0xB: //Pd0d
                    arhitecture.Pd0d();
                    DBUS = 0;
                    arhitecture.DBUSlabel.Text = simulator.Convert_Binary(DBUS.ToString(), 16);
                    break;

                default: //none
                    break;
            }
        }

        //decodificator camp Operatie ALU
        private void DecodifALU()
        {
            UInt16 campALU = (UInt16)((MIR & 0x0000000078000000) >> 27);
            switch (campALU)
            {
                case 0x1: //SUM
                    #region sum
                    if ((((UInt16)(MIR & 0x0000000007800000)) >> 18) == 2)
                    { //CIN activat atunci mai adun si val 1
                        try
                        {
                            arhitecture.ActivateCIN();
                            arhitecture.ALUlabel.Text = "SUM";
                            arhitecture.Alu();
                            RBUS = (UInt16)(SBUS + DBUS + 0x1);
                            arhitecture.RBUSlabel.Text = simulator.Convert_Binary(RBUS.ToString(), 16);
                        }
                        catch (OverflowException) // daca apare overflow
                        {
                            FLAG = (UInt16)(FLAG | 0X0001); // setez bit overflow V
                            //FLAGtext.Text = Convert_Binary(FLAG.ToString(), 16);
                        }
                    }
                    else
                    { //adunare
                        try
                        {
                            arhitecture.ALUlabel.Text = "SUM";
                            arhitecture.Alu();
                            RBUS = (UInt16)(SBUS + DBUS);
                            arhitecture.RBUSlabel.Text = simulator.Convert_Binary(RBUS.ToString(), 16);
                        }
                        catch (OverflowException)
                        {
                            FLAG = (UInt16)(FLAG | 0X0001); // setez bit V
                            //FLAGtext.Text = Convert_Binary(FLAG.ToString(), 16);
                        }
                    }
                    #endregion
                    break;
                case 0x2: //AND
                    arhitecture.ALUlabel.Text = "AND";
                    arhitecture.Alu();
                    RBUS = (UInt16)(SBUS & DBUS);
                    arhitecture.RBUSlabel.Text = simulator.Convert_Binary(RBUS.ToString(), 16);
                    break;
                case 0x3: //OR
                    arhitecture.ALUlabel.Text = "OR";
                    arhitecture.Alu();
                    RBUS = (UInt16)(SBUS | DBUS);
                    arhitecture.RBUSlabel.Text = simulator.Convert_Binary(RBUS.ToString(), 16);
                    break;
                case 0x4: //XOR
                    arhitecture.ALUlabel.Text = "XOR";
                    arhitecture.Alu();
                    RBUS = (UInt16)(SBUS ^ DBUS);
                    arhitecture.RBUSlabel.Text = simulator.Convert_Binary(RBUS.ToString(), 16);
                    break;
                case 0x5: //ASL
                    arhitecture.ALUlabel.Text = "ASL";
                    arhitecture.Alu();
                    Carry = (UInt16)((DBUS & 0x8000) >> 15);
                    RBUS = (UInt16)(DBUS << 1);
                    arhitecture.RBUSlabel.Text = simulator.Convert_Binary(RBUS.ToString(), 16);
                    break;
                case 0x6: //ASR
                    arhitecture.ALUlabel.Text = "ASR";
                    arhitecture.Alu();
                    Carry = (UInt16)(DBUS & 0x0001);
                    Int16 t = (Int16)((Int16)DBUS >> 1);
                    RBUS = (UInt16)t;
                    arhitecture.RBUSlabel.Text = simulator.Convert_Binary(RBUS.ToString(), 16);
                    break;
                case 0x7: //LSR
                    arhitecture.ALUlabel.Text = "LSR";
                    arhitecture.Alu();
                    Carry = (UInt16)(DBUS & 0x0001);
                    RBUS = (UInt16)(DBUS >> 1);
                    arhitecture.RBUSlabel.Text = simulator.Convert_Binary(RBUS.ToString(), 16);
                    break;
                case 0x8: //ROL
                    arhitecture.ALUlabel.Text = "ROL";
                    arhitecture.Alu();
                    Carry = (UInt16)((DBUS & 0x8000) >> 15);
                    RBUS = (UInt16)((DBUS << 1) + Carry);
                    arhitecture.RBUSlabel.Text = simulator.Convert_Binary(RBUS.ToString(), 16);
                    break;
                case 0x9: //ROR 
                    arhitecture.ALUlabel.Text = "ROR";
                    arhitecture.Alu();
                    Carry = (UInt16)(DBUS & 0x0001);
                    bit = (UInt16)(Carry << 15);
                    RBUS = (UInt16)((DBUS >> 1) + bit);
                    arhitecture.RBUSlabel.Text = simulator.Convert_Binary(RBUS.ToString(), 16);
                    break;
                case 0xA: //RLC
                    arhitecture.ALUlabel.Text = "RLC";
                    arhitecture.Alu();
                    bit = Carry;
                    Carry = (UInt16)((DBUS & 0x8000) >> 15);
                    RBUS = (UInt16)((DBUS << 1) + bit);
                    arhitecture.RBUSlabel.Text = simulator.Convert_Binary(RBUS.ToString(), 16);
                    break;
                case 0xB: //RRC
                    arhitecture.ALUlabel.Text = "RRC";
                    arhitecture.Alu();
                    bit = Carry;
                    Carry = (UInt16)(DBUS & 0x0001);
                    UInt16 leftBit = (UInt16)(Carry << 15);
                    RBUS = (UInt16)((DBUS >> 1) + (bit << 15));
                    arhitecture.RBUSlabel.Text = simulator.Convert_Binary(RBUS.ToString(), 16);
                    break;
                case 0xC: //nDBUS
                    arhitecture.ALUlabel.Text = "nDBUS";
                    arhitecture.Alu();
                    RBUS = (UInt16)(~DBUS);
                    arhitecture.RBUSlabel.Text = simulator.Convert_Binary(RBUS.ToString(), 16);
                    break;
                default: //none
                    break;
            }

        }

        //decodificator camp Destinatie RBUS
        private void DecodifDestRBUS()
        {
            UInt16 campRbus = (UInt16)((MIR & 0x0000000007800000) >> 23);
            switch (campRbus)
            {
                case 0x1: //PmFLAG
                    arhitecture.PmFLAG();
                    FLAG = RBUS;
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;

                case 0x2: //PmRG
                    int nr_reg = IR & 0x000F;
                    R[nr_reg] = RBUS;
                    arhitecture.PmRGline.BorderColor = Color.Red;
                    switch (nr_reg)
                    {
                        case 0:
                            arhitecture.R0text.Text = simulator.Convert_Binary(R[0].ToString(), 16);
                            arhitecture.R0label.BackColor = Color.Red;
                            break;
                        case 1:
                            arhitecture.R1text.Text = simulator.Convert_Binary(R[1].ToString(), 16);
                            arhitecture.R1label.BackColor = Color.Red;
                            break;
                        case 2:
                            arhitecture.R2text.Text = simulator.Convert_Binary(R[2].ToString(), 16);
                            arhitecture.R2label.BackColor = Color.Red;
                            break;
                        case 3:
                            arhitecture.R3text.Text = simulator.Convert_Binary(R[3].ToString(), 16);
                            arhitecture.R3label.BackColor = Color.Red;
                            break;
                        case 4:
                            arhitecture.R4text.Text = simulator.Convert_Binary(R[4].ToString(), 16);
                            arhitecture.R4label.BackColor = Color.Red;
                            break;
                        case 5:
                            arhitecture.R5text.Text = simulator.Convert_Binary(R[5].ToString(), 16);
                            arhitecture.R5label.BackColor = Color.Red;
                            break;
                        case 6:
                            arhitecture.R6text.Text = simulator.Convert_Binary(R[6].ToString(), 16);
                            arhitecture.R6label.BackColor = Color.Red;
                            break;
                        case 7:
                            arhitecture.R7text.Text = simulator.Convert_Binary(R[7].ToString(), 16);
                            arhitecture.R7label.BackColor = Color.Red;
                            break;
                        case 8:
                            arhitecture.R8text.Text = simulator.Convert_Binary(R[8].ToString(), 16);
                            arhitecture.R8label.BackColor = Color.Red;
                            break;
                        case 9:
                            arhitecture.R9text.Text = simulator.Convert_Binary(R[9].ToString(), 16);
                            arhitecture.R9label.BackColor = Color.Red;
                            break;
                        case 10:
                            arhitecture.R10text.Text = simulator.Convert_Binary(R[10].ToString(), 16);
                            arhitecture.R10label.BackColor = Color.Red;
                            break;
                        case 11:
                            arhitecture.R11text.Text = simulator.Convert_Binary(R[11].ToString(), 16);
                            arhitecture.R11label.BackColor = Color.Red;
                            break;
                        case 12:
                            arhitecture.R12text.Text = simulator.Convert_Binary(R[12].ToString(), 16);
                            arhitecture.R12label.BackColor = Color.Red;
                            break;
                        case 13:
                            arhitecture.R13text.Text = simulator.Convert_Binary(R[13].ToString(), 16);
                            arhitecture.R13label.BackColor = Color.Red;
                            break;
                        case 14:
                            arhitecture.R14text.Text = simulator.Convert_Binary(R[14].ToString(), 16);
                            arhitecture.R14label.BackColor = Color.Red;
                            break;
                        case 15:
                            arhitecture.R15text.Text = simulator.Convert_Binary(R[15].ToString(), 16);
                            arhitecture.R15label.BackColor = Color.Red;
                            break;
                    }
                    break;

                case 0x3: //PmSP
                    arhitecture.PmSP();
                    SP = RBUS;
                    arhitecture.SPtext.Text = simulator.Convert_Binary(SP.ToString(), 16);
                    break;

                case 0x4: //PmT
                    arhitecture.PmT();
                    T = RBUS;
                    arhitecture.Ttext.Text = simulator.Convert_Binary(T.ToString(), 16);
                    break;

                case 0x5: //PmPC
                    arhitecture.PmPC();
                    PC = RBUS;
                    arhitecture.PCtext.Text = simulator.Convert_Binary(PC.ToString(), 16);
                    break;

                case 0x6: //PmIVR
                    arhitecture.PmIVR();
                    IVR = RBUS;
                    arhitecture.IVRtext.Text = simulator.Convert_Binary(IVR.ToString(), 16);
                    break;

                case 0x7: //PmADR
                    arhitecture.PmADR();
                    ADR = RBUS;
                    arhitecture.ADRtext.Text = simulator.Convert_Binary(ADR.ToString(), 16);
                    break;

                case 0x8: //PmMDR
                    arhitecture.PmMDR();
                    MDR = RBUS;
                    arhitecture.MDRtext.Text = simulator.Convert_Binary(MDR.ToString(), 16);
                    break;

                default: //none
                    break;
            }
        }

        //decodificator camp Other Operations
        private void DecodifOtherOp()
        {
            UInt16 campOtherOp = (UInt16)((MIR & 0x00000000007C0000) >> 18);
            switch (campOtherOp)
            {
                case 0x1: //PdCOND
                    if (RBUS == 0) // rezultat 0 => setez bitul Z
                    {
                        FLAG = (UInt16)(FLAG | 0x0004);
                    }
                    if (RBUS >> 15 == 0x1) // setez bit de semn S
                    {
                        FLAG = (UInt16)(FLAG | 0x0002);
                    }
                    arhitecture.PdCond();
                    FLAG = (UInt16)(FLAG | (Carry << 3));
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;
                case 0x2: //CIN + PdCOND
                    if (RBUS == 0) // rezultat 0 => setez bitul Z
                    {
                        FLAG = (UInt16)(FLAG | 0x0004);
                    }
                    if (RBUS >> 15 == 0x1) // setez bit de semn S
                    {
                        FLAG = (UInt16)(FLAG | 0x0002);
                    }
                    arhitecture.PdCond();
                    FLAG = (UInt16)(FLAG | (Carry << 3));
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;
                case 0x3: //+2SP
                    arhitecture.SPinc();
                    SP += 2;
                    arhitecture.SPtext.Text = simulator.Convert_Binary(SP.ToString(), 16);
                    break;
                case 0x4: //-2SP
                    arhitecture.SPdec();
                    SP -= 2;
                    arhitecture.SPtext.Text = simulator.Convert_Binary(SP.ToString(), 16);
                    break;
                case 0x5: //+2PC
                    arhitecture.PCinc();
                    PC += 2;
                    arhitecture.PCtext.Text = simulator.Convert_Binary(PC.ToString(), 16);
                    break;
                case 0x6: //A(0)BPO
                    break;
                case 0x7: //A(0)C
                    arhitecture.FLAGlabel.BackColor = Color.Red;
                    FLAG = (UInt16)(FLAG & 0xFFF7);
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;
                case 0x8: //A(1)C
                    arhitecture.FLAGlabel.BackColor = Color.Red;
                    FLAG = (UInt16)(FLAG | 0x0008);
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;
                case 0x9: //A(0)V
                    arhitecture.FLAGlabel.BackColor = Color.Red;
                    FLAG = (UInt16)(FLAG & 0xFFFE);
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;
                case 0xA: //A(1)V
                    arhitecture.FLAGlabel.BackColor = Color.Red;
                    FLAG = (UInt16)(FLAG | 0x0001);
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;
                case 0xB: //A(0)Z
                    arhitecture.FLAGlabel.BackColor = Color.Red;
                    FLAG = (UInt16)(FLAG & 0xFFFB);
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;
                case 0xC: //A(1)Z
                    arhitecture.FLAGlabel.BackColor = Color.Red;
                    FLAG = (UInt16)(FLAG | 0x0004);
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;
                case 0xD: //A(0)S
                    arhitecture.FLAGlabel.BackColor = Color.Red;
                    FLAG = (UInt16)(FLAG & 0xFFFD);
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;
                case 0xE: //A(1)S
                    arhitecture.FLAGlabel.BackColor = Color.Red;
                    FLAG = (UInt16)(FLAG | 0x0002);
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;
                case 0xF: //A(0)CVZS
                    arhitecture.FLAGlabel.BackColor = Color.Red;
                    FLAG = (UInt16)(FLAG & 0xFFF0);
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;
                case 0x10: //A(1)CVZS
                    arhitecture.FLAGlabel.BackColor = Color.Red;
                    FLAG = (UInt16)(FLAG | 0x000F);
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;
                case 0x11: //A(0)BVI
                    arhitecture.FLAGlabel.BackColor = Color.Red;
                    FLAG = (UInt16)(FLAG & 0x0FF7F);
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;
                case 0x12: //A(1)BVI
                    arhitecture.FLAGlabel.BackColor = Color.Red;
                    FLAG = (UInt16)(FLAG | 0x0080);
                    arhitecture.FLAGtext.Text = simulator.Convert_Binary(FLAG.ToString(), 16);
                    break;
                default: //none
                    break;
            }

        }

        #endregion

        //test global function g to set next MAR
        private UInt16 TestG()
        {
            UInt16 nTF, cond = 0;
            nTF = (UInt16)((MIR & 0x0000000000000100) >> 8);
            switch ((MIR & 0x000000000000F000) >> 12)
            {
                case 1: //INT
                    if (isINT == true)
                        cond = 1;
                    break;
                case 2: //C
                    cond = (UInt16)((FLAG & 0x0008) >> 3);
                    break;
                case 3: //Z
                    cond = (UInt16)((FLAG & 0x0004) >> 2);
                    break;
                case 4: //S
                    cond = (UInt16)((FLAG & 0x0002) >> 1);
                    break;
                case 5: //V
                    cond = (UInt16)(FLAG & 0x0001);
                    break;
                case 6:
                    UInt16 ma = (UInt16)((IR & 0x0030) >> 4);
                    if (ma == 1)
                        cond = 1;
                    break;
                case 0x7://CIL
                    break;
                case 0x8://ACLOW
                    break;
                default: //NONE
                    cond = 1; // return 1 daca nT/F = 0(not nT/F)
                    break;
            }
            return (UInt16)(nTF ^ cond);
        }

        //test if there is a memory operation: Ifch,Read or Write
        private void TestIfchReadWrite()
        {
            UInt16 memOp = (UInt16)((MIR & 0x0000000000030000) >> 16);
            switch (memOp)
            {
                case 0x1: //IFCH
                    arhitecture.Ifch();
                    IR = (UInt16)((UInt16)(simulator.getLineRAM(ADR + 1) << 8) | (UInt16)(simulator.getLineRAM(ADR)));
                    arhitecture.IRtext.Text = simulator.Convert_Binary(IR.ToString(), 16);
                    break;
                case 0x2: //READ
                    arhitecture.Read();
                    MDR = (UInt16)((UInt16)(simulator.getLineRAM(ADR + 1) << 8) | (UInt16)(simulator.getLineRAM(ADR)));
                    arhitecture.MDRtext.Text = simulator.Convert_Binary(MDR.ToString(), 16);
                    break;
                case 0x3: //WRITE
                    arhitecture.Write();
                    simulator.addLineRAM(ADR, (byte)MDR);
                    simulator.addLineRAM(ADR + 1, (byte)((UInt16)(MDR >> 8)));
                    UInt16 ADR_2 = (UInt16)(ADR + 1);

                    try
                    {
                        if (simulator.getEntireMem.ContainsKey(ADR))
                        {
                            simulator.addLineMem(ADR, simulator.getLineRAM(ADR));
                            simulator.addLineMem(ADR_2, simulator.getLineRAM(ADR_2));
                        }
                        else
                        {
                            simulator.addLineMem(ADR, simulator.getLineRAM(ADR));
                            simulator.addLineMem(ADR + 1, simulator.getLineRAM(ADR + 1));
                        }
                    }
                    catch (System.NullReferenceException e) {
                        e.ToString();
                    }
                    break;
                default: // no mem op
                    break;
            }
        }

        private UInt16 getCl1()
        {
            UInt16 IR15 = (UInt16)((IR & 0x8000) >> 15);
            UInt16 IR13 = (UInt16)((IR & 0x2000) >> 13);
            UInt16 rez = (UInt16)(IR15 & IR13);
            return rez; // IR15 & IR13
        }

        private UInt16 getCl0()
        {
            UInt16 IR15 = (UInt16)((IR & 0x8000) >> 15);
            UInt16 nIR14 = (UInt16)(((~IR) & 0x4000) >> 14);
            UInt16 rez = (UInt16)(IR15 & nIR14);
            return rez; //IR15 & nIR14
        }

        private UInt16 getCl()
        {
            UInt16 CL1 = (UInt16)(getCl1());
            UInt16 CL0 = (UInt16)(getCl0());
            clasa = (UInt16)((CL1 << 1) | CL0);
            return clasa;
        }

        public bool itIsStep() {
            return this.isStep;
        }

        public void setIsStep(bool value) {
            this.isStep = value;
        }

        public void resetRegisters()
        {
            PC = 0; IR = 0; SP = 65534; ; MAR = 0; ADR = 0;
            FLAG = 0; T = 0; IVR = 0; MDR = 0;
            isStep = false;
            for (int i = 0; i < 16; i++)
            {
                R[i] = 0;
            }
        }
    }
}
