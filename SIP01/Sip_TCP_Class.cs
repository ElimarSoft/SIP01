
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Timers;
using System.Diagnostics;
using System.Threading;

namespace SIP01
{
	public class  Sip_TCP_Class
	{

		TcpClient TCP1;
		IPEndPoint target;
		static IPEndPoint source;
		public static bool Registered = false;
		RTP_UDP_Class RTP1 = new RTP_UDP_Class();
		static RTP_UDP_Class ActualRTP1;
		NetworkStream TCP1s;

		private static System.Timers.Timer MaxTimer1;

		//delegate void OnUdpData(IAsyncResult result);

		const string CrLf = "\r\n";
		Thread ReceiveThread;
		System.IO.MemoryStream MS1 = new System.IO.MemoryStream();


		// *******************************************************************************************************
		public Sip_TCP_Class()
        {


			target = new IPEndPoint(IPAddress.Parse(Const.Server_IP), Const.SIPort); // Destination
			source = new IPEndPoint(0, Const.TCP_Local_Port);

			TCP1 = new TcpClient(source); // Source Address

			ActualRTP1 = RTP1;

			MaxTimer1 = new System.Timers.Timer();
			MaxTimer1.Enabled=false;
			MaxTimer1.AutoReset = false;
			MaxTimer1.Interval = Const.Local_Timeout;
			MaxTimer1.Elapsed += (sender,e) => OnTimer1Event(sender, e, this);




		}
		// *******************************************************************************************************
		public void Connect()
        {


			//TCP1.BeginConnect(Const.Server_IP, Const.SIPort, new AsyncCallback(OnTcpData), TCP1);

			TCP1.Connect(Const.Server_IP, Const.SIPort);

			Thread.Sleep(1000);

			//TCP1.Connect(target);

			TCP1s = TCP1.GetStream();
	
			Thread.Sleep(2000);
			if (TCP1.Available > 0)
            {
				string ReceiveStr01 = ReceiveString();
				if (ReceiveStr01.StartsWith("OPTIONS")) Registered = true;
   			}

			if (!Registered)
            {
				Registered = Register();
            }

			if (Registered)
			{
				ReceiveThread = new Thread(new ThreadStart(GetData));
				ReceiveThread.Start();
			}

		}

		// *******************************************************************************************************
		private string ReceiveString()
        {
			const Byte LF = 10;
			const Byte CR = 13;


			int nlCnt = 0;

			string ReceiveStr = "";


			while (true)
			{
				
				if (TCP1s.DataAvailable)
				{
					byte BData = (byte)TCP1s.ReadByte();
					
					if (BData == LF) nlCnt++;
					else if (BData != CR) nlCnt = 0;

					MS1.WriteByte(BData);

					if (nlCnt == 1) // New Line
					{
						byte[] ReadBuffer = MS1.ToArray();
						MS1.SetLength(0);
						string LineData = Encoding.ASCII.GetString(ReadBuffer, 0, ReadBuffer.Length);
						ReceiveStr += LineData;

						const string ContentLengthStr = "Content-Length:";
						if (LineData.StartsWith(ContentLengthStr))
                        {
							int MessLength = int.Parse(LineData.Substring(ContentLengthStr.Length));
							if (MessLength > 0)
							{
 								byte[] RTP_Buffer = new byte[MessLength];
								TCP1s.Read(RTP_Buffer, 0, RTP_Buffer.Length);
								ReceiveStr += Encoding.ASCII.GetString(RTP_Buffer, 0, RTP_Buffer.Length);
							}

						}


					} 
					else if (nlCnt >= 2) // New Message
                    {

						MS1.SetLength(0); // Reset Memory Stream
						string ReturnString = RemovCRLF(ReceiveStr);
						return ReturnString;
					}


				}

			}
	

		}
		// *******************************************************************************************************
		public bool Register()
		{

			SendString(Register1.GetMessage() + CrLf);
			System.Threading.Thread.Sleep(100);
			if (TCP1.Available == 0) return false;
			string ReceiveStr01 = ReceiveString();

			if (!ReceiveStr01.StartsWith(ResponseCodes.Unauthorized))
			{
				System.Diagnostics.Debug.Print("Receive1 Abort");
				return false;
			}
			SendString(Register1.GetMessage() + Authorization1.GetAuthorization(ReceiveStr01) + CrLf + CrLf);
			System.Threading.Thread.Sleep(100);
			if (TCP1.Available == 0) return false;
			string ReceiveStr02 = ReceiveString();
			if (!ReceiveStr02.StartsWith(ResponseCodes.OK))
			{
				System.Diagnostics.Debug.Print("Receive2 Abort");
				return false;
			}
			//Send(Subscribe1.GetMessage() + CrLf + CrLf);
			//System.Threading.Thread.Sleep(100);
			//if (UDP1.Available == 0) return false;
			//string ReceiveStr03 = Encoding.ASCII.GetString(UDP1.Receive(ref source));

			return true;

        }

