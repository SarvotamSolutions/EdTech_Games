using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Maths.TeenBeads.Number
{
    public class GameController : MonoBehaviour
    {
        public Totorial totorialcheck;
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
        public Sprite currectanswer, WrongAnswer, NormalAnswer, NormalAnswerStage2;
        public GameObject  FinalButton;
        public GameObject TotalTen;
        public GameObject Stage_one_parent;
        public Sprite finalanswer;
        public Color newcolor;

        [Space(10)]
        public GameObject stage_two_parent;
        public SpriteRenderer stage_two_droperfirst,stage_two_dropersecondtext,Stage_two_droperAnser;
        public TextMeshPro stage_two_firstText, stage_two_secondText;
        public GameObject stage_two_optionContenr;
        public Sprite stage_two_normalinput_sprite,finalanswersprite;
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
            
            for (int i = 0; i <Number1; i++)
            {
                Contins1.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
            }

        }
        public void Relod()
        {
            StartCoroutine(Reloding());
        }
        
        public IEnumerator Reloding()
        {
            stagetwoDrop = 0;
            
            DrawCanvas.SetActive(false);
            ObjectDroping.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            TotalTen.SetActive(true);
            FinalButton.SetActive(false);
            Party_pop.SetActive(true);

            yield return new WaitForSeconds(2);
            for (int i = 0; i < Contins1.transform.GetChild(0).childCount; i++)
            {
                Contins1.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = ObjectDroping.transform.GetChild(0).childCount - 1; i >= 0; i--)
            {
                ObjectDroping.transform.GetChild(0).GetChild(i).transform.position = ObjectDroping.transform.GetChild(0).GetChild(i).GetComponent<IconDraw>().lastpos;
                ObjectDroping.transform.GetChild(0).GetChild(i).transform.parent = sidepanel.transform;
            }
            Contins1.transform.parent = Stage_one_parent.transform;
            Contins2.transform.parent = Stage_one_parent.transform;
            Contins1.GetComponent<SpriteRenderer>().sprite = NormalAnswer;
            Contins2.GetComponent<SpriteRenderer>().sprite = NormalAnswer;

            Stage_one_parent.SetActive(true);
            stage_two_parent.SetActive(false);
            for (int i = 0; i < stage_two_optionContenr.transform.childCount; i++)
            {
                stage_two_optionContenr.transform.GetChild(i).gameObject.SetActive(true);

            }
           
           
            Stage_two_droperAnser.sprite = finalanswersprite;
            stage_two_droperfirst.sprite = stage_two_normalinput_sprite;
            stage_two_dropersecondtext.sprite = stage_two_normalinput_sprite;


            secondstage1.GetComponentInChildren<TextMeshPro>().text = "";
            secondstage2.GetComponentInChildren<TextMeshPro>().text = "";
            Stage_two_droperAnser.GetComponentInChildren<TextMeshPro>().text = "";
            stage_two_droperfirst.GetComponentInChildren<TextMeshPro>().text = "";
            stage_two_dropersecondtext.GetComponentInChildren<TextMeshPro>().text = "";


            ObjectDroping.SetActive(true);
            DrawCanvas.SetActive(true);
            ObjectDroping.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

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
            
            DrawCanvas.gameObject.SetActive(true);
            ObjectDroping.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

            Inputfild.sprite = NormalAnswer;
            DropNumber.sprite = NormalAnswer;
            Number2text.color = newcolor;

            Number2 = 0;
            Number2text.text = "";
            Inputfild.GetComponentInChildren<TextMeshPro>().text = "";
            Number1text.color = newcolor;

            for (int i = 0; i < Contins2.transform.childCount; i++)
            {
                Contins2.transform.GetChild(i).gameObject.SetActive(true);
            }
            RelodingFuntion();
        }
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(5);
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
                for (int i = 0; i < Number1; i++)
                {
                    Contins1.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                }


        }
        public GameObject sidepanel;
        public GameObject equationNextbutton;

        public GameObject secondstage1, secondstage2;
        public void SecondStageActivate()
        {
            totorialcheck.directionWindow();
            Stage_one_parent.gameObject.SetActive(false);
            stage_two_parent.gameObject.SetActive(true);
            Contins1.transform.parent = stage_two_parent.transform;
            Contins2.transform.parent = stage_two_parent.transform;
            Contins1.GetComponent<SpriteRenderer>().sprite = NormalAnswerStage2;
            Contins2.GetComponent<SpriteRenderer>().sprite = NormalAnswerStage2;
            Contins1.transform.position = secondstage1.transform.position;
            Contins2.transform.position = secondstage2.transform.position;
            Stage_two_droperAnser.GetComponentInChildren<TextMeshPro>().text = "";
            stage_two_droperfirst.GetComponentInChildren<TextMeshPro>().text = "";
            stage_two_dropersecondtext.GetComponentInChildren<TextMeshPro>().text = ""; 
            Stage_two_droperAnser.GetComponentInChildren<TextMeshPro>().color = Color.white;
            stage_two_droperfirst.GetComponentInChildren<TextMeshPro>().color = newcolor;
            stage_two_dropersecondtext.GetComponentInChildren<TextMeshPro>().color = newcolor;
        }
        public void NextButton()
        {
            if(drawnno.no == Number1)
            {
                drawnno.textResult = null;
                DrawCanvas.gameObject.SetActive(false);
                ObjectDroping.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);

                Inputfild.sprite = currectanswer;
                Number1text.color = Color.white;
              
                FinalButton.SetActive(true);
                sidepanel.SetActive(true);
                totorialcheck.directionWindow();
            
            }
            else
            {
                drawnno.textResult = null;
                Inputfild.sprite = WrongAnswer;
                Number1text.color = Color.white;
                Number2text.color = Color.white;

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
            Number1text.color = newcolor;
            wrongAnswer_animtion.SetActive(false);
        }

    
        public void FinalFunction()
        {
            if(Number1 + Number2 == 10)
            {
                DrawCanvas.SetActive(false);

                TotalTen.SetActive(true);
                sidepanel.SetActive(false);
                DropNumber.sprite = currectanswer;
                Number2text.color = Color.white;


                stage_two_firstText.text = (Number1).ToString();
                stage_two_secondText.text = (Number2 ).ToString();
                FinalButton.SetActive(false);
                equationNextbutton.SetActive(true);
            }
            else
            {
                DropNumber.sprite = WrongAnswer;
                Number2text.color = Color.white;

                StartCoroutine(ResetFInsih());
            }
        }

        IEnumerator ResetFInsih()
        {
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
        
            for (int i = ObjectDroping.transform.GetChild(0).childCount-1; i >=0; i--)
            {
                ObjectDroping.transform.GetChild(0).GetChild(i).transform.position = ObjectDroping.transform.GetChild(0).GetChild(i).GetComponent<IconDraw>().lastpos;
                ObjectDroping.transform.GetChild(0).GetChild(i).transform.parent = sidepanel.transform;
            }
            wrongAnswer_animtion.SetActive(false);
            DropNumber.sprite = NormalAnswer;
            Number2text.color = newcolor;

            Number2 = 0;
            Number2text.text = "";
            for (int i = 0; i < Contins2.transform.childCount; i++)
            {
                Contins2.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}