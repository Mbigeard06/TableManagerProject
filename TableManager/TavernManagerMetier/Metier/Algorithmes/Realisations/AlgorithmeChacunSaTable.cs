using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Tavernes;
using TavernManagerMetier.Metier.Algorithmes.Graphes;
using System.Diagnostics;

namespace TavernManagerMetier.Metier.Algorithmes.Realisations
{
    internal class AlgorithmeChacunSaTable : IAlgorithme
    {
        private long tempsExecution = -1;
        public string Nom => "Chacun sa table";
        public long TempsExecution => tempsExecution;
        public void Executer(Taverne taverne)
        {   Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Graphe graphTavern = new Graphe(taverne);
            for (int i = 0; i < taverne.Clients.Count(); i++)
            {
                taverne.AjouterTable();
                taverne.AjouterClientTable(i,i);
            }
            stopwatch.Stop();
            this.tempsExecution = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(this.tempsExecution.ToString());
        }
    }
}
