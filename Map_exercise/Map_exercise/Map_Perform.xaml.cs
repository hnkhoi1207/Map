using System;
using System.Collections.Generic;

using Plugin.Geolocator;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

using Map_exercise.Models;

namespace Map_exercise
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Map_Perform : ContentPage
	{
        public static List<Devices> trackList = new List<Devices>();

        string NameUser = SigninPage.NameUser;

        //for search page
        public static string addedUser = "";

        public Map_Perform()
		{
            InitializeComponent();

            pkMapmode.SelectedIndex = 0;

            lbluser.Text = NameUser;
            lblstatus.Text = "You and 0 other(s)";

            trackList.Clear();
            trackList.Add(new Devices { Name = NameUser });

            try
            {
                //get my current pos
                GetMyCurrentPosition();

                //add me to the map
                GetUserPos(0);
                MyMap.Pins.Add(new Pin
                {
                    Label = "me",
                    Position = new Position(trackList[0].LastLatitude, trackList[0].LastLongitude)
                });
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(trackList[0].LastLatitude, trackList[0].LastLongitude), Distance.FromMiles(1)));
            }
            catch (Exception ex)
            {
                DependencyService.Get<IToast>().Show(string.Format("Error code: {0}", ex.HResult));
                Navigation.PopAsync();
            }

            //start to update position periodically
            Device.StartTimer(TimeSpan.FromMinutes(1), () =>
            {
                try
                {
                    GetMyCurrentPosition();

                    for (int i = 0; i < trackList.Count; ++i)
                    {
                        GetUserPos(i);
                        MyMap.Pins[i].Position = new Position(trackList[i].LastLatitude, trackList[i].LastLongitude);
                    }

                    ResetListView();
                }
                catch (Exception ex)
                {
                    DependencyService.Get<IToast>().Show(string.Format("Error code: {0}", ex.HResult));
                }

                return true;
            });
        }

        void ResetListView()
        {
            lvshow.ItemsSource = null;
            lvshow.ItemsSource = trackList;
        }

        private void BtnShow_Clicked(object sender, EventArgs e)
        {
            if (infopanel.IsVisible)
            {
                infopanel.IsVisible = false;
                MyMap.IsVisible = true;
                btnShow.Text = "Show";
            }
            else
            {
                ResetListView();

                infopanel.IsVisible = true;
                MyMap.IsVisible = false;
                btnShow.Text = "Hide";
            }
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (addedUser != "")
            {
                if (addedUser == NameUser)
                {
                    DependencyService.Get<IToast>().Show("Can not add yourself.");
                }
                else
                {
                    try
                    {
                        trackList.Add(new Devices { Name = addedUser });
                        int x = trackList.Count - 1;
                        GetUserPos(x);
                        MyMap.Pins.Add(new Pin
                        {
                            Label = trackList[x].Name,
                            Position = new Position(trackList[x].LastLatitude, trackList[x].LastLongitude)
                        });

                        ResetListView();

                        lblstatus.Text = string.Format("You and {0} other(s)", trackList.Count - 1);
                    }
                    catch (Exception ex)
                    {
                        DependencyService.Get<IToast>().Show(string.Format("Error code: {0}", ex.HResult));
                    }
                }

                addedUser = "";
            }
        }

        private void Btnadd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchPage());
        }

        private void Btnmapshow_Clicked(object sender, EventArgs e)
        {
            var menuitem = sender as MenuItem;
            Devices item = menuitem.CommandParameter as Devices;

            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(item.LastLatitude, item.LastLongitude), Distance.FromMiles(1)));

            infopanel.IsVisible = false;
            MyMap.IsVisible = true;
            btnShow.Text = "Show";
        }

        private void Btndel_Clicked(object sender, EventArgs e)
        {
            var menuitem = sender as MenuItem;
            Devices item = menuitem.CommandParameter as Devices;

            int idx = trackList.IndexOf(item);

            if (idx == 0)
            {
                DependencyService.Get<IToast>().Show("Can not delete yourself.");
                return;
            }

            trackList.RemoveAt(idx);
            MyMap.Pins.RemoveAt(idx);
            ResetListView();
        }

        private void GetUserPos(int idx)
        {
            trackList[idx].LastLatitude  = double.Parse(MyWebEngine.Get(trackList[idx].Name, "latitude"));
            trackList[idx].LastLongitude = double.Parse(MyWebEngine.Get(trackList[idx].Name, "longitude"));
            trackList[idx].LastDate = DateTime.Parse(MyWebEngine.Get(trackList[idx].Name, "date"));
        }

        private async void GetMyCurrentPosition()
        {
            //get position
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            //update to server
            MyWebEngine.Edit(NameUser, "latitude", position.Latitude.ToString());
            MyWebEngine.Edit(NameUser, "longitude", position.Longitude.ToString());
            MyWebEngine.Edit(NameUser, "date", DateTime.Now.ToString());
        }

        private void Btnabout_Clicked(object sender, EventArgs e)
        {
            DisplayAlert(NModels.title,
                "Project GPS" + Environment.NewLine +
                "by Dat, Khai, Khoi, and Thang" + Environment.NewLine +
                "VNU.HCM - HSG" + Environment.NewLine +
                "@2019",
                NModels.btnOk); 
        }

        private void PkMapmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (pkMapmode.SelectedIndex)
            {
                case 0:
                    MyMap.MapType = MapType.Hybrid;
                    break;
                case 1:
                    MyMap.MapType = MapType.Satellite;
                    break;
                case 2:
                    MyMap.MapType = MapType.Street;
                    break;
            }
        }

        private void Btnrefresh_Clicked(object sender, EventArgs e)
        {
            try
            {
                var menuitem = sender as MenuItem;
                Devices item = menuitem.CommandParameter as Devices;

                int idx = trackList.IndexOf(item);

                if (idx == 0) GetMyCurrentPosition();

                GetUserPos(idx);

                MyMap.Pins[idx].Position = new Position(trackList[idx].LastLatitude, trackList[idx].LastLongitude);

                DependencyService.Get<IToast>().Show(NModels.success);
            }
            catch (Exception ex)
            {
                DependencyService.Get<IToast>().Show(string.Format("Error code: {0}", ex.HResult));
            }
        }

        private void Btnuser_Clicked(object sender, EventArgs e) 
        {
            Navigation.PushAsync(new User_Page());
        }
    }
}