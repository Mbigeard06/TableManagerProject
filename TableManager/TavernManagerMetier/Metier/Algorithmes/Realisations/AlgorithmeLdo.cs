using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using TavernManagerMetier.Metier.Algorithmes.Graphes;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Metier.Algorithmes.Realisations
{
    internal class AlgorithmeLdo : IAlgorithme 
    {
        private long tempsExecution = -1;
        public string Nom => "Algorithme LDO";
        public long TempsExecution => tempsExecution;
        private bool IsNeighborInAnyList(Sommet sommet, Dictionary<int, List<Sommet>> couleur)
        {
            return couleur.Values.Any(liste => liste.Any(s => s.Voisins.Contains(sommet)));
        }
        public void Executer(Taverne taverne)
        {
            //Création du graphe
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Graphe graphe = new Graphe(taverne);

            //On regarde si la taverne est réalisable 
            AnalyseTaverne.capaciteTableInsufisante(graphe.Sommets, taverne.CapactieTables);
            AnalyseTaverne.amisDennemis(taverne);

            //Initialisation des données.
            List<Sommet> sommets = graphe.Sommets;
            var comparateur = Comparer<Sommet>.Create((x, y) => y.Voisins.Count.CompareTo(x.Voisins.Count)); //Création d'un comparateur selon les degrées (ordre décroissant)
            sommets.Sort(comparateur);//Classement des sommets selon leurs degrés

            int lastGroupe = AlgorithmeDeColorationCroissante.ColorationOptimale(graphe.Sommets, taverne.CapactieTables);//On effectue la coloration optimale 
            
            //Mise en place du plant de table 
            for (int i = 0; i <= lastGroupe; i++) taverne.AjouterTable(); //Crééer autant de table que de couleur 
            foreach (Client client in taverne.Clients) //Pour chaque client on regarde la couleur de son sommet associé
            {
                Sommet sommet = graphe.GetSommetWithClient(client);
                taverne.AjouterClientTable(client.Numero, sommet.Couleur);
            }
            stopwatch.Stop();
            this.tempsExecution = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(this.tempsExecution.ToString());
        }
    }
}
