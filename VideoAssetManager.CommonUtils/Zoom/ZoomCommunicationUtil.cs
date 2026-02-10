using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.IO;
using Newtonsoft.Json;

using Serilog;
using Microsoft.Extensions.Logging;

namespace VideoAssetManager.CommonUtils.Zoom
{
    public class ZoomCommunicationUtil
    {
       // private static readonly ILogger Logger = Log.ForContext<ZoomCommunicationUtil>();

        public string ApiKey { get; set; }

        public string ApiSecret { get; set; }
         
        //public ZoomCommunicationUtil()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}

        public static string GetValidZoomJwtToken(string apiKey, string apiSecret)
        {
            var encryptionKey = System.Text.Encoding.UTF8.GetBytes(apiSecret);
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = apiKey,
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(securityTokenDescriptor);
            var jsonWebToken = tokenHandler.WriteToken(securityToken);
            return jsonWebToken;
        }
        public static ZoomAccessToken  GetAccessToken(string accountid,string apiKey, string apiSecret)
        {
            var base64Code = GetBase64Encode(apiKey, apiSecret);
            var AuthenticationToken = $"Basic {base64Code}";// "Basic " + jwtToken;
            //  var AuthenticationToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE1NTI0NzUzNjQsImV4cCI6MTU4NDAxMTM2NCwiaWF0IjoxNTUyNDc1MzY0LCJpc3MiOiJ4bExIRExMZ1MtaXNrRzF1TGFMdldBIn0.CYsXmDYJP-ZvRjMZdY9fon-sCHmXqN5kMIRSzoUxfLY";
            //var ApiUrl = "https://api.zoom.us/v2/report/meetings/"+ meetingId + "/participants?page_size=30";
           
            var apiUrl = $"https://zoom.us/oauth/token?grant_type=account_credentials&account_id={accountid}";

            string Response = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentType = "application/json";
            request.Accept = "application/json, application/xml";
            request.Headers.Add("authorization", AuthenticationToken);

            ZoomAccessToken accessToken = new ZoomAccessToken();
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    Response = reader.ReadToEnd();
                    accessToken = JsonConvert.DeserializeObject<ZoomAccessToken>(Response);
                }
                return accessToken;
            }
            catch (Exception ex)
            {
                return accessToken;
            }
        }

        public static string GetBase64Encode(string userName,string password)
        {
            var byteArray = System.Text.Encoding.ASCII.GetBytes($"{userName}:{password}");
            string encodeString = Convert.ToBase64String(byteArray);
            return encodeString;

        }
        public static ZoomMeetingUserList GetZoomMeetingDetails(string apiUrl, string jwtToken)
        {
            var AuthenticationToken = "Bearer " + jwtToken;
            //  var AuthenticationToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE1NTI0NzUzNjQsImV4cCI6MTU4NDAxMTM2NCwiaWF0IjoxNTUyNDc1MzY0LCJpc3MiOiJ4bExIRExMZ1MtaXNrRzF1TGFMdldBIn0.CYsXmDYJP-ZvRjMZdY9fon-sCHmXqN5kMIRSzoUxfLY";
            //var ApiUrl = "https://api.zoom.us/v2/report/meetings/"+ meetingId + "/participants?page_size=30";
            //  var ApiUrl = "https://api.zoom.us/v2/report/meetings/511776872/participants?page_size=30";
            string Response = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "GET";
            request.KeepAlive = true;
            request.ContentType = "application/json";
            request.Accept = "application/json, application/xml";
            request.Headers.Add("authorization", AuthenticationToken);

            ZoomMeetingUserList zoomMeetingUserList = new ZoomMeetingUserList();
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    Response = reader.ReadToEnd();
                    zoomMeetingUserList = JsonConvert.DeserializeObject<ZoomMeetingUserList>(Response);
                }
                return zoomMeetingUserList;
            }
            catch
            {
                return zoomMeetingUserList;
            }

        }

        //public static Tuple<string, string> GetZoomSecret(int IltId)
        //{
        //    var ApiKey = string.Empty;
        //    var ApiSecret = string.Empty;
        //    Form0033 objForm = new Form0033();
        //    objForm.GetZoomSecret(IltId, out ApiKey, out ApiSecret);
        //    return new Tuple<string, string>(ApiKey, ApiSecret);
        //}
        public static string GetZoomjoinURLDetails(int IltID,string MeetingId)
        {
            var ZoomSecret = "";// GetZoomSecret(IltID);
            var AuthenticationJwtToken = "";// "Bearer " + GetValidZoomJwtToken(ZoomSecret.Item1.ToString(), ZoomSecret.Item2.ToString());
            var ApiUrl = "https://api.zoom.us/v2/meetings/" + MeetingId;
      
            string Response = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiUrl);
            request.Method = "GET";
            request.KeepAlive = true;
            request.ContentType = "application/json";
            request.Accept = "application/json, application/xml";
            request.Headers.Add("authorization", AuthenticationJwtToken);

            ZoomMeetingDetail objZoomMeetingDetail = new ZoomMeetingDetail();
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    Response = reader.ReadToEnd();
                    objZoomMeetingDetail = JsonConvert.DeserializeObject<ZoomMeetingDetail>(Response);
                    return objZoomMeetingDetail.start_url;
                }
           
            }
            catch
            {
                return objZoomMeetingDetail.start_url;
            }

        }

 

        /// <summary>
        /// call zoom v2 api for added webinar  
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="jwtToken"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        //public static ZoomWebinarDetail GetZoomWebinarjoinURLDetails(string jwtToken, ZoomWebinarDetail postData)
        //{

        //    var AuthenticationToken = "Bearer " + jwtToken;
        //    string Response = string.Empty;
        //    string strResponse = string.Empty;
        //    string apiUrl = string.Format("https://api.zoom.us/v2/users/{0}/webinars", postData.host_id);             

        //    var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
        //    httpWebRequest.ContentType = "application/json; charset=utf-8";
        //    httpWebRequest.Method = "POST";
        //    httpWebRequest.KeepAlive = true;
        //    httpWebRequest.Accept = "application/json";
        //    httpWebRequest.Headers.Add("authorization", AuthenticationToken);
        //    var data = JsonConvert.SerializeObject(postData);

        //    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //    {
        //        streamWriter.Write(data);
        //        streamWriter.Flush();
        //    }
        //    ZoomWebinarDetail zoomWebinarDetail = new ZoomWebinarDetail();            

        //    try
        //    {
        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //        if (httpResponse.StatusCode == HttpStatusCode.Created)
        //        {
        //            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //            {
        //                strResponse = streamReader.ReadToEnd();
        //                zoomWebinarDetail = JsonConvert.DeserializeObject<ZoomWebinarDetail>(strResponse);
        //            }
        //        }
        //    }
        //    catch (WebException webex)
        //    {
        //        Logger.Error(webex, "zoom meeting error");
        //        throw new MeetingToolException("zoom meeting error", webex)
        //        {
        //            StatusCode = (int)webex.Response.As<HttpWebResponse>().StatusCode
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex, "zoom meeting error");
        //        throw;
        //    }
            
        //    return zoomWebinarDetail;
        //}

        /// <summary>
        /// update webinar details
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        //public static string UpdateZoomWebinarDetails(string jwtToken, ZoomWebinarDetail postData)
        //{

        //    var AuthenticationToken = "Bearer " + jwtToken;
        //    string Response = string.Empty;
        //    string strResponse = string.Empty;
        //    string apiUrl = string.Format("https://api.zoom.us/v2/webinars/{0}", postData.id);

        //    var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
        //    httpWebRequest.ContentType = "application/json; charset=utf-8";
        //    httpWebRequest.Method = "PATCH";
        //    httpWebRequest.KeepAlive = true;
        //    httpWebRequest.Accept = "application/json";
        //    httpWebRequest.Headers.Add("authorization", AuthenticationToken);
        //    var data = JsonConvert.SerializeObject(postData);

        //    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //    {
        //        streamWriter.Write(data);
        //        streamWriter.Flush();
        //    }
        //    try
        //    {
        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //       if (httpResponse.StatusCode == HttpStatusCode.NoContent)
        //        {
        //            strResponse = Convert.ToString(postData.id);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return strResponse;
        //}

        ///// <summary>
        ///// delete webinar details
        ///// </summary>
        ///// <param name="jwtToken"></param>
        ///// <param name="postData"></param>
        ///// <returns></returns>
        //public static string DeleteZoomWebinar(string jwtToken, ZoomWebinarDetail postData)
        //{

        //    var AuthenticationToken = "Bearer " + jwtToken;
        //    string Response = string.Empty;
        //    string strResponse = string.Empty;
        //    string apiUrl = string.Format("https://api.zoom.us/v2/webinars/{0}", postData.id);

        //    var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
        //    httpWebRequest.ContentType = "application/json; charset=utf-8";
        //    httpWebRequest.Method = "DELETE";
        //    httpWebRequest.KeepAlive = true;
        //    httpWebRequest.Headers.Add("authorization", AuthenticationToken);

        //    try
        //    {
        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //        if (httpResponse.StatusCode == HttpStatusCode.NoContent)
        //        {
        //            strResponse = Convert.ToString(postData.id);
        //        }
        //    }
        //    catch (Exception)
        //    {
                
        //    }
        //    return strResponse;
        //}

        /// <summary>
        /// delete zoom meeting details
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string DeleteZoomMeeting(string jwtToken, string meetingId)
        {

            var AuthenticationToken = "Bearer " + jwtToken;
            string Response = string.Empty;
            string strResponse = string.Empty;
            string apiUrl = string.Format("https://api.zoom.us/v2/meetings/{0}", meetingId);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "DELETE";
            httpWebRequest.KeepAlive = true;
            httpWebRequest.Headers.Add("authorization", AuthenticationToken);
         
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode == HttpStatusCode.NoContent)
                {
                    strResponse = meetingId;
                }
            }
            catch (Exception)
            {

            }
            return strResponse;
        }

        /// <summary>
        /// end zoom meeting
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string EndZoomMeeting(string jwtToken, ZoomMeetingDetail postData)
        {

            var AuthenticationToken = "Bearer " + jwtToken;
            string Response = string.Empty;
            string strResponse = string.Empty;
            string apiUrl = string.Format("https://api.zoom.us/v2/meetings/{0}/status", postData.id);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "PUT";
            httpWebRequest.KeepAlive = true;
            httpWebRequest.Headers.Add("authorization", AuthenticationToken);

            var data = JsonConvert.SerializeObject(postData);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode == HttpStatusCode.NoContent)
                {
                    strResponse = Convert.ToString(postData.id);
                }
            }
            catch (Exception)
            {

            }
            return strResponse;
        }

        /// <summary>
        /// call zoom v2 api for added Meeting  
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="jwtToken"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string CreateZoomMeeting(string jwtToken, ZoomMeetingDetail postData)
        {

            var AuthenticationToken = "Bearer " + jwtToken;
            string Response = string.Empty;
            string strResponse = string.Empty;
            string apiUrl = string.Format("https://api.zoom.us/v2/users/{0}/meetings", postData.host_id);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.KeepAlive = true;
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Headers.Add("authorization", AuthenticationToken);
            var data = JsonConvert.SerializeObject(postData);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode == HttpStatusCode.Created)
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        strResponse = streamReader.ReadToEnd();
                    }
                }
            }
            //catch (WebException webex)
            //{
            //    //Logger.Error(webex, "zoom meeting error");
            //    //throw new MeetingToolException("zoom meeting error", webex)
            //    //{
            //    //    StatusCode = (int)webex.Response.As<HttpWebResponse>().StatusCode
            //    //};
            //}
            catch (Exception ex)
            {
                //Logger.Error(ex, "zoom meeting error");
                throw;
            }


            return strResponse;
        }


        /// <summary>
        /// call zoom v2 api for update Meeting
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="jwtToken"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string UpdateZoomMeeting(string jwtToken, ZoomMeetingDetail postData)
        {

            var AuthenticationToken = "Bearer " + jwtToken;
            string Response = string.Empty;
            string strResponse = string.Empty;
            string apiUrl = string.Format("https://api.zoom.us/v2/meetings/{0}", postData.id);            

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "PATCH";
            httpWebRequest.KeepAlive = true;
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Headers.Add("authorization", AuthenticationToken);
            var data = JsonConvert.SerializeObject(postData);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode == HttpStatusCode.NoContent)
                {
                    strResponse = Convert.ToString(postData.id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strResponse;
        }
    }
}
