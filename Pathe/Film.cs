using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
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

        public Film(int filmId, string title, string description, int duration, DateTime release, string kijkWijzers, bool normal, bool dried, bool imax, bool i3d, string primaryImage = null, List<string> images = null )
        {
            FilmId = filmId;
            Title = title;
            Description = description;
            Duration = duration;
            Release = new DateTime(release.Year + 2000, release.Month, release.Day);
            PrimaryImage = primaryImage;
            Images = images;

            IsNormaal = normal;
            Is3D = dried;
            IsImax = imax;
            IsI3D = i3d;

            foreach (string kw in kijkWijzers.Split(','))
            {
                Kijkwijzer temp;
                if (Enum.TryParse(kw, out temp))
                {
                    this.kijkWijzers.Add(temp);
                }
            }
        }

        public Film(int filmId, string title, string description, int duration, DateTime release, List<Kijkwijzer> kws, bool normal, bool dried, bool imax, bool i3d, string primaryImage = null, List<string> images = null)
        {
            FilmId = filmId;
            Title = title;
            Description = description;
            Duration = duration;
            Release = release;
            PrimaryImage = primaryImage;
            Images = images;

            IsNormaal = normal;
            Is3D = dried;
            IsImax = imax;
            IsI3D = i3d;

            kijkWijzers = kws;
        }

        #endregion

        #region Properties

        public int FilmId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Duration { get; private set; }
        public DateTime Release { get; private set; }
        public bool IsNormaal { get; private set; }
        public bool Is3D { get; private set; }
        public bool IsImax { get; private set; }
        public bool IsI3D { get; private set; }

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
            get { return GetKijwijzerString(KijkWijzers); }
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
                FilmId =
                    Convert.ToInt32(
                        db.Createfilm(Title, release, Duration, KijkwijzerString, Description, IsNormaal, Is3D, IsImax,
                            IsI3D, PrimaryImage)[0]["ID"]);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return FilmId;
        }

        /// <summary>
        /// Convert a comma-separated list of kijkwijzer ratings to a list of Kijkwijzer objects (Enum)
        /// </summary>
        /// <param name="kwString">comma separated list</param>
        /// <returns>List(Kijkwijzer)</returns>
        public List<Kijkwijzer> GetKijkwijzers(string kwString)
        {
            Kijkwijzer temp = Kijkwijzer.Al;
            return (from kw in kwString.Split(',') where Enum.TryParse(kw, out temp) select temp).ToList();
        }

        /// <summary>
        /// Converts a list of Kijkwijzer objects to a comma-separated list
        /// </summary>
        /// <param name="kws">List(Kijkwijzer)</param>
        /// <returns>comma-separated string</returns>
        public string GetKijwijzerString(List<Kijkwijzer> kws)
        {
            string returned = kws.Aggregate("", (current, kw) => current + (kw.ToString() + ","));

            return returned.TrimEnd(',');
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
