using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pathe
{
    public class SimpleDate
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

            month = GetMonth(oMonth);
            

            date = new DateTime(year, month, day);
        }

        public SimpleDate(DateTime date)
        {
            this.date = date;

            day = date.Day;
            month = date.Month;
            year = date.Year;

            oDay = Convert.ToString(date.Day);
            if (date.Day < 10) oDay = "0" + oDay;

            oMonth = GetOMonth(date.Month);

            oYear = Convert.ToString(date.Year);
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

        private int GetMonth(string oMonth)
        {
            int returned = 0;
            switch (oMonth.ToUpper())
            {
                case "JAN":
                    returned = 1;
                    break;
                case "FEB":
                    returned = 2;
                    break;
                case "MAR":
                    returned = 3;
                    break;
                case "APR":
                    returned = 4;
                    break;
                case "MAY":
                    returned = 5;
                    break;
                case "JUN":
                    returned = 6;
                    break;
                case "JUL":
                    returned = 7;
                    break;
                case "AUG":
                    returned = 8;
                    break;
                case "SEP":
                    returned = 9;
                    break;
                case "OCT":
                    returned = 10;
                    break;
                case "NOV":
                    returned = 11;
                    break;
                case "DEC":
                    returned = 12;
                    break;
            }
            return returned;
        }

        private string GetOMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "JAN";
                    break;
                case 2:
                    return "FEB";
                    break;
                case 3:
                    return "MAR";
                    break;
                case 4:
                    return "APR";
                    break;
                case 5:
                    return "MAY";
                    break;
                case 6:
                    return "JUN";
                    break;
                case 7:
                    return "JUL";
                    break;
                case 8:
                    return "AUG";
                    break;
                case 9:
                    return "SEP";
                    break;
                case 10:
                    return "OCT";
                    break;
                case 11:
                    return "NOV";
                    break;
                case 12:
                    return "DEC";
                    break;
            }
            return "NOPE";
        }
    }
}
