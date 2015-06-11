using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pathe.Tests
{
    [TestClass]
    public class Film_Tests
    {
        private Film film = new Film(0, "Test Film", "Dit is een test film", 120, DateTime.Now,
            new List<Kijkwijzer> {Kijkwijzer.Al}, true, true, false, false);
        [TestMethod]
        public void KijkwijzerString()
        {
            string kwString = film.KijkwijzerString;

            Assert.AreEqual("Al", kwString);
        }

        [TestMethod]
        public void GetKijkwijzerString()
        {
            string kwString =
                film.GetKijwijzerString(new List<Kijkwijzer>
                {
                    Kijkwijzer.Al,
                    Kijkwijzer.Discriminatie,
                    Kijkwijzer.Zestien
                });

            Assert.AreEqual("Al,Discriminatie,Zestien", kwString);
        }

        [TestMethod]
        public void UrlString()
        {
            string expected = "0-Test-Film";

            string actual = film.UrlString;

            Assert.AreEqual(expected, actual);
        }
        
    }
}
