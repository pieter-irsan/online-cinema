using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OnlineCinema.Models
{
    public class MovieDbHandler
    {
        private SqlConnection cinemaDb;
        private void connection()
        {
            cinemaDb = new SqlConnection(ConfigurationManager.ConnectionStrings["CinemaDB"].ToString());
        }

        public bool AddMovie(MovieModel movie)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddMovie", cinemaDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Title", movie.Title);
            cmd.Parameters.AddWithValue("@Director", movie.Director);
            cmd.Parameters.AddWithValue("@Synopsis", movie.Synopsis);
            cmd.Parameters.AddWithValue("@Price", movie.Price);
            cmd.Parameters.AddWithValue("@PosterPath", movie.PosterPath);
            cmd.Parameters.AddWithValue("@Trailer", movie.Trailer);

            cinemaDb.Open();
            int i = cmd.ExecuteNonQuery();
            cinemaDb.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }

        public List<MovieModel> GetMovie()
        {
            connection();
            List<MovieModel> movieList = new List<MovieModel>();
            SqlCommand cmd = new SqlCommand("GetMovie", cinemaDb);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();

            cinemaDb.Open();
            adapter.Fill(dataTable);
            cinemaDb.Close();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                movieList.Add(
                    new MovieModel
                    {
                        Id = Convert.ToInt32(dataRow["Id"]),
                        Title = Convert.ToString(dataRow["Title"]),
                        Director = Convert.ToString(dataRow["Director"]),
                        Synopsis = Convert.ToString(dataRow["Synopsis"]),
                        Price = Convert.ToInt32(dataRow["Price"]),
                        PosterPath = Convert.ToString(dataRow["PosterPath"]),
                        Trailer = Convert.ToString(dataRow["Trailer"])
                    });
            }
            return movieList;
        }

        public bool EditMovie(MovieModel movie)
        {
            connection();
            SqlCommand cmd = new SqlCommand("EditMovie", cinemaDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", movie.Id);
            cmd.Parameters.AddWithValue("@Title", movie.Title);
            cmd.Parameters.AddWithValue("@Director", movie.Director);
            cmd.Parameters.AddWithValue("@Synopsis", movie.Synopsis);
            cmd.Parameters.AddWithValue("@Price", movie.Price);
            cmd.Parameters.AddWithValue("@PosterPath", movie.PosterPath);
            cmd.Parameters.AddWithValue("@Trailer", movie.Trailer);

            cinemaDb.Open();
            int i = cmd.ExecuteNonQuery();
            cinemaDb.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeleteMovie(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteMovie", cinemaDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            cinemaDb.Open();
            int i = cmd.ExecuteNonQuery();
            cinemaDb.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }

        public List<MovieModel> SearchMovie(string param)
        {
            connection();
            List<MovieModel> movieList = new List<MovieModel>();
            SqlCommand cmd = new SqlCommand("SearchMovie", cinemaDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@param", param);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();

            cinemaDb.Open();
            int i = adapter.Fill(dataTable);
            cinemaDb.Close();
            if (i >= 1)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    movieList.Add(
                        new MovieModel
                        {
                            Id = Convert.ToInt32(dataRow["Id"]),
                            Title = Convert.ToString(dataRow["Title"]),
                            Director = Convert.ToString(dataRow["Director"]),
                            Synopsis = Convert.ToString(dataRow["Synopsis"]),
                            Price = Convert.ToInt32(dataRow["Price"]),
                            PosterPath = Convert.ToString(dataRow["PosterPath"]),
                            Trailer = Convert.ToString(dataRow["Trailer"])
                        });
                }
                return movieList;
            }
            else
                return null;
        }

        public List<MovieModel> UserMovie(string username)
        {
            connection();
            List<MovieModel> movieList = new List<MovieModel>();
            SqlCommand cmd = new SqlCommand("UserMovie", cinemaDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();

            cinemaDb.Open();
            adapter.Fill(dataTable);
            cinemaDb.Close();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                movieList.Add(
                    new MovieModel
                    {
                        Id = Convert.ToInt32(dataRow["Id"]),
                        Title = Convert.ToString(dataRow["Title"]),
                        Director = Convert.ToString(dataRow["Director"]),
                        Synopsis = Convert.ToString(dataRow["Synopsis"]),
                        PosterPath = Convert.ToString(dataRow["PosterPath"]),
                        Trailer = Convert.ToString(dataRow["Trailer"])
                    });
            }
            return movieList;
        }
    }
}