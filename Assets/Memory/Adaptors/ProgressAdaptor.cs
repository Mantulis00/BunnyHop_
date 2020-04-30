using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using Assets.Memory.Models;

namespace Assets.Memory.Adaptors
{
    public static class ProgressAdaptor
    {

         public static void SaveProgress(float last, float max)
        {
            string path = Application.persistentDataPath + "/progress.inv";

            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            ProgressModel progress = new ProgressModel(last, max);

            bf.Serialize(stream, progress);
            stream.Close();
        }

        public static ProgressModel LoadProgress()
        {
            string path = Application.persistentDataPath + "/progress.inv";

            if (File.Exists(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                ProgressModel model = bf.Deserialize(stream) as ProgressModel;
                stream.Close();

                return model;
            }
            else
            {
                ProgressModel model = new ProgressModel();

                SaveProgress(model.lastDistance, model.maxDistance);

                return model;
            }
        }



    }
}
