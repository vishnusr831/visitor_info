using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VisitorInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
   

    public partial class VisitorDetail : ContentPage
    {
        public VisitorDetail(String vId)
        {
            InitializeComponent();
            Init(vId);
        }
        private async void Init(String vId)
        {
            await GetVisitorDetails(vId);
        }
        /// <summary>
        /// Retrives the details of a particular visitor and displays it
        /// </summary>
        /// <param name="visitorId">Id of the visitor</param>
        /// <returns></returns>
        private async Task GetVisitorDetails(string visitorId)
        {
            string url = "https://api-dev.interfacema.de/visits/" + visitorId;

            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:

                    StreamReader sr = new StreamReader(stream);
                    var result = sr.ReadToEnd();
                    Console.WriteLine(result);
                    Console.WriteLine("creating model");
                    var model = JsonConvert.DeserializeObject<RootObjectVisitorDetails>(result);

                    Console.WriteLine("Listing details of a particular visitor");

                    label.Text = "Visitor Id : " + model.id + "\n" +
                        "Date of visit : " + model.dateOfVisit + "\n" +
                        "Address : " + model.address + "\n" +
                        "Status of Valuation : " + model.statusValuation + "\n" +
                        "Status of Photodoku : " + model.statusPhotodoku + "\n" +
                        "Is Archived : " + model.isArchived + "\n" +
                        "Type : " + model.type + "\n"
                        ;

                }
            }
        }
    }
    public class Tags
    {
        public string ima_SystemFilePath { get; set; }
    }

    public class RootObjectVisitorDetails
    {
        public string id { get; set; }
        public int statusValuation { get; set; }
        public int statusPhotodoku { get; set; }
        public bool isArchived { get; set; }
        public string dateOfVisit { get; set; }
        public string address { get; set; }
        public string dateUnknown { get; set; }
        public string objectName { get; set; }
        public string portfolioName { get; set; }
        public int type { get; set; }
        public string typeName { get; set; }
        public int status { get; set; }
        public string comment { get; set; }
        public Tags tags { get; set; }
    }
}
