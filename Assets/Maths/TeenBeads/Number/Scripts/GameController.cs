using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Maths.TeenBeads.Number
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        public ExampleGestureHandler drawnno;
        public GestureRecognizer.Recognizer Ai_recognizer;
        [Space(10)]
        
        public int Number1;
        public int Number2;
        List<int> allno = new List<int>();


        [Space(10)]
        public GameObject Contins1,Contins2;
        public GameObject DrawCanvas;
        public GameObject ObjectDroping;
        public TextMeshPro Number2text,Number1text;
        public SpriteRenderer Inputfild,DropNumber;
        public Sprite currectanswer, WrongAnswer, NormalAnswer;
        public GameObject Nextbuttton, FinalButton;
        public GameObject TotalTen;
        public GameObject Stage_one_parent;

        [Space(10)]
        public GameObject stage_two_parent;
        public SpriteRenderer stage_two_droperfirst,stage_two_dropersecondtext,Stage_two_droperAnser;
        public TextMeshPro stage_two_firstText, stage_two_secondText;
        public GameObject stage_two_optionContenr;
        public Sprite stage_two_normalinput_sprite;
        public int stagetwoDrop;

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
            Number1 = Random.Range(1, 10);
            allno.Add(Number1);
            Ai_recognizer.Recognigingnumber = Number1.ToString();
            Ai_recognizer.Changerecogniger();
            Contins1.transform.GetChild(1).gameObject.SetActive(Number1 > 5);
            if(Contins1.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                for (int i = 0; i < Contins1.transform.GetChild(0).childCount; i++)
                {

                    Contins1.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                }
                //privous Contins all active
                int no = Number1-5;
                for (int i = 0; i < no; i++)
                {
                    Contins1.transform.GetChild(1).GetChild(i).gameObject.SetActive(true);
                }

            }
            else
            {
                for (int i = 0; i <Number1; i++)
                {
                    Contins1.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                }
            }

        }
        public void Relod()
        {
            StartCoroutine(Reloding());
        }
        
        public IEnumerator Reloding()
        {
            stagetwoDrop = 0;
            
            ObjectDroping.SetActive(false);
            DrawCanvas.SetActive(false);
            TotalTen.SetActive(true);
            FinalButton.SetActive(false);
            Party_pop.SetActive(true);

            Debug.Log("Coming here");
            yield return new WaitForSeconds(2);
            Stage_one_parent.SetActive(true);
            stage_two_parent.SetActive(false);
            for (int i = 0; i < stage_two_optionContenr.transform.childCount; i++)
            {
                stage_two_optionContenr.transform.GetChild(i).gameObject.SetActive(true);

            }
           
           
            Stage_two_droperAnser.sprite = stage_two_normalinput_sprite;
            stage_two_droperfirst.sprite = stage_two_normalinput_sprite;
            stage_two_dropersecondtext.sprite = stage_two_normalinput_sprite;


            Stage_two_droperAnser.GetComponentInChildren<TextMeshPro>().text = "";
            stage_two_droperfirst.GetComponentInChildren<TextMeshPro>().text = "";
            stage_two_dropersecondtext.GetComponentInChildren<TextMeshPro>().text = "";


            Debug.Log("witing");
            ObjectDroping.SetActive(true);
            DrawCanvas.SetActive(true);
            TotalTen.SetActive(false);
            drawnno.textResult = Number1text;
            Party_pop.SetActive(false);
            for (int i = 0; i < Contins1.transform.GetChild(0).childCount; i++)
            {

                Contins1.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < Contins1.transform.GetChild(1).childCount; i++)
            {

                Contins1.transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
            }
            Contins1.transform.GetChild(1).gameObject.SetActive(false);
            Contins1.gameObject.SetActive(true);
            DrawCanvas.gameObject.SetActive(true);
            Inputfild.sprite = NormalAnswer;
            DropNumber.sprite = NormalAnswer;
            Number2 = 0;
            Number2text.text = "";
            Inputfild.GetComponentInChildren<TextMeshPro>().text = "";

            for (int i = 0; i < Contins2.transform.childCount; i++)
            {
                Contins2.transform.GetChild(i).gameObject.SetActive(true);
            }
            RelodingFuntion();
            Nextbuttton.SetActive(true);
         


        }
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        void RelodingFuntion()
        {
            if (allno.Count >= 9)
            {
                StartCoroutine(LevelCompleted());
                return;
            }
            Number1 = Random.Range(1, 10);
            foreach (var item in allno)
            {
                if(item == Number1)
                {
                    RelodingFuntion();
                    return;
                }

            }
            Ai_recognizer.Recognigingnumber = Number1.ToString();
            Ai_recognizer.Changerecogniger();

            allno.Add(Number1);

            Contins1.transform.GetChild(1).gameObject.SetActive(Number1 > 5);
            if (Contins1.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                for (int i = 0; i < Contins1.transform.GetChild(0).childCount; i++)
                {

                    Contins1.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                }
                //privous Contins all active
                int no = Number1 - 5;
                for (int i = 0; i < no; i++)
                {
                    Contins1.transform.GetChild(1).GetChild(i).gameObject.SetActive(true);
                }

            }
            else
            {
                for (int i = 0; i < Number1; i++)
                {
                    Contins1.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                }
            }


        }
        public GameObject sidepanel;
        public void NextButton()
        {
            if(drawnno.no == Number1)
            {
                drawnno.textResult = null;
                Contins1.gameObject.SetActive(false);
                DrawCanvas.gameObject.SetActive(false);
                Inputfild.sprite = currectanswer;
                Nextbuttton.SetActive(false);
                FinalButton.SetActive(true);
                sidepanel.SetActive(true);
            }
            else
            {
                drawnno.textResult = null;
                Inputfild.sprite = WrongAnswer;
                wrongAnswer_animtion.SetActive(true);
                StartCoroutine(WrongAnimtion());
            }
        }
        IEnumerator WrongAnimtion()
        {
            yield return new WaitForSeconds(2);
            drawnno.textResult = Number1text;
            Inputfild.sprite = NormalAnswer;
            Number1text.text = "";

            wrongAnswer_animtion.SetActive(false);
        }

    
        public void FinalFunction()
        {
            if(Number1 + Number2 == 10)
            {
                ObjectDroping.SetActive(false);
                DrawCanvas.SetActive(false);
                TotalTen.SetActive(true);
                sidepanel.SetActive(false);
                DropNumber.sprite = currectanswer;
                stage_two_firstText.text = (Number1).ToString();
                stage_two_secondText.text = (Number2 ).ToString();
               // StartCoroutine(Reloding());
            }
            else
            {
                DropNumber.sprite = WrongAnswer;
                StartCoroutine(ResetFInsih());
            }
        }

        IEnumerator ResetFInsih()
        {
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            wrongAnswer_animtion.SetActive(false);
            DropNumber.sprite = NormalAnswer;
            Number2 = 0;
            Number2text.text = "";
            for (int i = 0; i < Contins2.transform.childCount; i++)
            {
                Contins2.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
