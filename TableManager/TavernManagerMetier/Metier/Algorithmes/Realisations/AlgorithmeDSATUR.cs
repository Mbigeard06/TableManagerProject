using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;
using TavernManagerMetier.Metier.Algorithmes.Graphes;
using TavernManagerMetier.Metier.Algorithmes;
using TavernManagerMetier.Metier.Tavernes;
using TavernManagerMetier.Metier.Algorithmes.Realisations;

public class AlgorithmeDSATUR : IAlgorithme
{
    private long tempsExecution = -1;
    public string Nom => "Algorithme DSATUR";
    public long TempsExecution => tempsExecution;

    
    public void colorierSommet(Sommet s, Dictionary<int, List<Sommet>> couleur, int capacite)
    {
        bool attributed = false;
        int i = 0;
        //Parcourir les groupes de couleurs 
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
            }

            i++;//On passe au groupe suivant 
        }
    }   
    
    /// <summary>
    /// L'algorithme DSATUR
    /// </summary>
    /// <param name="taverne">Taverne générée </param>
    public void Executer(Taverne taverne)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        // Création du graphe
        Graphe graphe = new Graphe(taverne);

        //On regarde si la taverne est réalisable 
        AnalyseTaverne.capaciteTableInsufisante(graphe.Sommets, taverne.CapactieTables);
        AnalyseTaverne.amisDennemis(taverne);

        // Initialisation des données
        List<Sommet> sommetATraiter = graphe.Sommets;
        int nbSommetsColored = 0;
        Dictionary<int, List<Sommet>> couleur = new Dictionary<int, List<Sommet>>();
        Dictionary<Sommet, int> coloredVoisins = new Dictionary<Sommet, int>(); //Associe à chaque sommet son nombre de voisins coloriés 


        while (sommetATraiter.Count > 0) 
        {
                //Choix du sommet 
                Sommet sommetAColorier = null;
                coloredVoisins.Clear();
                foreach (Sommet s in sommetATraiter)
                {
                    int coloredV = 0;
                    foreach (Sommet voisin in s.Voisins)
                    {
                        if (voisin.Couleur != null)
                        {
                            coloredV++;
                        }
                    }
                    coloredVoisins.Add(s, coloredV);
                }
                
                List<Sommet> mostColored = new List<Sommet>();
                mostColored.Clear();
                foreach (Sommet s in coloredVoisins.Keys)
                {
                    int highestColoredNeighboors = 0;
                    if (coloredVoisins[s] > highestColoredNeighboors)
                    {
                        highestColoredNeighboors = coloredVoisins[s];
                        mostColored.Clear();
                        mostColored.Add(s);
                    }
                    else if (coloredVoisins[s] == highestColoredNeighboors)
                    {
                        mostColored.Add(s);
                    }
                    sommetAColorier = mostColored[0];
                }
                
                if (mostColored.Count > 1) //Plusieurs sommets ont le même nombre de voisins coloriés 
                {
                    int highestNeighboors = 0;
                    foreach (Sommet s in mostColored)
                    {
                        if (s.Voisins.Count() >= highestNeighboors)
                        {
                            highestNeighboors = s.Voisins.Count();
                        }
                        else
                        {
                            mostColored.Remove(s);
                        }
                    }
                    sommetAColorier = mostColored[0];
                }
                if (mostColored.Count > 1)
                {
                    Random random = new Random();
                    int index = random.Next(mostColored.Count());
                    sommetAColorier = mostColored[index];
                }
                //Coloration
                colorierSommet(sommetAColorier, couleur, taverne.NombreTables);
                sommetATraiter.Remove(sommetAColorier);//On enlève le sommet 
        }
        

        // Mise en place du plan de table
        int nombreCouleurs = couleur.Count;
        for (int i = 0; i < nombreCouleurs; i++)
        {
            taverne.AjouterTable();
        }
        foreach (Client client in taverne.Clients)
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

