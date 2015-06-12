using System;
using System.Collections.Generic;

using FluorineFx;
using LitJson;

namespace MYRtmpProject
{
    public class RtmpS2C
    {

        public string m_sid;

        public string m_WagersID;

        public bool sw_autospin;


        public RtmpS2C(string sid)
        {
            m_sid = sid;
            sw_autospin = false;
        }

        public void onLogin(System.Object _result)
        {
            try
            {
                string day = DateTime.Now.ToString("yyyy-MM-dd");
                string time = DateTime.Now.ToString("HH:mm:ssss");
                string str_json = JsonMapper.ToJson(_result);
                str_json = day + " " + time + "onLogin [" + str_json + "]";



                Debug.Log("onLogin str_json ");
            }
            catch (Exception EX)
            {
                Debug.Log("onLogin Exception " + EX);

            }
        }
        public void onGetMachineList(System.Object _result)
        {
            Debug.Log("onGetMachineList");
            NetConnectRTMP._net.Call("takeMachine", null, 0);
        }
        public void onTakeMachine(System.Object _result)
        {
            Debug.Log("onTakeMachine");
            NetConnectRTMP._net.Call("onLoadInfo2", null);
        }

        public void onOnLoadInfo2(System.Object _result)
        {
            Debug.Log("onOnLoadInfo2");
            NetConnectRTMP._net.Call("creditExchange", null, "1:100", "500000");
        }

        public void onCreditExchange(System.Object _result)
        {
            Debug.Log("onCreditExchange");
        }

        public void onBalanceExchange(System.Object _result)
        {
            Debug.Log("onBalanceExchange");
        }

        public void onBeginGame(System.Object _result)
        {
            Debug.Log("onbegingame");

            string str_json = JsonMapper.ToJson(_result);

            //Console.WriteLine(str_json);
            //Console.WriteLine("str_json is " + str_json);
            /*
            string str = "onBeginGame[";
            str += Parser.recurrence(_result);
            str += "]";

            MsgStore.Store(str);
            */

            string day = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ssss");
            JsonData jd = JsonMapper.ToObject(str_json);

            str_json = day + " " + time + " onBeginGame[" + str_json + "]";


            try
            {
                m_WagersID = (jd["data"]["WagersID"]).ToString();
                JsonData jd_fg = jd["data"]["FreeGame"];


                if (!jd_fg.IsArray)
                {
                    Console.WriteLine("hitFree ");
                    str_json = "hitFree " + str_json ;

                    NetConnectRTMP._net.Call("hitFree", null, m_sid, m_WagersID, 0);
                }


                NetConnectRTMP._net.Call("endGame", null, m_sid, m_WagersID);
                Debug.Log("endgame");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception " + ex);
            }


            //Console.WriteLine("str is " + str);
            /*
            ASObject asobj = _result as ASObject;
            ASObject asobj2 = asobj["data"] as ASObject;

            m_WagersID = asobj2["WagersID"].ToString();

            if(!(asobj2["FreeGame"] is Object[]))
            {
                NetConnectRTMP._net.Call("hitFree", null,m_sid, m_WagersID, "1");
                Console.WriteLine("FreeGame");
            }
            */
        }

        public void onEndGame(System.Object _result)
        {
            /*
            string str = "onEndGame[";
            str += Parser.recurrence(_result);
            str += "]";
            */
            Debug.Log("onendgame");
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ssss");
            string str_json = JsonMapper.ToJson(_result);
            str_json = day + " " + time + "onEndGame[" + str_json + "]";



            if (sw_autospin)
            {
                /*
                NetConnectRTMP._net.Call("beginGame4", null, m_sid, "1", "1");*/

                Dictionary<string, int> betinfo = new Dictionary<string, int>();
                betinfo.Add("LineNum", 1);
                betinfo.Add("LineBet", 1);

                NetConnectRTMP._net.Call("beginGame4", null, betinfo);
                Debug.Log("begingame");
            }
        }

        public void onHitFree(System.Object _result)
        {
            string str_json = JsonMapper.ToJson(_result);

            /*
            string str = "onHitFree[";
            str += Parser.recurrence(_result);
            str += "]";
            */

            Console.WriteLine("onHitFree " + str_json);
        }
        public void onHitBonus(System.Object _result)
        {
        }

        public void onEndBonus(System.Object _result)
        {
        }

        public void updateJP(System.Object _result)
        {
        }

        public void updateJPList(System.Object _result)
        {
        }


        public void updateMarquee(System.Object _result)
        {

        }
        public void onHitJackpot(System.Object _result)
        {

        }
    }
}
