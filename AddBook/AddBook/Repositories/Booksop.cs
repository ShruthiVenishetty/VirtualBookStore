using AddBook.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AddBook.Repositories
{
    public class Booksop : IBook
    {
        string str = @"Server=LTIN308457\SHRUTHI;Database=BooksList;User ID=sa;Password=Vlds@33333;";
            public string addBook(Books b)
            {
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString =str;
           
            SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "INSERT INTO Books(BookName,Author,Category,Subcategory,Publisher,Price,IsActive,Pages,AddedBy) Values (@BookName,@Author,@Category,@Subcategory,@Publisher,@Price,@IsActive,@Pages,@AddedBy)";
                sqlCmd.Connection = myConnection;

                sqlCmd.Parameters.AddWithValue("@BookName", b.BookName);
            sqlCmd.Parameters.AddWithValue("@Author", b.Author);
            sqlCmd.Parameters.AddWithValue("@Category", b.Category);
            sqlCmd.Parameters.AddWithValue("@Subcategory", b.Subcategory);
            sqlCmd.Parameters.AddWithValue("@Publisher",b.Publisher);
            sqlCmd.Parameters.AddWithValue("@Price", b.Price);
            sqlCmd.Parameters.AddWithValue("@IsActive",b.IsActive);
            sqlCmd.Parameters.AddWithValue("@Pages", b.Pages);
            sqlCmd.Parameters.AddWithValue("@AddedBy", b.AddedBy);
            myConnection.Open();
                int rowInserted = sqlCmd.ExecuteNonQuery();
                myConnection.Close();
            return "success";
        }

           
        
        public Books Viewbook(string name)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = str;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Books where BookName='" + name + "'";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Books b = null;
            while (reader.Read())
            {
                b = new Books();
                b.BookName = reader.GetValue(0).ToString();
                b.Author = reader.GetValue(1).ToString();
                b.Category = reader.GetValue(2).ToString();
                b.Subcategory = reader.GetValue(3).ToString();
                b.Publisher = reader.GetValue(4).ToString();
                b.Price = Convert.ToDecimal(reader.GetValue(5));
                b.IsActive = Convert.ToBoolean(reader.GetValue(6));
                b.Pages = Convert.ToInt32(reader.GetValue(7));
                b.AddedBy = reader.GetValue(8).ToString();
                b.AddedOn = Convert.ToDateTime(reader.GetValue(10));
                if (!(reader.GetValue(9) is DBNull))
                {
                    b.ModifiedBy = reader.GetValue(9).ToString();
                    b.LastModifiedOn = Convert.ToDateTime(reader.GetValue(11));
                }

            }
            
            myConnection.Close();
            return b;
        }

        public List<Books> viewAllBooks()
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = str;

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Books where IsActive = 1";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Books b = null;
            List<Books> li = new List<Books>();
            while (reader.Read())
            {
                b = new Books();
                b.BookName = reader.GetValue(0).ToString();
                b.Author = reader.GetValue(1).ToString();
                b.Category = reader.GetValue(2).ToString();
                b.Subcategory = reader.GetValue(3).ToString();
                b.Publisher = reader.GetValue(4).ToString();
                b.Price = Convert.ToDecimal(reader.GetValue(5));
                b.IsActive = Convert.ToBoolean(reader.GetValue(6));
                b.Pages = Convert.ToInt32(reader.GetValue(7));
                b.AddedBy= reader.GetValue(8).ToString();
                b.AddedOn = Convert.ToDateTime(reader.GetValue(10));
                if(!(reader.GetValue(9) is DBNull) || !(reader.GetValue(11) is DBNull))
                {
                    b.ModifiedBy = reader.GetValue(9).ToString();
                    b.LastModifiedOn = Convert.ToDateTime(reader.GetValue(11));
                }
                
                li.Add(b);
                
            }
           
            myConnection.Close();
            return li;
        }

        public List<Books> DelistedBooks()
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = str; 

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Books where IsActive = 0";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Books b = null;
            List<Books> li = new List<Books>();
            while (reader.Read())
            {
                b = new Books();
                b.BookName = reader.GetValue(0).ToString();
                b.Author = reader.GetValue(1).ToString();
                b.Category = reader.GetValue(2).ToString();
                b.Subcategory = reader.GetValue(3).ToString();
                b.Publisher = reader.GetValue(4).ToString();
                b.Price = Convert.ToDecimal(reader.GetValue(5));
                b.IsActive = Convert.ToBoolean(reader.GetValue(6));
                b.Pages = Convert.ToInt32(reader.GetValue(7));
                b.AddedBy = reader.GetValue(8).ToString();
                b.AddedOn = Convert.ToDateTime(reader.GetValue(10));
                if (!(reader.GetValue(9) is DBNull) || !(reader.GetValue(11) is DBNull))
                {
                    b.ModifiedBy = reader.GetValue(9).ToString();
                    b.LastModifiedOn = Convert.ToDateTime(reader.GetValue(11));
                }

                li.Add(b);

            }

            myConnection.Close();
            
            return li;

        }

        public string DeleteBook(string name)
        {
            Books b = Viewbook(name);
            if (b == null)
            {
                return "Failure";
            }
            else
            {

                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = str; 

                SqlCommand sqlCmd = new SqlCommand();

                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "DELETE FROM Books WHERE BookName='" + name + "'";
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                reader = sqlCmd.ExecuteReader();
                myConnection.Close();
                return "success";
            }

        }

        public string DelistBook(string name)
        {
            Books b = Viewbook(name);
            if (b == null)
            {
                return "Failure";
            }
            else
            {
                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = str; 

                SqlCommand sqlCmd = new SqlCommand();

                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "UPDATE Books SET IsActive = 0 WHERE BookName='" + name + "'";
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                reader = sqlCmd.ExecuteReader();
                myConnection.Close();

                return "success";
            }
        }

        public string EnlistBook(string name)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = str; 

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "UPDATE Books SET IsActive = 1 WHERE BookName='" + name + "'";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            myConnection.Close();
            return "success";
        }
          public string EditBookDetails(string name,Books b)
        {
            
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = str;
            SqlCommand sqlCmd = new SqlCommand("Update Books set Author=@Author,Category=@Category,Subcategory=@Subcategory,Publisher=@Publisher,Price=@Price,IsActive=@IsActive,Pages=@Pages,ModifiedBy=@ModifiedBy where BookName='" + name + "'", myConnection);
            sqlCmd.Connection = myConnection;

            sqlCmd.Parameters.AddWithValue("@BookName", b.BookName);
            sqlCmd.Parameters.AddWithValue("@Author", b.Author);
            sqlCmd.Parameters.AddWithValue("@Category", b.Category);
            sqlCmd.Parameters.AddWithValue("@Subcategory", b.Subcategory);
            sqlCmd.Parameters.AddWithValue("@Publisher", b.Publisher);
            sqlCmd.Parameters.AddWithValue("@Price", b.Price);
            sqlCmd.Parameters.AddWithValue("@IsActive", b.IsActive);
            sqlCmd.Parameters.AddWithValue("@Pages", b.Pages);
            sqlCmd.Parameters.AddWithValue("@ModifiedBy", b.ModifiedBy);
            myConnection.Open();
            int i = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
            return "success";
        }
    }
}
