using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Maths.Addision.addiwthObjects
{
    public class Gamecontroler : MonoBehaviour
    {
        public int number1, number2,number3;
        public ExampleGestureHandler ai;
        public GestureRecognizer.Recognizer Ai_recognizer;
        public Sprite[] allsprire;
        public SpriteRenderer question1, quesion2;
        public Sprite currectanswer, wronganswer, Normalanswer;
        public SpriteRenderer inputfield;

        private int reloding;
        [SerializeField] TextMeshPro DrawText;

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
            //ai.gameObject.SetActive(false);
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            ai.textResult = DrawText;
            inputfield.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            wrongAnswer_animtion.SetActive(false);
            inputfield.sprite = Normalanswer;
           // ai.gameObject.SetActive(true);
        }
        private void Start()
        {
            number1 = Random.Range(0, 10);
            number2 = Random.Range(0, 10);
            number3 = (number1 + 1) + (number2 + 1);
            Ai_recognizer.Recognigingnumber = number3.ToString();
            Ai_recognizer.Changerecogniger();
            question1.sprite = allsprire[number1];
            quesion2.sprite = allsprire[number2];
           

        }
        IEnumerator WaitForRelod()
        {
            Party_pop.SetActive(true);
            ai.transform.parent.gameObject.SetActive(false);
            yield return new WaitForSeconds(3);
            ai.transform.parent.gameObject.SetActive(true);
            Party_pop.SetActive(false);
            ai.textResult = DrawText;
            inputfield.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            reloding++;
            if (reloding > 9)
            {
                StartCoroutine(LevelCompleted());
            }
            number1 = Random.Range(0, 10);
            number2 = Random.Range(0, 10);
            number3 = (number1 + 1) + (number2 + 1);
            Ai_recognizer.Recognigingnumber = number3.ToString();
            Ai_recognizer.Changerecogniger();
            question1.sprite = allsprire[number1];
            quesion2.sprite = allsprire[number2];
            inputfield.sprite =Normalanswer;


        }
        public void Conform()
        {
            if(ai.no == number3)
            {
                ai.textResult = null;
                inputfield.sprite = currectanswer;
                StartCoroutine(WaitForRelod());

            }
            else
            {
                ai.textResult = null;
                inputfield.sprite = wronganswer;
                StartCoroutine(WrongAnswerAnimation());
            }
        }

    }
}