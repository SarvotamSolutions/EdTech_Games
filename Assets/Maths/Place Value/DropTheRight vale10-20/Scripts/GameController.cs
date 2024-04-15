using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.placeValue.Dropbeads
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        public TextMeshPro Question_text;
        int amount;
        public GameObject droppostion;
        public Sprite currectAnswer, wrongAnswer, normalAnswer;
        public SpriteRenderer checkAnswer;
        private int levelloaded;
        public TextMeshProUGUI infotext;
  

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            amount = Random.Range(11, 20);
            Question_text.text = amount.ToString();
            infotext.text = "Drag the right bead to make " + amount;
        }
        public bool Neartodestination(GameObject obj)
        {

            if (Vector3.Distance(obj.transform.position, droppostion.transform.position) < 1)
            {

                obj.transform.position = droppostion.transform.position;

                Drager drag = obj.GetComponent<Drager>();
                if(drag.no +10 == amount)
                {
                    checkAnswer.sprite = currectAnswer;
                    StartCoroutine(ResetingLevel(obj));
                }
                else
                {
                    checkAnswer.sprite = wrongAnswer;
                    StartCoroutine(WrongAnswerAnimation(obj));
                }
                return true;
            }

            return false;
        }
        IEnumerator WrongAnswerAnimation(GameObject obj)
        {
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            wrongAnswer_animtion.SetActive(false);
            checkAnswer.sprite = normalAnswer;
            obj.transform.position = obj.GetComponent<Drager>().lastpos;

        }
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        IEnumerator ResetingLevel(GameObject obj)
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            Party_pop.SetActive(false);
            checkAnswer.sprite = normalAnswer;
            amount = Random.Range(11, 20);
            Question_text.text = amount.ToString();
            obj.transform.position = obj.GetComponent<Drager>().lastpos;
            levelloaded++;
            if (levelloaded > 10)
            {
                StartCoroutine(LevelCompleted());
            }
        }
    }

}