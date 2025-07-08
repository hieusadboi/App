using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAO
{
    internal class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return instance; }
            private set { instance = value; }
        }

        private string connectionString = "Data Source=.\\;Initial Catalog=QuanLyQuanAn;Integrated Security=True;Encrypt=False";

        public DataTable ExecuteQuery( string query, object[] parameter = null )
        {
            DataTable data = new DataTable();

            using ( SqlConnection connection = new SqlConnection( connectionString ) )
            {
                connection.Open();

                SqlCommand command = new SqlCommand( query, connection );

                if ( parameter != null )
                {
                    string[] listParameter = query.Split( ' ' );

                    int i = 0;

                    foreach ( string item in listParameter )
                    {
                        if ( item.Contains( '@' ) )
                        {
                            command.Parameters.AddWithValue( item, parameter[i] );
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter( command );

                adapter.Fill( data ); // Fill the DataTable with the result of the query

                connection.Close();
            }

            return data;
        }

        public int ExecuteNonQuery( string query, object[] parameter = null )
        { 
            int data = 0;
            using ( SqlConnection connection = new SqlConnection( connectionString ) )
            {
                connection.Open();
                SqlCommand command = new SqlCommand( query, connection );
                if ( parameter != null )
                {
                    string[] listParameter = query.Split( ' ' );
                    int i = 0;
                    foreach ( string item in listParameter )
                    {
                        if ( item.Contains( '@' ) )
                        {
                            command.Parameters.AddWithValue( item, parameter[i] );
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();  // ExecuteNonQuery returns the number of rows affected by the command
                connection.Close();
            }
            return data;
        }

        public object ExecuteScalar( string query, object[] parameter = null )
        {
            object data = null;
            using ( SqlConnection connection = new SqlConnection( connectionString ) )
            {
                connection.Open();
                SqlCommand command = new SqlCommand( query, connection );
                if ( parameter != null )
                {
                    string[] listParameter = query.Split( ' ' );
                    int i = 0;
                    foreach ( string item in listParameter )
                    {
                        if ( item.Contains( '@' ) )
                        {
                            command.Parameters.AddWithValue( item, parameter[i] );
                            i++;
                        }
                    }
                }
                data = command.ExecuteScalar(); // ExecuteScalar returns the first column of the first row in the result set
                connection.Close();
            }
            return data;
        }



    }
}
