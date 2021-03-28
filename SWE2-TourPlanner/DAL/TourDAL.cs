using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.DAL
{
    public class TourDAL
    {
        private string _connectionString;

        public TourDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Tour> GetTours()
        {
            using NpgsqlConnection con = new NpgsqlConnection(_connectionString);
            try
            {
                con.Open();
            }
            catch (NpgsqlException e)
            {
                Debug.WriteLine("No DB connection");
                return new List<Tour>();
            }

            string sql = "SELECT * FROM tours";
            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            List<Tour> tours = new List<Tour>();
            while (rdr.Read())
            {
                tours.Add(new Tour(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3)));
            }

            return tours;
        }

        public void AddTour(Tour addedTour)
        {
            using NpgsqlConnection con = new NpgsqlConnection(_connectionString);
            try
            {
                con.Open();
            }
            catch (NpgsqlException e)
            {
                Debug.WriteLine("No DB connection");
            }

            try
            {
                string sql = "INSERT INTO tours (tourname, description, tourstart, tourend) VALUES (@tourname, @description, @tourstart, @tourend)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("tourname", addedTour.Name);
                    cmd.Parameters.AddWithValue("description", addedTour.Description);
                    cmd.Parameters.AddWithValue("tourstart", addedTour.Start);
                    cmd.Parameters.AddWithValue("tourend", addedTour.End);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
        }
    }
}
