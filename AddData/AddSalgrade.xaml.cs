using CompanyData.DataStore;
using CompanyData.Models;
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
    /// Interaction logic for AddSalgrade.xaml
    /// </summary>
    public partial class AddSalgrade : Window
    {
        private readonly SalgradeDataStore _salgradeDataStore;
        private bool checker=false;
        private Salgrade editSalgrade;
        public AddSalgrade()
        {
            InitializeComponent();
            _salgradeDataStore = new SalgradeDataStore();
        }
        public AddSalgrade(Salgrade editSalgrade)
        {
            InitializeComponent();
            checker = true;
            _salgradeDataStore = new SalgradeDataStore();
            this.editSalgrade = editSalgrade;
            LoadSalgrade(editSalgrade);
        }

        private void LoadSalgrade(Salgrade editSalgrade)
        {
            if(editSalgrade == null) { return; }
            SalgradeGrade.Text = editSalgrade.grade.ToString();
            SalgradeLosal.Text = editSalgrade.losal.ToString();
            SalgradeHisal.Text = editSalgrade.hisal.ToString();

        }
        private void EditSalgrade()
        {
            var oldSalgrade = editSalgrade;
            Salgrade salgrade = new Salgrade();
            salgrade.grade = int.Parse(SalgradeGrade.Text);
            salgrade.losal = decimal.Parse(SalgradeLosal.Text);
            salgrade.hisal = decimal.Parse(SalgradeHisal.Text);

            _salgradeDataStore.EditSalgrade(salgrade, oldSalgrade);
        }
       
        private void btnSaveSalgrade(object sender, RoutedEventArgs e)
        {
            if (checker)
            {
                EditSalgrade();
            }
            else
            {
                CreateSalgrade();
            }
            Close();
        }
        private void CreateSalgrade()
        {
            var newSalgrade = new Salgrade();
            newSalgrade.grade = int.Parse(SalgradeGrade.Text);
            newSalgrade.losal = decimal.Parse(SalgradeLosal.Text);
            newSalgrade.hisal = decimal.Parse(SalgradeHisal.Text);

            _salgradeDataStore.CreateSalgrade(newSalgrade);
        }
    }
}
