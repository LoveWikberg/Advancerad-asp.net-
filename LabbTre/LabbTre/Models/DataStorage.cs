using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabbTre.Models
{
    public class DataStorage
    {
        public List<Order> orders = new List<Order>
        {
            new Order{Id="RA-2999", Text="Kör mot rött"},
            new Order{Id="RB-0005", Text="Kör mot grönt"},
            new Order{Id="RA-0505", Text="Två små morötter"},
        };
    }
}
