using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextSimilarity
{
    class ConsoleDebug
    {
        static void Main(string[] args)
        {
            // 解析する文字列を入力
            Console.WriteLine("文字列その１:");
            string text1 = Console.ReadLine();
            Console.WriteLine("文字列その２:");
            string text2 = Console.ReadLine();

            // 文字列を解析して結果を返す
            TextSimilarity analyzer = new TextSimilarity(text1, text2);
            Console.WriteLine("Jaccard => " + analyzer.Jaccard().ToString());
            Console.WriteLine("Cosine  => " + analyzer.Cosine().ToString());

            // Enterで終了
            Console.ReadKey();
        }
    }
}
