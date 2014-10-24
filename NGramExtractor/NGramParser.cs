using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGramExtractor
{
    class NGramParser
    {
        // n-gramのnに相当する部分
        public int GramNum { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gramNum">n-gramのnに相当する部分</param>
        public NGramParser(int gramNum)
        {
            GramNum = gramNum;
        }
        
        /// <summary>
        /// 指定された文字列からn-gramを生成するメソッド
        /// </summary>
        /// <param name="str">n-gramにかける文字列</param>
        /// <returns>gramNumずつになった文字列の配列</returns>
        public string[] Parse(string str)
        {
            // 分割した語を格納するためのListを作成
            List<string> list = new List<string>();

            // i文字目の単語GramNum語をListに追加していく
            for (int i = 0; i < str.Length - GramNum + 1; i++)
            {
                // i文字目の単語GramNum語を取得
                string ithWord = str.Substring(i, GramNum);
                // i文字目の単語GramNum語をListに追加
                list.Add(ithWord);
            }

            // Listをstringの配列に変換して出力
            string[] result = list.ToArray();
            return result;
        }
    }
}
