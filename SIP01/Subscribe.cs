using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIP01
{
    static class Subscribe1
    {

		public static string sip = "192.168.1.39 SIP/2.0";

		public static string Via = "SIP/2.0/UDP 192.168.1.34:5060";
		public static string branch = "z9hG4bK.47HbA8dyT";

		public static string From= "<sip:104@192.168.1.39>;tag=odA0ePFyg";
		public static string To = "'Eli' <sip:102@192.168.1.39>";
		public static string CSeq = "20 SUBSCRIBE\r\n";

		public static string Call_ID = "M1Qg9hbcoy";
		public static string Max_Forwards = "70";
		public static string Supported = "outbound";
		public static string Event = "presence";

		public static string Contact = "<sip:104@192.168.1.34>;+sip.instance=\"<urn:uuid:0913ad69-c99e-4a87-8503-fab8ac35e544>";

		public static string Expires = "600";
		public static string User_Agent = "Linphone/3.8.1 (belle-sip/1.4.0)";

		static int SeqCounter = 20;

		//SUBSCRIBE sip:102@192.168.1.39 SIP/2.0
		//Via: SIP/2.0/UDP 192.168.1.34:5060;branch=z9hG4bK.p35EvNjDx;rport
		//From: <sip:104@192.168.1.39>;tag=mLlnSNkF4
		//To: "Eli" <sip:102@192.168.1.39>
		//CSeq: 20 SUBSCRIBE
		//Call-ID: XBj58JY0lS
		//Max-Forwards: 70
		//Supported: outbound
		//Event: presence
		//Expires: 600
		//Contact: <sip:104@192.168.1.34>;+sip.instance="<urn:uuid:0913ad69-c99e-4a87-8503-fab8ac35e544>"
		//User-Agent: Linphone/3.8.1 (belle-sip/1.4.0)



		public static string GetMessage()
        {
				  string message = "SUBSCRIBE " +
				  $"sip:{sip}\r\n" +
				  $"Via: {Via};branch={branch};rport\r\n" +
				  $"From: {From}\r\n" +
				  $"To: {To}\r\n" +
				  $"CSeq: {GetSequence()}\r\n" +
				  $"Call-ID: {Call_ID}\r\n" +

				  $"Max-Forwards: {Max_Forwards}\r\n" +
				  $"Supported: {Supported}\r\n" +
				  $"Event: {Event}\r\n" +

				  $"Expires: {Expires}\r\n" +
				  $"Contact: {Contact}\r\n" +
				  $"User-Agent: {User_Agent}\r\n";

				  message = message.Replace("'", "\"");

			return message;

		}

		private static string GetSequence()
        {

			return string.Format("{0} SUBSCRIBE",SeqCounter++);

        }




	}
}
