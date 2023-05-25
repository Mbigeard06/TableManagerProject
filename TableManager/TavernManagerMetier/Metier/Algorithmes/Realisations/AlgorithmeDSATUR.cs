using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;
using TavernManagerMetier.Metier.Algorithmes.Graphes;
using TavernManagerMetier.Metier.Algorithmes;
using TavernManagerMetier.Metier.Tavernes;
using TavernManagerMetier.Metier.Algorithmes.Realisations;
using System.Diagnostics.Eventing.Reader;

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

                if (AnalyseTaverne.nbClientGroupe(groupeCouleur) + s.NbClients > capacite)//Plus de place dans le groupe 
                {
                    i++;
                    continue;
                }

                foreach (Sommet sommetDuGroupe in groupeCouleur)
                {
                    if (sommetDuGroupe.ennemi(s)) //Le sommet a un ennemi dans le groupe 
                    {
                        friendlyWithGroup = false;
                        break;
                    }
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
        


        while (sommetATraiter.Count > 0) 
        {
                //Choix du sommet 
                Sommet sommetAColorier = null;
                int highestColoredNeighboors = 0;//Record de voisins coloriés
                List<Sommet> mostColored = new List<Sommet>();//Création de la liste qui contient la/les sommets avec le plus de voisins coloriés 
                foreach (Sommet s in sommetATraiter)//Calcul du nombre de voisins coloriés pour chaque sommet présent
                {
                    int coloredV = 0; //Nombre de voisins coloriés 
                    foreach (Sommet voisin in s.Voisins)
                    {
                        if (voisin.Couleur != null)//Le voisin a une couleur attribuée 
                        {
                            coloredV++;
                        }
                    }
                    if(coloredV > highestColoredNeighboors)//Le record est battu 
                    {
                        mostColored.Clear(); //Le sommet devient seul en tête 
                        mostColored.Add(s); 
                    }
                    else if (coloredV == highestColoredNeighboors)//Le record est égalisé
                    {
                        mostColored.Add(s);//On ajoute le sommet
                    }
                }
                
                
                if (mostColored.Count > 1) //Plusieurs sommets ont le même nombre de voisins coloriés 
                {
                    int highestNeighboors = 0;
                    foreach (Sommet s in mostColored)//On cherche celui avec le plus de voisins 
                    {
                        if (s.Voisins.Count() >= highestNeighboors)//Il bat le record du nombre de voisins
                        {
                            highestNeighboors = s.Voisins.Count();//Nouveau record fixé
                        }
                        else//Il ne bat pas le record
                        {
                            mostColored.Remove(s);//On l'enlève de la liste 
                        }
                    }
                }

                if (mostColored.Count > 1)//Plusieurs sommets ont le même nombre de voisins et le même nombre d'entre eux coloriés
                {
                    Random random = new Random(); //On choisi un sommet aléatoire dans la liste 
                    int index = random.Next(mostColored.Count());
                    sommetAColorier = mostColored[index];
                }
                else//Le sommet est seul dans la liste 
                {
                    sommetAColorier = mostColored[0];
                }
                //Coloration
                colorierSommet(sommetAColorier, couleur, taverne.CapactieTables);//On colorie le sommet
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

