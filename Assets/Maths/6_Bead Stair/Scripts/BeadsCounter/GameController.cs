using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
namespace Maths.BeadStair.NumberSelction
{
    public class GameController : Singleton<GameController>
    {
        public Totorial totorialcheck;
        public ExampleGestureHandler autoWriting;
        public GestureRecognizer.Recognizer AiRecognizer;

        public bool spritechnagr;

        [Space(10)]
        public SpriteRenderer[] Allanswer;
        public Sprite[] allcurrectanswer, allwronganswer;
        public GameObject[] dropPlace;
        public Sprite wrongAnswer;
        public Sprite CurrectAnswer;
        public Sprite NormalAnswer;
        public GameObject gamecompleted;
        public int level;
        public GameObject cover;
        public Sprite selectedbox;
        public SpriteRenderer[] allbeads;
        public List<int> allno = new List<int>();
        public float curretanswerInterval, wronganswerinterval;
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        private void Start()
        {
            AiRecognizer.Recognigingnumber = (level + 1).ToString();
            AiRecognizer.Changerecogniger();
            autoWriting.textResult = Allanswer[level].transform.GetChild(0).GetComponent<TextMeshPro>();
            allbeads[level].sortingOrder = 2;
        }


        public void COnform()
        {
            if (level == 9)
            {
                StartCoroutine(WaitforLevelComplete());
                return;
            }
            Allanswer[level].transform.GetChild(0).GetComponent<TextMeshPro>().text = autoWriting.no.ToString();
            if (autoWriting.no == (level + 1))
            {
                Allanswer[level].transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
                Allanswer[level].sprite = CurrectAnswer;
                level++;
                AiRecognizer.Recognigingnumber = (level + 1).ToString();
                AiRecognizer.Changerecogniger();
                autoWriting.textResult = Allanswer[level].transform.GetChild(0).GetComponent<TextMeshPro>();
                StartCoroutine(WaitForNextBead());

            }
            else
            {

                autoWriting.textResultUI = null;
                Allanswer[level].GetComponent<Image>().sprite = wrongAnswer;

                StartCoroutine(WrongAnswerAnimation());
            }

        }
        public Drager Selected;
        public Vector2 distace = Vector2.one;
        public TextMeshPro text;
        public bool NeartoDestination()
        {
            if (!Selected)
                return false;
            if (Mathf.Abs(Selected.transform.position.x - dropPlace[level].transform.position.x) < distace.x &&
                Mathf.Abs(Selected.transform.position.y - dropPlace[level].transform.position.y) < distace.y)
            {
                if (Selected.no == level + 1)
                {
                    if (!spritechnagr)
                    {
                        Allanswer[level].sprite = CurrectAnswer;
                        Allanswer[level].transform.GetChild(0).GetComponent<TextMeshPro>().text = (level + 1).ToString();
                        Selected.gameObject.SetActive(false);
                    }
                    else//answer sprite will replace with this sprite
                    {
                        Selected.GetComponent<BoxCollider2D>().enabled = false;
                        Selected.transform.position = dropPlace[level].transform.position;

                        dropPlace[level].GetComponent<SpriteRenderer>().sprite = allcurrectanswer[level];
                        text.text = "Drag Beads No " + (level + 2).ToString();

                    }
                    if (level == 9)
                    {
                        StartCoroutine(WaitforLevelComplete());
                    }
                    else
                    {
                        level++;
                        StartCoroutine(WaitForNextBead());
                    }

                    Selected = null;


                }
                else
                {
                    if (!spritechnagr)
                    {
                        Allanswer[level].sprite = wrongAnswer;
                        Allanswer[level].transform.GetChild(0).GetComponent<TextMeshPro>().text = Selected.no.ToString();
                    }
                    else
                    {
                        Selected.transform.position = dropPlace[level].transform.position;
                        dropPlace[level].GetComponent<SpriteRenderer>().sprite = allwronganswer[Selected.no];

                    }
                    StartCoroutine(WrongAnswerAnimation());
                }

                return true;
            }
            return false;
        }
        public GameObject DrawCanvas;
        IEnumerator WaitForNextBead()
        {
            gamePlay = false;
            DrawCanvas.SetActive(false);
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(curretanswerInterval);
            DrawCanvas.SetActive(true);
            Party_pop.SetActive(false);


            allbeads[level].color = new Color(1, 1, 1, 1);
            Allanswer[level].color = new Color(1, 1, 1, 1);
            Allanswer[level].sprite = selectedbox;

            gamePlay = true;
        }

        IEnumerator WaitforLevelComplete()
        {
            gamePlay = false;
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }

        IEnumerator WrongAnswerAnimation()
        {
            gamePlay = false;
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(wronganswerinterval);
            dropPlace[level].GetComponent<SpriteRenderer>().color = Color.white;
            Selected.transform.position = Selected.lastpos;
            Selected = null;
            autoWriting.textResult = Allanswer[level].transform.GetChild(0).GetComponent<TextMeshPro>();
            Allanswer[level].sprite = NormalAnswer;
            autoWriting.textResult.text = "";
            wrongAnswer_animtion.SetActive(false);
            gamePlay = true;
        }
    }
}
