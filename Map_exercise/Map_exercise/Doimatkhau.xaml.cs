using System;

using Rg.Plugins.Popup.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Map_exercise.Models;

namespace Map_exercise
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Doimatkhau
    {
        string _name = SigninPage.NameUser;
        string _pass = SigninPage.PassUser;

        private void Undo_Button_Clicked(object sender, EventArgs e) => PopupNavigation.PopAsync(true);

        private void Confirm_Button_Clicked(object sender, EventArgs e)
        {
            string p   = pass.Text;
            string pn  = passnew.Text;
            string pn1 = passnew1.Text;
            if (string.IsNullOrWhiteSpace(pn)|| string.IsNullOrWhiteSpace(pn1))
            {
                DisplayAlert(NModels.title, NModels.missingDetails, NModels.btnOk);
                return;
            }
            if (p != _pass)
            {
                DisplayAlert(NModels.title, NModels.wrongPass, NModels.btnOk);
                return;
            }
            if (pn == pn1)
            {
                try
                {
                    MyWebEngine.Edit(_name, "password", pn);
                    DependencyService.Get<IToast>().Show(NModels.success);
                    PopupNavigation.PopAsync(true);
                }
                catch (Exception ex)
                {
                    DisplayAlert(NModels.errorTitle, ex.Message, NModels.btnOk);
                }
            }
            else DisplayAlert(NModels.title, NModels.wrongPass1, NModels.btnOk);
        }

        public Doimatkhau()
        {
            InitializeComponent();
        }
    }
}