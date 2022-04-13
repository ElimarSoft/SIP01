using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIP01
{
    static class Register1
    {

		public static string sip = Const.RegisterSIP;

		public static string Via = Const.Local_Via;
		//public static string From = Const.Local_Add_RemIP_Tag;
		public static string From = Const.Local_Add_RemIP+";"+ "tag=" + Utils1.GenerateTag1(8);
		public static string To = Const.Local_Add_RemIP;
		public static string CSeq = "20 REGISTER\r\n";

		//public static string Call_ID = "M1Qg9hbcoy";
		public static string Call_ID = "tag=" + Utils1.GenerateTag1(10);
		public static string Max_Forwards = "70";
		public static string Supported = "outbound";

		public static string Accept = "application/sdp, text/plain, application/vnd.gsma.rcs-ft-http+xml";
		public static string Contact = Const.Local_Contact;

		public static string Expires = "3600";
		public static string User_Agent = Const.Local_User_Agent;

		static int SeqCounter = 20; 

		public static string GetMessage()
        {
				  string message = "REGISTER " +
				  $"sip:{sip}\r\n" +
				  //$"Via: {Via}\r\n" +
				  $"Via: {Const.Local_Add_Port+$";alias;{Utils1.GenerateBrachShort1()};rport"}\r\n" +
				  $"From: {From}\r\n" +
				  $"To: {To}\r\n" +
				  $"CSeq: {GetSequence()}\r\n" +
				  $"Call-ID: {Call_ID}\r\n" +

				  $"Max-Forwards: {Max_Forwards}\r\n" +
				  $"Supported: {Supported}\r\n" +
				  $"Accept: {Accept}\r\n" +
				  $"Contact: {Contact}\r\n" +
				  $"Expires: {Expires}\r\n" +
				  $"User-Agent: {User_Agent}\r\n"+
				  $"Content-Length: 0\r\n";
				  message = message.Replace("'", "\"");

			return message;

		}

		private static string GetSequence()
        {

			return string.Format("{0} REGISTER",SeqCounter++);

        }




	}
}
