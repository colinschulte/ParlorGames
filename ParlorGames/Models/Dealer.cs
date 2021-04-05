using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParlorGames.Models;

namespace ParlorGames.Models
{
    public class Dealer : Opponent
    {
        // don't store read-only properties in session
        [JsonIgnore]
        public bool MustHit => Hand.Total < 17 || Hand.HasSoftSeventeen;
        [JsonIgnore]
        public bool MustShowCards => Hand.HasCards && !Hand.HideHoleCard;

        public void ShowCards() => Hand.HideHoleCard = false;
    }
}
