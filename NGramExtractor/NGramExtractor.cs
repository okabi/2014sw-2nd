using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGramExtractor
{
    class NGramExtractor
    {
        static void Main(string[] args)
        {
            // 分割対象文字列の入力
            string inputStr = Console.ReadLine();
            
            // n-gramのn
            int gramNum = 2;
            
            // gramNum-gramのためのインスタンス
            NGramParser parser = new NGramParser(gramNum);

            // 文字列を分割
            string[] result = parser.Parse(inputStr);

            // 結果を出力
            Console.WriteLine(ArrayToString(result));

            // Enterで終了
            inputStr = Console.ReadLine();
        }

        /// <summary>
        /// 文字列の配列を、タスク指定形式の文字列に変換
        /// </summary>
        /// <param name="strs">文字列配列</param>
        /// <returns>タスク指定形式の文字列</returns>
        private static string ArrayToString(string[] strs)
        {
            string result = "[";
            foreach (string str in strs)
            {
                result += "\"" + str + "\", ";
            }
            result = result.Remove(result.Length - 2);
            result += "]";
            return result;
        }
    }
}
