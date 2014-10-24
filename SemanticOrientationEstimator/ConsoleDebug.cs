using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticOrientationEstimator
{
    class ConsoleDebug
    {
        static void Main(string[] args)
        {
            // 極性判定のためのインスタンス
            SemanticOrientationEstimator estimator = new SemanticOrientationEstimator("../../dic/pn_ja.dic", "dj0zaiZpPXV1MGMzOERiSGxZNCZzPWNvbnN1bWVyc2VjcmV0Jng9OTk-");

            // 分析対象文字列の入力
            string inputStr = Console.ReadLine();

            // 極性判定結果の表示
            Console.WriteLine(estimator.Estimate(inputStr).ToString());

            // Enterで終了
            Console.ReadKey();

        }
    }
}
