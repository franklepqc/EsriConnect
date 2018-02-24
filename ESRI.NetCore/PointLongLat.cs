﻿using ESRI.NetCore.Interfaces;

namespace ESRI.NetCore
{
    public class PointLongLat : IPointLongLat
    {
        /// <summary>
        /// Well-known ID. Projection.
        /// </summary>
        public int WKID => 4326;

        /// <summary>
        /// Longitude.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Écriture du point en sortie.
        /// </summary>
        /// <returns>Point prêt à être utilisé.</returns>
        public override string ToString()
        {
            return $"{ Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)},{Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
        }
    }
}