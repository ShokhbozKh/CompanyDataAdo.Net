using CompanyData.Constants;
using CompanyData.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompanyData.DataStore
{
    public class SalgradeDataStore
    {
        private readonly SqlConnection _connection;
        public SalgradeDataStore()
        {
            _connection = new SqlConnection(DataBaseConfigurations.CONNECTION_STRING);
        }

        public List<Salgrade> GetSalgrades()
        {
            List<Salgrade> salgrades = new List<Salgrade>();

            try
            {
                string query = "select *from salgrade;";
                SqlCommand sqlCommand = new SqlCommand(query, _connection);
                _connection.Open();
                var resultSalgrade = sqlCommand.ExecuteReader();


                if (resultSalgrade.HasRows)
                {
                    while (resultSalgrade.Read())
                    {
                        Salgrade salgrade = new Salgrade();
                        salgrade.grade = resultSalgrade.GetInt32(0);
                        salgrade.losal = resultSalgrade.GetDecimal(1);
                        salgrade.hisal = resultSalgrade.GetDecimal(2);

                        salgrades.Add(salgrade);
                    }
                }
            }
            catch(SqlException ex) { MessageBox.Show($"Salgrade sql:{ex.Data}"); }
            catch(Exception ex) { MessageBox.Show($"Error salgrade: {ex.Message}"); }

            finally { _connection.Close(); }
            
            return salgrades;

        }

        public void DeleteSalgrade(Salgrade deleteSal)
        {
            try
            {
                var query = $"Delete from Salgrade " +
               $" Where grade = {deleteSal.grade};";
                SqlCommand sqlCommand = new SqlCommand(query, _connection);
                _connection.Open();
                var test = sqlCommand.ExecuteNonQuery();

            }
            catch(SqlException ex)
            {
                MessageBox.Show("error sql salgrade " +  ex.Message );
            }
            catch(Exception ex)
            {
                MessageBox.Show("error salgrade: " + ex.ToString() );
            }
            finally { _connection.Close(); }
           

        }

        public void CreateSalgrade(Salgrade salgrade)
        {
            try
            {
                 var query = $"INSERT INTO salgrade (grade, losal, hisal) " +
                    $" VALUES ({salgrade.grade}, {salgrade.losal}, {salgrade.hisal}); ";
                SqlCommand sqlCommand = new SqlCommand( query, _connection);
                _connection.Open();
                var resultTest = sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Qushildi malumot..");
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error sql salgrade: " + ex.Message );
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error salgrade "+ ex.ToString() );
            }
            finally { _connection.Close(); }

        }

        public void EditSalgrade(Salgrade EditSalgrade, Salgrade oldSalgrade)
        {
            var constant = oldSalgrade.grade;
            string query = $"Update Salgrade  " +
                $" Set   grade={EditSalgrade.grade}, losal={EditSalgrade.losal}, hisal={EditSalgrade.hisal}" +
                $" Where  grade = {constant}";

            SqlCommand sqlCommand = new SqlCommand(query, _connection);
            _connection.Open();
            var resultTest = sqlCommand.ExecuteNonQuery();

        }
        

    }
}
