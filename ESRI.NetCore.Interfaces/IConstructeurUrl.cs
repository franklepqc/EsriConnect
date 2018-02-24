using System;
using System.Collections.Generic;
using System.Text;

namespace ESRI.NetCore.Interfaces
{
    public interface IConstructeurUrl
    {
        /// <summary>
        /// Finaliser la construction.
        /// </summary>
        /// <returns>L'uri finale.</returns>
        Uri Finaliser();
        
        /// <summary>
        /// Débute la construction de l'uri avec le début de chaîne.
        /// </summary>
        /// <param name="urlBase">Url comprenant le serveur.</param>
        void Debuter(string urlBase);

        /// <summary>
        /// Ajouter les paramètres dans la chaîne (query string).
        /// </summary>
        /// <param name="parametres">Paramètres.</param>
        void AjouterParametres(IParametresRequete parametres);
    }
}
