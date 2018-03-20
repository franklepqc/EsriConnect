using ESRI.NetCore.Interfaces;

namespace ESRI.NetCore
{
    public class PointLatLong : Point, IPointLatLong
    {
        /// <summary>
        /// Well-known ID pour la projection.
        /// </summary>
        private const int _WKID = 4326;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public PointLatLong()
        {

        }

        /// <summary>
        /// Constructeur avec longitude et latitude.
        /// </summary>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
        public PointLatLong(double latitude, double longitude)
            : base(latitude, longitude, _WKID)
        {
        }

        /// <summary>
        /// Well-known ID. Projection.
        /// </summary>
        new public int WKID 
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
            return $"{Y.ToString(System.Globalization.CultureInfo.InvariantCulture)},{X.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
        }
    }
}
