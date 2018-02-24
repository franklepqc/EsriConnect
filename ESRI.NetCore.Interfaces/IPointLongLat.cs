namespace ESRI.NetCore.Interfaces
{
    /// <summary>
    /// Point de la carte.
    /// </summary>
    public interface IPointLongLat : IPoint
    {
        /// <summary>
        /// Longitude.
        /// </summary>
        double Longitude { get; }

        /// <summary>
        /// Latitude.
        /// </summary>
        double Latitude { get; }
    }
}
