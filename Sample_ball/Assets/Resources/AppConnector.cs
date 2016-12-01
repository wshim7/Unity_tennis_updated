using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class AppConnector : MonoBehaviour
{
    internal Boolean socketReady = false;
    NetworkStream theStream;
    StreamWriter theWriter;
    StreamReader theReader;

    // receiving Thread
    Thread receiveThread;

    UdpClient mySocket;
    String host = "localhost";
    public int port; // = 20321;

    // infos
    public string lastReceivedUDPPacket = "";
    public string allReceivedUDPPackets = ""; // clean up this from time to time!

    // start from shell
    private static void Main()
    {
        AppConnector receiveObj = new AppConnector();
        receiveObj.init();

        string text = "";
        do
        {
            text = Console.ReadLine();
        }
        while (!text.Equals("exit"));
    }

    // Use this for initialization

    // OnGUI
    void OnGUI()
    {
        Rect rectObj = new Rect(40, 10, 200, 1000);
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        GUI.Box(rectObj, "# UDPReceive\n127.0.0.1 " + port + " #\n"
                    + "shell> nc -u 127.0.0.1 : " + port + " \n"
                    + "\nLast Packet: \n" + lastReceivedUDPPacket
                    + "\n\nAll Messages: \n" + allReceivedUDPPackets
                , style);
    }

    // start from unity3d
    public void Start()
    {

        init();
    }

    // init
    private void init()
    {
        // Endpunkt definieren, von dem die Nachrichten gesendet werden.
        print("UDPSend.init()");

        // define port
        port = 20321;

        // status
        print("Sending to 127.0.0.1 : " + port);
        print("Test-Sending to this Port: nc -u 127.0.0.1  " + port + "");


        // ----------------------------
        // Abhören
        // ----------------------------
        // Lokalen Endpunkt definieren (wo Nachrichten empfangen werden).
        // Einen neuen Thread für den Empfang eingehender Nachrichten erstellen.
        receiveThread = new Thread(
            new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

    }

    // receive thread
    private void ReceiveData()
    {

        mySocket = new UdpClient(port);
        while (true)
        {

            try
            {
                // Bytes empfangen.
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);

                //mySocket.Connect(host, port);
                //byte[] data = mySocket.Receive();
                byte[] data = mySocket.Receive(ref anyIP);

                // Bytes mit der UTF8-Kodierung in das Textformat kodieren.
                string text = System.Text.Encoding.UTF8.GetString(data);

                // Den abgerufenen Text anzeigen.
                print(">> " + text);

                // latest UDPpacket
                lastReceivedUDPPacket = text;

                // ....
                //if (text.Contains("BallBounce"))
                //{
                    allReceivedUDPPackets = allReceivedUDPPackets + text;
                //}
                //mySocket.Close();
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
            Thread.Sleep(2);
        }
    }

    // getLatestUDPPacket
    // cleans up the rest
    public string getLatestUDPPacket()
    {
        allReceivedUDPPackets = "";
        return lastReceivedUDPPacket;
    }

    void OnDisable()
    {
        Debug.Log("OnDisable: Terminating Socket");

        if (receiveThread != null && receiveThread.IsAlive)
            receiveThread.Abort();

        //mySocket.Close();
    }

    void OnApplicationQuit()
    {

        Debug.Log("OnApplicationQuit: Terminating Socket");

        if (receiveThread != null && receiveThread.IsAlive)
            receiveThread.Abort();
        //}
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy: Terminating Socket");

        if (receiveThread != null && receiveThread.IsAlive)
            receiveThread.Abort();
    }

        /*
    void Start () {
            try
            {
                mySocket = new UdpClient(Host, Port);
                //theStream = mySocket.GetStream();
                //theWriter = new StreamWriter(theStream);
                //theReader = new StreamReader(theStream);
                // Sends a message to the host to which you have connected.
                //Byte[] sendBytes = System.Text.Encoding.ASCII.GetBytes("Is anybody there?");

                //mySocket.Send(sendBytes, sendBytes.Length);

                socketReady = true;
                Debug.Log("Setup Successful");
            }
            catch (Exception e)
            {
                Debug.Log("Socket error: " + e);
            }
        }

        // Update is called once per frame
        void Update () {

            //IPEndPoint object will allow us to read datagrams sent from any source.
            System.Net.IPEndPoint RemoteIpEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 20321);

            if (mySocket.Available > 0)
            {
                // Blocks until a message returns on this socket from a remote host.
                Byte[] receiveBytes = mySocket.Receive(ref RemoteIpEndPoint);
                string returnData = System.Text.Encoding.ASCII.GetString(receiveBytes);

                // Uses the IPEndPoint object to determine which of these two hosts responded.
                Console.WriteLine("This is the message you received " +
                                             returnData.ToString());
                Console.WriteLine("This message was sent from " +
                                            RemoteIpEndPoint.Address.ToString() +
                                            " on their port number " +
                                            RemoteIpEndPoint.Port.ToString());
            }

        }

        // **********************************************
        public void setupSocket()
        {
            try
            {
               mySocket = new TcpClient(Host, Port);
                theStream = mySocket.GetStream();
                theWriter = new StreamWriter(theStream);
                theReader = new StreamReader(theStream);
                socketReady = true;
                Debug.Log("Setup Successful");
           }
            catch (Exception e)
            {
                Debug.Log("Socket error: " + e);
            }
        }
        public void writeSocket(string theLine)
        {
            if (!socketReady)
                return;
            String foo = theLine + "\r\n";
            theWriter.Write(foo);
            theWriter.Flush();
        }
        public String readSocket()
        {
            if (!socketReady)
                return "";
            if (theStream.DataAvailable)
                return theReader.ReadLine();
            return "";
        }
        public void closeSocket()
        {
            if (!socketReady)
                return;
            theWriter.Close();
            theReader.Close();
            mySocket.Close();
            socketReady = false;
        }
    */
    }
