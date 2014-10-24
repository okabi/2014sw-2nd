using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFVectorGenerator
{
    class TFVectorGenerator
    {
        /// <summary>
        /// 何もしないコンストラクタ
        /// </summary>
        public TFVectorGenerator()
        {

        }

        /// <summary>
        /// 指定された文字列のTFベクトルを生成するメソッド
        /// </summary>
        /// <param name="str">TFベクトルにする文字列</param>
        /// <returns>キーが単語で値が出現頻度の辞書</returns>
        public Dictionary<string, int> Generate(string str)
        {
            // 文字列をすべて小文字にする
            str = str.ToLower();

            // 戻り値を格納するインスタンス
            Dictionary<string, int> result = new Dictionary<string, int>();

            // 区切り文字指定
            char[] delimiterChars = { ' ', ',' };

            // 区切り文字で文字列を区切る
            string[] words = str.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            // 出現頻度を計算して返す
            foreach (string word in words)
            {
                if (result.ContainsKey(word) == true)
                {
                    result[word] += 1;
                }
                else
                {
                    result[word] = 1;
                }
            }
            return result;
        }
    }
}
