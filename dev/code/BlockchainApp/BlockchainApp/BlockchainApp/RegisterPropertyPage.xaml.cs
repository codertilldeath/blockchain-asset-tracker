﻿using BlockchainAPI;
using BlockchainAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlockchainApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPropertyPage : ContentPage
	{
        private BlockchainClient client;

        public RegisterPropertyPage(BlockchainClient blockchainclient)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            client = blockchainclient;
        }

        async void CreateProperty(object sender, EventArgs e)
	    {
            Property p = new Property();
            p.PropertyId = property_id.Text;
            p.description = description.Text;
            p.owner = client.thisTrader.traderId;
            BlockchainClient.Result error;
            error = await client.RegisterNewProperty(p);
            switch (error)
            {
                case BlockchainClient.Result.SUCCESS:
                    await DisplayAlert("Alert", "Sucessful create Asset", "Ok");
                    break;
                case BlockchainClient.Result.EXISTS:
                    await DisplayAlert("Alert", "Unsucessful create Asset: Asset id already exists", "Ok");
                    break;
                case BlockchainClient.Result.NETWORK:
                    await DisplayAlert("Alert", "Error: Network down. Please try again.", "Ok");
                    break;
            }

            await Navigation.PopAsync();
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            Plugin.Media.Abstractions.StoreCameraMediaOptions options = new Plugin.Media.Abstractions.StoreCameraMediaOptions();
            options.SaveToAlbum = true;

            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(options);

            if (photo != null)
                PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
        }
    }
}