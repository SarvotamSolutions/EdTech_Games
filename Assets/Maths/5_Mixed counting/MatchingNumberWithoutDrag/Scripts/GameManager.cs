using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Maths.Number1to10.WithoutDrag
{
    public class GameManager : Singleton<GameManager>
    {
        public Totorial totorialcheck;
        public Sprite[] Allquestion;
        public Sprite[] allbackground,holdingsprite;
        public Image background;
        public GameObject[]Option;
        public Color[] textCollor;
        public Sprite CurrectAnswer_sprite;
        public Sprite wronganswer_sprite;
        public Sprite Normal_sprite;
        public int Answer;
        public SpriteRenderer QuestionSprite, answerhodling;
        public List<int> AnsweredQuestion = new List<int>();

        public int currectanswerintevel,wronganswerinterval;
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        private void Awake()
        {
       
        }
        private void Start()
        {


            Reseting();
        }
        public void CurrectAnswer(SpriteRenderer obj)
        {
            
            for (int i = 0; i < Option.Length; i++)
            {

                Option[i].GetComponent<SpriteRenderer>().sprite = Normal_sprite;
            }
            Party_pop.SetActive(true);
            obj.sprite = CurrectAnswer_sprite;
            obj.transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
            
            QuestionSprite.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
            StartCoroutine(GoingNextlevel());
        }
        IEnumerator GoingNextlevel()
        {
            yield return new WaitForSeconds(currectanswerintevel);
            Party_pop.SetActive(false);
            for (int i = 0; i < Option.Length; i++)
            {
                Option[i].GetComponent<SpriteRenderer>().sprite = Normal_sprite;

            }
            QuestionSprite.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            //yield return new WaitForSeconds(.5f);
            Reseting();
        }
        public void WrongAnswer(SpriteRenderer obj)
        {
           
          
            obj.sprite = wronganswer_sprite;
            obj.transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
            QuestionSprite.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(WrongAnswerReseting());
        }
        IEnumerator WrongAnswerReseting()
        {
            yield return new WaitForSeconds(.5f);
           
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(wronganswerinterval);
            for (int i = 0; i < Option.Length; i++)
            {

                Option[i].GetComponent<SpriteRenderer>().sprite = Normal_sprite;
                Option[i].GetComponentInChildren<TextMeshPro>().color = textCollor[Answer];
            }
           
            QuestionSprite.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            wrongAnswer_animtion.SetActive(false);
            gamePlay = true;
        }
        IEnumerator GameCompletedAnimationDone()
        {
            yield return new WaitForSeconds(2);
            gameCompleted_animation.SetActive(false);
            SceneManager.LoadScene(0);
        }
        public void Reseting()
        {
            Answer = Random.Range(0, Allquestion.Length);

            if (AnsweredQuestion.Count >= Allquestion.Length)
            {
                Debug.Log("Go to Next level");
                gameCompleted_animation.SetActive(true);
                StartCoroutine(GameCompletedAnimationDone());
                return;
            }
            for (int i = 0; i < AnsweredQuestion.Count; i++)
            {

                if (Answer == AnsweredQuestion[i])
                {
                    i = -1;
                    Answer = Random.Range(0, Allquestion.Length);

                }

            }
            AnsweredQuestion.Add(Answer);
            QuestionSprite.sprite = Allquestion[Answer];
            int currectAnswerOption = Random.Range(0, 3);
            int[] tempsavedAnswer = { 0, 0, 0,0 };
            tempsavedAnswer[currectAnswerOption] = Answer;
            for (int i = 0; i < 4; i++)
            {
                Option[i].transform.GetChild(0).GetComponent<TextMeshPro>().color = textCollor[Answer];
                if (i == currectAnswerOption)
                {
                    //answer is currect 
                    Option[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = ""+ (Answer+1);
                    Option[i].GetComponent<Answer>().answerno = Answer;

                }
                else
                {
                    int no = Random.Range(0, Allquestion.Length);
                    for (int j = 0; j < tempsavedAnswer.Length; j++)
                    {

                        if (no == tempsavedAnswer[j])
                        {
                            no = Random.Range(0, Allquestion.Length);
                            j = -1;
                        }

                    }
                    tempsavedAnswer[i] = no;
                    Option[i].GetComponent<Answer>().answerno = no;
                    Option[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = "" + (no + 1);
                }
                
            }
            gamePlay = true;
            background.sprite = allbackground[Answer];
            answerhodling.sprite = holdingsprite[Answer];
        }
        public void ButtonSelect()
        {



        }

    }
}
