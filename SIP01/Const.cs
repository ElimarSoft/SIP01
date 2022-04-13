//#define UDP
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIP01


{
    static class Const
    {

        public const string Server_IP = "192.168.1.39";
        public const string Local_IP = "192.168.1.34";

        public const int SIPort = 5060;
        public const int RTPPortLocal = 7078;
        public const int TCP_Local_Port = 56700;

        public const string BranchStart = "z9hG4bK";

        public const string Ext1 = "104";
        public const string Ext1Tag = "7jdkftW";
        public const string Local_User_Agent = "Linphone/3.8.1 (belle-sip/1.4.0)";
        //public const string Local_User_Agent = "Linphone/3.8.1 (Elimar Soft)";
        public const string Local_Allow = "INVITE, ACK, CANCEL, OPTIONS, BYE, REFER, NOTIFY, MESSAGE, SUBSCRIBE, INFO, UPDATE";
        //public const string Local_Allow = "INVITE, ACK, CANCEL, OPTIONS, BYE";
        public const string Local_Supported = "outbound";

        public const string Local_Add_RemIP = "<sip:104@192.168.1.39>";
        public const string Local_Add_LocIP = "<sip:104@192.168.1.34>";

#if (UDP)
        public const string RegisterSIP = "192.168.1.39 SIP/2.0";
        public const string Local_Via = "SIP/2.0/UDP 192.168.1.34:5060;branch=z9hG4bK.r05YsgEpr;rport";
        public const string Local_Add_Port = "SIP/2.0/UDP 192.168.1.34:5060";

        public const string Local_Add = "sip:104@192.168.1.39";
        public const string Local_Add_RemIP_Tag = "<sip:104@192.168.1.39>;tag=odA0ePFyg";
        public const string Local_Add_LocIP_Tag = "<sip:104@192.168.1.34>;tag=blgn9~3";
        public const string Local_Contact = "<sip:104@192.168.1.34>;+sip.instance=\"<urn:uuid:0913ad69-c99e-4a87-8503-fab8ac35e544>\"";


#elif (TCP)
        //public const string RegisterSIP = "sbc01.grd.nexhub.com:5068;transport=TCP SIP/2.0";
        public const string RegisterSIP = "192.168.1.39 SIP/2.0";
        public const string Local_Via = "SIP/2.0/TCP 192.168.1.34:56700;alias;branch=z9hG4bK.yMzcMTVm6;rport";
        public const string Local_Add_Port = "SIP/2.0/TCP 192.168.1.34:56700";


        //public const string Local_Add = "<sip:104@192.168.1.39;transport=TCP>";
        //public const string Local_Add_Tag = "<sip:104@192.168.1.39;transport=TCP>;tag=odA0ePFyg";
        //public const string Local_Num_Tag = "<sip:104@192.168.1.34;transport=TCP>;tag=blgn9~3";

        public const string Local_Add_RemIP_Tag = "<sip:104@192.168.1.39>;tag=odA0ePFyg";
        public const string Local_Add_LocIP_Tag = "<sip:104@192.168.1.34>;tag=blgn9~3";

        public const string Local_Contact = "<sip:104@192.168.1.34:56700;transport=tcp>;+sip.instance=\"<urn:uuid:0913ad69-c99e-4a87-8503-fab8ac35e544>\"";

#endif


        public const int Local_Timeout = 7000; // ms

        public const int AudioBitRate = 8000;

        public const string LogFilesPath = @"C:\Users\admin\Desktop\SIP\Out\";


    }
}
