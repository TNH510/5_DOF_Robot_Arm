﻿using RobotArmHelix.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArmHelix.ViewModel
{
    class CustomerVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public int CustomerID
        {
            get { return _pageModel.CustomerCount; }
            set { _pageModel.CustomerCount = value; OnPropertyChanged(); }
        }

        public CustomerVM()
        {
            _pageModel = new PageModel();
            CustomerID = 100528;
        }
    }
}
