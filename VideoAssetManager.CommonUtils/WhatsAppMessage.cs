using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VideoAssetManager.CommonUtils
{
    public class WhatsAppMessage
    {

        public static async Task<string> WhatsAppService(string whatsappNo, string postbody, string serverApiKey, string accountTypeName = "rf")
        {
            string result = string.Empty;
            bool isDeliverd = false;
            string responseId = string.Empty;
            var responseMessage = string.Empty;

            try
            {
                whatsappNo = !string.IsNullOrEmpty(whatsappNo) ? whatsappNo : "9811985399";
               
                var url = "https://waapi.pepipost.com/api/v2/message/";

                var tRequest = (HttpWebRequest)WebRequest.Create(url);
                tRequest.Method = "POST";
                tRequest.Accept = "application/json";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization:{0}", serverApiKey));
                //tRequest.Headers[HttpRequestHeader.Authorization] = serverApiKey;

                ///* add below code to prevent "Authentication failed because the remote party has closed the transport stream" */
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postbody);
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {

                                result = tReader.ReadToEnd();
                                responseMessage = result;

                                var jResult = JsonConvert.DeserializeObject<WhatsAppResponse>(result);
                                //var json = new JavaScriptSerializer().Deserialize<WhatsAppResponse>(result);

                                result = jResult.message;
                                isDeliverd = true;
                                responseId = !string.IsNullOrEmpty(result)? jResult.data.id:"";
                            }
                        }
                    }
                }


                /* alternate method */
                //using (var streamWriter = new StreamWriter(tRequest.GetRequestStream()))
                //{
                //    streamWriter.Write(postbody);
                //}

                //var httpResponse = (HttpWebResponse)tRequest.GetResponse();
                //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                //{
                //    result = streamReader.ReadToEnd();
                //    var jResult = JsonConvert.DeserializeObject<WhatsAppResponse>(result);
                //    //var json = new JavaScriptSerializer().Deserialize<WhatsAppResponse>(result);

                //    result= jResult.message;
                //}

            }
            catch (Exception ex)
            {
                result = ex.InnerException.Message + " " + ex.Message + " Payload > " + postbody;
                responseMessage = result;
                responseId = "";
            }
            finally
            {

            }

            return result;
        }

        public static string PayloadWithAttribute(string whatsappNo, string accountName, string templateName, string imageUrl, string[] finalAttri, string visitWebsite, int lang = 1)
        {
            string payloadBody = string.Empty;

            string langShortName = lang == 2 ? "hi" : lang == 3 ? "ur" : "en";
            var whatsNotify = new WhatsAppNotification();
            var whatsBody = new WhatsAppMessageBodyWithoutVariable[1];
            var msgBody = new WhatsAppMessageBodyWithoutVariable();

            msgBody.recipient_whatsapp = whatsappNo;
            msgBody.recipient_type = "individual";
            msgBody.message_type = "media_template";
            msgBody.cta_link_track = "1";
            msgBody.source = accountName;
            msgBody.xapiheader = "custom_data";

            msgBody.type_media_template = new WhatsAppMediaButtonTemplate
            {
                type = "image",
                //type = "document",
                url = imageUrl,
              //  filename = "",
                button = new WhatsAppButtonVisitTemplate[]
                    {
                            new WhatsAppButtonVisitTemplate
                            {
                                index = "0",
                                payload = visitWebsite
                            }
                    }
            };

            msgBody.type_template = new WhatsAppTypeTemplateWithAttribute[]
            {
                        new WhatsAppTypeTemplateWithAttribute
                        {
                            name = templateName,
                            attributes= finalAttri,
                            language = new WhatsAppLanguageType
                            {
                                locale = langShortName,
                                policy = "deterministic"
                            }
                        }
            };

            whatsBody[0] = msgBody;
            whatsNotify.message = whatsBody;
            payloadBody = JsonConvert.SerializeObject(whatsNotify).ToString();

            return payloadBody;
        }

        public static string PayloadWithoutAttribute(string whatsappNo, string accountName, string templateName, string imageUrl, int lang = 1)
        {
            string payloadBody = string.Empty;

            string langShortName = lang == 2 ? "hi" : lang == 3 ? "ur" : "en";
            var whatsNotify = new WhatsAppNotification();
            var whatsBody = new WhatsAppMessageBodyWithoutVariable[1];
            var msgBody = new WhatsAppMessageBodyWithoutVariable();

            msgBody.recipient_whatsapp = whatsappNo;
            msgBody.recipient_type = "individual";
            msgBody.message_type = "media_template";
            msgBody.cta_link_track = "1";
            msgBody.source = accountName;
            msgBody.xapiheader = "custom_data";

            msgBody.type_media_template = new WhatsAppMediaTemplate
            {
                type = "image",
                url = imageUrl,
                filename = "",
            };

            msgBody.type_template = new WhatsAppTypeTemplate[]
            {
                            new WhatsAppTypeTemplate
                            {
                                name = templateName,
                                language = new WhatsAppLanguageType
                                {
                                    locale = langShortName,
                                    policy = "deterministic"
                                }
                            }
            };

            whatsBody[0] = msgBody;
            whatsNotify.message = whatsBody;
            payloadBody = JsonConvert.SerializeObject(whatsNotify).ToString();

            return payloadBody;
        }
        public static string PayloadWithMessageOnly(string whatsappNo, string accountName, string templateName,  int lang = 1)
        {
            string payloadBody = string.Empty;

            string langShortName = lang == 2 ? "hi" : lang == 3 ? "ur" : "en";
            var whatsNotify = new WhatsAppNotification();
            var whatsBody = new WhatsAppMessageBodyWithoutVariable[1];
            var msgBody = new WhatsAppMessageBodyWithoutVariable();

            msgBody.recipient_whatsapp = whatsappNo;
            msgBody.recipient_type = "individual";
            msgBody.message_type = "media_template";            
            msgBody.source = accountName;
            msgBody.xapiheader = "custom_data";

            msgBody.type_template = new WhatsAppTypeTemplate[]
            {
                            new WhatsAppTypeTemplate
                            {
                                name = templateName,
                                language = new WhatsAppLanguageType
                                {
                                    locale = langShortName,
                                    policy = "deterministic"
                                }
                            }
            };

            whatsBody[0] = msgBody;
            whatsNotify.message = whatsBody;
            payloadBody = JsonConvert.SerializeObject(whatsNotify).ToString();

            return payloadBody;
        }

        public class WhatsAppNotification
        {
            public WhatsAppMessageBodyWithoutVariable[] message { get; set; }
        }

        public class WhatsAppNotificationWithVariable
        {
            public WhatsAppMessageBodyWithVariable[] message { get; set; }
        }

        public class WhatsAppMessageBody
        {
            public string recipient_whatsapp { get; set; }
            public string recipient_type { get; set; }
            public string message_type { get; set; }
            public string cta_link_track { get; set; }
            public string source { get; set; }
            [JsonProperty("x-apiheader")]
            public string xapiheader { get; set; }
        }

        public class WhatsAppMessageBodyWithoutVariable : WhatsAppMessageBody
        {
            public WhatsAppTypeTemplate[] type_template { get; set; }
            public WhatsAppMediaTemplate type_media_template { get; set; }
        }

        public class WhatsAppMessageBodyWithVariable : WhatsAppMessageBody
        {
            public WhatsAppTypeTemplateWithAttribute[] type_template { get; set; }
            public WhatsAppMediaButtonTemplate type_media_template { get; set; }
        }

        public class WhatsAppMediaTemplate
        {
            public string type { get; set; }
            public string url { get; set; }
            public string filename { get; set; }
        }

        public class WhatsAppMediaButtonTemplate : WhatsAppMediaTemplate
        {
            public WhatsAppButtonVisitTemplate[] button { get; set; }
        }

        public class WhatsAppButtonVisitTemplate
        {
            public string index { get; set; }
            public string payload { get; set; }
        }

        public class WhatsAppTypeTemplate
        {
            public string name { get; set; }
            public WhatsAppLanguageType language { get; set; }
        }

        public class WhatsAppTypeTemplateWithAttribute : WhatsAppTypeTemplate
        {
            public string[] attributes { get; set; }

        }

        public class WhatsAppLanguageType
        {
            public string locale { get; set; }
            public string policy { get; set; }
        }

        public class WhatsAppResponse
        {
            public string status { get; set; }
            public string message { get; set; }
            public WhatsAppData data { get; set; }
        }

        public class WhatsAppData
        {
            public string id { get; set; }
        }

    }
}
