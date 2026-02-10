//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Common;
//using System.Data.SqlClient;
//using System.Threading.Tasks;
//using System.Text.Json;
//using System.Threading;
//using System.Linq;
//using VideoAssetManager.Models;

//namespace VideoAssetManager.Business
//{
//    public partial class DataAccessLearning : DataAccess
//    {



//        public DataSet GetContentDetails(string logOnCode)
//        {

//            List<DbParameter> objGenericList = new List<DbParameter>();

//            DbParameter objParam = NewParameter();
//            objParam.ParameterName = "@UserId";
//            objParam.DbType = DbType.String;
//            objParam.Size = logOnCode.Length;
//            objParam.Value = logOnCode;
//            objGenericList.Add(objParam);
//            return ExecuteQuery("procedurename", objGenericList.ToArray());
//        }
//        public DataSet GetTabMenuDetails(string logOnCode, string ControllerName, string ActionName)
//        {
//            List<DbParameter> objGenericList = new List<DbParameter>();
//            DbParameter objParam = NewParameter();
//            objParam.ParameterName = "@ControllerName";
//            objParam.DbType = DbType.String;
//            objParam.Size = logOnCode.Length;
//            objParam.Value = ControllerName;
//            objParam.ParameterName = "@ActionName";
//            objParam.DbType = DbType.String;
//            objParam.Size = logOnCode.Length;
//            objParam.Value = ActionName;
//            objGenericList.Add(objParam);
//            return ExecuteQuery(logOnCode, objGenericList.ToArray());
//        }
//        public void DeleteRoleAndPermission(string RoleId)
//        {

//            List<DbParameter> objGenericList = new List<DbParameter>();
//            DbParameter objParam = NewParameter();
//            objParam.ParameterName = "@RoleId";
//            objParam.DbType = DbType.String;
//            objParam.Size = RoleId.Length;
//            objParam.Value = RoleId;
//            objGenericList.Add(objParam);
//            ExecuteAction("SP_DeleteRoleAndPermission", objGenericList.ToArray());
//        }


//        public List<GetContentMaster> GetContentMaster(int pageindex, int pageSize, string SearchText, string SearchType, string RoleName, int? VideoFormat = null)
//        {
//            List<GetContentMaster> mod = new List<GetContentMaster>();
//            List<DbParameter> parameterList = new List<DbParameter>();

//            if (pageindex != 0)
//            {
//                pageindex -= 1;
//            }

//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@PageNumber";
//            obj1.DbType = DbType.String;
//            obj1.Value = pageindex;
//            parameterList.Add(obj1);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@PageSize";
//            obj2.DbType = DbType.String;
//            obj2.Value = pageSize;
//            parameterList.Add(obj2);

//            DbParameter obj3 = NewParameter();
//            obj3.ParameterName = "@SearchText";
//            obj3.DbType = DbType.String;
//            obj3.Value = SearchText;
//            parameterList.Add(obj3);

//            DbParameter obj4 = NewParameter();
//            obj4.ParameterName = "@SearchType";
//            obj4.DbType = DbType.String;
//            obj4.Value = SearchType;
//            parameterList.Add(obj4);

//            DbParameter obj5 = NewParameter();
//            obj5.ParameterName = "@RoleName";
//            obj5.DbType = DbType.String;
//            obj5.Value = RoleName;
//            parameterList.Add(obj5);

//            DbParameter videoFormatParam = NewParameter();
//            videoFormatParam.ParameterName = "@VideoFormat";
//            videoFormatParam.DbType = DbType.Int32;
//            videoFormatParam.Value = VideoFormat.HasValue ? (object)VideoFormat.Value : DBNull.Value; // Send DBNull if null
//            parameterList.Add(videoFormatParam);

//            DataSet ds = ExecuteQuery("SP_GetContentMaster", parameterList.ToArray());
//            foreach (DataRow row in ds.Tables[0].Rows)
//            {
//                mod.Add(new GetContentMaster
//                {
//                    ContentId = HandleDBNull<Int32>(row, "ContentId"),
//                    ContentGUID = HandleDBNull<Guid>(row, "ContentGUID"),
//                    ContentName = HandleDBNull<string>(row, "ContentName"),
//                    Title_HI = HandleDBNull<string>(row, "Title_HI"),
//                    Title_UR = HandleDBNull<string>(row, "Title_UR"),
//                    Titles = HandleDBNull<string>(row, "Titles"),
//                    Description = HandleDBNull<string>(row, "Description"),
//                    Description_HI = HandleDBNull<string>(row, "Description_HI"),
//                    Description_UR = HandleDBNull<string>(row, "Description_UR"),
//                    HostName = HandleDBNull<string>(row, "HostName"),
//                    VideoFormat = HandleDBNull<Int32>(row, "VideoFormat"),
//                    Thumbnail = HandleDBNull<string>(row, "Thumbnail"),
//                    Recordeddate = HandleDBNull<DateTime>(row, "Recordeddate"),
//                    SourceName = HandleDBNull<string>(row, "SourceName"),
//                    VideoStatus = HandleDBNull<Int32>(row, "VideoStatus"),
//                    CreatedOn = HandleDBNull<DateTime>(row, "CreatedOn"),
//                    ModifyOn = HandleDBNull<DateTime>(row, "ModifyOn"),
//                    Active = HandleDBNull<Boolean>(row, "Active"),
//                    VideoThumbnail = HandleDBNull<string>(row, "VideoThumbnail"),
//                    VideoFileNamne = HandleDBNull<string>(row, "VideoFileNamne"),
//                    VideoThumbnailImage = HandleDBNull<string>(row, "VideoThumbnailImage"),
//                    VideoFile = HandleDBNull<string>(row, "VideoFile"),
//                    Occasion = HandleDBNull<string>(row, "Occasion"),
//                    EntityName = HandleDBNull<string>(row, "EntityName"),
//                    Artist = HandleDBNull<string>(row, "Artist"),
//                    Duration = HandleDBNull<Int32>(row, "Duration"),
//                    TolalRec = HandleDBNull<Int32>(row, "TolalRec"),
//                    youtubeId = HandleDBNull<string>(row, "youtubeId"),
//                    TagName = HandleDBNull<string>(row, "TagName"),
//                    VOD = HandleDBNull<Int32>(row, "VOD"),
//                    CreatedName = HandleDBNull<string>(row, "CreatedName"),
//                    ModifyName = HandleDBNull<string>(row, "ModifyName")
//                });

//            }
//            return mod;
//        }



//        public List<HomeScreenData> GetHomeContent(Guid userId, string host, int lang)
//        {
//            var mod = new List<HomeScreenData>();
//            var mod1 = new List<VideoDetails>();
//            var mod2 = new List<RM_HomeTags>();

//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@lang";
//            obj1.DbType = DbType.Int32;
//            obj1.Value = lang;
//            parameterList.Add(obj1);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@host";
//            obj2.DbType = DbType.String;
//            obj2.Value = host;
//            parameterList.Add(obj2);

//            DbParameter obj3 = NewParameter();
//            obj3.ParameterName = "@UserId";
//            obj3.DbType = DbType.Guid;
//            obj3.Value = userId;
//            parameterList.Add(obj3);

//            DataSet ds = ExecuteQuery("RM_GetHomeContent", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new HomeScreenData
//                    {
//                        CollectionId = HandleDBNull<Guid>(row, "CollectionId"),
//                        CollectionName = HandleDBNull<string>(row, "CollectionName"),
//                        Host = HandleDBNull<string>(row, "Host"),
//                        Type = HandleDBNull<string>(row, "CategoryName"),
//                        Index = HandleDBNull<Int32>(row, "CollectionIndex"),
//                    });
//                }
//            }
//            int temp = 0;
//            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[1].Rows)
//                {
//                    mod1.Add(new VideoDetails
//                    {
//                        CollectionId = HandleDBNull<Guid>(row, "CollectionId"),
//                        CollectionName = HandleDBNull<string>(row, "CollectionName"),
//                        ContentId = HandleDBNull<Guid>(row, "ContentId"),
//                        Title = HandleDBNull<string>(row, "Title"),
//                        Description = HandleDBNull<string>(row, "Description"),
//                        VideoId = HandleDBNull<Guid>(row, "VideoId"),
//                        Seo_Slug = HandleDBNull<string>(row, "VideoSlug"),
//                        VideoThumbnail = HandleDBNull<string>(row, "VideoThumbnail"),
//                        Duration = HandleDBNull<Int32>(row, "Duration"),
//                        RecordedDate = HandleDBNull<DateTime>(row, "Recordeddate"),
//                        PublishDate = HandleDBNull<DateTime>(row, "PublishDate"),
//                        seq = HandleDBNull<Int32>(row, "seq"),
//                        Source = HandleDBNull<string>(row, "SourceName"),
//                        StreamingId = HandleDBNull<string>(row, "youtubeId"),
//                        StreamingServer = HandleDBNull<string>(row, "StreamingServer"),
//                        IsSaved = HandleDBNull<Boolean>(row, "IsSaved"),
//                        IsLike = HandleDBNull<Boolean>(row, "Islike"),
//                        LikeCount = HandleDBNull<Int32>(row, "likeCount"),
//                        SavedCount = HandleDBNull<Int32>(row, "SavedCount"),
//                        PoetName = HandleDBNull<string>(row, "EntityName")?.Trim(' ').TrimEnd(','),
//                        ArtistName = HandleDBNull<string>(row, "Artist")?.Trim(' ').TrimEnd(','),
//                        TimeSpent = HandleDBNull<Int32>(row, "TimeSpent"),
//                        ShareUrl = "http://rekhta.org/watch/" + HandleDBNull<string>(row, "VideoSlug")
//                    }); 
//                }
//            }
//            if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[2].Rows)
//                {
//                    mod2.Add(new RM_HomeTags
//                    {
//                        TopicId = HandleDBNull<Guid>(row, "TopicId"),
//                        ContentId = HandleDBNull<Guid>(row, "ContentId"),
//                        TopicName = HandleDBNull<string>(row, "TopicName"),
//                        HostName = HandleDBNull<string>(row, "HostName"),
//                        Description = HandleDBNull<string>(row, "Description")

//                    });
//                }
//            }

//            foreach (var coll in mod)
//            {
//                if (coll.Type == "ET")
//                {
//                    coll.Explore_Tags = mod1.Where(x => x.CollectionId == coll.CollectionId).Select(t => new Explore_Tags { Id = t.VideoId, Title = t.Title, Description = t.Description, seq = t.seq }).OrderBy(x1 => x1.seq).ToList();
//                }
//                else
//                {
//                    coll.VideoDetails = mod1.Where(x => x.CollectionId == coll.CollectionId).OrderBy(x => x.seq).ToList();
//                }
//            }

//            if (mod != null && mod.Count() > 0)
//            {
//                if (mod.FirstOrDefault(x => x.Type == "AD") != null)
//                    mod.FirstOrDefault(x => x.Type == "AD").AdDetails = GetAdds();
//            }

//            return mod;
//        }


//        public List<AdDetails> GetAdds()
//        {
//            List<AdDetails> mod = new List<AdDetails>();

//            List<DbParameter> parameterList = new List<DbParameter>();
//            DataSet ds = ExecuteQuery("WP_GetContentAds", parameterList.ToArray());
//            foreach (DataRow row in ds.Tables[0].Rows)
//            {
//                mod.Add(new AdDetails
//                {
//                    Id = HandleDBNull<Guid>(row, "Id"),
//                    Title = HandleDBNull<string>(row, "Title"),
//                    Thumbnail = HandleDBNull<string>(row, "Thumbnail"),
//                    CTAUrl = HandleDBNull<string>(row, "CTAUrl")
//                });
//            }
//            return mod;
//        }
//        public HomeScreenVideo GetHomeVideoDetails(string VideoId, Guid CollectionId, Guid UserId, int lang, int pageIndex)
//        {

//            pageIndex = ((pageIndex > 0) ? (pageIndex - 1) : 0);

//            var mod = new HomeScreenVideo();
//            mod.VideoDetails = new List<VideoDetails>();
//            var mod2 = new List<RM_HomeTags>();



//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@VideoId";
//            obj1.DbType = DbType.String;
//            obj1.Value = VideoId;
//            parameterList.Add(obj1);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@CollectionId";
//            obj2.DbType = DbType.Guid;
//            obj2.Value = CollectionId;
//            parameterList.Add(obj2);

//            DbParameter obj3 = NewParameter();
//            obj3.ParameterName = "@UserId";
//            obj3.DbType = DbType.Guid;
//            obj3.Value = UserId;
//            parameterList.Add(obj3);

//            DbParameter obj4 = NewParameter();
//            obj4.ParameterName = "@lang";
//            obj4.DbType = DbType.Int32;
//            obj4.Value = lang;
//            parameterList.Add(obj4);

//            DbParameter obj5 = NewParameter();
//            obj5.ParameterName = "@PageNumber";
//            obj5.DbType = DbType.Int32;
//            obj5.Value = pageIndex;
//            parameterList.Add(obj5);

//            DbParameter obj6 = NewParameter();
//            obj6.ParameterName = "@PageSize";
//            obj6.DbType = DbType.Int32;
//            obj6.Value = 50;
//            parameterList.Add(obj6);

//            DataSet ds = ExecuteQuery("RM_GetVideoDetails", parameterList.ToArray());

//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.VideoDetail = new VideoDetails();
//                    mod.VideoDetail.CollectionId = HandleDBNull<Guid>(row, "CollectionId");
//                    mod.VideoDetail.CollectionName = HandleDBNull<string>(row, "CollectionName");
//                    mod.VideoDetail.ContentId = HandleDBNull<Guid>(row, "Id");
//                    mod.VideoDetail.Title = HandleDBNull<string>(row, "Title");
//                    mod.VideoDetail.Description = HandleDBNull<string>(row, "Description");
//                    mod.VideoDetail.VideoId = HandleDBNull<Guid>(row, "VideoId");
//                    mod.VideoDetail.Seo_Slug = HandleDBNull<string>(row, "Seo_Slug");
//                    mod.VideoDetail.VideoThumbnail = HandleDBNull<string>(row, "VideoThumbnail");
//                    mod.VideoDetail.Duration = HandleDBNull<Int32>(row, "Duration");
//                    mod.VideoDetail.RecordedDate = HandleDBNull<DateTime>(row, "Recordeddate");
//                    mod.VideoDetail.PublishDate = HandleDBNull<DateTime>(row, "PublishDate");
//                    mod.VideoDetail.seq = HandleDBNull<Int32>(row, "seq");
//                    mod.VideoDetail.Source = HandleDBNull<string>(row, "SourceName");
//                    mod.VideoDetail.IsStarted = HandleDBNull<Boolean>(row, "IsStarted");
//                    mod.VideoDetail.TimeSpent = HandleDBNull<Int32>(row, "TimeSpent");
//                    mod.VideoDetail.IsSaved = HandleDBNull<Boolean>(row, "IsSave");
//                    mod.VideoDetail.IsLike = HandleDBNull<Boolean>(row, "IsLike");
//                    mod.VideoDetail.LikeCount = HandleDBNull<Int32>(row, "LikeCount");
//                    mod.VideoDetail.StreamingId = HandleDBNull<string>(row, "youtubeId");
//                    mod.VideoDetail.StreamingServer = HandleDBNull<string>(row, "StreamingServer");
//                    mod.VideoDetail.PoetName = HandleDBNull<string>(row, "EntityName");
//                    mod.VideoDetail.ArtistName = HandleDBNull<string>(row, "Artist");
//                    mod.VideoDetail.ShareUrl = "http://rekhta.org/watch/" + mod.VideoDetail.Seo_Slug;
//                   // dynamic res = GetDeepLinkingById(mod.VideoDetail.VideoId.ToString());

