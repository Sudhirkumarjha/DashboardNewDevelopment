using System;
using System.Collections.Generic;

namespace VideoAssetManager.CommonUtils.Zoom
{
    public class ZoomMeetingDetail
    {
        public string agenda { get; set; }
        public DateTime created_at { get; set; }
        public int duration { get; set; }
        public string host_id { get; set; }
        public string id { get; set; }
        public string join_url { get; set; }
        public Settings settings { get; set; }
        public string start_time { get; set; }
        public string start_url { get; set; }
        public string status { get; set; }
        public string timezone { get; set; }
        public string topic { get; set; }
        public int type { get; set; }
        public string uuid { get; set; }
        public string action { get; set; }
    }

    public class Settings
    {
        public string alternative_hosts { get; set; }
        public int approval_type { get; set; }
        public string audio { get; set; }
        public string auto_recording { get; set; }
        public bool close_registration { get; set; }
        public bool cn_meeting { get; set; }
        public string contact_email { get; set; }
        public string contact_name { get; set; }
        public bool enforce_login { get; set; }
        public string enforce_login_domains { get; set; }
        public List<string> global_dial_in_countries { get; set; }
        public bool host_video { get; set; }
        public bool in_meeting { get; set; }
        public bool join_before_host { get; set; }
        public bool mute_upon_entry { get; set; }
        public bool participant_video { get; set; }
        public bool registrants_confirmation_email { get; set; }
        public bool use_pmi { get; set; }
        public bool waiting_room { get; set; }
        public bool watermark { get; set; }
        public int registration_type { get; set; }
    }

    [Serializable]
    public class MeetingToolException : Exception
    {
        public MeetingToolException(string message): base(message)
        {
        }

        public MeetingToolException(string message, Exception innerException): base(message, innerException)
        {

        }

        public int StatusCode { get; set; }
    }
}
