using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VisitorInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    // Lists the visitors Id's
    public partial class VisitorList : ContentPage
    {

        public ObservableCollection<Item> Items { set; get; }
        public string[] visitorId { get; set; }
        List<String> visitorIdList = new List<string>();
        public VisitorList()
        {
            InitializeComponent();
            Items = new ObservableCollection<Item>();
            listView.ItemsSource = Items;
            Init();
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;


            await Navigation.PushAsync(new VisitorDetail(e.SelectedItem.ToString()));
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
        public class Item
        {
            public string Text { get; set; }
            public string Detail { get; set; }

            public override string ToString() => Text;
        }
        private async void Init()
        {
            string url = "https://api-dev.interfacema.de/visits";
            await GetAllVisitId(url);
        }

        /// <summary>
        /// Retrives all the visitors in the server and 
        /// display their id's
        /// </summary>
        /// <param name="url">request url</param>
        /// <returns></returns>
        public async Task GetAllVisitId(string url)
        {
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

                    var model = JsonConvert.DeserializeObject<List<RootObject>>(result);


                    // Extracting the id from the url
                    foreach (var m in model)
                    {
                        var tempUrl = m.href;
                        visitorId = tempUrl.Split('/');
                        visitorIdList.Add(visitorId[4]);

                    }

                    foreach (var value in visitorIdList)
                    {
                        Items.Add(new Item { Text = value.ToString(), Detail = "" });

                    }


                }
            }
        }

    }




    public class RootObject
    {
        public string href { get; set; }
        public string name { get; set; }
    }


}
