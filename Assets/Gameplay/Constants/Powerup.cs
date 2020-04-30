using UnityEngine;

namespace Assets.Gameplay.Constants
{
    public static class Powerup
    {

        public static Vector3 Boost()
        {
            return new Vector3(10f, 0, 0);
           
        }

        public static Vector3 Jump()
        {
            return new Vector3(0, 30f, 0);
        }

    }
}
