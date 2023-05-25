using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions.Realisations.GestionTaverne
{
    internal class ExceptionAmisDennemis : ExceptionGestionTaverne
    {
        /// <summary>
        /// Un client a un ami qui est aussi ami avec des ennemis du client
        /// </summary>
        public ExceptionAmisDennemis() : base("Un client a un ami qui est aussi ami avec des ennemis du client ")
        {

        }
    
    }
}
