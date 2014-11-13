using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;

namespace TwitterConnector
{
    /// <summary>Twitterに接続するためのクラス</summary>
    public class TwitterConnector
    {
        /// <summary>土台となるアプリケーションのキー</summary>
        private const string CONSUMER_KEY = "KK7lZEESw7VWMnmC7KG9PUNXa";
        /// <summary>土台となるアプリケーションのキー(ハッシュ値生成用)</summary>
        private const string CONSUMER_SECRET = "2o43r2QTwR7TfGhRDBnwif7N24wZ6QfX9W8IAHDWA8Kv87qBUX";
        /// <summary>リクエストトークン情報</summary>
        private Dictionary<string, string> requestTokenDictionary;
        /// <summary>このインスタンスで認証しているユーザの表示ID</summary>
        public string ScreenName { get; private set; }
        /// <summary>このインスタンスで認証しているユーザのAccessToken</summary>
        public string AccessToken { get; private set; }
        /// <summary>このインスタンスで認証しているユーザのAccessTokenSecret</summary>
        public string AccessTokenSecret { get; private set; }


        /// <summary>
        /// ユーザのscreen nameを保存する。
        /// </summary>
        /// <param name="screenName">ユーザのスクリーン名</param>
        public TwitterConnector(string screenName)
        {
            ScreenName = screenName;
        }


        /// <summary>
        /// リクエストトークン発行に必要なURLを返す
        /// </summary>
        /// <returns>リクエストトークン発行に必要なURL</returns>
        public string GetRequestUrl()
        {
            // リクエストトークン(アプリケーション認証)の発行
            string requestTokenString = TwitterAPI("https://api.twitter.com/oauth/request_token", "GET");
            requestTokenDictionary = ParameterSplit(requestTokenString);
            string url = "https://api.twitter.com/oauth/authorize?" + requestTokenString + "&screen_name=" + ScreenName;
            return url;
        }
        

        /// <summary>
        /// PINを受け取ってアクセストークンを発行する
        /// </summary>
        /// <param name="pin">PIN</param>
        public void GenerateAccessToken(string pin)
        {
            Dictionary<string, string> accessTokenParameter = new Dictionary<string, string>();
            accessTokenParameter.Add("oauth_verifier", pin);
            string accessTokenString = TwitterAPI("https://api.twitter.com/oauth/access_token", "POST", requestTokenDictionary["oauth_token"], requestTokenDictionary["oauth_token_secret"], accessTokenParameter);
            Dictionary<string, string> accessTokenDictionary = ParameterSplit(accessTokenString);
            AccessToken = accessTokenDictionary["oauth_token"];
            AccessTokenSecret = accessTokenDictionary["oauth_token_secret"];
            ScreenName = accessTokenDictionary["screen_name"];
        }


        /// <summary>
        /// TwitterAPIのURL(パラメータ付き)を渡し、APIを利用してツイートのリストを得る。
        /// </summary>
        /// <param name="url">利用するTwitterAPIのURL(パラメータ付き)</param>
        /// <returns>得られたツイートリスト</returns>
        private List<Tweet> GetTweetsFromAPI(string url)
        {
            string apiString = TwitterAPI(url, "GET", AccessToken, AccessTokenSecret);
            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(apiString);
            List<Tweet> result = new List<Tweet>();
            foreach (dynamic tweet in json)
            {
                dynamic tweetedUser = tweet.user;
                User user = new User((string)tweetedUser.name, (string)tweetedUser.screen_name);
                Tweet elem = new Tweet((long)tweet.id, (string)tweet.text, user);
                result.Add(elem);
            }
            return result;
        }