//                    //mod.VideoDetail.ShareUrl =Convert.ToString( res.ShortLink);
//                }
//            }
//            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[1].Rows)
//                {
//                    mod.VideoDetails.Add(new VideoDetails
//                    {
//                        CollectionId = HandleDBNull<Guid>(row, "CollectionId"),
//                        CollectionName = HandleDBNull<string>(row, "CollectionName"),
//                        ContentId = HandleDBNull<Guid>(row, "Id"),
//                        Title = HandleDBNull<string>(row, "Title"),
//                        Description = HandleDBNull<string>(row, "Description"),
//                        VideoId = HandleDBNull<Guid>(row, "VideoId"),
//                        Seo_Slug = HandleDBNull<string>(row, "Seo_Slug"),
//                    VideoThumbnail = HandleDBNull<string>(row, "VideoThumbnail"),
//                        Duration = HandleDBNull<Int32>(row, "Duration"),
//                        RecordedDate = HandleDBNull<DateTime>(row, "Recordeddate"),
//                        PublishDate = HandleDBNull<DateTime>(row, "PublishDate"),
//                        seq = HandleDBNull<Int32>(row, "seq"),
//                        Source = HandleDBNull<string>(row, "SourceName"),
//                        IsStarted = HandleDBNull<Boolean>(row, "IsStarted"),
//                        TimeSpent = HandleDBNull<Int32>(row, "TimeSpent"),
//                        IsSaved = HandleDBNull<Boolean>(row, "IsSave"),
//                        IsLike = HandleDBNull<Boolean>(row, "IsLike"),
//                        LikeCount = HandleDBNull<Int32>(row, "LikeCount"),
//                        StreamingId = HandleDBNull<string>(row, "youtubeId"),
//                        StreamingServer = HandleDBNull<string>(row, "StreamingServer"),
//                        PoetName = HandleDBNull<string>(row, "EntityName"),
//                        ArtistName = HandleDBNull<string>(row, "Artist"),
//                        ShareUrl = "http://rekhta.org/watch/" + HandleDBNull<string>(row, "Seo_Slug")
//                    });
//                }
//            }

//            if (mod.VideoDetail != null && !string.IsNullOrEmpty(mod.VideoDetail.PoetName))
//            {
//                if (string.IsNullOrEmpty(mod.VideoDetail.ArtistName))
//                {
//                    mod.VideoDetail.ArtistName = mod.VideoDetail.PoetName;
//                }
//                else
//                {
//                    var art = "";
//                    if (mod.VideoDetail.ArtistName != mod.VideoDetail.PoetName)
//                    {
//                        art=mod.VideoDetail.ArtistName + "," + mod.VideoDetail.PoetName;
//                    }
//                    else
//                    {
//                        art = mod.VideoDetail.ArtistName;
//                    }
                    
//                    string[] substrings = art.Split(',');
//                    // Step 2: Use HashSet to store unique substrings
//                    HashSet<string> uniqueSubstrings = new HashSet<string>(substrings);
//                    // Step 3: Convert HashSet back to array if needed
//                    string[] uniqueSubstringArray = uniqueSubstrings.ToArray();
//                    // Step 4: Join unique substrings into a comma-separated string
//                    mod.VideoDetail.ArtistName = string.Join(",", uniqueSubstringArray);
//                }
//            }


//            foreach (var video in mod.VideoDetails)
//            {
//                if (!string.IsNullOrEmpty(video.PoetName))
//                {
//                    if (string.IsNullOrEmpty(video.ArtistName))
//                    {
//                        video.ArtistName = video.PoetName;
//                    }
//                    else
//                    {
//                        var art = "";
//                        if (mod.VideoDetail.ArtistName != mod.VideoDetail.PoetName)
//                        {
//                            art = mod.VideoDetail.ArtistName + "," + mod.VideoDetail.PoetName;
//                        }
//                        else
//                        {
//                            art = mod.VideoDetail.ArtistName;
//                        }
//                        string[] substrings = art.Split(',');
//                        // Step 2: Use HashSet to store unique substrings
//                        HashSet<string> uniqueSubstrings = new HashSet<string>(substrings);
//                        // Step 3: Convert HashSet back to array if needed
//                        string[] uniqueSubstringArray = uniqueSubstrings.ToArray();
//                        // Step 4: Join unique substrings into a comma-separated string
//                        video.ArtistName = string.Join(",", uniqueSubstringArray);
//                    }
//                }
//            }

//            return mod;
//        }


//        public HomeScreenVideo GetShortsDetails(Guid VideoId, Guid UserId, int lang, int pageIndex)
//        {

//            pageIndex = ((pageIndex > 0) ? (pageIndex - 1) : 0);

//            var mod = new HomeScreenVideo();
//            mod.VideoDetails = new List<VideoDetails>();
//            var mod2 = new List<RM_HomeTags>();



//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@VideoId";
//            obj1.DbType = DbType.Guid;
//            obj1.Value = VideoId;
//            parameterList.Add(obj1);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@UserId";
//            obj2.DbType = DbType.Guid;
//            obj2.Value = UserId;
//            parameterList.Add(obj2);

//            DbParameter obj3 = NewParameter();
//            obj3.ParameterName = "@lang";
//            obj3.DbType = DbType.Int32;
//            obj3.Value = lang;
//            parameterList.Add(obj3);

//            DbParameter obj4 = NewParameter();
//            obj4.ParameterName = "@PageNumber";
//            obj4.DbType = DbType.Int32;
//            obj4.Value = pageIndex;
//            parameterList.Add(obj4);

//            DbParameter obj6 = NewParameter();
//            obj6.ParameterName = "@PageSize";
//            obj6.DbType = DbType.Int32;
//            obj6.Value = 50;
//            parameterList.Add(obj6);

