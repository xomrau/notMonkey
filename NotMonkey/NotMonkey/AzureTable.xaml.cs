using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace NotMonkey
{
    public partial class AzureTable : ContentPage
    {

        public AzureTable()
        {
            InitializeComponent();
        }

		async void Clicked_Async(object sender, System.EventArgs e)
		{
            List<NotMonkeyModel> notMonkeyInformation = await AzureManager.AzureManagerInstance.GetMonkeyInformation();

            MonkeyList.ItemsSource = notMonkeyInformation;

            await postLocationAsync();
		}

		async Task postLocationAsync()
		{

			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 50;

			var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            NotMonkeyModel model = new NotMonkeyModel()
			{
				Longitude = (float)position.Longitude,
				Latitude = (float)position.Latitude

			};

            await AzureManager.AzureManagerInstance.PostMonkeyInfo(model);
		}
    }
}
