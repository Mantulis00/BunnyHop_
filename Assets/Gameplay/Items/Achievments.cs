using Assets.Memory.Adaptors;
using Assets.Memory.Models;

namespace Assets.Gameplay.Items
{
    public class Achievments
    {
        private ProgressModel progress;

        public float lastDistance { get; private set; }

        public float maxDistance { get; private set; }

        public Achievments()
        {
            progress = ProgressAdaptor.LoadProgress();

            lastDistance = progress.lastDistance;
            maxDistance = progress.maxDistance;
        }

        public void  DistanceUpdate(float lastDistance)
        {

            if (lastDistance >= maxDistance)
            {
                maxDistance = (int)lastDistance;
            }
            this.lastDistance = (int)lastDistance;

        }

        public void Save()
        {
             ProgressAdaptor.SaveProgress(lastDistance, maxDistance);
        }



    }
}