        /// <summary>
        /// TwitterSearchAPIのURL(パラメータ付き)を渡し、APIを利用してツイートのリストを得る。
        /// </summary>
        /// <param name="url">利用するTwitterSearchAPIのURL(パラメータ付き)</param>
        /// <returns>得られたツイートリスト</returns>
        private List<Tweet> GetTweetsFromSearchAPI(string url)
        {
            string apiString = TwitterAPI(url, "GET", AccessToken, AccessTokenSecret);
            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(apiString);
            List<Tweet> result = new List<Tweet>();
            foreach (dynamic tweet in json.statuses)
            {
                dynamic tweetedUser = tweet.user;
                User user = new User((string)tweetedUser.name, (string)tweetedUser.screen_name);
                Tweet elem = new Tweet((long)tweet.id, (string)tweet.text, user);
                result.Add(elem);
            }
            return result;
        }


        /// <summary>
        /// 検索語句からツイートを取得する。
        /// </summary>
        /// <param name="q">検索語句</param>
        /// <param name="count">取得するツイートの数(最大100)</param>
        /// <returns>取得したツイートのリスト</returns>
        public List<Tweet> GetSearchTweets(string q, int count = 15, long max_id = 0)
        {
            string encoded_q = HttpUtility.UrlEncode(q);
            encoded_q = encoded_q.Replace("+", "%20");

            string url = "https://api.twitter.com/1.1/search/tweets.json?lang=ja&q=" + encoded_q + "&count=" + count.ToString();
            if (max_id != 0)
            {
                url += "&max_id=" + max_id.ToString();
            }
            List<Tweet> result = GetTweetsFromSearchAPI(url);
            return result;
        }


        /// <summary>
        /// 認証ユーザへの最近のメンションを取得する。
        /// </summary>
        /// <param name="count">取得するメンションの数(最大200)</param>
        /// <returns>取得したメンションのリスト</returns>
        public List<Tweet> GetMentionsTimeline(int count = 20)
        {
            string url = "https://api.twitter.com/1.1/statuses/mentions_timeline.json?count=" + count.ToString();
            List<Tweet> result = GetTweetsFromAPI(url);
            return result;
        }


        /// <summary>
        /// 指定ユーザの最近のタイムラインを取得する。
        /// </summary>
        /// <param name="count">取得するツイートの数(最大200)</param>
        /// <returns>取得したツイートのリスト</returns>
        public List<Tweet> GetUserTimeline(int count = 20)
        {
            return GetUserTimeline(ScreenName, count);
        }


        /// <summary>
        /// 指定ユーザの最近のタイムラインを取得する。
        /// </summary>
        /// <param name="screen_name">ツイートを取得するユーザのScreenName</param>
        /// <param name="count">取得するツイートの数(最大200)</param>
        /// <returns>取得したツイートのリスト</returns>
        public List<Tweet> GetUserTimeline(string screen_name, int count = 20)
        {
            string url = "https://api.twitter.com/1.1/statuses/user_timeline.json?count=" + count.ToString() + "&screen_name=" + screen_name;
            List<Tweet> result = GetTweetsFromAPI(url);
            return result;
        }


        /// <summary>
        /// 認証ユーザの最近のホームタイムラインを取得する。
        /// </summary>
        /// <param name="count">取得するツイートの数(最大200)</param>
        /// <returns>取得したツイートのリスト</returns>
        public List<Tweet> GetHomeTimeline(int count = 20)
        {
            string url = "https://api.twitter.com/1.1/statuses/home_timeline.json?count=" + count.ToString();
            List<Tweet> result = GetTweetsFromAPI(url);
            return result;
        }

        
        /// <summary>
        /// 指定されたIDのツイートを取得する。
        /// </summary>
        /// <param name="id">ツイートID</param>
        /// <returns>ツイート</returns>
        public Tweet GetTweet(long id)
        {
            string url = "https://api.twitter.com/1.1/statuses/show.json?id=" + id.ToString();
            string apiString = TwitterAPI(url, "GET", AccessToken, AccessTokenSecret);
            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(apiString);
            dynamic tweetedUser = json.user;
            User user = new User((string)tweetedUser.name, (string)tweetedUser.screen_name);
            Tweet result = new Tweet((long)json.id, (string)json.text, user);
            return result;
        }

        
        /// <summary>
        /// 認証ユーザでTwitterにつぶやきをpostする。
        /// </summary>
        /// <param name="str">postする文字列</param>
        public void Update(string str)
        {
            string encoded_str = HttpUtility.UrlEncode(str);
            encoded_str = encoded_str.Replace("+", "%20");
            string url = "https://api.twitter.com/1.1/statuses/update.json?status=" + encoded_str;
            TwitterAPI(url, "POST", AccessToken, AccessTokenSecret);
        }


