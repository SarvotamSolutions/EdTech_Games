using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.Number1to100.dragandrop
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;

        public GameObject[] option;
        public GameObject[] question;
        public Sprite answeroption,currectanswer,wronganswer,NormalQuestion;
        int deactive;
        int answerchoice;
        int reloding;
        public int addno;
        public List<int> alloption = new List<int>(12);

        private void Awake()
        {
            instance = this;

        }

        public bool Neartodestination(GameObject obj)
        {
            Debug.Log(Vector3.Distance(obj.transform.position, question[deactive].transform.position));
            if (Vector3.Distance(obj.transform.position, question[deactive].transform.position) < 1)
            {

                if (obj.GetComponent<Drag>().no == deactive+addno + 1)
                {
                    question[deactive].transform.GetChild(0).GetComponent<TextMeshPro>().text = (deactive+addno + 1).ToString();
                    question[deactive].GetComponent<SpriteRenderer>().sprite = currectanswer;
                    if (reloding > 9)
                    {
                        StartCoroutine(LevelCompleted());

                    }
                    
                    addno = Random.Range(0,5)*20;
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


            return false;
        }

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
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
            question[deactive].transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            question[deactive].GetComponent<SpriteRenderer>().sprite = answeroption;
            foreach (var item in option)
            {
                item.SetActive(true);
            }
            wrongAnswer_animtion.SetActive(false);

        }
        IEnumerator Reseting()
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            Party_pop.SetActive(false);
            
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

        }
        void Reloding()
        {

            Debug.Log("XXX");
            deactive = Random.Range(0, question.Length);
            for (int i = 0; i < question.Length; i++)
            {
                question[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = (addno + i + 1).ToString();
            }

          
            question[deactive].transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            question[deactive].GetComponent<SpriteRenderer>().sprite = answeroption;



            answerchoice = Random.Range(0, option.Length);
            alloption[answerchoice] = deactive + 1;
            for (int i = 0; i < option.Length; i++)
            {
                if (answerchoice== i)
                {
                    option[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = (deactive +addno + 1).ToString();
                    option[i].GetComponent<Drag>().no = (deactive+addno + 1);
                }
                else
                {
                    int no = Random.Range(1 + addno, 21 + addno);
                    for (int j = 0; j < alloption.Count; j++)
                    {
                        if (alloption[j] == no)
                        {
                            no = Random.Range(1+addno, 21+addno);
                            j = -1;
                        }

                    }
                    alloption[i] = no;
                    option[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = no.ToString();
                    option[i].GetComponent<Drag>().no = no;
                }



            }
        }
        private void Start()
        {
            Reloding();
            

        }
    }

}