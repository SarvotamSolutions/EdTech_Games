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
            if (!GameController.Instance.gamePlay)
                return;
            GameController.Instance.gamePlay = false;
            if (GameController.Instance.HourCout == hour && GameController.Instance.MinitCount == Minits)
            {
                GetComponent<SpriteRenderer>().sprite = GameController.Instance.currectanswer;
                StartCoroutine(GameController.Instance.WaitforRelod());

            }
            else
            {

                GetComponent<SpriteRenderer>().sprite = GameController.Instance.wrongAnswer;
                StartCoroutine(WrongAnswerAnimtion());
            }
        }
        IEnumerator WrongAnswerAnimtion()
        {
            GameController.Instance.wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            GameController.Instance.wrongAnswer_animtion.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = GameController.Instance.NormalAnswer;
            GameController.Instance.gamePlay = true;
        }
    }
}