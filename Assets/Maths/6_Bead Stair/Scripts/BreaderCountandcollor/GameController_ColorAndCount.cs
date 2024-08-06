using Maths.BeadStair.ColorSlection;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Maths.BeadStair.ColorandCount
{
    public class GameController_ColorAndCount : MonoBehaviour
    {
        
        public ColorSelection_ColorAndCount[] all_CollerSelection;
        public int level;
        public static GameController_ColorAndCount instace;
        public Color normaltextcolor;
        public AllCollors selectedcollor;
        public AllCollors selectedcollorDone;
        public Marble_ColorAndCount[] allmarble;
        public TextMeshProUGUI Hinttext;

        [Space(10)]
        public GameObject Sketchpen;
        public GameObject DrawnNO;
        public GestureRecognizer.Recognizer Ai_reconginzer;
        public ExampleGestureHandler Draw;
        public Sprite NormalInput, CurrectAnswerInput, WrongInput;
        public SpriteRenderer InputBox;
        public Button CollorButton, NumberButton;
        public float currectanswerInterval, wronganswerInterval;
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        private void Awake()
        {
            instace = this;

            Hinttext.text = "Count the Beads";
            Ai_reconginzer.Recognigingnumber = (level + 1).ToString();
            Ai_reconginzer.Changerecogniger();
        }
        public void ResetColler()
        {
           
            for (int i = 0; i < all_CollerSelection.Length; i++)
            {
                all_CollerSelection[i].Selectedobj.SetActive(false);
            }

        }
        public void NextButton()//changed to Ai conformtion button
        {

            if ((level + 1) == Draw.no)
            {
                Draw.textResult.color = Color.white;
                allmarble[level].GetComponent<Collider2D>().enabled = true;
                InputBox.sprite = CurrectAnswerInput;
                Draw.textResult = null;
                CollorButton.gameObject.SetActive(true);
                Sketchpen.SetActive(true);
                CollorButton.gameObject.SetActive(true);
                NumberButton.gameObject.SetActive(false);
                DrawnNO.SetActive(false);
             
                Hinttext.color = allmarble[level].colors;
                Hinttext.text =
                  "<color=white>Color the " + (level + 1) +
                  " bead with </color> " + allmarble[level].thiscolor;
            }
            else
            {
                DrawnNO.SetActive(false);
                Draw.textResult = null;
                InputBox.sprite = WrongInput;
                StartCoroutine(resetInput());
            }

        }
        IEnumerator resetInput()
        {
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(wronganswerInterval);
            wrongAnswer_animtion.SetActive(false);
            Draw.textResult = InputBox.GetComponentInChildren<TextMeshPro>();
            Draw.textResult.text = "";
            InputBox.sprite = NormalInput;
            DrawnNO.SetActive(true);
        }
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }

        IEnumerator WaitforReloding()
        {
            CollorButton.gameObject.SetActive(false);
            Party_pop.SetActive(true);
       
            yield return new WaitForSeconds(currectanswerInterval);
            Party_pop.SetActive(false);
            Hinttext.color = Color.white;
            Hinttext.text = "Count the number of beads";
            allmarble[level].gameObject.SetActive(false);
            level++;
            Ai_reconginzer.Recognigingnumber = (level + 1).ToString();
            Ai_reconginzer.Changerecogniger();
            if (level > 9)
            {
                StartCoroutine(LevelCompleted());
            }
            allmarble[level].GetComponent<Collider2D>().enabled = false;
            allmarble[level].gameObject.SetActive(true);
            Draw.textResult = InputBox.GetComponentInChildren<TextMeshPro>();
            Draw.textResult.color = normaltextcolor;
            Draw.textResult.text = "?";
            InputBox.sprite = NormalInput;
            Sketchpen.SetActive(false);
            DrawnNO.SetActive(true);
        }

        public void ColorSelectionDone()
        {
            if (selectedcollorDone == allmarble[level].thiscolor)
            {
                StartCoroutine(WaitforReloding());
            }
        }
    }
}