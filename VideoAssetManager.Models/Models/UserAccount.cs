using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace VideoAssetManager.Models
{
    public class UserMaster : IdentityUser
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
    public class UserDetail
    {
        [MaxLength(10)]
        public string HostName { get; set; }
        [Key]
        [Required]
        public int UserId { get; set; }
        public string Id { get; set; }

        public string Name { get; set; }

        [MaxLength(60)]
        public string Email { get; set; }

        public DateTime DOB { get; set; }

        public string Occupation { get; set; }

        [MaxLength(15)]
        public string MobileNumber { get; set; }

        [MaxLength(250)]
        public string? StreetAddress { get; set; }
        [MaxLength(100)]
        public string? City { get; set; }
        [MaxLength(50)]
        public string? State { get; set; }
        [MaxLength(10)]
        public string? PostalCode { get; set; }
        [MaxLength(4)]
        public string CountryCode { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool Active { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime ModifiedOn { get; set; }
        [MaxLength(1)]
        public string UserType { get; set; }

        [NotMapped]
        public string RoleName { get; set; }
    }
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class UserLoginResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Id { get; set; }
    }

    public class LoginRootResponse
    {
        public int statusCode { get; set; }
        public bool isSuccess { get; set; }
        public List<object> errorMessages { get; set; }
        public UserLoginResponse result { get; set; }
    }

    public class search
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }

    public class AdminUsersFromMaster
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }

    public class UserRoles
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
    public class GetQuickLinks
    {
        [Key]
        public int QuickLinkId { get; set; }
        public int QuickLinkParentId { get; set; }
        public string QuickLinkName { get; set; }
    }
    public partial class Registration
    {
        [Key]
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public char PaymentStatus { get; set; }
        public decimal Amount { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime PaymentDate { get; set; }
    }
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class UserConstants
    {
        public static List<UserLogin> Users = new List<UserLogin>
            {
                    new UserLogin(){ UserName="rekhta",Password="!R@e#k$h%t^a&",Role="Admin"}
            };


    }


    #region
    public class AspNetRoles
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }

    public class AspNetUserRoles
    {
        [Key]
        public string UserId { get; set; }

        public string RoleId { get; set; }
    }
    #endregion



    public class SP_GetUserRole
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }

}
