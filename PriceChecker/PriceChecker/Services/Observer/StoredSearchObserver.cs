using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriceChecker.Services
{
    class StoredSearchObserver : ISubject
    {
        private static List<IObserver> ObserverListe = new List<IObserver>();
        public void Attach(IObserver obs)
        {
            //Dette skal fjernes hvis pattern udvides på et tidspunkt
            if(ObserverListe.Count > 0)
            {
                Detach(ObserverListe[0]);
            }
            ObserverListe.Add(obs);
        }

        public void Detach(IObserver obs)
        {
            ObserverListe.Remove(obs);
        }

        public async Task Notify()
        {
            ObserverListe.ForEach(async(o) => await o.Update());
        }
    }
}
