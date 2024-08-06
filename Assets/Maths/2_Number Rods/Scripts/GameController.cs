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
        public bool gameplay;
        public bool onlycolor;
        public Color Selct, notselect;
        public static GameController instace;
        public Totorial totorialcheck;
        public TextMeshPro TitleText;
        public bool red;
        public int no;

        public GameObject Draw_canvas;
        public List<int> numbers = new List<int>();
        public TextMeshPro[] allnumber;
        public GameObject Redselected, blueSeleceted, rednotselected, bluenotselecected;


        public Sprite bluwin, redwin,inpuselected,inputNot_selected;
        public Gradient currectcolor;
        public Gradient Wrongcolor;

        public Sprite dropedCurrectanswer, dropedWrongAnswer, DropedNormalAnswer;

        public GameObject[] all_color_holder;
        public int filledno;
        public GameObject[] dropplace;
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        private void Awake()
        {
            instace = this;
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
            if (totorialcheck.totorialplaying)
                return;
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
                totorialcheck.directionWindow();
                int redno = (no+1) / 2;
                
      
                redno = no % 2 == 0 ? redno + 1 : redno;
             
                TitleText.text = "Color the "+ redno + " rod red and " + (no + 1) / 2 +" rod blue";
                for (int i = 0; i < all_color_holder[no].transform.childCount; i++)
                {
                    all_color_holder[no ].transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 2;
                    all_color_holder[no ].transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = inpuselected;
                    all_color_holder[no ].transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                }
                if (allnumber.Length > 0)
                {
                    allnumber[no].sortingOrder = 2;
                    allnumber[no].GetComponentInChildren<TextMeshPro>().sortingOrder = 2;
                }
            }
            else
            {
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
            no++;
        }
       

        public bool Neartodestination(GameObject obj)
        {
            Debug.Log(Vector3.Distance(obj.transform.position, dropplace[no - 1].transform.position));
            if (Vector3.Distance(obj.transform.position, dropplace[no - 1].transform.position)<1)
            {           
                return true;
            }
            return false;
        }
        public void ConformButton()
        {

        }
        public void AitextselectionwithoutColoring()
        {

            if (no <= 10)
            {
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
            gameplay = false;
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            gameplay = true;
            filler.GetComponent<SpriteRenderer>().sprite = inpuselected;
            filler.GetComponent<Collider2D>().enabled = true;
            filler.isFilled = false;
            filler.trailRenderer.Clear();
            filler.trailRenderer.gameObject.SetActive(true);
            wrongAnswer_animtion.SetActive(false);
        } 
        public IEnumerator WrongAnswerAnimation()
        {
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            wrongAnswer_animtion.SetActive(false);
        }

    }

}