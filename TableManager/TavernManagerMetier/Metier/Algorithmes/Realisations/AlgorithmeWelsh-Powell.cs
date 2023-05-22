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
    public class AlgorithmeWelsh_Powell : IAlgorithme
    {
        private long tempsExecution = -1;
        public string Nom => "Algorithme de Welsh-Powell";

        public long TempsExecution => -1;
        //Effectue la coloration d'un groupe de sommets 
        public static void OneGroupeColoration(List<Sommet> sommets, int capaciteTable, int GroupNumber)
        {
            //Initialisation des variables
            List<Sommet> groupe = new List<Sommet>(); //Création du groupe 
            int i = 0; //Indice du sommet pour parcourir la liste
            bool full = false;
            bool friendlyWithGroup;

            while (!full)
            {
                friendlyWithGroup = true;
                Sommet sommet = sommets[i]; //Récupération du sommet d'indice i

                foreach (Sommet sommetDuGroupe in groupe)//Parcours des sommets du groupe 
                {
                    if (sommetDuGroupe.ennemi(sommet))//Les deux sommets sont ennemis
                    {
                        friendlyWithGroup = false;
                    }
                }
                if (groupe.Count() >= capaciteTable)//Plus de place à la table 
                {
                    full = true;
                }
                if (friendlyWithGroup & !full) //Le sommet n'a pas d'ennemi dans le groupe 
                {
                    groupe.Add(sommet);
                    sommet.Couleur = GroupNumber;
                    sommets.Remove(sommet);
                }
                if (i < sommets.Count - 1)//On passe au sommet suivant 
                {
                    i++;
                }
                else
                {
                    full = true;
                }

            }
        }
        public void Executer(Taverne taverne)
        {
            //Création du graphe
            Graphe graphe = new Graphe(taverne);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int i = 0; //Index groupe

            //Initialisation des données.
            List<Sommet> sommets = graphe.Sommets;
            var comparateur = Comparer<Sommet>.Create((x, y) => y.Voisins.Count.CompareTo(x.Voisins.Count)); //Création d'un comparateur selon les degrées (ordre décroissant)
            sommets.Sort(comparateur);//Classement des sommets selon leurs degrés 
            Console.WriteLine();
            while (sommets.Count > 0)
            {
                OneGroupeColoration(sommets, taverne.CapactieTables, i);
                taverne.AjouterTable();
                i++;
            }
            Console.WriteLine();

            foreach (Client client in taverne.Clients) //Pour chaque client on regarde la couleur de son sommet associé
            {
                Sommet sommet = graphe.GetSommetWithClient(client);
                int couleurSommet = sommet.Couleur;
                taverne.AjouterClientTable(client.Numero, couleurSommet);
            }

            stopwatch.Stop();
            this.tempsExecution = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(this.tempsExecution.ToString());
        }
    }
}


