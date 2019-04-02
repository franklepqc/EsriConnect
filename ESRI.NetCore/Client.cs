using ESRI.NetCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ESRI.NetCore
{
    public class Client : IClient
    {
        #region Fields

        /// <summary>
        /// Repository pour récupérer et enregistrer les données.
        /// </summary>
        private IRepoFeatureClass _repoFeatureClass;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur par injection.
        /// </summary>
        /// <param name="repoFeatureClass">Repository du feature class.</param>
        public Client(IRepoFeatureClass repoFeatureClass = null)
        {
            _repoFeatureClass = (repoFeatureClass ?? new RepoFeatureClass(new ConstructeurUrl()));
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Envoie des features (POST).
        /// </summary>
        /// <returns>Vrai si le tout est ok.</returns>
        /// <typeparam name="T">Type des objets retournés par la requête.</typeparam>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="features">Features à enregistrer.</param>
        /// <param name="nombreElementsParPage">Nombre d'éléments à envoyer par page.</param>
        public bool AjouterFeatures<T>(string urlBase, IEnumerable<T> features, int nombreElementsParPage = 100)
        {
            // Variables de travail.
            try
            {
                var nombreElements = features.Count();

                if (nombreElements > nombreElementsParPage)
                {
                    for (int numeroPage = 0; (numeroPage * nombreElementsParPage) < nombreElements; numeroPage++)
                    {
                        _Ajouter(urlBase, features.Skip(numeroPage * nombreElementsParPage).Take(nombreElementsParPage));
                    }
                }
                else
                {
                    _Ajouter(urlBase, features);
                }
            }
            catch { return false; }

            return true;
        }

        /// <summary>
        /// Envoie des features (POST).
        /// </summary>
        /// <returns>Vrai si le tout est ok.</returns>
        /// <typeparam name="T">Type des objets retournés par la requête.</typeparam>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="features">Features à enregistrer.</param>
        /// <param name="nombreElementsParPage">Nombre d'éléments à envoyer par page.</param>
        public bool ModifierFeatures<T>(string urlBase, IEnumerable<T> features, int nombreElementsParPage = 100)
        {
            // Variables de travail.
            try
            {
                var nombreElements = features.Count();

                if (nombreElements > nombreElementsParPage)
                {
                    for (int numeroPage = 0; (numeroPage * nombreElementsParPage) < nombreElements; numeroPage++)
                    {
                        _MettreAJour(urlBase, features.Skip(numeroPage * nombreElementsParPage).Take(nombreElementsParPage));
                    }
                }
                else
                {
                    _MettreAJour(urlBase, features);
                }
            }
            catch { return false; }

            return true;
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
        /// Vider le feature class.
        /// </summary>
        /// <returns>Vrai si le tout est ok.</returns>
        /// <param name="urlBase">Url d'appel de base.</param>
        public bool Vider(string urlBase) => _repoFeatureClass.Vider(urlBase);

        /// <summary>
        /// Envoyer les éléments dans l'énumeration au feature.
        /// </summary>
        /// <typeparam name="T">Type d'éléments.</typeparam>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="parametres">Paramètres de l'envoie.</param>
        /// <param name="elements">Éléments.</param>
        private void _Ajouter<T>(string urlBase, IEnumerable<T> elements) =>
            _repoFeatureClass.Ajouter(urlBase, elements);

        /// <summary>
        /// Envoyer les éléments dans l'énumeration au feature.
        /// </summary>
        /// <typeparam name="T">Type d'éléments.</typeparam>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="elements">Éléments.</param>
        private void _MettreAJour<T>(string urlBase, IEnumerable<T> elements) =>
            _repoFeatureClass.MettreAJour(urlBase, elements);

        /// <summary>
        /// Obtenir toutes les instances de la requête.
        /// </summary>
        /// <typeparam name="T">Type de retour de la requête via le service web.</typeparam>
        /// <param name="urlBase">Url d'appel de base.</param>
        /// <param name="parametres">Paramètres pour la recherche.</param>
        /// <returns>Résultat.</returns>
        private IEnumerable<T> _Obtenir<T>(string urlBase, IParametresRequete parametres)
            => _repoFeatureClass.Obtenir<T>(urlBase, parametres);

        /// <summary>
        /// Copier les éléments d'une couche à l'autre.
        /// </summary>
        /// <returns>Vrai si le tout est ok.</returns>
        /// <param name="urlBaseSource">Url de la source.</param>
        /// <param name="urlBaseDestination">Url de destination.</param>
        /// <param name="where">Filtre des éléments.</param>
        public bool Copier<T>(string urlBaseSource, string urlBaseDestination, string where)
        {
            try
            {
                // Récupérer les éléments.
                var enregistrementsACopier = Obtenir<T>(urlBaseSource, new ParametresRequete
                {
                    Where = where,
                    AfficherTousLesChamps = true,
                    RetournerGeometrie = false
                });

                // Effectuer la copie.
                ModifierFeatures(urlBaseDestination, enregistrementsACopier);

                return true;
            }
            catch { }

            return false;
        }

        #endregion Methods
    }
}