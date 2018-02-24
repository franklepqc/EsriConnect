using ESRI.NetCore.Interfaces;

namespace ESRI.NetCore
{
    public class PointXY : IPointXY
    {
        /// <summary>
        /// Coordonnée X.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Coordonnée Y.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Well-known ID. Projection.
        /// </summary>
        public int WKID => 32189;

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
