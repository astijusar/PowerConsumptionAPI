namespace PowerConsumptionAPI.Models.RequestFeatures
{
    public class PowerConsumptionParameters
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
        public DateTime Cursor { get; set; } = DateTime.Now;
    }
}
