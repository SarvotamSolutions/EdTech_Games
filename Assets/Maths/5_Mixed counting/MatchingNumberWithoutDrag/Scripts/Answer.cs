using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.Number1to10.WithoutDrag
{
    public class Answer : MonoBehaviour
    {
        public int answerno;



        public void SlectedAnswer()
        {
            
            GameManager.Instance.gamePlay = false;
            if(GameManager.Instance.Answer == answerno)
            {

                GameManager.Instance.CurrectAnswer(GetComponent<SpriteRenderer>());

            }
            else
            {
                GameManager.Instance.WrongAnswer(GetComponent<SpriteRenderer>());
            }
        }

        private void OnMouseDown()
        {
            if (!GameManager.Instance.gamePlay)
                return;
            SlectedAnswer();
        }
    }
}