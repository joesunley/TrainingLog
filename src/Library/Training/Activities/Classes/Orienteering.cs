using Library.Activities.Enums;
using Library.Files;
using Library.Maps;
using Sunley.Orienteering.Results;

namespace Library.Activities
{
    public class Orienteering : Running, IActivity, IMovingActitvity, IHeartRateActivity, IMapActivities
    {
        #region -- Properties --
        public IMapFile? Map { get; set; }
        public ResultsFile Results { get; set; }
        public Difficulty Difficulty { get; set; }
        #endregion

        #region -- Methods --
        public void SetMap(string url) {
            
        }
        public void SetResults(string url) {
            if (ResultsFile.GetType(url) == "Winsplits") {
                Results = Winsplits.GetResults(url);
            }
        }

        public override string ToString() {            
            return base.ToString() + ";" + Map.ToString() + ";" + Results.ToString();
        }
        #endregion

        #region -- Constructors --
        public Orienteering(IActivityFile activityFile, IMapFile? mapFile = null, ResultsFile? resultsFile = null) : base(activityFile) {
            Map = mapFile;

            if (resultsFile != null) {
                Results = resultsFile;
            } else {
                Results = ResultsFile.Null;
            }
        }
        public Orienteering(string name, string desc, DateTime sTime, TimeSpan time, float dist, int[] hrStream, float avgCadence, int maxCadence, float avgSpeed, float maxSpeed, IMapFile? mapFile = null, ResultsFile? resultsFile = null) : base(name, desc, sTime, time, dist, hrStream, avgCadence, maxCadence, avgSpeed, maxSpeed){
            Map = mapFile;

            if (resultsFile != null) {
                Results = resultsFile;
            } else {
                Results = ResultsFile.Null;
            }
        }
        #endregion
    }
}
