using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIP01
{
    static class BYE1
    {

		public static string Via;
		public static string From;
		public static string To;
		public static string Call_ID;
		public static string CSeq;

		public static string User_Agent;
		public static string Content_Length;


		public static string GetMessage(string ServerMessage)
        {

			Via = Utils1.GetField(ServerMessage, "Via");
			From = Utils1.GetField(ServerMessage, "From");
			To = Utils1.GetField(ServerMessage, "To");
			Call_ID = Utils1.GetField(ServerMessage, "Call-ID");
			CSeq = Utils1.GetField(ServerMessage, "CSeq");

			User_Agent = Utils1.GetField(ServerMessage, "User-Agent");
			Content_Length = Utils1.GetField(ServerMessage, "Content-Length");

			string message = "SIP/2.0 200 Ok\r\n" +
			$"Via:{Via}\r\n" +
			$"From:{From}\r\n" +
			$"To:{To}\r\n" +
			$"Call-ID:{Call_ID}\r\n" +
			$"CSeq:{CSeq}\r\n" +
			$"User-Agent:{Const.Local_User_Agent}\r\n" +
			$"Supported:{Const.Local_Supported}\r\n" +
			$"Content-Length: 0\r\n\r\n";
			return message;

		}




	}
}

/*

"BYE sip:asterisk@192.168.1.39:5060 SIP/2.0
Via: SIP/2.0/UDP 192.168.1.34:5060;branch=z9hG4bK.r~13uNLi8;rport
From: <sip:104@192.168.1.34>;tag=9-2r5-A
To: ""Eli"" <sip:102@192.168.1.39>;tag=712b5356-b323-494a-81fe-9947acb89110
CSeq: 111 BYE
Call-ID: ea304edb-2414-4759-a0ac-04fb74645844
Max-Forwards: 70
User-Agent: Linphone/3.8.1 (belle-sip/1.4.0)

"
"SIP/2.0 200 OK
Via: SIP/2.0/UDP 192.168.1.34:5060;rport=5060;received=192.168.1.34;branch=z9hG4bK.r~13uNLi8
Call-ID: ea304edb-2414-4759-a0ac-04fb74645844
From: <sip:104@192.168.1.34>;tag=9-2r5-A
To: ""Eli"" <sip:102@192.168.1.39>;tag=712b5356-b323-494a-81fe-9947acb89110
CSeq: 111 BYE
Server: FPBX-15.0.16.81(16.13.0)
Content-Length:  0

"

 




*/


