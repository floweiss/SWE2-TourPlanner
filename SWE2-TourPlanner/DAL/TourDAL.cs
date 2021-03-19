using System;
using System.Collections.Generic;
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
            using var con = new NpgsqlConnection(_connectionString);
            con.Open();

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
    }
}
