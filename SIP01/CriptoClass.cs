using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace SIP01
{
    static class CriptoClass
    {
        //static MD5CryptoServiceProvider Sp1 = new MD5CryptoServiceProvider();
        static MD5Cng Sp1 = new MD5Cng();

        const string COP = "auth";
        const string METHOD = "REGISTER";
        const string SEP = ":";

        // *******************************************************************************************************
        public static void test()
        {

            string Authorization = Enc("104", "asterisk", "104", "1613301040/b1152279e52524f2775ccf644deb8ba7", "00000001", "055533c5", "sip:192.168.1.39");


        }

        // *******************************************************************************************************

        public static string Enc(string username, string realm, string pass, string nonce, string nc, string cnonce, string uri)

        {

            string HA1_Str = username + SEP + realm + SEP + pass;
            byte[] HA1_MD5 = Sp1.ComputeHash(Encoding.ASCII.GetBytes(HA1_Str));
            string HA1_Hex = GetHexStr(HA1_MD5);
            //Debug.Print(HA1_Hex);

            string HA2_Str = METHOD + SEP + uri;
            byte[] HA2_MD5 = Sp1.ComputeHash(Encoding.ASCII.GetBytes(HA2_Str));
            string HA2_Hex = GetHexStr(HA2_MD5);
            //Debug.Print(HA2_Hex);

            string HA3_Str = HA1_Hex + SEP + nonce + SEP + nc + SEP + cnonce + SEP + COP + SEP + HA2_Hex;
            byte[] HA3_MD5 = Sp1.ComputeHash(Encoding.ASCII.GetBytes(HA3_Str));
            string HA3_Hex = GetHexStr(HA3_MD5);
            //Debug.Print(HA3_Hex);

            return HA3_Hex;
        }

        // *******************************************************************************************************

       private static string GetHexStr(byte[] Data)
        {
            string ResultStr = "";
            foreach (byte byte1 in Data) ResultStr += byte1.ToString("x2");
            return ResultStr;

        }


        // *******************************************************************************************************

        // *******************************************************************************************************




    }
}
