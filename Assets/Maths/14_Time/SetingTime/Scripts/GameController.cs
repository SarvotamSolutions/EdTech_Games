using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Maths.Times.ClockTimeSet
{
    public class GameController : MonoBehaviour
    {
        public Totorial totorial;
        public bool GAMEPLAY;
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
        public Button NextButton;

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
            if (totorial.totorialplaying)
                return;
            NextButton.interactable = false;
            GAMEPLAY = false;
            if (questionHour == Hour && QuestionMinit == minit)
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
            GAMEPLAY = true;
            Clock.sprite = NormalClock;
            wrongAnswer_animtion.SetActive(false);
            NextButton.interactable = true;

        }
        IEnumerator Relod()
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            Party_pop.SetActive(false);
            GAMEPLAY = true;
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
            NextButton.interactable = true;

        }
        private void Update()
        {
           
            
        }
    }

}
