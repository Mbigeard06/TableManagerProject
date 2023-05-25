using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Algorithmes.Realisations;

namespace TavernManagerMetier.Metier.Algorithmes
{
    /// <summary>
    /// Manager des algorithmes
    /// </summary>
    public class AlgorithmeManager
    {
        /// <summary>
        /// Renvoie la liste des algorithmes implémentés
        /// </summary>
        /// <returns>La liste des algorithmes</returns>
        public List<IAlgorithme> ListeDesAlgorithmes()
        {
            List<IAlgorithme> algorithmes = new List<IAlgorithme>();

            //---- Lister ICI tous les algorithmes implémentés ----
            algorithmes.Add(new AlgorithmeExemple());
            algorithmes.Add(new AlgorithmeChacunSaTable());
            algorithmes.Add(new AlgorithmeDeColorationCroissante());
            algorithmes.Add(new AlgorithmeDeTest());
            algorithmes.Add(new AlgorithmeWelsh_Powell());
            algorithmes.Add(new AlgorithmeLdo());
            algorithmes.Add(new AlgorithmeDSATUR());
            return algorithmes;
        }
    }
}
