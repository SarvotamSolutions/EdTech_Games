using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.graterAndLesser
{
    public class GameController : MonoBehaviour
    {

        [Header("Ai")]
        public ExampleGestureHandler AiDrawtext;
     //   public GameObject DrawCanvas;
        public GestureRecognizer.Recognizer Ai_recognizer;

        [Space(10)]
        public static GameController instance;
        public GameObject firstbutton;
        public GameObject SecondButton;

        public TextMeshPro Firsttext;
        public TextMeshPro SecondText;


        public GameObject dropingobj;
        public int[] Numbers;
        public Answer[] allsprite;
        public Sprite currectAnswer, WrongAnswer, normalAnswer;
        public SpriteRenderer[] inputs;
        public SpriteRenderer firstbeads;
        public SpriteRenderer secondbeads;

        public Sprite currectanswer, Normalgratersprite;
        public bool GraterorLessselct;

        public int numbertimes;
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
            Numbers[0] = Random.Range(0, 9);
            Numbers[1] = Random.Range(0, 9);
            while(Numbers[0] == Numbers[1])
            {
                Numbers[1] = Random.Range(0, 9);
            }
            Ai_recognizer.Recognigingnumber = (Numbers[0] + 1).ToString();
            Ai_recognizer.Changerecogniger();
            firstbeads.sprite = allsprite[Numbers[0]].beads;
            secondbeads.sprite = allsprite[Numbers[1]].beads;
        }
        int no;
        public void Settext()
        {
            if (AiDrawtext.no == (Numbers[no] + 1))
            {
                no++;
                if (no > 1)
                {
                    inputs[no-1].sprite = currectAnswer;
                    inputs[no - 1].color = Color.white;
                    //     DrawCanvas.gameObject.SetActive(false);
                    AiDrawtext.textResult = null;

                    GraterorLessselct = true;
                    no = 0;
                //    SecondButton.gameObject.SetActive(false);
                }
                else
                {
                    Ai_recognizer.Recognigingnumber = (Numbers[no] + 1).ToString();
                    Ai_recognizer.Changerecogniger();
                    inputs[no - 1].sprite = currectAnswer;
                    inputs[no - 1].color = Color.white;
                    inputs[no].color = Color.blue;
                    AiDrawtext.textResult = SecondText;
                    //firstbutton.gameObject.SetActive(false);
                    //SecondButton.gameObject.SetActive(true);
                }
            }
            else
            {
                AiDrawtext.textResult = null;
                inputs[no].sprite = WrongAnswer;
                StartCoroutine(WrongAnswerAnimation());
            }


        }
        public void FinalButton()
        {
            //if (AiDrawtext.no == (Number2 + 1))
            //{
                
            //}
            //else
            //{
            //    secondInput.sprite = WrongAnswer;
            //    AiDrawtext.textResult = null;
            //    StartCoroutine(WrongAnswerAnimation());

            //}
        }
        IEnumerator WrongAnswerAnimation()
        {
            yield return new WaitForSeconds(.2f);
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            inputs[0].sprite = inputs[0].sprite == currectAnswer ? currectAnswer : normalAnswer;
            Firsttext.text = inputs[0].sprite == currectAnswer ? Firsttext.text : "";
            SecondText.text = "";

            AiDrawtext.textResult = Firsttext.text == "" ? Firsttext : SecondText;
            inputs[1].sprite =normalAnswer;
            wrongAnswer_animtion.SetActive(false);
        }


        public void FinalCheck(SpriteRenderer sprite)
        {
          
            sprite.sprite = currectAnswer;
            inputs[0].sprite = allsprite[Numbers[0]].box;
            inputs[1].sprite = allsprite[Numbers[1]].box;
            numbertimes++;
            if (numbertimes < 10)
            {
                StartCoroutine(Relodlevel(sprite));
            }
            else
            {
                StartCoroutine(LevelCompleted());
            }

        }
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        IEnumerator Relodlevel(SpriteRenderer obj)
        {
            Party_pop.SetActive(true);
            AiDrawtext.transform.parent.transform.gameObject.SetActive(false);
            yield return new WaitForSeconds(3);
            inputs[0].color = Color.blue;
          //  inputs[no].color = Color.white;
            AiDrawtext.transform.parent.transform.gameObject.SetActive(true);
            Party_pop.SetActive(false);
            Numbers[0] = Random.Range(0, 9);
            Numbers[1] = Random.Range(0, 9);
            while (Numbers[0] == Numbers[1])
            {
                Numbers[1] = Random.Range(0, 9);
            }
            AiDrawtext.textResult = Firsttext;
            Ai_recognizer.Recognigingnumber = (Numbers[0] + 1).ToString();
            Ai_recognizer.Changerecogniger();
            firstbeads.sprite = allsprite[Numbers[0]].beads;
            secondbeads.sprite = allsprite[Numbers[1]].beads;
            inputs[0].sprite = normalAnswer;
            inputs[1].sprite = normalAnswer;
            SecondText.GetComponent<TextMeshPro>().text = "";
            Firsttext.GetComponent<TextMeshPro>().text = "";
            firstbutton.SetActive(true);
          
           // DrawCanvas.SetActive(true);
            obj.sprite = Normalgratersprite;
            obj.transform.position = obj.GetComponent<Dragin>().lastpos;

        }




    }
}
[System.Serializable]
public class Answer
{
    public Sprite beads;
    public Sprite box;
}