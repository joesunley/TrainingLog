namespace Library.Planning
{
    public struct TrainingDay
    {
        // Fields + Properties
        private Activity _am = new(), _pm = new();

        public DateTime Date { get; }

        public Activity AM {
            get => _pm;

            set {
                if (_pm != new Activity()) {
                    if (value.Date == _pm.Date) {
                        _am = value;
                    } else {
                        throw new ArgumentException("Date must be consistent across Activities");
                    }
                } else {
                    _am = value;
                }
            }
        }
        public Activity PM {
            get => _pm;

            set {
                if (_am != new Activity()) {
                    if (value.Date == _am.Date) {
                        _pm = value;
                    } else {
                        throw new ArgumentException("Date must be consistent across Activities");
                    }
                } else {
                    _pm = value;
                }
            }
        }

        // Constructors
        public TrainingDay(Activity am, Activity pm) {
            if (am.Date == pm.Date) {
                _am = am;
                _pm = pm;

                Date = am.Date;
            } else 
                throw new ArgumentException("Date must be consistent across Activities");
        }
        public TrainingDay(Activity activity) {
            _am = activity;
            Date = activity.Date;
        }
        public TrainingDay() {
            Date = DateTime.MinValue;
        }

        // Overrides
        //public override string ToString() => _am.ToString() + ";" + _pm.ToString();
        public override string ToString() {
            if (_am != new Activity())
                return Date.ToUniversalTime().ToString() + ";" + _am.ToString();
            else
                return Date.ToUniversalTime().ToString() + ";" + _am.ToString() + ";" + _pm.ToString();
        }
    }
}
