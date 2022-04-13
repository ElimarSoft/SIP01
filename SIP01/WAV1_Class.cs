using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SIP01
{



    //***********************************************************************************
    static class Par1
    {
        //public const int BITSPERSAMPLE = 32;
        //public const int BYTES_SAMPLE = 4;
        public const int BITSPERSAMPLE = 16;
        public const int BYTES_SAMPLE = 2;
        public const int HEADER_SIZE = 44;

    }

    //***********************************************************************************
    class WAVEHEADER
    {
      public byte[] chunkid = { 0,0,0,0};
      public Int32 chunksize;
      public byte[] format= { 0, 0, 0, 0 };
      public byte[] subchunk1id= { 0, 0, 0, 0 };
      public Int32 subchunk1size; // Length of next Header
      public Int16 audioformat;
      public Int16 numchannels;
      public Int32 samplerate;
      public Int32 byterate;
      public Int16 blockalign;
      public Int16 bitspersample;
      public byte[] subchunk2id = { 0, 0, 0, 0 };
      public int subchunk2size;
     };
    //***********************************************************************************
    //  class Chunk
    //{
    //    public char[] Name = new char[4];
    //    public int size;

    //}
    //***********************************************************************************
    //[StructLayout(LayoutKind.Sequential)]
    //class FormatStruct
    //{
    //    public short audioformat;
    //    public short numchannels;
    //    public int samplerate;
    //    public int byterate;
    //    public short blockalign;
    //    public short bitspersample;
    //};
    //***********************************************************************************

    public class WAV1_Class
    {

     public bool RecordActive = false;

     public MemoryStream St1 = new MemoryStream();
     public void WriteWavFile()
        {
            St1.FlushAsync();

            byte[] ByteData1 = St1.ToArray();
            int DataLength = ByteData1.Length;

            byte[] WaveData = new byte[DataLength * 2];


            for (int n = 0; n < DataLength; n++)
            {

                Int16 Value1 = 0;
                if (ByteData1[n] < 128)
                {
                    Value1 = (Int16)(ByteData1[n]);
                    Value1 = (Int16)(Value1 - 128);
                }
                else
                {
                    Value1 = (Int16)(ByteData1[n]);
                    Value1 = (Int16)(255 - Value1);
                }
                byte[] ValueBytes = BitConverter.GetBytes(Value1*256);

                WaveData[2 * n] = ValueBytes[0];
                WaveData[2 * n+1] = ValueBytes[1];
            }
            
            int nsamples = WaveData.Length / Par1.BYTES_SAMPLE;

            int SampleRate = Const.AudioBitRate;
            string FileName;
            string filename1;
            byte[] Message = new byte[32];
            WAVEHEADER hd1 = new WAVEHEADER();

            filename1 = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") +" Out.wav";
 
            FileName = Const.LogFilesPath + filename1;

            hd1.chunkid = Encoding.ASCII.GetBytes("RIFF");
            hd1.chunksize = nsamples * Par1.BYTES_SAMPLE + Par1.HEADER_SIZE - 8;
            hd1.format = Encoding.ASCII.GetBytes("WAVE");

            hd1.subchunk1id = Encoding.ASCII.GetBytes("fmt ");
            hd1.subchunk1size = 16;

            hd1.audioformat = 1; //1 PCM 2
            hd1.numchannels = 1;
            hd1.samplerate = SampleRate;
            hd1.byterate = SampleRate * Par1.BYTES_SAMPLE;  //	(Sample Rate * BitsPerSample * Channels) / 8.
            hd1.blockalign = 2; //2
            hd1.bitspersample = Par1.BITSPERSAMPLE;

            hd1.subchunk2id = Encoding.ASCII.GetBytes("data");
            hd1.subchunk2size = nsamples * Par1.BYTES_SAMPLE;

            using (BinaryWriter bw1 =
             new BinaryWriter(File.Open(FileName, FileMode.Create)))
            {
                bw1.Write(hd1.chunkid);
                bw1.Write(hd1.chunksize);

                bw1.Write(hd1.format);
                bw1.Write(hd1.subchunk1id);
                bw1.Write(hd1.subchunk1size);
                bw1.Write(hd1.audioformat);
                bw1.Write(hd1.numchannels);
                bw1.Write(hd1.samplerate);
                bw1.Write(hd1.byterate);
                bw1.Write(hd1.blockalign);
                bw1.Write(hd1.bitspersample);
                bw1.Write(hd1.subchunk2id);
                bw1.Write(hd1.subchunk2size);
                bw1.Write(WaveData);

            }

            St1.SetLength(0);

        }
       

    }
}
