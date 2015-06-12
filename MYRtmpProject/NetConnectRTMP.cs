using FluorineFx;
using FluorineFx.Net;

using LitJson;
using System.Collections.Generic;

namespace MYRtmpProject
{
    class NetConnectRTMP
    {

        static public NetConnection _net;

        public RtmpS2C rtmpS2C;

        string m_sid;

        public void Initial(string sid)
        {
            
            m_sid = sid;

            rtmpS2C = new RtmpS2C(m_sid);

            _net = new NetConnection();
            _net.ObjectEncoding = ObjectEncoding.AMF3;
            _net.Client = rtmpS2C;

            _net.OnConnect += new ConnectHandler(this.OnConnect);
            _net.OnDisconnect += new DisconnectHandler(this.OnDisConnect);
            _net.NetStatus += new NetStatusHandler(this.OnNetStatus);
        }

        public void Connect()
        {
            //_net.Connect("rtmp://103.240.216.205:1935/SlotMachine");
            //_net.Connect("rtmp://103.252.135.2:23/SlotMachine");
            _net.Connect("rtmp://103.252.135.2:23/SlotMachine/service.mob");
        }

        public void DisConnect()
        {

            _net.Close();
        }

        public void SetAutoSpin(bool sw)
        {

            rtmpS2C.sw_autospin = sw;
        }


        public void OnConnect(object obj, System.EventArgs e)
        {

            Debug.Log("OnConnect");
            string str_json = JsonMapper.ToJson(obj);

            Debug.Log("OnConnect str_json " + str_json);

            //_net.Call("loginBySid", null, m_sid, "5835");
            _net.Call("loginBySid", null, m_sid, "5106");

            Debug.Log("Do loginBySid");
        }

        public void Spin()
        {
            Dictionary<string, int> betinfo = new Dictionary<string, int>();
            betinfo.Add("LineNum", 1);
            betinfo.Add("LineBet", 1);

            _net.Call("beginGame4", null, betinfo);

            /*_net.Call("beginGame4", null,
                m_sid,
                "1", "1");*/

        }

        public void endgame()
        {
            NetConnectRTMP._net.Call("endGame", null, m_sid, rtmpS2C.m_WagersID);
        }

        public void OnDisConnect(object obj, System.EventArgs e)
        {
            Debug.Log("OnDisConnect");
        }
        public void OnNetStatus(object obj, NetStatusEventArgs e)
        {
            Debug.Log("OnNetStatus");
        }

    }
}
