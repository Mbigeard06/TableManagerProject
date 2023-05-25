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
        public static int ColorationOptimale(List<Sommet> listeSommets, int capacite)
        {
            int firstFreeGroup = 0;
            bool attributed;
            bool friendlyWithGroup;
            bool groupeComplet;
            int i;
            Dictionary<int, List<Sommet>> couleur = new Dictionary<int, List<Sommet>>();
            couleur[firstFreeGroup] = new List<Sommet>();

            foreach (Sommet s in listeSommets)
            {
                attributed = false;
                i = firstFreeGroup;

                while (!attributed)
                {
                    if (couleur.TryGetValue(i, out List<Sommet> groupeCouleur))
                    {
                        groupeComplet = false;
                        friendlyWithGroup = true;

                        foreach (Sommet sommetDuGroupe in groupeCouleur)
                        {
                            if (sommetDuGroupe.ennemi(s)) // Utilisation de la méthode Ennemi() pour vérifier les ennemis
                            {
                                friendlyWithGroup = false;
                                break;
                            }
                        }

                        if (groupeCouleur.Count >= capacite)
                        {
                            groupeComplet = true;

                            if (i == firstFreeGroup)
                            {
                                firstFreeGroup++;
                            }
                        }

                        if (friendlyWithGroup && !groupeComplet)
                        {
                            groupeCouleur.Add(s);
                            s.Couleur = i;
                            attributed = true;
                            break; // Sortie de la boucle while dès que la couleur est attribuée
                        }
                        else
                        {
                            i++;
                        }
                    }
                    else
                    {
                        couleur[i] = new List<Sommet>();
                        couleur[i].Add(s);
                        s.Couleur = i;
                        attributed = true;
                        break; // Sortie de la boucle while dès que la couleur est attribuée
                    }
                }
            }

            return couleur.Keys.Last();
        }


        public void Executer(Taverne taverne)
        {
            //Création du graphe
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Graphe graphe = new Graphe(taverne);

            int lastGroupe = ColorationOptimale(graphe.Sommets, taverne.CapactieTables);//Mise en place de la coloration optimale et on récupére le numéro du dernier groupe 

            //Mise en place du plant de table 
            stopwatch.Start();
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

