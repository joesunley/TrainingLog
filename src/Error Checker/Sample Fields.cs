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
                new(new(DateTime.Today.AddDays(6), "RestDay", TimeSpan.FromMinutes(0), "")),
            };

        public static Event[] Events =>
            new Event[]
            {
                new(DateTime.Today, "British Long Champs", "BLAH BLAh some description", EventType.Major),
                new(DateTime.Today.AddMonths(2).AddDays(5), "JK Middle", "Speedy SHit", EventType.Selection),
                new(DateTime.Today.AddMonths(-1), "British Sprint Champs", "Noice", EventType.Major),
                new(DateTime.Today.AddMonths(1).AddDays(1), "Fight with the Night", "Calton Hill", EventType.Practice),
                new(DateTime.Today.AddMonths(1), "EUOC Training", "Techy schit", EventType.Training),
            };
    }
}
