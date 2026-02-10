using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace VideoAssetManager.DBUP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the server name: ");
            string str = Console.ReadLine();
            Console.WriteLine("Please enter the database name: ");
            string str2 = Console.ReadLine();
            Console.WriteLine("Please enter the user name : ");
            string str3 = Console.ReadLine();
            Console.WriteLine("Please enter the password: ");
            string str4 = Console.ReadLine();
            Console.WriteLine("Alter database y/n: ");
            bool flag = Console.ReadLine() == "y";
            string[] strArray = new string[] { "data source=", str, ";user = ", str3, ";password=", str4, ";" };
            SqlConnection connection = new SqlConnection(string.Concat(strArray));
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.Text
            };
            //if (flag)
            //{
            //    command.CommandText = "CREATE DATABASE " + str2 + ";";
            //    command.ExecuteNonQuery();
            //}
            command.CommandText = "USE " + str2 + ";";
            command.ExecuteNonQuery();
            DataSet set = new DataSet();
            set.ReadXml("db.xml");
            int count = set.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(set.Tables[0].Rows[i]["file"]);
                StreamReader reader = File.OpenText(@"Scripts\" + set.Tables[0].Rows[i]["file"]);
                string str6 = reader.ReadToEnd();
                reader.Close();
                command.CommandText = str6;
                command.ExecuteNonQuery();
            }
            connection.Close();
            Console.WriteLine("\n\nDatabase Update complete.\n Press enter to quit.");
            Console.ReadLine();
        }
    }
}
