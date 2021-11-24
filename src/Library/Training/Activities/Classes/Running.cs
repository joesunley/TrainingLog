using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Activities.Enums;
using Library.Files;
using System.Diagnostics;

namespace Library.Activities
{
    public class Running : IActivity, IMovingActitvity, IHeartRateActivity
    {
        #region -- Fields --
        private Cadence cadence;
        private Speed speed;
        #endregion

        #region -- Properties --
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Time { get; set; }
        public float Distance { get; set; }

        public IActivityFile? ActivityFile { get; }

        public int[]? HeartRateStream { get; set; }

        public ActivityFeel? ActivityFeel { get; set; }
        public PerceivedEffort? PerceivedEffort { get; set; }
        #endregion

        #region -- Methods --
        public float AverageCadence() => (cadence.AverageCadence != null) ? (float)cadence.AverageCadence : -1;
        public float AverageSpeed() => (speed.AverageSpeed != null) ? (float)speed.AverageSpeed : -1;
        public int MaxCadence() => (cadence.MaxCadence != null) ? (int)cadence.MaxCadence : -1;
        public float MaxSpeed() => (speed.MaxSpeed != null) ? (float)speed.MaxSpeed : -1;
        public float AverageHeartRate() => (HeartRateStream != null) ? (float)HeartRateStream.Average() : -1;
        public int MaxHeartRate() => (HeartRateStream != null) ? HeartRateStream.Max() : -1;

        public void SetCadence(float avgCadence, int maxCadence) {
            cadence = new Cadence(avgCadence, maxCadence);
        }
        public void SetSpeed(float avgSpeed, float maxSpeed) {
            speed = new Speed(avgSpeed, maxSpeed);
        }
        public void SetHeartRate(int[] heartRate) {
            HeartRateStream = heartRate;
        }

        public override string ToString() {
            string format = "1";
            List<string> output = new List<string>();
            output.Add(Name);

            if (Description != null) {
                output.Add(Description);
                format += "1";
            } else { format += "0"; }

            output.AddRange(new string[] {
                StartTime.ToUniversalTime().ToString(),
                Time.TotalMilliseconds.ToString(),
                Distance.ToString(),
            });
            format += "111";
            
            if (ActivityFeel != null) {
                output.Add(((int)ActivityFeel.Value).ToString());
                format += "1";
            } else { format += "0"; }

            if (PerceivedEffort != null) {
                output.Add(((int)PerceivedEffort.Value).ToString());
                format += "1";
            } else { format += "0"; }

            if (ActivityFile != null) {
                output.Add(ActivityFile.ToString());
                format += "1000";
            }
            else {
                if (HeartRateStream != null) {

                    format += "01";
                } else { format += "00"; }
                if (!cadence.IsNull) {
                    output.Add(cadence.ToString());
                    format += "1";
                } else { format += "0"; }
                if (speed.IsNull) {
                    output.Add(speed.ToString());
                    format += "1";
                } else { format += "0"; }
                
            }

            string o = format + ";";
            foreach (string s in output) {
                o += s + ";";
            }
            o.Substring(0, o.Length - 1);
            return o;
            
        }
#endregion

        #region -- Constructors --
        public Running(IActivityFile activityFile) {
            ActivityFile = activityFile;

            Name = activityFile.Name;
            Description = "";
            StartTime = activityFile.StartTime;
            Time = activityFile.TotalTime;
            Distance = activityFile.TotalDistance;
            HeartRateStream = activityFile.HeartRate.ToArray();
            cadence = new Cadence(activityFile.AverageCadence(), activityFile.MaxCadence());
            speed = new Speed(activityFile.AverageSpeed(), activityFile.MaxSpeed());
        }
        public Running(string name, string desc, DateTime sTime, TimeSpan time, float dist, int[] hrStream, float avgCadence, int maxCadence, float avgSpeed, float maxSpeed) {
            Name = name;
            Description = desc;
            StartTime = sTime;
            Time = time;
            Distance = dist;
            HeartRateStream = hrStream;
            cadence = new Cadence(avgCadence, maxCadence);
            speed = new Speed(avgSpeed, maxSpeed);
        }
        #endregion

        #region -- Structs --
        private struct Cadence
        {
            public float? AverageCadence { get; set; }
            public int? MaxCadence { get; set; }

            public Cadence(float avgCadence, int maxCadence) {
                AverageCadence = avgCadence;
                MaxCadence = maxCadence;
            }

            public bool IsNull => !(AverageCadence == null || MaxCadence == 0);
            public override string ToString() => $"{{{AverageCadence},{MaxCadence}}}";
        }
        private struct Speed
        {
            public float? AverageSpeed { get; set; }
            public float? MaxSpeed { get; set; }

            public Speed(float avgSpeed, float maxSpeed) {
                AverageSpeed = avgSpeed;
                MaxSpeed = maxSpeed;
            }

            public bool IsNull => !(AverageSpeed == null || MaxSpeed == null);
            public override string ToString() => $"{{{AverageSpeed},{MaxSpeed}}}";
        }    
        #endregion
    }
}
