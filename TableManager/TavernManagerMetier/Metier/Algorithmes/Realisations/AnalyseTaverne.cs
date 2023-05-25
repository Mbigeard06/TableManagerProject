using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Algorithmes.Graphes;
using TavernManagerMetier.Exceptions.Realisations.GestionTaverne;
using TavernManagerMetier.Metier.Tavernes;
using System.ComponentModel;

namespace TavernManagerMetier.Metier.Algorithmes.Realisations
{   
    /// <summary>
    /// Cette classe analyse si la taverne est réalisable 
    /// </summary>
    internal class AnalyseTaverne
    {
        /// <summary>
        /// Permet de compter le nombre de clients dans une liste de sommets 
        /// </summary>
        /// <param name="listeSommets">Liste qui représente le groupe de sommets</param>
        /// <returns></returns>
        public static int nbClientGroupe(List<Sommet> listeSommets)
        {
            int nbSommetGroup = 0;
            foreach (Sommet sommet in listeSommets)
            {
                nbSommetGroup++;
            }
            return nbSommetGroup;

        }
        /// <summary>
        /// Regarde si un groupe d'amis est plus nombreux que la capacité d'une table
        /// </summary>
        /// <param name="listeSommets"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static void capaciteTableInsufisante(List<Sommet> listeSommets, int c )
        {
            bool a = false;
            foreach(Sommet sommet in listeSommets)
            {
                if(sommet.NbClients > c)
                {
                    throw new ExceptionTooManyFriends();
                }
                
            }
            Console.WriteLine();
            
        }
        /// <summary>
        /// Regarde si un client a un ami qui est ami avec un des ses ennemmis 
        /// </summary>
        /// <param name="taverne"></param>
        /// <exception cref="ExceptionAmisDennemis"></exception>
        public static void amisDennemis(Taverne taverne)
        {
            foreach(Client client in taverne.Clients)
            {
                foreach (Client amis in client.Amis)
                {
                    foreach (Client amisDamis in amis.Amis)
                    {
                        if(client.Ennemis.Contains(amisDamis))
                        {
                            throw new ExceptionAmisDennemis();
                        }
                    }
                }
            }
        }

        
    }
}
