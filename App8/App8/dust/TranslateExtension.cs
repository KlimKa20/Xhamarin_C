﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Resources;
using System.Globalization;
using System.Reflection;
namespace App8
{
    //[ContentProperty("Text")]
    //public class TranslateExtension : IMarkupExtension
    //{
    //    readonly CultureInfo ci;
    //    const string ResourceId = "App8.Resource1.en";

    //    public TranslateExtension()
    //    {
    //        ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

    //        ci = new CultureInfo("App8.Resource1.en");
    //    }

    //    public string Text { get; set; }

    //    public object ProvideValue(IServiceProvider serviceProvider)
    //    {
    //        if (Text == null)
    //            return "";

    //        ResourceManager resmgr = new ResourceManager(ResourceId,
    //                    typeof(TranslateExtension).GetTypeInfo().Assembly);

    //        var translation = resmgr.GetString(Text, ci);

    //        if (translation == null)
    //        {
    //            translation = Text;
    //        }
    //        return translation;
    //    }
    //}
}
