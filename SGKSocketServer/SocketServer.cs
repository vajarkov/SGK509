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
using DataTransfer;
using Interfaces;
using System.Diagnostics;

namespace SGKSocketServer
{
	
	// State object for reading client data asynchronously
public class StateObject {
    // Client  socket.
    public Socket workSocket = null;
    // Size of receive buffer.
    public int BufferSize;
    // Receive buffer.
    public byte[] buffer;
// Received data string.
    //public StringBuilder sb = new StringBuilder();
    
    public StateObject(int lenghtBytes)
    {
    	BufferSize = lenghtBytes;
    	buffer = new byte[BufferSize];
    }
}

public class AsynchronousSocketListener {
    // Thread signal.
    public static ManualResetEvent allDone = new ManualResetEvent(false);

    
    // Объект манипуляции данными
    private static IDataClass dataObject = new DataClass();
    // Журнал событий
    private static EventLog eventLog = new EventLog();
    // Длина массива для передачи данных
    private static int lenghtBytes = dataObject.GetInitSize();
   
    
    public AsynchronousSocketListener() {
    }

    
    public static void StartListening() {
        
    	// Data buffer for incoming data.
    	int BufferSize = lenghtBytes;
    	
    	dataObject.GetEventConfig();
    	
    	// Имя журнала
    	eventLog.Log = dataObject.GetServiceName();
        // Имя источника
        eventLog.Source = dataObject.GetServiceName();
        // Имя компьютера
        eventLog.MachineName = dataObject.GetHostName();
        
        
    	byte[] bytes = new Byte[lenghtBytes];

        // Establish the local endpoint for the socket.
        // The DNS name of the computer
        // running the listener is "host.contoso.com".
        //IPHostEntry ipHostInfo = Dns.GetHostEntry(dataObject.GetServiceIPAddress());
        //IPAddress ipAddress = ipHostInfo. .AddressList[0];
        IPAddress ip;
        eventLog.WriteEntry(dataObject.GetServiceIPAddress());
        if(!IPAddress.TryParse(dataObject.GetServiceIPAddress(), out ip))
        {
        	throw new FormatException("Invalid ip-adress");
        }
    	
        IPEndPoint localEndPoint = new IPEndPoint(ip, dataObject.GetServicePort());

        // Create a TCP/IP socket.
        Socket listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp );

        // Bind the socket to the local endpoint and listen for incoming connections.
        try {
            listener.Bind(localEndPoint);
            listener.Listen(10);

            while (true) {
                // Set the event to nonsignaled state.
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.
                listener.BeginAccept( 
                    new AsyncCallback(AcceptCallback),
                    listener );

                // Wait until a connection is made before continuing.
                allDone.WaitOne();
            }

        } catch (Exception e) {
            eventLog.WriteEntry(e.ToString());
        }

    }

    public static void AcceptCallback(IAsyncResult ar) {
        // Signal the main thread to continue.
        allDone.Set();

        // Get the socket that handles the client request.
        Socket listener = (Socket) ar.AsyncState;
        Socket handler = listener.EndAccept(ar);
		       
        
        // Create the state object.
        StateObject state = new StateObject(1);
        state.workSocket = handler;
        handler.BeginReceive( state.buffer, 0, 1, 0,
            new AsyncCallback(ReadCallback), state);
    }

    
    public static void ReadCallback(IAsyncResult ar) {
        String content = String.Empty;

        // Retrieve the state object and the handler socket
        // from the asynchronous state object.
        StateObject state = (StateObject) ar.AsyncState;
        Socket handler = state.workSocket;

        // Read data from the client socket. 
        int bytesRead = handler.EndReceive(ar);
        Send(handler, dataObject.GetSocketData());
        if (bytesRead > 0) {
                // Отправка данных по Modbus
            } else {
                // Not all data received. Get more.
                handler.BeginReceive(state.buffer, 0, 1, 0,
                new AsyncCallback(ReadCallback), state);
            }
        }
    
    
    private static void Send(Socket handler, byte[] data) {
        // Begin sending the data to the remote device.
        handler.BeginSend(data, 0, data.Length, 0,
            new AsyncCallback(SendCallback), handler);
    }
    
    private static void SendCallback(IAsyncResult ar) {
        try {
            // Retrieve the socket from the state object.
            Socket handler = (Socket) ar.AsyncState;

            // Complete sending the data to the remote device.
            int bytesSent = handler.EndSend(ar);
           
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

        } catch (Exception e) {
            eventLog.WriteEntry(e.ToString());
        }
	}
	}

}