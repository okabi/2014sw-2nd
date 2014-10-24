using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooMorphologicalAnalyzer
{
    class ConsoleDebug
    {
        static void Main(string[] args)
        {
            // 形態素解析対象文字列の入力
            string inputStr = Console.ReadLine();

            // 形態素解析のためのインスタンス
            MorphologicalAnalyzer analyser = new MorphologicalAnalyzer();

            // 形態素解析を行う
            List<Morpheme> morphemes = analyser.Analyse(inputStr);

            // 結果を出力
            foreach (Morpheme morpheme in morphemes)
            {
                Console.WriteLine(morpheme.ToString());
            }

            // Enterで終了
            Console.ReadKey();
        }
    }
}
