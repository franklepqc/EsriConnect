using ESRI.NetCore.Interfaces;

namespace ESRI.NetCore
{
    public class FeatureSet<T> : IFeatureSet<T>
    {
        public IPoint geometry { get; set; }

        public T attributes { get; set; }
    }
}
