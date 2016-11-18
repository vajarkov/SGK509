/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 11/18/2016
 * Time: 10:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SGKSocketServer
{
	/// <summary>
	/// Сервер для передачи данных Modbus клиенту
	/// </summary>
	public class SocketServer
	{
		// Incoming data from the client.
    	public static string data = null;

    	public static void StartListening(int sum_size, int port) {
        // Data buffer for incoming data.
        byte[] bytes = new Byte[sum_size];

        // Establish the local endpoint for the socket.
        // Dns.GetHostName returns the name of the 
        // host running the application.
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        // Create a TCP/IP socket.
        Socket listener = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp );

        // Bind the socket to the local endpoint and 
        // listen for incoming connections.
        try {
            listener.Bind(localEndPoint);
            listener.Listen(10);

            // Start listening for connections.
            while (true) {
                // Program is suspended while waiting for an incoming connection.
                Socket handler = listener.Accept();
                data = null;
				handler.Send(msg);
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();
            }

        } catch (Exception e) {
            Console.WriteLine(e.ToString());
        }

        
        Console.Read();

    }

    public static int Main(String[] args) {
        StartListening();
        return 0;
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
	}
}