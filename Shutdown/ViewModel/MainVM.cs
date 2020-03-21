namespace Shutdown.ViewModel
{
    public class MainVM : ViewModel
    {
        private uint _hours;
        public uint Hours
        {
            get { return this._hours; }
            set 
            {
                this._hours = value;
                NotifiyPropertyChanged("Hours");
            }
        }

        private uint _minutes;
        public uint Minutes
        {
            get { return this._minutes; }
            set
            {
                this._minutes = value;
                NotifiyPropertyChanged("Minutes");
            }
        }

        private uint _seconds;
        public uint Seconds
        {
            get { return this._seconds; }
            set
            {
                this._seconds = value;
                NotifiyPropertyChanged("Seconds");
            }
        }

        private SystemActionsEnum _selectedAction = SystemActionsEnum.Shutdown;
        public SystemActionsEnum SelectedAction
        {
            get { return this._selectedAction; }
            set
            {
                this._selectedAction = value;
                NotifiyPropertyChanged("SelectedAction");
            }
        }

        public void 
    }
}
