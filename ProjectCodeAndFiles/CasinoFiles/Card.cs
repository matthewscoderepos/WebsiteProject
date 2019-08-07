namespace Casino
{
    public class Card
    {
        public Card(int x, string file)
        {
            cardValue = x;
            fileName = file;
        }

        public Card(int x, string file, SUIT s)
        {
            cardValue = x;
            fileName = file;
            suit = s;
        }

        public int cardValue;
        public string fileName;
        public SUIT suit;
        public enum SUIT
        {
            SPADES,
            HEARTS,
            DIAMONDS,
            CLUBS
        }
    }
}
