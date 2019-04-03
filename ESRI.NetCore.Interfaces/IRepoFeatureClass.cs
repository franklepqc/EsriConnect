using System.Collections.Generic;

namespace ESRI.NetCore.Interfaces
{
    public interface IRepoFeatureClass
    {
        IEnumerable<IFeatureSet<T>> Obtenir<T>(string urlBase, IParametresRequete parametres);

        void Ajouter<T>(string urlBase, IEnumerable<IFeatureSet<T>> elements);

        void MettreAJour<T>(string urlBase, IEnumerable<IFeatureSet<T>> elements);

        bool Vider(string urlBase);
    }
}
