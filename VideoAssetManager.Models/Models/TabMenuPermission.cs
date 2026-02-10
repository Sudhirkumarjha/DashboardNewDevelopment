using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VideoAssetManager.Models
{
    public class TabMenuPermission
    {
        [Key]
        public int TabPermissionId { get; set; }
        public string RoleId { get; set; }
        public int TabMenuId { get; set; }
        [NotMapped]
        public string RoleName { get; set; }
        [NotMapped]
        public string UserRoleId { get; set; }
        [NotMapped]
        public string TabMenuIds { get; set; }
    }
}