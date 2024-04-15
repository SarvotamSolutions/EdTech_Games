using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.TeenBeads.drop10to20
{
    public class GameController : MonoBehaviour
    {
      

        public static GameController instance;
        public Sprite currectanswer;
        public Sprite wrongAnswer;
        public Sprite normalAnswer;
        public TextMeshPro hintText;
        public GameObject[] droppos;
        public SpriteRenderer[] allsprite;
        public float offset=2;
       

        public int no;
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        private void Awake()
        {
            instance = this;
            TextUpdate();
        }
        
        void TextUpdate()
        {
            // int no = Random.Range(11, 20);
            

        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator CurrectAnswer()
        {
            no++;
           
            allsprite[no-1].sprite = currectanswer;
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            hintText.text = "Drag and join the right bead to make " + (no + 11).ToString();
            hintText.transform.position = new Vector2(hintText.transform.position.x, hintText.transform.position.y - offset);
            Party_pop.SetActive(false);
            if (no >= 9)
            {
                StartCoroutine(LevelCompleted());
                yield return null;
            }
            else
            {
                allsprite[no].sortingOrder = 4;
                allsprite[no].transform.GetChild(0).GetComponent<TextMeshPro>().sortingOrder = 4;
                allsprite[no].transform.parent.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 4;
                allsprite[no].transform.parent.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 5;

                //allsprite[no].transform.GetChild(0).GetComponent<TextMeshPro>().text = (10 + (no + 1)).ToString();
            }
        }

        IEnumerator WrongAnswerAnimation(GameObject obj)
        {
            allsprite[no].sprite = wrongAnswer;
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            allsprite[no].transform.GetChild(0).GetComponent<TextMeshPro>().text = 10.ToString();
            wrongAnswer_animtion.SetActive(false);
            allsprite[no].sprite = normalAnswer;
            obj.transform.position = obj.GetComponent<Drager>().lastpos;

        }
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }

        public GameObject Neartodestination(GameObject obj)
        {
            if (Vector3.Distance(obj.transform.position, droppos[no].transform.position) <4f)
            {
                allsprite[no].transform.GetChild(0).GetComponent<TextMeshPro>().text = (10+obj.GetComponent<Drager>().no).ToString();
                if (obj.GetComponent<Drager>().no == no + 1)
                {
                    obj.GetComponent<BoxCollider2D>().enabled = false;
                    StartCoroutine(CurrectAnswer());
                    return droppos[no - 1];
                }
                else
                {
                    StartCoroutine(WrongAnswerAnimation(obj));
                }
                return droppos[no];
            }
          
            return null;
        }
    }
}