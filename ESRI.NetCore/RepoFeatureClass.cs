using ESRI.NetCore.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ESRI.NetCore
{
    public class RepoFeatureClass : Interfaces.IRepoFeatureClass
    {
        #region Fields

        /// <summary>
        /// Conteneur de requête.
        /// </summary>
        private HttpClient _clientHttp = new HttpClient();

        /// <summary>
        /// Constructeur d'URL.
        /// </summary>
        private IConstructeurUrl _constructeurUrl;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur par injection.
        /// </summary>
        /// <param name="constructeurUrl">Constructeur d'URL.</param>
        public RepoFeatureClass(IConstructeurUrl constructeurUrl)
        {
            _constructeurUrl = constructeurUrl;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Enregistrer les types T vers le Feature Class.
        /// </summary>
        /// <typeparam name="T">Type à enregistrer.</typeparam>
        /// <param name="urlBase">Url de base.</param>
        /// <param name="elements">Éléments de sauvegarde.</param>
        public void Enregistrer<T>(string urlBase, IEnumerable<T> elements)
        {
            // Variables de travail.
            var parametres = new Dictionary<string, string>();

            // Paramètres / éléments à envoyer.
            parametres.Add("f", "json");
            parametres.Add("features", JsonConvert.SerializeObject(elements));

            // Envoie.
            _clientHttp.PostAsync(ConstruireUriEnregistrer(urlBase), new FormUrlEncodedContent(parametres)).Wait();
        }

        /// <summary>
        /// Obtenir la liste d'éléments via le type d'élément.
        /// </summary>
        /// <typeparam name="T">Type de retour.</typeparam>
        /// <param name="urlBase">Url de base.</param>
        /// <param name="parametres">Paramètres d'envoie.</param>
        /// <returns>Liste d'éléments.</returns>
        public IEnumerable<T> Obtenir<T>(string urlBase, IParametresRequete parametres)
        {
            // Variables de travail.
            var listeU = new List<T>();

            try
            {
                var reponse = _clientHttp.GetStringAsync(ConstruireUriObtenir(urlBase, parametres)).Result;

                dynamic json = JsonConvert.DeserializeObject(reponse);

                if (null != json)
                {
                    // Itérer dans les réponses.
                    foreach (dynamic attributs in json.features)
                    {
                        listeU.Add(JsonConvert.DeserializeObject<T>(attributs.attributes.ToString()));
                    }
                }
            }
            catch { }

            return listeU;
        }

        /// <summary>
        /// Vider le feature class.
        /// </summary>
        /// <returns>Vrai si le tout est ok.</returns>
        /// <param name="urlBase">Url de base.</param>
        public bool Vider(string urlBase)
        {
            try
            {
                var resultat = _clientHttp.GetAsync(urlBase + @"/query?where=1+%3D+1&returnGeometry=false&returnIdsOnly=true&f=pjson").Result;

                var dictionaireParametres = new Dictionary<string, string>();

                dynamic objectJson = JsonConvert.DeserializeObject(resultat.Content.ReadAsStringAsync().Result);

                var liste = string.Join(",", JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(objectJson.objectIds)));

                dictionaireParametres.Add("objectIds", liste);

                _clientHttp.PostAsync(urlBase + $@"/deleteFeatures", new FormUrlEncodedContent(dictionaireParametres)).Wait();

                return true;
            }
            catch { }

            return false;
        }

        /// <summary>
        /// Obtenir l'uri de la requête.
        /// </summary>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="parametres">Paramètres pour la recherche.</param>
        /// <returns>Url complète.</returns>
        private Uri ConstruireUriEnregistrer(string urlBase) =>
            new Uri(System.IO.Path.Combine(urlBase + @"/addFeatures"));

        /// <summary>
        /// Obtenir l'uri de la requête.
        /// </summary>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="parametres">Paramètres pour la recherche.</param>
        /// <returns>Url complète.</returns>
        private Uri ConstruireUriObtenir(string urlBase, IParametresRequete parametres)
        {
            // Construction de l'url.
            _constructeurUrl.Debuter(urlBase);
            _constructeurUrl.AjouterParametres(parametres);
            return _constructeurUrl.Finaliser();
        }

        #endregion Methods
    }
}