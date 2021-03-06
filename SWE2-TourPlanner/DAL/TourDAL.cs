﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Npgsql;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.DAL
{
    public class TourDal : ITourDal
    {
        private string _connectionString;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TourDal(string connectionString)
        {
            _connectionString = connectionString;
            log4net.Config.XmlConfigurator.Configure();
        }

        public List<IElement> GetTours()
        {
            using NpgsqlConnection con = new NpgsqlConnection(_connectionString);
            try
            {
                con.Open();
            }
            catch (NpgsqlException e)
            {
                _log.Error("No DB connection");
                return null;
            }

            string createSql = @"CREATE TABLE IF NOT EXISTS tours(tourid VARCHAR, tourname VARCHAR, description VARCHAR, tourstart VARCHAR, tourend VARCHAR, distance DOUBLE PRECISION)";
            using NpgsqlCommand createCmd = new NpgsqlCommand(createSql, con);
            createCmd.ExecuteNonQuery();

            string sql = "SELECT * FROM tours";
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

            List<IElement> tours = new List<IElement>();
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            { 
                tours.Add(new Tour(Guid.Parse(rdr.GetString(0)), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4), rdr.GetDouble(5)));
            }
            rdr.Close();
            _log.Info("Tours read from DB");
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
                _log.Error("No DB connection");
            }

            try
            {
                string sql = "INSERT INTO tours (tourid, tourname, description, tourstart, tourend, distance) VALUES (@tourid, @tourname, @description, @tourstart, @tourend, @distance)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("tourid", addedTour.Id.ToString());
                    cmd.Parameters.AddWithValue("tourname", addedTour.Name);
                    cmd.Parameters.AddWithValue("description", addedTour.Description);
                    cmd.Parameters.AddWithValue("tourstart", addedTour.Start);
                    cmd.Parameters.AddWithValue("tourend", addedTour.End);
                    cmd.Parameters.AddWithValue("distance", addedTour.Distance);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                _log.Info("Tour created on DB");
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
                _log.Error("No DB connection");
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
                _log.Info("Tour deleted from DB");
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
                _log.Error("No DB connection");
            }

            try
            {
                string sql = "UPDATE tours SET tourid = @tourid, tourname = @tourname, description = @description, tourstart = @tourstart, tourend = @tourend, distance = @distance WHERE tourid = @tourid";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("tourid", editedTour.Id.ToString());
                    cmd.Parameters.AddWithValue("tourname", editedTour.Name);
                    cmd.Parameters.AddWithValue("description", editedTour.Description);
                    cmd.Parameters.AddWithValue("tourstart", editedTour.Start);
                    cmd.Parameters.AddWithValue("tourend", editedTour.End);
                    cmd.Parameters.AddWithValue("distance", editedTour.Distance);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                _log.Info("Tour edited on DB");
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
        }
    }
}
