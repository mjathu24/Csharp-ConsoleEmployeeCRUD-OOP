using System;
using Microsoft.Data.Sqlite;

namespace EmployeeCRUDApp
{
    class Program
    {
        private static object employeeId;

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\n ----Employee Management----");
                Console.WriteLine("1.Create Employee");
                Console.WriteLine("2.List Employees");
                Console.WriteLine("3.Update Employee");
                Console.WriteLine("4.Delete Employee");
                Console.WriteLine("5.Search Employee");
                Console.WriteLine("q.Exit");
                Console.Write("select an option: ");
                string option = Console.ReadLine();
                using (var connection = OpenConnection())
                {
                    switch (option)
                    {
                        case "1":
                            Console.Clear();
                            CreateEmployee(connection);
                            break;

                        case "2":
                            Console.Clear();
                            ListEmployees(connection);
                            break;

                        case "3":
                            Console.Clear();
                            UpdateEmployee(connection);
                            break;

                        case "4":
                            Console.Clear();
                            DeleteEmployee(connection);
                            break;

                        case "5":
                            Console.Clear();
                            DisplayEmployee(connection);
                            break;

                        case "q":

                            exit = true;
                            break;

                        default:

                            Console.WriteLine("Invalid option.Please try again.");
                            break;

                    }

                    if (!exit)
                    {
                        Console.WriteLine("\n Press Enter to continue....");
                        Console.ReadLine();
                    }
                }
            }
        }

        static SqliteConnection OpenConnection()
        {
            var connection = new SqliteConnection("Data Source=employee.db;");
            connection.Open();
            string CreateTableQuery = @"CREATE TABLE IF NOT EXISTS Employees(
                EmployeeID TEXT PRIMARY KEY,
                FirstName TEXT NOT NULL,
                LastName TEXT NOT NULL,
                DateOfBirth TEXT NOT NULL)";

            using (var command = new SqliteCommand(CreateTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
            return connection;
        }



        //create Employee
        static void CreateEmployee(SqliteConnection connection)
        {
            Console.Write("Enter Employee ID: ");
            string empID = Console.ReadLine();
            Console.Write("Enter First Name:");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name:");
            string lastName = Console.ReadLine();
            Console.Write("Enter Date of Birth (yyyy-mm-dd):");
            string dob = Console.ReadLine();

            Employee empobj = new Employee
            {
                EmployeeId = empID,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dob
            };

            bool result = EmployeeRepository.CreateEmployee(connection, empobj);
            if (result == true)
            {
                Console.WriteLine("Employee Created Successfully");
            }
            else
            {
                Console.WriteLine("failed to create Employee.");
            }
        }



        //get single Employee
        public static void DisplayEmployee(SqliteConnection connection)
        {
            Console.Write("Enter Employee ID to Search: ");
            string employeeId = Console.ReadLine();
            Employee emp = EmployeeRepository.GetEmployeeById(connection, employeeId);

            if (emp != null)
            {
                Console.WriteLine("\n----Employee Details----");
                Console.WriteLine($"ID: {emp.EmployeeId}\n Name: {emp.FirstName} {emp.LastName}\n DOB: {emp.DateOfBirth}");
            }
            else
            {
                Console.WriteLine($"Employee with ID {employeeId} not found");
            }
        }




        //list of All Employees
        static void ListEmployees(SqliteConnection connection)
        {
            List<Employee> emplist = EmployeeRepository.ListEmployees(connection);

            Console.WriteLine("\n----Employee List ----");
            foreach (Employee emp in emplist)
            {
                Console.WriteLine($"ID: {emp.EmployeeId}, Name: {emp.FirstName}{emp.LastName},DOB: {emp.DateOfBirth}");
            }


        }



        //Update Employee
        static void UpdateEmployee(SqliteConnection connection)
        {
            Console.Write("Enter Employee ID to update: ");
            string employeeID = Console.ReadLine();

            Employee existingemp = EmployeeRepository.GetEmployeeById(connection, employeeID);

            if (existingemp != null)
            {
                Console.Write("Enter New First Name: ");
                string newFirstName = Console.ReadLine().Trim();
                Console.Write("Enter New Last Name: ");
                string newLastName = Console.ReadLine();
                Console.Write("Enter New Date of Birth: ");
                string newDOB = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(newFirstName))
                {
                    existingemp.FirstName = newFirstName;
                }

                if (!string.IsNullOrEmpty(newLastName))
                {
                    existingemp.LastName = newLastName;
                }

                if (!string.IsNullOrEmpty(newDOB))
                {
                    existingemp.DateOfBirth = newDOB;
                }


                bool result = EmployeeRepository.UpdateEmployee(connection, existingemp);
                if (result == true)
                {
                    Console.WriteLine("Employee Updated Successfully");
                }
                else
                {
                    Console.WriteLine("Failed to Update Employee");
                }
            }
            else
            {
                Console.WriteLine($"Employee with ID {employeeId} not found");
            }



        }



        //Delete Employee
        static void DeleteEmployee(SqliteConnection connection)
        {
            Console.Write("Enter Employee ID to delete: ");
            string employeeID = Console.ReadLine();

            Employee existingemp = EmployeeRepository.GetEmployeeById(connection, employeeID);

            if (existingemp != null)
            {
                Employee employeeobj = EmployeeRepository.DeleteEmployee(connection, existingemp);
                if (employeeobj != null)
                {
                    Console.WriteLine("Employee Deleted Successfully");
                }
                else
                {
                    Console.WriteLine("failed to Deleted Employee");
                }

            }
            else
            {
                Console.WriteLine($"Employee with ID {employeeId} not found");
            }


        }
    }
}