using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIP01
{
    static class Notify1
    {

		public static string Via;
		public static string From;
		public static string To;
		public static string Call_ID;
		public static string CSeq;

		public static string User_Agent;
		public static string Supported;


		public static string GetMessage(string ServerMessage)
        {

			Via = Utils1.GetField(ServerMessage, "Via");
			From = Utils1.GetField(ServerMessage, "From");
			To = Utils1.GetField(ServerMessage, "To");
			Call_ID = Utils1.GetField(ServerMessage, "Call-ID");
			CSeq = Utils1.GetField(ServerMessage, "CSeq");

			User_Agent = Utils1.GetField(ServerMessage, "User-Agent");
			Supported = Utils1.GetField(ServerMessage, "Supported");

			string message = "SIP/2.0 200 Ok\r\n" +
			$"Via:{Via}\r\n" +
			$"From:{From}\r\n" +
			$"To:{To}\r\n" +
			$"Call-ID:{Call_ID}\r\n" +
			$"CSeq:{CSeq}\r\n" +
			$"User-Agent:{User_Agent}\r\n" +
			$"Supported:{Supported}\r\n\r\n\r\n";

			return message;

		}




	}
}

/*
 
NOTIFY sip:104@192.168.1.34:5060 SIP/2.0
Via: SIP/2.0/UDP 192.168.1.39:5060;rport;branch=z9hG4bKPja26c2eb4-d86d-4a73-85eb-c4c52d47ff76
From: "Eli" <sip:102@192.168.1.39>;tag=44852412-162a-47ac-854f-289ea0688f12
To: <sip:104@192.168.1.39>;tag=wySq2ztf~
Contact: <sip:192.168.1.39:5060>
Call-ID: hNM3fKQeal
CSeq: 20832 NOTIFY
Event: presence
Subscription-State: active;expires=523
Allow-Events: presence, dialog, message-summary, refer
Max-Forwards: 70
User-Agent: FPBX-15.0.16.81(16.13.0)
Content-Type: application/pidf+xml
Content-Length:   478

<?xml version="1.0" encoding="UTF-8"?>
<presence entity="sip:102@192.168.1.39:5060" xmlns="urn:ietf:params:xml:ns:pidf" xmlns:dm="urn:ietf:params:xml:ns:pidf:data-model" xmlns:rpid="urn:ietf:params:xml:ns:pidf:rpid">
 <note>On the phone</note>
 <tuple id="102">
  <status>
   <basic>open</basic>
  </status>
  <contact priority="1">sip:104@192.168.1.39</contact>
 </tuple>
 <dm:person>
  <rpid:activities>
   <rpid:on-the-phone />
  </rpid:activities>
 </dm:person>
</presence>


*/
