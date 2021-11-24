namespace Library.Planning
{
    public class TrainingWeek
    {
        #region -- Fields + Properties --
        private DateTime _startDate;
        private Dictionary<DateTime, TrainingDay> _trainings;

        public DateTime StartDate => _startDate;
        public TrainingDay[] Trainings => _trainings.Values.ToArray();
        #endregion

        #region -- Constructors --
        public TrainingWeek(DateTime sDate) {
            _startDate = sDate;
            _trainings = new();

            for (int i = 0; i < 7; i++) 
                _trainings.Add(_startDate.AddDays(i), new());

        }
        public TrainingWeek(TrainingDay[] trainings) {
            _startDate = trainings[0].Date;
            foreach (TrainingDay t in trainings)
                if (t.Date < _startDate)
                    _startDate = t.Date;

            _trainings = new();

            for (int i = 0; i < 7; i++)
                _trainings.Add(_startDate.AddDays(i), new());

            foreach (TrainingDay day in trainings)
                _trainings[day.Date] = day;
        }
        #endregion

        #region -- Accessors --
        public TrainingDay this[DayOfWeek day] {
            get {
                foreach (TrainingDay date in _trainings.Values) {
                    if (date.Date.DayOfWeek == day) {
                        return date;
                    }
                }
                throw new ArgumentException("Day not found");
            }
            set {
                DateTime date = DateTime.MinValue;
                foreach (DateTime d in _trainings.Keys) {
                    if (d.DayOfWeek == day)
                        date = d;
                }

                if (date != DateTime.MinValue)
                    _trainings[date] = value;
                else throw new ArgumentException("Day not found");
                
            }
        }
        public TrainingDay this[string day] {
            get {
                DayOfWeek dow;

                switch (day.ToLower()) {
                    case "monday":
                        dow = DayOfWeek.Monday; break;
                    case "tuesday":
                        dow = DayOfWeek.Tuesday; break;
                    case "wednesday":
                        dow = DayOfWeek.Wednesday; break;
                    case "thursday":
                        dow = DayOfWeek.Thursday; break;
                    case "friday":
                        dow = DayOfWeek.Friday; break;
                    case "saturday":
                        dow = DayOfWeek.Saturday; break;
                    case "sunday":
                        dow = DayOfWeek.Sunday; break;
                    default: throw new ArgumentOutOfRangeException("day", "Must be a valid day of the week");

                }

                return this[dow];
            }

            set {
                DayOfWeek dow;

                switch (day.ToLower()) {
                    case "monday":
                        dow = DayOfWeek.Monday; break;
                    case "tuesday":
                        dow = DayOfWeek.Tuesday; break;
                    case "wednesday":
                        dow = DayOfWeek.Wednesday; break;
                    case "thursday":
                        dow = DayOfWeek.Thursday; break;
                    case "friday":
                        dow = DayOfWeek.Friday; break;
                    case "saturday":
                        dow = DayOfWeek.Saturday; break;
                    case "sunday":
                        dow = DayOfWeek.Sunday; break;
                    default: throw new ArgumentOutOfRangeException("day", "Must be a valid day of the week");

                }

                this[dow] = value;
            }
        }
        #endregion

        #region -- Methods --

        
        #endregion
    }
}
