using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Xunit;

namespace Private_Ethercloset.MVVM.Model
{
    public class Card
    {
        private EquipmentFactory equipmentFactory = new EquipmentFactory();
        private DatabaseHelper databaseHelper = new DatabaseHelper();

        private BitmapImage _image;
        private string _imagePath;

        private BodyEquipment _weapon;
        private BodyEquipment _head;
        private BodyEquipment _chest;
        private BodyEquipment _hand;
        private BodyEquipment _leg;
        private BodyEquipment _foot;

        private Accessory _ear;
        private Accessory _neck;
        private Accessory _bracelet;
        private Accessory _ring1;
        private Accessory _ring2;


        public Card(string imagePath)
        {
            // Create and load images
            _imagePath = imagePath;
            _image = loadBitmapImage();

            initEquipments();
        }

        public void setWeapon(int nameID, int dye1ID, int dye2ID)
        {
            string name = databaseHelper.GetItemNameByID(nameID);
            string dye1 = databaseHelper.GetDyeNameByID(dye1ID);
            string dye2 = databaseHelper.GetDyeNameByID(dye2ID);

            setWeapon(name, dye1, dye2);
        }

        public void setWeapon(string name, string dye1, string dye2)
        {
            _weapon = equipmentFactory.createWeapon(name, dye1, dye2);
        }

        public void setHead(int nameID, int dye1ID, int dye2ID)
        {
            string name = databaseHelper.GetItemNameByID(nameID);
            string dye1 = databaseHelper.GetDyeNameByID(dye1ID);
            string dye2 = databaseHelper.GetDyeNameByID(dye2ID);

            setHead(name, dye1, dye2);
        }

        public void setHead(string name, string dye1, string dye2)
        {
            _head = equipmentFactory.createHeadGear(name, dye1, dye2);
        }

        public void setChest(int nameID, int dye1ID, int dye2ID)
        {
            string name = databaseHelper.GetItemNameByID(nameID);
            string dye1 = databaseHelper.GetDyeNameByID(dye1ID);
            string dye2 = databaseHelper.GetDyeNameByID(dye2ID);

            setChest(name, dye1, dye2);
        }

        public void setChest(string name, string dye1, string dye2)
        {
            _chest = equipmentFactory.createChestGear(name, dye1, dye2);
        }

        public void setLeg(int nameID, int dye1ID, int dye2ID)
        {
            string name = databaseHelper.GetItemNameByID(nameID);
            string dye1 = databaseHelper.GetDyeNameByID(dye1ID);
            string dye2 = databaseHelper.GetDyeNameByID(dye2ID);

            setLeg(name, dye1, dye2);
        }

        public void setLeg(string name, string dye1, string dye2)
        {
            _leg = equipmentFactory.createLegGear(name, dye1, dye2);
        }

        public void setFoot(int nameID, int dye1ID, int dye2ID)
        {
            string name = databaseHelper.GetItemNameByID(nameID);
            string dye1 = databaseHelper.GetDyeNameByID(dye1ID);
            string dye2 = databaseHelper.GetDyeNameByID(dye2ID);

            setFoot(name, dye1, dye2);
        }

        public void setFoot(string name, string dye1, string dye2)
        {
            _foot = equipmentFactory.createFootGear(name, dye1, dye2);
        }

        public void setEar(int nameID)
        {
            string name = databaseHelper.GetItemNameByID(nameID);

            setEar(name);
        }

        public void setEar(string name)
        {
            _ear = equipmentFactory.createEarGear(name);
        }

        public void setNeck(int nameID)
        {
            string name = databaseHelper.GetItemNameByID(nameID);

            setNeck(name);
        }

        public void setNeck(string name)
        {
            _neck = equipmentFactory.createNeckGear(name);
        }

        public void setBracelet(int nameID)
        {
            string name = databaseHelper.GetItemNameByID(nameID);

            setBracelet(name);
        }

        public void setBracelet(string name)
        {
            _bracelet = equipmentFactory.createBracelet(name);
        }

        public void setRing1(int nameID)
        {
            string name = databaseHelper.GetItemNameByID(nameID);

            setRing1(name);
        }

        public void setRing1(string name)
        {
            _ring1 = equipmentFactory.createRing(name);
        }

