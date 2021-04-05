using Microsoft.AspNetCore.Mvc;
using ParlorGames.Models;

namespace ParlorGames.Components
{
    public class HandDisplay : ViewComponent
    {
        public IViewComponentResult Invoke(Hand hand) => View(hand);
    }
}
