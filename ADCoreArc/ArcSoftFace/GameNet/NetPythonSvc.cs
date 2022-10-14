using ArcSoftFace.ADCoreSystem;
using ArcSoftFace.ADCoreSystem.ADCoreGameWindow;
using Newtonsoft.Json;
using RRQMSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.GameNet
{
    public  class NetPythonSvc
    {
        public void Start()
        {
            if (System.Diagnostics.Process.GetProcessesByName("引体向上测试系统").ToList().Count > 1)
            {
                Application.Exit();
            }
            else
            {
                StartPythonServer();
            }
        }
        public class MsgData
        {
            public string cmd { get;set; }
            public string data { get; set; }
        }
        TcpService service = new TcpService();


        private void StartPythonServer()
        {
            service.Connecting += (client, e) =>
            {
                e.DataHandlingAdapter = new TerminatorPackageAdapter(1024 * 1024, "\r\n");
            };// 有客户端连接
            service.Connected += (client, e) => { };
            service.Received += (client, byteBlock, requestInfo) =>
            {
                // 接收来自客户端的信息
                string mes = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);
                //Debug.Log($"已从{client.Name}接收到信息：{mes}");//Name即IP+Port
                MsgData msg = JsonConvert.DeserializeObject<MsgData>(mes);
                NetPython(msg);
                if (msg.data != "Up_Start")
                {
                    Console.WriteLine(msg.cmd);
                    if (client.Online)
                    {
                        int num;
                        if (int.TryParse(StartTestingWindow.cameraId.ToString(), out num))
                        {
                             string str = "{ \"cmd\": \"Up_Start_VideoCapture\", \"msg\": \"" + num + "\"}\r\n";

                             byte[] byteArray = Encoding.Default.GetBytes(str);
                             client.Send(byteArray);
                        }
                        else
                        {
                            Console.WriteLine("相机名字不合理");
                        }
                    }
                }
            };
            //声明配置
            var config = new TcpServiceConfig();
            config.ListenIPHosts = new IPHost[] { new IPHost("127.0.0.1:7789"), new IPHost(7790) };//同时监听两个地址
                                                                                                   //载入配置                                                       
            service.Setup(config);
            service.Start();

        }

        private void NetPython(MsgData msg)
        {
            switch (msg.cmd)
            {
                case "SitUp":
                    StartTestingSys.Instance.Up_Test(msg.data);
                    break;
                default:
                    break;
            }
        }
    }
}
