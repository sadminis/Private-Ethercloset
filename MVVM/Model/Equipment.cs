using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Private_Ethercloset.MVVM.Model
{
    abstract internal class Equipment
    {
        private static DatabaseHelper databaseHelper = new DatabaseHelper();
        private string _name;
        private GearType _gearType;
        private int _id;

        public Equipment(string name, GearType gearType)
        {
            _name = name;
            _gearType = gearType;
            _id = getIDFromName();
        }

        private int getIDFromName()
        {
            return databaseHelper.GetItemIdByName(_name);
        }

        public string getName()
        {
            return _name;
        }

        public void setName(string name)
        {
            _name = name;
            _id = getIDFromName();
        }

        public GearType getGearType()
        {
            return _gearType;
        }

        public int getID()
        {
            return _id;
        }

        public override string ToString()
        {
            return "Equipment Name:" + _name;
        }
    }
}
