using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYRtmpProject
{
    public class Debug
    {

        static public void Log<T>(T str)
        {

            Console.WriteLine(str.ToString());
        }
    }

    class Program
    {

        static void Main(string[] args)
        {

            string sid = "d5857b12zm397j21z9cg2dgmzj6li2ez012";

            NetConnectRTMP connectRtmp = new NetConnectRTMP();

            connectRtmp.Initial(sid);

            while (true)
            {

                string str_command = Console.ReadLine();

                if (str_command == "Connect")
                {

                    connectRtmp.Connect();
                }
                else if (str_command == "Disconnect")
                {

                    connectRtmp.DisConnect();
                    break;
                }
                else if (str_command == "spin")
                {

                    connectRtmp.Spin();
                }
                else if (str_command == "autospin")
                {

                    connectRtmp.SetAutoSpin(true);
                }
                else if (str_command == "stop")
                {

                    connectRtmp.SetAutoSpin(false);
                }
                else if (str_command == "endgame")
                {
                    connectRtmp.endgame();
                }
            }
            Console.ReadLine();


        }
    }
}
