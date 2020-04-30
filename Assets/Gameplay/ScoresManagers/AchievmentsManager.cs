using Assets.Gameplay.Items;

namespace Assets.Gameplay.ScoresManagers
{
    public class AchievmentsManager
    {
        public Achievments achievments { get; private set; } 
        public AchievmentsManager()
        {
            achievments = new Achievments();
            SetupVars();
        }


        float distanceToScore;
        float scoreInc;

        private void SetupVars()
        {
            scoreInc = 10f;
            distanceToScore = 20f;
        }

        public void Restart()
        {
            SetupVars();
        }

        public string GetLastDistance()
        {
            return achievments.lastDistance.ToString();
        }

        public string GetMaxDistance()
        {
            return achievments.maxDistance.ToString();
        }


        public bool CheckSimpleMoneyScore(UnityEngine.Transform player, Inventory inventory)
        {
            achievments.DistanceUpdate( player.position.x );

            if (player.position.x >= distanceToScore)
            {
                inventory.MoneyUpdate();
                distanceToScore += scoreInc;
                return true;
            }
            return false;
        }

        public void Save()
        {
            achievments.Save();
        }


    }
}
