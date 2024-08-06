using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace Maths.Addision.addiwthObjects
{
    public class Gamecontroler : MonoBehaviour
    {
        public int number1, number2, number3;
        public ExampleGestureHandler ai;
        public GestureRecognizer.Recognizer Ai_recognizer;
        public Sprite[] allsprire;
        public SpriteRenderer question1, quesion2;
        public Sprite[] currectanswer, wronganswer, Normalanswer;
        public SpriteRenderer inputfield;
        public SpriteRenderer Plus, Equal;
        public Image background;
        public int randomid;
        public Sprite[] allIcon;
        public Sprite[] allbackgrounds;
        public Sprite[] allholder;
        public Sprite[] PlusS;
        public Sprite[] EqualS;

        private int reloding;
        [SerializeField] TextMeshPro DrawText;

        public float currectanswerInteva;
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
            ai.textResult = DrawText;
            inputfield.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            wrongAnswer_animtion.SetActive(false);
            inputfield.sprite = Normalanswer[randomid];
        }
        private void Start()
        {
            randomid = Random.Range(0, allIcon.Length);
            number1 = Random.Range(0, 10);
            number2 = Random.Range(0, 10);
            number3 = (number1 + 1) + (number2 + 1);
            Ai_recognizer.Recognigingnumber = number3.ToString();
            Ai_recognizer.Changerecogniger();
            question1.sprite = allsprire[number1];
            quesion2.sprite = allsprire[number2];
            question1.transform.parent.GetComponent<SpriteRenderer>().sprite = allholder[randomid];
            quesion2.transform.parent.GetComponent<SpriteRenderer>().sprite = allholder[randomid];
            inputfield.sprite = Normalanswer[randomid];
            for (int i = 0; i < number1 + 1; i++)
            {
                question1.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = allIcon[randomid];
                question1.transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = 0; i < number2 + 1; i++)
            {
                quesion2.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = allIcon[randomid];
                quesion2.transform.GetChild(i).gameObject.SetActive(true);
            }
            background.sprite = allbackgrounds[randomid];
            Plus.sprite = PlusS[randomid];
            Equal.sprite = EqualS[randomid];
        }
        IEnumerator WaitForRelod()
        {
            Party_pop.SetActive(true);
            ai.transform.parent.gameObject.SetActive(false);
            yield return new WaitForSeconds(currectanswerInteva);
            ai.transform.parent.gameObject.SetActive(true);
            Party_pop.SetActive(false);
            randomid = Random.Range(0, allIcon.Length);
            ai.textResult = DrawText;
            inputfield.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            reloding++;
            if (reloding > 9)
            {
                StartCoroutine(LevelCompleted());
            }
            for (int i = 0; i < question1.transform.childCount; i++)
            {
                question1.transform.GetChild(i).gameObject.SetActive(false);
                quesion2.transform.GetChild(i).gameObject.SetActive(false);
            }


            number1 = Random.Range(0, 10);
            number2 = Random.Range(0, 10);
            number3 = (number1 + 1) + (number2 + 1);
            Ai_recognizer.Recognigingnumber = number3.ToString();
            Ai_recognizer.Changerecogniger();
            question1.sprite = allsprire[number1];
            quesion2.sprite = allsprire[number2];
            inputfield.sprite = Normalanswer[randomid];
            question1.transform.parent.GetComponent<SpriteRenderer>().sprite = allholder[randomid];
            quesion2.transform.parent.GetComponent<SpriteRenderer>().sprite = allholder[randomid];
            for (int i = 0; i < number1 + 1; i++)
            {
                question1.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = allIcon[randomid];
                question1.transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = 0; i < number2 + 1; i++)
            {
                quesion2.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = allIcon[randomid];
                quesion2.transform.GetChild(i).gameObject.SetActive(true);
            }
            background.sprite = allbackgrounds[randomid];
            Plus.sprite = PlusS[randomid];
            Equal.sprite = EqualS[randomid];
        }
        public void Conform()
        {
            if (ai.no == number3)
            {
                ai.textResult = null;
                inputfield.sprite = currectanswer[randomid];
                StartCoroutine(WaitForRelod());
            }
            else
            {
                ai.textResult = null;
                inputfield.sprite = wronganswer[randomid];
                StartCoroutine(WrongAnswerAnimation());
            }
        }
    }
}