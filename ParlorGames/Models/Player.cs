using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParlorGames.Models
{
    public class Player
    {
        public Player() => Hand = new Hand(); // initialize on load

        public Hand Hand { get; set; }
        public WarDeck WarDeck { get; set; }
        public int TotalWinnings { get; set; }
        public int GameWins { get; set; }
        public int GameLosses { get; set; }
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }
        public bool Winner { get; set; }
        public int CardCount { get; set; }

        public void NewBJHand(Card card1, Card card2)
        {
            Hand = new Hand();
            Hand.Add(card1);
            Hand.Add(card2);
        }

        public void NewWarHand(Card card1)
        {
            Hand = new Hand();
            Hand.Cards.Add(card1);
        }

        public void NewWarHand(Card card1, Card card2, Card card3, Card card4, Card card5)
        {
            Hand = new Hand();
            Hand.Add(card1);
            Hand.Add(card2);
            Hand.Add(card3);
            Hand.Add(card4);
            Hand.Add(card5);
        }

        public void Hit(Card card)
        {
            Hand.Add(card);
        }
    }
}
