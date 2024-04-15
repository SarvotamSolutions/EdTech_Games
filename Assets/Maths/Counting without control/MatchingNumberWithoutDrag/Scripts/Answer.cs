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
            if(GameManager.instace.Answer == answerno)
            {

                GameManager.instace.CurrectAnswer(GetComponent<SpriteRenderer>());

            }
            else
            {
                GameManager.instace.WrongAnswer(GetComponent<SpriteRenderer>());
            }
        }

        private void OnMouseDown()
        {
            SlectedAnswer();
        }
    }
}