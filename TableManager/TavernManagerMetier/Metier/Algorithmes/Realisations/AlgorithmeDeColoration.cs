using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Tavernes;
using TavernManagerMetier.Metier.Algorithmes.Graphes;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Automation.Peers;

namespace TavernManagerMetier.Metier.Algorithmes.Realisations
{
    public class AlgorithmeDeColoration : IAlgorithme
    {
        private long tempsExecution = -1;
        public string Nom => "Coloration croissante";
        public long TempsExecution => tempsExecution;
        private bool IsNeighborInAnyList(Sommet sommet, Dictionary<int, List<Sommet>> couleur)
        {
            return couleur.Values.Any(liste => liste.Any(s => s.Voisins.Contains(sommet)));
        }
        //Effectue une coloration sur un seul groupe donné et retire les sommets qui se sont vues attribuer un groupe
                              
        
         public void Executer(Taverne taverne)
        {
            //Création du graphe
            Graphe graphe = new Graphe(taverne);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //Initialisation des données.
            int firstFreeGroup = 0;
            bool attributed;
            bool friendlyWithGroup;
            bool groupeComplet;
            int i;//Index
            int capaciteTable = taverne.CapactieTables; //capacité d'une table
            Dictionary<int, List<Sommet>> couleur = new Dictionary<int, List<Sommet>>(); //Dictionnaire qui repertorie les groupes
            couleur[firstFreeGroup] = new List<Sommet>();
            foreach (Sommet s in graphe.Sommets)
            {
                attributed = false;
                i = firstFreeGroup;
                while (!attributed)
                {
                    if (couleur.TryGetValue(i, out List<Sommet> groupeCouleur)) //Le groupe existe 
                    {
                        groupeComplet = false;
                        friendlyWithGroup = true;
                        foreach (Sommet sommetDuGroupe in groupeCouleur)//Parcours des sommets du groupe 
                        {
                            if (sommetDuGroupe.ennemi(s))//Les deux sommets sont ennemis
                            {
                                friendlyWithGroup = false;
                            }
                        }
                        if (groupeCouleur.Count() >= capaciteTable)//Plus de place à la table 
                        {
                            groupeComplet = true;
                            if (i == firstFreeGroup)//Le premier groupe accessible est complet 
                            {
                                firstFreeGroup++;
                            }
                        }
                        if (friendlyWithGroup & !groupeComplet) //Le sommet n'a pas d'ennemi dans le groupe et il y'a de la place à la table
                        {
                            groupeCouleur.Add(s);
                            s.Couleur = i;
                            attributed = true;
                        }
                        else //Le sommet a des ennemis dans le groupe 
                        {
                            i++;
                        }
                    }
                    else //Le groupe n'existe pas 
                    {
                        couleur[i] = new List<Sommet>();
                        couleur[i].Add(s);
                        s.Couleur = i;
                        attributed = true;
                    }

                }

            }
            //Mise en place du plant de table 
            foreach (int key in couleur.Keys) taverne.AjouterTable(); //Crééer autant de table que de couleur 
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
