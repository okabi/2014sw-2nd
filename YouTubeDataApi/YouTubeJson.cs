using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace YouTubeDataApi
{
    /// <summary>APIから返されるJSONを読み込むためのクラス。</summary>
    [DataContract]
    class YouTubeJson
    {
        [DataMember]
        public string kind;
        [DataMember]
        public string etag;
        [DataMember]
        public string nextPageToken;
        [DataMember]
        public YouTubeJsonPageInfo pageInfo;
        [DataMember]
        public YouTubeJsonItems[] items;
    }

    /// <summary>APIから返されるJSONを読み込むためのクラス。ページデータ。</summary>
    [DataContract]
    class YouTubeJsonPageInfo
    {
        [DataMember]
        public int totalResults;
        [DataMember]
        public int resultsPerPage;
    }

    /// <summary>APIから返されるJSONを読み込むためのクラス。個々の動画データ。</summary>
    [DataContract]
    class YouTubeJsonItems
    {
        [DataMember]
        public string kind;
        [DataMember]
        public string etag;
        [DataMember]
        public YouTubeJsonItemsId id;
        [DataMember]
        public YouTubeJsonItemsSnippet snippet;
    }

    /// <summary>APIから返されるJSONを読み込むためのクラス。個々の動画データのID。</summary>
    [DataContract]
    class YouTubeJsonItemsId
    {
        [DataMember]
        public string kind;
        [DataMember]
        public string videoId;
    }

    /// <summary>APIから返されるJSONを読み込むためのクラス。個々の動画データのsnippet要素。</summary>
    [DataContract]
    class YouTubeJsonItemsSnippet
    {
        [DataMember]
        public string publishedAt;
        [DataMember]
        public string channelId;
        [DataMember]
        public string title;
        [DataMember]
        public string description;
        [DataMember]
        public YouTubeJsonItemsThumbnails thumbnails;
        [DataMember]
        public string channelTitle;
        [DataMember]
        public string liveBroadcastContent;
    }

    /// <summary>APIから返されるJSONを読み込むためのクラス。個々の動画データのsnippetのサムネイル。</summary>
    [DataContract]
    class YouTubeJsonItemsThumbnails
    {
        [DataMember]
        public YouTubeJsonItemsThumbnailsInfo medium;
        [DataMember]
        public YouTubeJsonItemsThumbnailsInfo high;
    }

    /// <summary>APIから返されるJSONを読み込むためのクラス。個々の動画データのsnippetのサムネイルの情報。</summary>
    [DataContract]
    class YouTubeJsonItemsThumbnailsInfo
    {
        [DataMember]
        public string url;
    }

}
