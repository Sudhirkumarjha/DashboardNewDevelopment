using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VideoAssetManager.Models
{
    public class QuickLinkMaster
    {
        public string HostName { get; set; }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int QuickLinkId { get; set; }
        public int QuickLinkParentId { get; set; }
        public string QuickLinkName { get; set; }
        public string Type { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public int OrderNo { get; set; }
        public Boolean IsDeleted { get; set; }
    }

    public class QuickLinkPermission
    {
        public int QuickLinkId { get; set; }
        public int QuickLinkParentId { get; set; }
        public string QuickLinkName { get; set; }
        public string Type { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public int OrderNo { get; set; }
    }
    
}
