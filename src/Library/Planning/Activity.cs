namespace Library.Planning
{
    public class Activity
    {
        // Properties //
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public TimeSpan Time { get; set; }
        public string Description { get; set; }


        // Methods //
        public string Name() =>
            $"{Convert.ToInt16(Math.Floor(Time.TotalMinutes))}min {Type}";

        // Constructors //
        public Activity() {
            Type = "";
            Description = "";
        }
        public Activity(DateTime date, string type, TimeSpan length, string description) {
            Date = date;
            Type = type;
            Time = length;
            Description = description;
        }

        // Overrides //
        public override string ToString() {
            return Date.ToUniversalTime().ToString() + "," +
                Type + "," +
                Time.TotalMinutes + "," +
                Description;
        }

        
        // Statics //
        public static Activity Null { get { return new Activity() {
            Date = DateTime.MinValue,
            Type = "null",
            Time = TimeSpan.Zero,
            Description = "null"
        }; } }
    }
}
