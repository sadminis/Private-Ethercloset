using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Private_Ethercloset.MVVM.Model
{
    internal class Accessory : Equipment
    {
        public Accessory(string name, GearType gearType) : base(name, gearType) { }
    }
}
