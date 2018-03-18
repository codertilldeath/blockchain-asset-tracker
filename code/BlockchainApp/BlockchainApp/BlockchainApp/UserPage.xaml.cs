﻿using BlockchainAPI;
using BlockchainAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlockchainApp
{
	public partial class UserPage : ContentPage
    {
        ObservableCollection<AssetView> obc = new ObservableCollection<AssetView>();
        private BlockchainClient client;

        public class AssetView
        {
            public string title { get; set; }
            public string subtitle { get; set; }
        }

        public UserPage(BlockchainClient client)
        {
            this.client = client;

            InitializeComponent();
            welcomeMessage.Text = String.Format("Hello, {0}!", client.getName());
            NavigationPage.SetHasNavigationBar(this, false);
            populateAssetList();
        }
        
        void populateAssetList()
        {
            foreach (Property obj in client.getMyAssets())
            {
                obc.Add(new AssetView() { title = obj.PropertyId, subtitle = obj.description });
            }

            current_asset_list.ItemsSource = obc;
        }

        async void Submit_Send_Clicked(object Sender, EventArgs e)
        {
            Button b = (Button)Sender;
            string PropertyId = b.CommandParameter.ToString();
            await Navigation.PushAsync(new TransferPage(client,PropertyId));
            for(int i = obc.Count()-1; i >=0 ; i--)
            {
                if(obc.ElementAt<AssetView>(i).title == PropertyId)
                {
                    obc.RemoveAt(i);
                }
            }
        }

        void logout(object Sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async void TransactionButton(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new HistoryPage());
        }
    }
}