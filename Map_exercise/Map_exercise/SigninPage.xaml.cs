using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Map_exercise.Models;

namespace Map_exercise
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SigninPage : ContentPage
	{
        public static string NameUser = "thang", PassUser = "abc";

        public SigninPage()
		{
			InitializeComponent();
        }
        
        private void SigninButton_Clicked(object sender, EventArgs e)
        {
            if (!NModels.CheckInternetConnection()) return;
            try
            {
                if (MyWebEngine.Exist(nameEntry.Text) &&
                    MyWebEngine.Get(nameEntry.Text, "password") == passEntry.Text)
                {
                    DependencyService.Get<IToast>().Show(NModels.success);
                    NameUser = nameEntry.Text;
                    PassUser = passEntry.Text;
                    nameEntry.Text = passEntry.Text = "";
                    Navigation.PushAsync(new Map_Perform());
                    return;
                }
                DisplayAlert(NModels.title, NModels.loginFailed, NModels.btnOk);
            }
            catch (Exception ex)
            {
                DisplayAlert(NModels.errorTitle, ex.Message, NModels.btnOk);
            }
        }

        private void Btnoffline_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OfflineMapPage());
        }

        private void SignupButton_Clicked(object sender, EventArgs e)
        {
            if (!NModels.CheckInternetConnection()) return;
            try
            {
                if (string.IsNullOrWhiteSpace(nameEntry.Text) || string.IsNullOrWhiteSpace(passEntry.Text))
                {
                    DisplayAlert(NModels.title, NModels.missingDetails, NModels.btnOk);
                    return;
                }
                if (MyWebEngine.Exist(nameEntry.Text))
                {
                    DisplayAlert(NModels.title, NModels.existUsername, NModels.btnOk);
                    return;
                }
                DisplayAlert(NModels.title, MyWebEngine.Create(nameEntry.Text, passEntry.Text), NModels.btnOk);
            }
            catch (Exception ex)
            {
                DisplayAlert(NModels.errorTitle, ex.Message, NModels.btnOk);
            }
        }
    }
}