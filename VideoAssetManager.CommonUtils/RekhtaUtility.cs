using CoreHtmlToImage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VideoAssetManager.CommonUtils
{
    public static class RekhtaUtility
    {
        public const string Role_User_Indi = "Individual";
        public const string Role_Admin = "ADMIN";
        public const string Role_Learner = "Learner";

        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";
        public const string PaymentStatusRejected = "Rejected";

        public static string RoleName { get; set; }
        public static int QuestionLevelId { get; set; }
        public static decimal QuestionLevelMarks { get; set; }
        public static string QuestionLevelName { get; set; }

        public static Guid Id { get; set; }
        public static string Name { get; set; }

        /// <summary>
        /// To check directory is exist or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public static void DirectorCreateIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        /// <summary>
        /// GetFiles method is used to retrieve a list of files within  a directory.
        /// Example usage (retrieves a list of all the files within a directory): GetFiles(@"C:\SomeDirectory", "*.*", true);
        /// </summary>
        /// <param name="path">Directory for which list of files is required.</param>
        /// <param name="filter">Filter to be applied when retrieving list of files. Use *.* in case of all files.</param>
        /// <param name="nameOnly">Specify as true to retrieve only names of files. Specify false to retrieve complete names (which includes path) of files.</param>
        /// <returns>String array holding list of files.</returns>
        public static string[] GetFiles(string path, string filter, bool nameOnly)
        {
            var files = Directory.GetFiles(path, filter);
            int nLoopCounter;

            if (nameOnly)
            {
                //need to return only the names instead of full path...
                nLoopCounter = files.Length;

                for (var i = 0; i < nLoopCounter; i++)
                {
                    files[i] = GetFileName(files[i]);
                }
            }

            return files;
        }
        public const string FilePathSeparator = @"\";
        /// <summary>
        /// Separator for Day, Month and Year in a Date value.
        /// </summary>
        public static readonly string DatePartSeparator = "/";

        /// <summary>
        /// Separator for Hours, Minutes and Seconds in a Time value.
        /// </summary>
        public static readonly string TimePartSeparator = ":";


        /// <summary>
        /// GetFileName method is used to retrieve only the file name from complete file name (which contains path as well).
        /// Example usage: GetFileName(@"C:\SomeFolder\SomeSubFolder\SomeFile.aspx");
        /// The above mentioned function call will return a string value of "SomeFile.aspx"
        /// </summary>
        /// <param name="fileName">Complete file name of the file (which contains path as well).</param>
        /// <returns>File name from the complete file name (which aontains path as well).</returns>
        public static string GetFileName(string fileName)
        {
            int nIndexSlash;

            if (fileName != null && fileName.Length != 0)
            {
                nIndexSlash = fileName.LastIndexOf(FilePathSeparator);
                if (nIndexSlash != -1)
                {
                    return fileName.Substring(nIndexSlash + 1, fileName.Length - (nIndexSlash + 1));
                }
                else if (fileName.Length != 0)
                {
                    //returning the file name as is in case there are no file path
                    //separators found - happens in the case of mozilla.
                    return fileName;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
     
        /// <summary>
        /// EncodeXml is used to convert characters which can be part of XML content into escapse sequences.
        /// </summary>
        /// <param name="message">String message to be encoded.</param>
        /// <returns>Encoded string - which can be passed as XML content.</returns>
        public static string EncodeXml(string message)
        {
            message = message.Replace("&", "&amp;");
            message = message.Replace("<", "&lt;");
            message = message.Replace(">", "&gt;");
            message = message.Replace("\"", "&quot;");
            message = message.Replace("'", "&apos;");

            return message;
        }
        public static bool MoveFile(string sourceFile, string targetFile)
        {
            bool result;

            try
            {
                File.Move(sourceFile, targetFile);
                result = true;
            }
            catch (DirectoryNotFoundException)
            {
                result = false;
            }
            catch (FileNotFoundException)
            {
                result = false;
            }
            catch (PathTooLongException)
            {
                result = false;
            }
            catch (IOException)
            {
                result = false;
            }
            catch (ArgumentNullException)
            {
                result = false;
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (SecurityException)
            {
                result = false;
            }

            return result;
        }
        public static bool fileUpload(IFormFile? file, string tempUploads, string userUploads)
        {
            bool isFileSave = false; 
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);

                string fileName = Guid.NewGuid().ToString();
                var tempImage = Path.Combine(tempUploads, fileName + extension);
                var userImage = Path.Combine(userUploads, fileName + extension);

                using (var fileStreams = new FileStream(tempImage, FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                if (!RekhtaUtility.MoveFile(tempImage, userImage))
                {
                    //_logger.LogDebug("User {0} images not uploaded.");
                }
                isFileSave = true;
            }

            return isFileSave;
        }
        public static bool fileUpload(IFormFile? file, string tempUploads, string userUploads, string fileName)
        {
            bool isFileSave = false;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
            
                var tempImage = Path.Combine(tempUploads, fileName + extension);
                var userImage = Path.Combine(userUploads, fileName + extension);

                using (var fileStreams = new FileStream(tempImage, FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                if (!RekhtaUtility.MoveFile(tempImage, userImage))
                {
                    //_logger.LogDebug("User {0} images not uploaded.");
                }
                isFileSave = true;
            }

            return isFileSave;
        }
        public static string removeTags(string prmStr)
        {
            string returnString = string.Empty;
            if (!string.IsNullOrEmpty(prmStr))
            {
                System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
                returnString = rx.Replace(prmStr, "");
                returnString = returnString.Replace("&nbsp;", "");
            }


            return returnString;
        }
        public static string GetSlug(string strSlugName)
        {
            string Slug = string.Empty;
            bool hasSpace = strSlugName.Contains(" ");
            if (hasSpace == true)
            {
                Slug = strSlugName.Replace(" ", "-");
            }
            else
            {
                Slug = strSlugName;
            }

            return Slug;
        }
        public static string DecodeHtml(string str)
        {
          return  HttpUtility.HtmlDecode(str);
        }
        public static bool isNavigationShow(string controllerName, string ActionName)
        {
            bool isShow = false;
            if ((controllerName == "Program" || controllerName== "Course") && ActionName != "ProgramIndex")
            {
                isShow = true;
            }
            else if ((controllerName == "Content" || controllerName == "Quiz") && ActionName != "Index")
            {
                isShow = true;
            }
            else if (controllerName == "EventMasters")
            {
                isShow = true;
            }
            else if (controllerName == "Category")
            {
                isShow = true;
            }
            else if (controllerName == "VideoManagement")
            {
                isShow = true;
            }
            return isShow;
        }
        public static bool isActiveTab(string controllerName, string actionName)
        {
            bool isActive = false;
            if (controllerName == "Program" && (actionName == "ProgramIndex" || actionName == "LearningIndex" || actionName == "AddNode" || actionName == "SelectLearning"))
            {
                isActive = true;
            }
            else if (controllerName == "Course" && (actionName == "Index" || actionName != "Create"))
            {
                isActive = true;
            }
            else if (controllerName == "Program" && actionName == "Create" )
            {
                isActive = true;
            }
            else if (controllerName == "EventMasters")
            {
                isActive = true;
            }

            return isActive;
        }


        public static string ShowAlert(string messageType, string message)
        {
            string alertDiv = null;
            switch (messageType)
            {
                case "Success":
                    alertDiv = "<div class='alert alert-success alert-dismissable' id='alert'><button type='button' class='close' data-dismiss='alert'>×</button><strong> Success!</ strong > " + message + "</a>.</div>";
                    break;
                case "Danger":
                    alertDiv = "<div class='alert alert-danger alert-dismissible' id='alert'><button type='button' class='close' data-dismiss='alert'>×</button><strong> Error!</ strong > " + message + "</a>.</div>";
                    break;
                case "Info":
                    alertDiv = "<div class='alert alert-info alert-dismissable' id='alert'><button type='button' class='close' data-dismiss='alert'>×</button><strong> Info!</ strong > " + message + "</a>.</div>";
                    break;
                case "Warning":
                    alertDiv = "<div class='alert alert-warning alert-dismissable' id='alert'><button type='button' class='close' data-dismiss='alert'>×</button><strong> Warning!</strong> " + message + "</a>.</div>";
                    break;
            }
            return alertDiv;
        }
        public static List<SelectListItem> GetQuickLinkSelectItem(IDictionary<int, string> item, int Id = 0)
        {
            List<SelectListItem> contentitems = new List<SelectListItem>();

            contentitems.Add(new SelectListItem
            {
                Text = "Select Items",
                Value = "",
            });
            foreach (var content in item)
            {
                if (Id == content.Key)
                {
                    contentitems.Add(new SelectListItem
                    {
                        Text = content.Value,
                        Value = content.Key.ToString(),
                        Selected = true
                    });
                }
                else
                {
                    contentitems.Add(new SelectListItem
                    {
                        Text = content.Value,
                        Value = content.Key.ToString(),
                    });
                }

            }
            return contentitems;
        }
        public static List<SelectListItem> GetSelectItem(IDictionary<int, string> item, int Id = 0)
        {
            List<SelectListItem> contentitems = new List<SelectListItem>();

            contentitems.Add(new SelectListItem
            {
                Text = "Select Items",
                Value = "",
            });
            foreach (var content in item)
            {
                if (Id == content.Key)
                {
                    contentitems.Add(new SelectListItem
                    {
                        Text = content.Value,
                        Value = content.Key.ToString(),
                        Selected = true
                    });
                }
                else
                {
                    contentitems.Add(new SelectListItem
                    {
                        Text = content.Value,
                        Value = content.Key.ToString(),
                    });
                }

            }
            return contentitems;
        }
        public static List<SelectListItem> GetSelectItem(IDictionary<string, string> item, string Type = "")
        {
            List<SelectListItem> contentitems = new List<SelectListItem>();

            contentitems.Add(new SelectListItem
            {
                Text = "Select Items",
                Value = "",
            });
            foreach (var content in item)
            {
                if (Type == content.Value)
                {
                    contentitems.Add(new SelectListItem
                    {
                        Text = content.Value,
                        Value = content.Key.ToString(),
                        Selected = true
                    });
                }
                else
                {
                    contentitems.Add(new SelectListItem
                    {
                        Text = content.Value,
                        Value = content.Key.ToString(),
                    });
                }

            }
            return contentitems;
        }
        public static List<SelectListItem> GetSelectItem(IDictionary<Guid, string> item, Guid Id)
        {
            List<SelectListItem> contentitems = new List<SelectListItem>();
            contentitems.Add(new SelectListItem
            {
                Text = "Select Items",
                Value = "00000000-0000-0000-0000-000000000000",
            });
            foreach (var content in item)
            {
                if (Id == content.Key)
                {
                    contentitems.Add(new SelectListItem
                    {
                        Text = content.Value,
                        Value = content.Key.ToString(),
                        Selected = true
                    });
                }
                else
                {
                    contentitems.Add(new SelectListItem
                    {
                        Text = content.Value,
                        Value = content.Key.ToString(),
                    });
                }
            }
            return contentitems;
        }
        public static string RemoveFileExtension(string File)
        {
            if(!string.IsNullOrEmpty(File) && File.Contains("."))
            {
                string[] FileName = File.Split(".");
                return FileName[0].ToString();
            }
            else
            {
                return "";
            }
        }
        public static IDictionary<int, string> GetContentType(string pageType = "")
        {

            IDictionary<int, string> ContentType = new Dictionary<int, string>();
            if (pageType == "P")
            {
                ContentType.Add(1, "Video");
                ContentType.Add(2, "Quiz");
                ContentType.Add(3, "Resource File");
                ContentType.Add(4, "Live Class");
            }
            else
            {
                ContentType.Add(1, "Video");
                ContentType.Add(2, "Quiz");
                ContentType.Add(3, "Resource File");
            }
            return ContentType;
        }
        public static string GetNodeType(string ContenTypeId)
        {
            string NodeType = string.Empty;
            if (ContenTypeId == "4")
            {
                NodeType = ContenTypeId == "1" ? "V" : ContenTypeId == "2" ? "Q" : ContenTypeId == "3" ? "R" : ContenTypeId == "4" ? "E" : "N";
            }
            else
            {
                NodeType = ContenTypeId == "1" ? "V" : ContenTypeId == "2" ? "Q" : ContenTypeId == "3" ? "R" : "N";
            }

            return NodeType;
        }

        public static bool IsPermission(string action,string controller)
        {
          
            bool IsPermission = false;
            var linkPermission = RekhtaUtility.GetProperty.QuickLinkPermission.FirstOrDefault(a => a.Area == controller && a.Action == action);
            if(linkPermission!=null)
            {
                IsPermission = true;
            }

            return IsPermission;
        }
        public static bool IsTabPermission()
        {
            bool IsPermission = false;
            var linkPermission = RekhtaUtility.GetProperty.TabLinkPermission.FirstOrDefault(a => a.MenuId == VideoAssetManager.CommonUtils.RekhtaUtility.GetProperty.TabMenuId);
            if (linkPermission != null)
            {
                IsPermission = true;
            }


            return IsPermission;
        }
        public static string GetSubString(string prmstr, int strLength)
        {
            string retString = string.Empty;
            var wordlength = prmstr.Length;
            if (!string.IsNullOrEmpty(prmstr) && strLength <= wordlength)
                retString = prmstr.Substring(0, strLength)+"....";
            else
                retString = prmstr;

            return retString;
        }
        public static string RemoveTagsGetSubString(string prmstr, int strLength)
        {
            string returnString = string.Empty;
            if (!string.IsNullOrEmpty(prmstr))
            {
                System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
                returnString = rx.Replace(prmstr, "");
                returnString = returnString.Replace("&nbsp;", "");

                var wordlength = returnString.Length;
                if (!string.IsNullOrEmpty(returnString) && strLength <= wordlength)
                    returnString = returnString.Substring(0, strLength) + "....";
                return returnString;
            }
            else
            {
                return prmstr;
            }
           
        }
        public static decimal GetFinalpercent(decimal Price, decimal discountPrice)
        {
            decimal finalPrice = 0;
            decimal discount = 0;

            discount = (Price * discountPrice) / 100;

            finalPrice = (Price - discount);

            finalPrice = decimal.Truncate(finalPrice);

            return finalPrice;
        }
        public static string GenerateSerialNo(int certificatId, string CourseCode)
        {
            var Serial = string.Format("{0:0000}", certificatId);
            string SerialNo = "RL/" + CourseCode + "/" + Serial.ToString();
            return SerialNo.ToLower();
        }
        public static string GetFileName(int certificatId, string CourseCode)
        {
            var Serial = string.Format("{0:0000}", certificatId);
            string SerialNo = "RL-" + CourseCode + "-" + Serial.ToString();
            return SerialNo.ToLower();
        }
        public static string WriteImage(string Username, string SerialNo, string FileName, string CourseName, DateTime DateOfIssue, string CertificatesPath)
        {
            string cdate = DateOfIssue.ToString("d-MMM-yyyy");
            string imageFilePath = CertificatesPath + "Rekhta-Learning-Blank-Certificate.png";
            var ImagePath = CertificatesPath + FileName + ".jpg";
            var PdfFilePath = CertificatesPath + FileName + ".pdf";
            if (!File.Exists(PdfFilePath))
            {
                Bitmap bitmap = new Bitmap(imageFilePath);
                Graphics graphics = Graphics.FromImage(bitmap);

                // Define text color
                Brush brush = new SolidBrush(System.Drawing.Color.Black);
                // Define text font
                Font Style = new Font("product sans", 60, System.Drawing.FontStyle.Bold);
                Font Style1 = new Font("product sans", 35, System.Drawing.FontStyle.Bold);
                Font Style2 = new Font("product sans", 50, System.Drawing.FontStyle.Bold);

                // Define rectangle
                var NamePosition = (3168 / 2);

                var position = GetNamePosition(NamePosition, graphics, Style, Username);
                System.Drawing.Rectangle UserNameStyle = new System.Drawing.Rectangle(position, 1124, 2000, 100);
                System.Drawing.Rectangle dateStyle = new System.Drawing.Rectangle(1480, 1695, 450, 60);
                System.Drawing.Rectangle SerialNoStyle = new System.Drawing.Rectangle(2720, 150, 450, 80);
                System.Drawing.Rectangle CourseNamereStyle = new System.Drawing.Rectangle(900, 1480, 2000, 100);
                // Draw text on image
                graphics.DrawString(Username, Style, brush, UserNameStyle);
                graphics.DrawString(cdate, Style1, brush, dateStyle);
                graphics.DrawString(SerialNo, Style1, brush, SerialNoStyle);
                graphics.DrawString(CourseName, Style2, brush, CourseNamereStyle);
                // Save the output file
                bitmap.Save(CertificatesPath + FileName + ".jpg");


                ToPDF(ImagePath, CertificatesPath, FileName);

                Task.Delay(5000).Wait();
            }


            return PdfFilePath.ToString();
        }
        public static int GetNamePosition(int NamePosition, Graphics graphics, Font Style, string UserName)
        {
            int Position = 0;
            System.Drawing.SizeF sizeOfString = new System.Drawing.SizeF();
            sizeOfString = graphics.MeasureString(UserName, Style);
            var namewidth = sizeOfString.Width;
            var nPosition = (int)namewidth;
            nPosition = nPosition / 2;
            Position = (NamePosition - nPosition);

            return Position;

        }
        #region WhatsApp Service
        public static async Task<string> WhatsAppProcessing(Guid? userId, string eventCode, string whatsappNo, string templateName, string imageUrl, string[] finalAttri, string visitWebsite, bool isOnlyMessage = false, bool isDynamicAttribute = false, int lang = 1, string accountTypeName = "rf")
        {
            string result = string.Empty;
            string postbody = string.Empty;
            bool isDeliverd = false;
            string responseId = string.Empty;
            var responseMessage = string.Empty;
            try
            {
                string serverApiKey = string.Empty;
                string accountName = string.Empty;

                if (accountTypeName == "jer")
                {
                    serverApiKey = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJyZWtodGFmb3VuZGF0aW9ud2EiLCJleHAiOjI1MjY5NjUxNzd9.Zn-DW5qZ6JDejOpaYmhKmlkNWCoXHMmi8xfpgQgoenGV79WUhTNf8fpV5RoL3vCpTAJJ3LeEgMBz5RilGOAYTQ";
                    accountName = "a614f2b1a247c7745c05e656f7066181";
                }
                else
                {
                    /* anjas mahotsav */
                    serverApiKey = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJyZWtodGF3YSIsImV4cCI6MjUxNDYyNzY4NX0.PtKzUP6jCBsucYBy0Tniy63ciU-qS6i81zseLzFbT8ig_bgY_U1S4SzrnqGvGFoc7VBPYqnNXDtHSP0Jo9bfcw";
                    accountName = "7adea837863a09181fd757b2eb3e6a47";
                }

                if (!isOnlyMessage)
                {
                    if (isDynamicAttribute)
                    {
                        postbody = WhatsAppMessage.PayloadWithAttribute(whatsappNo, accountName, templateName, imageUrl, finalAttri, visitWebsite, lang);
                    }
                    else
                    {
                        postbody = WhatsAppMessage.PayloadWithoutAttribute(whatsappNo, accountName, templateName, imageUrl, lang);
                    }
                }
                else
                { postbody = WhatsAppMessage.PayloadWithMessageOnly(whatsappNo, accountName, templateName,  lang); }

                var response = WhatsAppMessage.WhatsAppService(whatsappNo, postbody, serverApiKey);

            }
            catch (Exception ex)
            {
                result = ex.InnerException.Message + " " + ex.Message + " Payload > " + postbody;
                responseMessage = result;
                responseId = "";
            }
            finally
            {
                /* whatsapp log */
                //if (whatsappNo.Contains("9887503020"))
                //{
                //var m = new ErrorMsgLog();
                //m.Id = Guid.NewGuid();
                //m.UserId = userId;
                //m.DateCreated = DateTime.Now;
                //m.UniqueReportId = responseId;

                //m.ErrorString = "whatsapp-response > " + responseMessage;
                //m.Phone = whatsappNo;
                //m.MsgSubmitted = isDeliverd;
                //m.RegistrationCode = eventCode;
                ////m.RegistrationCode = imageUrl;
                //_iwrapperRepository.ErrorMsgLog.Add(m);
                //_iwrapperRepository.Save();
                //}
            }

            return result;
        }
        #endregion


        public static async Task ToPDF(string ImagePath, string CertificatesPath, string FileName)
        {
            //string imageFileName; string pdfFileName; 
            string iPath = ImagePath;
            int width = 600;
            await Task.Run(() =>
            {
                using (var document = new PdfDocument())
                {
                    PdfPage page = document.AddPage();
                    using (XImage img = XImage.FromFile(ImagePath))
                    {
                        // Calculate new height to keep image ratio
                        var height = (int)(((double)width / (double)img.PixelWidth) * img.PixelHeight);

                        // Change PDF Page size to match image
                        page.Width = width;
                        page.Height = height;

                        XGraphics gfx = XGraphics.FromPdfPage(page);
                        gfx.DrawImage(img, 0, 0, width, height);
                    }
                    document.Save(CertificatesPath + FileName + ".pdf");
                }

                if (!Directory.Exists(ImagePath))
                {
                    File.Delete(ImagePath);
                }
            });

        }
        public static string UploadFiles(string FileUploadsPath, Guid FileId, IFormFile ThumbnailFile, string ImageName, string tempUploads, string extensionFor, ILogger _logger)
        {
            string fileName = string.Empty;
            string extension = string.Empty;
            bool isfile = false;
            extensionFor = extensionFor.ToLower();
            if (ThumbnailFile != null)
            {
                fileName = Path.GetFileName(ThumbnailFile.FileName);
                extension = Path.GetExtension(ThumbnailFile.FileName);
                isfile = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(ImageName))
                {
                    fileName = Path.GetFileName(ImageName);
                    string[] strExtension = ImageName.Split(".");
                    extension = strExtension[1];
                    isfile = false;
                }
            }

            string path = Path.Combine(FileUploadsPath, FileId.ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (isfile)
            {
                if (extensionFor.Contains(".pdf"))
                {
                    if (extension.ToLower() == ".pdf")
                    {

                        if (!string.IsNullOrEmpty(ImageName))
                        {
                            var oldfile = Path.GetFileName(ImageName);
                            var assetFileOld = Path.Combine(path, oldfile);
                            System.IO.File.Delete(assetFileOld);
                        }
                        extension = Path.GetExtension(ThumbnailFile.FileName);
                        var tempFile = Path.Combine(tempUploads, fileName);
                        var assetFile = Path.Combine(path, fileName);
                        using (var fileStreams = new FileStream(tempFile, FileMode.Create))
                        {
                            ThumbnailFile.CopyTo(fileStreams);
                        }
                        if (!RekhtaUtility.MoveFile(tempFile, assetFile))
                        {
                            _logger.LogDebug($"File Not uploaded {fileName} .");
                        }
                    }
                    else
                    {
                        fileName = "Extension not supported";
                    }
                }
                if (extensionFor.Contains(".jpg,.jpg,.png,.png"))
                {
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == "jpg" || extension.ToLower() == ".png" || extension.ToLower() == "png")
                    {

                        if (ImageName != null)
                        {
                            var oldfile = Path.GetFileName(ImageName);
                            var assetFileOld = Path.Combine(path, oldfile);
                            System.IO.File.Delete(assetFileOld);
                        }
                        extension = Path.GetExtension(ThumbnailFile.FileName);
                        var tempFile = Path.Combine(tempUploads, fileName);
                        var assetFile = Path.Combine(path, fileName);
                        using (var fileStreams = new FileStream(tempFile, FileMode.Create))
                        {
                            ThumbnailFile.CopyTo(fileStreams);
                        }
                        if (!RekhtaUtility.MoveFile(tempFile, assetFile))
                        {
                            _logger.LogDebug($"File Not uploaded {fileName} .");
                        }
                    }
                    else
                    {
                        fileName = "Extension not supported";
                    }
                }
                if (extensionFor.Contains(".mp4,mp4"))
                {
                    if (extension.ToLower() == ".mp4" || extension.ToLower() == "mp4")
                    {

                        if (!string.IsNullOrEmpty(ImageName))
                        {
                            var oldfile = Path.GetFileName(ImageName);
                            var assetFileOld = Path.Combine(path, oldfile);
                            System.IO.File.Delete(assetFileOld);
                        }
                        extension = Path.GetExtension(ThumbnailFile.FileName);
                        var tempFile = Path.Combine(tempUploads, fileName);
                        var assetFile = Path.Combine(path, fileName);
                        using (var fileStreams = new FileStream(tempFile, FileMode.Create))
                        {
                            ThumbnailFile.CopyTo(fileStreams);
                        }
                        if (!RekhtaUtility.MoveFile(tempFile, assetFile))
                        {
                            _logger.LogDebug($"File Not uploaded {fileName} .");
                        }
                    }
                    else
                    {
                        fileName = "Extension not supported";
                    }
                }


            }
            return fileName;
        }

        public static string UploadWithIds(string FileUploadsPath, Guid videoGUID, IFormFile ThumbnailFile, string ImageName, string tempUploads, string extensionFor, ILogger _logger)
        {
            string fileName = string.Empty;
            string extension = string.Empty; 
            bool isfile = false;
            extensionFor = extensionFor.ToLower();
            if (ThumbnailFile != null)
            {
                fileName = Path.GetFileName(ThumbnailFile.FileName);
                extension = Path.GetExtension(ThumbnailFile.FileName);
                isfile = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(ImageName))
                {
                    fileName = Path.GetFileName(ImageName);
                    string[] strExtension = ImageName.Split(".");
                    extension = strExtension[1];
                    isfile = false;
                }
            }
            //string path = Path.Combine(FileUploadsPath, videoGUID.ToString());
            string path = Path.Combine(FileUploadsPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (isfile)
            {
                if (extensionFor.Contains(".mp4,mp4"))
                {
                    if (extension.ToLower() == ".mp4" || extension.ToLower() == "mp4")
                    {
                        fileName = $"{videoGUID}{ extension}";
                        if (!string.IsNullOrEmpty(fileName))
                        {                           
                            var oldfile = Path.GetFileName(fileName);
                            var assetFileOld = Path.Combine(path, oldfile);
                            System.IO.File.Delete(assetFileOld);
                        }
                        //extension = Path.GetExtension(ThumbnailFile.FileName);
                        //fileName = $"{videoGUID},{ extension}";
                        var tempFile = Path.Combine(tempUploads, videoGUID + extension);
                        var assetFile = Path.Combine(path, videoGUID + extension);
                        using (var fileStreams = new FileStream(tempFile, FileMode.Create))
                        {
                            ThumbnailFile.CopyTo(fileStreams);
                        }
                        if (!RekhtaUtility.MoveFile(tempFile, assetFile))
                        {
                            _logger.LogDebug($"File Not uploaded {fileName} .");
                        }
                    }
                    else
                    {
                        fileName = "Extension not supported";
                    }
                }
                if (extensionFor.Contains(".jpg") || extensionFor.Contains(".png") || extensionFor.Contains(".jpeg"))
                {
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                    {
                        fileName = $"{videoGUID}{ extension}";
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            var oldfile = Path.GetFileName(fileName);
                            var assetFileOld = Path.Combine(path, oldfile);
                            System.IO.File.Delete(assetFileOld);
                        }
                        //extension = Path.GetExtension(ThumbnailFile.FileName);
                        //fileName = $"{videoGUID},{ extension}";
                        var tempFile = Path.Combine(tempUploads, videoGUID + extension);
                        var assetFile = Path.Combine(path, videoGUID + extension);
                        using (var fileStreams = new FileStream(tempFile, FileMode.Create))
                        {
                            ThumbnailFile.CopyTo(fileStreams);
                        }
                        if (!RekhtaUtility.MoveFile(tempFile, assetFile))
                        {
                            _logger.LogDebug($"File Not uploaded {fileName} .");
                        }
                    }
                    else
                    {
                        fileName = "Extension not supported";
                    }
                }

            }
            return fileName;
        }

        public static string UploadFiles(string FileUploadsPath, IFormFile ThumbnailFile, string ImageName, string tempUploads, string extensionFor, ILogger _logger)
        {
            string fileName = string.Empty;
            string extension = string.Empty;
            bool isfile = false;
            extensionFor = extensionFor.ToLower();
            if (ThumbnailFile != null)
            {
                fileName = Path.GetFileName(ThumbnailFile.FileName);
                extension = Path.GetExtension(ThumbnailFile.FileName);
                isfile = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(ImageName))
                {
                    fileName = Path.GetFileName(ImageName);
                    string[] strExtension = ImageName.Split(".");
                    extension = strExtension[1];
                    isfile = false;
                }
            }

            string path = Path.Combine(FileUploadsPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (isfile)
            {
                if (extensionFor.Contains(".pdf"))
                {
                    if (extension.ToLower() == ".pdf")
                    {

                        if (!string.IsNullOrEmpty(ImageName))
                        {
                            var oldfile = Path.GetFileName(ImageName);
                            var assetFileOld = Path.Combine(path, oldfile);
                            System.IO.File.Delete(assetFileOld);
                        }
                        extension = Path.GetExtension(ThumbnailFile.FileName);
                        var tempFile = Path.Combine(tempUploads, fileName);
                        var assetFile = Path.Combine(path, fileName);
                        using (var fileStreams = new FileStream(tempFile, FileMode.Create))
                        {
                            ThumbnailFile.CopyTo(fileStreams);
                        }
                        if (!RekhtaUtility.MoveFile(tempFile, assetFile))
                        {
                            _logger.LogDebug($"File Not uploaded {fileName} .");
                        }
                    }
                    else
                    {
                        fileName = "Extension not supported";
                    }
                }
                if (extensionFor.Contains(".jpg")|| extensionFor.Contains(".jpg")|| extensionFor.Contains(".png")|| extensionFor.Contains(".png"))
                {
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == "jpg" || extension.ToLower() == ".png" || extension.ToLower() == "png")
                    {

                        if (ImageName != null)
                        {
                            var oldfile = Path.GetFileName(ImageName);
                            var assetFileOld = Path.Combine(path, oldfile);
                            if(!string.IsNullOrEmpty(oldfile))
                                 System.IO.File.Delete(assetFileOld);
                        }
                        extension = Path.GetExtension(ThumbnailFile.FileName);
                        var tempFile = Path.Combine(tempUploads, fileName);
                        var assetFile = Path.Combine(path, fileName);
                        using (var fileStreams = new FileStream(tempFile, FileMode.Create))
                        {
                            ThumbnailFile.CopyTo(fileStreams);
                        }
                        if (!RekhtaUtility.MoveFile(tempFile, assetFile))
                        {
                            _logger.LogDebug($"File Not uploaded {fileName} .");
                        }
                    }
                    else
                    {
                        fileName = "Extension not supported";
                    }
                }
                if (extensionFor.Contains(".mp3") || extensionFor.Contains(".wav"))
                {
                    if (extension.ToLower() == ".mp3" || extension.ToLower() == ".wav")
                    {
                        if (ImageName != null)
                        {
                            var oldfile = Path.GetFileName(ImageName);
                            var assetFileOld = Path.Combine(path, oldfile);
                            if (!string.IsNullOrEmpty(oldfile))
                                System.IO.File.Delete(assetFileOld);
                        }
                        extension = Path.GetExtension(ThumbnailFile.FileName);
                        var tempFile = Path.Combine(tempUploads, fileName);
                        var assetFile = Path.Combine(path, fileName);
                        using (var fileStreams = new FileStream(tempFile, FileMode.Create))
                        {
                            ThumbnailFile.CopyTo(fileStreams);
                        }
                        if (!RekhtaUtility.MoveFile(tempFile, assetFile))
                        {
                            _logger.LogDebug($"File Not uploaded {fileName} .");
                        }
                    }
                    else
                    {
                        fileName = "Extension not supported";
                    }
                }                

            }
            return fileName;
        }
        public static class GetProperty
        {
            public static int ProgramId { get; set; }
            public static string ProgramCode { get; set; }
            public static string ProgramName { get; set; }
            public static bool isFromPublishCourse { get; set; }
            public static int ContentId { get; set; }
            public static Guid ContentGuidId { get; set; }
            public static bool isfromQurstionTab { get; set; }
            public static string ContentName { get; set; }
            public static Guid QuizGUID { get; set; }
            public static int QuestionId { get; set; }
            public static string QuestionName { get; set; }
            public static string ClassName { get; set; }
            public static string TabName { get; set; }
            public static string PublishedURL { get; set; }
            public static string UserRoleId { get; set; }
            public static string UserRoleName { get; set; }
            public static string ActionName { get; set; }
            public static string ControllerName { get; set; }
            public static string siteUrl { get; set; }
            public static IEnumerable<QuickLinkPermission> QuickLinkPermission { get; set; }
            public static int LinkId { get; set; }
            public static string LinkName { get; set; }
            public static IEnumerable<TabLinkPermission> TabLinkPermission { get; set; }
            public static int TabMenuId { get; set; }
            public static int Id { get; set; }
        }
        public class QuickLinkPermission
        {
            public int QuickLinkId { get; set; }
            public int QuickLinkParentId { get; set; }
            public string QuickLinkName { get; set; }
            public string Action { get; set; }
            public string Area { get; set; }
        }

        public class TabLinkPermission
        {
            //public Int64 RowNumber { get; set; }
            public Int64 MenuId { get; set; }
            //public string MenuName { get; set; }
            //public string area { get; set; }
            //public string action { get; set; }
            //public bool Active { get; set; }
            //public bool IsSoftDelete { get; set; }
            //public string TabdivId { get; set; }
            //public int SerialNo { get; set; }
            public string RoleId { get; set; }
            //public string value { get; set; }
           
        }


        public static string[] Split(string source, string delimeter)
        {
            var offset = 0;
            var index = 0;
            var offsets = new int[source.Length + 1];

            while (index < source.Length)
            {
                var indexOf = source.IndexOf(delimeter, index);
                if (indexOf != -1)
                {
                    offsets[offset++] = indexOf;
                    index = (indexOf + delimeter.Length);
                }
                else
                {
                    index = source.Length;
                }
            }

            var final = new string[offset + 1];

            if (offset == 0)
            {
                final[0] = source;
            }
            else
            {
                offset--;

                final[0] = source.Substring(0, offsets[0]);
                for (var i = 0; i < offset; i++)
                {
                    final[i + 1] = source.Substring(offsets[i] + delimeter.Length, offsets[i + 1] - offsets[i] - delimeter.Length);
                }

                final[offset + 1] = source.Substring(offsets[offset] + delimeter.Length);
            }

            return final;
        }
        public static IDictionary<int, string> GetCreditsType()
        {
            IDictionary<int, string> CreditsType = new Dictionary<int, string>();
            CreditsType.Add(1, "Course Presenter");
            CreditsType.Add(2, "Content Writer");
            CreditsType.Add(3, "Content Editor");
            CreditsType.Add(4, "Instructor");
            return CreditsType;
        }

        public static string UploadWithIdsForImage(string FileUploadsPath, Guid videoGUID, IFormFile ThumbnailFile,
            string ImageName, string tempUploads, string extensionFor, ILogger _logger)
        {
            string fileName = string.Empty;
            bool isfile = false;

            if (ThumbnailFile != null)
            {
                fileName = Path.GetFileName(ThumbnailFile.FileName);
                isfile = true;
            }
            else if (!string.IsNullOrEmpty(ImageName))
            {
                fileName = Path.GetFileName(ImageName);
                isfile = false;
            }

            string path = Path.Combine(FileUploadsPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (isfile)
            {
                // Delete all existing files for this GUID
                var existingFiles = Directory.GetFiles(path, $"{videoGUID}*.*");
                foreach (var file in existingFiles)
                {
                    System.IO.File.Delete(file);
                }

                // Save new file as GUID.jpg
                fileName = $"{videoGUID}.jpg";
                var tempFile = Path.Combine(tempUploads, fileName);
                var assetFile = Path.Combine(path, fileName);

                // Load and save using SixLabors.ImageSharp
                using (var image = SixLabors.ImageSharp.Image.Load(ThumbnailFile.OpenReadStream()))
                {
                    image.Save(tempFile, new JpegEncoder { Quality = 80 });
                }

                if (!RekhtaUtility.MoveFile(tempFile, assetFile))
                {
                    _logger.LogError($"Failed to upload {fileName}");
                    return "Upload failed";
                }
            }

            return fileName;
        }
    }
}