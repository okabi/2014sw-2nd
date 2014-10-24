using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeDataApi
{
    class ConsoleDebug
    {
        static void Main(string[] args)
        {
            // 動画検索のためのインスタンス
            YouTubeDataApi searcher = new YouTubeDataApi("AIzaSyCIAdiZ2x5YJgEqq6VDtSO_rQQSvts0cHk");

            // 検索クエリの入力
            string inputStr = Console.ReadLine();

            // 検索結果の表示
            List<YouTube> results = searcher.Search(inputStr, 10);
            foreach (YouTube video in results)
            {
                Console.WriteLine(video.ToString());
            }

            // Enterで終了
            Console.ReadKey();
        }
    }
}
