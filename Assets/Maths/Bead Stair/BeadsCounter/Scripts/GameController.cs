using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
namespace Maths.BeadStair.NumberSelction
{
    public class GameController : MonoBehaviour
    {
        public ExampleGestureHandler autoWriting;
        public GestureRecognizer.Recognizer AiRecognizer;


        [Space(10)]
        public GameObject[] Allanswer;
        public Sprite wrongAnswer;
        public Sprite CurrectAnswer;
        public Sprite NormalAnswer;
        public GameObject gamecompleted;
        public int level;
        public GameObject cover;
       // public GameObject DrawCanvas;


        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        private void Start()
        {
            AiRecognizer.Recognigingnumber = (level + 1).ToString();
            AiRecognizer.Changerecogniger();
            autoWriting.textResultUI = Allanswer[level].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            cover.transform.SetAsLastSibling();
            Allanswer[level].transform.SetAsLastSibling();
        }
        private void Update()
        {

        }

        public void COnform()
        {
            if (level == 9)
            {
                //Game Completed

                StartCoroutine(WaitforLevelComplete());
                return;

            }
            Allanswer[level].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = autoWriting.no.ToString();
            if (autoWriting.no == (level + 1))
            {
                Allanswer[level].GetComponent<Image>().sprite = CurrectAnswer;
                level++;
                AiRecognizer.Recognigingnumber = (level + 1).ToString();
                AiRecognizer.Changerecogniger();
                autoWriting.textResultUI = Allanswer[level].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                StartCoroutine(WaitForNextBead());
               
            }
            else
            {
               
                autoWriting.textResultUI = null;
                Allanswer[level].GetComponent<Image>().sprite = wrongAnswer;

                StartCoroutine(WrongAnswerAnimation());
            }

        }
        public GameObject DrawCanvas;
        IEnumerator WaitForNextBead()
        {
            DrawCanvas.SetActive(false);
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            DrawCanvas.SetActive(true);
            Party_pop.SetActive(false);
           
           
            cover.transform.SetAsLastSibling();
            Allanswer[level].transform.SetAsLastSibling();
        }

        IEnumerator WaitforLevelComplete()
        {
           // DrawCanvas.SetActive(false);
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }

        IEnumerator WrongAnswerAnimation()
        {
         //   DrawCanvas.SetActive(false);
            yield return new WaitForSeconds(.5f);
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            autoWriting.textResultUI = Allanswer[level].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
//DrawCanvas.SetActive(true);
            Allanswer[level].GetComponent<Image>().sprite = NormalAnswer;
            autoWriting.textResultUI.text = "";
            wrongAnswer_animtion.SetActive(false);
        }
    }
}
