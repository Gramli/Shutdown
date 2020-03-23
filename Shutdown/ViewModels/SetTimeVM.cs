using Shutdown.Validators;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;

namespace Shutdown.ViewModels
{
    /// <summary>
    /// View model for set and validate time
    /// </summary>
    public class SetTimeVM : ViewModel, IDataErrorInfo
    {
        private TimeUnitsEnum _selectedTimeUnit = TimeUnitsEnum.Minutes;
        public TimeUnitsEnum SelectedTimeUnit
        {
            get { return this._selectedTimeUnit; }
            set
            {
                this._selectedTimeUnit = value;
                NotifiyPropertyChanged("SelectedTimeUnit");
            }
        }

        private string _timeValue = "25";
        public string TimeValue
        {
            get { return this._timeValue; }
            set
            {
                this._timeValue = value;
                NotifiyPropertyChanged("TimeValue");
            }
        }

        public string[] DefaultTimeValues { get; private set; }

        /// <summary>
        /// Determines that setted value is valid
        /// </summary>
        public bool IsValid { get; private set; }
        public string Error => "....";
        /// <summary>
        /// Allows to validate property by name
        /// </summary>
        public string this[string columnName] => Validate(columnName);

        private NumberValidationRule _numberValidationRule;

        public SetTimeVM()
        {
            this._numberValidationRule = new NumberValidationRule();
            InitDefaultValues(6);
        }
        /// <summary>
        /// Create timespan from setted value
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetTimeSpan()
        {
            var result = TimeSpan.Zero;

            switch (this.SelectedTimeUnit)
            {
                case TimeUnitsEnum.Hours: result = new TimeSpan(0, Convert.ToInt32(this.TimeValue), 0, 0); break;
                case TimeUnitsEnum.Minutes: result = new TimeSpan(0, 0, Convert.ToInt32(this.TimeValue), 0); break;
                case TimeUnitsEnum.Seconds: result = new TimeSpan(0, 0, 0, Convert.ToInt32(this.TimeValue)); break;
            }

            return result;
        }

        /// <summary>
        /// Set value from timespan and selected time unit
        /// </summary>
        public void SetByTimeSpan(TimeSpan timeSpan)
        {
            switch (this.SelectedTimeUnit)
            {
                case TimeUnitsEnum.Hours: this.TimeValue = $"{timeSpan.TotalHours}"; break;
                case TimeUnitsEnum.Minutes: this.TimeValue = $"{timeSpan.TotalMinutes}"; break;
                case TimeUnitsEnum.Seconds: this.TimeValue = $"{timeSpan.TotalSeconds}"; break;
            }
        }

        private string Validate(string property)
        {
            var result = string.Empty;
            ValidationResult ruleResult = null;
            switch (property)
            {
                case "TimeValue": ruleResult = this._numberValidationRule.Validate(this.TimeValue, CultureInfo.InvariantCulture); break;
            }

            this.IsValid = ruleResult.IsValid;

            if (!ruleResult.IsValid) result = (string)ruleResult.ErrorContent;

            return result;
        }

        private void InitDefaultValues(int count)
        {
            this.DefaultTimeValues = new string[count];

            for (int i = 0; i < count; i++)
            {
                this.DefaultTimeValues[i] = $"{i * 5}";
            }

            NotifiyPropertyChanged("DefaultTimeValues");
        }
    }
}
