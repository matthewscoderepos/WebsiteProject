using System.Collections.Generic;

namespace Casino
{
    public class Player
    {
        public Player(List<Card> h, int val)
        {
            hand = new List<Card>(h);
            handTotal = val;
            aceCount = 0;
            chipCount = 1000;
            spadeCount = 0;
            heartCount = 0;
            diamondCount = 0;
            clubCount = 0;
            inHand = true;
        }
        public Player()
        {

        }
        public enum HandName
        {
            Fold,
            Nothing,
            OnePair,
            TwoPairs,
            ThreeKind,
            Straight,
            Flush,
            FullHouse,
            FourKind,
            StraightFlush,
            RoyalFlush
        }

        public HandName handName;
        public int aceCount;
        public int chipCount;
        public List<Card> hand = new List<Card>();
        public int handTotal;
        public int spadeCount;
        public int heartCount;
        public int diamondCount;
        public int clubCount;
        public int highCard;
        public int preBet;
        public int postBet;
        public int lastBet;
        public bool inHand;
        public int UpdateTotal(Card card)
        {
            handTotal += card.cardValue;
            return handTotal;
        }
    }
}
