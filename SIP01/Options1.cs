using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIP01
{
    static class Options1
    {

		public static string Via;
		public static string From;
		public static string To;
		public static string Call_ID;
		public static string CSeq;

		public static string Max_Forwards;
		public static string User_Agent;
		public static string Content_Length;
		public static string Ext1Tag;

		public static string GetMessage(string ServerMessage)
        {

			Via = Utils1.GetField(ServerMessage, "Via");
			From = Utils1.GetField(ServerMessage, "From");
			To = Utils1.GetField(ServerMessage, "To");
			Call_ID = Utils1.GetField(ServerMessage, "Call-ID");
			CSeq = Utils1.GetField(ServerMessage, "CSeq");

			Max_Forwards = Utils1.GetField(ServerMessage, "Max-Forwards");
			User_Agent = Utils1.GetField(ServerMessage, "User-Agent");
			Content_Length = Utils1.GetField(ServerMessage, "Content-Length");
			Ext1Tag = "tag=" + Utils1.GenerateTag1(8);

			string message = "SIP/2.0 200 Ok\r\n" +
			$"Via: {Via}\r\n" +
			$"From: {From}\r\n" +
			$"To: {To};{Ext1Tag}\r\n" +
			$"Call-ID: {Call_ID}\r\n" +
			$"Allow : {Const.Local_Allow }\r\n" + // Additional
			$"CSeq: {CSeq}\r\n" +
			$"Content-Length: 0\r\n"+
			"\r\n";

			return message;

		}




	}
}
