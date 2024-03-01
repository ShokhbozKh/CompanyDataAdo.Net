using CompanyData.Constants;
using CompanyData.DataStore;
using CompanyData.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Input;

namespace CompanyData.ViewModel
{
    internal class DepartmentViewModel : BaseViewModel
    {
        public readonly DepartmentDataStore departmentDataStore;
        

        public ICommand AddDept;
        public ICommand DeleteDept;
        public ICommand EditDept;

        public DepartmentViewModel()
        {
            departmentDataStore = new DepartmentDataStore();

            AddDept = new Command(OnAddDeptno);
            DeleteDept = new Command(OnDeleteDeptno);
            EditDept = new Command(OnEditDeptno);
        }
        private void OnAddDeptno()
        {
            

        }
        private void OnDeleteDeptno()
        {

        }
        private void OnEditDeptno()
        {

        }



    }
}
