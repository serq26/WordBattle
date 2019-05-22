using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WordBattle.WordProcessing
{
    public enum GameState
    {
        Win,
        Lose
    }

    public class Game : INotifyPropertyChanged
    {
        public int MaxScore { get; private set; }

        public int PlayerScore { get; private set; }

        public int AIScore { get; private set; }

        public string StartLetter { get; private set; }

        public ObservableCollection<ListModel> PlayerWords { get; private set; }

        public ObservableCollection<ListModel> AIWords { get; private set; }

        public string lastLetterForPlace = "";

        public Game(int maxScore)
        {
            MaxScore = maxScore;

            PlayerScore = 0;
            AIScore = 0;

            PlayerWords = new ObservableCollection<ListModel>();

            AIWords = new ObservableCollection<ListModel>();

        }

        /// <summary>
        /// AI plays the first move
        /// </summary>
        public void Start()
        {
            string word = WordProcessor.GetWord();
            StartLetter = word.LastLetter();
            AIWords.Add(new ListModel(word));
            BaloonSound();
        }

        void BaloonSound()
        {
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("balon.wav");
            player.Play();
        }

        /// <summary>
        /// AI plays a move
        /// </summary>
        public async void PlayAsAI()
        {
            if (CanPlay())
            {
                string word = WordProcessor.GetWord(StartLetter);
                StartLetter = word.LastLetter();

                if (StartLetter == "ğ")
                {
                    StartLetter = "g";
                }


                lastLetterForPlace = word.LastLetter();
                AIWords.Add(new ListModel(word));
                BaloonSound();
                DeletePreviousWords();

                AIScore += word.Length * 1;


            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new FinishPopup(GameState.Win));
            }
        }

        /// <summary>
        /// Player plays a move
        /// </summary>
        /// <param name="word"></param>
        public bool PlayAsPlayer(string word)
        {
            if (CanPlay())
            {
                bool valid = WordProcessor.IsValid(word, StartLetter);

                if (valid)
                {
                    StartLetter = word.LastLetter();

                    if (StartLetter == "ğ")
                    {
                        StartLetter = "g";
                    }

                    PlayerWords.Add(new ListModel(word));
                   
                    DeletePreviousWords();

                    PlayerScore += word.Length * 1;

                }

                PlayAsAI();

                return valid;
            }
            else
            {
                PopupNavigation.Instance.PushAsync(new FinishPopup(GameState.Lose));
            }

            return false;
        }

        bool CanPlay()
        {
            return PlayerScore < MaxScore && AIScore < MaxScore;
        }


        void DeletePreviousWords()
        {
            if(PlayerWords.Count > 6)
            {
                PlayerWords.RemoveAt(0);
            }

            if (AIWords.Count > 6)
            {
                AIWords.RemoveAt(0);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    public static class MyExtensions
    {
        public static string LastLetter(this string word)
        {
            return word.Substring(word.Length - 1);
        }
    }


}