using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Map_exercise.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Toast_Android))]

namespace Map_exercise.Droid
{
    public class Toast_Android : IToast
    {
        public void Show(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}