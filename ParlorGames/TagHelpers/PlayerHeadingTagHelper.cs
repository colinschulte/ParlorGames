using Microsoft.AspNetCore.Razor.TagHelpers;
using ParlorGames.Models;

namespace ParlorGames.TagHelpers
{
    [HtmlTargetElement("h5", Attributes = "my-player")]
    [HtmlTargetElement("h5", Attributes = "my-dealer")]
    [HtmlTargetElement("h5", Attributes = "my-opponent")]

    public class PlayerHeadingTagHelper : TagHelper
    {
        public Dealer MyDealer { get; set; }
        public Opponent MyOpponent { get; set; }
        public Player MyPlayer { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string text = "";

            if (MyDealer != null) {
                text = (MyDealer.MustShowCards) ? $"Dealer: {MyDealer.Hand.Total}": "Dealer";
            }
            if (MyOpponent != null)
            {
                text =  (MyOpponent.Hand.HasCards) ? $"Opponent: {MyOpponent.Hand.Cards[MyOpponent.Hand.Cards.Count-1].WarValue}" : "Opponent";
            }
            if (MyPlayer != null) {
                if (MyPlayer.WarDeck != null)
                {
                    text = (MyOpponent.Hand.HasCards) ? $"Player: {MyPlayer.Hand.Cards[MyPlayer.Hand.Cards.Count - 1].WarValue}" : "Player";
                }
                text = (MyPlayer.Hand.HasCards) ? $"Player: {MyPlayer.Hand.Total}" : "Player";
            }
            output.Content.Append(text);
        }
    }
}
