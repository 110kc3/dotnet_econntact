using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_windowsFormsApp.EcontactClasses
{
    class contactClass
    {
        //Getter setter properites - data carrier
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }


      
        static String myconn_string = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        //selecting data from database
        public DataTable Select()
        {
            //Database connection
            SqlConnection conn = new SqlConnection(myconn_string);
            DataTable dt = new DataTable();
            try
            {
                //writing sql querry
                string sql = "SELECT * FROM table_econntact";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);

            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            return dt;
        }

        //Inserting data into database
        public bool Insert(contactClass contact)
        {
            //Creating default return type 
            bool isSuccess = false;

            //Connecting database
            SqlConnection sql_conn = new SqlConnection(myconn_string);
            try
            {
                //Create sql querry to insert data
                string sql = "INSERT INTO table_econntact (FirstName, LastName, ContactNumber, Address, Gender) VALUES(@FirstName, @LastName, @ContactNumber, @Address, @Gender)";

                //Creating SQL command using sql and sql_conn
                SqlCommand cmd = new SqlCommand(sql, sql_conn);
                //create parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", contact.FirstName);
                cmd.Parameters.AddWithValue("@LastName", contact.LastName);
                cmd.Parameters.AddWithValue("@ContactNumber", contact.ContactNumber);
                cmd.Parameters.AddWithValue("@Address", contact.Address);
                cmd.Parameters.AddWithValue("@Gender", contact.Gender);

                //connection open
                sql_conn.Open();
                int rows = cmd.ExecuteNonQuery();

                //if querry runs successfully - value will be grater than 0
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

                
            }
            finally
            {
                sql_conn.Close();
            }
            return isSuccess;

        }


        //method to update data in database
        public bool Update(contactClass contact)
        {
            //create a default return value
            bool isSuccess = false;
            //Connecting database
            SqlConnection sql_conn = new SqlConnection(myconn_string);

            try
            {
                //sql querry to update
                string sql = "UPDATE table_econntact SET FirstName=@FirstName, LastName=@LastName, ContactNumber=@ContactNumber, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";

                //Creating SQL command using sql and sql_conn
                SqlCommand cmd = new SqlCommand(sql, sql_conn);

                //create parameters to add value
                cmd.Parameters.AddWithValue("@FirstName", contact.FirstName);
                cmd.Parameters.AddWithValue("@LastName", contact.LastName);
                cmd.Parameters.AddWithValue("@ContactNumber", contact.ContactNumber);
                cmd.Parameters.AddWithValue("@Address", contact.Address);
                cmd.Parameters.AddWithValue("@Gender", contact.Gender);
                cmd.Parameters.AddWithValue("ContactID", contact.ContactID);

                //connection open
                sql_conn.Open();
                int rows = cmd.ExecuteNonQuery();

                //if querry runs successfully - value will be grater than 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {

               
            }
            finally
            {
                sql_conn.Close();
            }
            return isSuccess;

        }

        //method to delete data from database
        public bool Delete(contactClass contact)
        {
            //create a default return value
            bool isSuccess = false;

            //Connecting database
            SqlConnection sql_conn = new SqlConnection(myconn_string);

            try
            {
                //sql querry to DELETE DATA
                string sql = "DELETE FROM table_econntact WHERE ContactID=@ContactID";

                //Creating SQL command using sql and sql_conn
                SqlCommand cmd = new SqlCommand(sql, sql_conn);

                //create parameters to add value
                cmd.Parameters.AddWithValue("@ContactID", contact.ContactID);

                //connection open
                sql_conn.Open();
                int rows = cmd.ExecuteNonQuery();

                //if querry runs successfully - value will be grater than 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {


            }
            finally
            {
                sql_conn.Close();
            }
            return isSuccess;
        }
        }
}
