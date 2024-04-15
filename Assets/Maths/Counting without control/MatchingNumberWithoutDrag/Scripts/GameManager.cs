using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.Number1to10.WithoutDrag
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instace;
        public Sprite[] Allquestion;
        public GameObject[]Option;
        public Sprite CurrectAnswer_sprite;
        public Sprite wronganswer_sprite;
        public Sprite Normal_sprite;
        public int Answer;
        public SpriteRenderer QuestionSprite;
        public List<int> AnsweredQuestion = new List<int>();

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        private void Awake()
        {
            instace = this;
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
            QuestionSprite.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
            StartCoroutine(GoingNextlevel());
        }
        IEnumerator GoingNextlevel()
        {
            yield return new WaitForSeconds(3f);
            Party_pop.SetActive(false);
            for (int i = 0; i < Option.Length; i++)
            {
                Option[i].GetComponent<SpriteRenderer>().sprite = Normal_sprite;

            }
            QuestionSprite.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.5f);
            Reseting();
        }
        public void WrongAnswer(SpriteRenderer obj)
        {
           
          
            obj.sprite = wronganswer_sprite;
            QuestionSprite.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(WrongAnswerReseting());
        }
        IEnumerator WrongAnswerReseting()
        {
            yield return new WaitForSeconds(.5f);
           
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            for (int i = 0; i < Option.Length; i++)
            {

                Option[i].GetComponent<SpriteRenderer>().sprite = Normal_sprite;
            }
            QuestionSprite.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            wrongAnswer_animtion.SetActive(false);
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
            int[] tempsavedAnswer = { 0, 0, 0 };
            tempsavedAnswer[currectAnswerOption] = Answer;
            for (int i = 0; i < 3; i++)
            {
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
        }
        public void ButtonSelect()
        {



        }

    }
}