		// *******************************************************************************************************
		private void GetData()
		{

			while (true)
			{


				while (TCP1.Available == 0) ;

				Thread.Sleep(200);

				//System.Diagnostics.Debug.Print("Async Data Received");

				//Sip_TCP_Class Sipx = result.AsyncState as Sip_TCP_Class;
				string ReceiveStr = ReceiveString();

				if (ReceiveStr.StartsWith("OPTIONS"))
				{
					Registered = true;
					Debug.Print("OPTIONS MESSAGE HAS ARRIVED");
					string message = Options1.GetMessage(ReceiveStr);
					SendString(message);
				}
				else if (ReceiveStr.StartsWith("INVITE"))
				{
					Registered = true;
					SDP1.GetInvite(ReceiveStr);
					SendString(Invite1.GetTryingMessage(ReceiveStr));
					Thread.Sleep(200);
					SendString(Invite1.GetRingingMessage());
					Thread.Sleep(200);
					SendString(Invite1.GetOkMessage());
					MaxTimer1.Start();
					ActualRTP1.Wav1.RecordActive = true;

				}
				else if (ReceiveStr.StartsWith("BYE"))
				{
					Registered = true;
					SendString(BYE1.GetMessage(ReceiveStr));
					StopRecording();

				}
				else if (ReceiveStr.StartsWith("NOTIFY"))
				{
					Registered = true;
					//Sipx.Send(Notify1.GetMessage(ReceiveStr));
				}

				//Sipx.TCP1.BeginConnect(Const.Server_IP, Const.SIPort, new AsyncCallback(OnTcpData), Sipx.TCP1);

			}
		
		}

		// *******************************************************************************************************

		// *******************************************************************************************************
		public void SendString (string Message)
		{ 
			byte[] message = Encoding.ASCII.GetBytes(Message);
			TCP1s.Write(message, 0, message.Length);
			//Debug.Print("Sent=" + Message.Length.ToString("D4") + "\r\n" + Message);
			TCP1s.Flush();
		}
		// *******************************************************************************************************

		//private void ReceiveAsync1()
		//      {
		//	UDP1.BeginReceive(new AsyncCallback(OnUdpData), UDP1);
		//}

		// *******************************************************************************************************
		private static void OnTimer1Event(Object source, System.Timers.ElapsedEventArgs e, Sip_TCP_Class TCP_Ref)
		{
			TCP_Ref.SendString(Invite1.GetByeMessage());

			StopRecording();

			//System.Diagnostics.Debug.Print("Timer Event");
		}
		// *******************************************************************************************************
		private static void StopRecording()
        {
			MaxTimer1.Stop();
			ActualRTP1.Wav1.RecordActive = false;
			ActualRTP1.Wav1.WriteWavFile();

		}
		// *******************************************************************************************************
		private string RemovCRLF(string Data)
        {
			while (Data.StartsWith("\r\n"))
            {
				Data = Data.Substring(2);
            }

			return Data;

        }
		// *******************************************************************************************************
		// *******************************************************************************************************
		// *******************************************************************************************************


	}
}
