using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParlorGames.Models;

namespace ParlorGames.Components
{
    public class WarWinningsDisplay : ViewComponent
    {
        private IWar game { get; set; }
        public WarWinningsDisplay(IWar g) => game = g;
        public IViewComponentResult Invoke() => View(game.Player.TotalWins);
    }
}
