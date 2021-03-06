﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Pathe
{
    [ExcludeFromCodeCoverage]
    public sealed class Database
    {
        #region Fields

        private string username;
        private string password;
        private string host;
        private OracleConnection connect = new OracleConnection();

        // Singleton
        private static readonly Lazy<Database> instance =
            new Lazy<Database>(() => new Database());

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize the connection with the database
        /// </summary>
        private Database()
        {
            Initialize();
        }

        #endregion

        #region Properties

        public static Database Instance { get { return instance.Value; } }

        #endregion

        #region Methods - Connection

        private void Initialize()
        {
            username = "pathe";
            password = "pathe";
            host = "localhost/xe";

            string connectionstring = string.Format("Data Source= {0};User ID={1};Password={2};", host, username,
                password);

            try
            {
                connect = new OracleConnection(connectionstring);
            }
            catch (OracleException ex)
            {
                System.Diagnostics.Debug.Write("Couldn't create connectionstring!");
                System.Diagnostics.Debug.Write(ex);
                throw new Exception("There was an error to make a new connectionstring!\nCode: " + ex.ErrorCode +
                                    "\nMessage: " + ex.Message);
            }

        }

        /// <summary>
        /// Try to open the connection with the database
        /// </summary>
        /// <returns>Returns a bool, true = connection success, false = connection failed</returns>
        public void Open_Connection()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Opening Database Connection");
                connect.Open();
            }
            catch (OracleException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                if (ex.Message == "Connection is already open")
                {
                    return;
                }
                throw;
            }
        }

        /// <summary>
        /// Try to close the connection with the database
        /// </summary>
        /// <returns>Return a bool, true = connection close success, false = connection close failed</returns>
        public bool Close_Connection()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Closing Database Connection");
                connect.Close();
                System.Diagnostics.Debug.WriteLine("---------------");
                return true;
            }
            catch (OracleException ex)
            {
                string error = "ERROR DETECTED!\nCode: " + ex.ErrorCode + "\nMessage: " + ex.Message;
                System.Diagnostics.Debug.WriteLine(error);
                return false;
            }
        }

        #endregion

        #region Methods - Select

        public List<Dictionary<string, object>> GetFilmInfo(int filmID)
        {
            OracleCommand cmd = new OracleCommand("SELECT * FROM film WHERE filmID = :filmID");
            cmd.Parameters.Add(new OracleParameter("filmID", filmID));

            return ExecuteQuery(cmd);
        }

        public bool FilmExists(int filmID)
        {
            OracleCommand cmd = new OracleCommand("SELECT COUNT(FILMID) AS COUNT FROM FILM WHERE FILMID = :filmID");
            cmd.Parameters.Add("filmID", filmID);

            int count = Convert.ToInt32(ExecuteQuery(cmd)[0]["COUNT"]);

            return (count == 1);
        }

        public List<Dictionary<string, object>> GetFilms(int type = 1)
        {
            string query = "";

            OracleCommand cmd = new OracleCommand();

            switch (type)
            {
                case 1: // Actueel
                    cmd.CommandText =
                        "SELECT DISTINCT f.Titel FROM Film f " +
                        "WHERE f.FilmID IN ( " +
                            "SELECT f.FilmID FROM PLANNING p " +
                            "JOIN ZAAL z ON z.ZaalID = p.Zaal " +
                            "JOIN FILM f ON f.FilmID = p.Film " +
                            "JOIN FILM_TYPE t ON t.Naam = p.FilmType " +
                            "JOIN BIOSCOOP b ON b.BioscoopID = z.Bioscoop " +
                            "p.Tijd >= ((SELECT SYSDATE FROM dual))" +
                        ") ORDER BY f.Titel ASC;";
                    break;
                case 2: // Verwacht
                    cmd.CommandText =
                        "SELECT DISTINCT f.Titel FROM Film f WHERE f.releasedate > ((SELECT SYSDATE FROM dual) + 7) ORDER BY f.releasedate ASC;";
                    break;
                default:
                    cmd.CommandText = "SELECT DISTINCT f.Titel FROM FILM f ORDER BY f.Titel ASC;";
                    break;
            }

            return ExecuteQuery(cmd);
        }

        public List<Dictionary<string, object>> GetFilmImages(int filmID)
        {
            OracleCommand cmd = new OracleCommand("SELECT * FORM FILM_IMAGE WHERE FILMID = :filmID");
            cmd.Parameters.Add("filmID", filmID);

            return ExecuteQuery(cmd);

        }

        public int GetCustomerCount(string username)
        {
            OracleCommand cmd = new OracleCommand("SELECT COUNT(*) AS count FROM KLANT WHERE username = :username");

            cmd.Parameters.Add("username", username);

            return Convert.ToInt32(ExecuteQuery(cmd)[0]["COUNT"]);
        }

        public int GetRoomCount(int bioscoopID)
        {
            OracleCommand cmd = new OracleCommand("SELECT COUNT(*) AS count FROM ZAAL WHERE BIOSCOOPID = :bioscoopID");
            cmd.Parameters.Add("bioscoopID", bioscoopID);

            return Convert.ToInt32(ExecuteQuery(cmd)[0]["COUNT"]);
        }

        public List<Dictionary<string, object>> GetRooms(int bioscoopID)
        {
            OracleCommand cmd = new OracleCommand("SELECT * FROM ZAAL WHERE BIOSCOOPID = :bioscoopID ORDER BY ZAALNUMMER ASC");
            cmd.Parameters.Add("bioscoopID", bioscoopID);

            return ExecuteQuery(cmd);
        }

        public Dictionary<string, object> GetRoomInfo(int roomId)
        {
            OracleCommand cmd = new OracleCommand("SELECT * FROM ZAAL WHERE ZAALID = :zaalID");
            cmd.Parameters.Add("zaalID", roomId);

            return ExecuteQuery(cmd)[0];
        }

        public bool RoomExists(int roomID)
        {
            OracleCommand cmd = new OracleCommand("SELECT COUNT(*) AS COUNT FROM ZAAL WHERE ZAALID = :zaalID");
            cmd.Parameters.Add("zaalID", roomID);

            return (Convert.ToInt32(ExecuteQuery(cmd)[0]["COUNT"]) > 0);
        }

        public bool CinemaExists(int bioscoopID)
        {
            OracleCommand cmd = new OracleCommand("SELECT COUNT(*) AS COUNT FROM BIOSCOOP WHERE BIOSCOOPID = :bioscoopID");
            cmd.Parameters.Add("bioscoopID", bioscoopID);

            return (Convert.ToInt32(ExecuteQuery(cmd)[0]["COUNT"]) > 0);
        }

        #endregion

        #region Methods - Insert

        public List<Dictionary<string, object>> Createfilm(string titel, string releaseDate, int duur, string kijkwijzers, string beschrijving, bool isNormaal, bool is3d, bool isImax, bool isI3D, string image)
        {
            try
            {
                OracleCommand cmd =
                    new OracleCommand(
                        "INSERT INTO FILM VALUES (NULL, :titel, :releaseDate, :duur, :kijkwijzer, :beschrijving, :image, :normaal, :dried, :imax, :i3d)");

                cmd.Parameters.Add("titel", titel);
                cmd.Parameters.Add("releaseDate", releaseDate);
                cmd.Parameters.Add("duur", duur);
                cmd.Parameters.Add("kijkwijzer", kijkwijzers);
                cmd.Parameters.Add("beschrijving", beschrijving);
                cmd.Parameters.Add("image", image);
                cmd.Parameters.Add("normaal", isNormaal ? "1" : "0");
                cmd.Parameters.Add("dried", is3d ? "1" : "0");
                cmd.Parameters.Add("imax", isImax ? "1" : "0");
                cmd.Parameters.Add("i3d", isI3D ? "1" : "0");

                if (!Execute(cmd)) return null;
                OracleCommand cmdId = new OracleCommand("SELECT MAX(FILMID) as id FROM FILM");
                return ExecuteQuery(cmdId);
            }
            catch (OracleException ex)
            {
                return new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object> {{"Error", ex.ErrorCode}}
                };
            }
        }

        /// <summary>
        /// Insert images associated with a film into the database
        /// </summary>
        /// <param name="filmID">FilmID that the images are associated to</param>
        /// <param name="images">List of file names</param>
        public bool AddFilmImages(int filmID, List<string> images)
        {
            List<OracleCommand> cmds = new List<OracleCommand>();
            foreach (string image in images)
            {
                try
                {
                    OracleCommand cmd = new OracleCommand("INSERT INTO FILM_IMAGE VALUES(:filmID, :image)");

                    cmd.Parameters.Add("filmID", filmID);
                    cmd.Parameters.Add("image", image);

                    cmds.Add(cmd);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                    return false;
                }
            }

            return Execute(cmds);
        }

        public int CreateUser(string username, string email, string firstname, string tussen, string lastname,
            string address,
            int huisnummer, string postcode, string birthday, bool newsletter)
        {
            OracleCommand cmd =
                new OracleCommand(
                    "INSERT INTO KLANT VALUES(" +
                    "NULL, :username, :email, :firstname, :tussen, :lastname, :address, :huisnr, :postcode, :birthday, :newsletter, NULL, NULL)");

            cmd.Parameters.Add("username", username);
            cmd.Parameters.Add("email", email);
            cmd.Parameters.Add("firstname", firstname);
            cmd.Parameters.Add("tussen", tussen);
            cmd.Parameters.Add("lastname", lastname);
            cmd.Parameters.Add("address", address);
            cmd.Parameters.Add("huisnr", huisnummer);
            cmd.Parameters.Add("postcode", postcode);
            cmd.Parameters.Add("birthday", birthday);
            cmd.Parameters.Add("newsletter", newsletter ? 1 : 0);

            if (!Execute(cmd)) return 0;
            OracleCommand cmdId = new OracleCommand("SELECT MAX(KLANTID) as id FROM KLANT");
            return Convert.ToInt32(ExecuteQuery(cmdId)[0]["ID"]);
        }

        public int CreateRoom(int bioscoopID, int number, int chaircount, bool imax)
        {
            OracleCommand cmd = new OracleCommand("INSERT INTO ZAAL VALUES(NULL, :nummer, :chairs, :bioscoop, :imax)");

            cmd.Parameters.Add("nummer", number);
            cmd.Parameters.Add("chairs", chaircount);
            cmd.Parameters.Add("bioscoop", bioscoopID);
            cmd.Parameters.Add("imax", (imax) ? "1" : "0");

            if (!Execute(cmd)) return 0;

            cmd = new OracleCommand("SELECT MAX(ZAALID) AS ID FROM ZAAL");
            return Convert.ToInt32(ExecuteQuery(cmd)[0]["ID"]);
        }

        public bool CreateCinema(string name, string address, string city, string open, string lift, string toilet,
            string ring, string imax)
        {
            OracleCommand cmd = new OracleCommand("INSERT INTO BIOSCOOP VALUES (NULL, :name, :address, :city, :open, :lift, :toilet, :ring, :imax)");

            cmd.Parameters.Add("name", name);
            cmd.Parameters.Add("address", address);
            cmd.Parameters.Add("city", city);
            cmd.Parameters.Add("open", open);
            cmd.Parameters.Add("lift", lift);
            cmd.Parameters.Add("toilet", toilet);
            cmd.Parameters.Add("ring", ring);
            cmd.Parameters.Add("imax", imax);

            return Execute(cmd);

        }

        #endregion

        #region Methods - Update

        public bool SetPrimaryImage(int filmId, string fileName)
        {
            OracleCommand cmd = new OracleCommand("UPDATE FILM SET AFBEELDING=:image WHERE FILMID=:filmID");
            cmd.Parameters.Add("image", fileName);
            cmd.Parameters.Add("filmID", filmId);

            return Execute(cmd);
        }

        public bool UpdateFilm(int filmID, string titel, string releaseDate, int duur, string kijkwijzers,
            string beschrijving, bool isNormaal, bool is3d, bool isImax, bool isI3D)
        {
            OracleCommand cmd = new OracleCommand("UPDATE FILM SET " +
                                                  "TITEL = :titel," +
                                                  "RELEASEDATE = :releasedate," +
                                                  "DUUR = :duur," +
                                                  "KIJKWIJZER = :kijkwijzer," +
                                                  "BESCHRIJVING = :beschrijving," +
                                                  "ISNORMAAL = :normaal," +
                                                  "ISDRIED = :dried," +
                                                  "ISIMAX = :imax," +
                                                  "ISI3D = :i3d" +
                                                  " WHERE FILMID = :filmID");







            cmd.Parameters.Add("titel", titel);
            cmd.Parameters.Add("releasedate", releaseDate);
            cmd.Parameters.Add("duur", duur);
            cmd.Parameters.Add("kijkwijzer", kijkwijzers);
            cmd.Parameters.Add("beschrijving", beschrijving);
            cmd.Parameters.Add("normaal", isNormaal ? "1" : "0");
            cmd.Parameters.Add("dried", is3d ? "1" : "0");
            cmd.Parameters.Add("imax", isImax ? "1" : "0");
            cmd.Parameters.Add("i3d", isI3D ? "1" : "0");
            cmd.Parameters.Add("filmID", filmID);

            return Execute(cmd);
        }

        public bool UpdateRoom(int id, int cinema, int number, int chairs, bool imax)
        {
            OracleCommand cmd =
                new OracleCommand(
                    "UPDATE ZAAL SET BIOSCOOPID = :bioscoop, ZAALNUMMER = :nummer, AANTALSTOELEN = :stoelen, CANIMAX = :imax WHERE ZAALID = :id");

            cmd.Parameters.Add("bioscoop", cinema);
            cmd.Parameters.Add("nummer", number);
            cmd.Parameters.Add("stoelen", chairs);
            cmd.Parameters.Add("imax", (imax) ? "1" : "0");
            cmd.Parameters.Add("id", id);

            return Execute(cmd);
        }

        #endregion

        #region Methods - Delete

        /// <summary>
        /// Delete item from database
        /// </summary>
        /// <param name="itemID">item ID</param>
        /// <returns>true if success, false otherwise</returns>
        public bool DeleteItem(int itemID)
        {
            OracleCommand cmd = new OracleCommand("DELETE FROM item WHERE itemID = :itemID");
            cmd.Parameters.Add(new OracleParameter("itemID", itemID));

            return Execute(cmd);
        }

        public bool DeleteCinema(int cinemaID)
        {
            OracleCommand cmd = new OracleCommand("UPDATE PLANNING SET ZAAL = (SELECT MIN(ZAALID) FROM ZAAL WHERE BIOSCOOPID = 0) WHERE ZAAL IN (SELECT ZAALID FROM ZAAL WHERE BIOSCOOPID = :bioscoopID)");
            cmd.Parameters.Add("bioscoopID", cinemaID);

            if (!Execute(cmd)) return false;

            cmd = new OracleCommand("DELETE FROM ZAAL WHERE BIOSCOOPID = :bioscoopID");
            cmd.Parameters.Add("bioscoopID", cinemaID);

            if (!Execute(cmd)) return false;

            cmd = new OracleCommand("DELETE FROM BIOSCOOP WHERE BIOSCOOPID = :bioscoopID");
            cmd.Parameters.Add("bioscoopID", cinemaID);

            return Execute(cmd);
        }
        #endregion

        #region Methods - Miscellaneous



        /// <summary>
        /// Executes a query that will insert, update or delete records in a database
        /// </summary>
        /// <returns>
        /// Returns true if successfull, false if not
        /// </returns>
        public bool Execute(OracleCommand cmd)
        {
            System.Diagnostics.Debug.WriteLine("---------------");
            System.Diagnostics.Debug.WriteLine("Attempting to execute query: " + cmd.CommandText);
            try
            {
                Open_Connection();
                cmd.Connection = connect;
                cmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine("COMPLETE");
                return true;
            }
            catch (OracleException ex)
            {
                System.Diagnostics.Debug.WriteLine("---------- ERROR WHILE EXECUTING QUERY ----------");
                System.Diagnostics.Debug.WriteLine("Error while executing query: " + cmd.CommandText);
                System.Diagnostics.Debug.WriteLine("Error code: {0}", ex.ErrorCode);
                System.Diagnostics.Debug.WriteLine("Error message: " + ex.Message);
                System.Diagnostics.Debug.WriteLine("---------- END OF EXCEPTION ----------");
                throw;
            }
            finally
            {
                Close_Connection();
            }
        }

        /// <summary>
        /// Executes a list of queries that will insert, update or delete records in a database
        /// </summary>
        /// <returns>
        /// Returns true if successfull, false if not
        /// </returns>
        public bool Execute(List<OracleCommand> cmds)
        {
            try
            {
                Open_Connection();

                foreach (OracleCommand cmd in cmds)
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine("---------------");
                        System.Diagnostics.Debug.WriteLine("Attempting to execute query: " + cmd.CommandText);

                        cmd.Connection = connect;
                        cmd.ExecuteNonQuery();
                        System.Diagnostics.Debug.WriteLine("COMPLETE");
                    }
                    catch (OracleException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("---------- ERROR WHILE EXECUTING QUERY ----------");
                        System.Diagnostics.Debug.WriteLine("Error while executing query: " + cmd.CommandText);
                        System.Diagnostics.Debug.WriteLine("Error code: {0}", ex.ErrorCode);
                        System.Diagnostics.Debug.WriteLine("Error message: " + ex.Message);
                        System.Diagnostics.Debug.WriteLine("---------- END OF EXCEPTION ----------");
                        throw;
                    }
                }
            }
            finally
            {
                Close_Connection();
            }
            return true;
        }

        /// <summary>
        /// Executes query and returns the result in a processable format. SqlExceptions will be had.
        /// </summary>
        /// <returns>List&lt;Dictionary&lt;&lt;fieldName, value&gt;&gt; or null on failure</returns>
        public List<Dictionary<string, object>> ExecuteQuery(OracleCommand cmd)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            System.Diagnostics.Debug.WriteLine("---------------");
            System.Diagnostics.Debug.WriteLine("Attempting to execute query: " + cmd.CommandText);
            try
            {
                Open_Connection();

                cmd.Connection = connect;

                using (OracleDataReader resultReader = cmd.ExecuteReader())
                {
                    //loop through the rows and add them to the result
                    while (resultReader.Read())
                    {
                        Dictionary<string, object> row = new Dictionary<string, object>();

                        //loop through the fields and add them to the row
                        for (int fieldId = 0; fieldId < resultReader.FieldCount; fieldId++)
                            row.Add(resultReader.GetName(fieldId), resultReader.GetValue(fieldId));

                        result.Add(row);
                    }
                }
                System.Diagnostics.Debug.WriteLine("COMPLETE");
                return result;
            }
            catch (OracleException ex)
            {
                System.Diagnostics.Debug.WriteLine("---------- ERROR WHILE EXECUTING QUERY ----------");
                System.Diagnostics.Debug.WriteLine("Error while executing query: {0}", cmd.CommandText);
                System.Diagnostics.Debug.WriteLine("Error code: {0}", ex.ErrorCode);
                System.Diagnostics.Debug.WriteLine("Error message: {0}", ex.Message);
                System.Diagnostics.Debug.WriteLine("---------- END OF EXCEPTION ----------");
            }
            finally
            {
                Close_Connection();
            }
            return null;
        }

        /// <summary>
        /// Get a substring based on a begin and end marker
        /// </summary>
        /// <param name="a">Begin marker. Everything after this is part of the substring</param>
        /// <param name="b">End marker. Everything before this is part of the substring</param>
        /// <param name="c">Full string. The begin marker, end marker and substring are in here</param>
        /// <returns>Requested substring</returns>
        public string GetSubstringByString(string a, string b, string c)
        {
            if (!(c.Contains(a) && c.Contains(b)))
                throw new ArgumentException(string.Format("{2} does not contain {0} and {1}", a, b, c));
            if (c.IndexOf(a, StringComparison.Ordinal) >= c.IndexOf(b, StringComparison.Ordinal))
                throw new ArgumentException(string.Format("{0} is not before {1} in {2}", a, b, c));

            return c.Substring((c.IndexOf(a, StringComparison.Ordinal) + a.Length), (c.IndexOf(b, StringComparison.Ordinal) - c.IndexOf(a, StringComparison.Ordinal) - a.Length));
        }

        #endregion
    }
}
