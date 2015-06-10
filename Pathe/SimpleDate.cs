using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pathe
{
    class SimpleDate
    {

        private DateTime date;
        private int day;
        private int month;
        private int year;

        private string oDay;
        private string oMonth;
        private string oYear;

        public SimpleDate(string dbDate)
        {
            if(!Regex.IsMatch(dbDate,  @"^[0-9]{2}-[A-Za-z]{3}-[0-9]{2}.*")) throw new ArgumentException("Input moet voldoen aan Oracle date format");

            string[] inDate = dbDate.Split(' ')[0].Split('-');

            oDay = inDate[0];
            oMonth = inDate[1];
            oYear = inDate[2];

            day = Convert.ToInt32(oDay);
            year = Convert.ToInt32(oYear);

            switch (oMonth.ToUpper())
            {
                case "JAN":
                    month = 1;
                    break;
                case "FEB":
                    month = 2;
                    break;
                case "MAR":
                    month = 3;
                    break;
                case "APR":
                    month = 4;
                    break;
                case "MAY":
                    month = 5;
                    break;
                case "JUN":
                    month = 6;
                    break;
                case "JUL":
                    month = 7;
                    break;
                case "AUG":
                    month = 8;
                    break;
                case "SEP":
                    month = 9;
                    break;
                case "OCT":
                    month = 10;
                    break;
                case "NOV":
                    month = 11;
                    break;
                case "DEC":
                    month = 12;
                    break;

            }

            date = new DateTime(year, month, day);
        }

        public DateTime RealDate
        {
            get { return date; }
        }

        public string OracleDate
        {
            get { return string.Format("{0}-{1}-{2}", oDay, oMonth, oYear); }
        }

        public string FancyDate
        {
            get { return string.Format("{0} {1} {2}", oDay, oMonth, oYear); }
        }
    }
}
