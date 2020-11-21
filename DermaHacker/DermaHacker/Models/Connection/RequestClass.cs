using DermaHacker.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DermaHacker.Models.Connection
{
  public  class RequestClass
    {
        HttpClientHandler clientHandler;
        private static RequestClass instance;
        public static RequestClass Instance(ICommanderReceivedData commanderReceivedData)
        {
           
                if (instance == null)
                {
                    instance = new RequestClass(commanderReceivedData);
                }
                return instance;                
           
        }
        private HttpClient client;
        private ICommanderReceivedData commanderReceivedData;
        private RequestClass(ICommanderReceivedData commanderReceivedData) {
            this.commanderReceivedData = commanderReceivedData;


          
        }

        string url = "https://192.168.43.179:5000/";
        public void SendGETRequest()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(url);
            // Add an Accept header for JSON format.    
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Image/com"));
            // List all Names.    
            HttpResponseMessage response = client.GetAsync("Image/com").Result;  // Blocking call!    
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
               ImageData imageData= JsonConvert.DeserializeObject<List<ImageData>>(products)[0];
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }
        public ImageData SendAndTakeImage(ImageData image)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(url);
            // Add an Accept header for JSON format.    
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.    
            // = JsonConvert.SerializeObject(image);
            var json = JsonConvert.SerializeObject(image);
         ///   HttpContent httpContent = new StringContent(json, Encoding.UTF8, "Image/com");

         //   HttpResponseMessage response = client.PostAsync("Image/com", httpContent).Result;  // Blocking call! 



            var httpRequestMessage = new HttpRequestMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsync("Image/com", httpRequestMessage.Content).Result;

            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;

                ImageData imageData = JsonConvert.DeserializeObject<ImageData>(products);
                return imageData;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            return null;
        }

    }
}
