using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Activities.Enums;

namespace Library.Activities
{
    public interface IActivity
    {
        string Name { get; set; }
        string? Description { get; set; }
        DateTime StartTime { get; set; }
        TimeSpan Time { get; set; }
        ActivityFeel? ActivityFeel { get; set; }
        PerceivedEffort? PerceivedEffort { get; set; }

    }
}
