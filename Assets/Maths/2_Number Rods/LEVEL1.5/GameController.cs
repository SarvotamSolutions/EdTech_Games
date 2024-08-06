
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Maths.NumberRoads.Building_Number_rods
{
    public class GameController : Singleton<GameController>
    {
        public Totorial totrial;
        public ExampleGestureHandler ai;
        public GestureRecognizer.Recognizer aireconniger;
        public GameObject Hodler;
        public GameObject[] option;
        public TextMeshPro[] alltext;
        public GameObject[] question;
        public GameObject drawCanvas;
        public Sprite curerctanswer;
        public bool Ai;
        public int no;
        public Sprite selectedrod;
        public Vector2 distacechec = Vector2.one;
        public Sprite normalinput;
        public GameObject TextHolder;// all text parent
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        public  IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }

        public bool Neartodestination(GameObject obj)
        {
            if(Mathf.Abs(question[no].transform.position.x-obj.transform.position.x) <distacechec.x
                && Mathf.Abs(question[no].transform.position.y - obj.transform.position.y) < distacechec.y)
            {
                return true;
            }
            return false;

        }
       
        public void CurrectAnswer()
        {

            if(ai.no == no+1)
            {
                TextHolder.SetActive(false);
                drawCanvas.SetActive(false);

                alltext[no].transform.parent.GetComponent<SpriteRenderer>().sprite = normalinput;
                ai.textResult = null;
                Hodler.SetActive(true);
                no++;

                if (no >= 10)
                {
                    StartCoroutine(LevelCompleted());
                    return;
                }
                else
                {
                    question[no].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
                for (int i = question[no].transform.childCount - 1; i >= 0; i--)
                {
                    question[no].transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = selectedrod;
                }
            }
        }
    }
}