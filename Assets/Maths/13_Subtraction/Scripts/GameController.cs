using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Maths.Substraction.Caluculation
{

    public class GameController : Singleton<GameController>
    {
        public Totorial totoralcheck;
        public SpriteRenderer BreadIcon1;
        public SpriteRenderer BreadIcon2;
        public TextMeshPro[] Inputtext;
        public ExampleGestureHandler AiHandler;
        public Image background;
        public SpriteRenderer Minus;
        public SpriteRenderer Equal;
        public SpriteRenderer OptBG;
        public GestureRecognizer.Recognizer Ai_recognizer;
        public bool Drag;
        public Answer[] allanswer;
        public int[] number;
        public Draging[] option;
        public int no;
        private int relod;
        public Sprite[] randomsprite;
        public Sprite[] randombackground;
        public Sprite[] randomAnsBG;
        public Sprite[] randomOptBG;
        public Sprite[] randomMinus;
        public Sprite[] randomEqual;
        public Sprite[] randomWrongAnswer;
        public Sprite[] randomcurrectAnswer;
        public Color[] randomTextColor;
        public Sprite[] Inputsprite;
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        int RandomNo;
        [SerializeField] float duration;
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        public Color selctedcolr;
        IEnumerator WrongAnswerAnimation()
        {
            wrongAnswer_animtion.SetActive(true);
            Inputtext[no].text = AiHandler.no.ToString();
            yield return new WaitForSeconds(2);
            AiHandler.textResult = Inputtext[no];
            Inputtext[no].transform.parent.GetComponent<SpriteRenderer>().color = selctedcolr;
            Inputtext[no].text = "?";
            Inputtext[no].transform.parent.GetComponent<SpriteRenderer>().sprite = Inputsprite[RandomNo];
            wrongAnswer_animtion.SetActive(false);
            gamePlay = true;
        }
        private void Start()
        {
            relod++;
            no = 0;
            AiHandler.textResult = Inputtext[0];
            number[0] = Random.Range(5, allanswer.Length - 2);
            number[1] = Random.Range(0, allanswer.Length);

            Ai_recognizer.Recognigingnumber = (number[0] + 1).ToString();
            Ai_recognizer.Changerecogniger();
            while (number[0] <= number[1])
            {
                number[1] = Random.Range(0, allanswer.Length);
            }
            number[2] = (number[0] + 1) - (number[1] + 1);
            if (Drag)
            {
                int k = 0;
                int n = Random.Range(0, 2);
                int n2 = Random.Range(2, 5);
                for (int d = 0; d < option.Length; d++)
                {
                    if (d == n || d == n2)
                    {
                        option[d].no = Random.Range(0, 10);
                        for (int i = 0; i < number.Length; i++)
                        {
                            if (option[d].no == number[i])
                            {
                                option[d].no = Random.Range(0, 10);
                                i = -1;
                            }
                        }
                    }
                    else
                    {
                        if (k < 2)
                        {
                            option[d].no = number[k] + 1;
                        }
                        else
                        {
                            option[d].no = number[k];
                        }
                        k++;
                    }
                    option[d].Textchange();
                }
            }
            int no2 = Random.Range(0, randomsprite.Length);
            for (int i = 0; i <= number[0]; i++)
            {
                BreadIcon1.transform.GetChild(i).gameObject.SetActive(true);
                BreadIcon1.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = randomsprite[no2];
            }


            for (int i = 0; i <= number[1]; i++)
            {
                BreadIcon2.transform.GetChild(i).gameObject.SetActive(true);
                BreadIcon2.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = randomsprite[no2];
            }
            RandomNo = no2;
            background.sprite = randombackground[no2];

            Minus.sprite = randomMinus[no2];
            Equal.sprite = randomEqual[no2];
            OptBG.sprite = randomOptBG[no2];
            for (int k = 0; k < option.Length; k++)
            {
                option[k].transform.GetComponentInChildren<TextMeshPro>().color = randomTextColor[no2];
            }

            Inputtext[0].transform.parent.GetComponent<SpriteRenderer>().sprite = Inputsprite[no2];
            Inputtext[1].transform.parent.GetComponent<SpriteRenderer>().sprite = Inputsprite[no2];
            Inputtext[2].transform.parent.GetComponent<SpriteRenderer>().sprite = Inputsprite[no2];

        }


        public bool Neartodestination(GameObject obj)
        {
            if (Vector3.Distance(Inputtext[no].transform.position, obj.transform.position) < 3)
            {
                AiHandler.no = obj.GetComponent<Draging>().no;
                gamePlay = false;
                NextButton();
                return true;
            }

            return false;
        }
        public void NextButton()
        {
            if (no < 2)
            {
                if (AiHandler.no == (number[no] + 1))
                {
                    gamePlay = true;
                    allanswer[number[no]].box = randomcurrectAnswer[RandomNo];
                    Inputtext[no].transform.parent.GetComponent<SpriteRenderer>().sprite = allanswer[number[no]].box;

                    no++;
                    Ai_recognizer.Recognigingnumber = no == 1 ? (number[no] + 1).ToString() : number[no].ToString();
                    Inputtext[no - 1].transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                    Inputtext[no].transform.parent.GetComponent<SpriteRenderer>().color = selctedcolr;
                    Ai_recognizer.Changerecogniger();
                    AiHandler.textResult = Inputtext[no];

                    if (Drag)
                    {
                        Inputtext[no - 1].text = (number[no - 1] + 1).ToString();

                    }
                }
                else
                {
                    AiHandler.textResult = null;
                    Inputtext[no].transform.parent.GetComponent<SpriteRenderer>().sprite = randomWrongAnswer[RandomNo];
                    StartCoroutine(WrongAnswerAnimation());
                }
            }
            else
            {
                if (AiHandler.no == (number[no]))
                {
                    Inputtext[no].transform.parent.GetComponent<SpriteRenderer>().sprite = randomcurrectAnswer[RandomNo];
                    AiHandler.textResult = null;
                    if (relod > 10)
                    {
                        StartCoroutine(LevelCompleted());
                        return;
                    }
                    if (Drag)
                    {
                        Inputtext[no].text = (number[no]).ToString();
                    }
                    StartCoroutine(Relod());
                }
                else
                {
                    AiHandler.textResult = null;
                    Inputtext[no].transform.parent.GetComponent<SpriteRenderer>().sprite = randomWrongAnswer[RandomNo];
                    StartCoroutine(WrongAnswerAnimation());
                }
            }
        }

        public GameObject canvsobj;
        IEnumerator Relod()
        {
            canvsobj.SetActive(false);
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(duration);
            canvsobj.SetActive(true);
            for (int i = 0; i <= number[0]; i++)
            {
                BreadIcon1.transform.GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i <= number[1]; i++)
            {
                BreadIcon2.transform.GetChild(i).gameObject.SetActive(false);
            }
            allanswer[number[no]].box = randomcurrectAnswer[RandomNo];
            Inputtext[no].transform.parent.GetComponent<SpriteRenderer>().sprite = allanswer[number[no]].box;
            for (int i = 0; i < Inputtext.Length; i++)
            {
                Inputtext[i].transform.parent.GetComponent<SpriteRenderer>().sprite = Inputsprite[RandomNo];
                Inputtext[i].transform.parent.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                Inputtext[i].text = "";

            }
            Inputtext[0].transform.parent.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            Party_pop.SetActive(false);
            relod++;
            no = 0;
            AiHandler.textResult = Inputtext[0];
            Inputtext[0].transform.parent.GetComponent<SpriteRenderer>().color = selctedcolr;
            number[0] = Random.Range(5, allanswer.Length - 2);
            number[1] = Random.Range(0, allanswer.Length);
            Ai_recognizer.Recognigingnumber = (number[0] + 1).ToString();
            Ai_recognizer.Changerecogniger();
            while (number[0] <= number[1])
            {
                number[1] = Random.Range(0, allanswer.Length);
            }
            number[2] = (number[0] + 1) - (number[1] + 1);
            int no2 = Random.Range(0, randomsprite.Length);
            for (int i = 0; i <= number[0]; i++)
            {
                BreadIcon1.transform.GetChild(i).gameObject.SetActive(true);
                BreadIcon1.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = randomsprite[no2];
            }
            if (Drag)
            {
                int k = 0;
                int n = Random.Range(0, 2);
                int n2 = Random.Range(2, 5);
                for (int d = 0; d < option.Length; d++)
                {
                    option[d].gameObject.SetActive(true);
                    if (d == n || d == n2)
                    {
                        option[d].no = Random.Range(0, 10);
                    }
                    else
                    {
                        if (k < 2)
                        {
                            option[d].no = number[k] + 1;
                        }
                        else
                        {
                            option[d].no = number[k];
                        }
                        k++;
                    }
                    option[d].Textchange();

                }
            }
            for (int i = 0; i <= number[1]; i++)
            {
                BreadIcon2.transform.GetChild(i).gameObject.SetActive(true);
                BreadIcon2.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = randomsprite[no2];
            }
            RandomNo = no2;
            background.sprite = randombackground[no2];
            Minus.sprite = randomMinus[no2];
            Equal.sprite = randomEqual[no2];
            OptBG.sprite = randomOptBG[no2];
            for (int k = 0; k < option.Length; k++)
            {
                option[k].transform.GetComponentInChildren<TextMeshPro>().color = randomTextColor[no2];
            }
            Inputtext[0].transform.parent.GetComponent<SpriteRenderer>().sprite = Inputsprite[no2];
            Inputtext[1].transform.parent.GetComponent<SpriteRenderer>().sprite = Inputsprite[no2];
            Inputtext[2].transform.parent.GetComponent<SpriteRenderer>().sprite = Inputsprite[no2];
            gamePlay = true;
        }
    }
}