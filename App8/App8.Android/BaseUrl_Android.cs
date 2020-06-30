using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App8.Droid;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl_Android))]
namespace App8.Droid
{
    public class BaseUrl_Android : account.IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}