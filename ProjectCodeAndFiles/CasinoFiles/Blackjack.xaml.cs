using Casino;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Casino
{
    /*
	 *      Need to store the amount in a file when I close the window. 
	 */

    /// <summary>
    /// Interaction logic for Blackjack.xaml
    /// </summary>
    public partial class Blackjack : Window
    {
        private string userName = "Guest";
        private int chipCount = 1000;

        public Blackjack()
        {
            InitializeComponent();
            hit_button.Visibility = Visibility.Hidden;
            standButton.Visibility = Visibility.Hidden;
            resetButton.Visibility = Visibility.Hidden;
            usernameLabel.Content += userName;
            chipTotalLabel.Content += chipCount.ToString();
        }
        public Blackjack(string uName, int bal)
        {
            InitializeComponent();
            hit_button.Visibility = Visibility.Hidden;
            standButton.Visibility = Visibility.Hidden;
            resetButton.Visibility = Visibility.Hidden;
            userName = uName;
            chipCount = bal;
            usernameLabel.Content += userName;
            chipTotalLabel.Content += chipCount.ToString();
        }

        public static List<Card> CreateCards()
        {
            string[] files = { "2s.png", "3s.png", "4s.png", "5s.png", "6s.png", "7s.png", "8s.png", "9s.png", "10s.png", "js.png", "qs.png", "ks.png", "as.png", "2h.png", "3h.png", "4h.png", "5h.png", "6h.png", "7h.png", "8h.png", "9h.png", "10h.png", "jh.png", "qh.png", "kh.png", "ah.png", "2d.png", "3d.png", "4d.png", "5d.png", "6d.png", "7d.png", "8d.png", "9d.png", "10d.png", "jd.png", "qd.png", "kd.png", "ad.png", "2c.png", "3c.png", "4c.png", "5c.png", "6c.png", "7c.png", "8c.png", "9c.png", "10c.png", "jc.png", "qc.png", "kc.png", "ac.png" };
            List<Card> CardSet = new List<Card>();
            //CREATING CARD SET ### THIS IS THE SORTED 52 CARD SET
            int count = 0;
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 2; j < 15; j++)
                {
                    Card card = new Card(j, files[count]);
                    CardSet.Add(card);
                    count++;
                }
            }
            return new List<Card>(CardSet);
        }
        public static List<Card> CreateShoe(List<Card> card)
        {
            List<Card> Shoe = new List<Card>();
            //Building the playable shoe of cards
            for (int i = 0; i < 52 * 6; i++) //52 cards * 6 decks in a normal shoe
            {
                Shoe.Add(card[i % 52]);
            }
            Shuffle(Shoe);

            return new List<Card>(Shoe);
        }
        public static List<Player> CreatePlayers()
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < 6; i++)
            {
                List<Card> hand = new List<Card>();
                Player player = new Player(hand, 0);
                players.Add(player);
            }
            return players;
        }
        public static Random rng = new Random();
        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public Card DrawCard(List<Card> set)
        {
            Card card = set[0];
            set.RemoveAt(0);
            return card;
        }
        private void Hit(int playerNum)
        {
            Card card = DrawCard(theShoe);
            players[playerNum].hand.Add(card);
            players[playerNum].UpdateTotal(card);
            if (card.cardValue == 11)
            {
                players[playerNum].aceCount++;
            }
            switch (playerNum)
            { 
            case 1:
                {
                    AI1_Cards.Visibility = Visibility.Visible;
                    switch (players[playerNum].hand.Count)
                    {
                        case 3:
                            {
                                ai1_card2.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai1_card2.Visibility = Visibility.Visible;
                                break;
                            }
                        case 4:
                            {
                                ai1_card3.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai1_card3.Visibility = Visibility.Visible;
                                break;
                            }
                        case 5:
                            {
                                ai1_card4.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai1_card4.Visibility = Visibility.Visible;
                                break;
                            }
                        case 6:
                            {
                                ai1_card5.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai1_card5.Visibility = Visibility.Visible;
                                break;
                            }
                        case 7:
                            {
                                ai1_card6.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai1_card6.Visibility = Visibility.Visible;
                                break;
                            }
                    }
                    break;
                }
            case 2:
                {
                    AI2_Cards.Visibility = Visibility.Visible;
                    switch (players[playerNum].hand.Count)
                    {
                        case 3:
                            {
                                ai2_card2.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai2_card2.Visibility = Visibility.Visible;
                                break;
                            }
                        case 4:
                            {
                                ai2_card3.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai2_card3.Visibility = Visibility.Visible;
                                break;
                            }
                        case 5:
                            {
                                ai2_card4.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai2_card4.Visibility = Visibility.Visible;
                                break;
                            }
                        case 6:
                            {
                                ai2_card5.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai2_card5.Visibility = Visibility.Visible;
                                break;
                            }
                        case 7:
                            {
                                ai2_card6.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai2_card6.Visibility = Visibility.Visible;
                                break;
                            }
                    }
                    break;
                }
            case 3:
                {
                    AI3_Cards.Visibility = Visibility.Visible;
                    switch (players[playerNum].hand.Count)
                    {
                        case 3:
                            {
                                ai3_card2.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai3_card2.Visibility = Visibility.Visible;
                                break;
                            }
                        case 4:
                            {
                                ai3_card3.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai3_card3.Visibility = Visibility.Visible;
                                break;
                            }
                        case 5:
                            {
                                ai3_card4.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai3_card4.Visibility = Visibility.Visible;
                                break;
                            }
                        case 6:
                            {
                                ai3_card5.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai3_card5.Visibility = Visibility.Visible;
                                break;
                            }
                        case 7:
                            {
                                ai3_card6.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai3_card6.Visibility = Visibility.Visible;
                                break;
                            }
                    }
                    break;
                }
            case 4:
                {
                    AI4_Cards.Visibility = Visibility.Visible;
                    switch (players[playerNum].hand.Count)
                    {
                        case 3:
                            {
                                ai4_card2.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai4_card2.Visibility = Visibility.Visible;
                                break;
                            }
                        case 4:
                            {
                                ai4_card3.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai4_card3.Visibility = Visibility.Visible;
                                break;
                            }
                        case 5:
                            {
                                ai4_card4.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai4_card4.Visibility = Visibility.Visible;
                                break;
                            }
                        case 6:
                            {
                                ai4_card5.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai4_card5.Visibility = Visibility.Visible;
                                break;
                            }
                        case 7:
                            {
                                ai4_card6.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                                ai4_card6.Visibility = Visibility.Visible;
                                break;
                            }
                    }
                    break;
                }
            }
            AceCheck(playerNum);
        }
        private void AceCheck(int playerNum)
        {
            while (players[playerNum].aceCount > 0 && players[playerNum].handTotal > 21)
            {
                players[playerNum].aceCount--;
                players[playerNum].handTotal -= 10;
            }
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
        private void chipCountUpdate(int betAmount, double multiplier)
        {
            chipCount += (int)Math.Floor(betAmount * multiplier);
            chipTotalLabel.Content = "Chip Count: " + chipCount;
        }
        public List<Card> CheckCards(List<Card> set)
        {
            foreach (Card card in set)
            {
                if (card.cardValue > 10)
                {
                    card.cardValue = 10;
                }
                if (card.fileName.Equals("as.png") || card.fileName.Equals("ah.png") || card.fileName.Equals("ac.png") || card.fileName.Equals("ad.png"))
                {
                    card.cardValue = 11;
                }
            }
            return set;
        }


        static List<Card> CardSet = CreateCards();       //creates a standard deck of cards
        List<Player> players = CreatePlayers();          //Always creates 4 AI players
        List<Card> theShoe = CreateShoe(CardSet);        //Always contains 6 decks for Blackjack


        public void InitialDeal(List<Card> set, List<Player> players, int playerNum)
        {
            for (int i = 0; i < playerNum; i++)
            {
                Card card = DrawCard(set);
                players[i].hand.Add(card); //deals 1 card to each player and the dealer
                players[i].UpdateTotal(card);
                switch (i)
                {
                    case 0:
                        {
                            player_card0.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            break;
                        }
                    case 1:
                        {
                            ai1_card0.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            break;
                        }
                    case 2:
                        {
                            ai2_card0.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            break;
                        }
                    case 3:
                        {
                            ai3_card0.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            break;
                        }
                    case 4:
                        {
                            ai4_card0.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            break;
                        }
                    case 5:
                        {
                            dealer_card0.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            break;
                        }
                }
                if (card.cardValue == 11)
                {
                    players[i].aceCount++;
                }
            }
            for (int i = 0; i < playerNum; i++)
            {
                Card card = DrawCard(set);
                players[i].hand.Add(card); //deals 1 card to each player and the dealer
                players[i].UpdateTotal(card);
                AceCheck(i);
                switch (i)
                {
                    case 0:
                        {
                            player_card1.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            break;
                        }
                    case 1:
                        {
                            ai1_card1.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            ai1_total.Content = players[1].handTotal;
                            break;
                        }
                    case 2:
                        {
                            ai2_card1.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            ai2_total.Content = players[2].handTotal;
                            break;
                        }
                    case 3:
                        {
                            ai3_card1.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            ai3_total.Content = players[3].handTotal;
                            break;
                        }
                    case 4:
                        {
                            ai4_card1.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            ai4_total.Content = players[4].handTotal;
                            break;
                        }
                }
                if (card.cardValue == 11)
                {
                    players[i].aceCount++;
                }
            }

            playerTotal.Content = players[0].handTotal;
            if (players[0].handTotal == 21)
            {

                blackjackLabel.Visibility = Visibility.Visible;
                DealerLoop();
                AIloop();
                Winners();
                resetButton.Visibility = Visibility.Visible;
                hit_button.Visibility = Visibility.Hidden;
                standButton.Visibility = Visibility.Hidden;
            }
        }
        private void AIloop()
        {
            //for each AI
            for (int i = 1; i <= 4; i++)
            {
                int stand = rng.Next(16, 20);
                while (players[i].handTotal < stand)
                {
                    Hit(i);
                    switch (i)
                    {
                        case 1:
                            {
                                ai1_total.Content = players[1].handTotal;
                                break;
                            }
                        case 2:
                            {
                                ai2_total.Content = players[2].handTotal;
                                break;
                            }
                        case 3:
                            {
                                ai3_total.Content = players[3].handTotal;
                                break;
                            }
                        case 4:
                            {
                                ai4_total.Content = players[4].handTotal;
                                break;
                            }
                    }
                }
            }
        }
        private void DealerLoop()
        {
            dealerTotal.Visibility = Visibility.Visible;
            dealer_card1.Source = new BitmapImage(new Uri("Resources/" + players[5].hand[1].fileName, UriKind.Relative));
            while (players[5].handTotal < 17)
            {
                Card card = DrawCard(theShoe);
                players[5].hand.Add(card);
                players[5].UpdateTotal(card);
                if (card.cardValue == 11)
                {
                    players[5].aceCount++;
                }
                switch (players[5].hand.Count)
                {
                    case 3:
                        {
                            dealer_card2.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            dealer_card2.Visibility = Visibility.Visible;
                            break;
                        }
                    case 4:
                        {
                            dealer_card3.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            dealer_card3.Visibility = Visibility.Visible;
                            break;
                        }
                    case 5:
                        {
                            dealer_card4.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            dealer_card4.Visibility = Visibility.Visible;
                            break;
                        }
                    case 6:
                        {
                            dealer_card5.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            dealer_card5.Visibility = Visibility.Visible;
                            break;
                        }
                    case 7:
                        {
                            dealer_card6.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                            dealer_card6.Visibility = Visibility.Visible;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                AceCheck(5);
            }
            dealerTotal.Content = players[5].handTotal;

        }
        private void Winners()
        {
            for (int i = 0; i < 5; i++) // all players but dealer
            {
                if ((players[i].handTotal <= 21 && players[i].handTotal > players[5].handTotal) || (players[i].handTotal <= 21 && players[5].handTotal > 21))
                {
                    winnersLabel.Visibility = Visibility.Visible;
                    //perform win conditions for the players here (chipCount increase, win label)
                    switch (i)
                    {
                        case 0:
                            {
                                if (players[0].handTotal == 21 && players[0].hand.Count == 2)
                                {
                                    hit_button.Visibility = Visibility.Hidden;
                                    standButton.Visibility = Visibility.Hidden;
                                    chipCountUpdate(CheckBet(), 2.5);
                                }
                                else
                                    chipCountUpdate(CheckBet(), 2);

                                winLabel.Visibility = Visibility.Visible;
                                winnersLabel.Content += "You | ";
                                break;
                            }
                        case 1:
                            {
                                winnersLabel.Content += "Player 2 | ";
                                break;
                            }
                        case 2:
                            {
                                winnersLabel.Content += "Player 3 | ";
                                break;
                            }
                        case 3:
                            {
                                winnersLabel.Content += "Player 4 | ";
                                break;
                            }
                        case 4:
                            {
                                winnersLabel.Content += "Player 5 | ";
                                break;
                            }
                    }
                }
                else if ((players[5].handTotal == 21 && players[5].hand.Count == 2) && (players[0].handTotal == 21 && players[0].hand.Count == 2))
                {
                    chipCountUpdate(CheckBet(), 1);
                    loseLabel.Content = "Both player and dealer have blackjack.";
                    loseLabel.Visibility = Visibility.Visible;
                }
                else if (i == 0)
                {
                    loseLabel.Visibility = Visibility.Visible;
                }
            }
        }



        #region Player region of play. All button clicks will relate to player only (players[0]).
        //When Stand button is pressed dealer will play.

        private void DealButton_Click(object sender, RoutedEventArgs e)
        {
            int betAmount = CheckBet();
            theShoe = CheckCards(theShoe);
            chipCountUpdate(betAmount, -1);
            InitialDeal(theShoe, players, 6);
            hit_button.Visibility = Visibility.Visible;
            standButton.Visibility = Visibility.Visible;
            dealButton.Visibility = Visibility.Hidden;

        }

        private void Hit_button_Click(object sender, RoutedEventArgs e)
        {
            Card card = DrawCard(theShoe);
            players[0].hand.Add(card);
            players[0].UpdateTotal(card);
            if (card.cardValue == 11)
            {
                players[0].aceCount++;
            }
            switch (players[0].hand.Count)
            {
                case 3:
                    {
                        player_card2.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                        player_card2.Visibility = Visibility.Visible;
                        break;
                    }
                case 4:
                    {
                        player_card3.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                        player_card3.Visibility = Visibility.Visible;
                        break;
                    }
                case 5:
                    {
                        player_card4.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                        player_card4.Visibility = Visibility.Visible;
                        break;
                    }
                case 6:
                    {
                        player_card5.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                        player_card5.Visibility = Visibility.Visible;
                        break;
                    }
                case 7:
                    {
                        player_card6.Source = new BitmapImage(new Uri("Resources/" + card.fileName, UriKind.Relative));
                        player_card6.Visibility = Visibility.Visible;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            AceCheck(0);
            playerTotal.Content = players[0].handTotal;
            if (players[0].handTotal > 21)
            {
                DealerLoop();
                AIloop();
                bustLabel.Visibility = Visibility.Visible;
                hit_button.Visibility = Visibility.Hidden;
                standButton.Visibility = Visibility.Hidden;
                resetButton.Visibility = Visibility.Visible;
                Winners();
            }
        }

        private void StandButton_Click(object sender, RoutedEventArgs e)
        {
            hit_button.Visibility = Visibility.Hidden;
            standButton.Visibility = Visibility.Hidden;
            DealerLoop();
            AIloop();
            Winners();
            resetButton.Visibility = Visibility.Visible;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            theShoe.Clear();
            theShoe = CreateShoe(CardSet); //deal with 
            Blackjack bj = new Blackjack(userName, chipCount);
            this.Close();
            bj.ShowDialog();
        }


        #endregion


        private void BlackjackForm_Closed(object sender, EventArgs e)
        {
            casinoApp.Properties.Settings.Default.chips = chipCount;
            casinoApp.Properties.Settings.Default.username = userName;
            casinoApp.Properties.Settings.Default.Save();
        }

        private void BlackjackForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            casinoApp.Properties.Settings.Default.chips = chipCount;
            casinoApp.Properties.Settings.Default.username = userName;
            casinoApp.Properties.Settings.Default.Save();
        }
    }
}

