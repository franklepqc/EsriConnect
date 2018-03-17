using ESRI.NetCore.Interfaces;

namespace ESRI.NetCore
{
    public class PointLongLat : Point, IPointLongLat
    {
        /// <summary>
        /// Well-known ID pour la projection.
        /// </summary>
        private const int _WKID = 4326;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public PointLongLat()
        {

        }

        /// <summary>
        /// Constructeur avec longitude et latitude.
        /// </summary>
        /// <param name="longitude">Longitude.</param>
        /// <param name="latitude">Latitude.</param>
        public PointLongLat(double longitude, double latitude)
            : base(latitude, longitude, _WKID)
        {
            X = longitude;
            Y = latitude;
        }

        /// <summary>
        /// Well-known ID. Projection.
        /// </summary>
        public int WKID 
        {
            get => _WKID;
            set { }
        }

        /// <summary>
        /// Écriture du point en sortie.
        /// </summary>
        /// <returns>Point prêt à être utilisé.</returns>
        public override string ToString()
        {
            return $"{ X.ToString(System.Globalization.CultureInfo.InvariantCulture)},{Y.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
        }
    }
}
