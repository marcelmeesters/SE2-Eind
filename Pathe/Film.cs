using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pathe
{
    class Film
    {
        #region Fields

        private Database db = Database.Instance;
        private readonly List<Kijkwijzer> kijkWijzers = new List<Kijkwijzer>(); 

        #endregion

        #region Constructor

        public Film(int filmId, string title, string description, int duration, DateTime release, string kijkWijzers, string primaryImage = null, List<string> images = null )
        {
            FilmId = filmId;
            Title = title;
            Description = description;
            Duration = duration;
            Release = release;
            PrimaryImage = primaryImage;
            Images = images;

            foreach (string kw in kijkWijzers.Split(','))
            {
                Kijkwijzer temp;
                if (Enum.TryParse(kw, out temp))
                {
                    this.kijkWijzers.Add(temp);
                }
            }
        }

        public Film(int filmId, string title, string description, int duration, DateTime release, List<Kijkwijzer> kws, string primaryImage = null, List<string> images = null)
        {
            FilmId = filmId;
            Title = title;
            Description = description;
            Duration = duration;
            Release = release;
            PrimaryImage = primaryImage;
            Images = images;
            kijkWijzers = kws;
        }

        #endregion

        #region Properties

        public int FilmId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Duration { get; private set; }
        public DateTime Release { get; private set; }

        public List<Kijkwijzer> KijkWijzers
        {
            get { return kijkWijzers; }
        }

        public string KijkwijzerStringImg
        {
            get
            {
                return kijkWijzers.Aggregate("", (current, kw) => current + ("<img src='/img/kw_" + kw + ".png' alt='Kijkwijzer " + kw + "' class='kijkwijzer'/>"));
            }
        }

        public string KijkwijzerString
        {
            get
            {
                return kijkWijzers.Aggregate("", (current, kw) => current + (kw + ","));
            }
        }

        public string UrlString
        {
            get
            {
                return FilmId + "-" + Regex.Replace(Title.Replace(" ", "-"), "[^a-zA-Z0-9-]+", "");
            }
        }

        public List<string> Images { get; set; }

        public string PrimaryImage { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Adds this film to the database, and returns the new ID
        /// </summary>
        /// <returns></returns>
        public int Create()
        {
            try
            {
                string release = Release.ToString("dd-MMM-yy");
                FilmId = Convert.ToInt32(db.Createfilm(Title, release, Duration, KijkwijzerString, Description, PrimaryImage)[0]["ID"]);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return FilmId;
        }

        #endregion
    }

    internal enum Kijkwijzer
    {
        Al,
        Zes,
        Negen,
        Twaalf,
        Zestien,
        Geweld,
        Angst,
        Seks,
        Discriminatie,
        Drugsalcohol,
        Taalgebruik
    }
}
