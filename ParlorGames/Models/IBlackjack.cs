using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParlorGames.Models
{
    public interface IBlackjack
    {
        Player Player { get; set; }
        Dealer Dealer { get; set; }

        Blackjack.Result Deal();
        Blackjack.Result Hit();
        Blackjack.Result Stand();
    }
}
