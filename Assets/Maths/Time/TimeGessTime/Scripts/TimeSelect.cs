using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.Times.Selecttime
{
    public class TimeSelect : MonoBehaviour
    {

        public int hour, Minits;

        private void OnMouseUpAsButton()
        {
            if (GameController.instance.HourCout == hour && GameController.instance.MinitCount == Minits)
            {
                GetComponent<SpriteRenderer>().sprite = GameController.instance.currectanswer;
                StartCoroutine(GameController.instance.WaitforRelod());

            }
            else
            {

                GetComponent<SpriteRenderer>().sprite = GameController.instance.wrongAnswer;
                StartCoroutine(WrongAnswerAnimtion());
            }
        }
        IEnumerator WrongAnswerAnimtion()
        {
            GameController.instance.wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            GameController.instance.wrongAnswer_animtion.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = GameController.instance.NormalAnswer;
        }
    }
}