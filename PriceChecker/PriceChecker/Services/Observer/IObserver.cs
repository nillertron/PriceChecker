using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriceChecker.Services
{
    interface IObserver
    {

        Task Update();
    }
}
