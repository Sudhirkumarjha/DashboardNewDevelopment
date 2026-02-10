using Microsoft.AspNetCore.Identity.UI.Services;
using VideoAssetManager.Business;
using VideoAssetManager.CommonUtils.Configuration;
using VideoAssetManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoAssetManager.DataAccess.Common
{
    public class MailUtility
    {
        public static List<AutoMailer> mobjAutoMailer;
        private static List<AutoMailerParameters> mobjParameters;
     

        public MailUtility()
        {        
            LoadAutoMailerConfiguration();
        }

        /// <summary>
        /// This method is used to load the configuration of auto mailers for all the corporates.
        /// </summary>
        public static void LoadAutoMailerConfiguration()
        {
            Task.Run(() =>
            {
                //LogicMailUtility objLogic = new LogicMailUtility();
                //mobjAutoMailer = objLogic.GetAutoMailerConfiguration();
                //mobjParameters = objLogic.GetAutoMailerParameters();
            });
        }
        public  async void DoSendNotifications( string autoMailerCode,  string userCode, string userName, string userEmail, string userMobile, string parameter1, string parameter2, string parameter3, string parameter4, string parameter5, string parameter6, string parameter7, string parameter8, string parameter9, string parameter10, string parameter11, string parameter12)
        {
            LogicMailUtility objLogic = new LogicMailUtility();
             mobjAutoMailer = objLogic.GetAutoMailerConfiguration();
            mobjParameters = objLogic.GetAutoMailerParameters();

            //Mail Notification      
            await  DoSendMailNotificationsAsync( autoMailerCode,  userCode, userName, userEmail, userMobile, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12);

        }
        public  async Task DoSendMailNotificationsAsync( string autoMailerCode,  string userCode, string userName, string userEmail, string userMobile, string parameter1, string parameter2, string parameter3, string parameter4, string parameter5, string parameter6, string parameter7, string parameter8, string parameter9, string parameter10, string parameter11, string parameter12)
        {

            string strUserName = userName;
            string strEmailId = userEmail;
            
                var objDictionaryMailParameters = new StringDictionary();
                
                if (autoMailerCode == "CRS_LNCH")
                {

                    objDictionaryMailParameters.Add("USR_NM", parameter1);
                    objDictionaryMailParameters.Add("CRS_NM", parameter2);
                    objDictionaryMailParameters.Add("CRS_DSCRPTN", parameter3);
                    objDictionaryMailParameters.Add("CRS_URL", parameter4);
                }
            
                if (autoMailerCode == "CRS_ENRLL")
                {

                    objDictionaryMailParameters.Add("USR_NM", parameter1);
                    objDictionaryMailParameters.Add("CRS_NM", parameter2);
                    objDictionaryMailParameters.Add("CRS_DSCRPTN", parameter3);
                    objDictionaryMailParameters.Add("CRS_IMG", parameter4);
                }

                if (autoMailerCode == "CRS_BundleEnrll")
                {

                    objDictionaryMailParameters.Add("USR_NM", parameter1);
                    objDictionaryMailParameters.Add("CRS_NM", parameter2);
                    objDictionaryMailParameters.Add("CRS_DSCRPTN", parameter3);
                    objDictionaryMailParameters.Add("CRS_IMG", parameter4);
                }
            await PrepareEmailMessageAsync(autoMailerCode, strUserName, strEmailId, objDictionaryMailParameters);
                objDictionaryMailParameters = null;
         
        }
        
        public  async Task<EmailMessage> PrepareEmailMessageAsync(string autoMailerCode, string receiverName, string receiverEmail, StringDictionary autoMailerParameters)
        {
            EmailSender emailSender = new EmailSender();
             EmailMessage message = null;
            if (!string.IsNullOrEmpty(receiverEmail))
            {
             
                string strAutoMailerBody = string.Empty;
             
                    AutoMailer autoMailer = mobjAutoMailer.Find(x => x.AutoMailerCode == autoMailerCode);

                    string[] strParameters = mobjParameters.Find(x => x.AutoMailerCode == autoMailerCode).Parameters;

                    if (strParameters != null && autoMailer != null )
                    {
                   
                        message = new EmailMessage();
                        message.From = new MailRecipient { Name = AppConfig.SmtpConfig.FromName, Email = AppConfig.SmtpConfig.FromAddress };
                        message.To = new List<MailRecipient>() { new MailRecipient { Name = receiverName, Email = receiverEmail } };

                        message.Subject = autoMailer.AutoMailerSubject;
                        strAutoMailerBody = autoMailer.AutoMailerBody;

                        int nLoopCounter = strParameters.Length;

                        for (int i = 0; i < nLoopCounter; i++)
                        {
                            var strTemp = autoMailerParameters[strParameters[i]];
                            strAutoMailerBody = strAutoMailerBody.Replace($"<<<{i + 1}>>>", strTemp);
                        }
                        message.Body = $"{strAutoMailerBody} <br /><br />";

                        List<MailRecipient> CClist = new List<MailRecipient>();
                        List<MailRecipient> BCClist = new List<MailRecipient>();
                        if (autoMailer.CcAdmin == true)
                        {
                            CClist.Add(new MailRecipient() { Name = AppConfig.SmtpConfig.FromName, Email = AppConfig.SmtpConfig.FromAddress });
                        }
                        else if (autoMailer.BccAdmin == true)
                        {
                            BCClist.Add(new MailRecipient() { Name = AppConfig.SmtpConfig.FromName, Email = AppConfig.SmtpConfig.FromAddress });
                        }

                        if (!string.IsNullOrEmpty(autoMailer.CcList))
                        {
                            var CCusers = autoMailer.CcList.Split(';').Where(x => !string.IsNullOrEmpty(x.Trim()));

                            if (CCusers.Any())
                                CClist.AddRange(CCusers.Select(user => new MailRecipient() { Email = user }));
                        }

                        if (!string.IsNullOrEmpty(autoMailer.BccList))
                        {
                            var BCCusers = autoMailer.BccList.Split(';').Where(x => !string.IsNullOrEmpty(x.Trim()));
                            if (BCCusers.Any())
                                BCClist.AddRange(BCCusers.Select(user => new MailRecipient() { Email = user }));
                        }

                        if (CClist.Any())
                            message.CopyTo = CClist;
                        if (BCClist.Any())
                            message.BlindCopyTo = BCClist;


                    await emailSender.SendEmailAsync(message);

                }
                
            }
            return message;
        }


       
            public static string GetCityNameById(int cityId, List<VM_City> cities)
            {
                return cities.FirstOrDefault(c => c.CityId == cityId)?.City ?? "Unknown";
            }
        

    }
}