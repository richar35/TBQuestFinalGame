using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestFinalGame.Models
{
    public class Character : ObservableObject
    {
        #region ENUMS
        public enum RaceType
        {
            Human,
            Wizzard,
            Orc
        }
        #endregion

        #region FIELDS
        protected int _id;
        protected string _name;
        protected int _locationid;
        protected int _age;
        protected RaceType _race;

        protected Random random = new Random();
        #endregion 

        #region PROPERTIES
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int LocationId
        {
            get { return _locationid; }
            set { _locationid = value; }
        }
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }
        public RaceType Race
        {
            get { return _race; }
            set { _race = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public Character() { }
        public Character(string Name, RaceType Race, int LocationId)
        {
            _name = Name;
            _race = Race;
            _locationid = LocationId;
        }
        #endregion

        #region METHODS
        public virtual string DefaultGreeting()
        {
            return $"Hello, My name is {_name}.";
        }
        #endregion
    }
}
