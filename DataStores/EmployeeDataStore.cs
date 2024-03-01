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
    internal class EmployeeDataStore
    {
        public readonly SqlConnection connection;
        public readonly Employee employee;
        public EmployeeDataStore()
        {
            connection = new SqlConnection(DataBaseConfigurations.CONNECTION_STRING);
            employee = new Employee();

        }
        public bool ConnectDataBase()
        {
            bool result = false;
            try
            {
                connection.Open();
                result = true;

            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Sql error: {ex.ToString()}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xatolik ulanishda: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> resultEmp = new List<Employee>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "Select *from emp;";
                connection.Open();
                sqlCommand.Connection = connection;

                var resultData = sqlCommand.ExecuteReader();

                var g = 0;
                if (resultData.HasRows)
                {
                    while (resultData.Read())
                    {
                        Employee emp = new Employee();
                        //var mgr = resultData.GetValue(3).ToString();

                        emp.Empno = resultData.IsDBNull(0) ? 0 : resultData.GetInt32(0);
                        emp.Ename = resultData.IsDBNull(1) ? null : resultData.GetString(1);
                        emp.Job = resultData.IsDBNull(2) ? "null" : resultData.GetString(2);
                        emp.Mgr = resultData.IsDBNull(3) ? null : resultData.GetInt32(3);

                        emp.Hiredate = resultData.IsDBNull(4) ? DateTime.Now : resultData.GetDateTime(4);
                        emp.Salary = resultData.IsDBNull(5) ? 0 : resultData.GetDecimal(5);
                        emp.Commision = resultData.IsDBNull(6) ? 0 : resultData.GetDecimal(6);
                        emp.Deptno = resultData.IsDBNull(7) ? 0 : resultData.GetInt32(7);

                        resultEmp.Add(emp);

                    }
                    }

            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Emp connect sql:{ex.Message}");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Emp connect :{ex.ToString()}");
            }
            finally
            {
                connection.Close();
            }

            return resultEmp;
        }

        public List<Employee> GetEmployees(int deptno)
        {
            List<Employee> resultEmp = new List<Employee>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = $"SELECT *FROM emp" +
                    $"WHERE deptno = {deptno};";
                connection.Open();
                sqlCommand.Connection = connection;

                var resultData = sqlCommand.ExecuteReader();

                var g = 0;
                if (resultData.HasRows)
                {
                    while (resultData.Read())
                    {
                        Employee emp = new Employee();
                        //var mgr = resultData.GetValue(3).ToString();

                        emp.Empno = resultData.IsDBNull(0) ? 0 : resultData.GetInt32(0);
                        emp.Ename = resultData.IsDBNull(1) ? null : resultData.GetString(1);
                        emp.Job = resultData.IsDBNull(2) ? "null" : resultData.GetString(2);
                        emp.Mgr = resultData.IsDBNull(3) ? null : resultData.GetInt32(3);

                        emp.Hiredate = resultData.IsDBNull(4) ? DateTime.Now : resultData.GetDateTime(4);
                        emp.Salary = resultData.IsDBNull(5) ? 0 : resultData.GetDecimal(5);
                        emp.Commision = resultData.IsDBNull(6) ? 0 : resultData.GetDecimal(6);
                        emp.Deptno = resultData.IsDBNull(7) ? 0 : resultData.GetInt32(7);

                        resultEmp.Add(emp);

                    }
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Emp connect sql:{ex.Message}");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Emp connect :{ex.ToString()}");
            }
            finally
            {
                connection.Close();
            }

            return resultEmp;
        }

        public List<Employee> GetEmployees(string searchString)
        {
            List<Employee> resultEmp = new List<Employee>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = $"SELECT *FROM emp " +
                    $" WHERE Ename LIKE  '%{searchString}%';";
                connection.Open();
                sqlCommand.Connection = connection;

                var resultData = sqlCommand.ExecuteReader();

                var g = 0;
                if (resultData.HasRows)
                {
                    while (resultData.Read())
                    {
                        Employee emp = new Employee();
                        //var mgr = resultData.GetValue(3).ToString();

                        emp.Empno = resultData.IsDBNull(0) ? 0 : resultData.GetInt32(0);
                        emp.Ename = resultData.IsDBNull(1) ? null : resultData.GetString(1);
                        emp.Job = resultData.IsDBNull(2) ? "null" : resultData.GetString(2);
                        emp.Mgr = resultData.IsDBNull(3) ? null : resultData.GetInt32(3);

                        emp.Hiredate = resultData.IsDBNull(4) ? DateTime.Now : resultData.GetDateTime(4);
                        emp.Salary = resultData.IsDBNull(5) ? 0 : resultData.GetDecimal(5);
                        emp.Commision = resultData.IsDBNull(6) ? 0 : resultData.GetDecimal(6);
                        emp.Deptno = resultData.IsDBNull(7) ? 0 : resultData.GetInt32(7);

                        resultEmp.Add(emp);

                    }
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Emp connect sql:{ex.Message}");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Emp connect :{ex.ToString()}");
            }
            finally
            {
                connection.Close();
            }

            return resultEmp;
        }

        public void CreateEmployees(Employee newEmp)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DataBaseConfigurations.CONNECTION_STRING))
                {

                    connection.Open();
                    var Query = $"INSERT INTO EMP " +
                        $"(empno, ename, job, mgr, hiredate, sal, comm, deptno) " +
                        $"VALUES ({newEmp.Empno}, '{newEmp.Ename}', '{newEmp.Job}', {newEmp.Mgr}, '{newEmp.Hiredate.ToString("yyyy-MM-dd")}'," +
                        $" {newEmp.Salary}, {newEmp.Commision}, {newEmp.Deptno})";

                    SqlCommand sqlCommand = new SqlCommand(Query, connection);
                    var resultData = sqlCommand.ExecuteNonQuery();

                    MessageBox.Show($"save ... {newEmp}");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Emp add error " + ex.ToString());
            }

        }

        public List<string> GetDistinctJobs()
        {
            List<string> resultJobs = new List<string>();

            using (SqlConnection connection = new SqlConnection(DataBaseConfigurations.CONNECTION_STRING))
            {
                connection.Open();
                var query = "SELECT DISTINCT(Job) FROM emp;";
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                var listData = sqlCommand.ExecuteReader();

                if (listData.HasRows)
                {
                    while (listData.Read())
                    {
                        resultJobs.Add(listData.GetString(0));
                    }
                }
            }
            return resultJobs;
        }

        public void DeleteEmployee(Employee emp)
        {
            try
            {
                var deleteEmp = emp;
                var query = $"Delete from emp  " +
                    $" where  empno = {deleteEmp.Empno}; ";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                connection.Open();
                var listData = sqlCommand.ExecuteNonQuery();

            }
            catch(SqlException ex)
            {
                MessageBox.Show($"Error delete sql emp:{ex.ToString()}");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error delete: {ex.Message}");
            }
            finally { connection.Close(); }
            
        }

        public void EditEmployees(Employee emp, Employee oldEmp)
        {
            try
            {
                var query = $"Update emp " +
                    $" Set empno = {emp.Empno}, ename = '{emp.Ename}', job='{emp.Job}', mgr = {emp.Mgr}, hiredate={emp.Hiredate}, sal={emp.Salary}," +
                    $" comm={emp.Commision}, deptno={emp.Deptno} " +
                    $" where empno = {oldEmp};";

                SqlCommand sqlCommand = new SqlCommand(query, connection);
                connection.Open();
                var result = sqlCommand.ExecuteNonQuery();
                MessageBox.Show($" edited... emp {result}");
            }
            catch(SqlException ex)
            {
                MessageBox.Show($"sql edit emp error {ex.ToString()}");
            }
            catch(Exception ex) { MessageBox.Show($"emp edit error {ex.Message}"); }
            finally { connection.Close(); }
        }
    }
}
