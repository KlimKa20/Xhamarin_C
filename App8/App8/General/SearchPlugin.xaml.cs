using System;
using Xamarin.Forms;

namespace App8
{
    public delegate void SearchPluginEventHandler(string text);
    public partial class SearchPlugin : ContentView
    {
        public event SearchPluginEventHandler Search;

        public SearchPlugin()
        {
            InitializeComponent();
            Searchb.Text = Resource1.Search;
        }
        private void OnClicked(object sender, EventArgs e)
        {
            Search?.Invoke(searchEntry.Text);
        }
    }
}