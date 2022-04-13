
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Timers;

namespace SIP01
{
	public class  Sip_UDP_Class
	{

		UdpClient UDP1;
		IPEndPoint target;
		static IPEndPoint source;
		public static bool Registered = false;
		RTP_UDP_Class RTP1 = new RTP_UDP_Class();
		static RTP_UDP_Class ActualRTP1;
		
		private static Timer MaxTimer1;

		//delegate void OnUdpData(IAsyncResult result);

		const string CrLf = "\r\n";

		// *******************************************************************************************************
		public Sip_UDP_Class()
        {

			UDP1 = new UdpClient(Const.SIPort); // Source Address

			target = new IPEndPoint(IPAddress.Parse(Const.Server_IP), Const.SIPort); // Destination
			source = new IPEndPoint(0, 0);

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
			
			System.Threading.Thread.Sleep(2000);
			if (UDP1.Available > 0)
            {
				UDP1.Receive(ref source);
				System.Threading.Thread.Sleep(1000);
				if (UDP1.Available > 0)
				{
					string ReceiveStr01 = Encoding.ASCII.GetString(UDP1.Receive(ref source));
					if (ReceiveStr01.StartsWith("OPTIONS"))
                    {
						Registered = true;
                    }
				
				}
			}

			if (!Registered)
            {
				Registered = Register();
            }

			UDP1.BeginReceive(new AsyncCallback(OnUdpData), this);

        }

		// *******************************************************************************************************
		public bool Register()
		{

			SendString(Register1.GetMessage() + CrLf + CrLf);
			System.Threading.Thread.Sleep(100);
			if (UDP1.Available == 0) return false;
			string ReceiveStr01 = Encoding.ASCII.GetString(UDP1.Receive(ref source));

			if (!ReceiveStr01.StartsWith(ResponseCodes.Unauthorized))
			{
				System.Diagnostics.Debug.Print("Receive1 Abort");
				return false;
			}
			SendString(Register1.GetMessage() + Authorization1.GetAuthorization(ReceiveStr01) + CrLf + CrLf);
			System.Threading.Thread.Sleep(100);
			if (UDP1.Available == 0) return false;
			string ReceiveStr02 = Encoding.ASCII.GetString(UDP1.Receive(ref source));
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
		private static void OnUdpData(IAsyncResult result)
		{
			System.Diagnostics.Debug.Print("Async Data Received");

			Sip_UDP_Class Sipx = result.AsyncState as Sip_UDP_Class;
			string ReceiveStr = Encoding.ASCII.GetString(Sipx.UDP1.EndReceive(result, ref source));
			if (ReceiveStr.StartsWith("OPTIONS"))
			{
				Registered = true;
				Sipx.SendString(Options1.GetMessage(ReceiveStr));
			}
			else if (ReceiveStr.StartsWith("INVITE"))
			{
				Registered = true;
				SDP1.GetInvite(ReceiveStr);
				Sipx.SendString(Invite1.GetTryingMessage(ReceiveStr));
				Sipx.SendString(Invite1.GetRingingMessage());
				Sipx.SendString(Invite1.GetOkMessage());
				MaxTimer1.Start();
				ActualRTP1.Wav1.RecordActive = true;

			}
			else if (ReceiveStr.StartsWith("BYE"))
			{
				Registered = true;
				Sipx.SendString(BYE1.GetMessage(ReceiveStr));
				StopRecording();

			}
			else if (ReceiveStr.StartsWith("NOTIFY"))
			{
				Registered = true;
				//Sipx.Send(Notify1.GetMessage(ReceiveStr));
			}

			Sipx.UDP1.BeginReceive(new AsyncCallback(OnUdpData), Sipx);
		}

		// *******************************************************************************************************

		// *******************************************************************************************************
		private void SendString (string Message)
		{ 
			byte[] message = Encoding.ASCII.GetBytes(Message);
			UDP1.Send(message, message.Length,target);
	
		}
		// *******************************************************************************************************

		//private void ReceiveAsync1()
  //      {
		//	UDP1.BeginReceive(new AsyncCallback(OnUdpData), UDP1);
		//}

		// *******************************************************************************************************
		private static void OnTimer1Event(Object source, System.Timers.ElapsedEventArgs e, Sip_UDP_Class UDP_Ref)
		{
			UDP_Ref.SendString(Invite1.GetByeMessage());

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



	}
	}
