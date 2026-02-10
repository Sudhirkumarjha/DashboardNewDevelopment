using System;
using System.Collections.Generic;

namespace VideoAssetManager.CommonUtils.Zoom
{
    public class ZoomMeetingUserList
    {
        public string next_page_token { get; set; }
        public int page_count { get; set; }
        public int page_size { get; set; }
        public List<Participant> participants { get; set; }
        public int total_records { get; set; }
        public class Participant
        {
            public string attentiveness_score { get; set; }
            public int duration { get; set; }
            public string id { get; set; }
            public DateTime join_time { get; set; }
            public DateTime leave_time { get; set; }
            public string name { get; set; }
            public string user_email { get; set; }
            public string user_id { get; set; }
        }

     

    }

 
}
