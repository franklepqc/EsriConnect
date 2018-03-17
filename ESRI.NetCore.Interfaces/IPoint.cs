namespace ESRI.NetCore.Interfaces
{
    /// <summary>
    /// Point de la carte.
    /// </summary>
    public interface IPoint
    {
        /// <summary>
        /// Coordonnée X / Latitude.
        /// </summary>
        double X { get; set; }
        
        /// <summary>
        /// Coordonnée Y / Longitude.
        /// </summary>
        double Y { get; set; }

        /// <summary>
        /// Well-known ID. Projection.
        /// </summary>
        int WKID { get; set; }
    }
}
