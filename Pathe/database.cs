using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Pathe
{
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
        public bool Open_Connection()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Opening Database Connection");
                connect.Open();
                return true;
            }
            catch (OracleException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                if (ex.Message == "Connection is already open")
                {
                    return false;
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

        public List<Dictionary<string, object>> GetVenue()
        {
            string query = string.Format("SELECT v.* FROM VENUE v " +
                                         "JOIN EVENT_VENUE e ON e.venueID = v.venueID " +
                                         "WHERE e.eventID = {0}", 1);
            return ExecuteQuery(query);
        }

        public List<Dictionary<string, object>> GetFilms(int type = 1)
        {
            string query = "";
            switch (type)
            {
                case 1: // Actueel
                    query =
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
                    query =
                        "SELECT DISTINCT f.Titel FROM Film f WHERE f.releasedate > ((SELECT SYSDATE FROM dual) + 7) ORDER BY f.releasedate ASC;";
                    break;
                default:
                    query = "SELECT DISTINCT f.Titel FROM FILM f ORDER BY f.Titel ASC;";
                    break;
            }

            return ExecuteQuery(query);
        }

        #endregion

        #region Methods - Insert

        /// <summary>
        /// Adds a new group to the database
        /// </summary>
        /// <param name="locID">Location used by the group</param>
        /// <param name="paid">whether the group has paid or not</param>
        /// <returns>true if success, false if not</returns>
        public bool CreateGroup(int locID, int paid)
        {
            string query = string.Format("INSERT INTO EVENTGROUP VALUES (NULL, {0}, {1}, {2})", locID, paid, 1);

            return Execute(query);
        }

        #endregion

        #region Methods - Update

        /// <summary>
        /// Update an existing item in the database
        /// </summary>
        /// <param name="itemID">item ID that will be updated</param>
        /// <param name="depostID">deposit ID</param>
        /// <param name="categoryID">category ID</param>
        /// <param name="brandID">brand ID</param>
        /// <param name="stock">stock of the item. Must be at least 0</param>
        /// <param name="name">item name</param>
        /// <param name="description">item description</param>
        /// <returns>true if success, false if not</returns>
        public bool UpdateItem(int itemID, int depostID, int categoryID, int brandID, int stock, string name,
            string description)
        {
            string query =
                string.Format(
                    "UPDATE ITEM SET name='{0}', description='{1}', brandID={2}, categoryID={3}, stock={4}, depositID={5} WHERE itemID={6}",
                    name, description, brandID, categoryID, stock, depostID, itemID);

            return Execute(query);
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
            string query = string.Format("DELETE FROM item WHERE itemID = {0}", itemID);
            return Execute(query);
        }
        #endregion

        #region Methods - Miscellaneous



        /// <summary>
        /// Executes a query that will insert, update or delete records in a database
        /// </summary>
        /// <returns>
        /// Returns true if successfull, false if not
        /// </returns>
        public bool Execute(string query)
        {
            System.Diagnostics.Debug.WriteLine("---------------");
            System.Diagnostics.Debug.WriteLine("Attempting to execute query: {0}", query);
            try
            {
                if (!Open_Connection()) throw new Exception("Could not connect to database");

                OracleCommand cmd = new OracleCommand(query, connect);
                cmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine("COMPLETE");
                return true;
            }
            finally
            {
                Close_Connection();
            }
        }

        /// <summary>
        /// Executes query and returns the result in a processable format. MySqlExceptions will be had.
        /// </summary>
        /// <returns>List&lt;Dictionary&lt;&lt;fieldName, value&gt;&gt; or null on failure</returns>
        public List<Dictionary<string, object>> ExecuteQuery(string query)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            System.Diagnostics.Debug.WriteLine("---------------");
            System.Diagnostics.Debug.WriteLine("Attempting to execute query: {0}", query);
            try
            {
                if (!Open_Connection()) throw new Exception("Could not connect to database");

                OracleDataReader resultReader = new OracleCommand(query, connect).ExecuteReader();

                //loop through the rows and add them to the result
                while (resultReader.Read())
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();

                    //loop through the fields and add them to the row
                    for (int fieldId = 0; fieldId < resultReader.FieldCount; fieldId++)
                        row.Add(resultReader.GetName(fieldId), resultReader.GetValue(fieldId));

                    result.Add(row);
                }
                System.Diagnostics.Debug.WriteLine("COMPLETE");
                return result;
            }
            catch (OracleException ex)
            {
                System.Diagnostics.Debug.WriteLine("---------- ERROR WHILE EXECUTING QUERY ----------");
                System.Diagnostics.Debug.WriteLine("Error while executing query: {0}", query);
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
