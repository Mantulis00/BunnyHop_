
namespace Assets.Memory.Models
{
    [System.Serializable]
    class InventoryModel
    {
        public int money { get; private set; }
        public int jumps { get; private set; }
        public int boosts { get; private set; }
        
        public InventoryModel(int money, int jumps, int boosts)
        {
            this.money = money;
            this.jumps = jumps;
            this.boosts = boosts;
        }

        public InventoryModel()
        {
            this.money = 0;
            this.jumps = 0;
            this.boosts = 0;
        }
    }
}
