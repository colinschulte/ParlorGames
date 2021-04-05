using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParlorGames.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ParlorGames.Controllers
{
    public class HomeController : Controller
    {

        private IBlackjack game { get; set; }
        public HomeController(IBlackjack g) => game = g;


        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Blackjack()
        {
            return View(game);
        }


        public IActionResult War()
        {
            return View();
        }

        public RedirectToActionResult Deal()
        {
            var result = game.Deal();

            if (result == Models.Blackjack.Result.Shuffling)
            {
                TempData["message"] = "Shuffling. Press Deal to continue.";
                TempData["background"] = "info";
            }
            else if (result == Models.Blackjack.Result.PlayerBlackJack)
            {
                TempData["message"] = "Blackjack! You win!";
                TempData["background"] = "success";
            }
            else if (result == Models.Blackjack.Result.DealerBlackJack)
            {
                TempData["message"] = "Dang! Dealer got a Blackjack! You lose.";
                TempData["background"] = "danger";
            }
            else if (result == Models.Blackjack.Result.DoubleBlackJack)
            {
                TempData["message"] = "Push";
                TempData["background"] = "info";
            }

            return RedirectToAction("Blackjack");

        }

        public RedirectToActionResult Hit()
        {
            var result = game.Hit();

            if (result == Models.Blackjack.Result.Shuffling)
            {
                TempData["message"] = "Shuffling. Press Hit to continue.";
                TempData["background"] = "info";
            }
            else if (result == Models.Blackjack.Result.PlayerBust)
            {
                TempData["message"] = "BUST! You lose.";
                TempData["background"] = "danger";
            }

            return RedirectToAction("Blackjack");
        }

        public RedirectToActionResult Stand()
        {
            var result = game.Stand();

            if (result == Models.Blackjack.Result.Shuffling)
            {
                TempData["message"] = "Shuffling. Press Hit to continue.";
                TempData["background"] = "info";
            }
            else if (result == Models.Blackjack.Result.Continue)
            {
                TempData["message"] = "Dealer needs another card. Hit Stand to continue.";
                TempData["background"] = "info";
            }
            else if (result == Models.Blackjack.Result.DealerBust)
            {
                TempData["message"] = "Dealer BUST! You win!";
                TempData["background"] = "success";
            }
            else if (result == Models.Blackjack.Result.DealerWin)
            {
                TempData["message"] = "You lose.";
                TempData["background"] = "danger";
            }
            else if (result == Models.Blackjack.Result.PlayerWin)
            {
                TempData["message"] = "You win!";
                TempData["background"] = "success";
            }
            else if (result == Models.Blackjack.Result.Push)
            {
                TempData["message"] = "PUSH";
                TempData["background"] = "info";
            }

            return RedirectToAction("Blackjack");



        }
//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
