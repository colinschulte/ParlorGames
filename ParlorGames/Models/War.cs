using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParlorGames.Models
{
    public class War : Game
    {
        private ISession session { get; set; }
        public Deck Deck { get; set; }
        public Player Player { get; set; }
        public Opponent Opponent { get; set; }

        public War(IHttpContextAccessor accessor)
        {
            // retrieve game data from session
            session = accessor.HttpContext.Session;
            Deck = session.GetObject<Deck>("deck") ?? new Deck();
            Player = session.GetObject<Player>("player") ?? new Player();
            Opponent = session.GetObject<Opponent>("opponent") ?? new Dealer();
        }
    }
}
