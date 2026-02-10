using VideoAssetManager.Models;
using VideoAssetManager.CommonUtils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using VideoAssetManager.DataAccess.Repository.IRepository;

namespace VideoAssetManager.DataAccess.DBUP
{
    public class DBUP : IDBUP
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly VideoAssetManagerDBContext _VideoAssetManagerDBContext;


        public DBUP(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            VideoAssetManagerDBContext VideoAssetManagerDBContext
                )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _VideoAssetManagerDBContext = VideoAssetManagerDBContext;
            
        }


        public void Initialize()
        {
            //migrations if they are not applied
            try
            {
                if (_VideoAssetManagerDBContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _VideoAssetManagerDBContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(RekhtaUtility.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(RekhtaUtility.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RekhtaUtility.Role_Learner)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RekhtaUtility.Role_User_Indi)).GetAwaiter().GetResult();                

                //if roles are not created, then we will create admin user as well

                _userManager.CreateAsync(new UserMaster
                {

                    UserName = "admin@VideoAssetManager.com",
                    Email = "admin@VideoAssetManager.com",
                    Name = "Super Admin",
                    PhoneNumber = "1112223333"
                   
                }, "Pass@123").GetAwaiter().GetResult();

                UserMaster user = _VideoAssetManagerDBContext.UserMasters.FirstOrDefault(u => u.Email == "admin@VideoAssetManager.com");
                UserDetail _userDetail = new UserDetail();


                _userDetail.Id = user.Id;
                _userDetail.Email = "admin@VideoAssetManager.com";
                _userDetail.Occupation = "Gov Job";
                _userDetail.MobileNumber = "2222000000";
                _userDetail.IsDeleted = false;
                _userDetail.Active = true;
                _userDetail.CountryCode = "+91";
                _userDetail.StreetAddress = "B 37";
                _userDetail.State = "UP";
                _userDetail.PostalCode = "201301";
                _userDetail.City = "Noida";
                _userDetail.HostName = "rekhta";
                _userDetail.UserType = "N";
                _userDetail.DOB = DateTime.ParseExact("01/01/2023", "MM/dd/yyyy", CultureInfo.InvariantCulture);
                _userDetail.CreatedOn = DateTime.UtcNow;
                _userDetail.ModifiedOn = DateTime.UtcNow;



                _VideoAssetManagerDBContext.UserDetail.Add(_userDetail);               

                _userManager.AddToRoleAsync(user, RekhtaUtility.Role_Admin).GetAwaiter().GetResult();

            }
            return;
        }
    }
}
