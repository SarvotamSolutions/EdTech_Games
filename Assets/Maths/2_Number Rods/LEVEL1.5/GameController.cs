
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Maths.NumberRoads.Building_Number_rods
{
    public class GameController : Singleton<GameController>
    {

        public ExampleGestureHandler ai;
        public GestureRecognizer.Recognizer aireconniger;
        public GameObject Hodler;
        public GameObject[] option;
        public TextMeshPro[] alltext;
        public GameObject[] question;
        public GameObject drawCanvas;
        public bool Ai;
        public int no;

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
       
       
        public void Update()
        {
            
        }
        public Vector2 distacechec = Vector2.one;

        public bool Neartodestination(GameObject obj)
        {
            if(Mathf.Abs(question[no].transform.position.x-obj.transform.position.x) <distacechec.x
                && Mathf.Abs(question[no].transform.position.y - obj.transform.position.y) < distacechec.y)
            {
                return true;
            }
            return false;

        }
        public Sprite normalinput;
        public void CurrectAnswer()
        {
            if(ai.no == no+1)
            {
                //currect answer
                alltext[no].transform.parent.GetComponent<SpriteRenderer>().sprite = normalinput;
                ai.textResult = null;
                drawCanvas.SetActive(false);
                Hodler.SetActive(true);
                no++;

                if (no >= 10)
                    StartCoroutine(LevelCompleted());
                else 
                {
                    question[no].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
            }
            else
            {

                // Wrong Answer
            }
        }

    }

}