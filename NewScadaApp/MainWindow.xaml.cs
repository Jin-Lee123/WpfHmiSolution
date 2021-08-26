using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace NewScadaApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private string serverIpNum = "192.168.127.185";  // 윈도우(MQTT Broker, SQLServer) 아이피
        private string clientId = "SCADA_system";
        private string factoryId = "Kasan01";            //  Kasan01/4001/  kasan01/4002/ 
        private string motorAddr = "4002";
        private string tankAddr = "4001";

        private string connectionString = string.Empty;  // SQLServer 연결문자열
        private MqttClient client;                       // MQTT 접속을 객체
        
        public MainWindow()
        {
            InitializeComponent();
            App.LOGGER.Info("SCADA Strat");  // 시작 로그
            InitAllCustomComponnet();
        }
        // SCADA 시스템용 커스텀 초기화 메서드
        private void InitAllCustomComponnet()
        {
            LblStatus.Content = string.Empty;
            // IPAddress serverAddress = IPAddress.Parse(serverIpNum);
            client = new MqttClient(serverIpNum);
            client.MqttMsgPublished += Client_MqttMsgPublished;
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            client.ConnectionClosed += Client_ConnectionClosed;

            connectionString = "Data Source=localhost;Initial Catalog=HMI_Data;Integrated Security=True";
        }
        // MQTT 서버와 접속이 끊어졌을때 이벤트 처리
        private void Client_ConnectionClosed(object sender, EventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                LblStatus.Content = "모니터링 종료!";
            }));
         }
        // MQTT에서 메세지를 구독하면 이벤트처리(****)
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                // 대리자에게 대신 UI 스레드에 속한 컨트롤에 일처리 호출
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    var message = Encoding.UTF8.GetString(e.Message); //e.Message(byte[]) ==> string 변환
                    LblStatus.Content = message;

                    // JSON 넘어온 데이터 확인 후 내부 SCADA 작업!
                    /**
                     {
                        "dev_addr" : "4001",
                        "currtime" : "2021-08-06 11:05:30",
                        "code"     : "red",
                        "value"    : "1" 
                      }
                     **/
                    var currData = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);
                    Debug.WriteLine(currData);

                    if (currData["dev_addr"] == "4001" )  //Tank로 부터 데이터 수신
                    {
                        if (currData["code"] == "red" && currData["value"] == "1")        // 경고등 ON
                        {
                            LedState.CurrState = Color.FromRgb(255, 0, 0);
                            MessageBox.Show("비상상황! FuelTank 모터를 구동하십시오!");
                        }
                        else if (currData["code"] == "green" && currData["value"] == "1")  //정상작동 ON
                        {
                            LedState.CurrState = Color.FromRgb(0, 255, 0);
                        }
                        else if (currData["value"] == "0")                                 // LED OFF
                        {
                            LedState.CurrState = Color.FromRgb(80, 80, 80);
                        }
                    }

                    InsertData(currData);  //HMI_Data/Kasan01_Device 테이블에 입력
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                App.LOGGER.Error($"예외발생,Client_MqttMsgPublishReceived : [{ex.Message}]");
            }
            
        }
        //SQL Server 테이블 입력처리
        private void InsertData(Dictionary<string, string> currData)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string insertQuery = $@"INSERT INTO Kasan01_Device
                                               (dev_addr
                                               ,currtime
                                               ,code
                                               ,value)
                                         VALUES
                                               ('{currData["dev_addr"]}'
                                               ,'{currData["currtime"]}'
                                               ,'{currData["code"]}'
                                               ,'{currData["value"]}')";
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    #region
                    //SqlParameter paramDev_addr = new SqlParameter("@dev_addr", currData["dev_addr"]);
                    //SqlParameter paramCurrtime = new SqlParameter("@currtime", currData["currtime"]);
                    //SqlParameter paramCode = new SqlParameter("@code", currData["code"]);
                    //SqlParameter paramValue = new SqlParameter("@value", currData["value"]);
                    //cmd.Parameters.Add(paramDev_addr);
                    //cmd.Parameters.Add(paramCurrtime);
                    //cmd.Parameters.Add(paramCode);
                    //cmd.Parameters.Add(paramValue);
                    #endregion 

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        App.LOGGER.Info("Iot 데이터 입력 성공!");
                    }
                    else
                    {
                        App.LOGGER.Error($"오류발생, InsertData Iot 데이터 입력 실패! : [{insertQuery}]");
                    }
                }
                catch (Exception ex)
                {
                    App.LOGGER.Error($"예외발생, InsertData : [{ex.Message}]");
                }
            }  // conn.Disconnect()자동샐행
        }



        // MQTT 에서 메세지를 발행(한뒤) 이벤트 처리
        private void Client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            // TODO :
        }



        // 모니터링 시작 버튼 클릭처리
        private void BtnMonitoring_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (client.IsConnected)
                {
                    client.Disconnect();
                    App.LOGGER.Info("모니터링 종료 : BtnMonitoring_Click");
                    BtnMonitoring.Content = "Start Monitoring";
                }
                else
                {
                    client.Connect(clientId);  //MQTT 접속
                                               //구독하면서 메서지 모니터링 시작
                    client.Subscribe(new string[] { $"{factoryId}/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    LblStatus.Content = "모니터링 시작!";
                    App.LOGGER.Info("모니터링 시작 : BtnMonitoring_Click");
                    BtnMonitoring.Content = "Stop Monitoring";
                }
               
            }
            catch (Exception ex)
            {
                App.LOGGER.Error($"예외발생,BtnMonitoring_Click : [{ex.Message}]");
            }
        }

        // 위급시 모터 동작처리
        private void BtnMotor_CustomClick(object sender, RoutedEventArgs e)
        {
            if (client.IsConnected)
            {
                var currtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string pubData = "{ " +
                                 "   \"dev_addr\" : \"4002\", " +
                                 $"   \"currtime\" : \"{currtime}\" , " +
                                 "   \"code\" : \"motor\", " +
                                 "   \"value\" : \"1\" " +
                                 "}";

                client.Publish($"{factoryId}/4002", Encoding.UTF8.GetBytes(pubData), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
                //==> byte  형태로 변환하여 전송
                LblStatus.Content = pubData;

                App.LOGGER.Warn("위급처리 : FuelTank정지할 수 있습니다!");
                MessageBox.Show("위급처리!");
            }
            else
            {
                MessageBox.Show("모니터링에 접속하지 않았습니다. \n 먼저 접속을 시도하세요.");
            }
        }

        // 메인 윈도우 닫히기 전 처리
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 리소스 해제
            if (client.IsConnected) client.Disconnect();  //
            App.LOGGER.Info("SCADA 프로그램 종료!");
        }
    }
}