//            DataSet ds = ExecuteQuery("RM_GetShortsDetails", parameterList.ToArray());
//            int temp = 0;
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.VideoDetails.Add(new VideoDetails
//                    {
//                   ContentId = HandleDBNull<Guid>(row, "Id"),
//                   Title = HandleDBNull<string>(row, "Title"),
//                   Description = HandleDBNull<string>(row, "Description"),
//                   VideoId = HandleDBNull<Guid>(row, "VideoId"),
//                   Seo_Slug = HandleDBNull<string>(row, "Seo_Slug"),
//                    VideoThumbnail = HandleDBNull<string>(row, "VideoThumbnail"),
//                   Duration = HandleDBNull<Int32>(row, "Duration"),
//                   RecordedDate = HandleDBNull<DateTime>(row, "Recordeddate"),
//                   PublishDate = HandleDBNull<DateTime>(row, "PublishDate"),
//                   Source = HandleDBNull<string>(row, "SourceName"),
//                   IsStarted = HandleDBNull<Boolean>(row, "IsStarted"),
//                   TimeSpent = HandleDBNull<Int32>(row, "TimeSpent"),
//                   IsSaved = HandleDBNull<Boolean>(row, "IsSave"),
//                   IsLike = HandleDBNull<Boolean>(row, "IsLike"),
//                   LikeCount = HandleDBNull<Int32>(row, "LikeCount"),
//                   StreamingId = HandleDBNull<string>(row, "youtubeId"),
//                   StreamingServer = HandleDBNull<string>(row, "StreamingServer"),
//                   PoetName = HandleDBNull<string>(row, "EntityName"),
//                   ArtistName = HandleDBNull<string>(row, "Artist"),
//                   seq=temp,
//                });
//                }

//            }
//            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[1].Rows)
//                {
//                    temp++;
//                    mod.VideoDetails.Add(new VideoDetails
//                    {
//                        ContentId = HandleDBNull<Guid>(row, "Id"),
//                        Title = HandleDBNull<string>(row, "Title"),
//                        Description = HandleDBNull<string>(row, "Description"),
//                        VideoId = HandleDBNull<Guid>(row, "VideoId"),
//                        Seo_Slug = HandleDBNull<string>(row, "Seo_Slug"),
//                        VideoThumbnail = HandleDBNull<string>(row, "VideoThumbnail"),
//                        Duration = HandleDBNull<Int32>(row, "Duration"),
//                        RecordedDate = HandleDBNull<DateTime>(row, "Recordeddate"),
//                        PublishDate = HandleDBNull<DateTime>(row, "PublishDate"),
//                        Source = HandleDBNull<string>(row, "SourceName"),
//                        IsStarted = HandleDBNull<Boolean>(row, "IsStarted"),
//                        TimeSpent = HandleDBNull<Int32>(row, "TimeSpent"),
//                        IsSaved = HandleDBNull<Boolean>(row, "IsSave"),
//                        IsLike = HandleDBNull<Boolean>(row, "IsLike"),
//                        LikeCount = HandleDBNull<Int32>(row, "LikeCount"),
//                        StreamingId = HandleDBNull<string>(row, "youtubeId"),
//                        StreamingServer = HandleDBNull<string>(row, "StreamingServer"),
//                        PoetName = HandleDBNull<string>(row, "EntityName"),
//                        ArtistName = HandleDBNull<string>(row, "Artist"),
//                        seq = temp,
//                    });
//                }
//            }




//            foreach (var video in mod.VideoDetails)
//            {
//                if (!string.IsNullOrEmpty(video.PoetName))
//                {
//                    if (string.IsNullOrEmpty(video.ArtistName))
//                    {
//                        video.ArtistName = video.PoetName;
//                    }
//                    else
//                    {
//                        var art = video.ArtistName + "," + video.PoetName;
//                        string[] substrings = art.Split(',');
//                        // Step 2: Use HashSet to store unique substrings
//                        HashSet<string> uniqueSubstrings = new HashSet<string>(substrings);
//                        // Step 3: Convert HashSet back to array if needed
//                        string[] uniqueSubstringArray = uniqueSubstrings.ToArray();
//                        // Step 4: Join unique substrings into a comma-separated string
//                        video.ArtistName = string.Join(",", uniqueSubstringArray);
//                    }
//                }
//            }

//            return mod;
//        }



//        public List<VideoDetails> GetContinueWatch(Guid UserId, int lang, int pageIndex)
//        {
//            pageIndex = ((pageIndex > 0) ? (pageIndex - 1) : 0);

//            List<VideoDetails> mod = new List<VideoDetails>();
//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj3 = NewParameter();
//            obj3.ParameterName = "@UserId";
//            obj3.DbType = DbType.Guid;
//            obj3.Value = UserId;
//            parameterList.Add(obj3);

//            DbParameter obj4 = NewParameter();
//            obj4.ParameterName = "@lang";
//            obj4.DbType = DbType.Int32;
//            obj4.Value = lang;
//            parameterList.Add(obj4);

//            DbParameter obj5 = NewParameter();
//            obj5.ParameterName = "@PageNumber";
//            obj5.DbType = DbType.Int32;
//            obj5.Value = pageIndex;
//            parameterList.Add(obj5);

//            DbParameter obj6 = NewParameter();
//            obj6.ParameterName = "@PageSize";
//            obj6.DbType = DbType.Int32;
//            obj6.Value = 20;
//            parameterList.Add(obj6);

//            DataSet ds = ExecuteQuery("RM_GetContinueWatch", parameterList.ToArray());

//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new VideoDetails
//                    {
//                        CollectionId = HandleDBNull<Guid>(row, "CollectionId"),
//                        CollectionName = HandleDBNull<string>(row, "CollectionName"),
//                        ContentId = HandleDBNull<Guid>(row, "ContentId"),
//                        Title = HandleDBNull<string>(row, "Title"),
//                        Description = HandleDBNull<string>(row, "Description"),
//                        VideoId = HandleDBNull<Guid>(row, "VideoId"),
//                        VideoThumbnail = HandleDBNull<string>(row, "VideoThumbnail"),
//                        Duration = HandleDBNull<Int32>(row, "Duration"),
//                        RecordedDate = HandleDBNull<DateTime>(row, "Recordeddate"),
//                        PublishDate = HandleDBNull<DateTime>(row, "PublishDate"),
//                        seq = HandleDBNull<Int32>(row, "seq"),
//                        Source = HandleDBNull<string>(row, "SourceName"),
//                        StreamingId = HandleDBNull<string>(row, "youtubeId"),
//                        StreamingServer = HandleDBNull<string>(row, "StreamingServer"),
//                        IsSaved = HandleDBNull<Boolean>(row, "IsSaved"),
//                        IsLike = HandleDBNull<Boolean>(row, "Islike"),
//                        LikeCount = HandleDBNull<Int32>(row, "likeCount"),
//                        SavedCount = HandleDBNull<Int32>(row, "SavedCount"),
//                        PoetName = HandleDBNull<string>(row, "EntityName"),
//                        ArtistName = HandleDBNull<string>(row, "Artist"),
//                        TimeSpent = HandleDBNull<Int32>(row, "TimeSpent")
//                    });
//                }
//            }

//            return mod;
//        }



//        public IEnumerable<UserSurfingInfo> GetUserSurfingInfo(Guid userId, string videoIds)
//        {
//            var mod = new List<UserSurfingInfo>();

//            DataTable table = new DataTable();
//            table.Columns.Add("VideoId", typeof(Guid));


//            List<DbParameter> parameterList = new List<DbParameter>();
//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@UserId";
//            obj1.DbType = DbType.Guid;
//            obj1.Value = userId;
//            parameterList.Add(obj1);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@Ids";
//            obj2.DbType = DbType.String;
//            obj2.Value = videoIds;
//            parameterList.Add(obj2);

//            DataSet ds = ExecuteQuery("RM_GetUserSurfingInfo", parameterList.ToArray());

//            foreach (DataRow row in ds.Tables[0].Rows)
//            {
//                mod.Add(new UserSurfingInfo
//                {

//                    VideoId = HandleDBNull<Guid>(row, "VideoId"),
//                    UserId = userId,
//                    IsSave = HandleDBNull<Boolean>(row, "IsSave"),
//                    IsLike = HandleDBNull<Boolean>(row, "IsLike"),
//                    IsStarted = HandleDBNull<Boolean>(row, "IsStarted"),
//                    TimeSpent = HandleDBNull<Int32>(row, "TimeSpent"),
//                    LikeCount = HandleDBNull<Int32>(row, "LikeCount")
//                });
//            }
//            return mod;
//        }


//        public Kids GetKidsVideos(Guid VideoId, Guid CollectionId, Guid UserId, int lang)
//        {
//            var mod = new Kids();
//            mod.VideoDetail = new VideoDetails();
//            mod.VideoDetails = new List<VideoDetails>();


//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@VideoId";
//            obj1.DbType = DbType.Guid;
//            obj1.Value = VideoId;
//            parameterList.Add(obj1);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@CollectionId";
//            obj2.DbType = DbType.Guid;
//            obj2.Value = CollectionId;
//            parameterList.Add(obj2);

//            DbParameter obj3 = NewParameter();
//            obj3.ParameterName = "@UserId";
//            obj3.DbType = DbType.Guid;
//            obj3.Value = UserId;
//            parameterList.Add(obj3);

//            DbParameter obj4 = NewParameter();
//            obj4.ParameterName = "@lang";
//            obj4.DbType = DbType.Int32;
//            obj4.Value = lang;
//            parameterList.Add(obj4);

//            DataSet ds = ExecuteQuery("RM_GetKidsVideoDetails", parameterList.ToArray());

//            foreach (DataRow row in ds.Tables[0].Rows)
//            {
//                mod.VideoDetail = new VideoDetails();
//                mod.VideoDetail.CollectionId = HandleDBNull<Guid>(row, "CollectionId");
//                mod.VideoDetail.CollectionName = HandleDBNull<string>(row, "CollectionName");
//                mod.VideoDetail.ContentId = HandleDBNull<Guid>(row, "Id");
//                mod.VideoDetail.Title = HandleDBNull<string>(row, "Title");
//                mod.VideoDetail.Description = HandleDBNull<string>(row, "Description");
//                mod.VideoDetail.VideoId = HandleDBNull<Guid>(row, "VideoId");
//                mod.VideoDetail.VideoThumbnail = HandleDBNull<string>(row, "VideoThumbnail");
//                mod.VideoDetail.Duration = HandleDBNull<Int32>(row, "Duration");
//                mod.VideoDetail.RecordedDate = HandleDBNull<DateTime>(row, "Recordeddate");
//                mod.VideoDetail.PublishDate = HandleDBNull<DateTime>(row, "PublishDate");
//                mod.VideoDetail.seq = HandleDBNull<Int32>(row, "seq");
//                mod.VideoDetail.Source = HandleDBNull<string>(row, "SourceName");
//                mod.VideoDetail.IsStarted = HandleDBNull<Boolean>(row, "IsStarted");
//                mod.VideoDetail.TimeSpent = HandleDBNull<Int32>(row, "TimeSpent");
//                mod.VideoDetail.IsSaved = HandleDBNull<Boolean>(row, "IsSave");
//                mod.VideoDetail.LikeCount = HandleDBNull<Int32>(row, "LikeCount");
//                mod.VideoDetail.StreamingId = HandleDBNull<string>(row, "youtubeId");
//                mod.VideoDetail.StreamingServer = HandleDBNull<string>(row, "StreamingServer");
//            }
//            foreach (DataRow row in ds.Tables[1].Rows)
//            {
//                mod.VideoDetails.Add(new VideoDetails
//                {
//                    CollectionId = HandleDBNull<Guid>(row, "CollectionId"),
//                    CollectionName = HandleDBNull<string>(row, "CollectionName"),
//                    ContentId = HandleDBNull<Guid>(row, "Id"),
//                    Title = HandleDBNull<string>(row, "Title"),
//                    Description = HandleDBNull<string>(row, "Description"),
//                    VideoId = HandleDBNull<Guid>(row, "VideoId"),
//                    VideoThumbnail = HandleDBNull<string>(row, "VideoThumbnail"),
//                    Duration = HandleDBNull<Int32>(row, "Duration"),
//                    RecordedDate = HandleDBNull<DateTime>(row, "Recordeddate"),
//                    PublishDate = HandleDBNull<DateTime>(row, "PublishDate"),
//                    seq = HandleDBNull<Int32>(row, "seq"),
//                    Source = HandleDBNull<string>(row, "SourceName"),
//                    IsStarted = HandleDBNull<Boolean>(row, "IsStarted"),
//                    TimeSpent = HandleDBNull<Int32>(row, "TimeSpent"),
//                    IsSaved = HandleDBNull<Boolean>(row, "IsSave"),
//                    LikeCount = HandleDBNull<Int32>(row, "LikeCount"),
//                    StreamingId = HandleDBNull<string>(row, "youtubeId"),
//                    StreamingServer = HandleDBNull<string>(row, "StreamingServer"),
//                });
//            }



//            return mod;
//        }



//        public List<HomeScreenData> GetKidsHomeContent(int lang, string host)
//        {
//            var mod = new List<HomeScreenData>();
//            var mod1 = new List<VideoDetails>();
//            var mod2 = new List<RM_HomeTags>();

//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@lang";
//            obj1.DbType = DbType.Int32;
//            obj1.Value = lang;
//            parameterList.Add(obj1);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@host";
//            obj2.DbType = DbType.String;
//            obj2.Value = host;
//            parameterList.Add(obj2);

//            DataSet ds = ExecuteQuery("RM_GetKidsHomeContent", parameterList.ToArray());
//            foreach (DataRow row in ds.Tables[0].Rows)
//            {
//                mod.Add(new HomeScreenData
//                {
//                    CollectionId = HandleDBNull<Guid>(row, "CollectionId"),
//                    CollectionName = HandleDBNull<string>(row, "CollectionName"),
//                    Host = HandleDBNull<string>(row, "Host"),
//                    Type = HandleDBNull<string>(row, "CategoryName"),
//                    Index = HandleDBNull<Int32>(row, "CollectionIndex"),
//                });
//            }
//            foreach (DataRow row in ds.Tables[1].Rows)
//            {
//                mod1.Add(new VideoDetails
//                {
//                    CollectionId = HandleDBNull<Guid>(row, "CollectionId"),
//                    CollectionName = HandleDBNull<string>(row, "CollectionName"),
//                    ContentId = HandleDBNull<Guid>(row, "ContentId"),
//                    Title = HandleDBNull<string>(row, "Title"),
//                    Description = HandleDBNull<string>(row, "Description"),
//                    VideoId = HandleDBNull<Guid>(row, "VideoId"),
//                    VideoThumbnail = HandleDBNull<string>(row, "VideoThumbnail"),
//                    Duration = HandleDBNull<Int32>(row, "Duration"),
//                    RecordedDate = HandleDBNull<DateTime>(row, "Recordeddate"),
//                    PublishDate = HandleDBNull<DateTime>(row, "PublishDate"),
//                    seq = HandleDBNull<Int32>(row, "seq"),
//                    Source = HandleDBNull<string>(row, "SourceName"),
//                    StreamingId = HandleDBNull<string>(row, "youtubeId"),
//                    StreamingServer = HandleDBNull<string>(row, "StreamingServer"),
//                    IsSaved = HandleDBNull<Boolean>(row, "IsSaved"),
//                    IsLike = HandleDBNull<Boolean>(row, "Islike"),
//                    LikeCount = HandleDBNull<Int32>(row, "likeCount"),
//                    SavedCount = HandleDBNull<Int32>(row, "SavedCount"),
//                });
//            }
//            foreach (DataRow row in ds.Tables[2].Rows)
//            {
//                mod2.Add(new RM_HomeTags
//                {
//                    TopicId = HandleDBNull<Guid>(row, "TopicId"),
//                    ContentId = HandleDBNull<Guid>(row, "ContentId"),
//                    TopicName = HandleDBNull<string>(row, "TopicName"),
//                    HostName = HandleDBNull<string>(row, "HostName"),
//                    Description = HandleDBNull<string>(row, "Description")

//                });
//            }


//            foreach (var coll in mod)
//            {
//                coll.VideoDetails = mod1.Where(x => x.CollectionId == coll.CollectionId).OrderBy(x => x.seq).ToList();

//            }


//            return mod;
//        }





//        public List<FeedCard> FeedCardList(string searchText, int PageNumber, int PageSize, Guid? Id, string FromDate, string ToDate)
//        {
//            var mod = new List<FeedCard>();
//            if (PageNumber > 0)
//            {
//                PageNumber = PageNumber - 1;
//            }


//            List<DbParameter> parameterList = new List<DbParameter>();
//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@SearchText";
//            obj1.DbType = DbType.String;
//            obj1.Value = searchText;
//            parameterList.Add(obj1);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@PageNumber";
//            obj2.DbType = DbType.Int32;
//            obj2.Value = PageNumber;
//            parameterList.Add(obj2);

//            DbParameter obj3 = NewParameter();
//            obj3.ParameterName = "@PageSize";
//            obj3.DbType = DbType.Int32;
//            obj3.Value = PageSize;
//            parameterList.Add(obj3);

//            DbParameter obj4 = NewParameter();
//            obj4.ParameterName = "@FeedId";
//            obj4.DbType = DbType.Guid;
//            obj4.Value = Id;
//            parameterList.Add(obj4);

//            DbParameter obj5 = NewParameter();
//            obj5.ParameterName = "@FromDate";
//            obj5.DbType = DbType.String;
//            obj5.Value = FromDate;
//            parameterList.Add(obj5);

//            DbParameter obj6 = NewParameter();
//            obj6.ParameterName = "@ToDate";
//            obj6.DbType = DbType.String;
//            obj6.Value = ToDate;
//            parameterList.Add(obj6);


//            DataSet ds = ExecuteQuery("WP_FeedList", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new FeedCard
//                    {
//                        Id = HandleDBNull<Guid>(row, "Id"),
//                        CardTitle_En = HandleDBNull<string>(row, "CardTitle_En"),
//                        CardTitle_Hi = HandleDBNull<string>(row, "CardTitle_Hi"),
//                        CardTitle_Ur = HandleDBNull<string>(row, "CardTitle_Ur"),
//                        FeedTypeId = HandleDBNull<Guid>(row, "FeedTypeId"),
//                        FeedType = HandleDBNull<string>(row, "FeedType"),
//                        DisplayDate = HandleDBNull<DateTime>(row, "DisplayDate"),
//                        SourceId = HandleDBNull<Guid>(row, "SourceId"),
//                        SourceName = HandleDBNull<string>(row, "SourceName"),
//                        TemplateNo = HandleDBNull<Int32>(row, "TemplateNo"),
//                        EntityId = HandleDBNull<Guid>(row, "EntityId"),
//                        EntityName_En = HandleDBNull<string>(row, "EntityName_En"),
//                        EntityName_Hi = HandleDBNull<string>(row, "EntityName_Hi"),
//                        EntityName_Ur = HandleDBNull<string>(row, "EntityName_Ur"),
//                        EntityDomicile_En = HandleDBNull<string>(row, "EntityDomicile_En"),
//                        EntityDomicile_Hi = HandleDBNull<string>(row, "EntityDomicile_Hi"),
//                        EntityDomicile_Ur = HandleDBNull<string>(row, "EntityDomicile_Ur"),
//                        EntityDomicile_URL = HandleDBNull<string>(row, "EntityDomicile_URL"),
//                        URLTypeId = HandleDBNull<Guid>(row, "URLTypeId"),
//                        URLTypeName = HandleDBNull<string>(row, "URLTypeName"),
//                        EntitySlug = HandleDBNull<string>(row, "EntitySlug"),
//                        ContentTypeId = HandleDBNull<Guid>(row, "ContentTypeId"),
//                        ContentTypeName_En = HandleDBNull<string>(row, "ContentTypeName_En"),
//                        ContentTypeName_Hi = HandleDBNull<string>(row, "ContentTypeName_Hi"),
//                        ContentTypeName_Ur = HandleDBNull<string>(row, "ContentTypeName_Ur"),
//                        ContentTypeSlug = HandleDBNull<string>(row, "ContentTypeSlug"),
//                        ContentId = HandleDBNull<Guid>(row, "ContentId"),
//                        ContentTitle_En = HandleDBNull<string>(row, "ContentTitle_En"),
//                        ContentTitle_Hi = HandleDBNull<string>(row, "ContentTitle_Hi"),
//                        ContentTitle_Ur = HandleDBNull<string>(row, "ContentTitle_Ur"),
//                        ContentBody_En = HandleDBNull<string>(row, "ContentBody_En"),
//                        ContentBody_Hi = HandleDBNull<string>(row, "ContentBody_Hi"),
//                        ContentBody_Ur = HandleDBNull<string>(row, "ContentBody_Ur"),
//                        ContentSlug = HandleDBNull<string>(row, "ContentSlug"),
//                        ContentAvailableIn_En = HandleDBNull<Boolean>(row, "ContentAvailableIn_En"),
//                        ContentAvailableIn_Hi = HandleDBNull<Boolean>(row, "ContentAvailableIn_Hi"),
//                        ContentAvailableIn_Ur = HandleDBNull<Boolean>(row, "ContentAvailableIn_Ur"),
//                        ScriptLanguage = HandleDBNull<Int32>(row, "ScriptLanguage"),
//                        IsShowContentTitle = HandleDBNull<Boolean>(row, "IsShowContentTitle"),
//                        IsShayaryImage = HandleDBNull<Boolean>(row, "IsShayaryImage"),
//                        LinkingContentId = HandleDBNull<Guid>(row, "LinkingContentId"),
//                        ImageLanguage = HandleDBNull<Int32>(row, "ImageLanguage"),
//                        MediaType = HandleDBNull<Int32>(row, "MediaType"),
//                        IsVideo = HandleDBNull<Boolean>(row, "IsVideo"),
//                        VideoType = HandleDBNull<Int32>(row, "VideoType"),
//                        MediaAlbumName = HandleDBNull<string>(row, "MediaAlbumName"),
//                        MediaTitle = HandleDBNull<string>(row, "MediaTitle"),
//                        MediaDescription = HandleDBNull<string>(row, "MediaDescription"),
//                        IsMediaThumbImage = HandleDBNull<Boolean>(row, "IsMediaThumbImage"),
//                        MediaVideoUrl = HandleDBNull<string>(row, "MediaVideoUrl"),
//                        MediaDuration = HandleDBNull<Int32>(row, "MediaDuration"),
//                        BlogUrl = HandleDBNull<string>(row, "BlogUrl"),
//                        FeedStatus = HandleDBNull<Int32>(row, "FeedStatus"),
//                        DateCreated = HandleDBNull<DateTime>(row, "DateCreated"),
//                        DateModified = HandleDBNull<DateTime>(row, "DateModified"),
//                        CreatedBy = HandleDBNull<Guid>(row, "CreatedBy"),
//                        ModifiedBy = HandleDBNull<Guid>(row, "ModifiedBy"),
//                        IsActive = HandleDBNull<Boolean>(row, "IsActive"),
//                        VideoId = HandleDBNull<Guid>(row, "VideoId"),
//                        MediaImageURL = HandleDBNull<string>(row, "MediaImageURL"),
//                        VideoIntId = HandleDBNull<Int32>(row, "VideoIntId"),
//                        VideoFileName = HandleDBNull<string>(row, "VideoFileName"),
//                        TotalRec = HandleDBNull<Int32>(row, "TotalRec"),
//                        BlogId = HandleDBNull<Int32>(row, "BlogId"),
//                        ContentTitle = HandleDBNull<string>(row, "ContentTitle"),
//                        RescheduleTotalCount = HandleDBNull<Int32>(row, "RescheduleCount")
//                    });
//                }
//            }


//            return mod;
//        }

//        public void ActiveInactiveFeed(Guid Id, int type)
//        {
//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@Id";
//            obj1.DbType = DbType.Guid;
//            obj1.Value = Id;
//            parameterList.Add(obj1);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@Type";
//            obj2.DbType = DbType.Int32;
//            obj2.Value = type;
//            parameterList.Add(obj2);

//            ExecuteAction("WP_ActiveInactiveFeed", parameterList.ToArray());
//        }
//        public List<FeedType> FeedTypeList()
//        {
//            var mod = new List<FeedType>();

//            List<DbParameter> parameterList = new List<DbParameter>();
//            DataSet ds = ExecuteQuery("WP_FeedTypeList", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new FeedType
//                    {
//                        Id = HandleDBNull<Guid>(row, "Id"),
//                        Name_En = HandleDBNull<string>(row, "Name_En"),
//                        Name_Hi = HandleDBNull<string>(row, "Name_Hi"),
//                        Name_Ur = HandleDBNull<string>(row, "Name_Ur"),
//                        Seo_Slug = HandleDBNull<string>(row, "Seo_Slug"),
//                        DateCreated = HandleDBNull<DateTime>(row, "DateCreated"),
//                        DateModified = HandleDBNull<DateTime>(row, "DateModified"),
//                        CreatedBy = HandleDBNull<Guid>(row, "CreatedBy"),
//                        ModifiedBy = HandleDBNull<Guid>(row, "ModifiedBy"),
//                        IsActive = HandleDBNull<Boolean>(row, "IsActive")
//                    });
//                }
//            }


//            return mod;
//        }

//        public List<FeedSource> FeedSourceList()
//        {
//            var mod = new List<FeedSource>();

//            List<DbParameter> parameterList = new List<DbParameter>();
//            DataSet ds = ExecuteQuery("WP_FeedSourceList", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new FeedSource
//                    {
//                        Id = HandleDBNull<Guid>(row, "Id"),
//                        SourceName = HandleDBNull<string>(row, "SourceName"),
//                        Slug = HandleDBNull<string>(row, "Slug"),
//                        DateCreated = HandleDBNull<DateTime>(row, "DateCreated"),
//                        DateModified = HandleDBNull<DateTime>(row, "DateModified"),
//                        CreatedBy = HandleDBNull<Guid>(row, "CreatedBy"),
//                        ModifiedBy = HandleDBNull<Guid>(row, "ModifiedBy"),
//                        IsActive = HandleDBNull<Boolean>(row, "IsActive")
//                    });
//                }
//            }


//            return mod;
//        }



//        public List<Contenttype> ContentTypeList()
//        {
//            var mod = new List<Contenttype>();
//            List<DbParameter> parameterList = new List<DbParameter>();


//            DataSet ds = ExecuteQuery("sp_GetContenttypeList", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new Contenttype
//                    {
//                        Id = HandleDBNull<Guid>(row, "Id"),
//                        Name = HandleDBNull<string>(row, "Name"),

//                    });
//                }
//            }
//            return mod;
//        }


//        public void CreateFeed(FeedCard mod)
//        {
//            try
//            {
//                List<DbParameter> parameterList = new List<DbParameter>();

//                DbParameter obj0 = NewParameter();
//                obj0.ParameterName = "@Id";
//                obj0.DbType = DbType.Guid;
//                obj0.Value = mod.Id;
//                parameterList.Add(obj0);

//                DbParameter obj1 = NewParameter();
//                obj1.ParameterName = "@CardTitle_En";
//                obj1.DbType = DbType.String;
//                obj1.Value = mod.CardTitle_En;
//                parameterList.Add(obj1);

//                DbParameter obj2 = NewParameter();
//                obj2.ParameterName = "@CardTitle_Hi";
//                obj2.DbType = DbType.String;
//                obj2.Value = mod.CardTitle_Hi;
//                parameterList.Add(obj2);

//                DbParameter obj3 = NewParameter();
//                obj3.ParameterName = "@CardTitle_Ur";
//                obj3.DbType = DbType.String;
//                obj3.Value = mod.CardTitle_Ur;
//                parameterList.Add(obj3);

//                DbParameter obj4 = NewParameter();
//                obj4.ParameterName = "@FeedTypeId";
//                obj4.DbType = DbType.Guid;
//                obj4.Value = mod.FeedTypeId;
//                parameterList.Add(obj4);

//                DbParameter obj5 = NewParameter();
//                obj5.ParameterName = "@DisplayDate";
//                obj5.DbType = DbType.DateTime;
//                obj5.Value = mod.DisplayDate;
//                parameterList.Add(obj5);

//                DbParameter obj6 = NewParameter();
//                obj6.ParameterName = "@SourceId";
//                obj6.DbType = DbType.Guid;
//                obj6.Value = mod.SourceId;
//                parameterList.Add(obj6);

//                DbParameter obj7 = NewParameter();
//                obj7.ParameterName = "@SourceName";
//                obj7.DbType = DbType.String;
//                obj7.Value = mod.SourceName;
//                parameterList.Add(obj7);

//                DbParameter obj8 = NewParameter();
//                obj8.ParameterName = "@VideoId";
//                obj8.DbType = DbType.Guid;
//                obj8.Value = mod.VideoId;
//                parameterList.Add(obj8);

//                DbParameter obj9 = NewParameter();
//                obj9.ParameterName = "@EntityId";
//                obj9.DbType = DbType.Guid;
//                obj9.Value = mod.EntityId;
//                parameterList.Add(obj9);

//                DbParameter obj10 = NewParameter();
//                obj10.ParameterName = "@EntityName_En";
//                obj10.DbType = DbType.String;
//                obj10.Value = mod.EntityName_En;
//                parameterList.Add(obj10);

//                DbParameter obj11 = NewParameter();
//                obj11.ParameterName = "@EntityName_Hi";
//                obj11.DbType = DbType.String;
//                obj11.Value = mod.EntityName_Hi;
//                parameterList.Add(obj11);

//                DbParameter obj12 = NewParameter();
//                obj12.ParameterName = "@EntityName_Ur";
//                obj12.DbType = DbType.String;
//                obj12.Value = mod.EntityName_Ur;
//                parameterList.Add(obj12);

//                DbParameter obj13 = NewParameter();
//                obj13.ParameterName = "@EntityDomicile_En";
//                obj13.DbType = DbType.String;
//                obj13.Value = mod.EntityDomicile_En;
//                parameterList.Add(obj13);

//                DbParameter obj14 = NewParameter();
//                obj14.ParameterName = "@EntityDomicile_Hi";
//                obj14.DbType = DbType.String;
//                obj14.Value = mod.EntityDomicile_Hi;
//                parameterList.Add(obj14);

//                DbParameter obj15 = NewParameter();
//                obj15.ParameterName = "@EntityDomicile_Ur";
//                obj15.DbType = DbType.String;
//                obj15.Value = mod.EntityDomicile_Ur;
//                parameterList.Add(obj15);

//                DbParameter obj16 = NewParameter();
//                obj16.ParameterName = "@EntityDomicile_URL";
//                obj16.DbType = DbType.String;
//                obj16.Value = mod.EntityDomicile_URL;
//                parameterList.Add(obj16);

//                DbParameter obj17 = NewParameter();
//                obj17.ParameterName = "@URLTypeId";
//                obj17.DbType = DbType.Guid;
//                obj17.Value = mod.URLTypeId;
//                parameterList.Add(obj17);

//                DbParameter obj18 = NewParameter();
//                obj18.ParameterName = "@URLTypeName";
//                obj18.DbType = DbType.String;
//                obj18.Value = mod.URLTypeName;
//                parameterList.Add(obj18);

//                DbParameter obj19 = NewParameter();
//                obj19.ParameterName = "@EntitySlug";
//                obj19.DbType = DbType.String;
//                obj19.Value = mod.EntitySlug;
//                parameterList.Add(obj19);

//                DbParameter obj20 = NewParameter();
//                obj20.ParameterName = "@ContentTypeId";
//                obj20.DbType = DbType.Guid;
//                obj20.Value = mod.ContentTypeId;
//                parameterList.Add(obj20);

//                DbParameter obj21 = NewParameter();
//                obj21.ParameterName = "@ContentTypeName_En";
//                obj21.DbType = DbType.String;
//                obj21.Value = mod.ContentTypeName_En;
//                parameterList.Add(obj21);

//                DbParameter obj22 = NewParameter();
//                obj22.ParameterName = "@ContentTypeName_Hi";
//                obj22.DbType = DbType.String;
//                obj22.Value = mod.ContentTypeName_Hi;
//                parameterList.Add(obj22);

//                DbParameter obj23 = NewParameter();
//                obj23.ParameterName = "@ContentTypeName_Ur";
//                obj23.DbType = DbType.String;
//                obj23.Value = mod.ContentTypeName_Ur;
//                parameterList.Add(obj23);

//                DbParameter obj24 = NewParameter();
//                obj24.ParameterName = "@ContentTypeSlug";
//                obj24.DbType = DbType.String;
//                obj24.Value = mod.ContentTypeSlug;
//                parameterList.Add(obj24);

//                DbParameter obj25 = NewParameter();
//                obj25.ParameterName = "@ContentId";
//                obj25.DbType = DbType.Guid;
//                obj25.Value = mod.ContentId;
//                parameterList.Add(obj25);

//                DbParameter obj26 = NewParameter();
//                obj26.ParameterName = "@ContentTitle_En";
//                obj26.DbType = DbType.String;
//                obj26.Value = mod.ContentTitle_En;
//                parameterList.Add(obj26);

//                DbParameter obj27 = NewParameter();
//                obj27.ParameterName = "@ContentTitle_Hi";
//                obj27.DbType = DbType.String;
//                obj27.Value = mod.ContentTitle_Hi;
//                parameterList.Add(obj27);

//                DbParameter obj28 = NewParameter();
//                obj28.ParameterName = "@ContentTitle_Ur";
//                obj28.DbType = DbType.String;
//                obj28.Value = mod.ContentTitle_Ur;
//                parameterList.Add(obj28);

//                DbParameter obj29 = NewParameter();
//                obj29.ParameterName = "@ContentBody_En";
//                obj29.DbType = DbType.String;
//                obj29.Value = mod.ContentBody_En;
//                parameterList.Add(obj29);

//                DbParameter obj30 = NewParameter();
//                obj30.ParameterName = "@ContentBody_Hi";
//                obj30.DbType = DbType.String;
//                obj30.Value = mod.ContentBody_Hi;
//                parameterList.Add(obj30);

//                DbParameter obj31 = NewParameter();
//                obj31.ParameterName = "@ContentBody_Ur";
//                obj31.DbType = DbType.String;
//                obj31.Value = mod.ContentBody_Ur;
//                parameterList.Add(obj31);

//                DbParameter obj32 = NewParameter();
//                obj32.ParameterName = "@ContentSlug";
//                obj32.DbType = DbType.String;
//                obj32.Value = mod.ContentSlug;
//                parameterList.Add(obj32);

//                DbParameter obj33 = NewParameter();
//                obj33.ParameterName = "@ContentAvailableIn_En";
//                obj33.DbType = DbType.Boolean;
//                obj33.Value = mod.ContentAvailableIn_En;
//                parameterList.Add(obj33);

//                DbParameter obj34 = NewParameter();
//                obj34.ParameterName = "@ContentAvailableIn_Hi";
//                obj34.DbType = DbType.Boolean;
//                obj34.Value = mod.ContentAvailableIn_Hi;
//                parameterList.Add(obj34);

//                DbParameter obj35 = NewParameter();
//                obj35.ParameterName = "@ContentAvailableIn_Ur";
//                obj35.DbType = DbType.Boolean;
//                obj35.Value = mod.ContentAvailableIn_Ur;
//                parameterList.Add(obj35);

//                DbParameter obj36 = NewParameter();
//                obj36.ParameterName = "@ScriptLanguage";
//                obj36.DbType = DbType.Int32;
//                obj36.Value = mod.ScriptLanguage;
//                parameterList.Add(obj36);

//                DbParameter obj37 = NewParameter();
//                obj37.ParameterName = "@IsShowContentTitle";
//                obj37.DbType = DbType.Boolean;
//                obj37.Value = mod.IsShowContentTitle;
//                parameterList.Add(obj37);

//                DbParameter obj38 = NewParameter();
//                obj38.ParameterName = "@IsShayaryImage";
//                obj38.DbType = DbType.Boolean;
//                obj38.Value = mod.IsShayaryImage;
//                parameterList.Add(obj38);

//                DbParameter obj39 = NewParameter();
//                obj39.ParameterName = "@LinkingContentId";
//                obj39.DbType = DbType.Guid;
//                obj39.Value = mod.LinkingContentId;
//                parameterList.Add(obj39);


//                DbParameter obj40 = NewParameter();
//                obj40.ParameterName = "@ImageLanguage";
//                obj40.DbType = DbType.Int32;
//                obj40.Value = mod.ImageLanguage;
//                parameterList.Add(obj40);


//                DbParameter obj41 = NewParameter();
//                obj41.ParameterName = "@MediaType";
//                obj41.DbType = DbType.Int32;
//                obj41.Value = mod.MediaType;
//                parameterList.Add(obj41);


//                DbParameter obj42 = NewParameter();
//                obj42.ParameterName = "@IsVideo";
//                obj42.DbType = DbType.Boolean;
//                obj42.Value = mod.IsVideo;
//                parameterList.Add(obj42);


//                DbParameter obj43 = NewParameter();
//                obj43.ParameterName = "@VideoType";
//                obj43.DbType = DbType.Int32;
//                obj43.Value = mod.VideoType;
//                parameterList.Add(obj43);


//                DbParameter obj44 = NewParameter();
//                obj44.ParameterName = "@MediaAlbumName";
//                obj44.DbType = DbType.String;
//                obj44.Value = mod.MediaAlbumName;
//                parameterList.Add(obj44);


//                DbParameter obj45 = NewParameter();
//                obj45.ParameterName = "@MediaTitle";
//                obj45.DbType = DbType.String;
//                obj45.Value = mod.MediaTitle;
//                parameterList.Add(obj45);


//                DbParameter obj46 = NewParameter();
//                obj46.ParameterName = "@MediaDescription";
//                obj46.DbType = DbType.String;
//                obj46.Value = mod.MediaDescription;
//                parameterList.Add(obj46);


//                DbParameter obj47 = NewParameter();
//                obj47.ParameterName = "@IsMediaThumbImage";
//                obj47.DbType = DbType.Boolean;
//                obj47.Value = mod.IsMediaThumbImage;
//                parameterList.Add(obj47);


//                DbParameter obj48 = NewParameter();
//                obj48.ParameterName = "@MediaVideoUrl";
//                obj48.DbType = DbType.String;
//                obj48.Value = mod.MediaVideoUrl;
//                parameterList.Add(obj48);

//                DbParameter obj49 = NewParameter();
//                obj49.ParameterName = "@MediaImageURL";
//                obj49.DbType = DbType.String;
//                obj49.Value = mod.MediaImageURL;
//                parameterList.Add(obj49);

//                DbParameter obj50 = NewParameter();
//                obj50.ParameterName = "@MediaDuration";
//                obj50.DbType = DbType.Int32;
//                obj50.Value = mod.MediaDuration;
//                parameterList.Add(obj50);


//                DbParameter obj51 = NewParameter();
//                obj51.ParameterName = "@BlogUrl";
//                obj51.DbType = DbType.String;
//                obj51.Value = mod.BlogUrl;
//                parameterList.Add(obj51);


//                DbParameter obj52 = NewParameter();
//                obj52.ParameterName = "@FeedStatus";
//                obj52.DbType = DbType.Int32;
//                obj52.Value = mod.FeedStatus;
//                parameterList.Add(obj52);


//                DbParameter obj53 = NewParameter();
//                obj53.ParameterName = "@CreatedBy";
//                obj53.DbType = DbType.Guid;
//                obj53.Value = mod.CreatedBy;
//                parameterList.Add(obj53);

//                DbParameter obj54 = NewParameter();
//                obj54.ParameterName = "@BlogId";
//                obj54.DbType = DbType.Int32;
//                obj54.Value = mod.BlogId;
//                parameterList.Add(obj54);

//                DbParameter obj55 = NewParameter();
//                obj55.ParameterName = "@MediaPlatforms";
//                obj55.DbType = DbType.String;
//                obj55.Value = mod.MediaPlatforms;
//                parameterList.Add(obj55);

//                DbParameter obj56 = NewParameter();
//                obj56.ParameterName = "@Composition";
//                obj56.DbType = DbType.Int32;
//                obj56.Value = mod.Composition;
//                parameterList.Add(obj56);

//                DbParameter obj57 = NewParameter();
//                obj57.ParameterName = "@RescheduleCount";
//                obj57.DbType = DbType.Int32;
//                obj57.Value = mod.RescheduleTotalCount;
//                parameterList.Add(obj57);

//                DbParameter obj58 = NewParameter();
//                obj58.ParameterName = "@ParentId";
//                obj58.DbType = DbType.Guid;
//                obj58.Value = mod.ParentId;
//                parameterList.Add(obj58);

//                DbParameter obj59 = NewParameter();
//                obj59.ParameterName = "@IsDeferred";
//                obj59.DbType = DbType.Boolean;
//                obj59.Value = mod.IsDeferred;
//                parameterList.Add(obj59);

//                ExecuteAction("WP_CreateFeed", parameterList.ToArray());
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

//        }


//        //
//        public async Task CreateFeedAsync(List<FeedCard> mod)
//        {

//            foreach (var item in mod)
//            {
//                List<DbParameter> parameterList = new List<DbParameter>();

//                DbParameter obj0 = NewParameter();
//                obj0.ParameterName = "@Id";
//                obj0.DbType = DbType.Guid;
//                obj0.Value = item.Id;
//                parameterList.Add(obj0);

//                DbParameter obj1 = NewParameter();
//                obj1.ParameterName = "@CardTitle_En";
//                obj1.DbType = DbType.String;
//                obj1.Value = item.CardTitle_En;
//                parameterList.Add(obj1);

//                DbParameter obj2 = NewParameter();
//                obj2.ParameterName = "@CardTitle_Hi";
//                obj2.DbType = DbType.String;
//                obj2.Value = item.CardTitle_Hi;
//                parameterList.Add(obj2);

//                DbParameter obj3 = NewParameter();
//                obj3.ParameterName = "@CardTitle_Ur";
//                obj3.DbType = DbType.String;
//                obj3.Value = item.CardTitle_Ur;
//                parameterList.Add(obj3);

//                DbParameter obj4 = NewParameter();
//                obj4.ParameterName = "@FeedTypeId";
//                obj4.DbType = DbType.Guid;
//                obj4.Value = item.FeedTypeId;
//                parameterList.Add(obj4);

//                DbParameter obj5 = NewParameter();
//                obj5.ParameterName = "@DisplayDate";
//                obj5.DbType = DbType.DateTime;
//                obj5.Value = item.DisplayDate;
//                parameterList.Add(obj5);

//                DbParameter obj6 = NewParameter();
//                obj6.ParameterName = "@SourceId";
//                obj6.DbType = DbType.Guid;
//                obj6.Value = item.SourceId;
//                parameterList.Add(obj6);

//                DbParameter obj7 = NewParameter();
//                obj7.ParameterName = "@SourceName";
//                obj7.DbType = DbType.String;
//                obj7.Value = item.SourceName;
//                parameterList.Add(obj7);

//                DbParameter obj8 = NewParameter();
//                obj8.ParameterName = "@VideoId";
//                obj8.DbType = DbType.Guid;
//                obj8.Value = item.VideoId;
//                parameterList.Add(obj8);

//                DbParameter obj9 = NewParameter();
//                obj9.ParameterName = "@EntityId";
//                obj9.DbType = DbType.Guid;
//                obj9.Value = item.EntityId;
//                parameterList.Add(obj9);

//                DbParameter obj10 = NewParameter();
//                obj10.ParameterName = "@EntityName_En";
//                obj10.DbType = DbType.String;
//                obj10.Value = item.EntityName_En;
//                parameterList.Add(obj10);

//                DbParameter obj11 = NewParameter();
//                obj11.ParameterName = "@EntityName_Hi";
//                obj11.DbType = DbType.String;
//                obj11.Value = item.EntityName_Hi;
//                parameterList.Add(obj11);

//                DbParameter obj12 = NewParameter();
//                obj12.ParameterName = "@EntityName_Ur";
//                obj12.DbType = DbType.String;
//                obj12.Value = item.EntityName_Ur;
//                parameterList.Add(obj12);

//                DbParameter obj13 = NewParameter();
//                obj13.ParameterName = "@EntityDomicile_En";
//                obj13.DbType = DbType.String;
//                obj13.Value = item.EntityDomicile_En;
//                parameterList.Add(obj13);

//                DbParameter obj14 = NewParameter();
//                obj14.ParameterName = "@EntityDomicile_Hi";
//                obj14.DbType = DbType.String;
//                obj14.Value = item.EntityDomicile_Hi;
//                parameterList.Add(obj14);

//                DbParameter obj15 = NewParameter();
//                obj15.ParameterName = "@EntityDomicile_Ur";
//                obj15.DbType = DbType.String;
//                obj15.Value = item.EntityDomicile_Ur;
//                parameterList.Add(obj15);

//                DbParameter obj16 = NewParameter();
//                obj16.ParameterName = "@EntityDomicile_URL";
//                obj16.DbType = DbType.String;
//                obj16.Value = item.EntityDomicile_URL;
//                parameterList.Add(obj16);

//                DbParameter obj17 = NewParameter();
//                obj17.ParameterName = "@URLTypeId";
//                obj17.DbType = DbType.Guid;
//                obj17.Value = item.URLTypeId;
//                parameterList.Add(obj17);

//                DbParameter obj18 = NewParameter();
//                obj18.ParameterName = "@URLTypeName";
//                obj18.DbType = DbType.String;
//                obj18.Value = item.URLTypeName;
//                parameterList.Add(obj18);

//                DbParameter obj19 = NewParameter();
//                obj19.ParameterName = "@EntitySlug";
//                obj19.DbType = DbType.String;
//                obj19.Value = item.EntitySlug;
//                parameterList.Add(obj19);

//                DbParameter obj20 = NewParameter();
//                obj20.ParameterName = "@ContentTypeId";
//                obj20.DbType = DbType.Guid;
//                obj20.Value = item.ContentTypeId;
//                parameterList.Add(obj20);

//                DbParameter obj21 = NewParameter();
//                obj21.ParameterName = "@ContentTypeName_En";
//                obj21.DbType = DbType.String;
//                obj21.Value = item.ContentTypeName_En;
//                parameterList.Add(obj21);

//                DbParameter obj22 = NewParameter();
//                obj22.ParameterName = "@ContentTypeName_Hi";
//                obj22.DbType = DbType.String;
//                obj22.Value = item.ContentTypeName_Hi;
//                parameterList.Add(obj22);

//                DbParameter obj23 = NewParameter();
//                obj23.ParameterName = "@ContentTypeName_Ur";
//                obj23.DbType = DbType.String;
//                obj23.Value = item.ContentTypeName_Ur;
//                parameterList.Add(obj23);

//                DbParameter obj24 = NewParameter();
//                obj24.ParameterName = "@ContentTypeSlug";
//                obj24.DbType = DbType.String;
//                obj24.Value = item.ContentTypeSlug;
//                parameterList.Add(obj24);

//                DbParameter obj25 = NewParameter();
//                obj25.ParameterName = "@ContentId";
//                obj25.DbType = DbType.Guid;
//                obj25.Value = item.ContentId;
//                parameterList.Add(obj25);

//                DbParameter obj26 = NewParameter();
//                obj26.ParameterName = "@ContentTitle_En";
//                obj26.DbType = DbType.String;
//                obj26.Value = item.ContentTitle_En;
//                parameterList.Add(obj26);

//                DbParameter obj27 = NewParameter();
//                obj27.ParameterName = "@ContentTitle_Hi";
//                obj27.DbType = DbType.String;
//                obj27.Value = item.ContentTitle_Hi;
//                parameterList.Add(obj27);

//                DbParameter obj28 = NewParameter();
//                obj28.ParameterName = "@ContentTitle_Ur";
//                obj28.DbType = DbType.String;
//                obj28.Value = item.ContentTitle_Ur;
//                parameterList.Add(obj28);

//                DbParameter obj29 = NewParameter();
//                obj29.ParameterName = "@ContentBody_En";
//                obj29.DbType = DbType.String;
//                obj29.Value = item.ContentBody_En;
//                parameterList.Add(obj29);

//                DbParameter obj30 = NewParameter();
//                obj30.ParameterName = "@ContentBody_Hi";
//                obj30.DbType = DbType.String;
//                obj30.Value = item.ContentBody_Hi;
//                parameterList.Add(obj30);

//                DbParameter obj31 = NewParameter();
//                obj31.ParameterName = "@ContentBody_Ur";
//                obj31.DbType = DbType.String;
//                obj31.Value = item.ContentBody_Ur;
//                parameterList.Add(obj31);

//                DbParameter obj32 = NewParameter();
//                obj32.ParameterName = "@ContentSlug";
//                obj32.DbType = DbType.String;
//                obj32.Value = item.ContentSlug;
//                parameterList.Add(obj32);

//                DbParameter obj33 = NewParameter();
//                obj33.ParameterName = "@ContentAvailableIn_En";
//                obj33.DbType = DbType.Boolean;
//                obj33.Value = item.ContentAvailableIn_En;
//                parameterList.Add(obj33);

//                DbParameter obj34 = NewParameter();
//                obj34.ParameterName = "@ContentAvailableIn_Hi";
//                obj34.DbType = DbType.Boolean;
//                obj34.Value = item.ContentAvailableIn_Hi;
//                parameterList.Add(obj34);

//                DbParameter obj35 = NewParameter();
//                obj35.ParameterName = "@ContentAvailableIn_Ur";
//                obj35.DbType = DbType.Boolean;
//                obj35.Value = item.ContentAvailableIn_Ur;
//                parameterList.Add(obj35);

//                DbParameter obj36 = NewParameter();
//                obj36.ParameterName = "@ScriptLanguage";
//                obj36.DbType = DbType.Int32;
//                obj36.Value = item.ScriptLanguage;
//                parameterList.Add(obj36);

//                DbParameter obj37 = NewParameter();
//                obj37.ParameterName = "@IsShowContentTitle";
//                obj37.DbType = DbType.Boolean;
//                obj37.Value = item.IsShowContentTitle;
//                parameterList.Add(obj37);

//                DbParameter obj38 = NewParameter();
//                obj38.ParameterName = "@IsShayaryImage";
//                obj38.DbType = DbType.Boolean;
//                obj38.Value = item.IsShayaryImage;
//                parameterList.Add(obj38);

//                DbParameter obj39 = NewParameter();
//                obj39.ParameterName = "@LinkingContentId";
//                obj39.DbType = DbType.Guid;
//                obj39.Value = item.LinkingContentId;
//                parameterList.Add(obj39);


//                DbParameter obj40 = NewParameter();
//                obj40.ParameterName = "@ImageLanguage";
//                obj40.DbType = DbType.Int32;
//                obj40.Value = item.ImageLanguage;
//                parameterList.Add(obj40);


//                DbParameter obj41 = NewParameter();
//                obj41.ParameterName = "@MediaType";
//                obj41.DbType = DbType.Int32;
//                obj41.Value = item.MediaType;
//                parameterList.Add(obj41);


//                DbParameter obj42 = NewParameter();
//                obj42.ParameterName = "@IsVideo";
//                obj42.DbType = DbType.Boolean;
//                obj42.Value = item.IsVideo;
//                parameterList.Add(obj42);


//                DbParameter obj43 = NewParameter();
//                obj43.ParameterName = "@VideoType";
//                obj43.DbType = DbType.Int32;
//                obj43.Value = item.VideoType;
//                parameterList.Add(obj43);


//                DbParameter obj44 = NewParameter();
//                obj44.ParameterName = "@MediaAlbumName";
//                obj44.DbType = DbType.String;
//                obj44.Value = item.MediaAlbumName;
//                parameterList.Add(obj44);


//                DbParameter obj45 = NewParameter();
//                obj45.ParameterName = "@MediaTitle";
//                obj45.DbType = DbType.String;
//                obj45.Value = item.MediaTitle;
//                parameterList.Add(obj45);


//                DbParameter obj46 = NewParameter();
//                obj46.ParameterName = "@MediaDescription";
//                obj46.DbType = DbType.String;
//                obj46.Value = item.MediaDescription;
//                parameterList.Add(obj46);


//                DbParameter obj47 = NewParameter();
//                obj47.ParameterName = "@IsMediaThumbImage";
//                obj47.DbType = DbType.Boolean;
//                obj47.Value = item.IsMediaThumbImage;
//                parameterList.Add(obj47);


//                DbParameter obj48 = NewParameter();
//                obj48.ParameterName = "@MediaVideoUrl";
//                obj48.DbType = DbType.String;
//                obj48.Value = item.MediaVideoUrl;
//                parameterList.Add(obj48);

//                DbParameter obj49 = NewParameter();
//                obj49.ParameterName = "@MediaImageURL";
//                obj49.DbType = DbType.String;
//                obj49.Value = item.MediaImageURL;
//                parameterList.Add(obj49);

//                DbParameter obj50 = NewParameter();
//                obj50.ParameterName = "@MediaDuration";
//                obj50.DbType = DbType.Int32;
//                obj50.Value = item.MediaDuration;
//                parameterList.Add(obj50);


//                DbParameter obj51 = NewParameter();
//                obj51.ParameterName = "@BlogUrl";
//                obj51.DbType = DbType.String;
//                obj51.Value = item.BlogUrl;
//                parameterList.Add(obj51);


//                DbParameter obj52 = NewParameter();
//                obj52.ParameterName = "@FeedStatus";
//                obj52.DbType = DbType.Int32;
//                obj52.Value = item.FeedStatus;
//                parameterList.Add(obj52);


//                DbParameter obj53 = NewParameter();
//                obj53.ParameterName = "@CreatedBy";
//                obj53.DbType = DbType.Guid;
//                obj53.Value = item.CreatedBy;
//                parameterList.Add(obj53);

//                DbParameter obj54 = NewParameter();
//                obj54.ParameterName = "@BlogId";
//                obj54.DbType = DbType.Int32;
//                obj54.Value = item.BlogId;
//                parameterList.Add(obj54);

//                DbParameter obj55 = NewParameter();
//                obj55.ParameterName = "@MediaPlatforms";
//                obj55.DbType = DbType.String;
//                obj55.Value = item.MediaPlatforms;
//                parameterList.Add(obj55);

//                DbParameter obj56 = NewParameter();
//                obj56.ParameterName = "@Composition";
//                obj56.DbType = DbType.Int32;
//                obj56.Value = item.Composition;
//                parameterList.Add(obj56);

//                DbParameter obj57 = NewParameter();
//                obj57.ParameterName = "@RescheduleCount";
//                obj57.DbType = DbType.Int32;
//                obj57.Value = item.RescheduleTotalCount;
//                parameterList.Add(obj57);

//                DbParameter obj58 = NewParameter();
//                obj58.ParameterName = "@ParentId";
//                obj58.DbType = DbType.Guid;
//                obj58.Value = item.ParentId;
//                parameterList.Add(obj58);

//                DbParameter obj59 = NewParameter();
//                obj59.ParameterName = "@IsDeferred";
//                obj59.DbType = DbType.Boolean;
//                obj59.Value = item.IsDeferred;
//                parameterList.Add(obj59);

//                await ExecuteActionAsync("WP_CreateFeed", parameterList.ToArray());
//            }
//        }
//        //


//        public FeedCard GetContentDetail(Guid Id)
//        {
//            var mod = new FeedCard();
//            List<DbParameter> parameterList = new List<DbParameter>();
//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@id";
//            obj1.DbType = DbType.Guid;
//            obj1.Value = Id;
//            parameterList.Add(obj1);

//            DataSet ds = ExecuteQuery("WP_GetContentById", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.EntityId = HandleDBNull<Guid>(row, "EntityId");
//                    mod.EntityName_En = HandleDBNull<string>(row, "EntityName_En");
//                    mod.EntityName_Hi = HandleDBNull<string>(row, "EntityName_Hi");
//                    mod.EntityName_Ur = HandleDBNull<string>(row, "EntityName_Ur");
//                    mod.EntityDomicile_En = HandleDBNull<string>(row, "Domicile_En");
//                    mod.EntityDomicile_Hi = HandleDBNull<string>(row, "Domicile_Hi");
//                    mod.EntityDomicile_Ur = HandleDBNull<string>(row, "Domicile_Ur");
//                    mod.EntitySlug = HandleDBNull<string>(row, "EntitySlug");
//                    mod.URLTypeId = HandleDBNull<Guid>(row, "UrlTypeId");
//                    mod.URLTypeName = HandleDBNull<string>(row, "UrlTypeName");
//                    mod.ContentTypeId = HandleDBNull<Guid>(row, "ContentTypeId");
//                    mod.ContentTypeName_En = HandleDBNull<string>(row, "ContentTypeName_En");
//                    mod.ContentTypeName_Hi = HandleDBNull<string>(row, "ContentTypeName_Hi");
//                    mod.ContentTypeName_Ur = HandleDBNull<string>(row, "ContentTypeName_Ur");
//                    mod.ContentTypeSlug = HandleDBNull<string>(row, "ContentTypeSlug");
//                    mod.ContentId = HandleDBNull<Guid>(row, "ContentId");
//                    mod.ContentTitle_En = HandleDBNull<string>(row, "ContentTitle_En");
//                    mod.ContentTitle_Hi = HandleDBNull<string>(row, "ContentTitle_Hi");
//                    mod.ContentTitle_Ur = HandleDBNull<string>(row, "ContentTitle_Ur");
//                    mod.ContentBody_En = HandleDBNull<string>(row, "Content_En");
//                    mod.ContentBody_Hi = HandleDBNull<string>(row, "Content_Hi");
//                    mod.ContentBody_Ur = HandleDBNull<string>(row, "Content_Ur");
//                    mod.ContentSlug = HandleDBNull<string>(row, "ContentSlug");
//                    mod.IsShowContentTitle = HandleDBNull<Boolean>(row, "IsShowContentTitle");
//                    mod.Composition = HandleDBNull<Int16>(row, "Composition");
//                }
//            }
//            return mod;
//        }


//        public List<Feed_WordOfTheDay> WordOfTheDayList(Guid wordId, string text)
//        {
//            var mod = new List<Feed_WordOfTheDay>();
//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@WordId";
//            obj1.DbType = DbType.Guid;
//            obj1.Value = wordId;
//            parameterList.Add(obj1);


//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@Text";
//            obj2.DbType = DbType.String;
//            obj2.Value = text;
//            parameterList.Add(obj2);


//            DataSet ds = ExecuteQuery("WP_GetWordFeedList", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new Feed_WordOfTheDay
//                    {
//                        WordId = HandleDBNull<Guid>(row, "WordId"),
//                        Word = HandleDBNull<string>(row, "Word"),
//                        Word_Ro = HandleDBNull<string>(row, "Word_Ro"),
//                        Word_Ur = HandleDBNull<string>(row, "Word_Ur"),
//                        Word_Hi = HandleDBNull<string>(row, "Word_Hi"),
//                        SEO_Slug = HandleDBNull<string>(row, "SEO_Slug"),
//                        Word_Ur_Processed = HandleDBNull<string>(row, "Word_Ur_Processed"),
//                    });
//                }
//            }
//            return mod;
//        }



//        public List<GetAppBannerType> AppBannerList()
//        {
//            var mod = new List<GetAppBannerType>();
//            List<DbParameter> parameterList = new List<DbParameter>();
//            try
//            {

//                DataSet ds = ExecuteQuery("sp_GetAppBannerList", parameterList.ToArray());
//                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//                {
//                    foreach (DataRow row in ds.Tables[0].Rows)
//                    {
//                        mod.Add(new GetAppBannerType
//                        {
//                            Id = HandleDBNull<Int16>(row, "Id"),
//                            Name = HandleDBNull<string>(row, "Name"),
//                        });
//                    }
//                }
//            }
//            catch (Exception ex)
//            {

//            }
//            return mod;
//        }

//        public List<GetAppBannerForType> AppBannerForList()
//        {
//            var mod = new List<GetAppBannerForType>();
//            List<DbParameter> parameterList = new List<DbParameter>();


//            DataSet ds = ExecuteQuery("sp_BannerForList", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new GetAppBannerForType
//                    {
//                        Id = HandleDBNull<Guid>(row, "Id"),
//                        Name_Tr = HandleDBNull<string>(row, "Name_Tr"),
//                    });
//                }
//            }
//            return mod;
//        }

//        public List<GetAppBannerContentType> AppBannerContentList()
//        {
//            var mod = new List<GetAppBannerContentType>();
//            List<DbParameter> parameterList = new List<DbParameter>();


//            DataSet ds = ExecuteQuery("sp_GetContenttypeList", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new GetAppBannerContentType
//                    {
//                        Id = HandleDBNull<Guid>(row, "Id"),
//                        Name = HandleDBNull<string>(row, "Name"),
//                    });
//                }
//            }
//            return mod;
//        }


//        public List<Feed_WordOfTheDay> FeedWordOfTheDay(string Text)
//        {
//            var mod = new List<Feed_WordOfTheDay>();
//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj = NewParameter();
//            obj.ParameterName = "@Text";
//            obj.DbType = DbType.String;
//            obj.Value = Text;
//            parameterList.Add(obj);

//            DataSet ds = ExecuteQuery("WP_GetWordFeedList", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new Feed_WordOfTheDay
//                    {
//                        WordId = HandleDBNull<Guid>(row, "WordId"),
//                        Word = HandleDBNull<string>(row, "Word"),
//                    });
//                }
//            }
//            return mod;
//        }

//        public List<Feed_WordOfTheDayDetalis> Feed_WordOfTheDayDetalis(Guid WordId)
//        {
//            var mod = new List<Feed_WordOfTheDayDetalis>();
//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj = NewParameter();
//            obj.ParameterName = "@WordId";
//            obj.DbType = DbType.Guid;
//            obj.Value = WordId;
//            parameterList.Add(obj);

//            DataSet ds = ExecuteQuery("WP_GetWordFeedDetail", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new Feed_WordOfTheDayDetalis
//                    {
//                        WordId = HandleDBNull<Guid>(row, "WordId"),
//                        ImageId = HandleDBNull<Guid>(row, "ImageId"),
//                        Word = HandleDBNull<string>(row, "Word"),
//                        Word_Ro = HandleDBNull<string>(row, "Word_Ro"),
//                        Word_Hi = HandleDBNull<string>(row, "Word_Hi"),
//                        Word_Ur = HandleDBNull<string>(row, "Word_Ur")
//                    });
//                }
//            }
//            return mod;
//        }

//        #region Banner (19-06-2024)
//        public List<Feed_BannerDetalis> FeedBannerList(string SearchText, int PageNumber, int PageSize, Guid? Id)
//        {
//            var mod = new List<Feed_BannerDetalis>();
//            if (PageNumber > 0)
//            {
//                PageNumber = PageNumber - 1;
//            }

//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@SearchText";
//            obj1.DbType = DbType.String;
//            obj1.Value = SearchText;
//            parameterList.Add(obj1);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@PageNumber";
//            obj2.DbType = DbType.Int32;
//            obj2.Value = PageNumber;
//            parameterList.Add(obj2);

//            DbParameter obj3 = NewParameter();
//            obj3.ParameterName = "@PageSize";
//            obj3.DbType = DbType.Int32;
//            obj3.Value = PageSize;
//            parameterList.Add(obj3);

//            DbParameter obj4 = NewParameter();
//            obj4.ParameterName = "@Id";
//            obj4.DbType = DbType.Guid;
//            obj4.Value = Id;
//            parameterList.Add(obj4);

//            DataSet ds = ExecuteQuery("WP_FeedBannerList_New", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new Feed_BannerDetalis
//                    {
//                        Id = Guid.Parse(row["Id"].ToString()),
//                        BannerTypeId = Convert.ToInt32(row["BannerTypeId"]),
//                        BannerForId = Guid.Parse(row["BannerForId"].ToString()),
//                        ContentTypeId = Guid.Parse(row["ContentTypeId"].ToString()),
//                        BannerName_En = Convert.ToString(row["BannerName_En"]),
//                        BannerName_Hi = Convert.ToString(row["BannerName_Hi"]),
//                        BannerName_Ur = Convert.ToString(row["BannerName_Ur"]),
//                        Seq = Convert.ToInt32(row["Seq"]),
//                        BannerUrl = Convert.ToString(row["BannerUrl"]),
//                        IsActive = Convert.ToBoolean(row["IsActive"]),
//                        ImageUrl = Convert.ToString(row["ImageUrl"]),
//                        // FromDisplayDate = Convert.ToDateTime(row["FromDisplayDate"]),
//                        // ToDisplayDate = Convert.ToDateTime(row["ToDisplayDate"]),
//                        FromDisplayDate = row["FromDisplayDate"] != DBNull.Value ? Convert.ToDateTime(row["FromDisplayDate"]) : DateTime.MinValue,
//                        ToDisplayDate = row["ToDisplayDate"] != DBNull.Value ? Convert.ToDateTime(row["ToDisplayDate"]) : DateTime.MinValue, // Or any default value
//                        TotalRec = Convert.ToInt32(row["TotalRec"]),
//                        SourceId = Guid.TryParse(row["SourceId"]?.ToString(), out Guid sourceId) ? sourceId : Guid.Empty,
//                        //IsTargetNewWindow = Convert.ToBoolean(row["IsTargetNewWindow"])
//                        IsTargetNewWindow = row["IsTargetNewWindow"] != DBNull.Value && Convert.ToBoolean(row["IsTargetNewWindow"]),
//                        BannerForName = Convert.ToString(row["BannerForName"]),
//                        ContentTypeName = Convert.ToString(row["ContentTypeName"]),
//                        RelatedUrl = Convert.ToString(row["RelatedUrl"]),
//                        BannerTypeName = Convert.ToString(row["BannerTypeName"]),
//                        IsClick = row["IsClick"] != DBNull.Value ? Convert.ToBoolean(row["IsClick"]) : false,
//                        HaveImage = Convert.ToBoolean(row["HaveImage"]),
//                        IsFeedBased = row["IsFeedBased"] != DBNull.Value && Convert.ToBoolean(row["IsFeedBased"]),
//                        IsPoetBased = row["IsPoetBased"] != DBNull.Value && Convert.ToBoolean(row["IsPoetBased"])
//                    });
//                }
//            }
//            return mod;
//        }

//        public void CreateBanner(Feed_BannerDetalis mod)
//        {
//            try
//            {
//                List<DbParameter> parameterList = new List<DbParameter>();

//                DbParameter obj0 = NewParameter();
//                obj0.ParameterName = "@Id";
//                obj0.DbType = DbType.Guid;
//                obj0.Value = mod.Id;
//                parameterList.Add(obj0);

//                DbParameter obj1 = NewParameter();
//                obj1.ParameterName = "@BannerTypeId";
//                obj1.DbType = DbType.Int32;
//                obj1.Value = mod.BannerTypeId;
//                parameterList.Add(obj1);

//                DbParameter obj2 = NewParameter();
//                obj2.ParameterName = "@BannerForId";
//                obj2.DbType = DbType.Guid;
//                obj2.Value = mod.BannerForId;
//                parameterList.Add(obj2);

//                DbParameter obj3 = NewParameter();
//                obj3.ParameterName = "@ContentTypeId";
//                obj3.DbType = DbType.Guid;
//                obj3.Value = mod.ContentTypeId;
//                parameterList.Add(obj3);

//                DbParameter obj4 = NewParameter();
//                obj4.ParameterName = "@FromDisplayDate";
//                obj4.DbType = DbType.DateTime;
//                obj4.Value = mod.FromDisplayDate;
//                parameterList.Add(obj4);

//                DbParameter obj5 = NewParameter();
//                obj5.ParameterName = "@BannerName_En";
//                obj5.DbType = DbType.String;
//                obj5.Value = mod.BannerName_En;
//                parameterList.Add(obj5);

//                DbParameter obj6 = NewParameter();
//                obj6.ParameterName = "@BannerName_Hi";
//                obj6.DbType = DbType.String;
//                obj6.Value = mod.BannerName_Hi;
//                parameterList.Add(obj6);

//                DbParameter obj7 = NewParameter();
//                obj7.ParameterName = "@BannerName_Ur";
//                obj7.DbType = DbType.String;
//                obj7.Value = mod.BannerName_Ur;
//                parameterList.Add(obj7);

//                DbParameter obj8 = NewParameter();
//                obj8.ParameterName = "@BannerUrl";
//                obj8.DbType = DbType.String;
//                obj8.Value = mod.BannerUrl;
//                parameterList.Add(obj8);

//                DbParameter obj9 = NewParameter();
//                obj9.ParameterName = "@Seq";
//                obj9.DbType = DbType.Int32;
//                obj9.Value = mod.Seq;
//                parameterList.Add(obj9);

//                DbParameter obj10 = NewParameter();
//                obj10.ParameterName = "@HaveImage";
//                obj10.DbType = DbType.Boolean;
//                obj10.Value = mod.HaveImage;
//                parameterList.Add(obj10);

//                DbParameter obj11 = NewParameter();
//                obj11.ParameterName = "@IsActive";
//                obj11.DbType = DbType.Boolean;
//                obj11.Value = mod.IsActive;
//                parameterList.Add(obj11);

//                DbParameter obj12 = NewParameter();
//                obj12.ParameterName = "@DateCreated";
//                obj12.DbType = DbType.DateTime;
//                obj12.Value = mod.DateCreated;
//                parameterList.Add(obj12);

//                DbParameter obj13 = NewParameter();
//                obj13.ParameterName = "@DateModified";
//                obj13.DbType = DbType.DateTime;
//                obj13.Value = mod.DateModified;
//                parameterList.Add(obj13);

//                DbParameter obj14 = NewParameter();
//                obj14.ParameterName = "@CreatedBy";
//                obj14.DbType = DbType.Guid;
//                obj14.Value = mod.CreatedBy;
//                parameterList.Add(obj14);

//                DbParameter obj15 = NewParameter();
//                obj15.ParameterName = "@ModifiedBy";
//                obj15.DbType = DbType.Guid;
//                obj15.Value = mod.ModifiedBy;
//                parameterList.Add(obj15);

//                DbParameter obj16 = NewParameter();
//                obj16.ParameterName = "@ImageUrl";
//                obj16.DbType = DbType.String;
//                obj16.Value = mod.ImageUrl;
//                parameterList.Add(obj16);

//                DbParameter obj17 = NewParameter();
//                obj17.ParameterName = "@BannerTypeName";
//                obj17.DbType = DbType.String;
//                obj17.Value = mod.BannerTypeName;
//                parameterList.Add(obj17);

//                DbParameter obj18 = NewParameter();
//                obj18.ParameterName = "@SourceId";
//                obj18.DbType = DbType.Guid;
//                obj18.Value = mod.SourceId;
//                parameterList.Add(obj18);

//                DbParameter obj19 = NewParameter();
//                obj19.ParameterName = "@ToDisplayDate";
//                obj19.DbType = DbType.DateTime;
//                obj19.Value = mod.ToDisplayDate;
//                parameterList.Add(obj19);

//                DbParameter obj20 = NewParameter();
//                obj20.ParameterName = "@IsTargetNewWindow";
//                obj20.DbType = DbType.Boolean;
//                obj20.Value = mod.IsTargetNewWindow;
//                parameterList.Add(obj20);

//                DbParameter obj21 = NewParameter();
//                obj21.ParameterName = "@BannerForName";
//                obj21.DbType = DbType.String;
//                obj21.Value = mod.BannerForName;
//                parameterList.Add(obj21);

//                DbParameter obj22 = NewParameter();
//                obj22.ParameterName = "@ContentTypeName";
//                obj22.DbType = DbType.String;
//                obj22.Value = mod.ContentTypeName;
//                parameterList.Add(obj22);

//                DbParameter obj23 = NewParameter();
//                obj23.ParameterName = "@RelatedUrl";
//                obj23.DbType = DbType.String;
//                obj23.Value = mod.RelatedUrl;
//                parameterList.Add(obj23);

//                DbParameter obj24 = NewParameter();
//                obj24.ParameterName = "@IsClick";
//                obj24.DbType = DbType.Boolean;
//                obj24.Value = mod.IsClick;
//                parameterList.Add(obj24);

//                DbParameter obj25 = NewParameter();
//                obj25.ParameterName = "@IsPoetBased";
//                obj25.DbType = DbType.Boolean;
//                obj25.Value = mod.IsPoetBased;
//                parameterList.Add(obj25);

//                DbParameter obj26 = NewParameter();
//                obj26.ParameterName = "@IsFeedBased";
//                obj26.DbType = DbType.Boolean;
//                obj26.Value = mod.IsFeedBased;
//                parameterList.Add(obj26);

//                ExecuteAction("WP_CreateBanner", parameterList.ToArray());
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

//        }
//        public void ToggleFeedBannerStatus(Guid Id)
//        {
//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@Id";
//            obj1.DbType = DbType.Guid;
//            obj1.Value = Id;
//            parameterList.Add(obj1);

//            ExecuteAction("WP40_ToggleFeedBannerStatus", parameterList.ToArray());
//        }
//        #endregion
//        #region

//        public ContentMaster GetContentMaster(int contentId)
//        {
//            var mod = new ContentMaster();
//            var mod1 = new List<InterViewDetlais>();
//            List<DbParameter> parameterList = new List<DbParameter>();
//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@contentId";
//            obj1.DbType = DbType.Int64;
//            obj1.Value = contentId;
//            parameterList.Add(obj1);

//            DataSet ds = ExecuteQuery("GETContenDetlais", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.ContentId = HandleDBNull<int>(row, "ContentId");
//                    mod.HostName = HandleDBNull<string>(row, "HostName");
//                    mod.ContentGUID = HandleDBNull<Guid>(row, "ContentGUID");
//                    mod.ContentName = HandleDBNull<string>(row, "ContentName");
//                    mod.Description = HandleDBNull<string>(row, "Description");
//                    mod.TagName = HandleDBNull<string>(row, "TagName");
//                    mod.Thumbnail = HandleDBNull<string>(row, "Thumbnail");
//                    mod.TargetURL = HandleDBNull<string>(row, "TargetURL");
//                    mod.Recordeddate = HandleDBNull<DateTime>(row, "Recordeddate");
//                    mod.SourceId = HandleDBNull<Guid>(row, "SourceId");
//                    mod.CategorySource = HandleDBNull<int>(row, "CategorySource");
//                    mod.VOD = HandleDBNull<int>(row, "VOD");
//                    mod.ScriptLang = HandleDBNull<int>(row, "ScriptLang");
//                    mod.OccasionListId = HandleDBNull<int>(row, "OccasionListId");
//                    mod.VideoFormat = HandleDBNull<int>(row, "VideoFormat");
//                    mod.ScriptLang = HandleDBNull<int>(row, "ScriptLang");
//                    mod.ScriptLang = HandleDBNull<int>(row, "ScriptLang");
//                    mod.RekhtaContentId = HandleDBNull<Guid>(row, "RekhtaContentId");
//                    mod.RekhtaContentTypeId = HandleDBNull<Guid>(row, "RekhtaContentTypeId");
//                    mod.youtubeId = HandleDBNull<string>(row, "youtubeId");
//                    mod.ChannelName = HandleDBNull<string>(row, "ChannelName");
//                    mod.ContentTitle = HandleDBNull<string>(row, "ContentTitle");
//                    mod.ContentTypeTitle = HandleDBNull<string>(row, "ContentTypeTitle");
//                    mod.VideoThumbnail = HandleDBNull<string>(row, "VideoThumbnail");
//                    mod.FileName = HandleDBNull<string>(row, "FileName");
//                    mod.Active = HandleDBNull<bool>(row, "Active");
//                    mod.VideoStatus = HandleDBNull<int>(row, "VideoStatus");
//                    mod.ShowAsAd = HandleDBNull<bool>(row, "ShowAsAd");
//                }

//                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
//                {
//                    foreach (DataRow row in ds.Tables[1].Rows)
//                    {
//                        mod1.Add(new InterViewDetlais
//                        {
//                            Id = HandleDBNull<Guid>(row, "Id"),
//                            ContentMasterId = HandleDBNull<Guid>(row, "ContentMasterId"),
//                            Name = HandleDBNull<string>(row, "Name"),
//                            InterViewType = HandleDBNull<string>(row, "InterViewType"),
//                            Name_Tr = HandleDBNull<string>(row, "Name_Tr"),
//                            EntityType = HandleDBNull<Guid>(row, "EntityType")
//                        });
//                    }
//                    mod.Interview = mod1; // Link details
//                }
//            }
//            return mod;
//        }

        
//        #endregion


//        #region Entity Explorer
//        public List<EntityExpolere> EntityExplorer(string searchText, int PageNumber, int PageSize, Guid? Id, string FromDate, string ToDate)
//        {
//            var mod = new List<EntityExpolere>();
//            if (PageNumber > 0)
//            {
//                PageNumber = PageNumber - 1;
//            }

//            List<DbParameter> parameterList = new List<DbParameter>();
//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@SearchText";
//            obj1.DbType = DbType.String;
//            obj1.Value = searchText;
//            parameterList.Add(obj1);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@PageNumber";
//            obj2.DbType = DbType.Int32;
//            obj2.Value = PageNumber;
//            parameterList.Add(obj2);

//            DbParameter obj3 = NewParameter();
//            obj3.ParameterName = "@PageSize";
//            obj3.DbType = DbType.Int32;
//            obj3.Value = PageSize;
//            parameterList.Add(obj3);

//            DbParameter obj4 = NewParameter();
//            obj4.ParameterName = "@FeedId";
//            obj4.DbType = DbType.Guid;
//            obj4.Value = Id;
//            parameterList.Add(obj4);

//            DbParameter obj5 = NewParameter();
//            obj5.ParameterName = "@FromDate";
//            obj5.DbType = DbType.String;
//            obj5.Value = FromDate;
//            parameterList.Add(obj5);

//            DbParameter obj6 = NewParameter();
//            obj6.ParameterName = "@ToDate";
//            obj6.DbType = DbType.String;
//            obj6.Value = ToDate;
//            parameterList.Add(obj6);


//            DataSet ds = ExecuteQuery("WP40_GetAllEntityExploreList", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new EntityExpolere
//                    {
//                        Id = HandleDBNull<Guid>(row, "Id"),
//                        EntityId = HandleDBNull<Guid>(row, "EntityId"),
//                        Name = HandleDBNull<string>(row, "Name"),
//                        IsActive = HandleDBNull<Boolean>(row, "IsActive")
//                    });
//                }
//            }
//            return mod;
//        }


//        public List<EntityExplorerDetails> EntityExploreByIdList(Guid id, string searchText)
//        {
//            var mod = new List<EntityExplorerDetails>();

//            List<DbParameter> parameterList = new List<DbParameter>();
//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@id";
//            obj1.DbType = DbType.Guid;
//            obj1.Value = id;
//            parameterList.Add(obj1);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@searchText";
//            obj2.DbType = DbType.String;
//            obj2.Value = searchText;
//            parameterList.Add(obj2);


//            DataSet ds = ExecuteQuery("WP40_GetEntityExplorerById", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new EntityExplorerDetails
//                    {
//                        TotalFeed = HandleDBNull<int>(row, "TotalFeed"),
//                        Id = HandleDBNull<Guid>(row, "Id"),
//                        ExploreId = HandleDBNull<Guid>(row, "ExploreId"),
//                        Sequence = HandleDBNull<int>(row, "Sequence"),
//                        ContentId = HandleDBNull<Guid>(row, "ContentId"),
//                        ContentType = HandleDBNull<string>(row, "ContentType"),
//                        Title = HandleDBNull<string>(row, "Title"),
//                        Description = HandleDBNull<string>(row, "Description"),
//                        Link = HandleDBNull<string>(row, "Link"),
//                        Duration = HandleDBNull<string>(row, "Duration"),
//                        ContentTitle_En = HandleDBNull<string>(row, "ContentTitle_En"),
//                        ContentTitle_Hi = HandleDBNull<string>(row, "ContentTitle_Hi"),
//                        ContentTitle_Ur = HandleDBNull<string>(row, "ContentTitle_Ur"),
//                        ContentBody_En = HandleDBNull<string>(row, "ContentBody_En"),
//                        ContentBody_Hi = HandleDBNull<string>(row, "ContentBody_Hi"),
//                        ContentBody_Ur = HandleDBNull<string>(row, "ContentBody_Ur"),
//                        EntityName = HandleDBNull<string>(row, "EntityName"),
//                        DisplayTypeId = HandleDBNull<int>(row, "DisplayTypeId")
//                        //ContentSlug = HandleDBNull<string>(row, "ContentSlug")
//                    });
//                }
//            }
//            return mod;
//        }


//        public List<EntityExplorerDetails> EntityExplore(Guid id, string MediaType)
//        {
//            var mod = new List<EntityExplorerDetails>();

//            List<DbParameter> parameterList = new List<DbParameter>();
//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@id";
//            obj1.DbType = DbType.Guid;
//            obj1.Value = id;
//            parameterList.Add(obj1);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@MediaType";
//            obj2.DbType = DbType.String;
//            obj2.Value = MediaType;
//            parameterList.Add(obj2);

//            DataSet ds = ExecuteQuery("WP40_GETEntityExplore", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new EntityExplorerDetails
//                    {
//                        Id = HandleDBNull<Guid>(row, "Id"),
//                        ExploreId = HandleDBNull<Guid>(row, "ExploreId"),
//                        ContentId = HandleDBNull<Guid>(row, "ContentId"),
//                        Title = HandleDBNull<string>(row, "Title"),
//                        MediaId = HandleDBNull<Guid>(row, "MediaId"),
//                        ContentTypeId = HandleDBNull<Guid>(row, "ContentTypeId")
//                    });
//                }
//            }
//            return mod;
//        }

//        public void CreateEntityExplore(EntityExplorerDetails ed)
//        {
//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj0 = NewParameter();
//            obj0.ParameterName = "@Id";
//            obj0.DbType = DbType.Guid;
//            obj0.Value = ed.Id;
//            parameterList.Add(obj0);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@ExploreId";
//            obj2.DbType = DbType.Guid;
//            obj2.Value = ed.ExploreId;
//            parameterList.Add(obj2);


//            DbParameter obj3 = NewParameter();
//            obj3.ParameterName = "@ContentId";
//            obj3.DbType = DbType.Guid;
//            obj3.Value = ed.ContentId;
//            parameterList.Add(obj3);

//            DbParameter obj4 = NewParameter();
//            obj4.ParameterName = "@MediaId";
//            obj4.DbType = DbType.Guid;
//            obj4.Value = ed.MediaId;
//            parameterList.Add(obj4);

//            //DbParameter obj3 = NewParameter();
//            //obj3.ParameterName = "@DateModified";
//            //obj3.DbType = DbType.Date;
//            //obj3.Value = ed.DateModified;
//            //parameterList.Add(obj3);

//            //DbParameter obj4 = NewParameter();
//            //obj4.ParameterName = "@ModifiedBy";
//            //obj4.DbType = DbType.Guid;
//            //obj4.Value = ed.ModifiedBy;
//            //parameterList.Add(obj4);

//            ExecuteAction("WP40_UpSrtEntityExplorer", parameterList.ToArray());
//        }

//        #endregion

//        #region Feed Banner 
//        public Feed_BannerDetalis GetBannerDetalisById(string bannerForType, string ContentType, int BannerTypeId)
//        {
//            var mod = new Feed_BannerDetalis();
//            List<DbParameter> parameterList = new List<DbParameter>();
//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@bannerForType";
//            obj1.DbType = DbType.String;
//            obj1.Value = bannerForType;
//            parameterList.Add(obj1);

//            DbParameter obj2 = NewParameter();
//            obj2.ParameterName = "@ContentType";
//            obj2.DbType = DbType.String;
//            obj2.Value = ContentType;
//            parameterList.Add(obj2);

//            DbParameter obj3 = NewParameter();
//            obj3.ParameterName = "@BannerTypeId";
//            obj3.DbType = DbType.Int64;
//            obj3.Value = BannerTypeId;
//            parameterList.Add(obj3);

//            DataSet ds = ExecuteQuery("WP40_BannerDetalisById", parameterList.ToArray());
//            // DataSet ds = ExecuteQuery("WP40_GetDetailsTest", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {

//                    if (BannerTypeId == 12)
//                    {
//                        mod.BannerForId = HandleDBNull<Guid>(row, "EntityUrlId");
//                        mod.ContentTypeId = HandleDBNull<Guid>(row, "ContentUrlId");
//                        mod.IsPoetBased = HandleDBNull<bool>(row, "IsPoetBased");
//                    }
//                    else
//                    {
//                        mod.BannerForId = HandleDBNull<Guid>(row, "EntityUrlId");
//                    }
//                }
//            }

//            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[1].Rows)
//                {
//                    mod.ContentTypeId = HandleDBNull<Guid>(row, "ContentUrlId");
//                }
//            }
//            return mod;
//        }
//        #endregion

//        #region
//        public List<FeedCard> FeedTypeDetalis(Guid FeedTypeId, int DaysRecordCount)
//        {
//            var mod = new List<FeedCard>();

//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj = NewParameter();
//            obj.ParameterName = "@FeedTypeId";
//            obj.DbType = DbType.Guid;
//            obj.Value = FeedTypeId;
//            parameterList.Add(obj);

//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@DaysRecordCount";
//            obj1.DbType = DbType.Int64;
//            obj1.Value = DaysRecordCount;
//            parameterList.Add(obj1);

//            DataSet ds = ExecuteQuery("W40_GetFeedDetalis", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new FeedCard
//                    {
//                        Id = HandleDBNull<Guid>(row, "Id"),
//                        MediaTitle = HandleDBNull<string>(row, "MediaTitle"),
//                        MediaDescription = HandleDBNull<string>(row, "MediaDescription"),
//                        ContentTitle_En = HandleDBNull<string>(row, "ContentTitle_En"),
//                        ContentTitle_Hi = HandleDBNull<string>(row, "ContentTitle_Hi"),
//                        ContentTitle_Ur = HandleDBNull<string>(row, "ContentTitle_Ur"),
//                        FeedTypeId = HandleDBNull<Guid>(row, "FeedTypeId"),
//                        DateCreated = HandleDBNull<DateTime>(row, "DateCreated"),
//                        DateModified = HandleDBNull<DateTime>(row, "DateModified"),
//                        CreatedBy = HandleDBNull<Guid>(row, "CreatedBy"),
//                        ModifiedBy = HandleDBNull<Guid>(row, "ModifiedBy"),
//                        IsActive = HandleDBNull<Boolean>(row, "IsActive"),
//                        ContentId = HandleDBNull<Guid>(row, "ContentId")
//                    });
//                }
//            }
//            return mod;
//        }


//        public List<Videosuggestions> FeedVideodetalis(Guid FeedTypeId, Guid ContentTypeId)
//        {
//            var mod = new List<Videosuggestions>();

//            List<DbParameter> parameterList = new List<DbParameter>();
//            DbParameter obj = NewParameter();
//            obj.ParameterName = "@FeedTypeId";
//            obj.DbType = DbType.Guid;
//            obj.Value = FeedTypeId;
//            parameterList.Add(obj);

//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@ContentTypeId";
//            obj1.DbType = DbType.Guid;
//            obj1.Value = ContentTypeId;
//            parameterList.Add(obj1);
//            DataSet ds = ExecuteQuery("WP40_GetAudioList", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new Videosuggestions
//                    {
//                        Id = HandleDBNull<Guid>(row, "Id"),
//                        Title = HandleDBNull<string>(row, "Title"),
//                        VideoId = HandleDBNull<Guid>(row, "Id"),
//                        Duration = HandleDBNull<int>(row, "Duration"),
//                        VideoThumbnail = HandleDBNull<string>(row, "VideoThumbnail"),
//                        FileName = HandleDBNull<string>(row, "FileName"),
//                        Description = HandleDBNull<string>(row, "Description"),
//                        ContentId = HandleDBNull<int>(row, "ContentId"),
//                        VideoFormat = HandleDBNull<int>(row, "VideoFormat"),
//                    });
//                }
//            }
//            return mod;
//        }
//        #endregion


//        public List<SearchContentModel> FeedContentDetalis(Guid FeedTypeId, Guid ContentTypeId)
//        {
//            var mod = new List<SearchContentModel>();

//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj = NewParameter();
//            obj.ParameterName = "@FeedTypeId";
//            obj.DbType = DbType.Guid;
//            obj.Value = FeedTypeId;
//            parameterList.Add(obj);

//            DbParameter obj1 = NewParameter();
//            obj1.ParameterName = "@ContentTypeId";
//            obj1.DbType = DbType.Guid;
//            obj1.Value = ContentTypeId;
//            parameterList.Add(obj1);


//            DataSet ds = ExecuteQuery("WP40_GetAudioList", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new SearchContentModel
//                    {
//                        Id = HandleDBNull<Guid>(row, "Id"),
//                        Title_En = HandleDBNull<string>(row, "Title_En"),
//                        Title_Hi = HandleDBNull<string>(row, "Title_Hi"),
//                        Title_Ur = HandleDBNull<string>(row, "Title_Ur"),
//                        Content_En = HandleDBNull<string>(row, "Content_En"),
//                        Content_Hi = HandleDBNull<string>(row, "Content_Hi"),
//                        Content_Ur = HandleDBNull<string>(row, "Content_Ur"),
//                    });
//                }
//            }
//            return mod;
//        }

//        public static T HandleDBNull<T>(DataRow row, string key)
//        {
//            var v = row[key];
//            if (v == DBNull.Value || v == null)
//            {
//                return default(T);
//            }
//            return (T)v;
//        }


//        public static dynamic GetDeepLinkingById(string idSlug)
//        {
//            string result = "";
//            bool isValid = true;
//            try
//            {

//                //if (!string.IsNullOrEmpty(urlPath) && urlPath.Contains("/"))
//                //{
//                //    idSlug = urlPath.TrimStart('/');
//                //}
//                if (isValid)
//                {
//                    var payload = new
//                    {
//                        dynamicLinkInfo = new
//                        {
//                            domainUriPrefix = "https://rekhta.page.link",
//                            link = "https://www.rekhta.org/" + idSlug,
//                            androidInfo = new
//                            {
//                                androidPackageName = "org.Rekhta",
//                            },
//                            iosInfo = new
//                            {
//                                iosBundleId = "org.rekhta",
//                            }
//                        }
//                    };

//                    string postbody = Newtonsoft.Json.JsonConvert.SerializeObject(payload).ToString();

//                    string serverApiKey = "AIzaSyAEd-NGGDVnPO8Q950c_8qFrLQ80RxgAGE";
//                    result = GetDeepLinkUrl(postbody, serverApiKey, "rekhta-media");
//                }
//            }
//            catch (Exception ex)
//            {

//            }

//            var res = new
//            {
//                ShortLink = result,

//            };

//            return res;
//        }

//        public static string GetDeepLinkUrl(string postbody, string serverApiKey, string sourceRef)
//        {
//            string shortLink = "";
//            string result = "";
//            try
//            {
//                string url = "https://firebasedynamiclinks.googleapis.com/v1/shortLinks?key=" + serverApiKey;
//                System.Net.WebRequest tRequest = System.Net.WebRequest.Create(url);
//                tRequest.Method = "post";
//                tRequest.ContentType = "application/json";

//                /* add below code to prevent "Authentication failed because the remote party has closed the transport stream" */
//                //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
//                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; // Only TLS 1.2 — safest and recommended



//                Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postbody);
//                tRequest.ContentLength = byteArray.Length;

//                using (System.IO.Stream dataStream = tRequest.GetRequestStream())
//                {
//                    dataStream.Write(byteArray, 0, byteArray.Length);

//                    using (System.Net.WebResponse tResponse = tRequest.GetResponse())
//                    {
//                        using (System.IO.Stream dataStreamResponse = tResponse.GetResponseStream())
//                        {
//                            using (System.IO.StreamReader tReader = new System.IO.StreamReader(dataStreamResponse))
//                            {
//                                result = tReader.ReadToEnd();

//                                if (!string.IsNullOrEmpty(result))
//                                {
//                                    // var json = JsonSerializer().Deserialize<dynamic>(result);
//                                    var json = JsonSerializer.Deserialize<dynamic>(result);
//                                    if (json.Count > 0)
//                                        shortLink = json["shortLink"];
//                                }

//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//            return shortLink;
//        }

//        #region Get Search Entity
//        public List<EntittyBordingModel> GetSearchEntity(string term)
//        {
//            try
//            {
//                var mod = new List<EntittyBordingModel>();
//                List<DbParameter> parameterList = new List<DbParameter>();
//                DbParameter obj1 = NewParameter();
//                obj1.ParameterName = "@Term";
//                obj1.DbType = DbType.String;
//                obj1.Value = term;
//                parameterList.Add(obj1);

//                DataSet ds = ExecuteQuery("WP40_EntittyOnBording", parameterList.ToArray());
//                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//                {
//                    foreach (DataRow row in ds.Tables[0].Rows)
//                    {
//                        mod.Add(new EntittyBordingModel
//                        {
//                            Id = HandleDBNull<Guid>(row, "Id"),
//                            Name = HandleDBNull<string>(row, "Name")
//                        });
//                    }
//                }
//                return mod;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        #endregion
       

//        #region sEARCH cOLLECTION 
//        public List<SearchCollectionModel> GetSeacrchCollection()
//        {
//            List<SearchCollectionModel> mod = new List<SearchCollectionModel>();
//            List<DbParameter> parameterList = new List<DbParameter>();

//            DataSet ds = ExecuteQuery("WP40_GetSearchCollection", parameterList.ToArray());
//            foreach (DataRow row in ds.Tables[0].Rows)
//            {
//                mod.Add(new SearchCollectionModel
//                {
//                    Id = HandleDBNull<Guid>(row, "Id"),
//                    CollectionTypeName = HandleDBNull<string>(row, "CollectionTypeName"),
//                    CollectionTypeSlug = HandleDBNull<string>(row, "CollectionTypeSlug"),
//                    TypeName = HandleDBNull<string>(row, "TypeName"),
//                });

//            }
//            return mod;
//        }

//        public List<SearchCollection> GetSeacrhCollectionById(Guid id)
//        {
//            var mod = new List<SearchCollection>();
//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj = NewParameter();
//            obj.ParameterName = "@Id";
//            obj.DbType = DbType.Guid;
//            obj.Value = id;
//            parameterList.Add(obj);

//            DataSet ds = ExecuteQuery("WP40_GetSearchCollectionList", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new SearchCollection
//                    {
//                        Id = HandleDBNull<Guid>(row, "Id"),
//                        //CollectionTypeId = HandleDBNull<Guid>(row, "CollectionTypeId"),
//                        Seq = HandleDBNull<Int32>(row, "Seq"),
//                        Name_En = HandleDBNull<string>(row, "Name_En"),
//                        Name_Hi = HandleDBNull<string>(row, "Name_Hi"),
//                        Name_Ur = HandleDBNull<string>(row, "Name_Ur"),
//                        DateCreated = HandleDBNull<DateTime>(row, "DateCreated"),
//                        CreatedBy = HandleDBNull<Guid>(row, "CreatedBy")
//                    });
//                }
//            }
//            return mod;
//        }

//        public List<CollectionSeriesModel> GetCollectionSeriesList(Guid id)
//        {
//            var mod = new List<CollectionSeriesModel>();
//            List<DbParameter> parameterList = new List<DbParameter>();

//            DbParameter obj = NewParameter();
//            obj.ParameterName = "@Id";
//            obj.DbType = DbType.Guid;
//            obj.Value = id;
//            parameterList.Add(obj);

//            DataSet ds = ExecuteQuery("WP40_GetSearchCollectionSeriesList", parameterList.ToArray());
//            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                foreach (DataRow row in ds.Tables[0].Rows)
//                {
//                    mod.Add(new CollectionSeriesModel
//                    {
//                        Id = HandleDBNull<Guid>(row, "Id"),
//                        Name_En = HandleDBNull<string>(row, "Name_En"),
//                        Name_Hi = HandleDBNull<string>(row, "Name_Hi"),
//                        Name_Ur = HandleDBNull<string>(row, "Name_Ur")
//                    });
//                }
//            }
//            return mod;
//        }
//        #endregion

//        #region
//        public string GenratedSlug(string ContentName)
//        {
//            try
//            {
//                string slug = string.Empty;
//                List<DbParameter> parameterList = new List<DbParameter>();

//                // Create a new parameter
//                DbParameter obj1 = NewParameter();
//                obj1.ParameterName = "@Slug";
//                obj1.DbType = DbType.String;
//                obj1.Value = ContentName;
//                parameterList.Add(obj1);

//                DataSet ds = ExecuteQuery("sp_GetVideoSlug", parameterList.ToArray());

//                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//                {
//                    DataRow row = ds.Tables[0].Rows[0]; // Get first row
//                    slug = HandleDBNull<string>(row, "Seo_Slug"); // Ensure correct column name
//                }

//                return slug;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error in GenratedSlug: {ex.Message}");
//                return string.Empty;
//            }
//        }
//        #endregion

//    }
//}
