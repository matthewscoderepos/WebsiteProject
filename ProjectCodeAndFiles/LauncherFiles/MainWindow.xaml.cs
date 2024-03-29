﻿using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;
using Point = System.Windows.Point;

namespace VG_Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public Library Curlibrary { get; private set; }
        public bool locked;
        string prevTitle = "";

        public MainWindow()
        {
            InitializeComponent();
            Curlibrary = new Library();
            Curlibrary.InitLib();
            if (Curlibrary.gameList.Count < 1)
            {
                //Empty game Library, launch account setup
                AccountSetup accountSetup = new AccountSetup(Curlibrary);
                Console.WriteLine("NO GAMES, INIT CONDITIONS");

                App.Current.MainWindow.Hide();
                accountSetup.Show();
                //Continue with setup procedures
            }
            else
            {
                if (Properties.Settings.Default.ChildEnabled)
                {
                    //Go to Login Screen
                    logIn();
                    locked = Properties.Settings.Default.ParentalLockEngaged;
                    if (locked)
                        CreateButtons(true);
                    else
                        CreateButtons(false);
                }
                else
                {
                    CreateButtons(false);
                }
            }
            //  DispatcherTimer setup
            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //debug stuff
            if(prevTitle != GetActiveWindowTitle())
            {
                prevTitle = GetActiveWindowTitle();
                Console.WriteLine(prevTitle + " detected as new title.");

            }

            string titleName = GetActiveWindowTitle();
            foreach(Game g in Curlibrary.gameList)
            {
                if (titleName != null)
                {
                    if (titleName != null && g.name != null)
                    {
                        if (CleanName(titleName).Contains(CleanName(g.name)))
                        {
                            g.time++;
                        }
                    }
                }
            }
        }

        public void CreateButtons(bool locked)
        {
            gameWrapPanel.Children.Clear();
            foreach (Game game in Curlibrary.gameList)
            {
                if (!(Int32.Parse(game.parentLock) == 1 && locked == true))
                {
                    try
                    {
                        Button btn = new Button();
                        btn.Name = CleanName(game.name); //replace this with an identitier ie: game.id
                        btn.Tag = game;
                        if (!File.Exists(game.image))
                        {
                            try
                            {
                                WebClient wc = new WebClient();

                                wc.Headers.Add("Authorization", "Bearer 47af29a9fb8d5d08ba57a06f2bc15261");

                                var json = wc.DownloadString("https://www.steamgriddb.com/api/v2/search/autocomplete/" + game.name.ToLower());

                                //Choose the first game in the list. The first one most closely matches the name
                                dynamic idJson = JsonConvert.DeserializeObject(json);
                                dynamic firstGameInArray = idJson["data"][0];
                                string gameId = firstGameInArray.id;
                                json = wc.DownloadString("https://www.steamgriddb.com/api/v2/grids/game/" + gameId);
                                dynamic imageJson = JsonConvert.DeserializeObject(json);

                                //Choose the first image in the list. We can obviously choose an image based on its properties.
                                //For instance, we could check::::  imageJson["data"][0]["style"] == "blurred"
                                //and if thats not true we could go down the image list
                           
                                    string imageUrl = imageJson["data"][0]["url"];
                                    game.image = "../../Resources/" + CleanName(game.name).ToLower() + ".png";
                                    Console.WriteLine("Pulled image " + game.name);
                                    wc.Headers.Clear();
                                    wc.DownloadFile(imageUrl, "../../Resources/" + CleanName(game.name).ToLower() + ".png");
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine("There was an error pulling the image for " + game.name);
                                game.image = "../../Resources/DefaultGameImage.PNG";
                            }
                         
                            ImageBrush myBrush = new ImageBrush();
                            myBrush.ImageSource = new BitmapImage(new Uri(game.image, UriKind.Relative));
                            btn.Background = myBrush;
                        }
                        else
                        {
                            Console.WriteLine("Found File for game " + game.name);
                            ImageBrush myBrush = new ImageBrush();
                            myBrush.ImageSource = new BitmapImage(new Uri(game.image, UriKind.Relative));
                            btn.Background = myBrush;
                        }
                        if(game.image == "../../Resources/DefaultGameImage.PNG")
                        {
                            //If we are using the default Logo, display the name
                            btn.Content = game.name;
                        }
                        //"#4CFFFFFF"
                        //Static values. All buttons should have the same values for these.
                        btn.Width = 360;
                        btn.Height = 160;
                        btn.Margin = new Thickness(8);
                        btn.HorizontalContentAlignment = HorizontalAlignment.Center;
                        btn.VerticalContentAlignment = VerticalAlignment.Bottom;
                        btn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7FFFFFFF"));
                        btn.FontSize = 24;
                        btn.FontWeight = FontWeights.SemiBold;
                        btn.Style = Resources["noHighlightButton"] as Style;

                        //This lets us click the button. All of the buttons will share a function called Button_Click so we will have to be creative.
                        //Theres no way we can create a method for each new button, at least not that I know of. 
                        btn.Click += Button_Click;

                        if (game.name != "Steamworks Common Redistributables")
                            gameWrapPanel.Children.Add(btn);

                    }
                    catch (Exception e)
                    {
                        //Let the user choose their own image here
                        Console.WriteLine("There was an error creating the button, not related to image search.");
                    }
                }
            }
        }

        public string CleanName(string str)
        {
            str = str.ToLower();
            str = str.Replace(" ", "");
            str = str.Replace(":", "");
            str = str.Replace(",", "");
            str = str.Replace("'", "");
            str = str.Replace(".", "");
            //keep adding as things break
            //probably should replace this with something that does it better and faster
            //all we really want is letters and numbers
            return str;
        }

        public void logIn()
        {
            LogInService li = new LogInService();
            li.ShowDialog();
        }
       
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button; //lets us edit the button that sent the function call


            GameScreen gs = new GameScreen();
            Game game = (Game)btn.Tag;
            gs.Tag = game;
            gs.playButton.Tag = game;
            gs.settingsButton.Tag = game;
            TimeSpan t = TimeSpan.FromSeconds(game.time);
            gs.hoursLabel.Content = t.ToString(@"hh\:mm\:ss");



            gs.Name = "gs";
            gs.gameName.Content = game.name;

            //Setting up the background image
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(game.image, UriKind.Relative);
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            gs.image.Source = bitmapImage;

            var graphics = Graphics.FromHwnd(IntPtr.Zero);
            var scaleWidth = (int)(graphics.DpiX / 96);
            var scaleHeight = (int)(graphics.DpiY / 96);
            //location of gamescreen
            Point point = btn.PointToScreen(new Point(0, 0));
            if ((point.X + gs.Width) > (mainWindow.Left + mainWindow.Width))
            {
                gs.Left = ((point.X - (gs.Width - btn.Width))/(scaleWidth));
                gs.Top = ((point.Y + btn.Height) / (scaleHeight))+2;
            }
            else if (point.X - 90 < mainWindow.Left)
            {
                gs.Left = (point.X / (scaleWidth));
                gs.Top = ((point.Y + btn.Height) / (scaleHeight))+2;
            }
            else
            {
                gs.Left = ((point.X - 90) / (scaleWidth));
                gs.Top = ((point.Y + btn.Height) / (scaleHeight))+2;
            }
            //this will keep the gamescreens from going off the bottom of the window
            if ((point.Y + gs.Height + btn.Height ) > (mainWindow.Top + mainWindow.Height))
            {
                gs.Top = ((point.Y - gs.Height) / (scaleHeight))-2;
            }
            gs.Show();
            clickReciever.Visibility = Visibility.Visible;
        }

        private void ClickReciever_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (Window w in App.Current.Windows)
            {
                if (w.Name.Equals("gs"))
                {
                    w.Close();
                }
            }
            clickReciever.Visibility = Visibility.Hidden;
            
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
            Curlibrary.SaveJson(Curlibrary);
            System.Windows.Application.Current.Shutdown();
        }

        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            foreach (Window w in App.Current.Windows)
            {
                if (w.Name.Equals("gs"))
                {
                    w.Close();
                }
            }
            clickReciever.Visibility = Visibility.Hidden;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (Window w in App.Current.Windows)
            {
                if (w.Name.Equals("gs"))
                {
                    w.Close();
                }
            }
            clickReciever.Visibility = Visibility.Hidden;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button; 
            Point point = btn.PointToScreen(new Point(0, 0));
            clickReciever.Visibility = Visibility.Visible;
            clickReciever.Opacity = 1;

            MenuScreen ms = new MenuScreen(Curlibrary);
            ms.Name = "gs";
            ms.Top = point.Y + btn.Height;
            ms.Left = point.X + btn.Width;
            ms.ShowDialog();
            clickReciever.Visibility = Visibility.Hidden;
        }

        public void HideGame(string name)
        {
            Button remove = new Button();
            foreach(Button b in gameWrapPanel.Children)
            {
                if (b.Name != null && name != null)
                {
                    if (CleanName(b.Name) == CleanName(name))
                    {
                        remove = b;
                    }
                }
            }
            gameWrapPanel.Children.Remove(remove);
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
    }
}