using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Casino
{
    /// <summary>
    /// Interaction logic for Roulette.xaml
    /// </summary>
    /// 

    public class Bet
    {
        public Bet(int space, int bet, string btn)
        {
            spaces.Add(space);
            amount = bet;
            button = btn;
        }
        public Bet(int s1, int s2, int bet, string btn)
        {
            spaces.Add(s1);
            spaces.Add(s2);
            amount = bet;
            button = btn;

        }
        public Bet(int s1, int s2, int s3, int bet, string btn)
        {
            spaces.Add(s1);
            spaces.Add(s2);
            spaces.Add(s3);
            amount = bet;
            button = btn;

        }
        public Bet(int s1, int s2, int s3, int s4, int bet, string btn)
        {
            spaces.Add(s1);
            spaces.Add(s2);
            spaces.Add(s3);
            spaces.Add(s4);
            amount = bet;
            button = btn;

        }
        public Bet(int s1, int s2, int s3, int s4, int s5, int s6, int bet, string btn)
        {
            spaces.Add(s1);
            spaces.Add(s2);
            spaces.Add(s3);
            spaces.Add(s4);
            spaces.Add(s5);
            spaces.Add(s6);
            amount = bet;
            button = btn;

        }
        public int amount;
        public List<int> spaces = new List<int>();
        public string button;
    }


    public partial class Roulette : Window
    {
        static public double BUCKETSIZE = 9.47368421053;
        public enum BetList { FIRST12 = 100, SECOND12, THIRD12, EVEN, ODD, RED, BLACK, ONETO18, NINETEENTO36, HIGHWAY1, HIGHWAY2, HIGHWAY3 };
        public List<int> redNums = new List<int>() { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
        public List<int> blackNums = new List<int>() { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
        public List<int> buckets = new List<int>() { 17, 32, 20, 7, 11, 30, 26, 9, 28, 0, 2, 14, 35, 23, 4, 16, 33, 21, 6, 18, 31, 19, 8, 12, 29, 25, 10, 27, 38, 1, 13, 36, 24, 3, 15, 34, 22, 5 };
        private string userName = "Guest";
        private int chipCount = 1000;
        List<Bet> bets = new List<Bet>();



        public Roulette()
        {
            InitializeComponent();
            usernameLabel.Content += userName;
            chipTotalLabel.Content += chipCount.ToString();
        }
        public Roulette(string uName, int chips)
        {
            InitializeComponent();
            userName = uName;
            usernameLabel.Content += uName;
            chipCount = chips;
            chipTotalLabel.Content += chipCount.ToString();
        }

        private int CheckBet()
        {
            string intString = bet.Text;
            int x = 0;
            if (!Int32.TryParse(intString, out x))
            {
                x = 5;
                bet.Text = "5";
            }
            if (x > 500 || x > chipCount || x < 5)
            {
                x = 5;
                bet.Text = "5";
            }
            return x;
        }

        private void chipCountUpdate(int betAmount, double multiplier) //positive mult means win, use negative when setting up the table
        {
            chipCount += (int)Math.Floor(betAmount * multiplier);
            chipTotalLabel.Content = "Chip Count: " + chipCount;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Control ctrl in numGrid.Children)
            {
                ctrl.Opacity = 0;
            }

            //NEED TO GO THROUGH THE BET LIST AND GIVE THE MONEY BACK
            foreach (Bet bet in bets)
            {
                chipCount += bet.amount;
            }
            chipTotalLabel.Content = "Chip Count: " + chipCount;
            bets.Clear();
        }

        private void SpinButton_Click(object sender, RoutedEventArgs e)
        {


        }

        private void SpinBoard(int angle, TimeSpan duration)
        {
            var ease = new PowerEase { EasingMode = EasingMode.EaseOut };

            //DoubleAnimation(FromValue. ToValue, Duration)
            DoubleAnimation myanimation = new DoubleAnimation
                    (0, angle, duration);

            //Adding Power ease to the animation
            myanimation.EasingFunction = ease;

            RotateTransform rt = new RotateTransform();

            //  "img" is Image added in XAML
            spinButton.RenderTransform = rt;
            spinButton.RenderTransformOrigin = new Point(0.5, 0.5);
            rt.BeginAnimation(RotateTransform.AngleProperty, myanimation);
        }
        private void SpinBall(int angle, TimeSpan duration)
        {
            var ease = new PowerEase { EasingMode = EasingMode.EaseOut };

            //DoubleAnimation(FromValue. ToValue, Duration)
            DoubleAnimation myanimation = new DoubleAnimation
                    (0, -angle, duration);

            //Adding Power ease to the animation
            myanimation.EasingFunction = ease;

            RotateTransform rt = new RotateTransform();

            //  "img" is Image added in XAML
            dropBallButton.RenderTransform = rt;
            dropBallButton.RenderTransformOrigin = new Point(0.5, 0.5);
            rt.BeginAnimation(RotateTransform.AngleProperty, myanimation);
        }

        #region bets
        #region straight bets (x36 bet)
        private void N00_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n00.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(37, CheckBet(), btn.Name)); }
        private void N0_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n0.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(0, CheckBet(), btn.Name)); }
        private void N1_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n1.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(1, CheckBet(), btn.Name)); }
        private void N2_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n2.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(2, CheckBet(), btn.Name)); }
        private void N3_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n3.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(3, CheckBet(), btn.Name)); }
        private void N4_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n4.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(4, CheckBet(), btn.Name)); }
        private void N5_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n5.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(5, CheckBet(), btn.Name)); }
        private void N6_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n6.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(6, CheckBet(), btn.Name)); }
        private void N7_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n7.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(7, CheckBet(), btn.Name)); }
        private void N8_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n8.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(8, CheckBet(), btn.Name)); }
        private void N9_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n9.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(9, CheckBet(), btn.Name)); }
        private void N10_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n10.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(10, CheckBet(), btn.Name)); }
        private void N11_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n11.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(11, CheckBet(), btn.Name)); }
        private void N12_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n12.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(12, CheckBet(), btn.Name)); }
        private void N13_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n13.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(13, CheckBet(), btn.Name)); }
        private void N14_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n14.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(14, CheckBet(), btn.Name)); }
        private void N15_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n15.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(15, CheckBet(), btn.Name)); }
        private void N16_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n16.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(16, CheckBet(), btn.Name)); }
        private void N17_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n17.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(17, CheckBet(), btn.Name)); }
        private void N18_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n18.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(18, CheckBet(), btn.Name)); }
        private void N19_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n19.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(19, CheckBet(), btn.Name)); }
        private void N20_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n20.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(20, CheckBet(), btn.Name)); }
        private void N21_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n21.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(21, CheckBet(), btn.Name)); }
        private void N22_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n22.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(22, CheckBet(), btn.Name)); }
        private void N23_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n23.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(23, CheckBet(), btn.Name)); }
        private void N24_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n24.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(24, CheckBet(), btn.Name)); }
        private void N25_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n25.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(25, CheckBet(), btn.Name)); }
        private void N26_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n26.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(26, CheckBet(), btn.Name)); }
        private void N27_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n27.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(27, CheckBet(), btn.Name)); }
        private void N28_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n28.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(28, CheckBet(), btn.Name)); }
        private void N29_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n29.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(29, CheckBet(), btn.Name)); }
        private void N30_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n30.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(30, CheckBet(), btn.Name)); }
        private void N31_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n31.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(31, CheckBet(), btn.Name)); }
        private void N32_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n32.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(32, CheckBet(), btn.Name)); }
        private void N33_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n33.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(33, CheckBet(), btn.Name)); }
        private void N34_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n34.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(34, CheckBet(), btn.Name)); }
        private void N35_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n35.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(35, CheckBet(), btn.Name)); }
        private void N36_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n36.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(36, CheckBet(), btn.Name)); }
        #endregion

        #region split bets (x18 bet)

        #region vertical
        private void N0n00_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n0n00.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(37, 0, CheckBet(), btn.Name)); }
        private void N1n2_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n1n2.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(1, 2, CheckBet(), btn.Name)); }
        private void N2n3_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n2n3.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(2, 3, CheckBet(), btn.Name)); }
        private void N4n5_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n4n5.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(4, 5, CheckBet(), btn.Name)); }
        private void N5n6_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n5n6.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(5, 6, CheckBet(), btn.Name)); }
        private void N7n8_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n7n8.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(7, 8, CheckBet(), btn.Name)); }
        private void N8n9_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n8n9.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(8, 9, CheckBet(), btn.Name)); }
        private void N10n11_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n10n11.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(10, 11, CheckBet(), btn.Name)); }
        private void N11n12_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n11n12.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(11, 12, CheckBet(), btn.Name)); }
        private void N13n14_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n13n14.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(13, 14, CheckBet(), btn.Name)); }
        private void N14n15_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n14n15.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(14, 15, CheckBet(), btn.Name)); }
        private void N16n17_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n16n17.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(16, 17, CheckBet(), btn.Name)); }
        private void N17n18_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n17n18.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(17, 18, CheckBet(), btn.Name)); }
        private void N19n20_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n19n20.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(19, 20, CheckBet(), btn.Name)); }
        private void N20n21_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n20n21.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(20, 21, CheckBet(), btn.Name)); }
        private void N22n23_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n22n23.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(22, 23, CheckBet(), btn.Name)); }
        private void N23n24_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n23n24.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(23, 24, CheckBet(), btn.Name)); }
        private void N25n26_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n25n26.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(25, 26, CheckBet(), btn.Name)); }
        private void N26n27_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n26n27.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(26, 27, CheckBet(), btn.Name)); }
        private void N28n29_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n28n29.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(28, 29, CheckBet(), btn.Name)); }
        private void N29n30_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n29n30.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(29, 30, CheckBet(), btn.Name)); }
        private void N31n32_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n31n32.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(31, 32, CheckBet(), btn.Name)); }
        private void N32n33_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n32n33.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(32, 33, CheckBet(), btn.Name)); }
        private void N34n35_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n34n35.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(34, 35, CheckBet(), btn.Name)); }
        private void N35n36_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n35n36.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(35, 36, CheckBet(), btn.Name)); }
        #endregion

        #region horizontal
        private void N1n4_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n1n4.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(1, 4, CheckBet(), btn.Name)); }
        private void N2n5_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n2n5.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(2, 5, CheckBet(), btn.Name)); }
        private void N3n6_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n3n6.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(3, 6, CheckBet(), btn.Name)); }
        private void N4n7_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n4n7.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(4, 7, CheckBet(), btn.Name)); }
        private void N5n8_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n5n8.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(5, 8, CheckBet(), btn.Name)); }
        private void N6n9_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n6n9.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(6, 9, CheckBet(), btn.Name)); }
        private void N7n10_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n7n10.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(7, 10, CheckBet(), btn.Name)); }
        private void N8n11_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n8n11.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(8, 11, CheckBet(), btn.Name)); }
        private void N9n12_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n9n12.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(9, 12, CheckBet(), btn.Name)); }
        private void N10n13_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n10n13.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(10, 13, CheckBet(), btn.Name)); }
        private void N11n14_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n11n14.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(11, 14, CheckBet(), btn.Name)); }
        private void N12n15_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n12n15.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(12, 15, CheckBet(), btn.Name)); }
        private void N13n16_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n13n16.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(13, 16, CheckBet(), btn.Name)); }
        private void N14n17_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n14n17.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(14, 17, CheckBet(), btn.Name)); }
        private void N15n18_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n15n18.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(15, 18, CheckBet(), btn.Name)); }
        private void N16n19_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n16n19.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(16, 19, CheckBet(), btn.Name)); }
        private void N17n20_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n17n20.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(17, 20, CheckBet(), btn.Name)); }
        private void N18n21_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n18n21.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(18, 21, CheckBet(), btn.Name)); }
        private void N19n22_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n19n22.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(19, 22, CheckBet(), btn.Name)); }
        private void N20n23_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n20n23.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(20, 23, CheckBet(), btn.Name)); }
        private void N21n24_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n21n24.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(21, 24, CheckBet(), btn.Name)); }
        private void N22n25_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n22n25.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(22, 25, CheckBet(), btn.Name)); }
        private void N23n26_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n23n26.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(23, 26, CheckBet(), btn.Name)); }
        private void N24n27_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n24n27.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(24, 27, CheckBet(), btn.Name)); }
        private void N25n28_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n25n28.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(25, 28, CheckBet(), btn.Name)); }
        private void N26n29_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n26n29.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(26, 29, CheckBet(), btn.Name)); }
        private void N27n30_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n27n30.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(27, 30, CheckBet(), btn.Name)); }
        private void N28n31_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n28n31.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(28, 31, CheckBet(), btn.Name)); }
        private void N29n32_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n29n32.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(29, 32, CheckBet(), btn.Name)); }
        private void N30n33_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n30n33.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(30, 33, CheckBet(), btn.Name)); }
        private void N31n34_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n31n34.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(31, 34, CheckBet(), btn.Name)); }
        private void N32n35_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n32n35.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(32, 35, CheckBet(), btn.Name)); }
        private void N33n36_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n33n36.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(33, 36, CheckBet(), btn.Name)); }

        #endregion

        #endregion

        #region lane bets (x12 bet)
        private void Lane3_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;lane3.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(1, 2, 3, CheckBet(), btn.Name)); }
        private void Lane6_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;lane6.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(4, 5, 6, CheckBet(), btn.Name)); }
        private void Lane9_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;lane9.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(7, 8, 9, CheckBet(), btn.Name)); }
        private void Lane12_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;lane12.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(10, 11, 12, CheckBet(), btn.Name)); }
        private void Lane15_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;lane15.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(13, 14, 15, CheckBet(), btn.Name)); }
        private void Lane18_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;lane18.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(16, 17, 18, CheckBet(), btn.Name)); }
        private void Lane21_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;lane21.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(19, 20, 21, CheckBet(), btn.Name)); }
        private void Lane24_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;lane24.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(22, 23, 24, CheckBet(), btn.Name)); }
        private void Lane27_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;lane27.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(25, 26, 27, CheckBet(), btn.Name)); }
        private void Lane30_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;lane30.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(28, 29, 30, CheckBet(), btn.Name)); }
        private void Lane33_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;lane33.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(31, 32, 33, CheckBet(), btn.Name)); }
        private void Lane36_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;lane36.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(34, 35, 36, CheckBet(), btn.Name)); }
        #endregion

        #region street bets (x6 bet)
        private void Street3_6_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;street3_6.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(1, 2, 3, 4, 5, 6, CheckBet(), btn.Name)); }
        private void Street6_9_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;street6_9.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(4, 5, 6, 7, 8, 9, CheckBet(), btn.Name)); }
        private void Street9_12_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;street9_12.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(7, 8, 9, 10, 11, 12, CheckBet(), btn.Name)); }
        private void Street12_15_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;street12_15.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(10, 11, 12, 13, 14, 15, CheckBet(), btn.Name)); }
        private void Street15_18_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;street15_18.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(13, 14, 15, 16, 17, 18, CheckBet(), btn.Name)); }
        private void Street18_21_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;street18_21.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(16, 17, 18, 19, 20, 21, CheckBet(), btn.Name)); }
        private void Street21_24_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;street21_24.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(19, 20, 21, 22, 23, 24, CheckBet(), btn.Name)); }
        private void Street24_27_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;street24_27.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(22, 23, 24, 25, 26, 27, CheckBet(), btn.Name)); }
        private void Street27_30_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;street27_30.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(25, 26, 27, 28, 29, 30, CheckBet(), btn.Name)); }
        private void Street30_33_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;street30_33.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(28, 29, 30, 31, 32, 33, CheckBet(), btn.Name)); }
        private void Street33_36_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;street33_36.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(31, 32, 33, 34, 35, 36, CheckBet(), btn.Name)); }


        #endregion

        #region square bets (x9 bet)
        private void N1n2n4n5_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n1n2n4n5.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(1, 2, 4, 5, CheckBet(), btn.Name)); }
        private void N2n3n5n6_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n2n3n5n6.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(2, 3, 5, 6, CheckBet(), btn.Name)); }
        private void N4n5n7n8_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n4n5n7n8.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(4, 5, 7, 8, CheckBet(), btn.Name)); }
        private void N5n6n8n9_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n5n6n8n9.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(5, 6, 8, 9, CheckBet(), btn.Name)); }
        private void N7n8n10n11_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n7n8n10n11.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(7, 8, 10, 11, CheckBet(), btn.Name)); }
        private void N8n9n11n12_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n8n9n11n12.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(8, 9, 11, 12, CheckBet(), btn.Name)); }
        private void N10n11n13n14_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n10n11n13n14.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(10, 11, 13, 14, CheckBet(), btn.Name)); }
        private void N11n12n14n15_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n11n12n14n15.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(11, 12, 14, 15, CheckBet(), btn.Name)); }
        private void N13n14n16n17_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n13n14n16n17.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(13, 14, 16, 17, CheckBet(), btn.Name)); }
        private void N14n15n17n18_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n14n15n17n18.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(14, 15, 17, 18, CheckBet(), btn.Name)); }
        private void N16n17n19n20_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n16n17n19n20.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(16, 17, 19, 20, CheckBet(), btn.Name)); }
        private void N17n18n20n21_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n17n18n20n21.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(17, 18, 20, 21, CheckBet(), btn.Name)); }
        private void N19n20n22n23_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n19n20n22n23.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(19, 20, 22, 23, CheckBet(), btn.Name)); }
        private void N20n21n23n24_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n20n21n23n24.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(20, 21, 23, 24, CheckBet(), btn.Name)); }
        private void N22n23n25n26_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n22n23n25n26.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(22, 23, 25, 26, CheckBet(), btn.Name)); }
        private void N23n24n26n27_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n23n24n26n27.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(23, 24, 26, 27, CheckBet(), btn.Name)); }
        private void N25n26n28n29_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n25n26n28n29.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(25, 26, 28, 29, CheckBet(), btn.Name)); }
        private void N26n27n29n30_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n26n27n29n30.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(26, 27, 29, 30, CheckBet(), btn.Name)); }
        private void N28n29n31n32_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n28n29n31n32.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(28, 29, 31, 32, CheckBet(), btn.Name)); }
        private void N29n30n32n33_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n29n30n32n33.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(29, 30, 32, 33, CheckBet(), btn.Name)); }
        private void N31n32n34n35_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n31n32n34n35.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(31, 32, 34, 35, CheckBet(), btn.Name)); }
        private void N32n33n35n36_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n32n33n35n36.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet(32, 33, 35, 36, CheckBet(), btn.Name)); }
        #endregion

        #region special bets
        private void N1to12_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n1to12.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet((int)BetList.FIRST12, CheckBet(), btn.Name)); }
        private void N13to24_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n13to24.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet((int)BetList.SECOND12, CheckBet(), btn.Name)); }
        private void N25to36_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n25to36.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet((int)BetList.THIRD12, CheckBet(), btn.Name)); }
        private void N1to34_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n1to34.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet((int)BetList.HIGHWAY1, CheckBet(), btn.Name)); }
        private void N2to35_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n2to35.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet((int)BetList.HIGHWAY2, CheckBet(), btn.Name)); }
        private void N3to36_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n3to36.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet((int)BetList.HIGHWAY3, CheckBet(), btn.Name)); }
        private void N1to18_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n1to18.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet((int)BetList.ONETO18, CheckBet(), btn.Name)); }
        private void N19to36_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;n19to36.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet((int)BetList.NINETEENTO36, CheckBet(), btn.Name)); }
        private void Even_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;even.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet((int)BetList.EVEN, CheckBet(), btn.Name)); }
        private void Odd_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;odd.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet((int)BetList.ODD, CheckBet(), btn.Name)); }
        private void Red_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;red.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet((int)BetList.RED, CheckBet(), btn.Name)); }
        private void Black_Click(object sender, RoutedEventArgs e) { Button btn = sender as Button;black.Opacity = 1; chipCountUpdate(CheckBet(), -1); bets.Add(new Bet((int)BetList.BLACK, CheckBet(), btn.Name)); }

        #endregion

        #endregion

        private void XX_MouseRightButtonDown(object sender, RoutedEventArgs e) {
            List<Bet> remove = new List<Bet>();
            Button btn = sender as Button; 
            foreach (Bet bet in bets)
            {
                if (bet.button == btn.Name)
                {
                    chipCount += bet.amount;
                    remove.Add(bet);
                    btn.Opacity = 0;
                    chipTotalLabel.Content = "Chip Total: " + chipCount;
                }
            }
            foreach(Bet bet in remove)
            {
                bets.Remove(bet);
            }
        }


        private async void DropBallButton_Click(object sender, RoutedEventArgs e)
        {
            winner.Content = "Number: ";
            winnings.Content = "Earnings on this spin: ";



            Random r = new Random();
            int winningPocket = r.Next(0, 37);
            int winningNumber = buckets[winningPocket];

            double degreesTillWinningPocket = winningPocket * BUCKETSIZE;

            int numBoard = r.Next(1080, 1440);
            int numBall = Convert.ToInt32((1080-(numBoard%360)) + degreesTillWinningPocket - 86);

            TimeSpan t = TimeSpan.FromSeconds(5);
            SpinBoard(numBoard, t);
            SpinBall(numBall,t);

            await Task.Delay(t);
            winner.Content = "Number: " + winningNumber.ToString();
            if (winningNum.Content.ToString().Length <= 48)
            {
                winningNum.Content += " ";
                winningNum.Content += winningNumber.ToString() + '-';
            }
            Win(winningNumber);

            //reset the board
            foreach (Control ctrl in numGrid.Children)
            {
                ctrl.Opacity = 0;
            }
            bets.Clear();
        }

        private void Win(int num)
        {
            int prevchipCount = chipCount;
            foreach (Bet bet in bets)
            {
                Trace.WriteLine(bet.amount);
                if (bet.spaces.Count == 2)
                {
                    if (bet.spaces.Contains(num))
                    {
                        chipCount += bet.amount * 18;
                    }
                }
                if (bet.spaces.Count == 3)
                {
                    if (bet.spaces.Contains(num))
                    {
                        chipCount += bet.amount * 12;
                    }
                }
                if (bet.spaces.Count == 4)
                {
                    if (bet.spaces.Contains(num))
                    {
                        chipCount += bet.amount * 9;
                    }
                }
                if (bet.spaces.Count == 6)
                {
                    if (bet.spaces.Contains(num))
                    {
                        chipCount += bet.amount * 6;
                    }
                }
                if (bet.spaces.Count == 1)
                {
                    //if spaces < 38, its a single space bet
                    //otherwise its a special bet

                    if (bet.spaces.Contains(num))
                    {
                        chipCount += bet.amount * 36;
                    }
                    if (bet.spaces.Contains((int)BetList.FIRST12) && num % 2 == 0 && num < 13)
                    {
                        chipCount += bet.amount * 3;
                    }
                    if (bet.spaces.Contains((int)BetList.SECOND12) && num % 2 == 0 && num > 12 && num < 25)
                    {
                        chipCount += bet.amount * 3;
                    }
                    if (bet.spaces.Contains((int)BetList.THIRD12) && num % 2 == 0 && num > 24 && num < 37)
                    {
                        chipCount += bet.amount * 3;
                    }
                    if (bet.spaces.Contains((int)BetList.EVEN) && num % 2 == 0)
                    {
                        chipCount += bet.amount * 2;
                    }
                    if (bet.spaces.Contains((int)BetList.ODD) && num % 2 == 1)
                    {
                        chipCount += bet.amount * 2;
                    }
                    if (bet.spaces.Contains((int)BetList.RED) && redNums.Contains(num))
                    {
                        chipCount += bet.amount * 2;
                    }
                    if (bet.spaces.Contains((int)BetList.BLACK) && blackNums.Contains(num))
                    {
                        chipCount += bet.amount * 2;
                    }
                    if (bet.spaces.Contains((int)BetList.ONETO18) && num > 0 && num < 19)
                    {
                        chipCount += bet.amount * 2;
                    }
                    if (bet.spaces.Contains((int)BetList.NINETEENTO36) && num > 18 && num < 37)
                    {
                        chipCount += bet.amount * 2;
                    }
                    if (bet.spaces.Contains((int)BetList.HIGHWAY1) && num % 3 == 1)
                    {
                        chipCount += bet.amount * 3;
                    }
                    if (bet.spaces.Contains((int)BetList.HIGHWAY2) && num % 3 == 2)
                    {
                        chipCount += bet.amount * 3;
                    }
                    if (bet.spaces.Contains((int)BetList.HIGHWAY3) && num % 3 == 0)
                    {
                        chipCount += bet.amount * 3;
                    }
                }
            }
            winnings.Content = "Earnings on this spin: " + (chipCount - prevchipCount);
            chipTotalLabel.Content = "Chip Count: " + chipCount;
        }

        private void RouletteForm_Closed(object sender, EventArgs e)
        {
            casinoApp.Properties.Settings.Default.chips = chipCount;
            casinoApp.Properties.Settings.Default.username = userName;
            casinoApp.Properties.Settings.Default.Save();
        }

        private void RouletteForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            casinoApp.Properties.Settings.Default.chips = chipCount;
            casinoApp.Properties.Settings.Default.username = userName;
            casinoApp.Properties.Settings.Default.Save();
        }
    }
}
