using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.TeenBeads.drop10to20
{
    public class GameController : Singleton<GameController>
    {


        public Totorial totorialcheck;
        public Sprite currectanswer;
        public Sprite wrongAnswer;
        public Sprite normalAnswer;
        public TextMeshPro hintText;
        public GameObject[] droppos;
        public SpriteRenderer[] allsprite;
        public float offset = 2;

        public float curectanswerInteval, wronganswerInteval;
        public int no;
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        private void Awake()
        {
            TextUpdate();
        }

        void TextUpdate()
        {

        }

        IEnumerator CurrectAnswer()
        {
            allsprite[no].transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
            no++;

            allsprite[no - 1].sprite = currectanswer;
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(curectanswerInteval);
            Party_pop.SetActive(false);
            if (no >= 9)
            {
                StartCoroutine(LevelCompleted());
                yield return null;
            }
            else
            {
                allsprite[no].transform.GetChild(0).GetComponent<TextMeshPro>().sortingOrder = 4;
                allsprite[no].transform.parent.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                allsprite[no].transform.parent.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            gamePlay = true;
        }

        IEnumerator WrongAnswerAnimation(GameObject obj)
        {
            allsprite[no].sprite = wrongAnswer;
            TextMeshPro temptextesh = allsprite[no].transform.GetChild(0).GetComponent<TextMeshPro>();
            Color tempcolr = temptextesh.color;
            temptextesh.color = Color.white;
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(wronganswerInteval);
            temptextesh.color = tempcolr;
            temptextesh.text = 10.ToString();
            wrongAnswer_animtion.SetActive(false);
            allsprite[no].sprite = normalAnswer;
            obj.transform.position = obj.GetComponent<Drager>().lastpos;
            gamePlay = true;
        }
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }

        public GameObject Neartodestination(GameObject obj)
        {
            if (Vector3.Distance(obj.transform.position, droppos[no].transform.position) < 4f)
            {
                gamePlay = false;
                allsprite[no].transform.GetChild(0).GetComponent<TextMeshPro>().text = (10 + obj.GetComponent<Drager>().no).ToString();
                if (obj.GetComponent<Drager>().no == no + 1)
                {
                    obj.GetComponent<BoxCollider2D>().enabled = false;
                    StartCoroutine(CurrectAnswer());
                    return droppos[no - 1];
                }
                else
                {
                    StartCoroutine(WrongAnswerAnimation(obj));
                }
                return droppos[no];
            }
            return null;
        }
    }
}