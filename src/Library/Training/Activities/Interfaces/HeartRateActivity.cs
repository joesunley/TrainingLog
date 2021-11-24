using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Activities
{
    public interface IHeartRateActivity
    {
        int[]? HeartRateStream { get; set; }

        void SetHeartRate(int[] HeartRate);
        float AverageHeartRate();
        int MaxHeartRate();
    }
}
