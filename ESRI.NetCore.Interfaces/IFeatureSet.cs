namespace ESRI.NetCore.Interfaces
{
    public interface IFeatureSet<T>
    {
        IPoint geometry { get; set; }

        T attributes { get; set; }
    }
}
