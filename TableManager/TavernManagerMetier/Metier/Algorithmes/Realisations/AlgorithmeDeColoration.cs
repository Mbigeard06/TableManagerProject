using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Algorithmes.Graphes;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Metier.Algorithmes.Realisations
{   /// <summary>
    /// Algorithme de coloration croissante 
    /// </summary>
    internal class AlgorithmeDeColorationCroissante : IAlgorithme
    {
        private long tempsExecution = -1;
        public string Nom => "Coloration croissante";
        public long TempsExecution => tempsExecution;
        private bool IsNeighborInAnyList(Sommet sommet, Dictionary<int, List<Sommet>> couleur)
        {
            return couleur.Values.Any(liste => liste.Any(s => s.Voisins.Contains(sommet)));
        }

        /// <summary>
        /// Coloration qui attribue pour chaque sommet la plus petite valeur possible et qui retourne le numéro du dernier groupe 
        /// </summary>
        /// <param name="capacite">la capacité des tables</param>
        /// <param name="listeSommets"> la liste des sommets </param>
        public static int ColorationOptimale(List<Sommet> listeSommets, int capacite)
        {
            int firstFreeGroup = 0;
            int nbGroups = 0;
            Dictionary<int, List<Sommet>> couleur = new Dictionary<int, List<Sommet>>();

            foreach (Sommet s in listeSommets)
            {
                bool attributed = false;
                int i = firstFreeGroup;

                while (!attributed)
                {
                    if (couleur.TryGetValue(i, out List<Sommet> groupeCouleur)) //Le groupe existe 
                    {
                        bool friendlyWithGroup = true;

                        foreach (Sommet sommetDuGroupe in groupeCouleur)
                        {
                            if (sommetDuGroupe.Voisins.Contains(s)) //Le sommet a un ennemi dans le groupe 
                            {
                                friendlyWithGroup = false;
                                break;
                            }
                        }

                        if (AnalyseTaverne.nbClientGroupe(groupeCouleur) + s.NbClients > capacite)//Plus de place dans le groupe 
                        {
                            i++;
                            continue;
                        }

                        if (friendlyWithGroup)//Le sommet n'a pas d'ennemis 
                        {

                            groupeCouleur.Add(s);
                            s.Couleur = i;
                            attributed = true;
                        }
                    }
                    else //Le groupe n'existe pas encore 
                    {
                        couleur[i] = new List<Sommet>();
                        couleur[i].Add(s);
                        s.Couleur = i;
                        attributed = true;
                        nbGroups++;
                    }

                    i++;//On passe au groupe suivant 
                }
            }

            return nbGroups;
        }

        public void Executer(Taverne taverne)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //Création du graphe
            Graphe graphe = new Graphe(taverne);
            //On regarde si la taverne est réalisable 
            AnalyseTaverne.capaciteTableInsufisante(graphe.Sommets, taverne.CapactieTables);
            AnalyseTaverne.amisDennemis(taverne);

            //On lance la coloration
            int lastGroupe = ColorationOptimale(graphe.Sommets, taverne.CapactieTables);//Mise en place de la coloration optimale et on récupére le numéro du dernier groupe 

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

