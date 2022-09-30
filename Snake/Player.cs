using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Player
    {
        //имя
        public string PlayerName { get; set; }
        //счетчик очков
        public string Score { get; set; }
        //карта, на которой чел играл
        public string PlayingMap { get; set; }
    }
}
