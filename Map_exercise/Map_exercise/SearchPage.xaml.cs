using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Map_exercise.Models;

namespace Map_exercise
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void Btnfind_Clicked(object sender, EventArgs e)
        {
            try
            {
                searchlist.ItemsSource = MyWebEngine.Find(txtkeyword.Text);
            }
            catch (Exception ex)
            {
                //Device.BeginInvokeOnMainThread(() =>
                DisplayAlert(NModels.errorTitle, ex.Message, NModels.btnOk);
            }
        }

        private void Searchlist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Map_Perform.addedUser = e.SelectedItem as string;
        }

        private void Btnback_Clicked(object sender, EventArgs e) => Navigation.PopAsync();
    }
}