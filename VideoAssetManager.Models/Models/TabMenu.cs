using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VideoAssetManager.Models
{
    public class TabMenu
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int  MenuId { get; set; }
        public string MenuName { get; set; }
        public string ClassName { get; set; }
        public string area { get; set; }
        public string action { get; set; }
        public string clickEvent { get; set; }
        public bool Active { get; set; }
        public bool IsSoftDelete { get; set; }
        public string TabdivId { get; set; }
        public int SerialNo { get; set; }
        public int QuickLinkId { get; set; }
    }
    public class Sp_GetTabMenu
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string ClassName { get; set; }
        public string area { get; set; }
        public string action { get; set; }
        public string clickEvent { get; set; }
        public bool Active { get; set; }
        public bool IsSoftDelete { get; set; }
        public string TabdivId { get; set; }
        public int SerialNo { get; set; }
    }
    public class Sp_GetTabMenuPermission
    {
        [Key]
        public int RowNumber { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string area { get; set; }
        public string action { get; set; }
        public bool Active { get; set; }
        public bool IsSoftDelete { get; set; }
        public string TabdivId { get; set; }
        public int SerialNo { get; set; }
        public string RoleId { get; set; }
        public string value { get; set; }
        
    }
}
