using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Metier.Algorithmes.Graphes
{
    internal class Graphe
    {
        private Dictionary<Client, Sommet> sommets;
        public List<Sommet> Sommets
        {
            get
            {
                return this.sommets.Values.Distinct().ToList<Sommet>();
            }
        }
        public Graphe(Taverne taverne)
        {
            sommets = new Dictionary<Client, Sommet>();

            foreach(Client client in taverne.Clients)
            {
                this.AjouterSommet(client, new Sommet());
            }
            foreach (Client client in taverne.Clients) 
            { 
                  foreach (Client ennemi in client.Ennemis)
                  { 
                    this.AjouterArete(client, ennemi); //On ajoute les relations avec les ennemis
                  }
            }
        }
        private void AjouterSommet(Client client, Sommet sommet)
        {
            if (!this.sommets.ContainsKey(client))
            {
                this.sommets[client] = sommet;//Les amis sont mis sur le même sommet 
                sommet.NbClients++;//On incrémente de 1 le nb de clients dans le sommet
                foreach (Client ami in client.Amis) this.AjouterSommet(ami, sommet);//On ajoute une clée/valeur amie mais qui renvoi vers le même sommet
            }
        }
        private void AjouterArete(Client client1, Client client2)
        {
            sommets[client1].ajouterVoisin(sommets[client2]);
        }

        //Obtenir le somemt associé au client
        public Sommet GetSommetWithClient(Client client)
        {        
            this.sommets.TryGetValue(client, out Sommet s);
            return s;
        }

        //Obtenir liste des degrées des sommets 
        public List<int> CalculDegrees()
        {
            List<int> degrees = new List<int>();

            for (int i = 0; i < this.Sommets.Count; i++) //On parcourt tous les sommets 
            {
                degrees[i] = this.Sommets[i].Voisins.Count;
            }
            return degrees;
        }


    }   
}
