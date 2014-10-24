using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeCabMorphologicalAnalyzer
{
    class ConsoleDebug
    {
        static void Main(string[] args)
        {
            // 形態素解析のためのインスタンス作成
            MeCabMorphologicalAnalyzer mcma = new MeCabMorphologicalAnalyzer();

            // 形態素解析する文字列の入力
            string str = Console.ReadLine();

            // 形態素解析の実行
            List<Morpheme> morphemes = mcma.Analyse(str);

            // 結果の表示
            foreach (Morpheme elem in morphemes)
            {
                Console.WriteLine(elem.ToString());
            }

            // Enterで終了
            Console.ReadKey();
        }
    }
}
