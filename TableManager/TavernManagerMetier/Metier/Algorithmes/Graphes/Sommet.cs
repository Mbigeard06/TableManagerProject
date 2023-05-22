using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Metier.Algorithmes.Graphes
{
    public class Sommet
    {
        private List<Sommet> voisins; //représente les ennemis 
        private int nbClients; //représente le nombre d'amis 
        private int couleur;
        public int Couleur
        {
            set { couleur = value; }
            get { return couleur; }
        }
        public int NbClients
        {
            get { return nbClients; }
            set { nbClients = value; }
        }
        public List<Sommet> Voisins
        {
            get { return voisins; }
            set { voisins = value; }
        }
        public Sommet()
        {
            nbClients = 0;
            this.voisins = new List<Sommet>();
        }
        public void ajouterVoisin(Sommet sommet)
        {
            this.voisins.Add(sommet);
        }
        public bool ennemi(Sommet sommet)
        {
            return this.voisins.Contains(sommet);
        }
    }
}
