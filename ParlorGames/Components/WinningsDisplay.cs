using Microsoft.AspNetCore.Mvc;
using ParlorGames.Models;

namespace ParlorGames.Components
{
    public class WinningsDisplay : ViewComponent
    {
        private IBlackjack game { get; set; }
        public WinningsDisplay(IBlackjack g) => game = g;

        public IViewComponentResult Invoke() => View(game.Player.TotalWinnings);
    }

}
