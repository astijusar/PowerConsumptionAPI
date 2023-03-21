namespace PowerConsumptionAPI.Models.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxCount = 50;
        private int _count = 10;

        public int Count
        {
            get { return _count; }
            set
            {
                _count = (value > maxCount) ? maxCount : value;
            }
        }

        public int PrevCount { get; set; } = 0;
    }
}
