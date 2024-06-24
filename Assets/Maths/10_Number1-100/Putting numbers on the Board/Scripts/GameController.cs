using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maths.Number1to100.Putting_numbers_Board
{
    public class GameController : MonoBehaviour
    {
     
        public static GameController instace;
        public ExampleGestureHandler Ai;
        public GestureRecognizer.Recognizer Ai_recognizer;

        public Sprite currectanswer,selectedsprire, notselcted;
        public Numbers selectedno;
        //public GameObject AiCanvas;
        public Numbers[] allnumber;

        public bool randomNo;

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        private void Start()
        {
            Ai_recognizer.Recognigingnumber = selectedno.no.ToString();
            Ai_recognizer.Changerecogniger();
        }
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        IEnumerator WrongAnswerAnimation()
        {
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            wrongAnswer_animtion.SetActive(false);
         //   AiCanvas.SetActive(true);
        }
        private void Awake()
        {

            instace = this;
        }

        public void Conform()
        {
            if(Ai.no == selectedno.no && !selectedno.competed)
            {
                int selectno = 0;
                selectedno.competed = true;
                selectedno.GetComponent<SpriteRenderer>().sprite = currectanswer;
                //Ai.textResult = null;
                while (selectedno.competed == true)
                {
                    Debug.Log(selectedno + "CCC");
                    selectno++;
                    Debug.Log(selectno);
                    if(selectno > 99)
                    {
                        StartCoroutine(LevelCompleted());
                        return;
                    }
                    selectedno = allnumber[selectedno.no%100];
                }
                selectedno.SelectedthisNumber();
            }else if(Ai.no != selectedno.no)
            {
           //     AiCanvas.SetActive(false);
                Ai.textResult.text = "";
                Ai.no = 0;
                StartCoroutine(WrongAnswerAnimation());
            }
        }
    }
}
