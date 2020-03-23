using Shutdown.Commands;
using Shutdown.Models;
using System.Windows.Input;

namespace Shutdown.ViewModels
{
    public class MainVM : ViewModel
    {
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

        public SetTimeVM SetTime { get; private set; }
        public CountdownVM Countdown { get; private set; }

        private ICommand _actionCommand;
        public ICommand ActionCommand 
        { 
            get { return this._actionCommand; } 
        }

        public MainVM()
        {
            this._actionCommand = new RelayCommand(Execute,CanExecute);
            InitViewModels();
        }

        private void InitViewModels()
        {
            this.SetTime = new SetTimeVM();
            NotifiyPropertyChanged("SetTime");

            this.Countdown = new CountdownVM();
            this.Countdown.Elapsed += Countdown_Elapsed;
            NotifiyPropertyChanged("Countdown");
        }

        private void Countdown_Elapsed()
        {
            switch(this.SelectedAction)
            {
                case SystemActionsEnum.Shutdown: SystemActions.Shutdown(); break;
                case SystemActionsEnum.Restart: SystemActions.Restart(); break;
            }
        }

        private void Execute(object parameter)
        {
            switch((string)parameter)
            {
                case "start":
                    var countdownTime = this.SetTime.GetTimeSpan();
                    this.Countdown.Start(countdownTime); break;
                case "stop":
                    this.SetTime.SetByTimeSpan(this.Countdown.RestTime);
                    this.Countdown.Stop();
                    break;
            }
        }

        private bool CanExecute(object parameter)
        {
            return this.SetTime.IsValid;
        }

    }
}
