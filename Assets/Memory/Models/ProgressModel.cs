

namespace Assets.Memory.Models
{
    [System.Serializable]
    public class ProgressModel
    {
        public float lastDistance { get; private set; }
        public float maxDistance { get; private set;}

        public ProgressModel(float last, float max)
        {
            lastDistance = last;
            maxDistance = max;
        }

        public ProgressModel()
        {
            lastDistance = 0;
            maxDistance = 0;
        }
    }
}
