using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathe
{
    class User
    {
        #region Fields

        private int id;
        private string username;

        #endregion

        #region Constructor

        public User(int id, string username)
        {
            this.id = id;
            this.username = username;
        }

        public User() {}

        #endregion

        #region Properties

        #endregion

        #region Methods

        /*static public bool VerifyLogin(string username, string password)
        {
            
        }*/

        #endregion


    }

    abstract class Customer : User
    {
        #region Fields
        private User user;
        private string name;
        private string email;
        private int customerId;

        private bool isSenior;
        #endregion

        #region Constructor

        protected Customer(int customerId, User user, string name, string email, bool isSenior)
        {
            this.customerId = customerId;
            this.user = user;
            this.name = name;
            this.email = email;

            this.isSenior = isSenior;
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion
    }

    class Regular : Customer
    {

        public Regular(int customerId, User user, string name, string email, bool isSenior)
            : base(customerId, user, name, email, isSenior) { }

    }

    class Unlimited : Customer
    {
        #region Fields
        
        #endregion

        #region Constructor

        public Unlimited(int customerId, User user, string name, string email, bool isSenior, int pasnummer, int korting)
            : base(customerId, user, name, email, isSenior)
        {
            Pasnummer = pasnummer;
            Korting = korting;
        }

        #endregion

        #region Properties

        public int Pasnummer { get; private set; }

        public int Korting { get; private set; }

        #endregion

        #region Methods

        #endregion
    }

    class Employee : User
    {
        
    }
}
