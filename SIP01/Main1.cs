//#define UDP
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIP01
{
    public class Main1
    {

    #if (UDP)
                    public Sip_UDP_Class Sip1 = new Sip_UDP_Class();
    # elif (TCP)
                    public Sip_TCP_Class Sip1 = new Sip_TCP_Class();
    #endif

    }
}
