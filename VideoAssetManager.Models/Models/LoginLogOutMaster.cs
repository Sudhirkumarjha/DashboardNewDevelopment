using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoAssetManager.Models
{
    
    public class LoginLogOutMaster
    {
        [MaxLength(10)]
        public string HostName { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Key]
        public string SessionId { get; set; }

        public DateTime LoginDate { get; set; }
        public DateTime? LogOutDate { get; set; }
        public string IPAddress { get; set; }

        public bool AppLogin { get; set; }
        public string AppSessionId { get; set; }
        public string JWTToken { get; set; }

        
    }


}
