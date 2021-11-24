using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net;
using Ical.Net.Serialization;
using Ical.Net.Evaluation;
using System.IO;


namespace Library.Planning.Statics
{
    public static class TrainingIcal
    {
        public static Calendar Create() {
            TrainingWeek[] trainingWeeks = new TrainingWeek[5];

            Calendar calendar = new Calendar();

            foreach (TrainingWeek week in trainingWeeks) {
                foreach (TrainingDay day in week.Trainings) {
                    if (day.AM != new Activity())
                        calendar.Events.Add(GetEvent(day.AM));
                    if (day.PM != new Activity())
                        calendar.Events.Add(GetEvent(day.PM));
                }
            }

            return calendar;
        }
        public static void SaveCalendar(Calendar calendar, string filePath) {
            CalendarSerializer iCalSerializer = new();
            string result = iCalSerializer.SerializeToString(calendar);

            File.WriteAllText(filePath, result);
        }


        private static CalendarEvent GetEvent(Activity a) =>
            new CalendarEvent()
            {
                Summary = a.Name(),
                Description = a.Description,

                IsAllDay = true,
                Start = new CalDateTime(a.Date),
            };
        
    }
}
