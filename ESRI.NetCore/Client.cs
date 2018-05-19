using ESRI.NetCore.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace ESRI.NetCore
{
    public class Client : IClient
    {
        /// <summary>
        /// Conteneurs.
        /// </summary>
        private HttpClient _clientHttp;
        private IConstructeurUrl _constructeurUrl;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="mapper">Mappeur de données.</param>
        public Client()
            : this(new ConstructeurUrl(), new HttpClient())
        {
        }

        /// <summary>
        /// Constructeur par injection.
        /// </summary>
        /// <param name="mapper">Mappeur de données.</param>
        /// <param name="constructeurUrl">Constructeur d'url.</param>
        /// <param name="clientHttp">Client HTTP.</param>
        public Client(IConstructeurUrl constructeurUrl, HttpClient clientHttp)
        {
            _clientHttp = clientHttp;
            _constructeurUrl = constructeurUrl;
        }

        /// <summary>
        /// Obtenir les valeurs des couches ESRI en la convertissant avec le type T.
        /// </summary>
        /// <typeparam name="T">Type de retour.</typeparam>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="parametres">Paramètres.</param>
        /// <returns>Liste d'éléments convertis.</returns>
        public IEnumerable<T> Obtenir<T>(string urlBase, IParametresRequete parametres) => _Obtenir<T>(urlBase, parametres);

        /// <summary>
        /// Obtenir les valeurs des couches ESRI en la convertissant avec le type T.
        /// </summary>
        /// <typeparam name="T">Type de retour.</typeparam>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <returns>Liste d'éléments convertis.</returns>
        public IEnumerable<T> Obtenir<T>(string urlBase) => _Obtenir<T>(urlBase, ParametresRequete.Defaut);

        /// <summary>
        /// Envoie des features (POST).
        /// </summary>
        /// <returns>Vrai si le tout est ok.</returns>
        /// <typeparam name="T">Type des objets retournés par la requête.</typeparam>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="features">Features à enregistrer.</param>
        public bool EnregistrerFeatures<T>(string urlBase, T features)
        {
            var contenu = new Dictionary<string, string>();

            contenu.Add("f", "json");
            contenu.Add("features", JsonConvert.SerializeObject(features));

            return _clientHttp.PostAsync(urlBase, new FormUrlEncodedContent(contenu)).Result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Obtenir toutes les instances de la requête.
        /// </summary>
        /// <typeparam name="T">Type de retour de la requête via le service web.</typeparam>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="parametres">Paramètres pour la recherche.</param>
        /// <returns>Résultat.</returns>
        private IEnumerable<T> _Obtenir<T>(string urlBase, IParametresRequete parametres)
        {
            // Construction de l'url.
            var url = ObtenirUri(urlBase, parametres);

            var listeU = new List<T>();
            var reponse = _clientHttp.GetStringAsync(url).Result;

            dynamic json = JsonConvert.DeserializeObject(reponse);

            if (null != json)
            {
                // Itérer dans les réponses.
                foreach (dynamic attributs in json.features)
                {
                    listeU.Add(JsonConvert.DeserializeObject<T>(attributs.attributes.ToString()));
                }
            }

            return listeU;
        }

        /// <summary>
        /// Obtenir l'uri de la requête.
        /// </summary>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="parametres">Paramètres pour la recherche.</param>
        /// <returns>Url complète.</returns>
        private System.Uri ObtenirUri(string urlBase, IParametresRequete parametres)
        {
            // Construction de l'url.
            _constructeurUrl.Debuter(urlBase);
            _constructeurUrl.AjouterParametres(parametres);
            return _constructeurUrl.Finaliser();
        }
    }
}
