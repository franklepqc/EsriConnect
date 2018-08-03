using System;
using System.Collections.Generic;

namespace ESRI.NetCore.Interfaces
{
    public interface IRepoFeatureClass
    {
        IEnumerable<T> Obtenir<T>(string urlBase, IParametresRequete parametres);

        void Enregistrer<T>(string urlBase, IEnumerable<T> elements);
    }
}
