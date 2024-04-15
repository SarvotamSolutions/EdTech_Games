using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Maths.NumberRoads
{
    public class GameController : MonoBehaviour
    {
        public bool onlycolor;

        public static GameController instace;
        public ExampleGestureHandler ai;
        public GestureRecognizer.Recognizer aireconniger;
        public bool red;
        public int no;

        public GameObject Draw_canvas;
        public List<int> numbers = new List<int>();
        public TextMeshPro[] allnumber;
        public GameObject Redselected, blueSeleceted, rednotselected, bluenotselecected;


        public Sprite bluwin, redwin,notfilled,inpuselected,inputNot_selected;
        public Gradient currectcolor;
        public Gradient Wrongcolor;

        public Sprite dropedCurrectanswer, dropedWrongAnswer, DropedNormalAnswer;

        public GameObject[] all_color_holder;
        public int filledno;
   //     public GameObject option;
        public GameObject[] dropplace;
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        private void Awake()
        {
            instace = this;
            aireconniger.Recognigingnumber = no.ToString();
            aireconniger.Changerecogniger();
            if (!Draw_canvas)
                return;

            for (int i = 0; i < allnumber.Length-1; i++)
            {
                int nos = Random.Range(1, 11);
                for (int J = 0; J < numbers.Count; J++)
                {
                    if (nos == numbers[J])
                    {
                        nos = Random.Range(1, 11);
                        J = -1;
                    }

                }
                numbers.Add(nos); 
                Draw_canvas.transform.GetChild(i).GetComponent<Draging>().no = nos;

            }
        }
        private void Start()
        {
           
        }
        public void RedSelected(bool redSelected)
        {
            if (redSelected)
            {
                red = true;
                rednotselected.SetActive(false);
                blueSeleceted.SetActive(false);
                Redselected.SetActive(true);
                bluenotselecected.SetActive(true);
            }
            else
            {
                red = false;
                rednotselected.SetActive(true);
                blueSeleceted.SetActive(true);
                Redselected.SetActive(false);
                bluenotselecected.SetActive(false); ;
            }
        }

        public void ChangetoColorfiller()
        {
            if (all_color_holder[0].transform.GetChild(0).TryGetComponent<Collider2D>(out Collider2D colider))
            {
                Debug.Log("getting collider");
               // no++;
                for (int i = 0; i < all_color_holder[no].transform.childCount; i++)
                {
                    all_color_holder[no ].transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 2;
                    all_color_holder[no ].transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                   // all_color_holder[no - 2].transform.GetChild(i).GetComponent<Collider2D>().enabled = false;
                }
                if (allnumber.Length > 0)
                {
                    allnumber[no].sortingOrder = 2;
                    allnumber[no].GetComponentInChildren<TextMeshPro>().sortingOrder = 2;
                }
            }
            else
            {
                Debug.Log(no);
                for (int i = 0; i < all_color_holder[no - 1].transform.childCount; i++)
                {
                    all_color_holder[no - 1].transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 2;
                    all_color_holder[no - 1].transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                }

                for (int i = 0; i < all_color_holder[no-2].transform.childCount; i++)
                {
                    all_color_holder[no-2].transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 0;
                    all_color_holder[no-2].transform.GetChild(i).GetComponent<Collider2D>().enabled =false;
                }
            }
        //    Draw_canvas.interactable = false;
            //  ai.textResult = allnumber[no];
            //  Draw_canvas.SetActive(false);
           // ai.textResult = null;
            no++;
          //  aireconniger.Recognigingnumber = no.ToString();
           // aireconniger.Changerecogniger();
        }
       

        public bool Neartodestination(GameObject obj)
        {
            Debug.Log(Vector3.Distance(obj.transform.position, dropplace[no - 1].transform.position));
            if (Vector3.Distance(obj.transform.position, dropplace[no - 1].transform.position)<1)
            {
              //  Draging drag = obj.GetComponent<Draging>();
           


                return true;
            }
            return false;
        }
        public void ConformButton()
        {
            Debug.Log("Conform");
            if(ai.no == no)
            {



                ChangetoColorfiller();
              //  Draw_canvas.interactable = false;
              //  ai.textResult = allnumber[no];
                //  Draw_canvas.SetActive(false);
                 ai.textResult = null;
               // no++;
                aireconniger.Recognigingnumber = no.ToString();
                aireconniger.Changerecogniger();

                if (onlycolor)
                    AitextselectionwithoutColoring();
            }
            else
            {
                StartCoroutine(WrongAnswerAnimation());
            }

        }
        public void AitextselectionwithoutColoring()
        {

            if (no <= 10)
            {
                ai.textResult = allnumber[no - 1];
         //       Draw_canvas.interactable = true;
                filledno = 0;
                foreach (var item in allnumber)
                {
                    item.transform.parent.GetComponent<SpriteRenderer>().sprite = inputNot_selected;
                }
                allnumber[no - 1].transform.parent.GetComponent<SpriteRenderer>().sprite = inpuselected;
            }
            else
            {
                StartCoroutine(LevelCompleted());
            }
        }
        public  IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        public IEnumerator WrongAnswerAnimation(TrailFiller filler)
        {
         //   ai.gameObject.SetActive(false);
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            //      inputfield.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            filler.GetComponent<SpriteRenderer>().sprite = notfilled;
            filler.GetComponent<Collider2D>().enabled = true;
            filler.isFilled = false;
            filler.trailRenderer.Clear();
            filler.trailRenderer.gameObject.SetActive(true);
            wrongAnswer_animtion.SetActive(false);
           // inputfield.sprite = Normalanswer;
        //    ai.gameObject.SetActive(true);
        } 
        public IEnumerator WrongAnswerAnimation()
        {
         //   ai.gameObject.SetActive(false);
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            //      inputfield.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            wrongAnswer_animtion.SetActive(false);
           // inputfield.sprite = Normalanswer;
        //    ai.gameObject.SetActive(true);
        }

    }

}