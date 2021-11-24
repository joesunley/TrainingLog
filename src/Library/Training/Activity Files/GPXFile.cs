using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Library.Files
{
    public class GPXFile : IActivityFile
    {
        #region -- Fields --
        private string? name = null;
        private string? sport = null;
        private DateTime startTime = new DateTime();
        private TimeSpan totalTime = new TimeSpan();
        private float totalDisance = -1;

        private readonly List<TrackPoint> trackPoints = new List<TrackPoint>();

        private readonly List<DateTime> timeStream = new List<DateTime>();
        private readonly List<int> heartRateStream = new List<int>();
        private readonly List<int> cadenceStream = new List<int>();
        private readonly List<float> altitudeStream = new List<float>();
        private readonly List<float> temperatureStream = new List<float>();
        private readonly List<Coordinate> positionStream = new List<Coordinate>();

        private readonly List<float> distanceStream = new List<float>();
        private readonly List<float> speedStream = new List<float>();
        #endregion

        #region -- Properties --
        public string Name => name;
        public string Sport => sport;
        public DateTime StartTime => startTime;
        public TimeSpan TotalTime => totalTime;
        public float TotalDistance => totalDisance;
        public List<DateTime> Time => timeStream;
        public List<int> HeartRate => heartRateStream;
        public List<int> Cadence => cadenceStream;
        public List<float> Altitude => altitudeStream;
        public List<float> Temperature => temperatureStream;
        public List<Coordinate> Position => positionStream;
        public List<float> Distance => distanceStream;
        public List<float> Speed => speedStream;
        #endregion

        #region -- Methods --
        public float AverageHeartRate() { return Convert.ToInt32(Math.Round(heartRateStream.Average())); }
        public int MaxHeartRate() { return heartRateStream.Max(); }
        public float AverageCadence() { return Convert.ToInt32(Math.Round(cadenceStream.Average())); }
        public int MaxCadence() { return cadenceStream.Max(); }
        public float AverageSpeed() { return speedStream.Average(); }
        public float MaxSpeed() { return speedStream.Max(); }
        public float MinElevation() { return altitudeStream.Min(); }
        public float MaxElevation() { return altitudeStream.Max(); }
        public float ElevationGain() {
            float gain = 0;

            for (int i = 1; i < altitudeStream.Count; i++) {
                float delta = altitudeStream[i] - altitudeStream[i - 1];
                if (delta > 0) { gain += delta; }
            }

            return gain;
        }
        public float ElevationLoss() {
            float loss = 0;

            for (int i = 1; i < altitudeStream.Count; i++) {
                float delta = altitudeStream[i] - altitudeStream[i - 1];
                if (delta < 0) { loss += delta; }
            }

            return loss;
        }
        #endregion

        #region -- Constructors --
        public GPXFile(string filePath) {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode metadata = doc.ChildNodes[1].ChildNodes[0];
            XmlNode trackData = doc.ChildNodes[1].ChildNodes[1];

            startTime = Convert.ToDateTime(metadata.ChildNodes[1].InnerText);
            name = trackData.ChildNodes[0].InnerText;
            sport = trackData.ChildNodes[1].InnerText;

            XmlNodeList points = trackData.ChildNodes[2].ChildNodes;

            int index = 0;
            foreach (XmlNode point in points) {
                TrackPoint trackPoint = new TrackPoint();
                trackPoint.Index = index;
                index++;

                string
                    lat = point.Attributes[0].Value,
                    lon = point.Attributes[1].Value;
                Coordinate pos = new Coordinate(Convert.ToDouble(lat), Convert.ToDouble(lon));

                string altitude = point.ChildNodes[0].InnerText;
                string time = point.ChildNodes[1].InnerText;

                XmlNode extensions = point.ChildNodes[2].ChildNodes[0];

                string
                    temp = extensions.ChildNodes[0].InnerText,
                    heartRate = extensions.ChildNodes[1].InnerText,
                    cadence = extensions.ChildNodes[2].InnerText;

                trackPoint.Time = Convert.ToDateTime(time);
                trackPoint.Position = pos;
                trackPoint.Altitude = (float)Convert.ToDouble(altitude);

                trackPoint.HeartRate = Convert.ToInt32(heartRate);

                trackPoint.Cadence = Convert.ToInt32(cadence);
                trackPoint.Temperature = (float)Convert.ToDouble(temp);

                trackPoints.Add(trackPoint);
            }

            CreateStreams();

            float dist = 0;
            foreach (float f in distanceStream)
                dist += f;

            TimeSpan tTaken = timeStream.Last() - timeStream[0];

            totalDisance = dist;
            totalTime = tTaken;
        }
        private void CreateStreams() {
            TrackPoint first = trackPoints[0];

            timeStream.Add(first.Time);
            heartRateStream.Add(first.HeartRate);
            cadenceStream.Add(first.Cadence);
            altitudeStream.Add(first.Altitude);
            temperatureStream.Add(first.Temperature);
            positionStream.Add(first.Position);
            distanceStream.Add(0);
            speedStream.Add(0);


            for (int i = 1; i < trackPoints.Count; i++) {
                TrackPoint current = trackPoints[i];
                TrackPoint previous = trackPoints[i - 1];

                timeStream.Add(current.Time);
                heartRateStream.Add(current.HeartRate);
                cadenceStream.Add(current.Cadence);
                altitudeStream.Add(current.Altitude);
                temperatureStream.Add(current.Temperature);
                positionStream.Add(current.Position);

                double dist = Coordinate.DistanceBetween(previous.Position, current.Position);
                TimeSpan time = current.Time - previous.Time;

                double speed = (dist * 1000) / time.TotalSeconds;

                distanceStream.Add((float)dist);
                speedStream.Add((float)speed);
            }
        }
        #endregion

        private struct TrackPoint
        {
            public int Index { get; set; }
            public DateTime Time { get; set; }
            public Coordinate Position { get; set; }
            public float Altitude { get; set; }
            public int HeartRate { get; set; }
            public int Cadence { get; set; }
            public float Temperature { get; set; }
        }
    }
}
