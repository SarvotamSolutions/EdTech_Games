using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.placeValue.Dropbeads
{
    public class GameController : Singleton<GameController>
    {
        public TextMeshPro Question_text;
        int amount;
        public GameObject droppostion;
        public Sprite currectAnswer, wrongAnswer, normalAnswer;
        public SpriteRenderer checkAnswer;
        private int levelloaded;
        public TextMeshProUGUI infotext;

        public List<int> allno;
        public float currectanswerInteval;
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
      
        private void Start()
        {
            amount = Random.Range(11, 20);
            allno.Add(amount);
            Question_text.text = amount.ToString();
            infotext.text = "Drag the right bead to make " + amount;
        }
        public Vector2 Distance;
        public bool Neartodestination(GameObject obj)
        {

            if (obj.transform.position.x - droppostion.transform.position.x < Distance.x &&
                obj.transform.position.y - droppostion.transform.position.x < Distance.y)
            {

                obj.transform.position = droppostion.transform.position;
                gamePlay = false;
                Drager drag = obj.GetComponent<Drager>();
                if(drag.no +10 == amount)
                {
                    checkAnswer.sprite = currectAnswer;
                    if (levelloaded >= 8)
                    {
                        StartCoroutine(LevelCompleted());
                         return true;
                    }
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
            Color tempcolor = Question_text.color;
            Question_text.color = Color.white;
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            Question_text.color = tempcolor;
            wrongAnswer_animtion.SetActive(false);
            checkAnswer.sprite = normalAnswer;
            obj.transform.position = obj.GetComponent<Drager>().lastpos;
            gamePlay = true;

        }
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        IEnumerator ResetingLevel(GameObject obj)
        {
            Color tempcolor = Question_text.color;
            Question_text.color = Color.white;

           
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(currectanswerInteval);
            Question_text.color = tempcolor;
            Party_pop.SetActive(false);
            checkAnswer.sprite = normalAnswer;
            amount = Random.Range(11, 20);
            for (int i = 0; i < allno.Count; i++)
            {
                if(amount== allno[i])
                {
                    amount = Random.Range(11, 20);
                    i = -1;
                }
            }
            allno.Add(amount);
            infotext.text = "Drag the right bead to make " + amount;
            Question_text.text = amount.ToString();
            obj.transform.position = obj.GetComponent<Drager>().lastpos;
            levelloaded++;
           
            gamePlay = true;
        }
    }

}