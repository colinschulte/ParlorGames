using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParlorGames.Models
{
    public interface IWar
    {
        Player Player { get; set; }
        Opponent Opponent { get; set; }

        War.Result Flip();
    }
}
