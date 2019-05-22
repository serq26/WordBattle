using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBattle.WordProcessing;
using Xamarin.Forms;

namespace WordBattle
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {    
        

        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);


        }

        private async void PlayButton_Clicked(object sender, EventArgs e)
        {
            LoadSound();

            await Navigation.PushAsync(new GamePage());
            Navigation.RemovePage(this);

        }   
        
        void LoadSound()
        {     
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("button.flac");
            player.Play();
        }
    }
}