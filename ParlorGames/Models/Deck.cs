using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParlorGames.Models
{
    public class Deck
    {
        private string[] ranks = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        private string[] suits = { "C", "D", "H", "S" };

        public List<Card> Cards { get; set; }

        public void Shuffle()
        {
            Cards = new List<Card>();
            foreach (string r in ranks)
            {
                foreach (string s in suits)
                {
                    Cards.Add(new Card { Rank = r, Suit = s });
                }
            }
        }

        public (WarDeck, WarDeck) MakeWarDecks()
        {
            Shuffle();
            int count = 0;
            WarDeck PlayerDeck = new WarDeck();
            WarDeck OpponentDeck = new WarDeck();
            PlayerDeck.Cards = new List<Card>();
            OpponentDeck.Cards = new List<Card>();
            while (count < 52)
            {
                // get a card at random
                Random random = new Random();
                int index = 0;
                index = random.Next(Cards.Count);
                //retrieve card and add it to a deck
                Card card = Cards[index];
                if(count % 2 == 0)
                {
                    PlayerDeck.Cards.Add(card);
                }
                else
                {
                    OpponentDeck.Cards.Add(card);
                }
                Cards.Remove(card);

                count++;
            }
            return (PlayerDeck, OpponentDeck);
        }

        public Card Deal()
        {
            // get a card at random
            Random random = new Random();
            int index = random.Next(Cards.Count);

            // retrieve card and then remove it from the deck
            Card card = Cards[index];
            Cards.Remove(card);

            return card;
        }
    }
}
