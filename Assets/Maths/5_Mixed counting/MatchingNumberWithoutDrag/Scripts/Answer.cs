using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.Number1to10.WithoutDrag
{
    public class Answer : MonoBehaviour
    {
        public int answerno;
        public AudioSource clicksound;


        public void SlectedAnswer()
        {
            clicksound.Play();
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
            if (!GameManager.Instance.gamePlay || GameManager.Instance.totorialcheck.totorialplaying)
                return;
            SlectedAnswer();
        }
    }
}