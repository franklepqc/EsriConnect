﻿using ESRI.NetCore.Interfaces;

namespace ESRI.NetCore
{
    public class PointXY : Point, IPointXY
    {
        /// <summary>
        /// Well-known ID pour la projection.
        /// </summary>
        private const int _WKID = 32189;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public PointXY()
        {
        }

        /// <summary>
        /// Constructeur avec points en entrée.
        /// </summary>
        /// <param name="x">Coordonnée X.</param>
        /// <param name="y">Coordonnée Y.</param>
        public PointXY(double x, double y)
            : base(x, y, _WKID)
        {
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
