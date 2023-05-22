using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Metier.Algorithmes.Realisations
{
    public class AlgorithmeExemple : IAlgorithme
    {
        private long tempsExecution = -1;
        public string Nom => "Exemple";

        public long TempsExecution => -1;

        public void Executer(Taverne taverne)
        {
            taverne.AjouterTable();
            taverne.AjouterTable();
            for (int i=0;i<taverne.Clients.Count();i++)
            {
                taverne.AjouterClientTable(i, i % 2);
            }
        }
    }
}
