namespace ESRI.NetCore.Interfaces
{
    public interface IConstructeurParametres
    {
        IConstructeurParametres AjouterPoint(double x, double y, int projection);
        IConstructeurParametres AjouterPointXY(double x, double y);
        IConstructeurParametres AjouterPointLatLong(double latitude, double longitude);
        IConstructeurParametres AfficherTousLesChamps(bool afficherTousLesChamps = true);
        IConstructeurParametres AjouterConditionsWhere(params string[] conditionsWhere);
        IConstructeurParametres AjouterChampsSortie(params string[] champs);
        IParametresRequete ConstruireParametresRequete();
    }
}