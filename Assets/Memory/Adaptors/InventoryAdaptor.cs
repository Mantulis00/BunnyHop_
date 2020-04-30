using Assets.Memory.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Memory.Adaptors
{
    class InventoryAdaptor
    {
        public static void SaveInventory(int money, int jumps, int boosts)
        {
            BinaryFormatter bf = new BinaryFormatter();
            string path = Application.persistentDataPath + "/inventory.inv";

            FileStream stream = new FileStream(path, FileMode.Create);

            InventoryModel progress = new InventoryModel(money, jumps, boosts);


            bf.Serialize(stream, progress);
            stream.Close();
        }

        public static InventoryModel LoadInventory()
        {
            string path = Application.persistentDataPath + "/inventory.inv";

            if (File.Exists(path))
            {

                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                InventoryModel model = bf.Deserialize(stream) as InventoryModel; // < ---- bug

                stream.Close();

                return model;
            }
            else
            {
                
                InventoryModel model = new InventoryModel();

                SaveInventory(model.money, model.jumps, model.boosts);

                return model;
            }
        }

    }
}
