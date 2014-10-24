using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;

namespace GoodsSearcher
{
    public class GoodsSearcher
    {
        /// <summary>楽天のアプリケーションID</summary>
        private const string APP_ID = "1012221618336335965";
        /// <summary>検索クエリummary>
        public string Query { get; private set; }

        /// <summary>
        /// クエリを設定するコンストラクタ。
        /// </summary>
        /// <param name="query">検索クエリ</param>
        public GoodsSearcher(string query)
        {
            Query = query;
        }

        /// <summary>
        /// クエリで実際に検索を行うメソッド
        /// </summary>
        /// <param name="num">取得する検索結果数</param>
        /// <returns>キーが商品名で値が価格である辞書</returns>
        public Dictionary<string, int> Search(int num)
        {
            // リクエストURL
            string url = "https://app.rakuten.co.jp/services/api/IchibaItem/Search/20140222?applicationId=" 
                            + APP_ID.ToString() + "&format=xml&keyword=" + Query + "&hits=" + num.ToString();

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
            Dictionary<string, int> result = new Dictionary<string,int>();

            // XML中の「Items」要素をすべて取得
            XmlNodeList items = xml.GetElementsByTagName("Item");
            foreach (XmlNode item in items)
            {
                // item要素内のitemName要素を取得
                XmlNode itemNameNode = item["itemName"];
                string itemName = itemNameNode.InnerText;
                // item要素内のitemPrice要素を取得
                XmlNode itemPriceNode = item["itemPrice"];
                int itemPrice = int.Parse(itemPriceNode.InnerText);
                // 辞書に情報を追加
                result[itemName] = itemPrice;
            }
            return result;
        }
    }
}
