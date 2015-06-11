using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pathe
{
    class User
    {
        #region Fields

        private int id;
        private readonly string username;

        #endregion

        #region Constructor

        public User(int id, string username, string email, string firstname, string tussen, string lastname, string address, int huisnummer, string postcode, SimpleDate birthday, bool newsletter)
        {
            this.id = id;
            this.username = username;

            FirstName = firstname;
            Tussenvoegsel = tussen;
            LastName = lastname;
            Address = address;
            Huisummer = huisnummer;
            Postcode = postcode;
            Birthday = birthday;
            Newsletter = newsletter;
            Email = email;
        }

        #endregion

        #region Properties
        
        public int Huisummer { get; private set; }
        public string FirstName { get; private set; }
        public string Tussenvoegsel { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
        public string Postcode { get; private set; }

        public bool Newsletter { get; set; }
        public SimpleDate Birthday { get; private set; }


        #endregion

        #region Methods

        public int Register()
        {
            id = Database.Instance.CreateUser(username, Email, FirstName, Tussenvoegsel, LastName, Address, Huisummer,
                Postcode, Birthday.OracleDate, Newsletter);
            return id;
        }

        #endregion


    }

}
