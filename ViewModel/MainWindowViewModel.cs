using CompanyData.Models;
using MvvmHelpers;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyData.ViewModel
{
    internal class MainWindowViewModel : BaseViewModel
    {
        public string MainTitle { get; set; } = "Employees";
        public ICommand AddClicked { get; set; }

        public MainWindowViewModel( )
        {
            AddClicked = new DelegateCommand(OnAddStudent);

        }

        public void OnAddStudent()
        {


        }

    }
}
