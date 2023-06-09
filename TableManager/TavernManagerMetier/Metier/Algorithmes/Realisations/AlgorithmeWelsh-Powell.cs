﻿using System;
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

        public long TempsExecution => tempsExecution;
        /// <summary>
        /// Coloration 
        /// </summary>
        /// <param name="sommets"> la liste des sommets </param>
        /// <param name="capaciteTable">la capacité de la table</param>
        /// <param name="GroupNumber"> la "couleur" du groupe </param>
        public void OneGroupeColoration(List<Sommet> sommets, int capaciteTable, int GroupNumber)
        {
            //Initialisation des variables
            List<Sommet> groupe = new List<Sommet>(); //Création du groupe 
            bool friendlyWithGroup;

            foreach (Sommet sommet in sommets)//On parcourt tous les sommets
            {
                friendlyWithGroup = true;
                
                if (AnalyseTaverne.nbClientGroupe(groupe) + sommet.NbClients > capaciteTable)//Plus de place dans le groupe
                {
                    break;
                }

                foreach (Sommet sommetDuGroupe in groupe)//Parcours des sommets du groupe 
                {
                    if (sommetDuGroupe.ennemi(sommet))//Les deux sommets sont ennemis
                    {
                        friendlyWithGroup = false;
                    }
                }
                
                if (friendlyWithGroup) //Le sommet n'a pas d'ennemi dans le groupe 
                {
                    groupe.Add(sommet);
                    sommet.Couleur = GroupNumber;
                }
            }
            sommets.RemoveAll(s => groupe.Contains(s));//On supprime les sommets ajoutés au groupe de la liste des sommets 
        }
        public void Executer(Taverne taverne)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //Création du graphe 
            Graphe graphe = new Graphe(taverne);
            int i = 0; //Index groupe
            
            //On regarde si la taverne est réalisable 
            AnalyseTaverne.capaciteTableInsufisante(graphe.Sommets, taverne.CapactieTables);
            AnalyseTaverne.amisDennemis(taverne);
            
            //Initialisation des données.
            List<Sommet> sommets = graphe.Sommets;
            var comparateur = Comparer<Sommet>.Create((x, y) => y.Voisins.Count.CompareTo(x.Voisins.Count)); //Création d'un comparateur selon les degrées (ordre décroissant)
            sommets.Sort(comparateur);//Classement des sommets selon leurs degrés (décroissants)
            while (sommets.Count > 0)//Tant que sommet n'est pas vide
            {
                OneGroupeColoration(sommets, taverne.CapactieTables, i); //On crée un nouveau groupe et on y affecte des sommets 
                taverne.AjouterTable();//On créée une table correspondant au groupe
                i++;
            }

            //Mise en place du plant de table
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


