using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Casino;

namespace Casino
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> userPass = new Dictionary<string, string>();
        Dictionary<string, int> userChips = new Dictionary<string, int>();
        private int chipCount = 777;
        private string username = "Guest";


        public MainWindow()
        {
            ReadFile();
            InitializeComponent();
        }
        
        private void Blackjack_button_Click(object sender, RoutedEventArgs e)
        {
            Blackjack bj = new Blackjack(username, chipCount);
            this.Hide();
            bj.ShowDialog();
            this.Show();
        }

        private void RouletteButton_Click(object sender, RoutedEventArgs e)
        {
            Roulette rl = new Roulette(username, chipCount);
            this.Hide();
            rl.ShowDialog();
            this.Show();
        }

        private void PokerButton_Click(object sender, RoutedEventArgs e)
        {
            Poker pk = new Poker(username, chipCount);
            this.Hide();
            pk.ShowDialog();
            this.Show();
        }

        private void ReadFile()
        {
            userPass.Clear();
            userChips.Clear();
            string line;
            StreamReader sr = new StreamReader("C:\\Users\\thalrukt\\Dropbox\\Casino\\casinoApp\\Users\\users.txt"); //hardcoded
            line = sr.ReadLine();
            while (line != null)
            {
                string[] userInfo = line.Split(',');
                int chips;
                bool success = Int32.TryParse(userInfo[2], out chips);
                userPass.Add(userInfo[0], userInfo[1]);
                userChips.Add(userInfo[0], chips);
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
            Console.ReadLine();

        }

        public void Save()
        {
            //for each in userPass
            Console.WriteLine("Save run on " + casinoApp.Properties.Settings.Default.username + " , " + casinoApp.Properties.Settings.Default.chips);
            if (!username.Equals("Guest"))
            {
                int i = 0;
                Dictionary<string, int>.ValueCollection chipValues = userChips.Values;
                int[] chipArray = new int[chipValues.Count];
                chipValues.CopyTo(chipArray, 0);
                StreamWriter sw = new StreamWriter("C:\\Users\\thalrukt\\Dropbox\\Casino\\casinoApp\\Users\\users.txt");
                foreach (KeyValuePair<string, string> kvp in userPass)
                {
                    if (kvp.Key.Equals(casinoApp.Properties.Settings.Default.username))
                        sw.WriteLine(kvp.Key + ',' + kvp.Value + ',' + casinoApp.Properties.Settings.Default.chips);
                    else
                        sw.WriteLine(kvp.Key + ',' + kvp.Value + ',' + chipArray[i]);
                    i++;
                }
                sw.Close();
            }
            casinoApp.Properties.Settings.Default.Save();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (userPass.TryGetValue(usernameTextbox.Text, out string pass))
            {
                if (passwordBox.Password.Equals(pass))
                {
                    userChips.TryGetValue(usernameTextbox.Text, out int chips);
                    usernameTextbox.Opacity = 0;
                    passwordLabel.Visibility = Visibility.Hidden;
                    passwordBox.Visibility = Visibility.Hidden;
                    loginButton.Visibility = Visibility.Hidden;
                    username = usernameTextbox.Text;
                    casinoApp.Properties.Settings.Default.username = username;
                    chipCount = chips;
                    casinoApp.Properties.Settings.Default.chips = chipCount;
                    usernameLabel.Content = "Logged in as " + usernameTextbox.Text;
                }
                else
                {
                    usernameTextbox.Background = Brushes.PaleVioletRed;
                    passwordBox.Background = Brushes.PaleVioletRed;
                }
            }
            else
            {
                usernameTextbox.Background = Brushes.PaleVioletRed;
                passwordBox.Background = Brushes.PaleVioletRed;
            }
            casinoApp.Properties.Settings.Default.Save();
        }

        private void UsernameTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            usernameTextbox.Background = Brushes.WhiteSmoke;
            passwordBox.Background = Brushes.WhiteSmoke;
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            usernameTextbox.Background = Brushes.WhiteSmoke;
            passwordBox.Background = Brushes.WhiteSmoke;
        }

        private void Menu_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Save();
            casinoApp.Properties.Settings.Default.Save();
        }

        private void Menu_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!username.Equals("Guest"))
            {
                username = casinoApp.Properties.Settings.Default.username;
                chipCount = casinoApp.Properties.Settings.Default.chips;
            }
        }
    }
}
