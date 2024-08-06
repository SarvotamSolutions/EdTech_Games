using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.Number1to100.dragandrop
{
    public class GameController : Singleton<GameController>
    {
        public Totorial totorialcheck;
        public Color selectcolor;
        public GameObject[] option;
        public GameObject[] question;
        public Sprite answeroption, currectanswer, wronganswer, NormalQuestion;
        int deactive;
        int answerchoice;
        int reloding;
        public int multiplenumber;
        public bool relodOption;
        public int timecanrelod;
        public bool multipleChoice;
        public int addno;
        public List<int> alloption = new List<int>(12);
        public bool randomno;
        public int maxholding;

        public bool Neartodestination(GameObject obj)
        {
            if (multipleChoice)
            {
                if (Vector3.Distance(obj.transform.position, question[alloption[deactive]].transform.position) < 1)
                {
                    if (obj.GetComponent<Drag>().no == alloption[deactive] + 1)
                    {
                        question[alloption[deactive]].transform.GetChild(0).GetComponent<TextMeshPro>().color = selectcolor;// issue is here
                        question[alloption[deactive]].transform.GetChild(0).GetComponent<TextMeshPro>().text = (alloption[deactive] + 1).ToString();
                        question[alloption[deactive]].GetComponent<SpriteRenderer>().sprite = currectanswer;
                        deactive++;
                        obj.gameObject.SetActive(false);

                        if (deactive == option.Length)
                        {
                            if (relodOption)
                            {
                                foreach (var item in option)
                                {
                                    item.SetActive(true);
                                }
                                deactive = 0;
                                addno += 10;
                                if (addno < 100)
                                    RelodOption();
                                else
                                    StartCoroutine(LevelCompleted());
                                for (int i = addno; i < 10 + addno; i++)
                                {
                                    question[i].GetComponent<SpriteRenderer>().color = Color.white;
                                }
                            }
                            else
                                StartCoroutine(LevelCompleted());
                        }
                        if (deactive < maxholding)
                            question[alloption[deactive]].GetComponent<SpriteRenderer>().sprite = NormalQuestion;
                        else
                        {
                            question[alloption[0]].GetComponent<SpriteRenderer>().sprite = NormalQuestion;
                        }

                    }
                    else
                    {
                        question[alloption[deactive]].transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
                        question[alloption[deactive]].transform.GetChild(0).GetComponent<TextMeshPro>().text = (obj.GetComponent<Drag>().no).ToString();
                        question[alloption[deactive]].GetComponent<SpriteRenderer>().sprite = wronganswer;
                        StartCoroutine(wrongAnimationforMultipleChoic());
                    }
                }
            }
            else
            {
                Debug.Log(Vector3.Distance(obj.transform.position, question[deactive].transform.position));
                if (Vector3.Distance(obj.transform.position, question[deactive].transform.position) < 1)
                {
                    if (obj.GetComponent<Drag>().no == deactive + addno + 1)
                    {
                        question[deactive].transform.GetChild(0).GetComponent<TextMeshPro>().text = (deactive + addno + 1).ToString();
                        question[deactive].GetComponent<SpriteRenderer>().sprite = currectanswer;
                        if (reloding > 8)
                        {
                            StartCoroutine(LevelCompleted());
                            return true;
                        }
                        addno = addno + 1 * 10;
                        StartCoroutine(Reseting());
                    }
                    else
                    {
                        question[deactive].transform.GetChild(0).GetComponent<TextMeshPro>().text = obj.GetComponent<Drag>().no.ToString();
                        question[deactive].GetComponent<SpriteRenderer>().sprite = wronganswer;
                        StartCoroutine(WrongAnswerAnimation());
                    }
                    return true;
                }
            }
            return false;
        }

        public float levelCompleteduration = 2f;
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(levelCompleteduration);
            SceneManager.LoadScene(0);
        }
        public void RelodOption()
        {
            for (int i = 0; i < option.Length; i++)
            {
                alloption[i] = Random.Range(addno, option.Length + addno);
                for (int j = 0; j < option.Length; j++)
                {
                    if (alloption[i] == alloption[j] && i != j)
                    {
                        alloption[i] = Random.Range(addno, option.Length + addno);
                        j = -1;
                    }
                }
                option[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = (alloption[i] + 1).ToString();
                option[i].GetComponent<Drag>().no = (alloption[i] + 1);
            }
            alloption.Sort();
            foreach (var item in alloption)
            {
                question[item].transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
                question[item].GetComponent<SpriteRenderer>().sprite = answeroption;
            }
        }
        IEnumerator wrongAnimationforMultipleChoic()
        {
            gamePlay = false;
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            question[alloption[deactive]].transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            question[alloption[deactive]].GetComponent<SpriteRenderer>().sprite = answeroption;
            wrongAnswer_animtion.SetActive(false);
            gamePlay = true;
        }
        IEnumerator WrongAnswerAnimation()
        {
            gamePlay = false;
            question[deactive].transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            question[deactive].transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            question[deactive].GetComponent<SpriteRenderer>().sprite = answeroption;
            foreach (var item in option)
            {
                item.SetActive(true);
            }
            wrongAnswer_animtion.SetActive(false);
            gamePlay = true;
        }
        IEnumerator Reseting()
        {
            gamePlay = false;
            Color tempcolor = question[deactive].transform.GetChild(0).GetComponent<TextMeshPro>().color;
            question[deactive].transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            Party_pop.SetActive(false);
            question[deactive].transform.GetChild(0).GetComponent<TextMeshPro>().color = tempcolor;
            foreach (var item in question)
            {
                item.GetComponent<SpriteRenderer>().sprite = NormalQuestion;
            }
            foreach (var item in option)
            {
                item.SetActive(true);
            }
            reloding++;
            Reloding();
            gamePlay = true;

        }
        void Reloding()
        {
            if (!multipleChoice)
            {
                for (int i = 0; i < question.Length; i++)
                {
                    question[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = (addno + i + 1).ToString();
                }
                deactive = Random.Range(0, question.Length);
                question[deactive].transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
                question[deactive].GetComponent<SpriteRenderer>().sprite = answeroption;
                answerchoice = Random.Range(0, option.Length);
                alloption[answerchoice] = deactive + addno + 1;
                for (int i = 0; i < option.Length; i++)
                {
                    if (answerchoice == i)
                    {
                        option[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = (deactive + addno + 1).ToString();
                        option[i].GetComponent<Drag>().no = (deactive + addno + 1);
                    }
                    else
                    {
                        int no = Random.Range(1 + addno, 11 + addno);
                        for (int j = 0; j < alloption.Count; j++)
                        {
                            if (alloption[j] == no)
                            {
                                no = Random.Range(1 + addno, 11 + addno);
                                j = -1;
                            }
                        }
                        alloption[i] = no;
                        option[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = no.ToString();
                        option[i].GetComponent<Drag>().no = no;
                    }
                }
            }
            else
            {
                if (!relodOption)
                {
                    for (int i = 0; i < question.Length; i++)
                    {
                        question[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = (addno + i + 1).ToString();
                    }
                }

                if (randomno)
                {
                    for (int i = 0; i < option.Length; i++)
                    {
                        alloption[i] = Random.Range(0, question.Length);
                        for (int j = 0; j < option.Length; j++)
                        {
                            if (alloption[i] == alloption[j] && i != j)
                            {
                                alloption[i] = Random.Range(0, option.Length);

                                j = -1;
                            }
                        }
                        option[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = (alloption[i] + 1).ToString();
                        option[i].GetComponent<Drag>().no = (alloption[i] + 1);
                    }
                    alloption.Sort();
                    foreach (var item in alloption)
                    {
                        question[item].transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
                        question[item].GetComponent<SpriteRenderer>().sprite = answeroption;
                    }
                    question[alloption[0]].GetComponent<SpriteRenderer>().sprite = NormalQuestion;
                }
                else
                {
                    if (!relodOption)
                    {
                        for (int i = 0; i < question.Length; i++)
                        {
                            question[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = (addno + i + 1).ToString();
                        }
                    }

                    for (int i = 0; i < option.Length; i++)
                    {
                        alloption[i] = Random.Range(0, option.Length);
                        for (int j = 0; j < option.Length; j++)
                        {
                            if (alloption[i] == alloption[j] && i != j)
                            {
                                alloption[i] = Random.Range(0, option.Length);
                                j = -1;
                            }
                        }
                        option[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = (alloption[i] + 1).ToString();
                        option[i].GetComponent<Drag>().no = (alloption[i] + 1);
                    }
                    alloption.Sort();
                    foreach (var item in alloption)
                    {
                        question[item].transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
                        question[item].GetComponent<SpriteRenderer>().sprite = answeroption;
                    }
                    question[0].GetComponent<SpriteRenderer>().sprite = NormalQuestion;
                }
            }
        }
        private void Start()
        {
            Reloding();
        }
    }
}