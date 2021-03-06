﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Interface;

namespace BotFactory.Common
{
    public class StatusChangedEventArgs : EventArgs, IStatusChangedEventArgs
    {
        private string newStatus;
        public string NewStatus
        {
            get { return this.newStatus; }
            set
            {
                if (this.newStatus != null)
                { this.newStatus = value; }
            }
        }
    }
}
