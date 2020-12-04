using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibVLCSharpContentView
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Appearing += MainPage_Appearing;
            Disappearing += MainPage_Disappearing;
        }

        private void MainPage_Disappearing(object sender, EventArgs e)
        {
            Player.Stop(); 
        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            Player.Start();
        }
    }
}
