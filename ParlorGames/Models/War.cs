﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParlorGames.Models
{
    public class War : IWar
    {
        private ISession session { get; set; }
        public Deck Deck { get; set; }
        public WarDeck PlayerDeck { get; set; }
        public WarDeck OpponentDeck { get; set; }
        public Hand PlayerHand { get; set; }
        public Hand OpponentHand { get; set; }
        public Player Player { get; set; }
        public Opponent Opponent { get; set; }
        public bool IsWar { get; set; }
        public enum Result
        {
            Shuffling, Continue, DealerBust, DealerBlackJack, OpponentWin,
            PlayerBust, PlayerBlackJack, PlayerWin, Tie, Push
        }

        public War(IHttpContextAccessor accessor)
        {
            // retrieve game data from session
            session = accessor.HttpContext.Session;
            Deck = session.GetObject<Deck>("deck") ?? new Deck();
            PlayerDeck = session.GetObject<WarDeck>("playerdeck") ?? new WarDeck();
            OpponentDeck = session.GetObject<WarDeck>("opponentdeck") ?? new WarDeck();
            PlayerHand = session.GetObject<Hand>("playerhand") ?? new Hand();
            OpponentHand = session.GetObject<Hand>("opponenthand") ?? new Hand();
            Player = session.GetObject<Player>("player") ?? new Player();
            Opponent = session.GetObject<Opponent>("opponent") ?? new Opponent();
            int warVal = session.GetInt32("iswar") ?? 0;  // default value of false (0)
            IsWar = Convert.ToBoolean(warVal);
        }

        public Result Flip()
        {
            var result = Result.Continue;  // default result

            // on first load, deck is null - shuffle
            if (PlayerDeck.Cards == null)
            {
                var WarDecks = Deck.MakeWarDecks();
                PlayerDeck = WarDecks.Item1;
                OpponentDeck = WarDecks.Item2;
                Player.CardCount = PlayerDeck.Cards.Count();
                Opponent.CardCount = OpponentDeck.Cards.Count();
            }

            if (IsWar == true)
            {
                int CardsInHand = PlayerHand.Cards.Count;
                int i = 0;
                while (i < 4)
                {
                    PlayerHand.Cards.Add(PlayerDeck.Cards[CardsInHand+i]);
                    OpponentHand.Cards.Add(OpponentDeck.Cards[CardsInHand + i]);
                    i++;
                }
                Player.Hand = PlayerHand;
                Opponent.Hand = OpponentHand;
                CardsInHand = PlayerHand.Cards.Count;
                if (PlayerHand.Cards[CardsInHand-1].WarValue > OpponentHand.Cards[CardsInHand-1].WarValue)
                {
                    result = Result.PlayerWin;
                    Player.Winner = true;
                    int count = 0;
                    while(count < CardsInHand)
                    {
                        PlayerDeck.Cards.Add(PlayerDeck.Cards[0]);
                        PlayerDeck.Cards.Remove(PlayerDeck.Cards[0]);
                        PlayerDeck.Cards.Add(OpponentDeck.Cards[0]);
                        OpponentDeck.Cards.Remove(OpponentDeck.Cards[0]);
                        count++;
                    }
                    Player.CardCount = PlayerDeck.Cards.Count();
                    Opponent.CardCount = OpponentDeck.Cards.Count();
                    IsWar = false;
                    Update();
                }
                else if (PlayerHand.Cards[CardsInHand-1].WarValue < OpponentHand.Cards[CardsInHand-1].WarValue)
                {
                    result = Result.OpponentWin;
                    Player.Winner = false;

                    int count = 0;
                    while (count < CardsInHand)
                    {
                        OpponentDeck.Cards.Add(PlayerDeck.Cards[0]);
                        PlayerDeck.Cards.Remove(PlayerDeck.Cards[0]);
                        OpponentDeck.Cards.Add(OpponentDeck.Cards[0]);
                        OpponentDeck.Cards.Remove(OpponentDeck.Cards[0]);
                        count++;
                    }

                    Player.CardCount = PlayerDeck.Cards.Count();
                    Opponent.CardCount = OpponentDeck.Cards.Count();
                    IsWar = false;
                    Update();
                }
                else if (PlayerHand.Cards[CardsInHand-1].WarValue == OpponentHand.Cards[CardsInHand-1].WarValue)
                {
                    result = Result.Tie;
                    IsWar = true;
                }
            }
            else
            {
                //get top card from both player's decks, then remove them
                Card PlayerCard = PlayerDeck.Cards[0];
                Card OpponentCard = OpponentDeck.Cards[0];
                PlayerHand.Cards.Clear();
                OpponentHand.Cards.Clear();
                PlayerHand.Cards.Add(PlayerCard);
                OpponentHand.Cards.Add(OpponentCard);
                Player.Hand = PlayerHand;
                Opponent.Hand = OpponentHand;
                if (PlayerCard.WarValue > OpponentCard.WarValue)
                {
                    result = Result.PlayerWin;
                    Player.Winner = true;
                    PlayerDeck.Cards.Remove(PlayerCard);
                    OpponentDeck.Cards.Remove(OpponentCard);
                    PlayerDeck.Cards.Add(PlayerCard);
                    PlayerDeck.Cards.Add(OpponentCard);
                    Player.CardCount = PlayerDeck.Cards.Count();
                    Opponent.CardCount = OpponentDeck.Cards.Count();
                    Update();
                }
                else if (PlayerCard.WarValue < OpponentCard.WarValue)
                {
                    result = Result.OpponentWin;
                    Player.Winner = false;
                    PlayerDeck.Cards.Remove(PlayerCard);
                    OpponentDeck.Cards.Remove(OpponentCard);
                    OpponentDeck.Cards.Add(PlayerCard);
                    OpponentDeck.Cards.Add(OpponentCard);
                    Player.CardCount = PlayerDeck.Cards.Count();
                    Opponent.CardCount = OpponentDeck.Cards.Count();
                    Update();
                }
                else if (PlayerCard.WarValue == OpponentCard.WarValue)
                {
                    result = Result.Tie;
                    IsWar = true;
                }
            }
            Save();
            return result;
        }

        public Result WarFlip(Hand PlayerHand, Hand OpponentHand)
        {
            var result = Result.Continue;  // default result


            return result;
        }
        
        private int Bet => 10;
        
        private void Update()
        {
            if (Player.Winner == true)
            {
                Player.TotalWinnings += Bet;
            }
            else if (Player.Winner == false)
            {
                Player.TotalWinnings -= Bet;
            }
        }

        private void Save()
        {
            // save game data to session
            session.SetObject<Deck>("deck", Deck);
            session.SetObject<WarDeck>("playerdeck", PlayerDeck);
            session.SetObject<WarDeck>("opponentdeck", OpponentDeck);
            session.SetObject<Hand>("playerhand", PlayerHand);
            session.SetObject<Hand>("opponenthand", OpponentHand);
            session.SetObject<Player>("player", Player);
            session.SetObject<Opponent>("opponent", Opponent);    
            session.SetInt32("iswar", Convert.ToInt32(IsWar));
        }
    }
}
