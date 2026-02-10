using Microsoft.Extensions.Configuration;
using RekhtaMedia.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace RekhtaMedia.DataAccess.Data
{
   public static class RekhtaMediaAdo
    {
        public static string ConnectionString
        {
            get
            {
                return GetConnectionString();
            }
        }
        private static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();

            return config.GetValue<string>("ConnectionStrings:DefaultConnection");
        }
        public static T HandleDBNull<T>(SqlDataReader reader, string key)
        {
            var v = reader[key];
            if (v == DBNull.Value || v == null)
            {
                return default(T);
            }
            return (T)v;
        }
     
        public static List<RM_GetHomeContent> GetHomeContent(int lang,string host)
        {
            var mod =new  List<RM_GetHomeContent>();
            var mod1 = new List<RM_HomeVideo>();
            
            var mod2 = new List<RM_HomeTags>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RM_GetHomeContent", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@lang", lang));
                    cmd.Parameters.Add(new SqlParameter("@host", host));

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 180000;
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mod.Add(new RM_GetHomeContent {
                               CollectionId = HandleDBNull<System.Guid>(reader, "CollectionId"),
                                CollectionName =HandleDBNull<string>(reader, "CollectionName"),
                                Host = HandleDBNull<string>(reader, "Host"),
                                CategoryName = HandleDBNull<string>(reader, "CategoryName"),
                                CollectionIndex = HandleDBNull<int>(reader, "CollectionIndex"),
                            });
                        }


                        reader.NextResult();
                        while (reader.Read())
                        {
                            mod1.Add(new RM_HomeVideo
                            {
                                CollectionId = HandleDBNull<System.Guid>(reader, "CollectionId"),
                                ContentId = HandleDBNull<Nullable<System.Guid>>(reader, "ContentId"),
                                Title = HandleDBNull<string>(reader, "Title"),
                                Description = HandleDBNull<string>(reader, "Description"),
                                VideoId = HandleDBNull<System.Guid>(reader, "VideoId"),
                                VideoThumbnail = HandleDBNull<string>(reader, "VideoThumbnail"),
                                Duration = HandleDBNull<int>(reader, "Duration"),
                                RecordedDate = HandleDBNull<DateTime>(reader, "Recordeddate"),
                                PublishDate = HandleDBNull<DateTime>(reader, "PublishDate"),
                                seq = HandleDBNull<int>(reader, "seq"),
                                Source = HandleDBNull<string>(reader, "SourceName"),
                            });
                        }
                        reader.NextResult();
                        while (reader.Read())
                        {
                            mod2.Add(new RM_HomeTags
                            {
                                TopicId = HandleDBNull<System.Guid>(reader, "TopicId"),
                                ContentId = HandleDBNull<System.Guid>(reader, "ContentId"),
                                TopicName = HandleDBNull<string>(reader, "TopicName"),
                                HostName = HandleDBNull<string>(reader, "HostName"),
                                Description = HandleDBNull<System.String>(reader, "Description")
                              
                            });
                        }
                    }
                    con.Close();
                }
            }


            foreach (var coll in mod)
            {
                coll.VideoList = mod1.Where(x=>x.CollectionId==coll.CollectionId).OrderBy(x=>x.seq).ToList();
               
            }


            return mod;
        }

        public static VideoDetail GetHomeVideoDetails(Guid VideoId,Guid CollectionId, Guid UserId, int lang)
        {
            var mod = new VideoDetail();
            mod.keepWatching = new List<RM_HomeVideo>();

            var mod2 = new List<RM_HomeTags>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RM_GetVideoDetails", con))
                {

                    cmd.Parameters.Add(new SqlParameter("@VideoId", VideoId));
                    cmd.Parameters.Add(new SqlParameter("@CollectionId", CollectionId));
                    cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                    cmd.Parameters.Add(new SqlParameter("@lang", lang));
                  

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 180000;
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mod.CollectionId = HandleDBNull<System.Guid>(reader, "CollectionId");
                            mod.ContentId = HandleDBNull<Nullable<System.Guid>>(reader, "Id");
                            mod.Title = HandleDBNull<string>(reader, "Title");
                            mod.Description = HandleDBNull<string>(reader, "Description");
                            mod.VideoId = HandleDBNull<System.Guid>(reader, "VideoId");
                            mod.VideoThumbnail = HandleDBNull<string>(reader, "VideoThumbnail");
                            mod.Duration = HandleDBNull<int>(reader, "Duration");
                            mod.RecordedDate = HandleDBNull<DateTime>(reader, "Recordeddate");
                            //mod.PublishDate = HandleDBNull<DateTime>(reader, "PublishDate"); 
                            mod.seq = HandleDBNull<int>(reader, "seq");
                            mod.Source = HandleDBNull<string>(reader, "SourceName");
                            mod.IsStarted = HandleDBNull<bool>(reader, "IsStarted");
                            mod.TimeSpent =  HandleDBNull<DateTime>(reader, "TimeSpent");
                            mod.IsSave = HandleDBNull<bool>(reader, "IsSave");
                            mod.LikeCount = HandleDBNull<int>(reader, "LikeCount");

                        }


                        reader.NextResult();
                        while (reader.Read())
                        {
                            mod.keepWatching.Add(new RM_HomeVideo
                            {
                                CollectionId = HandleDBNull<System.Guid>(reader, "CollectionId"),
                                ContentId = HandleDBNull<Nullable<System.Guid>>(reader, "Id"),
                                Title = HandleDBNull<string>(reader, "Title"),
                                Description = HandleDBNull<string>(reader, "Description"),
                                VideoId = HandleDBNull<System.Guid>(reader, "VideoId"),
                                VideoThumbnail = HandleDBNull<string>(reader, "VideoThumbnail"),
                                Duration = HandleDBNull<int>(reader, "Duration"),
                                RecordedDate = HandleDBNull<DateTime>(reader, "Recordeddate"),
                                //PublishDate = HandleDBNull<DateTime>(reader, "PublishDate"),
                                seq = HandleDBNull<int>(reader, "seq"),
                                Source = HandleDBNull<string>(reader, "SourceName"),
                                IsStarted =  HandleDBNull<bool>(reader, "IsStarted"),
                                TimeSpent = HandleDBNull<DateTime>(reader, "TimeSpent"),
                            IsSave = HandleDBNull<bool>(reader, "IsSave"),
                            LikeCount = HandleDBNull<int>(reader, "LikeCount"),
                        });
                        }
                        //reader.NextResult();
                        //while (reader.Read())
                        //{
                        //    mod2.Add(new RM_HomeTags
                        //    {
                        //        TopicId = HandleDBNull<System.Guid>(reader, "TopicId"),
                        //        ContentId = HandleDBNull<System.Guid>(reader, "ContentId"),
                        //        TopicName = HandleDBNull<string>(reader, "TopicName"),
                        //        HostName = HandleDBNull<string>(reader, "HostName"),
                        //        Description = HandleDBNull<System.String>(reader, "Description")

                        //    });
                        //}
                    }
                    con.Close();
                }
            }



            return mod;
        }
    }
}
