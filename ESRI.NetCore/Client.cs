using AutoMapper;
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
        private IMapper _mapper;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="mapper">Mappeur de données.</param>
        public Client(IMapper mapper)
            : this(mapper, new ConstructeurUrl(), new HttpClient())
        {
        }

        /// <summary>
        /// Constructeur par injection.
        /// </summary>
        /// <param name="mapper">Mappeur de données.</param>
        /// <param name="constructeurUrl">Constructeur d'url.</param>
        /// <param name="clientHttp">Client HTTP.</param>
        public Client(IMapper mapper, IConstructeurUrl constructeurUrl, HttpClient clientHttp)
        {
            _clientHttp = clientHttp;
            _constructeurUrl = constructeurUrl;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtenir les valeurs des couches ESRI en la convertissant avec le type T.
        /// </summary>
        /// <typeparam name="T">Type de retour.</typeparam>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="parametres">Paramètres.</param>
        /// <returns>Liste d'éléments convertis.</returns>
        public IEnumerable<U> Obtenir<T, U>(string urlBase, IParametresRequete parametres) => _Obtenir<T, U>(urlBase, parametres);

        /// <summary>
        /// Obtenir les valeurs des couches ESRI en la convertissant avec le type T.
        /// </summary>
        /// <typeparam name="T">Type de retour.</typeparam>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <returns>Liste d'éléments convertis.</returns>
        public IEnumerable<U> Obtenir<T, U>(string urlBase) => _Obtenir<T, U>(urlBase, ParametresRequete.Defaut);

        /// <summary>
        /// Obtenir toutes les instances de la requête.
        /// </summary>
        /// <typeparam name="T">Type de retour de la requête via le service web.</typeparam>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="parametres">Paramètres pour la recherche.</param>
        /// <returns>Résultat.</returns>
        private IEnumerable<U> _Obtenir<T, U>(string urlBase, IParametresRequete parametres)
        {
            // Construction de l'url.
            _constructeurUrl.Debuter(urlBase);
            _constructeurUrl.AjouterParametres(parametres);
            var url = _constructeurUrl.Finaliser();

            var listeU = new List<U>();
            var reponse = _clientHttp.GetStringAsync(url).Result;

            dynamic json = JsonConvert.DeserializeObject(reponse);

            if (null != json)
            {
                // Itérer dans les réponses.
                foreach (dynamic attributs in json.features)
                {
                    listeU.Add(_mapper.Map<T, U>(JsonConvert.DeserializeObject<T>(attributs.attributes.ToString())));
                }
            }

            return listeU;
        }
    }
}
