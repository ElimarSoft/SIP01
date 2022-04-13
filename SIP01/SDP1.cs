using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Session description protocol



namespace SIP01
{
    static class SDP1
    {

        public static int RTP_ServerPort = 0;


        //*************************************************************************************
        public static void GetInvite(string message)
        {
            char[] Sep = { '\r', '\n' };
            string SDP_Data = GetSDPBlock(message);
            string[] SDP_Fields = SDP_Data.Split(Sep);

            string Audio_Data = GetField(SDP_Fields,"m=audio");
            string[] Audio_Fields = Audio_Data.Split(' ');

            RTP_ServerPort = int.Parse(Audio_Fields[1]);

        }

        //*************************************************************************************
        public static string SetOK()
        {
            string message =
                "\r\n" +
                "v=0\r\n" +
                $"o={Const.Ext1} 2648 3076 IN IP4 {Const.Local_IP}\r\n" +
                $"s=Talk\r\n" +
                $"c=IN IP4 {Const.Local_IP}\r\n" +
                $"t=0 0\r\n" +
                $"m=audio {Const.RTPPortLocal} RTP/AVP 0 8 3 9 101\r\n" +
                $"a=rtpmap:101 telephone -event/8000\r\n";


            return message;

        }

        //*************************************************************************************
        private static string GetSDPBlock(string message)
        {

            const string ContentTypeTag = "Content-Type: application/sdp";
            const string ContentLengthTag = "Content-Length:";
 
            int pos1 = message.IndexOf(ContentTypeTag);
            string strData = message.Substring(message.IndexOf(ContentTypeTag));

            int pos2 = strData.IndexOf(ContentLengthTag) + ContentLengthTag.Length;
            int pos3 = strData.IndexOf("\r\n", pos2);
            string LengthStr = strData.Substring(pos2, pos3 - pos2);
            int Length1 = int.Parse(LengthStr);
            int pos4 = strData.IndexOf("v",pos3)-1;

            return strData.Substring(pos4, Length1);

        }

        //*************************************************************************************
        private static string GetField(string[] Data, string Field)
        {
            foreach (string Fl in Data) if (Fl.StartsWith(Field)) return Fl;
            return "";

        }

        //*************************************************************************************



    }
}
