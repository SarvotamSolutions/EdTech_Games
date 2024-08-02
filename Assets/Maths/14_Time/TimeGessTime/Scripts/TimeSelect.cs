using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Maths.Times.Selecttime
{
    public class TimeSelect : MonoBehaviour
    {


        public int hour, Minits;

        private void OnMouseUpAsButton()
        {
            if (!GameController.Instance.gamePlay || GameController.Instance.totorial.totorialplaying)
                return;
            GameController.Instance.gamePlay = false;
            if (GameController.Instance.HourCout == hour && GameController.Instance.MinitCount == Minits)
            {
                GetComponent<SpriteRenderer>().sprite = GameController.Instance.currectanswer;
                GetComponentInChildren<TextMeshPro>().color = Color.white;
                StartCoroutine(GameController.Instance.WaitforRelod());

            }
            else
            {

                GetComponent<SpriteRenderer>().sprite = GameController.Instance.wrongAnswer;
                GetComponentInChildren<TextMeshPro>().color = Color.white;
                StartCoroutine(WrongAnswerAnimtion());
            }
        }
        IEnumerator WrongAnswerAnimtion()
        {
            GameController.Instance.wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            GameController.Instance.wrongAnswer_animtion.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = GameController.Instance.NormalAnswer;
            GetComponentInChildren<TextMeshPro>().color = new Color(0.2745098f, 0.4745098f, 0.6117647f, 1);
            GameController.Instance.gamePlay = true;
        }
    }
}