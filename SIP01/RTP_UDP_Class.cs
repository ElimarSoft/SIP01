using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SIP01
{
    public class RTP_UDP_Class
    {
        public UdpClient UDP1;
        IPEndPoint target;
        static IPEndPoint source;

        public WAV1_Class Wav1 = new WAV1_Class();
        public static WAV1_Class ActualWav1;
 
        public RTP_UDP_Class()
        {
            UDP1 = new UdpClient(Const.RTPPortLocal); // Source Address
            target = new IPEndPoint(IPAddress.Parse(Const.Server_IP), Const.RTPPortLocal); // Destination
            source = new IPEndPoint(0, 0);

            AsyncCallback CallBack2 = new AsyncCallback(OnUdpData);
            UDP1.BeginReceive(CallBack2, this.UDP1);

            ActualWav1 = Wav1;

        }
        // *******************************************************************************************************
          private static void OnUdpData(IAsyncResult result)
		{
            UdpClient UDP1 = result.AsyncState as UdpClient;
            byte[] MessData = UDP1.EndReceive(result, ref source);

            byte[] VoiceData = new byte[MessData.Length - 12];
            Array.Copy(MessData, 12, VoiceData, 0, VoiceData.Length);
            

            if (ActualWav1.RecordActive) ActualWav1.St1.Write(VoiceData, 0, VoiceData.Length);
 
            AsyncCallback CallBack1 = new AsyncCallback(OnUdpData);
            UDP1.BeginReceive(CallBack1, UDP1);
		}

        // *******************************************************************************************************
        // MaxTimer1.Elapsed += (sender, e) => OnTimer1Event(sender, e, this);

        // *******************************************************************************************************
       
        






    }
}
