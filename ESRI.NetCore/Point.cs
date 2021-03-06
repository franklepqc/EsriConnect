﻿using ESRI.NetCore.Interfaces;

namespace ESRI.NetCore
{
    public class Point : IPoint
    {
        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public Point()
        {

        }

        /// <summary>
        /// Constructeur avec points en entrée.
        /// </summary>
        /// <param name="x">Coordonnée X.</param>
        /// <param name="y">Coordonnée Y.</param>
        /// <param name="srid">Identifiant de la référence spatiale.</param>
        public Point(double x, double y, int srid)
        {
            X = x;
            Y = y;
            SRID = srid;
        }

        /// <summary>
        /// Coordonnée X.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Coordonnée Y.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Projection.
        /// </summary>
        public int SRID { get; set; }

        /// <summary>
        /// Écriture du point en sortie.
        /// </summary>
        /// <returns>Point prêt à être utilisé.</returns>
        public override string ToString()
        {
            return $"{X.ToString(System.Globalization.CultureInfo.InvariantCulture)},{Y.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
        }
    }
}
