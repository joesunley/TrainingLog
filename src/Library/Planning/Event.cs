using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Planning
{
    public class Event
    {
        // Properties
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EventType Type { get; set; }

        // Constructors
        public Event() {
            Date = DateTime.MinValue;
            Name = "";
            Description = "";
        }
        public Event(DateTime d, string n, string desc, EventType t) {
            Date = d;
            Name = n;
            Description = desc;
            Type = t;
        }

        // Overrides
        public override string ToString() =>
             Date.ToUniversalTime().ToString() + "," + Name + "," + Description + "," + Type.ToString();
    }

    public enum EventType
    {
        Training, 
        Practice, // BOF registered Events
        Simulation,
        Major, // JK, British, Regionals
        Selection, // Selection Races for Internationals & Nationals
        National, // Home Internationals, Interland
        International // JWOC, WOC, JEC
    }
}
