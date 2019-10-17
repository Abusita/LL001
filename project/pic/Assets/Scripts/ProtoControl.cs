using System;
using System.Text;

using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Google.Protobuf;
using CsProtobuf;
using System.Collections;

public class ProtoControl : MonoBehaviour {

    public static Socket clientSocket;

    /// <summary>
    /// 连接到服务器
    /// </summary>
    void ConnectToSever()
    {
        //设定服务器IP地址 
        IPAddress ip = IPAddress.Parse("10.0.117.46");
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(new IPEndPoint(ip, 8885)); //配置服务器IP与端口 
            Debug.Log("连接服务器成功");
        }
        catch
        {
            Debug.Log("连接服务器失败，请按回车键退出！");
            return;
        }

        //开始监听服务器消息
        Thread resciveTr = new Thread(ResciveMsg);
        resciveTr.Start(clientSocket);
    }



    /// <summary>
    /// 发送消息接口
    /// </summary>
    /// <param name="msg"></param>
    public static void SendMsg(MsgPack msg)
    {
        var m = ProtoSerialize.Serialize<MsgPack>(msg);
        clientSocket.Send(m);
    }

    /// <summary>
    /// 接收消息接口
    /// </summary>
    /// <param name="clientSocket">本地客户端</param>
    void ResciveMsg(object clientSocket)
    {
        while (true)
        {
            Socket myClientSocket = (Socket)clientSocket;
            try
            {
                byte[] tempBuffer = new byte[1024 * 1024 * 2];
                var effective = myClientSocket.Receive(tempBuffer);
                byte[] resultBuffer = new byte[effective];

                Array.Copy(tempBuffer, 0, resultBuffer, 0, effective);
                if (effective == 0)
                {
                    break;
                }
                MsgPack msg = ProtoSerialize.Deserialize<MsgPack>(resultBuffer);
                MsgHandle.ReceiveMsgList.Enqueue(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                myClientSocket.Shutdown(SocketShutdown.Both);
                myClientSocket.Close();
                break;
            }
        }

    }

    // Use this for initialization
    void Start()
    {
        ConnectToSever();
    }

    // Update is called once per frame
    void Update()
    {
        
	}
}
