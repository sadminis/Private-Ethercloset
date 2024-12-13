using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Private_Ethercloset.MVVM.Model
{
    internal class BodyEquipment : Equipment
    {
        string _dye1 = "无染色";
        string _dye2 = "无染色";

        public BodyEquipment(string name, GearType gearType) : base(name, gearType) { }
        public BodyEquipment(string name, GearType gearType, string dye1, string dye2) : base(name, gearType)
        {
            Assert.NotNull(dye1);
            Assert.NotNull(dye2);
            _dye1 = dye1;
            _dye2 = dye2;
        }

        public string getDye1()
        {
            return _dye1;
        }

        public void setDye1(string dye1)
        {
            _dye1 = dye1;
        }

        public string getDye2()
        {
            return _dye2;
        }

        public void setDye2(string dye2)
        {
            _dye2 = dye2;
        }
    }
}
