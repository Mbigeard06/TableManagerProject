using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Algorithmes.Graphes;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Metier.Algorithmes.Realisations
{
    public class AlgorithmeDeTest : IAlgorithme
    {
        private long tempsExecution = -1;
        public string Nom => "Algorithme de Test";
        public long TempsExecution => tempsExecution;
        private bool IsNeighborInAnyList(Sommet sommet, Dictionary<int, List<Sommet>> couleur)
        {
            return couleur.Values.Any(liste => liste.Any(s => s.Voisins.Contains(sommet)));
        }
        public void Executer(Taverne taverne)
        {
            //Création du graphe
            Graphe graphe = new Graphe(taverne);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int i = 0; //Index groupe

            //Initialisation des données.
        }
    }
}
