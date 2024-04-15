using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Laguage.beginning_sounds.Idintfyletter
{
    public class GameController : GameControllerforAll 
    {
        public SpriteRenderer arrow;

        public override void GameStart()
        {
            base.GameStart();

             
        }
        public override bool Neartodestination()
        {
            if (base.Neartodestination())
            {

                if(letter == selectedoption.GetComponent<DragerForall>().no)
                {
                    arrow.color = currect_answer_color;
                    
                }
                else
                {
                    arrow.color = wrong_answer_color;
                    StartCoroutine(WaitWrongAnimtion());
                }
                return true;
            }
            return base.Neartodestination();

        }

        public override void ResetingDrage()
        {
            base.ResetingDrage();
            arrow.color = Color.white;


        }
    }
}