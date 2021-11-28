using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CoinAPI;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft;
using CryptoTracker.Model;

namespace CryptoTracker
{
    public partial class MainPage : ContentPage
    {
        private string apikey = "81051166-91A3-413E-8E7F-CF78203F69A9";
        private string baseimageurl = "https://s3.eu-central-1.amazonaws.com/bbxt-static-icons/type-id/png_32/";
        public MainPage()
        {
            InitializeComponent();
            coinListView.ItemsSource = GetCoin();
        }
        private void Refresh_Clicked(object sender, EventArgs e)
        {
            coinListView.ItemsSource = GetCoin();
        }
        private List<Coin> GetCoin()
        {
            List<Coin> coins;
            var client = new RestClient("http://rest.coinapi.io/v1/assets?filter_asset_id=ETH;XRP;BTC;LTC;DOGE");
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-CoinAPI-Key", apikey);
            var response = client.Execute(request);
            coins = JsonConvert.DeserializeObject<List<Coin>>(response.Content);
            foreach (var c in coins)
            {
                c.icon_url = baseimageurl + c.id_icon.Replace("-", "") + ".png";
            }
            return coins;
        }
    }
}
