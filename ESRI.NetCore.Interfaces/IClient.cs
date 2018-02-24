﻿using System.Collections.Generic;

namespace ESRI.NetCore.Interfaces
{
    /// <summary>
    /// Client pour l'utilisation du client.
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Obtenir une liste typée après une instruction GET.
        /// </summary>
        /// <typeparam name="T">Type des objets retournés par la requête.</typeparam>
        /// <typeparam name="U">Type de retour.</typeparam>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <returns>Liste générique.</returns>
        IEnumerable<U> Obtenir<T,U>(string urlBase);

        /// <summary>
        /// Obtenir une liste typée après une instruction GET.
        /// </summary>
        /// <returns>Liste générique.</returns>
        /// <typeparam name="T">Type des objets retournés par la requête.</typeparam>
        /// <typeparam name="U">Type de retour.</typeparam>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="parametres">Paramètres de la requête.</param>
        IEnumerable<U> Obtenir<T,U>(string urlBase, IParametresRequete parametres);
    }
}