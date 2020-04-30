using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriceChecker.Services
{
    interface ISubject
    {
        void Attach(IObserver obs);
        void Detach(IObserver obs);
        Task Notify();
    }
}
