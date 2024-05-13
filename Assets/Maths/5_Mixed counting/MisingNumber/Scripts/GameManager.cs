using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
namespace Maths.Number1to10.MisingNO
{
    public class GameManager : MonoBehaviour
    {
        public int no;
        public ExampleGestureHandler Nodfyn0;
        public GestureRecognizer.Recognizer AiRecogniser;
        public TextMeshPro PreviousNo, NextNO,currectNo;
        public Sprite CurrectAnswer, WrongAnswer, normalAnswer;
        public SpriteRenderer MiddleNoobject;
     
        public List<int> allanswer = new List<int>();
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        private void Start()
        {
            no = Random.Range(1, 10);
            allanswer.Add(no);
            AiRecogniser.Recognigingnumber = no.ToString();
            AiRecogniser.Changerecogniger();
            PreviousNo.text = "" + (no - 1);
            NextNO.text = "" + (no + 1);

        }
        private void Update()
        {
       
        }
        public void Conform()
        {

            if (Nodfyn0.no == no)
            {
                Nodfyn0.textResult = null;
                Nodfyn0.transform.parent.gameObject.SetActive(false);
                Party_pop.SetActive(true);
                MiddleNoobject.sprite = CurrectAnswer;
               // Drawobject.SetActive(false);
                StartCoroutine(WairforReset());

            }
            else
            {
                
                Nodfyn0.textResult = null;
                //Drawobject.SetActive(false);
                MiddleNoobject.sprite = WrongAnswer;
                StartCoroutine(WairforRelode());

            }
        }

        IEnumerator WairforRelode()
        {
            yield return new WaitForSeconds(.5f);
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            currectNo.text = "";
            Nodfyn0.textResult = currectNo;

            MiddleNoobject.GetComponentInChildren<TextMeshPro>().text = "";
           // Drawobject.SetActive(true);
            MiddleNoobject.sprite = normalAnswer;
            wrongAnswer_animtion.SetActive(false);


        }
        IEnumerator LevelCompleted()
        {
            yield return new WaitForSeconds(.4f);
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        public void Resetgame()
        {
            no = Random.Range(1, 10);
            if (allanswer.Count >= 9)
            {
                StartCoroutine(LevelCompleted());
                return;
            }
            foreach (var item in allanswer)
            {
                if (no == item)
                {
                    Resetgame();
                    return;
                }
            }
            AiRecogniser.Recognigingnumber = no.ToString();
            AiRecogniser.Changerecogniger();
            allanswer.Add(no);
            PreviousNo.text = "" + (no - 1);
            NextNO.text = "" + (no + 1);
        }
        IEnumerator WairforReset()
        {
            yield return new WaitForSeconds(3);
            Nodfyn0.textResult = currectNo;
            Nodfyn0.transform.parent.gameObject.SetActive(true);
            Party_pop.SetActive(false);
            //Drawobject.SetActive(true);
            MiddleNoobject.GetComponentInChildren<TextMeshPro>().text = "";
            MiddleNoobject.sprite = normalAnswer;
            Resetgame();

        }
    }
}
