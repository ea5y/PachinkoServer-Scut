using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Script.CsScript.Cast
{
    public interface ICast
    {
        void Send();
    }

    public class DispatchCast
    {
        public static void Send(ICast cast)
        {
            cast.Send();
        }
    }
}
