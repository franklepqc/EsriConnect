using ESRI.NetCore.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ESRI.NetCore
{
    public class RepoFeatureClass : IRepoFeatureClass
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
        public void Ajouter<T>(string urlBase, IEnumerable<T> elements)
        {
            // Variables de travail.
            var parametres = new Dictionary<string, string>();

            // Paramètres / éléments à envoyer.
            parametres.Add("f", "json");
            parametres.Add("features", CreerFeatures(elements));

            // Envoie.
            _clientHttp.PostAsync(ConstruireUriAjouter(urlBase), new FormUrlEncodedContent(parametres)).Wait();
        }

        /// <summary>
        /// Mettre à jour les enregistrements.
        /// </summary>
        /// <typeparam name="T">Type de base.</typeparam>
        /// <param name="urlBase">Url d'enregistrement.</param>
        /// <param name="elements">Éléments.</param>
        public void MettreAJour<T>(string urlBase, IEnumerable<T> elements)
        {
            // Variables de travail.
            var parametres = new Dictionary<string, string>();

            // Paramètres / éléments à envoyer.
            parametres.Add("f", "json");
            parametres.Add("features", CreerFeatures(elements));

            // Envoie.
            _clientHttp.PostAsync(ConstruireUriMettreAJour(urlBase), new FormUrlEncodedContent(parametres)).Wait();
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
                var dictionaireParametres = new Dictionary<string, string>();

                dictionaireParametres.Add(@"where", @"OBJECTID > -1");

                _clientHttp.PostAsync(urlBase + @"/deleteFeatures", new FormUrlEncodedContent(dictionaireParametres)).Wait();

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
        private Uri ConstruireUriAjouter(string urlBase) =>
            new Uri(System.IO.Path.Combine(urlBase + @"/addFeatures"));

        /// <summary>
        /// Obtenir l'uri de la requête.
        /// </summary>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="parametres">Paramètres pour la recherche.</param>
        /// <returns>Url complète.</returns>
        private Uri ConstruireUriMettreAJour(string urlBase) =>
            new Uri(System.IO.Path.Combine(urlBase + @"/updateFeatures"));

        /// <summary>
        /// Créer les éléments sous la rubrique "features".
        /// </summary>
        /// <typeparam name="T">Type d'éléments.</typeparam>
        /// <param name="elements">Éléments à convertir.</param>
        /// <returns>Objet sérialisé en Json.</returns>
        private string CreerFeatures<T>(IEnumerable<T> elements) =>
            JsonConvert.SerializeObject(
                elements
                .Select(element => new
                {
                    attributes = element
                })
                .ToList());

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