using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Casino
{
    /// <summary>
    /// Interaction logic for Poker.xaml
    /// </summary>
    public partial class Poker : Window
    {

        private string userName = "Guest";
        private int chipCount = 1000;
        private int potTotal = 0;
        private int blind = 0;
        static List<Card> CardSet = CreateCards();       //creates a standard deck of cards
        List<Player> players = CreatePlayers();          //Always creates 3 AI players
        List<Card> theShoe = CreateShoe(CardSet);        //Always contains 1 deck for poker


        public Poker()
        {
            InitializeComponent();
            usernameLabel.Content += userName;
        }
        public Poker(string uName, int chips)
        {
            InitializeComponent();
            usernameLabel.Content += uName;
            userName = uName;
            chipCount = chips;
            players[0].chipCount = chips;
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
                    Card card = new Card(j, files[count], (Card.SUIT)i); //value, image, suit
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
            for (int i = 0; i < 52; i++) //52 cards decks in a normal shoe
            {
                Shoe.Add(card[i % 52]);
            }

            Shuffle(Shoe);
            Shuffle(Shoe);
            Shuffle(Shoe);

            return Shoe;
        }
        public static List<Player> CreatePlayers()
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < 4; i++)
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

        private void DealButton_Click(object sender, RoutedEventArgs e)
        {
            theShoe = CreateShoe(CardSet);
            foreach (Player p in players) //reset all players
            {
                p.hand.Clear();
                p.spadeCount = 0;
                p.heartCount = 0;
                p.clubCount = 0;
                p.diamondCount = 0;
            }

            foreach (Player p in players)
            {
                p.chipCount -= 50;
                potTotal += 50;
            } //50 chip ante per round

            InitialDeal(theShoe, players, 4);

            dealButton.Visibility = Visibility.Hidden;
            switchButton.Visibility = Visibility.Visible;
            card1Box.Visibility = Visibility.Visible;
            card2Box.Visibility = Visibility.Visible;
            card3Box.Visibility = Visibility.Visible;
            card4Box.Visibility = Visibility.Visible;
            card5Box.Visibility = Visibility.Visible;
            betAmountLabel.Visibility = Visibility.Visible;
            betBox.Visibility = Visibility.Visible;
            foldLabel.Visibility = Visibility.Visible;
            potLabel.Content = "Total in pot: " + potTotal;

        }

        public void InitialDeal(List<Card> set, List<Player> players, int playerNum)
        {
            for (int i = 0; i < 5; i++) //give 5 cards 
            {
                for (int j = 0; j < playerNum; j++) //to 4 players
                {
                    Card card = DrawCard(set);
                    players[j].hand.Add(card);
                }
            }
            potLabel.Content = "Total in pot: " + potTotal;
            //sort the hands by card value, this will allow for easier hand evaluation
            foreach (Player p in players)
                p.hand.Sort((x, y) => y.cardValue.CompareTo(x.cardValue));

            for (int i = 0; i < playerNum; i++)
            {
                players[i].handName = HandEval(i);
            }

            //Show the players cards
            player_card0.Source = new BitmapImage(new Uri("Resources/" + players[0].hand[0].fileName, UriKind.Relative));
            player_card1.Source = new BitmapImage(new Uri("Resources/" + players[0].hand[1].fileName, UriKind.Relative));
            player_card2.Source = new BitmapImage(new Uri("Resources/" + players[0].hand[2].fileName, UriKind.Relative));
            player_card3.Source = new BitmapImage(new Uri("Resources/" + players[0].hand[3].fileName, UriKind.Relative));
            player_card4.Source = new BitmapImage(new Uri("Resources/" + players[0].hand[4].fileName, UriKind.Relative));


            int bet = 0;
            int maxBet = 0;
            int call;
            for (int i = 1; i < playerNum; i++)
            {
                bet = AIBets(i, 0);
                if (bet > 500) //table max of 750 per bet, this keeps the ai's in a fair state when one has a lot of money
                {
                    bet = 500;
                }

                if (bet > 0)
                {
                    players[i].preBet = bet;
                    potTotal += bet;
                    if (bet > maxBet)
                        maxBet = bet;
                    players[i].chipCount -= bet;
                    betBox.Text = bet.ToString();
                }
                else
                {
                    players[i].inHand = false;
                    players[i].handName = Player.HandName.Fold;
                }
            }

            if (maxBet > 0)
                betBox.Text = maxBet.ToString();
            else
                betBox.Text = "10";

            for (int i = 1; i < 4; i++)
            {
                if (players[i].inHand)
                {
                    call = AICall(i, maxBet, 0);
                    if (call > 0) //not max but called
                    {
                        players[i].chipCount -= call;
                        potTotal += call;

                        switch (i)
                        {
                            case 1:
                                {
                                    ai0pre.Content = players[i].preBet + call;
                                    break;
                                }
                            case 2:
                                {
                                    ai1Pre.Content = players[i].preBet + call;
                                    break;
                                }
                            case 3:
                                {
                                    ai2Pre.Content = players[i].preBet + call;
                                    break;
                                }
                        }
                    }
                    else if (call == -1) //had max bet already
                    {
                        switch (i)
                        {
                            case 1:
                                {
                                    ai0pre.Content = players[i].preBet;
                                    break;
                                }
                            case 2:
                                {
                                    ai1Pre.Content = players[i].preBet;
                                    break;
                                }
                            case 3:
                                {
                                    ai2Pre.Content = players[i].preBet;
                                    break;
                                }
                        }
                    }
                    else if (call == 0) //folded
                    {
                        switch (i)
                        {
                            case 1:
                                {
                                    ai0pre.Content = "Fold";
                                    players[i].handName = Player.HandName.Fold;
                                    break;
                                }
                            case 2:
                                {
                                    ai1Pre.Content = "Fold";
                                    players[i].handName = Player.HandName.Fold;
                                    break;
                                }
                            case 3:
                                {
                                    ai2Pre.Content = "Fold";
                                    players[i].handName = Player.HandName.Fold;
                                    break;
                                }
                        }
                    }

                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            {
                                ai0pre.Content = "Fold";
                                players[i].handName = Player.HandName.Fold;
                                break;
                            }
                        case 2:
                            {
                                ai1Pre.Content = "Fold";
                                players[i].handName = Player.HandName.Fold;
                                break;
                            }
                        case 3:
                            {
                                ai2Pre.Content = "Fold";
                                players[i].handName = Player.HandName.Fold;
                                break;
                            }
                    }
                }
            }

            ShowChips();
            playerHand.Content = players[0].handName;
        }

        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            potTotal += CheckBet();
            players[0].chipCount -= CheckBet();
            if (CheckBet() == 0)
            {
                players[0].handName = Player.HandName.Fold;
                players[0].inHand = false;
                AILoop(); //this includes card switching and pre flip bets
                foreach (Player p in players)
                    p.hand.Sort((x, y) => y.cardValue.CompareTo(x.cardValue));
                betBox.Text = "0";
                betBox.Visibility = Visibility.Hidden;
                betAmountLabel.Visibility = Visibility.Hidden;
                foldLabel.Visibility = Visibility.Hidden;
                playerPreSwitch.Content = playerPreSwitch.Content.ToString() + "Fold";
            }
            else
            {
                //run ai call again, set all of the appropriate values
                for (int i = 1; i < 4; i++)
                {
                    int call;
                    if (players[i].inHand)
                    {
                        call = AICall(i, CheckBet(), 0);
                        if (call > 0) //not max but called
                        {
                            players[i].chipCount -= call;
                            potTotal += call;

                            switch (i)
                            {
                                case 1:
                                    {
                                        ai0pre.Content = players[i].preBet + call;
                                        break;
                                    }
                                case 2:
                                    {
                                        ai1Pre.Content = players[i].preBet + call;
                                        break;
                                    }
                                case 3:
                                    {
                                        ai2Pre.Content = players[i].preBet + call;
                                        break;
                                    }
                            }
                        }
                        else if (call == -1) //had max bet already
                        {
                            switch (i)
                            {
                                case 1:
                                    {
                                        ai0pre.Content = players[i].preBet;
                                        break;
                                    }
                                case 2:
                                    {
                                        ai1Pre.Content = players[i].preBet;
                                        break;
                                    }
                                case 3:
                                    {
                                        ai2Pre.Content = players[i].preBet;
                                        break;
                                    }
                            }
                        }
                        else if (call == 0) //folded
                        {
                            switch (i)
                            {
                                case 1:
                                    {
                                        ai0pre.Content = "Fold";
                                        players[i].handName = Player.HandName.Fold;
                                        break;
                                    }
                                case 2:
                                    {
                                        ai1Pre.Content = "Fold";
                                        players[i].handName = Player.HandName.Fold;
                                        break;
                                    }
                                case 3:
                                    {
                                        ai2Pre.Content = "Fold";
                                        players[i].handName = Player.HandName.Fold;
                                        break;
                                    }
                            }
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 1:
                                {
                                    ai0pre.Content = "Fold";
                                    players[i].handName = Player.HandName.Fold;
                                    break;
                                }
                            case 2:
                                {
                                    ai1Pre.Content = "Fold";
                                    players[i].handName = Player.HandName.Fold;
                                    break;
                                }
                            case 3:
                                {
                                    ai2Pre.Content = "Fold";
                                    players[i].handName = Player.HandName.Fold;
                                    break;
                                }
                        }
                    }
                }
                playerPreSwitch.Content = playerPreSwitch.Content.ToString() + CheckBet();

                AILoop(); //this includes card switching and pre flip bets

                #region Switch Cards Here
                if (card5Box.IsChecked == true)
                {
                    players[0].hand.RemoveAt(4);
                    Card card = DrawCard(theShoe);
                    players[0].hand.Add(card);
                }
                if (card4Box.IsChecked == true)
                {
                    players[0].hand.RemoveAt(3);
                    Card card = DrawCard(theShoe);
                    players[0].hand.Add(card);
                }
                if (card3Box.IsChecked == true)
                {
                    players[0].hand.RemoveAt(2);
                    Card card = DrawCard(theShoe);
                    players[0].hand.Add(card);
                }
                if (card2Box.IsChecked == true)
                {
                    players[0].hand.RemoveAt(1);
                    Card card = DrawCard(theShoe);
                    players[0].hand.Add(card);
                }
                if (card1Box.IsChecked == true)
                {
                    players[0].hand.RemoveAt(0);
                    Card card = DrawCard(theShoe);
                    players[0].hand.Add(card);
                }
                #endregion

                card1Box.IsChecked = false;
                card2Box.IsChecked = false;
                card3Box.IsChecked = false;
                card4Box.IsChecked = false;
                card5Box.IsChecked = false;
                card1Box.Visibility = Visibility.Hidden;
                card2Box.Visibility = Visibility.Hidden;
                card3Box.Visibility = Visibility.Hidden;
                card4Box.Visibility = Visibility.Hidden;
                card5Box.Visibility = Visibility.Hidden;

                foreach (Player p in players)
                    p.hand.Sort((x, y) => y.cardValue.CompareTo(x.cardValue));

                players[0].handName = HandEval(0);
                playerHand.Content = players[0].handName;

                player_card0.Source = new BitmapImage(new Uri("Resources/" + players[0].hand[0].fileName, UriKind.Relative));
                player_card1.Source = new BitmapImage(new Uri("Resources/" + players[0].hand[1].fileName, UriKind.Relative));
                player_card2.Source = new BitmapImage(new Uri("Resources/" + players[0].hand[2].fileName, UriKind.Relative));
                player_card3.Source = new BitmapImage(new Uri("Resources/" + players[0].hand[3].fileName, UriKind.Relative));
                player_card4.Source = new BitmapImage(new Uri("Resources/" + players[0].hand[4].fileName, UriKind.Relative));
            }
            flipButton.Visibility = Visibility.Visible;
            switchButton.Visibility = Visibility.Hidden;

            potLabel.Content = "Total in pot: " + potTotal;
            ShowChips();
        }

        private void AILoop()
        {
            int bet = 0;
            int maxBet = 0;
            int call;

            for (int i = 1; i < 4; i++)
            {
                int removed = 0;

                if (rng.Next(10) >= 2)
                {
                    if (players[i].handName < Player.HandName.Straight)
                    {
                        #region flush draw
                        if (players[i].spadeCount == 4)
                        {

                            foreach (Card c in players[i].hand.ToList())
                            {
                                if (c.suit != Card.SUIT.SPADES)
                                {
                                    players[i].hand.Remove(c);
                                    removed++;
                                }
                            }
                        }
                        else if (players[i].diamondCount == 4)
                        {

                            foreach (Card c in players[i].hand.ToList())
                            {
                                if (c.suit != Card.SUIT.DIAMONDS)
                                {
                                    players[i].hand.Remove(c);
                                    removed++;
                                }
                            }
                        }
                        else if (players[i].heartCount == 4)
                        {

                            foreach (Card c in players[i].hand.ToList())
                            {
                                if (c.suit != Card.SUIT.HEARTS)
                                {
                                    players[i].hand.Remove(c);
                                    removed++;
                                }
                            }
                        }
                        else if (players[i].clubCount == 4)
                        {

                            foreach (Card c in players[i].hand.ToList())
                            {
                                if (c.suit != Card.SUIT.CLUBS)
                                {
                                    players[i].hand.Remove(c);
                                    removed++;
                                }
                            }
                        }
                        #endregion

                        else //three of kind or lower
                        {
                            if (players[i].handName == Player.HandName.ThreeKind)
                            {
                                //redraw the two off 
                                if ((players[i].hand[0].cardValue == players[i].hand[1].cardValue && players[i].hand[0].cardValue == players[i].hand[2].cardValue))
                                {
                                    players[i].hand.RemoveAt(4);
                                    removed++;
                                    players[i].hand.RemoveAt(3);
                                    removed++;
                                }
                                else if (players[i].hand[1].cardValue == players[i].hand[2].cardValue && players[i].hand[1].cardValue == players[i].hand[3].cardValue)
                                {
                                    players[i].hand.RemoveAt(4);
                                    removed++;
                                    players[i].hand.RemoveAt(0);
                                    removed++;
                                }
                                else if (players[i].hand[2].cardValue == players[i].hand[3].cardValue && players[i].hand[2].cardValue == players[i].hand[4].cardValue)
                                {
                                    players[i].hand.RemoveAt(1);
                                    removed++;
                                    players[i].hand.RemoveAt(0);
                                    removed++;
                                }
                            }
                            else if (players[i].handName == Player.HandName.TwoPairs)
                            {
                                //redraw the one off 
                                if (players[i].hand[0].cardValue == players[i].hand[1].cardValue && players[i].hand[2].cardValue == players[i].hand[3].cardValue)
                                {
                                    players[i].hand.RemoveAt(4);
                                    removed++;
                                }
                                else if (players[i].hand[0].cardValue == players[i].hand[1].cardValue && players[i].hand[3].cardValue == players[i].hand[4].cardValue)
                                {
                                    players[i].hand.RemoveAt(2);
                                    removed++;
                                }
                                else if (players[i].hand[1].cardValue == players[i].hand[2].cardValue && players[i].hand[3].cardValue == players[i].hand[4].cardValue)
                                {
                                    players[i].hand.RemoveAt(0);
                                    removed++;
                                }
                            }
                            else if (players[i].handName == Player.HandName.OnePair)
                            {
                                //redraw the three off 
                                if (players[i].hand[0].cardValue == players[i].hand[1].cardValue)
                                {
                                    players[i].hand.RemoveAt(4);
                                    removed++;
                                    players[i].hand.RemoveAt(3);
                                    removed++;
                                    players[i].hand.RemoveAt(2);
                                    removed++;
                                }
                                else if (players[i].hand[1].cardValue == players[i].hand[2].cardValue)
                                {
                                    players[i].hand.RemoveAt(4);
                                    removed++;
                                    players[i].hand.RemoveAt(3);
                                    removed++;
                                    players[i].hand.RemoveAt(0);
                                    removed++;
                                }
                                else if (players[i].hand[2].cardValue == players[i].hand[3].cardValue)
                                {
                                    players[i].hand.RemoveAt(4);
                                    removed++;
                                    players[i].hand.RemoveAt(1);
                                    removed++;
                                    players[i].hand.RemoveAt(0);
                                    removed++;
                                }
                                else if (players[i].hand[3].cardValue == players[i].hand[4].cardValue)
                                {
                                    players[i].hand.RemoveAt(2);
                                    removed++;
                                    players[i].hand.RemoveAt(1);
                                    removed++;
                                    players[i].hand.RemoveAt(0);
                                    removed++;
                                }
                            }
                            else //player only has high card
                            {
                                //redraw the bottom 3? 
                                players[i].hand.RemoveAt(4);
                                removed++;
                                players[i].hand.RemoveAt(3);
                                removed++;
                                players[i].hand.RemoveAt(2);
                                removed++;
                            }
                        }
                    }
                } //80% of time, do normal card switch operations
                else
                {

                    if (players[i].handName == Player.HandName.ThreeKind)
                    {
                        //redraw the two off 
                        if ((players[i].hand[0].cardValue == players[i].hand[1].cardValue && players[i].hand[0].cardValue == players[i].hand[2].cardValue))
                        {
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(4);
                                removed++;
                            }
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(3);
                                removed++;
                            }
                        }
                        else if (players[i].hand[1].cardValue == players[i].hand[2].cardValue && players[i].hand[1].cardValue == players[i].hand[3].cardValue)
                        {
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(4);
                                removed++;
                            }
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(0);
                                removed++;
                            }
                        }
                        else if (players[i].hand[2].cardValue == players[i].hand[3].cardValue && players[i].hand[2].cardValue == players[i].hand[4].cardValue)
                        {
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(1);
                                removed++;
                            }
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(0);
                                removed++;
                            }
                        }
                    }
                    else if (players[i].handName == Player.HandName.TwoPairs)
                    {
                        //redraw the one off 
                        if (players[i].hand[0].cardValue == players[i].hand[1].cardValue && players[i].hand[2].cardValue == players[i].hand[3].cardValue && rng.Next(10) >= 5)
                        {
                            players[i].hand.RemoveAt(4);
                            removed++;
                        }
                        else if (players[i].hand[0].cardValue == players[i].hand[1].cardValue && players[i].hand[3].cardValue == players[i].hand[4].cardValue && rng.Next(10) >= 5)
                        {
                            players[i].hand.RemoveAt(2);
                            removed++;
                        }
                        else if (players[i].hand[1].cardValue == players[i].hand[2].cardValue && players[i].hand[3].cardValue == players[i].hand[4].cardValue && rng.Next(10) >= 5)
                        {
                            players[i].hand.RemoveAt(0);
                            removed++;
                        }
                    }
                    else if (players[i].handName == Player.HandName.OnePair)
                    {
                        //redraw the three off 
                        if (players[i].hand[0].cardValue == players[i].hand[1].cardValue)
                        {
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(4);
                                removed++;
                            }
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(3);
                                removed++;
                            }
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(2);
                                removed++;
                            }
                        }
                        else if (players[i].hand[1].cardValue == players[i].hand[2].cardValue)
                        {
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(4);
                                removed++;
                            }
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(3);
                                removed++;
                            }
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(0);
                                removed++;
                            }
                        }
                        else if (players[i].hand[2].cardValue == players[i].hand[3].cardValue)
                        {
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(4);
                                removed++;
                            }
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(1);
                                removed++;
                            }
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(0);
                                removed++;
                            }
                        }
                        else if (players[i].hand[3].cardValue == players[i].hand[4].cardValue)
                        {
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(2);
                                removed++;
                            }
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(1);
                                removed++;
                            }
                            if (rng.Next(10) >= 5)
                            {
                                players[i].hand.RemoveAt(0);
                                removed++;
                            }
                        }
                    }
                    else //player only has high card
                    {
                        if (rng.Next(10) >= 5)
                        {
                            players[i].hand.RemoveAt(4);
                            removed++;
                        }
                        if (rng.Next(10) >= 5)
                        {
                            players[i].hand.RemoveAt(3);
                            removed++;
                        }
                        if (rng.Next(10) >= 5)
                        {
                            players[i].hand.RemoveAt(2);
                            removed++;
                        }
                        if (rng.Next(10) >= 5)
                        {
                            players[i].hand.RemoveAt(1);
                            removed++;
                        }
                    }
                } //20% of time, bluff number of cards

                for (int j = 0; j < removed; j++)
                {
                    Card card = DrawCard(theShoe);
                    players[i].hand.Add(card);

                } //redraws removed cards

                players[i].handName = HandEval(i);

                bet = AIBets(i, 0);
                if (bet > 750) //table max of 750, this keeps the ai's in a fair state when one has a lot of money
                {
                    bet = 750;
                }
                if (bet > 0)
                {
                    potTotal += bet;
                    players[i].postBet = bet;
                    if (bet > maxBet)
                        maxBet = bet;
                    players[i].chipCount -= bet;
                }
                else
                {
                    players[i].inHand = false;
                    players[i].handName = Player.HandName.Fold;
                }
            }

            if (maxBet > 0)
                betBox.Text = maxBet.ToString();
            else
                betBox.Text = "10";

            foreach (Player p in players)
                p.hand.Sort((x, y) => y.cardValue.CompareTo(x.cardValue));

            for (int i = 1; i < 4; i++)
            {
                if (players[i].inHand)
                {
                    call = AICall(i, maxBet, 0);
                    if (call > 0)
                    {
                        players[i].chipCount -= call;
                        potTotal += call;

                        switch (i)
                        {
                            case 1:
                                {
                                    ai0post.Content = players[i].postBet + call;
                                    break;
                                }
                            case 2:
                                {
                                    ai1Post.Content = players[i].postBet + call;
                                    break;
                                }
                            case 3:
                                {
                                    ai2Post.Content = players[i].postBet + call;
                                    break;
                                }
                        }
                    }
                    else if (call == -1)
                    {
                        switch (i)
                        {
                            case 1:
                                {
                                    ai0post.Content = players[i].postBet;
                                    break;
                                }
                            case 2:
                                {
                                    ai1Post.Content = players[i].postBet;
                                    break;
                                }
                            case 3:
                                {
                                    ai2Post.Content = players[i].postBet;
                                    break;
                                }
                        }
                    }
                    else if (call == 0)
                    {
                        switch (i)
                        {
                            case 1:
                                {
                                    ai0post.Content = "Fold";
                                    break;
                                }
                            case 2:
                                {
                                    ai1Post.Content = "Fold";
                                    break;
                                }
                            case 3:
                                {
                                    ai2Post.Content = "Fold";
                                    break;
                                }
                        }
                    }

                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            {
                                ai0post.Content = "Fold";
                                break;
                            }
                        case 2:
                            {
                                ai1Post.Content = "Fold";
                                break;
                            }
                        case 3:
                            {
                                ai2Post.Content = "Fold";
                                break;
                            }
                    }
                }
                call = 0;
            }

            ShowChips();
            potLabel.Content = "Total in pot: " + potTotal;
        }

        private int AIBets(int i, int preOrPost)
        {
            /**
            Normal Bets Breakdown
            RF,SF,4K,FH,F,S == 50/50 Mid/High
            3K/2P == 30/60/10 Low/Mid/High
            P == 40/45/5 Low/Mid/High
            N == 70/30 Low/Zero(Fold)
             
            Bluffed Bets Breakdown
            RF,SF,4K,FH,F,S == 30/70 Mid/High
            3K/2P == 10/40/50 Low/Mid/High
            P == 10/60/30 Low/Mid/High
            N == 100 Low
            

            Low/Mid/High Breakdown (Subject to Change)
            Low -> 5% of chipCount + 0-5% of chipCount
            Mid -> 10% of chipCount + 0-7% of chipCount
            High -> 15% of chipCount + 5-10% of chipCount
            */

            int bet = 0;
            int lowRange = rng.Next(0, 6);
            int midRange = rng.Next(0, 8);
            int highRange = rng.Next(5, 11);
            double low = .05;
            double mid = .1;
            double high = .15;


            if (rng.Next(10) >= 2)
            {
                if (players[i].handName >= Player.HandName.Straight) //RF,SF,4K,FH,F,S
                {
                    if (rng.Next(10) < 5)
                    {
                        //mid bet
                        bet = Convert.ToInt32((players[i].chipCount * mid) + (players[i].chipCount * midRange / 100));
                    }
                    else
                    {
                        //high bet
                        bet = Convert.ToInt32((players[i].chipCount * high) + (players[i].chipCount * highRange / 100));
                    }
                }
                else if (players[i].handName >= Player.HandName.TwoPairs) //3K,2P
                {
                    int rand = rng.Next(10);
                    if (rand == 9)
                    {
                        //high bet
                        bet = Convert.ToInt32((players[i].chipCount * high) + (players[i].chipCount * highRange / 100));

                    }
                    else if (rand < 3)
                    {
                        //low
                        bet = Convert.ToInt32((players[i].chipCount * low) + (players[i].chipCount * lowRange / 100));

                    }
                    else
                    {
                        //mid
                        bet = Convert.ToInt32((players[i].chipCount * mid) + (players[i].chipCount * midRange / 100));

                    }
                }
                else if (players[i].handName == Player.HandName.OnePair) //1P
                {
                    int rand = rng.Next(100);
                    if (rand >= 95)
                    {
                        //high bet
                        bet = Convert.ToInt32((players[i].chipCount * high) + (players[i].chipCount * highRange / 100));

                    }
                    else if (rand < 40)
                    {
                        //low
                        bet = Convert.ToInt32((players[i].chipCount * low) + (players[i].chipCount * lowRange / 100));

                    }
                    else
                    {
                        //mid
                        bet = Convert.ToInt32((players[i].chipCount * mid) + (players[i].chipCount * midRange / 100));

                    }
                }
                else // Nothing
                {
                    if (rng.Next(10) >= 7)
                    {
                        //low
                        bet = Convert.ToInt32((players[i].chipCount * low) + (players[i].chipCount * lowRange / 100));

                    }
                    else
                    {
                        //bet nothing, fold
                    }
                }
            }//80% of time, bet normally
            else
            {
                if (players[i].handName >= Player.HandName.Straight) //RF,SF,4K,FH,F,S
                {
                    if (rng.Next(10) < 3)
                    {
                        //mid bet
                        bet = Convert.ToInt32((players[i].chipCount * mid) + (players[i].chipCount * midRange / 100));

                    }
                    else
                    {
                        //high bet
                        bet = Convert.ToInt32((players[i].chipCount * high) + (players[i].chipCount * highRange / 100));

                    }
                }
                else if (players[i].handName >= Player.HandName.TwoPairs) //3K,2P
                {
                    int rand = rng.Next(10);
                    if (rand >= 5)
                    {
                        //high bet
                        bet = Convert.ToInt32((players[i].chipCount * high) + (players[i].chipCount * highRange / 100));

                    }
                    else if (rand < 1)
                    {
                        //low
                        bet = Convert.ToInt32((players[i].chipCount * low) + (players[i].chipCount * lowRange / 100));

                    }
                    else
                    {
                        //mid
                        bet = Convert.ToInt32((players[i].chipCount * mid) + (players[i].chipCount * midRange / 100));

                    }
                }
                else if (players[i].handName == Player.HandName.OnePair) //1P
                {
                    int rand = rng.Next(10);
                    if (rand >= 7)
                    {
                        //high bet
                        bet = Convert.ToInt32((players[i].chipCount * high) + (players[i].chipCount * highRange / 100));

                    }
                    else if (rand < 1)
                    {
                        //low
                        bet = Convert.ToInt32((players[i].chipCount * low) + (players[i].chipCount * lowRange / 100));

                    }
                    else
                    {
                        //mid
                        bet = Convert.ToInt32((players[i].chipCount * mid) + (players[i].chipCount * midRange / 100));

                    }
                }
                else // Nothing
                {
                    //low
                    bet = Convert.ToInt32((players[i].chipCount * low) + (players[i].chipCount * lowRange / 100));

                }
            }//20% of time, bluff bet


            if (bet > 0 && bet < 25)
                bet = 25;
            //round the bet to the nearest 25 (like we're using chips)
            if (bet % 25 >= 13)
            {
                bet += (25 - (bet % 25));
            }
            else
            {
                bet -= (bet % 25);
            }

            if (preOrPost == 0)
                players[i].preBet = bet;
            else
                players[i].postBet = bet;

            return bet;
        }

        private int AICall(int i, int maxBet, int preOrPost)
        {

            if (preOrPost == 0)
            {
                if (players[i].handName >= Player.HandName.ThreeKind || rng.Next(10) > 5 || maxBet < (players[i].chipCount / 5) || players[0].preBet == maxBet)
                {
                    if (players[i].preBet < maxBet)
                        return maxBet - players[i].preBet;
                    else
                        return -1;
                }
                else
                {
                    players[i].inHand = false;
                    players[i].handName = Player.HandName.Fold;
                    return 0;
                }
            }
            else
            {
                if (players[i].handName >= Player.HandName.ThreeKind || rng.Next(10) > 5 || maxBet < (players[i].chipCount / 5) || players[0].postBet == maxBet)
                {

                    if (players[i].postBet < maxBet)
                        return maxBet - players[i].postBet;
                    else
                        return -1;
                }
                else
                {
                    players[i].inHand = false;
                    players[i].handName = Player.HandName.Fold;
                    return 0;
                }

            }
        }



        private void FlipButton_Click(object sender, RoutedEventArgs e)
        {
            potTotal += CheckBet();
            players[0].chipCount -= CheckBet();
            if (CheckBet() == 0)
            {
                players[0].handName = Player.HandName.Fold;
                players[0].inHand = false;
            }
            if (players[0].inHand)
            {
                PlayerPostSwitch.Content = PlayerPostSwitch.Content.ToString() + CheckBet();
            }
            else
            {
                PlayerPostSwitch.Content = PlayerPostSwitch.Content.ToString() + "Fold";
            }

            //run ai call again, set all of the appropriate values
            for (int i = 1; i < 4; i++)
            {
                int call;
                if (players[i].inHand)
                {
                    call = AICall(i, CheckBet(), 0);
                    if (call > 0) //not max but called
                    {
                        players[i].chipCount -= call;
                        potTotal += call;

                        switch (i)
                        {
                            case 1:
                                {
                                    ai0post.Content = players[i].postBet + call;
                                    break;
                                }
                            case 2:
                                {
                                    ai1Post.Content = players[i].postBet + call;
                                    break;
                                }
                            case 3:
                                {
                                    ai2Post.Content = players[i].postBet + call;
                                    break;
                                }
                        }
                    }
                    else if (call == -1) //had max bet already
                    {
                        switch (i)
                        {
                            case 1:
                                {
                                    ai0post.Content = players[i].postBet;
                                    break;
                                }
                            case 2:
                                {
                                    ai1Post.Content = players[i].postBet;
                                    break;
                                }
                            case 3:
                                {
                                    ai2Post.Content = players[i].postBet;
                                    break;
                                }
                        }
                    }
                    else if (call == 0) //folded
                    {
                        switch (i)
                        {
                            case 1:
                                {
                                    ai0post.Content = "Fold";
                                    players[i].handName = Player.HandName.Fold;
                                    break;
                                }
                            case 2:
                                {
                                    ai1Post.Content = "Fold";
                                    players[i].handName = Player.HandName.Fold;
                                    break;
                                }
                            case 3:
                                {
                                    ai2Post.Content = "Fold";
                                    players[i].handName = Player.HandName.Fold;
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            {
                                ai0post.Content = "Fold";
                                players[i].handName = Player.HandName.Fold;
                                break;
                            }
                        case 2:
                            {
                                ai1Post.Content = "Fold";
                                players[i].handName = Player.HandName.Fold;
                                break;
                            }
                        case 3:
                            {
                                ai2Post.Content = "Fold";
                                players[i].handName = Player.HandName.Fold;
                                break;
                            }
                    }
                }
            }

            foreach (Player p in players)
            {
                p.clubCount = 0;
                p.diamondCount = 0;
                p.spadeCount = 0;
                p.heartCount = 0;
            }

            playerHand.Content = players[0].handName;
            ai0Hand.Content = players[1].handName;
            ai1Hand.Content = players[2].handName;
            ai2Hand.Content = players[3].handName;

            Winners();
            ShowChips();
            ShowImages();

            #region manage visibility
            resetButton.Visibility = Visibility.Visible;
            switchButton.Visibility = Visibility.Hidden;
            card1Box.Visibility = Visibility.Hidden;
            card2Box.Visibility = Visibility.Hidden;
            card3Box.Visibility = Visibility.Hidden;
            card4Box.Visibility = Visibility.Hidden;
            card5Box.Visibility = Visibility.Hidden;
            potLabel.Content = "Total in pot: " + potTotal;
            flipButton.Visibility = Visibility.Hidden;
            resetButton.Visibility = Visibility.Visible;
            betBox.Visibility = Visibility.Hidden;
            betAmountLabel.Visibility = Visibility.Hidden;
            foldLabel.Visibility = Visibility.Hidden;
            #endregion
        }

        private void Winners()
        {
            Player winner = new Player();
            winner.highCard = (int)Player.HandName.Nothing;
            //compare actual hands
            foreach (Player p in players)
            {
                if (p.inHand)
                {
                    if (p.handName > winner.handName)
                        winner = p;
                    else if (p.handName == winner.handName && p.handTotal > winner.handTotal)
                        winner = p;
                    else if (p.handName == winner.handName && p.handTotal == winner.handTotal && p.highCard > winner.highCard)
                        winner = p;
                }
            }

            if (winner == players[0])
            {
                winnerLabel.Content = "You win with hand: " + winner.handName;
                players[0].chipCount += potTotal;
            }
            else if (winner == players[1])
            {
                winnerLabel.Content = "Player 2 wins with hand: " + winner.handName;
                players[1].chipCount += potTotal;

            }
            else if (winner == players[2])
            {
                winnerLabel.Content = "Player 3 wins with hand: " + winner.handName;
                players[2].chipCount += potTotal;

            }
            else
            {
                winnerLabel.Content = "Player 4 wins with hand: " + winner.handName;
                players[3].chipCount += potTotal;

            }
            potTotal = 0;
        }

        private void ShowImages()
        {
            #region show card images
            if (players[1].inHand)
            {
                ai0_card0.Source = new BitmapImage(new Uri("Resources/" + players[1].hand[0].fileName, UriKind.Relative));
                ai0_card1.Source = new BitmapImage(new Uri("Resources/" + players[1].hand[1].fileName, UriKind.Relative));
                ai0_card2.Source = new BitmapImage(new Uri("Resources/" + players[1].hand[2].fileName, UriKind.Relative));
                ai0_card3.Source = new BitmapImage(new Uri("Resources/" + players[1].hand[3].fileName, UriKind.Relative));
                ai0_card4.Source = new BitmapImage(new Uri("Resources/" + players[1].hand[4].fileName, UriKind.Relative));
            }
            if (players[2].inHand)
            {

                ai1_card0.Source = new BitmapImage(new Uri("Resources/" + players[2].hand[0].fileName, UriKind.Relative));
                ai1_card1.Source = new BitmapImage(new Uri("Resources/" + players[2].hand[1].fileName, UriKind.Relative));
                ai1_card2.Source = new BitmapImage(new Uri("Resources/" + players[2].hand[2].fileName, UriKind.Relative));
                ai1_card3.Source = new BitmapImage(new Uri("Resources/" + players[2].hand[3].fileName, UriKind.Relative));
                ai1_card4.Source = new BitmapImage(new Uri("Resources/" + players[2].hand[4].fileName, UriKind.Relative));
            }
            if (players[3].inHand)
            {

                ai2_card0.Source = new BitmapImage(new Uri("Resources/" + players[3].hand[0].fileName, UriKind.Relative));
                ai2_card1.Source = new BitmapImage(new Uri("Resources/" + players[3].hand[1].fileName, UriKind.Relative));
                ai2_card2.Source = new BitmapImage(new Uri("Resources/" + players[3].hand[2].fileName, UriKind.Relative));
                ai2_card3.Source = new BitmapImage(new Uri("Resources/" + players[3].hand[3].fileName, UriKind.Relative));
                ai2_card4.Source = new BitmapImage(new Uri("Resources/" + players[3].hand[4].fileName, UriKind.Relative));
            }
            #endregion

        }


        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            theShoe.Clear();
            theShoe = CreateShoe(CardSet);
            foreach (Image i in table.Children.OfType<Image>())
            {
                //if(i.Name != "playerBlind")
                i.Source = new BitmapImage(new Uri("Resources/back.png", UriKind.Relative));
            } //reset cards
            foreach (Button b in table.Children.OfType<Button>())
            {
                b.Visibility = Visibility.Hidden;
            } //reset buttons
            foreach (Player p in players)
            {
                p.inHand = true;
                if (p.chipCount < 100)
                    p.chipCount = 1000;
            }
            dealButton.Visibility = Visibility.Visible;
            betBox.Visibility = Visibility.Hidden;
            betAmountLabel.Visibility = Visibility.Hidden;
            foldLabel.Visibility = Visibility.Hidden;
            playerHand.Content = "Your Hand";
            ai0Hand.Content = "Hand: ";
            ai1Hand.Content = "Hand: ";
            ai2Hand.Content = "Hand: ";
            ai0pre.Content = "";
            ai0post.Content = "";
            ai1Pre.Content = "";
            ai1Post.Content = "";
            ai2Pre.Content = "";
            ai2Post.Content = "";
            playerPreSwitch.Content = "Pre-Switch Bet: ";
            PlayerPostSwitch.Content = "Post-Switch Bet: ";
            winnerLabel.Content = "";
            card1Box.IsChecked = false;
            card2Box.IsChecked = false;
            card3Box.IsChecked = false;
            card4Box.IsChecked = false;
            card5Box.IsChecked = false;
            potTotal = 0;
            potLabel.Content = "Total in pot: " + potTotal;
            blind = (blind + 1) % 4;
            switch (blind)
            {
                case 0:
                    {
                        playerBlind.Source = new BitmapImage(new Uri("Resources/blind.png", UriKind.Relative));
                        playerBlind.Visibility = Visibility.Visible;
                        ai2Blind.Visibility = Visibility.Hidden;
                        break;
                    }
                case 1:
                    {
                        ai0Blind.Source = new BitmapImage(new Uri("Resources/blind.png", UriKind.Relative));
                        ai0Blind.Visibility = Visibility.Visible;
                        playerBlind.Visibility = Visibility.Hidden;
                        break;
                    }
                case 2:
                    {
                        ai1Blind.Source = new BitmapImage(new Uri("Resources/blind.png", UriKind.Relative));
                        ai1Blind.Visibility = Visibility.Visible;
                        ai0Blind.Visibility = Visibility.Hidden;
                        break;
                    }
                case 3:
                    {
                        ai2Blind.Source = new BitmapImage(new Uri("Resources/blind.png", UriKind.Relative));
                        ai2Blind.Visibility = Visibility.Visible;
                        ai1Blind.Visibility = Visibility.Hidden;
                        break;
                    }
            }
        }

        private int CheckBet()
        {
            string intString = betBox.Text;
            if (!Int32.TryParse(intString, out int x))
            {
                x = 0;
                betBox.Text = "10";
            }
            if (x < 0)
            {
                x = 0;
                betBox.Text = "10";
            }
            return x;
        }

        private void ShowChips()
        {
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            playerChipCount.Content = "Chips: " + players[i].chipCount.ToString();
                            break;
                        }
                    case 1:
                        {
                            ai0Chips.Content = "Chips: " + players[i].chipCount.ToString();
                            break;
                        }
                    case 2:
                        {
                            ai1Chips.Content = "Chips: " + players[i].chipCount.ToString();
                            break;
                        }
                    case 3:
                        {
                            ai2Chips.Content = "Chips: " + players[i].chipCount.ToString();
                            break;
                        }
                } //shows chip total
            }
        }

        #region hand evaluation
        private Player.HandName HandEval(int playerNum)
        {
            players[playerNum].spadeCount = 0;
            players[playerNum].diamondCount = 0;
            players[playerNum].heartCount = 0;
            players[playerNum].clubCount = 0;

            GetSuitCounts(playerNum);

            if (Straight(playerNum) && Flush(playerNum) && players[playerNum].hand[0].cardValue == 14)
                return Player.HandName.RoyalFlush;
            else if (Flush(playerNum) && Straight(playerNum))
                return Player.HandName.StraightFlush;
            else if (FourOfKind(playerNum))
                return Player.HandName.FourKind;
            else if (FullHouse(playerNum))
                return Player.HandName.FullHouse;
            else if (Flush(playerNum))
                return Player.HandName.Flush;
            else if (Straight(playerNum))
                return Player.HandName.Straight;
            else if (ThreeOfKind(playerNum))
                return Player.HandName.ThreeKind;
            else if (TwoPairs(playerNum))
                return Player.HandName.TwoPairs;
            else if (OnePair(playerNum))
                return Player.HandName.OnePair;
            else
            {
                HighCard(playerNum);
                return Player.HandName.Nothing;
            }
        }

        private void GetSuitCounts(int playerNum)
        {
            foreach (Card c in players[playerNum].hand)
            {
                if (c.suit == Card.SUIT.SPADES)
                    players[playerNum].spadeCount++;
                else if (c.suit == Card.SUIT.HEARTS)
                    players[playerNum].heartCount++;
                else if (c.suit == Card.SUIT.DIAMONDS)
                    players[playerNum].diamondCount++;
                else if (c.suit == Card.SUIT.CLUBS)
                    players[playerNum].clubCount++;
            }
        }

        private bool FourOfKind(int playerNum)
        {
            //either the first 4 or the last 4 will be the matches with the remaining card being the high card
            if (players[playerNum].hand[0].cardValue == players[playerNum].hand[1].cardValue &&
                players[playerNum].hand[0].cardValue == players[playerNum].hand[2].cardValue &&
                players[playerNum].hand[0].cardValue == players[playerNum].hand[3].cardValue)
            {
                players[playerNum].handTotal = (int)players[playerNum].hand[1].cardValue * 4;
                players[playerNum].highCard = (int)players[playerNum].hand[4].cardValue;
                return true;
            }
            else if (players[playerNum].hand[1].cardValue == players[playerNum].hand[2].cardValue &&
                players[playerNum].hand[1].cardValue == players[playerNum].hand[3].cardValue &&
                players[playerNum].hand[1].cardValue == players[playerNum].hand[4].cardValue)
            {
                players[playerNum].handTotal = (int)players[playerNum].hand[1].cardValue * 4;
                players[playerNum].highCard = (int)players[playerNum].hand[0].cardValue;
                return true;
            }

            return false;
        }

        private bool FullHouse(int playerNum)
        {
            if ((players[playerNum].hand[0].cardValue == players[playerNum].hand[1].cardValue &&
                players[playerNum].hand[0].cardValue == players[playerNum].hand[2].cardValue &&
                players[playerNum].hand[3].cardValue == players[playerNum].hand[4].cardValue))
            {
                players[playerNum].handTotal = players[playerNum].hand[0].cardValue * 16 + players[playerNum].hand[1].cardValue * 16 + players[playerNum].hand[2].cardValue * 16 + players[playerNum].hand[3].cardValue + players[playerNum].hand[4].cardValue;
                return true;
            }
            else if (players[playerNum].hand[0].cardValue == players[playerNum].hand[1].cardValue &&
                players[playerNum].hand[2].cardValue == players[playerNum].hand[3].cardValue &&
                players[playerNum].hand[2].cardValue == players[playerNum].hand[4].cardValue)
            {
                players[playerNum].handTotal = players[playerNum].hand[2].cardValue * 16 + players[playerNum].hand[3].cardValue * 16 + players[playerNum].hand[4].cardValue * 16 + players[playerNum].hand[0].cardValue + players[playerNum].hand[1].cardValue;
                return true;
            }
            return false;
        }

        private bool Flush(int playerNum)
        {
            //if all suits are the same
            if (players[playerNum].heartCount == 5 || players[playerNum].diamondCount == 5 ||
                players[playerNum].clubCount == 5 || players[playerNum].spadeCount == 5)
            {
                players[playerNum].handTotal = players[playerNum].hand[4].cardValue;
                players[playerNum].highCard = (int)players[playerNum].hand[4].cardValue;
                //if flush, the player with higher cards win
                //whoever has the last card the highest, has automatically all the cards total higher
                return true;
            }

            return false;
        }

        private bool Straight(int playerNum)
        {
            //if 5 consecutive values
            if (players[playerNum].hand[0].cardValue + 1 == players[playerNum].hand[1].cardValue &&
                players[playerNum].hand[1].cardValue + 1 == players[playerNum].hand[2].cardValue &&
                players[playerNum].hand[2].cardValue + 1 == players[playerNum].hand[3].cardValue &&
                players[playerNum].hand[3].cardValue + 1 == players[playerNum].hand[4].cardValue)
            {
                //player with the highest value of the last card wins
                players[playerNum].handTotal = (int)players[playerNum].hand[0].cardValue;
                return true;
            }

            return false;
        }

        private bool ThreeOfKind(int playerNum)
        {
            //if the 1,2,3 cards are the same OR
            //2,3,4 cards are the same OR
            //3,4,5 cards are the same
            //3rds card will always be a part of Three of A Kind
            if ((players[playerNum].hand[0].cardValue == players[playerNum].hand[1].cardValue &&
                players[playerNum].hand[0].cardValue == players[playerNum].hand[2].cardValue))
            {
                players[playerNum].highCard = (int)players[playerNum].hand[3].cardValue * 16 + (int)players[playerNum].hand[4].cardValue;
                players[playerNum].handTotal = players[playerNum].hand[2].cardValue * 3;
                return true;
            }
            else if (players[playerNum].hand[1].cardValue == players[playerNum].hand[2].cardValue &&
            players[playerNum].hand[1].cardValue == players[playerNum].hand[3].cardValue)
            {
                players[playerNum].highCard = (int)players[playerNum].hand[0].cardValue * 16 + (int)players[playerNum].hand[4].cardValue;
                players[playerNum].handTotal = players[playerNum].hand[2].cardValue * 3;
                return true;
            }
            else if (players[playerNum].hand[2].cardValue == players[playerNum].hand[3].cardValue &&
                players[playerNum].hand[2].cardValue == players[playerNum].hand[4].cardValue)
            {
                players[playerNum].handTotal = players[playerNum].hand[2].cardValue * 3;
                players[playerNum].highCard = (int)players[playerNum].hand[0].cardValue * 16 + (int)players[playerNum].hand[1].cardValue;
                return true;
            }
            return false;
        }

        private bool TwoPairs(int playerNum)
        {
            //if 1,2 and 3,4
            //if 1.2 and 4,5
            //if 2.3 and 4,5
            //with two pairs, the 2nd card will always be a part of one pair 
            //and 4th card will always be a part of second pair
            if (players[playerNum].hand[0].cardValue == players[playerNum].hand[1].cardValue &&
                players[playerNum].hand[2].cardValue == players[playerNum].hand[3].cardValue)
            {
                players[playerNum].handTotal = players[playerNum].hand[0].cardValue * 16 + players[playerNum].hand[2].cardValue;
                players[playerNum].highCard = (int)players[playerNum].hand[4].cardValue;
                return true;
            }
            else if (players[playerNum].hand[0].cardValue == players[playerNum].hand[1].cardValue &&
                players[playerNum].hand[3].cardValue == players[playerNum].hand[4].cardValue)
            {
                players[playerNum].handTotal = players[playerNum].hand[0].cardValue * 16 + players[playerNum].hand[3].cardValue;
                players[playerNum].highCard = (int)players[playerNum].hand[2].cardValue;
                return true;
            }
            else if (players[playerNum].hand[1].cardValue == players[playerNum].hand[2].cardValue &&
                players[playerNum].hand[3].cardValue == players[playerNum].hand[4].cardValue)
            {
                players[playerNum].handTotal = players[playerNum].hand[1].cardValue * 16 + players[playerNum].hand[3].cardValue;
                players[playerNum].highCard = (int)players[playerNum].hand[0].cardValue;
                return true;
            }
            return false;
        }

        private bool OnePair(int playerNum)
        {
            //if 1,2 -> 5th card has the highest value
            //2.3
            //3,4
            //4,5 -> card #3 has the highest value
            if (players[playerNum].hand[0].cardValue == players[playerNum].hand[1].cardValue)
            {
                players[playerNum].handTotal = players[playerNum].hand[0].cardValue;
                players[playerNum].highCard = (int)players[playerNum].hand[2].cardValue;
                return true;
            }
            else if (players[playerNum].hand[1].cardValue == players[playerNum].hand[2].cardValue)
            {
                players[playerNum].handTotal = players[playerNum].hand[1].cardValue;
                players[playerNum].highCard = (int)players[playerNum].hand[0].cardValue;
                return true;
            }
            else if (players[playerNum].hand[2].cardValue == players[playerNum].hand[3].cardValue)
            {
                players[playerNum].handTotal = players[playerNum].hand[2].cardValue;
                players[playerNum].highCard = (int)players[playerNum].hand[0].cardValue;
                return true;
            }
            else if (players[playerNum].hand[3].cardValue == players[playerNum].hand[4].cardValue)
            {
                players[playerNum].handTotal = players[playerNum].hand[3].cardValue;
                players[playerNum].highCard = (int)players[playerNum].hand[0].cardValue;
                return true;
            }

            return false;
        }

        private bool HighCard(int playerNum)
        {
            players[playerNum].handTotal = players[playerNum].hand[0].cardValue * 65536 + players[playerNum].hand[1].cardValue * 4096 + players[playerNum].hand[2].cardValue * 256 + players[playerNum].hand[3].cardValue * 16 + players[playerNum].hand[4].cardValue;
            return true;
        }
        #endregion

        private void Window_Closed(object sender, EventArgs e)
        {
            casinoApp.Properties.Settings.Default.chips = players[0].chipCount;
            casinoApp.Properties.Settings.Default.username = userName;
            casinoApp.Properties.Settings.Default.Save();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            casinoApp.Properties.Settings.Default.chips = players[0].chipCount;
            casinoApp.Properties.Settings.Default.username = userName;
            casinoApp.Properties.Settings.Default.Save();
        }
    }
}
