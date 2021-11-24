using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Files
{
    public interface IActivityFile
    {
        string Name { get; }
        string Sport { get; }
        DateTime StartTime { get; }
        TimeSpan TotalTime { get; }
        float TotalDistance { get; }
        List<DateTime> Time { get; }
        List<int> HeartRate { get; }
        List<int> Cadence { get; }
        List<float> Speed { get; }
        List<float> Altitude { get; }
        List<float> Distance { get; }
        List<Coordinate> Position { get; }

        float AverageHeartRate();
        int MaxHeartRate();
        float AverageCadence();
        int MaxCadence();
        float AverageSpeed();
        float MaxSpeed();
        float MinElevation();
        float MaxElevation();
        float ElevationGain();
        float ElevationLoss();
    }
}
