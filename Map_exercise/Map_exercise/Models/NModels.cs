using Xamarin.Essentials;
using Xamarin.Forms;

using Plugin.Geolocator;

namespace Map_exercise.Models
{
    public static class NModels
    {
        public const string title          = "Info";
        public const string errorTitle     = "Error";
        public const string btnOk          = "Ok";
        public const string wrongPass      = "Wrong password.";
        public const string wrongPass1     = "Mismatched password confirmation.";
        public const string missingDetails = "Please fill in the blank details.";
        public const string confirmPass    = "Please reenter your new password.";
        public const string success        = "Success.";
        public const string loginFailed    = "Wrong username or password.";
        public const string existUsername  = "Username exists.";
        public const string noUsername     = "Username does not exist.";
        public const string relogin        = "Please log in.";
        public const string erroccur       = "Error occured.";

        public static bool CheckInternetConnection()
        {
            // check internet connection
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.None)
            {
                DependencyService.Get<IToast>().Show("No internet connection.");
                return false;
            }
            return true;
        }
    }
}