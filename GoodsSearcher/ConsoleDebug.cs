using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsSearcher
{
    class ConsoleDebug
    {
        static void Main(string[] args)
        {
            // 検索対象文字列の入力
            string inputStr = Console.ReadLine();

            // 検索のためのインスタンス
            GoodsSearcher searcher = new GoodsSearcher(inputStr);

            // 検索件数
            int num = 20;

            // 検索する
            Dictionary<string, int> results = searcher.Search(num);

            // 結果を出力
            foreach(KeyValuePair<string, int> pair in results){
                Console.WriteLine("商品名 => " + pair.Key + ", 価格 => " + pair.Value);
            }

            // Enterで終了
            Console.ReadKey();
        }
    }
}
