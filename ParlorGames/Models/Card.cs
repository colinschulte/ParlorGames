using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParlorGames.Models
{
    public class Card
    {
        public string Rank { get; set; }
        public string Suit { get; set; }

        // don't store read-only properties in session
        [JsonIgnore]
        public bool IsAce => Rank == "A";
        [JsonIgnore]
        public bool IsFaceCard => Rank == "J" || Rank == "Q" || Rank == "K";
        [JsonIgnore]
        public string Name => $"{Rank}-{Suit}";
        [JsonIgnore]
        public int Value
        {
            get
            {
                if (IsAce)
                    return 11;
                else if (IsFaceCard)
                    return 10;
                else
                    return Convert.ToInt32(Rank);
            }
        }
        public int WarValue
        {
            get
            {
                if (IsAce)
                    return 14;
                else if (Rank == "J")
                    return 11;
                else if (Rank == "Q")
                    return 12;
                else if (Rank == "K")
                    return 13;
                else
                    return Convert.ToInt32(Rank);
            }
        }
    }
}
