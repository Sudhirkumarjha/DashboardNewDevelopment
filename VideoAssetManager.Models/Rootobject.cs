using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAssetManager.Models
{
    //public class Rootobject
    //{
    //    public List<Rules> rules { get; set; }
    //}
    //public class Rules
    //{
    //    public string field { get; set; }
    //    public string data { get; set; }
    //    public string op { get; set; }
    //}

    public class Rootobject
    {
        public string groupOp { get; set; }
        public List<Rules> rules { get; set; }
    }

    public class Rules
    {
        public string field { get; set; }
        public string op { get; set; }
        public string data { get; set; }
    }

}