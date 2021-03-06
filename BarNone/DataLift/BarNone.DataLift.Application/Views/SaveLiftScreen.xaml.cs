﻿using BarNone.DataLift.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BarNone.DataLift.UI.Views
{
    /// <summary>
    /// Interaction logic for SaveLiftScreen.xaml
    /// </summary>
    public partial class SaveLiftScreen : UserControl
    {
        public SaveLiftScreen()
        {
            InitializeComponent();

            var vm = DataContext as ViewModelBase;

            Loaded += (a, b) => vm.Loaded();
        }
    }
}
