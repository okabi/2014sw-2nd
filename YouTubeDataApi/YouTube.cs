using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeDataApi
{
    /// <summary>YouTube動画1つのデータを保持するクラス。</summary>
    public class YouTube
    {
        /// <summary>動画ID</summary>
        public string VideoId { get; private set; }
        /// <summary>動画タイトル</summary>
        public string Title { get; private set; }
        /// <summary>動画説明</summary>
        public string Description { get; private set; }
        /// <summary>動画サムネイル</summary>
        public string ThumbnailUrl { get; private set; }
        /// <summary>投稿日</summary>
        public DateTime PublishedAt { get; private set; }

        /// <summary>
        /// 動画データを作成。
        /// </summary>
        /// <param name="videoId">動画ID</param>
        /// <param name="title">動画タイトル</param>
        /// <param name="description">動画説明</param>
        /// <param name="thumbnailUrl">動画サムネイル</param>
        /// <param name="publishedAt">投稿日</param>
        public YouTube(string videoId, string title, string description, string thumbnailUrl, DateTime publishedAt)
        {
            VideoId = videoId;
            Title = title;
            Description = description;
            ThumbnailUrl = thumbnailUrl;
            PublishedAt = publishedAt;
        }

        /// <summary>
        /// インスタンスが保持する動画情報を文字列形式で返す。
        /// </summary>
        /// <returns>インスタンスが保持する動画情報</returns>
        public override string ToString()
        {
            return "[videoId => " + VideoId + "\n title => " + Title + "\n description => "
                        + Description + "\n thumbnailUrl => " + ThumbnailUrl + "\n publishedAt => " + PublishedAt.ToString() + "]";
        }
    }
}
