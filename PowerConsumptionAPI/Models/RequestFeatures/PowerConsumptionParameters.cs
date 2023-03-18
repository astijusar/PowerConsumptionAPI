using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

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

        public int PrevCount { get; set; } = 0;
        public string OrderBy { get; set; }
        public string GroupBy { get; set; }

        public uint MinInactivity { get; set; }
        public uint MaxInactivity { get; set; } = int.MaxValue;
        public uint MinCpuDraw { get; set; }
        public uint MaxCpuDraw { get; set; } = int.MaxValue;
        public uint MinGpuDraw { get; set; }
        public uint MaxGpuDraw { get; set; } = int.MaxValue;
        public uint MinTotalDraw { get; set; }
        public uint MaxTotalDraw { get; set; } = int.MaxValue;
        public DateTime MinTime { get; set; }
        public DateTime MaxTime { get; set; } = DateTime.MaxValue;

        // [BindNever] is used so that swagger wouldn't include it

        [BindNever]
        public bool ValidInactivityRange => MaxInactivity > MinInactivity;
        [BindNever]
        public bool ValidCpuRange => MaxCpuDraw > MinCpuDraw;
        [BindNever]
        public bool ValidGpuRange => MaxGpuDraw > MinGpuDraw;
        [BindNever]
        public bool ValidTotalRange => MaxTotalDraw > MinTotalDraw;
        [BindNever]
        public bool ValidTimeRange => MaxTime > MinTime;
    }
}
