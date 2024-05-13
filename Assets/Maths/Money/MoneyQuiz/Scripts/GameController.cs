using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.Money.MoneyQuiz
{
    public class GameController : Singleton<GameController>
    {
      
        public GameObject dropplace;
        public string[] allcoinname;
        public GameObject Win;
        public TextMeshPro text;
        int no;
        public Color whitecolor;

        public GameObject Option_parent;
        List<int> allno = new List<int>();

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
            yield return new WaitForSeconds(.5f);
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);

            if (dropplace.transform.childCount != 0)
            {
                dropplace.transform.GetChild(0).transform.position = dropplace.transform.GetChild(0).GetComponent<DragObj>().permantlastpos;
                dropplace.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().color = whitecolor;
                dropplace.transform.GetChild(0).transform.parent = Option_parent.transform;

            }
            wrongAnswer_animtion.SetActive(false);
            gamePlay = true;
        }
  
        // Start is called before the first frame update
        void Start()
        {
            no = Random.Range(0, allcoinname.Length);
            allno.Add(no);
            text.text = allcoinname[no];
        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool Neartodestination(GameObject obj)
        {
            if (Vector3.Distance(obj.transform.position, dropplace.transform.position)<1)
            {

                gamePlay = false;
                Transform test = obj.transform.parent;
                obj.transform.parent = dropplace.transform;
                obj.transform.localPosition =Vector3.zero;
                if(obj.GetComponent<DragObj>().moneyValue == no)
                {
                    obj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    
                   // Win.transform.DOScale(new Vector3(3, 3, 3), 1);

                    if (test.childCount != 0)
                    {



                        StartCoroutine(WaitForNext(obj));
                    }
                    else
                    {
                        StartCoroutine(LevelCompleted());
                      
                    }
                }
                else
                {
                    obj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
                    StartCoroutine(WrongAnswerAnimation());
                }
       
               


                return true;
            }

            return false;
        }
       
        IEnumerator WaitForNext(GameObject obj)
        {
            Win.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);


            Destroy(obj);
            Win.gameObject.SetActive(false);
           // Win.transform.localScale = Vector3.zero;
            no = Random.Range(0, allcoinname.Length);
            
            for (int i = 0; i < allno.Count; i++)
            {
                if(no == allno[i])
                {
                    no = Random.Range(0, allcoinname.Length);
                    i = -1;
                }
            }
            allno.Add(no);
            text.text = allcoinname[no];
            gamePlay = true;

        }
    }

}