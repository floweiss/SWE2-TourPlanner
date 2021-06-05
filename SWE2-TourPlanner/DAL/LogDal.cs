using System;
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
    public class LogDal : ILogDal
    {
        private string _connectionString;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public LogDal(string connectionString)
        {
            _connectionString = connectionString;
            log4net.Config.XmlConfigurator.Configure();
        }

        public List<IElement> GetLogs()
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

            // get tour names
            string createSqlTours = @"CREATE TABLE IF NOT EXISTS tours(tourid VARCHAR, tourname VARCHAR, description VARCHAR, tourstart VARCHAR, tourend VARCHAR, distance DOUBLE PRECISION)";
            using NpgsqlCommand createCmdTours = new NpgsqlCommand(createSqlTours, con);
            createCmdTours.ExecuteNonQuery();

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
            string createSql = @"CREATE TABLE IF NOT EXISTS logs(logid VARCHAR, logname VARCHAR, description VARCHAR, report VARCHAR, vehicle VARCHAR, datetime TIMESTAMP, tourid VARCHAR, distance DOUBLE PRECISION, totaltime DOUBLE PRECISION, rating VARCHAR)";
            using NpgsqlCommand createCmd = new NpgsqlCommand(createSql, con);
            createCmd.ExecuteNonQuery();

            string sql = "SELECT * FROM logs";
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

            List<IElement> logs = new List<IElement>();
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
            _log.Info("Logs read from DB");
            return logs;
        }


        public List<Log> GetLogsForTour(Tour tour)
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

            string sql = "SELECT * FROM logs";
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

            List<Log> logs = new List<Log>();
            string tourId;
            Rating rating;
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                tourId = rdr.GetString(6);
                Enum.TryParse(rdr.GetString(9), out rating);
                if (tourId == tour.Id.ToString())
                {
                    logs.Add(new Log(Guid.Parse(rdr.GetString(0)), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4),
                        rdr.GetTimeStamp(5).ToDateTime(),
                        Guid.Parse(tourId), tour.Name,
                        rdr.GetDouble(7), rdr.GetDouble(8), rating));
                }
            }
            rdr.Close();
            _log.Info("Logs read for one Tour from DB");
            return logs;
        }

        public Log GetLogById(string logId)
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

            Log log = null;
            string tourId, tourName;
            Rating rating;
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                tourId = rdr.GetString(6);
                tourIdName.TryGetValue(tourId, out tourName);
                Enum.TryParse(rdr.GetString(9), out rating);
                if (rdr.GetString(0) == logId)
                {
                    log = new Log(Guid.Parse(rdr.GetString(0)), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4),
                        rdr.GetTimeStamp(5).ToDateTime(),
                        Guid.Parse(tourId), tourName,
                        rdr.GetDouble(7), rdr.GetDouble(8), rating);
                    break;
                }
            }
            rdr.Close();
            _log.Info("Log by ID read from DB");
            return log;
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
                _log.Error("No DB connection");
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
                _log.Info("Log created on DB");
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
        }

        public void DeleteLog(string logId)
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
                string sql = "DELETE FROM logs WHERE logid = @logid";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("logId", logId);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                _log.Info("Log deleted from DB");
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
        }

        public void EditLog(Log editedLog)
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
                string sql = "UPDATE logs SET logid = @logid, logname = @logname, description = @description, report = @report, vehicle = @vehicle, datetime = @datetime, tourid = @tourid," +
                             "distance = @distance, totaltime = @totaltime, rating = @rating WHERE logid = @logid";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("logid", editedLog.Id.ToString());
                    cmd.Parameters.AddWithValue("logname", editedLog.Name);
                    cmd.Parameters.AddWithValue("description", editedLog.Description);
                    cmd.Parameters.AddWithValue("report", editedLog.Report);
                    cmd.Parameters.AddWithValue("vehicle", editedLog.Vehicle);
                    cmd.Parameters.AddWithValue("datetime", editedLog.DateTime);
                    cmd.Parameters.AddWithValue("tourId", editedLog.TourId);
                    cmd.Parameters.AddWithValue("distance", editedLog.Distance);
                    cmd.Parameters.AddWithValue("totaltime", editedLog.TotalTime);
                    cmd.Parameters.AddWithValue("rating", editedLog.Rating.ToString());
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                _log.Info("Log edited on DB");
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
        }
    }
}
