using CompanyData.DataStore;
using CompanyData.Models;
using CompanyData.ViewModel;
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
using System.Windows.Shapes;

namespace CompanyData.AddData
{
    /// <summary>
    /// Interaction logic for AddDepartment.xaml
    /// </summary>
    public partial class AddDepartment : Window
    {
        private readonly DepartmentDataStore _dataStore;
        private bool checker=false;
        private Department editDeptno;
        public AddDepartment()
        {
            InitializeComponent();
            _dataStore = new DepartmentDataStore();
        }
        public AddDepartment(Department editDepartment)
        {
            InitializeComponent();
            editDeptno = editDepartment;
            checker = true;
            _dataStore = new DepartmentDataStore();
            LoadSalgrade();
        }
        

        private void LoadSalgrade()
        {
            deptnoNumber.Text = editDeptno.Deptno.ToString();
            deptnoName.Text = editDeptno.DName.ToString();
            deptnoLocation.Text = editDeptno.Location.ToString();

        }


        private void SaveDepartment(object sender, RoutedEventArgs e)
        {
            if(checker)
            {
                EditDepartment();
            }
            else
            {
                CreateDepartment();
            }
            Close();
        }
        private void EditDepartment()
        {
            Department department = new Department();
            department.Deptno = int.Parse(deptnoNumber.Text);
            department.DName = (deptnoName.Text);
            department.Location = (deptnoLocation.Text);

            _dataStore.EditDepartment(department, editDeptno);

        }
        public void CreateDepartment()
        {
            var department = new Department();
            department.Deptno = int.Parse(deptnoNumber.Text);
            department.DName = deptnoName.Text;
            department.Location = deptnoLocation.Text;

            _dataStore.CreateDepartment(department);

        }
    }
}
