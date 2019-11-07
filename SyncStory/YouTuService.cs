using Fas;
using Fas.Sql;
using Fas.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;

namespace SyncStory
{
    public class YouTuService
    {
        private Dictionary<string, string> cates = new Dictionary<string, string>() {
            {"18:奇幻玄幻","1:玄幻,2:奇幻"},
            {"19:武侠仙侠","3:武侠,4:仙侠"},
            {"20:历史军事","7:历史"},
            {"21:都市娱乐","5:都市,8:军事"},
            {"22:科幻世界","11:科幻"},
            {"23:悬疑灵异","17:推理悬疑"},
            {"25:古装言情","13:古代言情"},
            {"26:都市言情","12:现代言情"},
            {"27:浪漫青春","15:青春校园"},
            {"28:幻想言情","14:幻想言情"},
            {"34:游戏竞技","9:游戏,10:竞技"},
            {"35:其他小说","298:轻小说,116:其他,298:轻小说,116:其他"},
            {"38:灵异悬疑","84:恐怖惊悚,112:有兔怪谈"},
            {"39:同人衍生","16:同人作品"},
            {"40:耽美百合","128:耽美,18:次元专区"},
            {"42:热血校园","6:校园"},
        };
        private string baseUrl = "http://app.youzibank.com/";

        public void SyncBook()
        {
            foreach (var cate in cates)
            {
                (string id, string name) _s = cate.Key.ToKV(":");
                var _t = cate.Value.ToKVList(":", ",");

                foreach ((string id, string name) t in _t)
                {
                    try
                    {
                        insertBooks(_s.id, 1, t.id);
                    }
                    catch (Exception e)
                    {

                        break;
                    }
                }
            }
        }

        public void insertBooks(string category, int page, string clsId)
        {

            string result = HttpUtil.Get($"{baseUrl}book/list?wordFilter=0&fullFlag=0&clsIdSecond=0&pageNo={page}&orderBy=read_cnt&clsIdFirst={clsId}");
            JsonElement datas = JsonSerializer.Deserialize<JsonElement>(result).GetProperty("data");
            List<Hashtable> dataList = new List<Hashtable>();
            for (int i = 0; i < datas.GetArrayLength(); i++)
            {
                var data = datas[i];

                Hashtable ht = new Hashtable();
                ht["category"] = category;
                ht["title"] = data.GetProperty("name").ToString();
                ht["author"] = data.GetProperty("author").ToString();
                ht["pic"] = data.GetProperty("photoPath").ToString();
                ht["content"] = data.GetProperty("intro").ToString();
                ht["tag"] = data.GetProperty("clsName").ToString();
                ht["hits"] = data.GetProperty("readCnt").ToString();
                ht["rating"] = data.GetProperty("strScore").ToString();
                ht["rating_count"] = data.GetProperty("score").ToString();
                ht["create_time"] = DateTime.Now.ToTimeStamp() / 1000;
                ht["reurl"] = $"{baseUrl}book/info?bookId={data.GetProperty("id").ToString()}";
                dataList.Add(ht);
            }

            //var sql = DispatchProxy.Create<ISql, SqlProxy>();
            //sql.Insert();

            Sql sql = new Sql("novel");

            sql.Insert(dataList);

            //sqlProxy.insert(dbDatas, "sync");
        }

        public void GetBookInfo(string bookId)
        {
            string result = HttpUtil.Get($"{baseUrl}book/info?bookId={bookId}");

            object json = JsonSerializer.Deserialize<object>(result);
        }

        public void ListChapter(string bookId)
        {
            string result = HttpUtil.Get($"{baseUrl}book/chapter/listAll?bookId={bookId}");

            object json = JsonSerializer.Deserialize<object>(result);
        }

        public void GetBookTxt()
        {
            string url = "";
        }
    }
}
