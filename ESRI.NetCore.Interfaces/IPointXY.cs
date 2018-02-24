namespace ESRI.NetCore.Interfaces
{
    /// <summary>
    /// Point de la carte.
    /// </summary>
    public interface IPointXY : IPoint
    {
        /// <summary>
        /// Coordonnée X.
        /// </summary>
        double X { get; }

        /// <summary>
        /// Coordonnée Y.
        /// </summary>
        double Y { get; }
    }
}
