using CompanyData.AddData;
using CompanyData.DataStore;
using CompanyData.Models;
using CompanyData.ViewModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompanyData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EmployeeDataStore employeeDataStore;
        private readonly DepartmentDataStore departmentDataStore;
        private readonly SalgradeDataStore salgradeDataStore;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            employeeDataStore = new EmployeeDataStore();
            departmentDataStore = new DepartmentDataStore();
            salgradeDataStore = new SalgradeDataStore();
            LoadAllData();
        }
        private void LoadAllData()
        {
            LoadDeptno();
            LoadComboBox();
            LoadSalgrade();
            LoadEmp();
        }
        private void LoadEmp()
        {
            var resultEmpData = employeeDataStore.GetEmployees();

            EmployeesDataGrid.ItemsSource = null;
            EmployeesDataGrid.ItemsSource = resultEmpData;
        }
        private void LoadDeptno()
        {
            var resultDept = departmentDataStore.GetDepartments();
            if (resultDept != null)
            {
                DepartmentDataGrid.ItemsSource = resultDept;
            }
        }
        private void LoadSalgrade()
        {
            var resultSal = salgradeDataStore.GetSalgrades();

            SalgradeDataGrid.ItemsSource = resultSal;
        }

        private void LoadComboBox()
        {
            var resultJob = employeeDataStore.GetDistinctJobs();
            resultJob.Add("All");
            DepartmentsComboBox.ItemsSource = resultJob.Order();

            //DepartmentsComboBox.DisplayMemberPath = "Dname";
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (employeeDataStore.ConnectDataBase())
            {
                MessageBox.Show("Ulandi db ...");
            }
            else
            {
                MessageBox.Show("Ulanmadi ....");
            }
        }

        private void EmpDataLoader(object sender, RoutedEventArgs e)
        {
            try
            {
                var resultEmpData = employeeDataStore.GetEmployees();
                if (resultEmpData is null)
                {
                    return;
                }
                EmployeesDataGrid.ItemsSource = null;
                EmployeesDataGrid.ItemsSource = resultEmpData;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik emp: " + ex.Message);
            }

        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditEmployee addEmp = new AddOrEditEmployee();
            addEmp.ShowDialog();
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            var searchInput = searchTextBox.Text;
            var resultSearchData = employeeDataStore.GetEmployees(searchInput);

            EmployeesDataGrid.ItemsSource = null;
            EmployeesDataGrid.ItemsSource = resultSearchData;
        }

        private void DepartmentsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedJob = DepartmentsComboBox.SelectedItem;

            if (EmployeesDataGrid.SelectedIndex == 0)
            {
                EmployeesDataGrid.ItemsSource = employeeDataStore.GetEmployees();/////////////////////////
            }
            else
            {
                EmployeesDataGrid.ItemsSource = employeeDataStore.GetEmployees().Where(x => x.Job == (SelectedJob).ToString());
            }
        }

        private void DeleteEmp(object sender, RoutedEventArgs e)
        {
            var deleteEmp = EmployeesDataGrid.SelectedItem as Employee;
            if (deleteEmp != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"{deleteEmp.Ename} uchirilmoqda. ","Error",MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if(MessageBoxResult.Yes == messageBoxResult)
                {
                    employeeDataStore.DeleteEmployee(deleteEmp);
                    MessageBox.Show("Delete......");
                }
            }
            
            //deleteEmp.
        }

        private void AddDepartments(object sender, RoutedEventArgs e)
        {
            AddDepartment dept = new AddDepartment();
            dept.ShowDialog();
        }

        
        private void btnSearchDeptno(object sender, RoutedEventArgs e)
        {
            var searchInput = searchTextBoxDept.Text;

            if (string.IsNullOrWhiteSpace(searchInput))
            {
                var data = departmentDataStore.GetDepartments();
                DepartmentDataGrid.ItemsSource = data;
            }

            var resultData = departmentDataStore.FilterDepartments(searchTextBoxDept.Text);

            DepartmentDataGrid.ItemsSource = null;
            DepartmentDataGrid.ItemsSource = resultData;

        }

        private void btnSearchSal(object sender, RoutedEventArgs e)
        {
            var searchData = decimal.TryParse(searchTextBoxSal.Text, out decimal resultPrice); //Super

            if (!searchData)
            {
                var dataFilter = salgradeDataStore.GetSalgrades();
                SalgradeDataGrid.ItemsSource = null;
                SalgradeDataGrid.ItemsSource = dataFilter;
            }
            else
            {
                var dataFilter = salgradeDataStore.GetSalgrades()
                .Where(x => x.losal <= resultPrice && x.hisal >= resultPrice);

                SalgradeDataGrid.ItemsSource = null;
                SalgradeDataGrid.ItemsSource = dataFilter;
            }

            
        }

        private void deleteDeptno(object sender, RoutedEventArgs e)
        {
            var selectedDept = DepartmentDataGrid.SelectedItem as Department;
            if (selectedDept != null)
            {
                var check=MessageBox.Show($"{selectedDept.DName} delete...","! diqqat",MessageBoxButton.OKCancel,MessageBoxImage.Error);
                if (check.HasFlag(MessageBoxResult.OK))
                {
                    departmentDataStore.DeleteDepartment(selectedDept);
                    MessageBox.Show("Delete successful..");
                }
            }
        }

        private void btnDeleteSal(object sender, RoutedEventArgs e)
        {
            var selectedSal = SalgradeDataGrid.SelectedItem as Salgrade;
            MessageBoxResult result = MessageBox.Show($"delete {selectedSal.grade}","diqqat...",MessageBoxButton.YesNo);

            if(selectedSal != null)
            {
                if (result == MessageBoxResult.Yes)
                {
                    salgradeDataStore.DeleteSalgrade(selectedSal);
                    MessageBox.Show("Delete..");
                }

            }
            
           
        }

        private void btnCreateSalgrades(object sender, RoutedEventArgs e)
        {
            AddSalgrade addSalgrade = new AddSalgrade();
            addSalgrade.ShowDialog();
        }

        private void btnEditSalgrade(object sender, RoutedEventArgs e)
        {
            AddSalgrade addSalgrade = new AddSalgrade(SalgradeDataGrid.SelectedItem as Salgrade);
            addSalgrade.ShowDialog();
        }

        private void btnEditDept(object sender, RoutedEventArgs e)
        {
            AddDepartment addDepartment = new AddDepartment(DepartmentDataGrid.SelectedItem as Department);
            addDepartment.ShowDialog();
        }

        private void btnEditEmp(object sender, RoutedEventArgs e)
        {
            AddOrEditEmployee addEmployee = new AddOrEditEmployee(EmployeesDataGrid.SelectedItem as Employee);
            addEmployee.ShowDialog();
        }
    }
}