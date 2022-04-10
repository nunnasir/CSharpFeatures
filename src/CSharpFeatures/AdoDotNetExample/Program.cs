using System;
using System.Collections.Generic;

namespace AdoDotNetExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Database and Table Schema
            string connectionString = "Server=DESKTOP-H28I08B;Database=csharpb9;User Id=csharpb9;Password=123456";

            //DeleteData(connectionString);
            //InsertData(connectionString);
            GetData(connectionString);
            //UpdateData(connectionString);
        }

        public static void InsertData(string connectionString)
        {
            DataUtility utility = new(connectionString);
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Birthday: ");
            string birthDate = Console.ReadLine();

            string query = "INSERT INTO STUDENT([Name], DateOfBirth) VALUES(@name, @dateOfBirth)";

            List<(string, object)> parameters = new()
            {
                ("name", name),
                ("dateOfBirth", birthDate),
            };

            utility.ExecuteCommand(query, parameters);
        }

        public static void UpdateData(string connectionString)
        {
            DataUtility dataUtility = new DataUtility(connectionString);

            Console.Write("Your Updated Name: ");
            string updateName = Console.ReadLine();

            Console.Write("Your Updated Value Id: ");
            string id = Console.ReadLine();

            string updateQuery = "UPDATE STUDENT SET [Name] = @name WHERE id = @id";

            List<(string, object)> parameters = new()
            {
                ("name", updateName),
                ("id", id)
            };

            dataUtility.ExecuteCommand(updateQuery, parameters);
        }

        public static void DeleteData(string connectionString)
        {
            DataUtility dataUtility = new DataUtility(connectionString);

            string deleteQuery = "DELETE FROM STUDENT";

            dataUtility.ExecuteCommand(deleteQuery);
        }

        public static void GetData(string connectionString)
        {
            DataUtility dataUtility = new DataUtility(connectionString);
            string selectQuery = "SELECT * FROM STUDENT";

            var result = dataUtility.GetDatas(selectQuery, null);
            //var result = dataUtility.GetDatas(selectQuery);

            foreach (var item in result[0])
            {
                Console.Write(item.Key + "\t");
            }
            Console.WriteLine();
            foreach (var rows in result)
            {
                foreach (var column in rows.Values)
                {
                    Console.Write(column + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
