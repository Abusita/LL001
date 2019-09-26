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

    private  GameObject battleControl;
    private  BattleControl battleControlCS;

    private  byte[] result = new byte[1024];

    private  IEnumerator resciveCor;
    private bool isRecMsg;
    private MsgPack msg;
    public static Socket clientSocket;

    void ConnectToSever()
    {
        //设定服务器IP地址 
        IPAddress ip = IPAddress.Parse("127.0.0.1");
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

        //开始监听
        Thread resciveTr = new Thread(ResciveMsg);
        resciveTr.Start(clientSocket);
        //Thread sendTr = new Thread(SendMessage);
        //sendTr.Start(clientSocket);

        //resciveCor = WaitAndPrint(0.2f, clientSocket);
        //StartCoroutine(resciveCor);

    }


    private IEnumerator WaitAndPrint(float waitTime, object clientSocket)
    {
        Debug.Log("IEnum");
        while (true)
        {
            Socket myClientSocket = (Socket)clientSocket;
            byte[] tempBuffer = new byte[1024 * 1024 * 2];
            var effective = myClientSocket.Receive(tempBuffer);
            byte[] resultBuffer = new byte[effective];

            Array.Copy(tempBuffer, 0, resultBuffer, 0, effective);
            if (effective == 0)
            {
                break;
            }
            msg = ProtoSerialize.Deserialize<MsgPack>(resultBuffer);
            MsgHandle.ReceiveMsgList.Enqueue(msg);

            //isRecMsg = true;
            /*foreach (CampInfo item in msg.InitItemPack.CampInfos)
                {
                    Debug.Log(item.Camp);
                    Debug.Log(item.ItemsCount);
                    foreach (CardMsg c in item.CardItems)
                    {
                        Debug.Log("c.Id: " + c.Id + "  c.BornPos:" + c.BornPos);
                    }
                }*/

            //battleControlCS.InitScene(message);
            yield return new WaitForSeconds(waitTime);
        }
        
    }


    public static void SendMsg(MsgPack msg)
    {
        var m = ProtoSerialize.Serialize<MsgPack>(msg);
        clientSocket.Send(m);
        Debug.Log("send");
    }

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
                msg = ProtoSerialize.Deserialize<MsgPack>(resultBuffer);
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

    static void SendMessage(object clientSocket)
    {
        Socket myClientSocket = (Socket)clientSocket;
        while (true)
        {
            try
            {
                Thread.Sleep(2000);    //等待1秒钟 
                string sendMessage = "client send Message Hellp ";
                myClientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                Debug.Log("向客户端发送消息：{0}" + sendMessage);
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
        battleControl = GameObject.Find("BattleControl");
        battleControlCS = battleControl.GetComponent<BattleControl>();
        isRecMsg = false;
        msg = new MsgPack();


        ConnectToSever();
    }

    // Update is called once per frame
    void Update()
    {
        
        /*if (isRecMsg)
        {
            battleControlCS.InitScene(msg);
            isRecMsg = false;
        }*/
	}
}
