using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIP01
{
    static class Invite1
    {

		public static string Via;
		public static string From;
		public static string To; //To: <sip:104@192.168.1.34>
		public static string Call_ID;
		public static string CSeq;

		public static string User_Agent;
		public static string Supported;
		public static string Allow;
		public static string Contact;

		public static string Ext1Tag;

		public static int LocalByeCounter = 111;

//**************************************************************************************	
		public static string GetTryingMessage(string ServerMessage)
        {

			Via = Utils1.GetField(ServerMessage, "Via");
			From = Utils1.GetField(ServerMessage, "From");
			To = Utils1.GetField(ServerMessage, "To");
			Call_ID = Utils1.GetField(ServerMessage, "Call-ID");
			CSeq = Utils1.GetField(ServerMessage, "CSeq");

			User_Agent = Utils1.GetField(ServerMessage, "User-Agent");
			Supported = Utils1.GetField(ServerMessage, "Supported");
			Allow = Utils1.GetField(ServerMessage, "Allow");
			Contact = Utils1.GetField(ServerMessage, "Contact");

			Ext1Tag = "tag="+Utils1.GenerateTag1(8);

			string message = "SIP/2.0 100 Trying\r\n" +
			$"Via: {Via}\r\n" +
			$"From: {From}\r\n" +
			$"To: {To.Replace("<","").Replace(">","")}\r\n" +
			$"Call-ID: {Call_ID}\r\n" +
			$"CSeq: {CSeq}\r\n" +
			$"Content-Length: 0\r\n\r\n";

			return message;

		}

		//**************************************************************************************	
		public static string GetRingingMessage()
		{

			string message = "SIP/2.0 180 Ringing\r\n" +
			$"Via: {Via}\r\n" +
			$"From: {From}\r\n" +
			//$"To: {To};tag={Const.Ext1Tag}\r\n" +
			$"To: {To};{Ext1Tag}\r\n" +
			$"Call-ID: {Call_ID}\r\n" +
			$"CSeq: {CSeq}\r\n" +
			$"User-Agent: {Const.Local_User_Agent}\r\n" +
			$"Supported: {Const.Local_Supported}\r\n" +
			$"Content-Length: 0\r\n\r\n";
			return message;

		}

		//**************************************************************************************	
		public static string GetOkMessage()
		{

			string SDP_Data = SDP1.SetOK();

			string message = "SIP/2.0 200 Ok\r\n" +
			$"Via: {Via}\r\n" +
			$"From: {From}\r\n" +
			//$"To: {To};tag={Const.Ext1Tag}\r\n" +
			$"To: {To};{Ext1Tag}\r\n" +
			$"Call-ID: {Call_ID}\r\n" +
			$"CSeq: {CSeq}\r\n" +
			$"User-Agent: {Const.Local_User_Agent}\r\n" +
			$"Supported: outbound\r\n" +
			$"Allow: {Const.Local_Allow}\r\n" +
			$"Contact: {Const.Local_Contact}\r\n" +
			$"Content-Type: application/sdp\r\n" +
			$"Content-Length: {SDP_Data.Length}\r\n" +
			SDP_Data+"\r\n";

			return message;

		}
		//**************************************************************************************	
		public static string GetByeMessage()
		{

			string message = $"BYE {RemBrackets(Contact)} SIP/2.0\r\n" +
			//$"Via: {Const.Local_Via}\r\n" +
			$"Via: {Const.Local_Add_Port + $";{Utils1.GenerateBrachShort1()};rport"}\r\n" +
			$"From: {To};{Ext1Tag}\r\n" +
			$"To: {From}\r\n" +
			$"CSeq: {LocalByeCounter++} BYE\r\n" +
			$"Call-ID: {Call_ID}\r\n" +
			$"Max_forwards: 70\r\n" +
			$"User-Agent: {Const.Local_User_Agent}\r\n" +
			$"Content-Length: 0\r\n\r\n";

			return message;

		}       //**************************************************************************************	
				//**************************************************************************************	
	
			private static string RemBrackets(string Data)
			{
				Data = Data.Replace("<", "");
				Data = Data.Replace(">", "");
				Data = Data.Trim();
				return Data;
			}

			
				//**************************************************************************************	



	}
}
