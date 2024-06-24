using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


namespace Maths.Times.ClockTimeSet
{
    public class GameController : MonoBehaviour
    {
        public static GameController instace;
        public TextMeshPro[] alltext;
        public SpriteRenderer[] allminits;
        public int minit;
        public int Hour;
        public Sprite blueicon;
        public int questionHour, QuestionMinit;
        public TextMeshPro text;

        public Sprite CurrectClock;
        public Sprite WrongClock;
        public Sprite NormalClock;
        public SpriteRenderer Clock;
        public int rolod;

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
            questionHour = Random.Range(0, 12);
            QuestionMinit = Random.Range(0, 4);
            QuestionMinit = QuestionMinit * 15;
            int hour = questionHour;

            if (hour == 0) hour = 12;

            text.text = hour.ToString("00") + ":" + QuestionMinit.ToString("00");

        }

        public void check()
        {
            if(questionHour == Hour && QuestionMinit == minit)
            {
                Clock.sprite = CurrectClock;
                StartCoroutine(Relod());
            }
            else
            {
                Clock.sprite = WrongClock;
                StartCoroutine(WrongAnswer());
                
            }
        }

        IEnumerator WrongAnswer()
        {
            yield return new WaitForSeconds(.2f);
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            Clock.sprite = NormalClock;
            wrongAnswer_animtion.SetActive(false);
        }
        IEnumerator Relod()
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            Party_pop.SetActive(false);
            rolod++;
            if (rolod > 10)
            {
                gameCompleted_animation.SetActive(true);
                yield return new WaitForSeconds(2);
                SceneManager.LoadScene(0);

            }
            Clock.sprite = NormalClock;
            questionHour = Random.Range(0, 12);
            QuestionMinit = Random.Range(0, 4);
            QuestionMinit = QuestionMinit * 15;

            int hour = questionHour;

            if (hour == 0) hour = 12;

            text.text = hour.ToString("00") + ":" + QuestionMinit.ToString("00");
        }
        private void Update()
        {
           
            
        }
    }

}
