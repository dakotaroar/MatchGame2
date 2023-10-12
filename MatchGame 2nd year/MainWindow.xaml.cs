﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame_2nd_year
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        int rightPicks = 0;
        int wrongPicks = 0;

        int bestTime = 100000;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");


            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - PLay Again?";
                
                if (tenthsOfSecondsElapsed < bestTime)
                {
                    bestTime = tenthsOfSecondsElapsed;
                    textBest.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
                }


            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
               "🐙", "🐙",
               "🦕", "🦕",
               "🐫", "🐫",
               "🕷", "🕷",
               "🦘", "🦘",
               "🦔", "🦔",
               "🐉", "🐉",
               "🐳", "🐳",

            };
            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Tag == null)
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
                
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;

        }


        private void UpdateStats()
        {
            textRight.Text = rightPicks.ToString();
            textWrong.Text = wrongPicks.ToString();
            float percent = (float)rightPicks / (float)(rightPicks + wrongPicks);
            textPercent.Text = percent.ToString();
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock; 
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text) 
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
                rightPicks++;
                UpdateStats();
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
                wrongPicks++;
                UpdateStats();
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
