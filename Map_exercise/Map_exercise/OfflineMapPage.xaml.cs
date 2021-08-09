using Map_exercise.Models;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Map_exercise
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OfflineMapPage : ContentPage
	{
		public OfflineMapPage()
		{
			InitializeComponent();

            GetCurrentPosition();

            Device.StartTimer(TimeSpan.FromMinutes(1), () =>
            {
                GetCurrentPosition();
                return true;
            });
        }

        private async void GetCurrentPosition()
        {
            try
            {
                //get position
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
                Position _pos = new Position(position.Latitude, position.Longitude);

                MyMap.Pins.Clear();
                MyMap.Pins.Add(new Pin
                {
                    Position = _pos,
                    Label = string.Format("me ({0}, {1})", Math.Round(_pos.Latitude, 3), Math.Round(_pos.Longitude, 3))
                });

                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(_pos, Distance.FromMiles(1)));
            }
            catch (Exception ex)
            {
                DependencyService.Get<IToast>().Show(string.Format("Error code: {0}", ex.HResult));
            }
        }
    }
}