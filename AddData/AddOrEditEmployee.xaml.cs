using CompanyData.DataStore;
using CompanyData.EditData;
using CompanyData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
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
    /// Interaction logic for AddOrEditEmployee.xaml
    /// </summary>
    public partial class AddOrEditEmployee : Window
    {
        private Employee employee;
        private readonly EmployeeDataStore employeeDataStore;
        private Department department;
        private readonly List<Department> departments;
        private readonly DepartmentDataStore departmentDataStore;
        private readonly List<Employee> employees;
        private bool checker = false;
        private Employee oldEmployee;

        public AddOrEditEmployee()
        {
            InitializeComponent();
            employeeDataStore = new EmployeeDataStore();
            departmentDataStore = new DepartmentDataStore();
            employees = new List<Employee>();
            CreateDataLoad();
        }
        public AddOrEditEmployee(Employee employee)
        {
            InitializeComponent();
            oldEmployee = employee;
            employeeDataStore = new EmployeeDataStore();
            departmentDataStore = new DepartmentDataStore();
            checker =true;
            EditDataLoad();
        }
        private void EditDataLoad()
        {
            Empno.Text = oldEmployee.Empno.ToString();
            Ename.Text = oldEmployee.Ename.ToString();
            //
            JobsComboBox.SelectedIndex = employeeDataStore.GetDistinctJobs().IndexOf(oldEmployee.Job);
            JobsComboBox.ItemsSource = employeeDataStore.GetDistinctJobs();

            //
            MgrComboBox.ItemsSource = employeeDataStore.GetEmployees();

            var deptno = departmentDataStore.GetDepartments();
            DepartmentComboBox.ItemsSource = deptno;
            //DepartmentComboBox.DisplayMemberPath = "DName";// ustun nomi
            DepartmentComboBox.DisplayMemberPath = nameof(Department.DName);


            HireDate.Text = oldEmployee.Hiredate.ToString();
            Sal.Text = oldEmployee.Salary.ToString();
            Commision.Text = oldEmployee.Commision.ToString();
        }
        private void CreateDataLoad()
        {
            var deptno = departmentDataStore.GetDepartments();
            var job = employeeDataStore.GetDistinctJobs();
            var mgr = employeeDataStore.GetEmployees();

            JobsComboBox.SelectedIndex = 0;
            MgrComboBox.SelectedIndex = 0;
            DepartmentComboBox.SelectedIndex = 0;

            HireDate.SelectedDate = DateTime.Now;

            MgrComboBox.ItemsSource = mgr;

            JobsComboBox.ItemsSource = job;
            // stringlar listni qabul qilyapti  object emas
            //JobsComboBox.DisplayMemberPath = nameof(Employee.Job);

            DepartmentComboBox.ItemsSource = deptno;
            //DepartmentComboBox.DisplayMemberPath = "DName";// ustun nomi
            DepartmentComboBox.DisplayMemberPath = nameof(Department.DName);
        }

        private void EditEmployee()
        {
            try
            {
                Employee newEmployee = new Employee();
                newEmployee.Empno = int.Parse(Empno.Text);
                newEmployee.Ename = Ename.Text;
                var jobs = JobsComboBox.Items;
                newEmployee.Job = jobs.ToString();
                var manager = MgrComboBox.SelectedItem as Employee;
                newEmployee.Mgr = manager.Mgr;
                newEmployee.Hiredate = HireDate.SelectedDate.Value;
                newEmployee.Salary = decimal.Parse(Sal.Text);
                newEmployee.Commision = decimal.Parse(Commision.Text);
                var deptno = DepartmentComboBox.SelectedItem as Employee;
                newEmployee.Deptno = deptno.Deptno;

                employeeDataStore.EditEmployees(newEmployee, oldEmployee);

            }
            catch(Exception ex)
            {
                MessageBox.Show("edit emp " + ex.Message);
            }
           

        }
        private void btnEmpAddOrEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checker)
                {
                    EditEmployee();
                }
                else
                {

                    AddEmployee();
                }

            }catch (Exception ex)
            {
                MessageBox.Show("Xatolik edit emp " +  ex.Message);
            }
            
        }
        private void AddEmployee()
        {
            var department = DepartmentComboBox.SelectedItem as Department;
            var jobs = JobsComboBox.SelectedItem as string;
            var manager = MgrComboBox.SelectedItem as Employee;
            try
            {
                employee = new Employee()
                {
                    Empno = int.Parse(Empno.Text),
                    Ename = Ename.Text,

                    //Job = Job.Text,
                    Job = jobs,

                    //Mgr = int.Parse(Mgr.Text),
                    Mgr = manager.Empno,

                    Hiredate = DateTime.Parse(HireDate.Text),
                    Salary = decimal.Parse(Sal.Text),
                    Commision = decimal.Parse(Commision.Text),

                    // Deptno = int.Parse(Deptno.Text),
                    Deptno = department.Deptno
                };

                employeeDataStore.CreateEmployees(employee);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Add emp:" + ex.Message);
            }

            Close();
        }
    }
    
}
