using Microsoft.Data.Sqlite;

namespace EmployeeCRUDApp
{
    class EmployeeRepository
    {
        //create Employee
        public static bool CreateEmployee(SqliteConnection connection, Employee employee)
        {

            string insertQuery = "INSERT INTO Employees(EmployeeId,FirstName,LastName,DateOfBirth) VALUES(@EmployeeId,@FirstName,@LastName,@DateOfBirth)";
            using (var command = new SqliteCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }



        //get single employee
        public static Employee GetEmployeeById(SqliteConnection connection, string employeeId)
        {
            Employee empObj = null;
            string selectQuery = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";

            using (var command = new SqliteCommand(selectQuery, connection))
            {
                // Add parameter to prevent SQL injection
                command.Parameters.AddWithValue("@EmployeeID", employeeId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read()) // Since it's fetching a single employee, no need for a while loop
                    {
                        empObj = new Employee
                        {
                            EmployeeId = reader["EmployeeID"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            DateOfBirth = reader["DateOfBirth"].ToString()
                        };
                    }
                }
            }
            return empObj;
        }




        //list of All Employees
        public static List<Employee> ListEmployees(SqliteConnection connection)
        {
            List<Employee> emplist = new List<Employee>();
            string selectQuery = "SELECT * FROM Employees";
            using (var command = new SqliteCommand(selectQuery, connection))
            {
                using (var reader = command.ExecuteReader())
                {


                    while (reader.Read())
                    {
                        Employee empobj = new Employee
                        {
                            EmployeeId = reader["EmployeeID"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            DateOfBirth = reader["DateOfBirth"].ToString()
                        };


                        emplist.Add(empobj);
                    }
                }
            }
            return emplist;
        }



        //Update Employee
        public static bool UpdateEmployee(SqliteConnection connection, Employee employee)
        {
            string updateQuery = @"UPDATE Employees
                             SET FirstName=@FirstName,LastName=@LastName,DateOfBirth =@DateOfBirth 
                             WHERE EmployeeID=@EmployeeID";

            using (var command = new SqliteCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeId);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }




        //Delete Employee
        public static Employee DeleteEmployee(SqliteConnection connection, Employee employee)
        {

            string deleteQuery = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";

            using (var command = new SqliteCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeId);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return employee;
                }
                else
                {
                    return null;
                }
            }
        }

        internal static Employee GetEmployeeById(SqliteConnection connection, int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}