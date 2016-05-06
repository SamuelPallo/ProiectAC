using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectAC
{
    class Variable
    {
        private struct label
        {
            public string name;
            public int address;
        };

        private struct procedure
        {
            public string name;
            public int address;
        };

        private label[] labels = new label[50];
        private procedure[] procedures = new procedure[50];

        private byte[] RAM = new byte[65536];
        private UInt16 IR = 0;
        private UInt16 FLAG = 0;
        private UInt16 IVR = 0;
        private UInt16 T = 0;
        private UInt16 SP = 65534;
        private UInt16 MDR = 0;
        private UInt16 ADR = 0;
        private UInt16 PC = 0;
        private UInt16 PCMax = 0; // PC Maxim, in functie de cate instructiuni sunt in fisier
        private UInt16[] R = new UInt16[16];
        private UInt16 reg = 0;
        private UInt16 g = 0;
        private UInt16 clasa;
        private UInt16 IndexValoare;

        private UInt64[] MPM = new UInt64[200];
        private UInt64 MIR = 0;
        private UInt16 MAR = 0;
        private UInt16 SBUS = 0;
        private UInt16 DBUS = 0;
        private UInt16 RBUS = 0;
        private UInt16 Carry = 0;

        private int Step = 0;

        private UInt16 bit = 0;

        private string registru;
        private string valoareImediata;
        private string valImediataBinar;
        private string IM = "00";
        private string AD = "01";
        private string AI = "10";
        private string AX = "11";

        private int contor;
        private int adresaSalt;
        private int modAdresare;
        private int offset;
        private string valIndex;
        private string valIndexSursa;
        private string valIndexDest;
        private string offsetString;

        Dictionary<string, string> base1 = new Dictionary<string, string>();
        Dictionary<string, string> base2 = new Dictionary<string, string>();
        Dictionary<string, string> base3 = new Dictionary<string, string>();
        Dictionary<string, string> base4 = new Dictionary<string, string>();
        Dictionary<string, string> registers = new Dictionary<string, string>() {
            {"R0","0000"},{"R1","0001"},{"R2","0010"},{"R3","0011"},{"R4","0100"},{"R5","0101"},{"R6","0110"},{"R7","0111"},
            {"R8","1000"},{"R9","1001"},{"R10","1010"},{"R11","1011"},{"R12","1100"},{"R13","1101"},{"R14","1110"},{"R15","1111"}
        };
        Dictionary<string, string> registreIndex = new Dictionary<string, string>();
        Dictionary<UInt16, byte> mem = new Dictionary<UInt16, byte>();

        List<string> prelines = new List<string>();
        List<string> instructions = new List<string>();

        List<string> microcod = new List<string>();

        string opcode = null;
        string codif;
        int numberLabels = 0;
        int numberProcedures = 0;

        private bool isbase1 = false;
        private bool isbase2 = false;
        private bool isbase3 = false;
        private bool isbase4 = false;
        private bool isValImm = false;
        private bool isIndexSrc = false;
        private bool isIndexDest = false;
        private bool isError = false;
        private bool isJMP = false;
        private bool isCALL = false;
        private bool isPUSHRi = false;
        private bool isPOPRi = false;
        bool asmFileOpened = false;
        bool microCodeOpened = false;
        bool compiledOk;
        private bool isStep;
        private bool isINT;

    }
}
