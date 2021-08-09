using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Map_exercise
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class User_Page : ContentPage
	{
		public User_Page()
		{
			InitializeComponent();

            name.Text = SigninPage.NameUser;
		}

        private void Button_Clicked(object sender, EventArgs e) // log out
        {
            //Navigation.PopAsync();
            Navigation.PopToRootAsync();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new Doimatkhau());
        }
    }
}