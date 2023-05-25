using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions.Realisations.GestionTaverne
{   /// <summary>
    /// Exception levée si un client a un ami qui a pour ami un des ses ennemis 
    /// </summary>
    internal class ExceptionAmisDennemis : ExceptionGestionTaverne
    {
        /// <summary>
        /// Constructeur par défaut 
        /// </summary>
        public ExceptionAmisDennemis() : base("Un client a un ami qui est aussi ami avec des ennemis du client ")
        {

        }
    
    }
}
