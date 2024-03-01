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
    internal class DepartmentDataStore
    {
        public readonly SqlConnection _connection;

        public DepartmentDataStore()
        {
            _connection = new SqlConnection(DataBaseConfigurations.CONNECTION_STRING);

        }

        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            try
            {
                string query = "SELECT *FROM dept;";
                _connection.Open();
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = query;
                var result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        var department = new Department();
                        department.Deptno = result.GetInt32(0);
                        department.DName = result.GetString(1);
                        department.Location = result.GetString(2);

                        departments.Add(department);
                    }
                }
                var g = 0;
            }
            catch (SqlException e)
            {
                MessageBox.Show("SQL xatoligi: " + e.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Umumiy xatolik: " + e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return departments;
        }

        public List<int> GetOnlyDepartments()
        {
                var departments = new List<int>();
            try
            {
                var query = $"Select deptno from dept;";
                _connection.Open();
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = query;
                var result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        var department = new Department();
                        department.Deptno = (int)result.GetInt32(0);
                        departments.Add(department.Deptno);
                    }
                }

            }
            catch(SqlException e)
            {
                MessageBox.Show($"Sql error only deptno {e.Message}");
            }
            catch(Exception e)
            {
                MessageBox.Show($"Error only deptno {e.ToString()}");
            }
            finally { _connection.Close(); }
            

            return departments;
        }

        public List<Department> FilterDepartments(string location)
        {
            List<Department> departments = new List<Department>();
            try
            {
                string query = $"Select *from dept " +
                    $" where location LIKE '%{location}%'";
                _connection.Open();
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = query;
                var result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        var department = new Department();
                        department.Deptno = result.IsDBNull(0) ? 0 : result.GetInt32(0);
                        department.DName = result.IsDBNull(1) ? null : result.GetString(1);
                        department.Location = result.IsDBNull(2) ? null : result.GetString(2);

                        departments.Add(department);
                    }
                }
                
            }
            catch(SqlException e)
            {
                MessageBox.Show("sql error department filter: " + e.ToString());
            }
            catch(Exception e)
            {
                MessageBox.Show("Xatolik: filter deptno:" + e.Message);
            }
            finally { _connection.Close(); }
            return departments;
        }

        public void CreateDepartment(Department department)
        {
            try
            {
                string query = $"Insert into dept (deptno, dname, location) " +
                    $" values('{department.Deptno}', '{department.DName}', '{department.Location}');";

                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();

                var result = command.ExecuteNonQuery();

                MessageBox.Show($"Create Succed: {result}  saved...");


            }catch(SqlException ex)
            {
                MessageBox.Show($"error dept sql:{ex.ToString()}");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error dept" + ex.Message);
            }
            finally { _connection.Close(); }
        }

        public void DeleteDepartment(Department deleteDept)
        {
            try
            {
                var query = $"Delete from dept " +
                $" where deptno = {deleteDept.Deptno} ;";

                SqlCommand sqlCommand = new SqlCommand(query, _connection);
                _connection.Open();
                var result = sqlCommand.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Delete sql deptno: {ex.Message}");
            }
            catch( Exception ex )
            {
                MessageBox.Show("Delete deptno error:" + ex.ToString());
            }
            finally { _connection.Close(); }
              
        }

        public void EditDepartment(Department department, Department oldDept)
        {
            try
            {
                var query = $"Update dept Set deptno={department.Deptno}, dname='{department.DName}', location='{department.Location}' " +
                    $" where deptno={oldDept.Deptno};";
                SqlCommand sqlCommand = new SqlCommand( query, _connection);
                _connection.Open();
                var result = sqlCommand.ExecuteNonQuery();
                MessageBox.Show($"Edited... {result}");
            }
            catch(SqlException ex)
            {
                MessageBox.Show($"edit dept error sql{ ex.Message}");
            }
            catch(Exception e)
            {
                MessageBox.Show($"Erroe edit dept {e.Message}");
            }
            finally { _connection.Close(); }
        }
    }
}
