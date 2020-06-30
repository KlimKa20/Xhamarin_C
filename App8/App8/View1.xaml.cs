using Android.Content;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App8
{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class View1 : ContentPage
    {
        public View1()
        {
            InitializeComponent();
            pic.SelectedItem = 1;
            tw.BorderColor = Color.FromHex(MainPage.when1);
            tw.Text = Resource1.apply;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, "file1.json");
            if (pic.SelectedIndex == -1)
                await DisplayAlert(Resource1.Alert, Resource1.The_field_is_not_selected, Resource1.OK);
            else
            {
                if (pic.Items[pic.SelectedIndex] == "ActionBar")
                {
                    MainPage.when1 = HslColorViewModel.w.ToHex();
                    if (!File.Exists(path))
                    {
                        App.myCollection.Options.ColorBar = HslColorViewModel.w.ToHex();
                        App.myCollection.Options.Background = Color.White.ToHex();
                        App.myCollection.Options.Languge = Resource1.lun;
                        string serialized = JsonConvert.SerializeObject(App.myCollection);
                        File.AppendAllText(path, serialized);
                    }
                    else
                    {
                        MyMusic newMusic = JsonConvert.DeserializeObject<MyMusic>(File.ReadAllText(path));
                        newMusic.Options.Languge = Resource1.lun;
                        newMusic.Options.ColorBar = HslColorViewModel.w.ToHex();
                        newMusic.Options.Background = MainPage.when2;
                        string serializednew = JsonConvert.SerializeObject(newMusic);
                        File.Delete(path);
                        File.AppendAllText(path, serializednew);
                    }

                }
                else
                {
                    MainPage.when2 = HslColorViewModel.w.ToHex();
                    if (!File.Exists(path))
                    {
                        App.myCollection.Options.ColorBar = "A69272FF";
                        App.myCollection.Options.Background = HslColorViewModel.w.ToHex();
                        App.myCollection.Options.Languge = Resource1.lun;
                        string serialized = JsonConvert.SerializeObject(App.myCollection);
                        File.AppendAllText(path, serialized);
                    }
                    else
                    {
                        MyMusic newMusic = JsonConvert.DeserializeObject<MyMusic>(File.ReadAllText(path));
                        newMusic.Options.Languge = Resource1.lun;
                        newMusic.Options.ColorBar = MainPage.when1;
                        newMusic.Options.Background = HslColorViewModel.w.ToHex();

                        string serializednew = JsonConvert.SerializeObject(newMusic);
                        File.Delete(path);
                        File.AppendAllText(path, serializednew);
                    }// Documents folder
                }
            }

        }
    }
    public class HslColorViewModel : INotifyPropertyChanged
    {
        Color color;
        string name;

        public event PropertyChangedEventHandler PropertyChanged;
        public static Color w;
        public double Hue
        {
            set
            {
                if (color.Hue != value)
                {
                    Color = Color.FromHsla(value, color.Saturation, color.Luminosity);
                }
            }
            get
            {
                return color.Hue;
            }
        }

        public double Saturation
        {
            set
            {
                if (color.Saturation != value)
                {
                    Color = Color.FromHsla(color.Hue, value, color.Luminosity);
                }
            }
            get
            {
                return color.Saturation;
            }
        }

        public double Luminosity
        {
            set
            {
                if (color.Luminosity != value)
                {
                    Color = Color.FromHsla(color.Hue, color.Saturation, value);
                }
            }
            get
            {
                return color.Luminosity;
            }
        }

        public Color Color
        {
            set
            {
                if (color != value)
                {
                    w = value;
                    color = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Hue"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Saturation"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Luminosity"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color"));
                    Name = NamedColor.GetNearestColorName(color);
                }
            }
            get
            {
                return color;
            }
        }

        public string Name
        {
            private set
            {
                if (name != value)
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
            get
            {
                return name;
            }
        }
    }
    public class NamedColor : IEquatable<NamedColor>, IComparable<NamedColor>
    {
        // Instance members
        private NamedColor()
        {
        }

        public string Name { private set; get; }

        public string FriendlyName { private set; get; }

        public Color Color { private set; get; }

        public string RgbDisplay { private set; get; }

        public bool Equals(NamedColor other)
        {
            return Name.Equals(other.Name);
        }

        public int CompareTo(NamedColor other)
        {
            return Name.CompareTo(other.Name);
        }

        // Static members
        static NamedColor()
        {
            List<NamedColor> all = new List<NamedColor>();
            StringBuilder stringBuilder = new StringBuilder();

            // Loop through the public static fields of the Color structure.
            foreach (FieldInfo fieldInfo in typeof(Color).GetRuntimeFields())
            {
                if (fieldInfo.IsPublic &&
                    fieldInfo.IsStatic &&
                    fieldInfo.FieldType == typeof(Color))
                {
                    // Convert the name to a friendly name.
                    string name = fieldInfo.Name;
                    stringBuilder.Clear();
                    int index = 0;

                    foreach (char ch in name)
                    {
                        if (index != 0 && Char.IsUpper(ch))
                        {
                            stringBuilder.Append(' ');
                        }
                        stringBuilder.Append(ch);
                        index++;
                    }

                    // Instantiate a NamedColor object.
                    Color color = (Color)fieldInfo.GetValue(null);

                    NamedColor namedColor = new NamedColor
                    {
                        Name = name,
                        FriendlyName = stringBuilder.ToString(),
                        Color = color,
                        RgbDisplay = String.Format("{0:X2}-{1:X2}-{2:X2}",
                                                    (int)(255 * color.R),
                                                    (int)(255 * color.G),
                                                    (int)(255 * color.B)),
                    };

                    // Add it to the collection.
                    all.Add(namedColor);
                }
            }
            all.TrimExcess();
            all.Sort();
            All = all;
        }

        public static IList<NamedColor> All { private set; get; }

        public static NamedColor Find(string name)
        {
            return ((List<NamedColor>)All).Find(nc => nc.Name == name);
        }

        public static string GetNearestColorName(Color color)
        {
            double shortestDistance = 1000;
            NamedColor closestColor = null;

            foreach (NamedColor namedColor in NamedColor.All)
            {
                double distance = Math.Sqrt(Math.Pow(color.R - namedColor.Color.R, 2) +
                                            Math.Pow(color.G - namedColor.Color.G, 2) +
                                            Math.Pow(color.B - namedColor.Color.B, 2));

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestColor = namedColor;
                }

            }
            return closestColor.Name;
        }
    }

}