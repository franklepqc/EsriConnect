using System;
using System.Collections.Generic;

namespace ESRI.NetCore.Interfaces
{
    public interface IRepoFeatureClass
    {
        IEnumerable<T> Obtenir<T>(string urlBase, IParametresRequete parametres);

        void Ajouter<T>(string urlBase, IEnumerable<T> elements);

        void MettreAJour<T>(string urlBase, IEnumerable<T> elements);

        bool Vider(string urlBase);
    }
}
