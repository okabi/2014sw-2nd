using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;

namespace YahooMorphologicalAnalyzer
{
    class MorphologicalAnalyzer
    {
        // YahooのアプリケーションID
        const string APP_ID = "dj0zaiZpPXV1MGMzOERiSGxZNCZzPWNvbnN1bWVyc2VjcmV0Jng9OTk-";

        /// <summary>
        /// なにもしないコンストラクタ
        /// </summary>
        public MorphologicalAnalyzer()
        {

        }

        /// <summary>
        /// 指定された文字列の形態素解析を行う
        /// </summary>
        /// <param name="str">形態素解析を行う文字列</param>
        /// <returns>Morphemeクラス(形態素)のリスト</returns>
        public List<Morpheme> Analyse(string str)
        {
            // リクエストURL
            string url = "http://jlp.yahooapis.jp/MAService/V1/parse?appid=" + APP_ID + "&sentence=" + str;

            // 空のXMLを作成
            XmlDocument xml = new XmlDocument();

            // HTTPによるリクエストの作成
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            // リクエストを送信してレスポンスを受信
            using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
            using (Stream st = res.GetResponseStream())
            {
                // XMLを読み込み、xmlにWebサービスから返されたXMLが入る。
                xml.Load(st);
            }

            // 戻り値となるリスト
            List<Morpheme> result = new List<Morpheme>();

            // XML中の「word」要素をすべて取得
            XmlNodeList words = xml.GetElementsByTagName("word");
            foreach (XmlNode word in words)
            {
                // word要素内のsurface要素を取得
                XmlNode surfaceNode = word["surface"];
                // surface要素内のテキストを取得
                string surface = surfaceNode.InnerText;
                // word要素内のpos要素を取得
                XmlNode posNode = word["pos"];
                // pos要素内のテキストを取得
                string pos = posNode.InnerText;
                // リストに情報を追加
                Morpheme morpheme = new Morpheme(surface, pos);
                result.Add(morpheme);
            }
            return result;
        }
    }
}