        public void setRing2(int nameID)
        {
            string name = databaseHelper.GetItemNameByID(nameID);

            setRing2(name);
        }

        public void setRing2(string name)
        {
            _ring2 = equipmentFactory.createRing(name);
        }

        public string getImagePath()
        {
            return _imagePath;
        }

        private void initEquipments()
        {
            // Init body parts
            _weapon = equipmentFactory.defaultWeapon();
            _head = equipmentFactory.defaultHead();
            _chest = equipmentFactory.defaultChest();
            _hand = equipmentFactory.defaultHand();
            _leg = equipmentFactory.defaultLeg();
            _foot = equipmentFactory.defaultFoot();

            // Init accessories
            _ear = equipmentFactory.defaultEar();
            _neck = equipmentFactory.defaultNeck();
            _bracelet = equipmentFactory.defaultBracelet();
            _ring1 = equipmentFactory.defaultRing();
            _ring2 = equipmentFactory.defaultRing();
        }

        private BitmapImage loadBitmapImage()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(_imagePath);
            image.EndInit();
            image.Freeze();

            return image;
        }

        public BitmapImage getImage()
        {
            Assert.True(hasImage());
            return _image;
        }

        public bool hasImage()
        {
            return _image != null;
        }

        public void changeBodyEquipmentTo(GearType gearType, string name, string dye1, string dye2)
        {
            BodyEquipment equipmentToChange = locateBodyEquipment(gearType);

            Assert.NotNull(equipmentToChange);

            equipmentToChange.setName(name);
            equipmentToChange.setDye1(dye1);
            equipmentToChange.setDye2(dye2);
        }

        public void changeAccessoryTo(GearType gearType, string name)
        {
            Accessory accessoryToChange = locateAccesssory(gearType);

            Assert.NotNull(accessoryToChange);

            accessoryToChange.setName(name);
        }

        private Accessory locateAccesssory(GearType gearType)
        {
            switch (gearType)
            {
                case GearType.EAR:
                    return _ear;
                case GearType.NECK:
                    return _neck;
                case GearType.BRACELET:
                    return _bracelet;
                case GearType.RING1:
                    return _ring1;
                case GearType.RING2:
                    return _ring2;
                default:
                    throw new InvalidOperationException("Unknown gear type");
            }
        }

        private BodyEquipment locateBodyEquipment(GearType gearType)
        {
            BodyEquipment equipmentToChange;
            switch (gearType)
            {
                case GearType.WEAPON:
                    equipmentToChange = _weapon;
                    break;
                case GearType.HEAD:
                    equipmentToChange = _head;
                    break;
                case GearType.CHEST:
                    equipmentToChange = _chest;
                    break;
                case GearType.HAND:
                    equipmentToChange = _hand;
                    break;
                case GearType.LEG:
                    equipmentToChange = _leg;
                    break;
                case GearType.FOOT:
                    equipmentToChange = _foot;
                    break;
                default:
                    throw new InvalidOperationException("Unknown gear type");
            }

            return equipmentToChange;
        }


        public List<int> getEncriptionMessage()
        {
            List<int> equipToIDList = new List<int>();
            List<BodyEquipment> equipments = getBodyEquipmentList();
            foreach (BodyEquipment equipment in equipments)
            {
                equipToIDList.Add(equipment.getID());

                // Add Dye1 ID
                equipToIDList.Add(getDyeID(equipment.getDye1()));
                // Add Dye2 ID
                equipToIDList.Add(getDyeID(equipment.getDye2()));
            }

            List<Accessory> accessories = getAccessoriesList();
            foreach (Accessory accessory in accessories)
            {
                equipToIDList.Add(accessory.getID());
            }

            return equipToIDList;
        }

        private List<Accessory> getAccessoriesList()
        {
            return new List<Accessory>() { _ear, _neck, _bracelet, _ring1, _ring2 };
        }

        private List<BodyEquipment> getBodyEquipmentList()
        {
            return new List<BodyEquipment> { _weapon, _head, _chest, _leg, _foot };
        }

        private int getDyeID(string name)
        {
            return databaseHelper.getDyeIDByName(name);
        }
    }
}
