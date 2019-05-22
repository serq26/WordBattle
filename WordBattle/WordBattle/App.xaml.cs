using System;
using WordBattle.WordProcessing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordBattle
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            WordProcessor.Initialize();

            MainPage = new NavigationPage(new MainPage());

#if DEBUG
            LiveReload.Init();
#endif
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}