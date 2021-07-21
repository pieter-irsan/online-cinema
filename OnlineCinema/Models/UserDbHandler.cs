using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OnlineCinema.Models
{
    public class UserDbHandler
    {
        private SqlConnection cinemaDb;
        private void connection()
        {
            cinemaDb = new SqlConnection(ConfigurationManager.ConnectionStrings["CinemaDB"].ToString());
        }

        public bool RegisterUser(UserModel user)
        {
            connection();
            SqlCommand cmd = new SqlCommand("RegisterUser", cinemaDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);

            cinemaDb.Open();
            int i = cmd.ExecuteNonQuery();
            cinemaDb.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool LoginUser(UserModel user)
        {
            connection();
            SqlCommand cmd = new SqlCommand("LoginUser", cinemaDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);

            cinemaDb.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            try
            {
                if (dataReader.Read())
                    return true;
                else
                    return false;
            }
            finally
            {
                cinemaDb.Close();
            }
        }

        public bool LoginAdmin(AdminModel admin)
        {
            connection();
            SqlCommand cmd = new SqlCommand("LoginAdmin", cinemaDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", admin.Username);
            cmd.Parameters.AddWithValue("@Password", admin.Password);

            cinemaDb.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            try
            {
                if (dataReader.Read())
                    return true;
                else
                    return false;
            }
            finally
            {
                cinemaDb.Close();
            }
        }
    }
}