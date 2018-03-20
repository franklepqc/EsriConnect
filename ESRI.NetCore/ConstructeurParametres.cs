using ESRI.NetCore.Interfaces;

namespace ESRI.NetCore
{
    public class ConstructeurParametres : IConstructeurParametres
    {
        private IParametresRequete _parametres = new ParametresRequete();

        public IConstructeurParametres AfficherTousLesChamps(bool afficherTousLesChamps = true)
        {
            _parametres.AfficherTousLesChamps = afficherTousLesChamps;
            return this;
        }

        public IConstructeurParametres AjouterChampsSortie(params string[] champs)
        {
            _parametres.ChampsSorties = champs;
            return this;
        }

        public IConstructeurParametres AjouterConditionsWhere(params string[] conditionsWhere)
        {
            _parametres.Where = string.Join(" AND ", conditionsWhere);
            return this;
        }

        public IConstructeurParametres AjouterPoint(double x, double y, int projection)
        {
            _parametres.Point = new Point(x, y, projection);
            return this;
        }

        public IConstructeurParametres AjouterPointLatLong(double latitude, double longitude)
        {
            _parametres.Point = new PointLatLong(latitude, longitude);
            return this;
        }

        public IConstructeurParametres AjouterPointXY(double x, double y)
        {
            _parametres.Point = new PointXY(x, y);
            return this;
        }

        public IParametresRequete ConstruireParametresRequete()
        {
            return _parametres;
        }
    }
}