using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Private_Ethercloset.MVVM.Model
{
    internal class EquipmentFactory
    {
        private const string DEFAULT_NAME = "";

        private DatabaseHelper databaseHelper = new DatabaseHelper();
        private List<string> dyeList = new List<string>();
        public EquipmentFactory()
        {
            dyeList = databaseHelper.getDyes();
        }

        public BodyEquipment defaultWeapon()
        {
            return new BodyEquipment(DEFAULT_NAME, GearType.WEAPON);
        }

        public BodyEquipment createWeapon(string name, string dye1, string dye2)
        {
            return new BodyEquipment(name, GearType.WEAPON, dye1, dye2);
        }

        public BodyEquipment defaultHead()
        {
            return new BodyEquipment(DEFAULT_NAME, GearType.HEAD);
        }

        public BodyEquipment createHeadGear(string name, string dye1, string dye2)
        {
            return new BodyEquipment(name, GearType.HEAD, dye1, dye2);
        }

        public BodyEquipment defaultHand()
        {
            return new BodyEquipment(DEFAULT_NAME, GearType.HAND);
        }

        public BodyEquipment createHandGear(string name, string dye1, string dye2)
        {
            return new BodyEquipment(name, GearType.HAND, dye1, dye2);
        }

        public BodyEquipment defaultChest()
        {
            return new BodyEquipment(DEFAULT_NAME, GearType.CHEST);
        }

        public BodyEquipment createChestGear(string name, string dye1, string dye2)
        {
            return new BodyEquipment(name, GearType.CHEST, dye1, dye2);
        }

        public BodyEquipment defaultLeg()
        {
            return new BodyEquipment(DEFAULT_NAME, GearType.LEG);
        }

        public BodyEquipment createLegGear(string name, string dye1, string dye2)
        {
            return new BodyEquipment(name, GearType.LEG, dye1, dye2);
        }

        public BodyEquipment defaultFoot()
        {
            return new BodyEquipment(DEFAULT_NAME, GearType.FOOT);
        }
        public BodyEquipment createFootGear(string name, string dye1, string dye2)
        {
            return new BodyEquipment(name, GearType.FOOT, dye1, dye2);
        }

        public Accessory defaultEar()
        {
            return new Accessory(DEFAULT_NAME, GearType.EAR);
        }

        public Accessory createEarGear(string name)
        {
            return new Accessory(name, GearType.EAR);
        }

        public Accessory defaultNeck()
        {
            return new Accessory(DEFAULT_NAME, GearType.NECK);
        }

        public Accessory createNeckGear(string name)
        {
            return new Accessory(name, GearType.NECK);
        }

        public Accessory defaultBracelet()
        {
            return new Accessory(DEFAULT_NAME, GearType.BRACELET);
        }

        public Accessory createBracelet(string name)
        {
            return new Accessory(name, GearType.BRACELET);
        }

        public Accessory defaultRing()
        {
            return new Accessory(DEFAULT_NAME, GearType.RING);
        }

        public Accessory createRing(string name)
        {
            return new Accessory(name, GearType.RING);
        }
    }
}
