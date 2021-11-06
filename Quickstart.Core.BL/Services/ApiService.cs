using Newtonsoft.Json;
using Quickstart.Core.BL.DTOs;
using RestSharp;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Quickstart.Core.BL.Services
{
    public class ApiService
    {
        public enum MethodHttp
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public static ResponseDTO RequestHttp<T>(string urlBase,
            string prefix,
            MethodHttp method,
            object data = null)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlBase);

                    HttpContent content = null;
                    if (data != null)
                        content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    HttpResponseMessage response;                    

                    if (method == MethodHttp.GET)
                        response = client.GetAsync(prefix).Result;
                    else if (method == MethodHttp.POST)
                        response = client.PostAsync(prefix, content).Result;
                    else if (method == MethodHttp.PUT)
                        response = client.PutAsync(prefix, content).Result;
                    else
                        response = client.DeleteAsync(prefix).Result;

                    var responseText = response.Content.ReadAsStringAsync().Result;

                    return new ResponseDTO
                    {
                        Code = (int)response.StatusCode,
                        Message = response.IsSuccessStatusCode ? string.Empty : responseText,
                        Data = response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(responseText) : new object()
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = new object()
                };
            }
        }

        public static ResponseDTO RequestRest<T>(string baseUrl,
            string prefix,
            Method method,
            object data = null)
        {
            try
            {
                var client = new RestClient(baseUrl);
                var request = new RestRequest(prefix, method);

                if (data != null)
                {
                    request.RequestFormat = DataFormat.Json;
                    request.AddJsonBody(data);
                }

                var response = client.Execute(request);                

                return new ResponseDTO
                {
                    Code = (int)response.StatusCode,
                    Message = response.IsSuccessful ? string.Empty : response.Content,
                    Data = response.IsSuccessful ? JsonConvert.DeserializeObject<T>(response.Content) : new object()
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = new object()
                };
            }
        }
    }
}
