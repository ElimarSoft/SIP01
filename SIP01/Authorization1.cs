using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIP01
{
    static class Authorization1
    {

        public static string realm = "asterisk";
        public static string algorithm = "md5";
        public static string username = "104";
        public static string password = "104";
        public static string uri = "sip:192.168.1.39";
        public static string cnonce = "faaab3b5";
        public static string nc = "00000001";
        public static string qop = "auth";
        public static string opaque;
        public static string nonce;
        public static string response;

        public static string GetAuthorization(string Response)
        {

            string AuthLine = Utils1.GetField(Response, "WWW-Authenticate:");
            nonce = Utils1.GetParam(AuthLine, "nonce");
            opaque = Utils1.GetParam(AuthLine, "opaque");

            response = CriptoClass.Enc(username, realm, password, nonce, nc, cnonce, uri);

            string message = "Authorization: Digest " +
            $"realm='{realm}'," +
            $"nonce='{nonce}'," +
            $"algorithm={algorithm}," +
            $"opaque='{opaque}'," +
            $"username='{username}'," +
            $"uri='{uri}'," +
            $"response='{response}'," +
            $"cnonce='{cnonce}'," +
            $"nc={nc}," +
            $"qop={qop}";

            message= message.Replace("'", "\"");

            return message;
        }

        //private string GetAuthorization(string message)
        //{
        //    string AuthLine = GetField(message, "WWW-Authenticate:");
        //    string nonce = GetParam(AuthLine, "nonce");
        //    string opaque = GetParam(AuthLine, "opaque");

        //    //string Auth =
        //    //"Authorization:  Digest realm = 'asterisk'," +
        //    ////"nonce = '1613235151/da77f000efae58ddad2937c5fe8f31f3'," +
        //    //"nonce " + nonce + "," +
        //    //"algorithm = md5," +
        //    ////			"opaque = '6cadbc0a4cf53d94',"+
        //    //"opaque " + opaque + "," +
        //    //"username = '104', " +
        //    //"uri = 'sip:192.168.1.39', " +
        //    //"response = 'a8354442fb23f9c35d79ef0e9eb54b1a', " +
        //    //"cnonce = 'faaab3b5', " +
        //    //"nc = " + "," +
        //    //"qop = auth";

        //    //Auth = Auth.Replace("'", "\"") + "\r\n\r\n\r\n";


        //    return Auth;
        //}


    }
}
