using Library.Activities.Enums;
using Library.Maps;
using Sunley.Orienteering.Results;

namespace Library.Activities
{
    public interface IMapActivities
    {
        ResultsFile Results { get; set; }
        IMapFile? Map { get; set; }
        Difficulty Difficulty { get; set; }

        void SetMap(string url);
        void SetResults(string url);
    }
}
