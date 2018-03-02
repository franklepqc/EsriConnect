using ESRI.NetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ESRI.NetCore
{
    public class ConstructeurUrl : IConstructeurUrl
    {
        /// <summary>
        /// Contenants.
        /// </summary>
        private IDictionary<string, string> _parametres = new Dictionary<string, string>();
        private string _urlBase;
        
        /// <summary>
        /// Ajouter les paramètres dans la chaîne (query string).
        /// </summary>
        /// <param name="parametres">Paramètres.</param>
        public void AjouterParametres(IParametresRequete parametres)
        {
            // On s'assure que la liste de paramètres est vide.
            _parametres.Clear();

            var siVide = new Func<object, bool>((item) => item == null);

            if (!siVide(parametres.Where))
            {
                _parametres.Add(@"where", WebUtility.UrlEncode(parametres.Where));
            }

            if (!siVide(parametres.Point))
            {
                _parametres.Add(@"geometry", WebUtility.UrlEncode(parametres.Point.ToString()));
                _parametres.Add(@"geometryType", "esriGeometryPoint");      // Point.
                _parametres.Add(@"inSR", WebUtility.UrlEncode(parametres.Point.WKID.ToString()));
            }

            _parametres.Add(@"returnGeometry", parametres.RetournerGeometrie.ToString().ToLower());

            if (parametres.AfficherTousLesChamps)
            {
                _parametres.Add(@"outFields", @"*");
            }
            else if (0 < parametres.ChampsSorties?.Count())
            {
                _parametres.Add(@"outFields", string.Join(",", parametres.ChampsSorties));
            }

            // À la toute fin, on s'assure que c'est en json!
            _parametres.Add(@"f", "json");
        }

        /// <summary>
        /// Débute la construction de l'uri avec le début de chaîne.
        /// </summary>
        /// <param name="urlBase">Url comprenant le serveur.</param>
        public void Debuter(string urlBase)
        {
            _urlBase = urlBase;
        }

        /// <summary>
        /// Finaliser la construction.
        /// </summary>
        /// <returns>L'uri finale.</returns>
        public Uri Finaliser() => new Uri(_urlBase + (_parametres.Any() ? "/query?" + string.Join("&", _parametres.Select(p => p.Key + "=" + p.Value)) : string.Empty));
    }
}
