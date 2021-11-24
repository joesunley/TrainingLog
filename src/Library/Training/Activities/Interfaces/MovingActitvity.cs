using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Files;

namespace Library.Activities
{
    public interface IMovingActitvity
    {
        float Distance { get; set; }
        IActivityFile? ActivityFile { get; }

        void SetSpeed(float avgSpeed, float maxSpeed);
        float AverageSpeed();
        float MaxSpeed();

        void SetCadence(float avgCadence, int maxCadence);
        float AverageCadence();
        int MaxCadence();

    }
}
