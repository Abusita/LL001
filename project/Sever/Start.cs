using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

using CsProtobuf;

namespace Sever
{
    class Start
    {
      
        static void Main(string[] args)
        {

            //启动数据管理器
            DataManager.Start();
            //启动连接管理器
            ConnectManager.Start();
            //启动消息管理器
            MsgManager.Start();
        }
    }
}
