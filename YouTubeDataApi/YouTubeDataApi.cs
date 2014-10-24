using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace YouTubeDataApi
{
    /// <summary>YouTube動画検索クラス。</summary>
    public class YouTubeDataApi
    {
        /// <summary>APIキー</summary>
        private string api_key;

        /// <summary>
        /// 動画検索のためにAPIキーを登録
        /// </summary>
        /// <param name="key">APIキー</param>
        public YouTubeDataApi(string key)
        {
            api_key = key;
        }

        /// <summary>
        /// 指定されたクエリで動画を検索する。
        /// </summary>
        /// <param name="query">検索クエリ</param>
        /// <param name="max_results">結果セットとして返されるアイテムの最大数(0以上50以下)</param>
        /// <returns>YouTubeクラスからなる検索結果のリスト</returns>
        public List<YouTube> Search(string query, int max_results = 20)
        {
            // リクエストURL
            string url = "https://www.googleapis.com/youtube/v3/search?part=id,snippet&type=video&key=" + api_key 
                            + "&q=" + query + "&maxResults=" + max_results.ToString();

            // リクエストの作成
            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();

            // リクエストを送信してレスポンスを受信、インスタンス化
            YouTubeJson info;
            using (res)
            {
                using (Stream st = res.GetResponseStream())
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(YouTubeJson));
                    info = (YouTubeJson)serializer.ReadObject(st);
                }
            }

            // 受け取った情報をリストにして返す
            List<YouTube> results = new List<YouTube>();
            foreach (YouTubeJsonItems item in info.items)
            {
                YouTube result = new YouTube(item.id.videoId,
                                             item.snippet.title,
                                             item.snippet.description,
                                             item.snippet.thumbnails.high.url,
                                             DateTime.Parse(item.snippet.publishedAt));
                results.Add(result);
            }
            return results;
        }
    }
}
