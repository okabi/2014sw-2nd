using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFVectorGenerator
{
    class ConsoleDebug
    {
        static void Main(string[] args)
        {
            // 分割対象文字列の入力
            string inputStr = Console.ReadLine();

            // TFベクトル生成のためのインスタンス
            TFVectorGenerator generator = new TFVectorGenerator();

            // TFベクトルを生成
            Dictionary<string, int> result = generator.Generate(inputStr);

            // 結果を出力
            Console.WriteLine(DictionaryToString(result));

            // Enterで終了
            Console.ReadKey();
        }
        
        /// <summary>
        /// Dictionary(string, int)をタスク指定形式文字列に変換
        /// </summary>
        /// <param name="dic">変換対象の連想配列</param>
        /// <returns>渡された連想配列をタスク指定形式に変換した文字列</returns>
        private static string DictionaryToString(Dictionary<string, int> dic)
        {
            string result = "{";
            foreach (KeyValuePair<string, int> pair in dic)
            {
                result += "{\"" + pair.Key + "\", " + pair.Value + "}, ";
            }
            result = result.Remove(result.Length - 2);
            result += "}";
            return result;
        }
    }
}
