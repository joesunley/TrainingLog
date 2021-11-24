using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Planning
{
    public class TrainingLog
    {
        #region Fields
        private Dictionary<DateTime, TrainingWeek> _trainingWeeks;
        private Dictionary<DateTime, Event> _events;
        #endregion

        #region Properties 
        public TrainingWeek[] TrainingWeeks => _trainingWeeks.Values.ToArray();
        public Event[] Events => _events.Values.ToArray();
        #endregion

        #region Methods 
        public void AddEvent(Event e) {
            if (!_events.ContainsKey(e.Date))
                _events.Add(e.Date, e);
            else throw new ArgumentException("Only 1 event can take place per day");
        }
        public void AddEvent(Event[] e) {
            foreach (Event ev in e)
                if (!_events.ContainsKey(ev.Date))
                    _events.Add(ev.Date, ev);
        }
        public void RemoveEvent(Event e) {
            bool t = _events.Remove(e.Date);
            if (!t) throw new ArgumentException("Event must be contained within Log");
        }
        public void RemoveEvent(DateTime d) {
            bool t = _events.Remove(d);
            if (!t) throw new ArgumentException("Date must match that of an event within Log");
        }

        public void AddTrainingWeek(TrainingWeek t) {
            if (!_trainingWeeks.ContainsKey(t.StartDate))
                _trainingWeeks.Add(t.StartDate, t);
        }
        public void AddTrainingWeek(TrainingWeek[] t) {
            foreach (TrainingWeek tw in t)
                if (!_trainingWeeks.ContainsKey(tw.StartDate))
                    _trainingWeeks.Add(tw.StartDate, tw);
        }
        public void RemoveTrainingWeek(TrainingWeek t) { _events.Remove(t.StartDate); }
        #endregion

        #region Constructors
        public TrainingLog() {
            _trainingWeeks = new();
            _events = new();
        }

        #endregion

    }
}
