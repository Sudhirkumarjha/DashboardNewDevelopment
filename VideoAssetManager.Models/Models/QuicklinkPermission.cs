using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VideoAssetManager.Models
{
    public class QuicklinkPermission
    {
        [Key]
        public int PermissionId { get; set; }
        public string RoleId { get; set; }
       
        public int QuicklinkId { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "This field is required.")]
        public string RoleName { get; set; }
        [NotMapped]
        public string UserRoleId { get; set; }
        [NotMapped]
        public string QuicklinkIds { get; set; }
        [NotMapped]
        public string TabMenuIds { get; set; }
    }
}
