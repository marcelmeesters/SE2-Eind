using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathe
{
    class Zaal
    {
        #region Constructor

        public Zaal(int id, int number, int chairCount, int cinema, bool imax)
        {
            Id = id;
            Number = number;
            ChairCount = chairCount;
            CinemaID = cinema;
            Imax = imax;
        }

        #endregion

        #region Properties

        public int Id { get; private set; }
        public int Number { get; private set; }
        public int ChairCount { get; private set; }
        public int CinemaID { get; private set; }
        public bool Imax { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the room in the database
        /// </summary>
        /// <returns>Unique ID of the newly created room</returns>
        public int Create()
        {
            Id = Database.Instance.CreateRoom(CinemaID, Number, ChairCount, Imax);
            return Id;
        }

        public bool Update()
        {
            return Database.Instance.UpdateRoom(Id, CinemaID, Number, ChairCount, Imax);
        }

        #endregion

    }
}
