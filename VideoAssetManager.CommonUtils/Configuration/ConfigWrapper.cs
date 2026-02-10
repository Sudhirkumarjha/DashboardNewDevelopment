using Microsoft.Extensions.Configuration;
using System;

namespace VideoAssetManager.CommonUtils.Configuration
{
    
    public class AppConfig
    {
        public WCRFolderRelativePaths WCRFolderRelativePaths { get; set; }
        public static SmtpConfig SmtpConfig { get; set; }
        public URLPaths URLPaths { get; set; }
        public ZoomApi ZoomApi { get; set; }

        public SqlConnectionStrings SqlConnectionStrings { get; set; }

        public  RazorPayApiKeys RazorPayApiKeys { get; set; }

        public EventEdition EventEdition { get; set; }
        public AppSettings AppSettings { get; set; }


    }

    public class SqlConnectionStrings
    {
        public string DefaultConnection { get; set; }

    }

    public class EventEdition
    { 
    public string Edition { get; set; }
    }

    public class AppSettings
    {
        public string RawFootagePath { get; set; }
    }
    public class WCRFolderRelativePaths
    {
        /// <summary>
        /// WCRTemp folder relative path
        /// </summary>
        public string WCRTempPath { get; set; }

        public string WCRUserInfo { get; set; }
        public string WCRUserImage { get; set; }
        /// <summary>
        /// WCRContentDirectory folder relative path
        /// </summary>
        public string WCRContentPath { get; set; }
        public string WCRUpcomingCoursesImage { get; set; }
        public string WCRUpcomingCoursesImageMobile { get; set; }
        /// <summary>
        /// WCRVideoLibrary folder relative path
        /// </summary>
        public string WCRVideoAssetsPath { get; set; }
        public string WCRPath { get; set; }
        public string WCRCourseAssetsPath { get; set; }
        public string WCRBannerImagesPath { get; set; }
        public string WCRSliderImagesPath { get; set; }
        public string WCRPartnerAndSponserImagesPath { get; set; }
        public string WCRBannerImagesPathMobile { get; set; }
        public string WCRSliderImagesPathMobile { get; set; }
        public string WCRPartnerAndSponserImagesPathMobile { get; set; }

        public string WCRHighlightsImagesPath { get; set; }
        public string WCRHighlightsImagesPathMobile { get; set; }
        public string WCRHighlightsBannerImagesPath { get; set; }
        public string WCRHighlightsBannerImagesPathMobile { get; set; }

        public string WCRCourseAssetsPathMobile { get; set; }
        public string WCRResourceFilesPath { get; set; }
        public string WCROurValuesImagesPath { get; set; }
        public string WCRManageCreditsImagesPath { get; set; }
        public string fonts { get; set; }
        public string Certificate { get; set; }
        public string WCRDetailThumbnailPath { get; set; }
        public string WCRDetailThumbnailPathMobile { get; set; }
        public string WCRFeatureCourseThumbnailPath { get; set; }
        public string WCRFeatureCourseThumbnailPathMobile { get; set; }
        public string WCRBundleCourseImagePath { get; set; }
        public string CertificateImage { get; set; }
        public string CertificatesPath { get; set; }
        public string videosThumbnail { get; set; }
        public string FeedImage { get; set; }
        public string FeedBannerImage { get; set; }
        public string Kids_Vocab { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string BackgroundImage { get; set; }
        public string BackgroundSound { get; set; }
        public string Sound_En { get; set; }
        public string Sound_Gj { get; set; }
        public string WCRImageFilesPath { get; set; }

    }
    public class SmtpConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string FromAddress { get; set; }
        public string FromName { get; set; }
    }
    public class ZoomApi
    {
        //public string  Key { get; set; }
        //public string SecretKe { get; set; }
        //public string ApiUrl { get; set; }
        public string AccountId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ApiUrl { get; set; }
        public string HostId { get; set; }
    }
    public class ZoomHosts
    {   
        public string Email { get; set; }
        public string Password { get; set; }
        public string HostId { get; set; }       
    }

    public class URLPaths
    {
        public string ApplicationAdminPath { get; set; }
        public string WCRWebPath { get; set; }
        public string ImagesWebPath { get; set; }
        public string ServiceApiUrl { get; set; }
    }

    public class RazorPayApiKeys
    {
        public string Sandbox_Key_Id { get; set; }
        public string Sandbox_Key_Secret { get; set; }
        public string Key_Id { get; set; }
        public string Key_Secret { get; set; }
    }

}





