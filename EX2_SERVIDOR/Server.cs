using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EX2_SERVIDOR
{
    internal class Server
    {
        private List<Usuario> usuarios = new List<Usuario>();
        private IPAddress ip;
        private int port;
        public static Socket socket;

        public void Init()
        {
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            using (socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {


            }
        }

        public bool isFreePort(int port)
        {
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            using (socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    socket.Bind(endPoint);
                    socket.Listen(1);
                }
                catch (SocketException s) when (s.SocketErrorCode == SocketError.AddressAlreadyInUse) // TODO OJO utilizo propiedad socketError no ErrorCode
                {
                    port++;
                } catch (SocketException)
                {
                    port++;
                }
            }
        }

        public void Stop()
        {

        }
    }
}
