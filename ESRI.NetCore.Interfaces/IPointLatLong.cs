namespace ESRI.NetCore.Interfaces
{
    /// <summary>
    /// Point de la carte.
    /// </summary>
    public interface IPointLatLong : IPoint
    {
        /// <summary>
        /// Well-known ID. Projection.
        /// </summary>
        new int SRID { get; }
    }
}
