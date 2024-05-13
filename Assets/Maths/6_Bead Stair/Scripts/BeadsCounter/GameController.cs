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
        public ExampleGestureHandler autoWriting;
        public GestureRecognizer.Recognizer AiRecognizer;

        public bool spritechnagr;

        [Space(10)]
        public SpriteRenderer[] Allanswer;
        public GameObject[] dropPlace;
        public Sprite wrongAnswer;
        public Sprite CurrectAnswer;
        public Sprite NormalAnswer;
        public GameObject gamecompleted;
        public int level;
        public GameObject cover;
        // public GameObject DrawCanvas;
        public SpriteRenderer[] allbeads;
        public List<int> allno = new List<int>();
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
            //cover.transform.SetAsLastSibling();
          //  Allanswer[level].transform.SetAsLastSibling();
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
            Allanswer[level].transform.GetChild(0).GetComponent<TextMeshPro>().text = autoWriting.no.ToString();
            if (autoWriting.no == (level + 1))
            {
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
            if (Mathf.Abs(Selected.transform.position.x-dropPlace[level].transform.position.x) < distace.x &&
                Mathf.Abs(Selected.transform.position.y - dropPlace[level].transform.position.y) < distace.y)
            {
                if(Selected.no == level+1)
                {
                    if (!spritechnagr)
                    {//answer sprite will replace with another sprite

                        Allanswer[level].sprite = CurrectAnswer;
                        Allanswer[level].transform.GetChild(0).GetComponent<TextMeshPro>().text = (level + 1).ToString();
                        //  AiRecognizer.Recognigingnumber = (level + 1).ToString();
                        //AiRecognizer.Changerecogniger();
                        Selected.gameObject.SetActive(false);
                    }
                    else//answer sprite will replace with this sprite
                    {
                        Selected.GetComponent<BoxCollider2D>().enabled = false;
                        Selected.transform.position = dropPlace[level].transform.position;
                        dropPlace[level].GetComponent<SpriteRenderer>().color = Color.green;
                        if(level<9)
                            dropPlace[level+1].GetComponent<SpriteRenderer>().color = Color.blue;
                        text.text = "Drag Beads No " + (level + 2).ToString();

                    }
                    if (level == 9)
                    {
                        //Game Completed

                        StartCoroutine(WaitforLevelComplete());
                        //return;

                    }
                    else
                        level++;

                    Selected = null;
                    StartCoroutine(WaitForNextBead());

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
                        dropPlace[level].GetComponent<SpriteRenderer>().color = Color.red;
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
            yield return new WaitForSeconds(3);
            DrawCanvas.SetActive(true);
            Party_pop.SetActive(false);

           // autoWriting.textResult = Allanswer[level].transform.GetChild(0).GetComponent<TextMeshPro>();
            allbeads[level].sortingOrder = 2;
            Allanswer[level].sortingOrder = 2;
            Allanswer[level].transform.GetChild(0).GetComponent<TextMeshPro>().sortingOrder = 2;
            gamePlay = true;
            //cover.transform.SetAsLastSibling();
            // Allanswer[level].transform.SetAsLastSibling();
        }

        IEnumerator WaitforLevelComplete()
        {
            // DrawCanvas.SetActive(false);
            gamePlay = false;
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }

        IEnumerator WrongAnswerAnimation()
        {
            //   DrawCanvas.SetActive(false);
            gamePlay = false;
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            dropPlace[level].GetComponent<SpriteRenderer>().color = Color.white;
            Selected.transform.position = Selected.lastpos;
            Selected = null;
            autoWriting.textResult = Allanswer[level].transform.GetChild(0).GetComponent<TextMeshPro>();
//DrawCanvas.SetActive(true);
            Allanswer[level].sprite = NormalAnswer;
            autoWriting.textResult.text = "";
            wrongAnswer_animtion.SetActive(false);
            gamePlay = true;
        }
    }
}
