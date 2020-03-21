﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Shutdown.ViewModel
{
    /// <summary>
    /// Base object for view models 
    /// Implements INotifyPropertyChanged interface and Invoke method
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Invoke property change
        /// </summary>
        /// <param name="property"></param>
        protected void NotifiyPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Event for invoking propery change
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
