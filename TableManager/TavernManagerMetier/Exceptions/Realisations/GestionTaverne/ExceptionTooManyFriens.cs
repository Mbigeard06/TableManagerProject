﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions.Realisations.GestionTaverne
{
    internal class ExceptionTooManyFriends : ExceptionGestionTaverne
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public ExceptionTooManyFriends() : base("Un groupe d'amis est trop nombreux pour qu'il puisse tous être mit à la même table")
        {

        }
    
    }
}
