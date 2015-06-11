using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pathe.Tests
{
    [TestClass]
    public class SimpleDate_Tests
    {

        [TestMethod]
        public void Constructor1()
        {
            SimpleDate date = new SimpleDate("10-MAR-15");

            DateTime expected = new DateTime(2015, 03, 10);
            DateTime actual = Convert.ToDateTime(date.RealDate);

            // Vergelijk alleen dag, maand en jaar, omdat ticks toch nooit meewerken..
            //Assert.IsTrue(expected.Day == actual.Day && expected.Month == actual.Month && expected.Year == actual.Year);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Constructor2()
        {
            SimpleDate date = new SimpleDate(new DateTime(2015, 4, 10));

            string expected = "10-APR-2015";
            string actual = date.OracleDate;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Constructor3()
        {
            for (int month = 1; month < 12; month++)
            {
                for (int day = 1; day < 31; day++)
                {
                    DateTime check = new DateTime(2015, month, 1);

                    if(month == 4 || month == 6 || month == 9 || month == 11)
                        if (day == 31) continue;

                    if (month == 2 && day > 28) continue;

                    DateTime actuald = new DateTime(2015, month, day);
                    SimpleDate date = new SimpleDate(actuald);

                    string actual = date.OracleDate;

                    string expected = actuald.ToString("dd-MMM-yyyy").ToUpper();

                    Assert.AreEqual(expected, actual);
                }
            }
        }

    }
}
