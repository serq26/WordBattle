using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WordBattle.WordProcessing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordBattle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GamePage : ContentPage, INotifyPropertyChanged
	{
        Game game;


        public GamePage ()
		{
			InitializeComponent ();

            NavigationPage.SetHasNavigationBar(this, false);
           
            game = new Game(100);

            game.Start();

            RefleshItemSource();

            EntryFocus();

            UpdateScore();
        }

        void ButtonSound()
        {
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("button.flac");
            player.Play();
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            ButtonSound();

            if (EntryText.Text != null && EntryText.Text.Length > 1)
            {
                bool valid = game.PlayAsPlayer(EntryText.Text.ToLower());
                
                if(!valid)                
                {
                    warning.IsVisible = true;
                }
                else
                {
                    warning.IsVisible = false;
                }

                EntryFocus();

                UpdateScore();
            }

        }

        void EntryFocus()
        {
            EntryText.Text = "";

            EntryText.Text = game.lastLetterForPlace;

            EntryText.Focus();

            EntryText.CursorPosition = 1;
        }

        void UpdateScore()
        {
            playerScore.Text = game.PlayerScore.ToString();

            AIScore.Text = game.AIScore.ToString();
        }

        void RefleshItemSource()
        {
            PlayerListview.ItemsSource = game.PlayerWords;

            AIListview.ItemsSource = game.AIWords;
        }     

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {

              await PopupNavigation.Instance.PushAsync(new PopupView());
                
            });

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}