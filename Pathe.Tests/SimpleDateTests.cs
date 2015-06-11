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

            Assert.IsTrue(expected.Date == actual.Date && expected.Month == actual.Month && expected.Year == actual.Year);
        }

        [TestMethod]
        public void Constructor2()
        {
            SimpleDate date = new SimpleDate(new DateTime(2015, 4, 10));

            string expected = "10-APR-2015";
            string actual = date.OracleDate;

            Assert.AreEqual(expected, actual);
        }
        
    }
}
