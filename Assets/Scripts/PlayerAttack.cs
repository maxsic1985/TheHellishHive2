using UnityEngine;

namespace Skills
{
    public class PlayerAttack : MonoBehaviour
    {
        private static PlayerAttack instance;


        public static PlayerAttack Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<PlayerAttack>();
                }

                return instance;
            }
        }
        
        public  int SimpleAttack()
        {
            print("simple");
            return PlayerHelper.Instance.Atack;
        }
        
    }
}