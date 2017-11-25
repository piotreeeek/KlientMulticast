using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace KlientMulticast
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient client = new UdpClient();

            client.ExclusiveAddressUse = false;
            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 2222);
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;

            client.Client.Bind(localEp);

            IPAddress multicastAddr = IPAddress.Parse("239.0.0.222");
            client.JoinMulticastGroup(multicastAddr);
            while (true)
            {
                byte[] dataByte = client.Receive(ref localEp);
                string data = Encoding.UTF8.GetString(dataByte);
                Console.WriteLine(data);
            }
        }
    }
}
