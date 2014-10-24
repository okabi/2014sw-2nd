using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;

namespace TwitterConnector
{
    class ConcoleDebug
    {
        static void Main(string[] args)
        {
            // 認証するユーザのScreenName
            string screenName;
            Console.WriteLine("ScreenName? >");
            screenName = Console.ReadLine();

            // 認証するユーザのScreenName
            TwitterConnector tc = new TwitterConnector(screenName);

            // 認証ユーザの最近のメンションを5件取得して表示
            foreach (Tweet elem in tc.GetMentionsTimeline(5))
            {
                Console.WriteLine(elem.ToString());
            }
            Console.WriteLine("");

            // 認証ユーザのホームタイムラインを10件取得して表示
            foreach (Tweet elem in tc.GetHomeTimeline(10))
            {
                Console.WriteLine(elem.ToString());
            }
            Console.WriteLine("");

            // 指定ユーザのつぶやきを20件取得して表示。引数のscreenNameは認証ユーザと等しいので、引数を与えなくても動作する
            foreach (Tweet elem in tc.GetUserTimeline(screenName))
            {
                Console.WriteLine(elem.ToString());
            }
            Console.WriteLine("");

            // 指定IDのつぶやきを表示
            Console.WriteLine(tc.GetTweet(522990443391225856).ToString());
            
            // つぶやく
            tc.Update("テストツイート #jikken4tweet");

            Console.ReadKey();
        }
    }
}
