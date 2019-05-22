using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBattle.WordProcessing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordBattle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FinishPopup : PopupPage
	{
		public FinishPopup (GameState gameState)
		{
			InitializeComponent ();

            if(gameState == GameState.Win)
            {
                gameStateImage.Source = "win";
            }
            else
            {
                gameStateImage.Source = "lose";
            }
		}
        void ButtonSound()
        {
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("button.flac");
            player.Play();
        }

        private async void Play_Clicked(object sender, EventArgs e)
        {
            ButtonSound();
            await PopupNavigation.Instance.PopAsync(true);
        }

        private void Exit_Clicked(object sender, EventArgs e)
        {
            ButtonSound();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private async void Restart_Clicked(object sender, EventArgs e)
        {
            ButtonSound();
            await Navigation.PushAsync(new GamePage());
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}