using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class BallBounceMarker : MonoBehaviour {

    Socket s;
    byte[] sendbuf;
    IPEndPoint ep;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
    ProtocolType.Udp);

        IPAddress broadcast = IPAddress.Parse("127.0.0.1");

        sendbuf = Encoding.ASCII.GetBytes("BallBounce 1\n");
        ep = new IPEndPoint(broadcast, 20320);

        if (col.gameObject.tag == "court")
        {
            s.SendTo(sendbuf, ep);


            System.Threading.Thread.Sleep(100);
            sendbuf = Encoding.ASCII.GetBytes("BallBounce 0\n");
            s.SendTo(sendbuf, ep);


            s.Close();
        }

        /*if (col.gameObject.tag == "court")
        {
            render.material.color = Color.Lerp(Color.red, Color.green, 0);
            print(transform.lossyScale.y);
        }
        */

    }
}
