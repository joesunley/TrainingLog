using Library.Planning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Error_Checker
{
    internal static class Sample
    {
        public static TrainingDay[] TrainingDays =>
            new TrainingDay[]
            {
                new(new(DateTime.Today, "Run", TimeSpan.FromMinutes(50), "Easy")),
                new(new(DateTime.Today.AddDays(1), "Run", TimeSpan.FromMinutes(120), "Long")),
                new(new(DateTime.Today.AddDays(2), "Run", TimeSpan.FromMinutes(30), "Recovery"), new(DateTime.Today.AddDays(2), "Cycling", TimeSpan.FromMinutes(30), "Easy Spin")),
                new(new(DateTime.Today.AddDays(3), "Run", TimeSpan.FromMinutes(60), "Intervals")),
                new(new(DateTime.Today.AddDays(4), "Run", TimeSpan.FromMinutes(50), "Easy")),
                new(new(DateTime.Today.AddDays(5), "Orienteering", TimeSpan.FromMinutes(120), "Fight with the Night")),
                new(new(DateTime.Today.AddDays(6), "Rest Day", TimeSpan.FromMinutes(0), "")),
            };

        public static Event[] Events =>
            new Event[]
            {
                new(DateTime.Today, "British Long Champs", "BLAH BLAh some description", EventType.Major),
                new(DateTime.Today.AddMonths(2).AddDays(5), "JK Middle", "Speedy SHit", EventType.Selection),
                new(DateTime.Today.AddMonths(7), "British Sprint Champs", "Noice", EventType.Major),
                new(DateTime.Today.AddMonths(1).AddDays(1), "Fight with the Night", "Calton Hill", EventType.Practice),
                new(DateTime.Today.AddMonths(1), "EUOC Training", "Techy schit", EventType.Training),
            };


        public static TrainingWeek[] RandomTrainingWeeks {
            get {
                Random r = new Random();
                List<TrainingWeek> weeks = new();

                string[] types = { "Run", "Orienteering", "Cycling", "S & C", "Rest Day" };
                string[] descriptions = { "Easy", "Long", "Recovery", "Intervals", "Fight with the Night", "Hard", "Tempo" };

                for (int i = 0; i < r.Next(5, 10); i++) {
                    DateTime baseT = DateTime.Today.AddDays(r.Next(0, 1000));
                    TrainingDay[] week = new TrainingDay[7];

                    for (int j = 0; j < 7; j++) {
                        DateTime t = baseT.AddDays(j);
                        string type = types[r.Next(0, types.Length)];
                        string desc = descriptions[r.Next(0, descriptions.Length)];
                        TimeSpan time = TimeSpan.FromMinutes(r.Next(6, 24) * 5);

                        TrainingDay day = new(new(t, type, time, desc));
                        week[j] = day;
                    }
                    weeks.Add(new(week));
                }

                return weeks.ToArray();
            }
        }
    }
}

