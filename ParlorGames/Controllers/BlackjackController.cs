using Microsoft.AspNetCore.Mvc;
using ParlorGames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParlorGames.Controllers
{
    public class BlackjackController : Controller
    {
        private IBlackjack bjGame { get; set; }
        public BlackjackController(IBlackjack b) => bjGame = b;
        public IActionResult Index()
        {
            return View(bjGame);
        }

        public RedirectToActionResult Deal()
        {
            var result = bjGame.Deal();

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

            return RedirectToAction("Index");

        }

        public RedirectToActionResult Hit()
        {
            var result = bjGame.Hit();

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

            return RedirectToAction("Index");
        }

        public RedirectToActionResult Stand()
        {
            var result = bjGame.Stand();

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

            return RedirectToAction("Index");



        }
        public RedirectToActionResult Back()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
