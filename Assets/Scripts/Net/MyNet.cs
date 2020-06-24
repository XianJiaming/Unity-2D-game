using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class MyNet
{




    UdpClient client;

    void Main()
    {
        client = new UdpClient(new IPEndPoint(IPAddress.Any, 7788));

        IPEndPoint point = new IPEndPoint(IPAddress.Any, 0);

        byte[] data = client.Receive(ref point);

        string message = Encoding.UTF8.GetString(data);
    }

    void Send()
    {
        byte[] data = new byte[] { 1 };
        client.Send(data, data.Length, new IPEndPoint(IPAddress.Any, 7788));
    }
    //建房间
    void CreatRoom()
    {

    }
    //查找局域网内的房间
    void FindRoom()
    {

    }
    //进入房间
    void EnterRoom()
    {

    }
}
    

