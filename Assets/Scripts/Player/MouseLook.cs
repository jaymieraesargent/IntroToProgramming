using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MouseLook : MonoBehaviour
    {
        public enum MEH
        {
            Horizontal,
            Vertical
        }
        public MEH meh;
        private void Start()
        {
            if (meh == MEH.Horizontal)
            {
                meh = MEH.Vertical;
            }
        }
        private void Update()
        {
            if (meh == MEH.Vertical)
            {
                //run Vertical behaviour
            }
            else
            {
                //run Horizontal behaviour 
            }
        }
    }

}
public enum GAMESTATE
{
    PreGame,
    Game,
    PostGame
}
