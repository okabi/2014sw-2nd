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
    public class MorphologicalAnalyzer
    {
        // YahooのアプリケーションID
        public string AppID { get; private set; }

        /// <summary>
        /// YahooのアプリケーションIDを受け取る
        /// </summary>
        public MorphologicalAnalyzer(string app_id)
        {
            AppID = app_id;
        }

        /// <summary>
        /// 指定された文字列の形態素解析を行う
        /// </summary>
        /// <param name="str">形態素解析を行う文字列</param>
        /// <returns>Morphemeクラス(形態素)のリスト</returns>
        public List<Morpheme> Analyse(string str)
        {
            // リクエストURL
            string url = "http://jlp.yahooapis.jp/MAService/V1/parse?appid=" + AppID + "&response=surface,reading,pos,baseform&sentence=" + str;

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
                string surface = surfaceNode.InnerText;
                // word要素内のpos要素を取得
                XmlNode posNode = word["pos"];
                string pos = posNode.InnerText;
                // word要素内のpos要素を取得
                XmlNode baseformNode = word["baseform"];
                string baseform = baseformNode.InnerText;
                // リストに情報を追加
                Morpheme morpheme = new Morpheme(surface, pos, baseform);
                result.Add(morpheme);
            }
            return result;
        }
    }
}
