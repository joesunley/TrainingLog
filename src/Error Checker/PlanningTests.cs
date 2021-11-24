using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Planning;

namespace Error_Checker
{
    internal static class PlanningTests
    {
        #region TrainingDay
        public static bool TrainingDay_Constructor() {
            Activity am = new(DateTime.Today, "Run", TimeSpan.FromMilliseconds(30), "Easy");
            Activity pm = new(DateTime.Today, "Run", TimeSpan.FromMilliseconds(30), "Hard");

            try {
                TrainingDay t = new(am, pm);
            } catch {
                return false;
            }
            return true;
        }
        public static bool TrainingDay_Constructor_1() {
            Activity am = new(DateTime.Today, "Run", TimeSpan.FromMilliseconds(30), "Easy");
            Activity pm = new(DateTime.Today.AddDays(2), "Run", TimeSpan.FromMilliseconds(30), "Hard");

            try {
                TrainingDay t = new(am, pm);
            } catch {
                return true;
            }
            return false;
        }
        public static bool TrainingDay_Constructor_2() {
            try {
                Activity a = new(DateTime.Today, "Run", TimeSpan.FromMinutes(45), "Yeet");
                TrainingDay t = new(a);
            } catch { return false; }
            return true;
        }
        public static bool TrainingDay_ToString() {
            Activity a = new(DateTime.Today, "Run", TimeSpan.FromMinutes(45), "Yeet");
            TrainingDay t = new(a);
            string s = $"{DateTime.Today.ToUniversalTime()};{DateTime.Today.ToUniversalTime()},Run,45,Yeet";

            return t.ToString().Equals(s);
        }
        public static bool TrainingDay_ToString_1() {
            Activity a = new(DateTime.Today, "Run", TimeSpan.FromMinutes(45), "Yeet");
            TrainingDay t = new(a);
            string s = $"{DateTime.Today.AddDays(2).ToUniversalTime()};{DateTime.Today.ToUniversalTime()},Run,45,Yeet";

            return !t.ToString().Equals(s);
        }
        #endregion
        #region Activity
        public static bool Activity_Constructor() {
            Activity t = new();

            return (t.Type == "") && (t.Description == "");
        }
        public static bool Activity_Constructor_1() {
            Activity a = new(DateTime.Today, "Run", TimeSpan.FromMinutes(30), "Easy Yeet");

            return
                (a.Date == DateTime.Today) &&
                (a.Type == "Run") &&
                (a.Time.TotalMinutes == 30) &&
                (a.Description == "Easy Yeet");
        }
        public static bool Activity_ToString() {
            Activity a = new(DateTime.Today, "Run", TimeSpan.FromMinutes(30), "Easy Yeet");
            string s = $"{DateTime.Today.ToUniversalTime()},Run,30,Easy Yeet";

            return (a.ToString().Equals(s));
        }
        public static bool Activity_ToString_Fail() {
            Activity a = new(DateTime.Today, "Run", TimeSpan.FromMinutes(30), "Easy Yeet");
            string s = $"{DateTime.Today.ToUniversalTime()},Run,30,EasyYeet";

            return !(a.ToString().Equals(s));
        }
        public static bool Activity_Name() {
            Activity a = new(DateTime.Today, "Run", TimeSpan.FromMinutes(30), "Easy Yeet");
            string s = "30min Run";

            return (a.Name().Equals(s));
        }
        #endregion
        #region TrainingWeek
        public static bool TrainingWeek_Constructor() {
            TrainingWeek t = new(new DateTime(2021, 11, 20));

            return
                (t.StartDate == new DateTime(2021, 11, 20)) &&
                (t.Trainings.Length == 7);
        }
        public static bool TrainingWeek_Constructor_1() {
            TrainingWeek t = new(Sample.TrainingDays);

            return
                (t.StartDate == DateTime.Today) &&
                (t.Trainings.Length == 7) &&
                (t.Trainings[2].Date == DateTime.Today.AddDays(2));
        }
        public static bool TrainingWeek_Accessor() {
            TrainingWeek t = new(Sample.TrainingDays);

            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilTuesday = ((int)DayOfWeek.Tuesday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextTuesday = today.AddDays(daysUntilTuesday);

            return (t[DayOfWeek.Tuesday].Date == nextTuesday);
        }
        public static bool TrainingWeek_Accessor_1() {
            TrainingWeek t = new(Sample.TrainingDays);

            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilTuesday = ((int)DayOfWeek.Tuesday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextTuesday = today.AddDays(daysUntilTuesday);

            return (t["TuesDay"].Date == nextTuesday);
        }
        #endregion
        #region Event
        public static bool Event_ToString() {
            Event e = Sample.Events[2];
            string s = $"{DateTime.Today.AddMonths(7).ToUniversalTime().ToString()},British Sprint Champs,Noice,Major";

            return e.ToString().Equals(s);
        }
        #endregion
        #region TrainingLog
        public static bool TrainingLog_AddEvent_Single() {
            TrainingLog t = new();
            Event e = Sample.Events[2];
            try {
                t.AddEvent(e);
            } catch { return false; }
            
            return 
                (t.Events.Length == 1) &&
                (t.Events[0] == e);
        }
        public static bool TrainingLog_AddEvent_Single_1() {
            TrainingLog t = new();
            Event e1 = Sample.Events[3];
            Event e2 = Sample.Events[4];
            e2.Date = e1.Date;

            t.AddEvent(e1);
            try { t.AddEvent(e2); } catch { return true; }
            return false;
        }
        public static bool TrainingLog_AddEvent_Multiple() {
            TrainingLog t = new();
            t.AddEvent(Sample.Events);

            return t.Events.Length == 5;
        }
        public static bool TrainingLog_AddEvent_Multiple_1() {
            TrainingLog t = new();
            Event[] events = Sample.Events;
            events[0].Date = Sample.Events[1].Date;

            t.AddEvent(events);

            return t.Events.Length != 5;
        }
        public static bool TrainingLog_RemoveEvent_Event() {
            TrainingLog t = new();
            t.AddEvent(Sample.Events);

            try {
                t.RemoveEvent(Sample.Events[2]);
            } catch { return false; }
            return t.Events.Length == 4;
        }
        public static bool TrainingLog_RemoveEvent_Event_1() {
            TrainingLog t = new();
            t.AddEvent(Sample.Events);

            try {
                t.RemoveEvent(new Event(DateTime.Today.AddDays(-1), "Your Mum", "Haha no", EventType.International));
            } catch { return true; }
            return false;
        }
        public static bool TrainingLog_RemoveEvent_Date() {
            TrainingLog t = new();
            t.AddEvent(Sample.Events);

            try {
                t.RemoveEvent(DateTime.Today.AddMonths(1));
            } catch { return false; }
            return t.Events.Length == 4;
        }
        public static bool TrainingLog_RemoveEvent_Date_1() {
            TrainingLog t = new();
            t.AddEvent(Sample.Events);

            try {
                t.RemoveEvent(DateTime.Today.AddMonths(2));
            } catch { return true; }
            return false;
        }
        public static bool TrainingLog_AddTrainingWeek() {
            return false;
        }
        #endregion
    }
}