        /// <summary>
        /// 指定したURLに指定した方法でアクセスし、得られた文字列を返す。
        /// </summary>
        /// <param name="uri">アクセスするURL</param>
        /// <param name="method">アクセス方法("GET" または "POST")</param>
        /// <returns>得られた文字列</returns>
        private string GetStringFromWeb(Uri uri, string method)
        {
            HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(uri);
            webreq.Method = method;
            HttpWebResponse webres = (HttpWebResponse)webreq.GetResponse();
            Stream st = webres.GetResponseStream();
            StreamReader sr = new StreamReader(st, Encoding.GetEncoding(932));
            string result = sr.ReadToEnd();
            sr.Close();
            st.Close();
            return result;
        }


        /// <summary>
        /// key1=value1＆key2=value2 形式のパラメータ文字列を辞書に変換する。
        /// </summary>
        /// <param name="st">パラメータ文字列</param>
        /// <returns>辞書に変換したもの</returns>
        private Dictionary<string, string> ParameterSplit(string st)
        {
            char[] separator = { '&' };
            string[] arr = st.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string elem in arr)
            {
                string parameter_regex = @"(.*)=(.*)";
                string key = System.Text.RegularExpressions.Regex.Match(elem, parameter_regex).Groups[1].Value;
                string value = System.Text.RegularExpressions.Regex.Match(elem, parameter_regex).Groups[2].Value;
                result.Add(key, value);
            }
            return result;
        }

        
        /// <summary>
        /// TwitterAPIから情報を取得する。
        /// </summary>
        /// <param name="uri">アクセスするURL(+SignatureBase生成時に関与するパラメータ)</param>
        /// <param name="method">アクセス方法("GET" または "POST")</param>
        /// <param name="parameters">SignatureBase生成時に関与しないパラメータ</param>
        /// <returns>TwitterAPIから返される文字列</returns>
        private string TwitterAPI(string uri, string method, Dictionary<string, string> parameters = null)
        {
            return TwitterAPI(uri, method, null, null, parameters);
        }


        /// <summary>
        /// TwitterAPIに情報を送信したり、情報を取得したりする。
        /// </summary>
        /// <param name="uri">アクセスするURL(+SignatureBase生成時に関与するパラメータ)</param>
        /// <param name="method">アクセス方法("GET" または "POST")</param>
        /// <param name="token">Token</param>
        /// <param name="tokenSecret">Token Secret</param>
        /// <param name="parameters">SignatureBase生成時に関与しないパラメータ</param>
        /// <returns>TwitterAPIから返される文字列</returns>
        private string TwitterAPI(string uri, string method, string token, string tokenSecret, Dictionary<string, string> parameters = null)
        {
            // OAuth認証のライブラリを使用
            OAuth.OAuthBase oauth = new OAuth.OAuthBase();

            // トークン発行に必要なパラメータとか用意してsignature発行
            string normalizedUrl, normalizedRequestParameters;
            string signature = oauth.GenerateSignature(new Uri(uri), CONSUMER_KEY, CONSUMER_SECRET, token, tokenSecret, method, oauth.GenerateTimeStamp(), oauth.GenerateNonce(), out normalizedUrl, out normalizedRequestParameters);

            // APIにアクセスして文字列取得
            string tokenRq = normalizedUrl + "?" + normalizedRequestParameters + "&oauth_signature=" + HttpUtility.UrlEncode(signature);
            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                    tokenRq += "&" + parameter.Key + "=" + HttpUtility.UrlEncode(parameter.Value);
                }
            }
            return GetStringFromWeb(new Uri(tokenRq), method);
        }
    }
}
