using Microsoft.AspNetCore.Mvc;
using ParlorGames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParlorGames.Controllers
{
    public class WarController : Controller
    {
        private IWar warGame { get; set; }
        public WarController(IWar w) => warGame = w;

        public IActionResult Index()
        {
            return View(warGame);
        }

        public RedirectToActionResult Flip()
        {
            var result = warGame.Flip();

            if (result == Models.War.Result.Shuffling)
            {
                TempData["message"] = "Shuffling. Press Deal to continue.";
                TempData["background"] = "info";
            }
            else if (result == Models.War.Result.PlayerWin)
            {
                TempData["message"] = "You win!";
                TempData["background"] = "success";
            }
            else if (result == Models.War.Result.OpponentWin)
            {
                TempData["message"] = "Dang! You lose.";
                TempData["background"] = "danger";
            }
            else if (result == Models.War.Result.Tie)
            {
                TempData["message"] = "THIS MEANS WAR!!!";
                TempData["background"] = "info";
            }
            else if (result == Models.War.Result.PlayerWarWin)
            {
                TempData["message"] = "You won the War!";
                TempData["background"] = "success";
            }
            else if (result == Models.War.Result.OpponentWarWin)
            {
                TempData["message"] = "Dang, Your opponent won the War...";
                TempData["background"] = "danger";
            }

            return RedirectToAction("Index");

        }
        public RedirectToActionResult Back()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
