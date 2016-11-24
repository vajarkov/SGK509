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
using System.Threading;


namespace SGKSocketServer
{
	// State object for reading client data asynchronously
	public class StateObject {
    	// Client  socket.
    	public Socket workSocket = null;
    	// Size of receive buffer.
    	public const int BufferSize = 1024;
    	// Receive buffer.
    	public byte[] buffer = new byte[BufferSize];
		// Received data string.
    	public StringBuilder sb = new StringBuilder();  
	}
	
	/// <summary>
	/// Сервер для передачи данных Modbus клиенту
	/// </summary>
	public class AsynchronousSocketListener {
    
	// 
    public static ManualResetEvent allDone = new ManualResetEvent(false);


    public AsynchronousSocketListener() {
    }

    public static void StartListening() {
    
    	// Data buffer for incoming data.
        byte[] bytes = new Byte[1024];

        // Establish the local endpoint for the socket.
        // The DNS name of the computer
        // running the listener is "host.contoso.com".
        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
		
        // Create a TCP/IP socket.
    	Socket listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp );

        // Bind the socket to the local endpoint and listen for incoming connections.
        try {
        	listener.Bind(localEndPoint);
        	listener.Listen(100);
        	
            while (true) {
                // Set the event to nonsignaled state.
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.
                Console.WriteLine("Waiting for a connection...");
                //listener.BeginAccept(     new AsyncCallback(AcceptCallback),listener );

                // Wait until a connection is made before continuing.
                allDone.WaitOne();
            }

        } catch (Exception e) {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }
    
	}
}