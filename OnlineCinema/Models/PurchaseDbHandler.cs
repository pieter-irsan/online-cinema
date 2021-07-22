using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OnlineCinema.Models
{
    public class PurchaseDbHandler
    {
        private SqlConnection cinemaDb;
        private void connection()
        {
            cinemaDb = new SqlConnection(ConfigurationManager.ConnectionStrings["CinemaDB"].ToString());
        }

        public bool AddPurchase(PurchaseModel purchase)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddPurchase", cinemaDb);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", purchase.Username);
            cmd.Parameters.AddWithValue("@Title", purchase.Title);
            cmd.Parameters.AddWithValue("@Price", purchase.Price);
            cmd.Parameters.AddWithValue("@Time", purchase.Time);

            cinemaDb.Open();
            int i = cmd.ExecuteNonQuery();
            cinemaDb.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }

        public List<PurchaseModel> GetPurchase()
        {
            connection();
            List<PurchaseModel> purchaseList = new List<PurchaseModel>();
            SqlCommand cmd = new SqlCommand("GetPurchase", cinemaDb);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();

            cinemaDb.Open();
            adapter.Fill(dataTable);
            cinemaDb.Close();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                purchaseList.Add(
                    new PurchaseModel
                    {
                        Id = Convert.ToInt32(dataRow["Id"]),
                        Username = Convert.ToString(dataRow["Username"]),
                        Title = Convert.ToString(dataRow["Title"]),
                        Price = Convert.ToInt32(dataRow["Price"]),
                        Time = Convert.ToDateTime(dataRow["Time"])
                    });
            }
            return purchaseList;
        }

        public List<PurchaseModel> SearchPurchase(string param)
        {
            connection();
            List<PurchaseModel> purchaseList = new List<PurchaseModel>();
            SqlCommand cmd = new SqlCommand("SearchPurchase", cinemaDb);
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
                    purchaseList.Add(
                        new PurchaseModel
                        {
                            Id = Convert.ToInt32(dataRow["Id"]),
                            Username = Convert.ToString(dataRow["Username"]),
                            Title = Convert.ToString(dataRow["Title"]),
                            Price = Convert.ToInt32(dataRow["Price"]),
                            Time = Convert.ToDateTime(dataRow["Time"])
                        });
                }
                return purchaseList;
            }
            else
                return null;
        }

        public string Revenue()
        {
            connection();
            string query = "SELECT SUM(Price) FROM Purchase";
            using (IDbCommand command = new SqlCommand(query, cinemaDb))
            {
                cinemaDb.Open();
                object result = command.ExecuteScalar();
                cinemaDb.Close();
                return Convert.ToString(result);
            }
        }
    }
}