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
    public class TourDal : ITourDal
    {
        private string _connectionString;

        public TourDal(string connectionString)
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
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

            List<Tour> tours = new List<Tour>();
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            { 
                tours.Add(new Tour(Guid.Parse(rdr.GetString(0)), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4)));
            }
            rdr.Close();
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
                string sql = "INSERT INTO tours (tourid, tourname, description, tourstart, tourend) VALUES (@tourid, @tourname, @description, @tourstart, @tourend)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("tourid", addedTour.Id.ToString());
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

        public void DeleteTour(Tour deletedTour)
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
                string sql = "DELETE FROM tours WHERE tourid = @tourid";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("tourid", deletedTour.Id.ToString());
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                sql = "DELETE FROM logs WHERE tourid = @tourid";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("tourid", deletedTour.Id.ToString());
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
        }

        public void EditTour(Tour editedTour)
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
                string sql = "UPDATE tours SET tourid = @tourid, tourname = @tourname, description = @description, tourstart = @tourstart, tourend = @tourend WHERE tourid = @tourid";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("tourid", editedTour.Id.ToString());
                    cmd.Parameters.AddWithValue("tourname", editedTour.Name);
                    cmd.Parameters.AddWithValue("description", editedTour.Description);
                    cmd.Parameters.AddWithValue("tourstart", editedTour.Start);
                    cmd.Parameters.AddWithValue("tourend", editedTour.End);
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
