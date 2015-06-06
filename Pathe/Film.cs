using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Film(int filmId, string title, string description, int duration, DateTime release, string kijkWijzers)
        {
            FilmId = filmId;
            Title = title;
            Description = description;
            Duration = duration;
            Release = release;

            foreach (string kw in kijkWijzers.Split(','))
            {
                Kijkwijzer temp;
                if (Enum.TryParse(kw, out temp))
                {
                    this.kijkWijzers.Add(temp);
                }
            }
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

        public string KijkwijzerString
        {
            get
            {
                return kijkWijzers.Aggregate("", (current, kw) => current + ("<img src='/img/kw_" + kw + ".png' alt='Kijkwijzer " + kw + "' class='kijkwijzer'/>"));
            }
        }

        #endregion

        #region Methods

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
