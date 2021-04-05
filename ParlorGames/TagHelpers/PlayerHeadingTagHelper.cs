﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using ParlorGames.Models;

namespace ParlorGames.TagHelpers
{
    [HtmlTargetElement("h5", Attributes = "my-player")]
    [HtmlTargetElement("h5", Attributes = "my-dealer")]
    public class PlayerHeadingTagHelper : TagHelper
    {
        public Dealer MyDealer { get; set; }
        public Player MyPlayer { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string text = "";

            if (MyDealer != null) {
                text = (MyDealer.MustShowCards) ? $"Dealer: {MyDealer.Hand.Total}": "Dealer";
            }
            if (MyPlayer != null) {
                text = (MyPlayer.Hand.HasCards) ? $"Player: {MyPlayer.Hand.Total}" : "Player";
            }
            output.Content.Append(text);
        }
    }
}