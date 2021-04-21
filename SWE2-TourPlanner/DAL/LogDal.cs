﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.DAL
{
    public class LogDal : ILogDal
    {
        private string _connectionString;

        public LogDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Log> GetLogs()
        {
            using NpgsqlConnection con = new NpgsqlConnection(_connectionString);
            try
            {
                con.Open();
            }
            catch (NpgsqlException e)
            {
                Debug.WriteLine("No DB connection");
                return new List<Log>();
            }

            // get tour names
            string sqlTours = "SELECT tourid, tourname FROM tours";
            using NpgsqlCommand cmdTours = new NpgsqlCommand(sqlTours, con);

            Dictionary<string, string> tourIdName = new Dictionary<string, string>();
            using NpgsqlDataReader rdrTours = cmdTours.ExecuteReader();

            while (rdrTours.Read())
            {
                tourIdName.Add(rdrTours.GetString(0), rdrTours.GetString(1));
            }
            rdrTours.Close();


            // get logs
            string sql = "SELECT * FROM logs";
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

            List<Log> logs = new List<Log>();
            string tourId, tourName;
            Rating rating;
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                tourId = rdr.GetString(6);
                tourIdName.TryGetValue(tourId, out tourName);
                Enum.TryParse(rdr.GetString(9), out rating);
                logs.Add(new Log(Guid.Parse(rdr.GetString(0)), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4),
                    rdr.GetTimeStamp(5).ToDateTime(),
                    Guid.Parse(tourId), tourName,
                    rdr.GetDouble(7), rdr.GetDouble(8), rating));
            }
            rdr.Close();
            return logs;
        }

        public void AddLog(Log addedLog)
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
                string sql = "INSERT INTO logs (logid, logname, description, report, vehicle, datetime, tourid, distance, totaltime, rating)" +
                             "VALUES (@logid, @logname, @description, @report, @vehicle, @datetime, @tourid, @distance, @totaltime, @rating)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("logid", addedLog.Id.ToString());
                    cmd.Parameters.AddWithValue("logname", addedLog.Name);
                    cmd.Parameters.AddWithValue("description", addedLog.Description);
                    cmd.Parameters.AddWithValue("report", addedLog.Report);
                    cmd.Parameters.AddWithValue("vehicle", addedLog.Vehicle);
                    cmd.Parameters.AddWithValue("datetime", addedLog.DateTime);
                    cmd.Parameters.AddWithValue("tourId", addedLog.TourId);
                    cmd.Parameters.AddWithValue("distance", addedLog.Distance);
                    cmd.Parameters.AddWithValue("totaltime", addedLog.TotalTime);
                    cmd.Parameters.AddWithValue("rating", addedLog.Rating.ToString());
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
