using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Maths.NumberRoads.Making_10_with_Number_roads
{
    public class Draging : MonoBehaviour
    {

        private bool clicked;
        public int no;
        private SpriteRenderer border;
        private bool canChnagepos;
        public Vector3 lastpos;
        public bool Leftside;
        public bool full;
        private void Start()
        {
            border = transform.GetChild(0).GetComponent<SpriteRenderer>();
               lastpos = transform.position;
         //   transform.parent.SetSiblingIndex(Random.Range(0, transform.parent.parent.childCount));
        }
        private void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay)
            {
                Debug.Log("Notavilabe");
                return;
            }
                // if (EventSystem.current.IsPointerOverGameObject()) return;
            clicked = true;
            GameController.Instance.selecteddraging = this;
        //    lastpos = transform.position;
        }

        private void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay)
            {
                Debug.Log("Notavilabe");
                return;
            }
            clicked = false;
            //   GameObject obj = GameController.instance.Neartodestination(this.gameObject);
            if (GameController.Instance.Neartodestination())
            {


                if (Leftside)
                    transform.position = GameController.Instance.dropplace[GameController.Instance.no].transform.GetChild(1).transform.position;
                else
                    transform.position = GameController.Instance.dropplace[GameController.Instance.no].transform.GetChild(2).transform.position;

                GameController.Instance.gamePlay = false;
                if (Mathf.Abs(no) == GameController.Instance.draginno)
                {

                    border.color = Color.green;
                    transform.parent = GameController.Instance.dropplace[GameController.Instance.no].transform;
                    GetComponent<Collider2D>().enabled = false;
                    border.gameObject.SetActive(true);
                    GameController.Instance.draginno = 10 - GameController.Instance.draginno;
                   
                    if (GameController.Instance.firstanswer|| (full && Leftside))
                    {
                        Debug.Log("CCCC final" + GameController.Instance.draginno);
                        GameController.Instance.firstanswer = false;
                        GameController.Instance.no++;
                        GameController.Instance.draginno = 10 - (GameController.Instance.no);
                        GameController.Instance.dropplace[GameController.Instance.no].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;
                        
                        if (!full)
                        {
                            GameController.Instance.holder2.Remove(GameController.Instance.holder2[0]);
                        }
                        else
                        {

                        }
                      
                        GetComponent<Collider2D>().enabled = false;
                        if (no == -9)
                        {
                            GameController.Instance.firstanswer = true;
                        }
                        else
                        {
                            foreach (var item in GameController.Instance.holder2)
                            {
                                if (item)
                                    item.GetComponent<Collider2D>().enabled = false;
                            }

                            foreach (var item in GameController.Instance.holder1)
                            {
                                if (item)
                                    item.GetComponent<Collider2D>().enabled = true;
                            }

                            GameController.Instance.blocker1.SetActive(true);
                            GameController.Instance.blocker2.SetActive(false);
                        }
                        GameController.Instance.hinttext.text = "drage and drop the number rod " + GameController.Instance.draginno;
                        GameController.Instance.gamePlay = true;
                        
                    }
                    else 
                    {
                        Debug.Log("CCCC" + GameController.Instance.draginno);
                        //GetComponent<Collider2D>().enabled = false;
                        GameController.Instance.hinttext.text = "drag the right no to make 10";
                          
                            GameController.Instance.firstanswer = true;
                        GameController.Instance.holder1.Remove(GameController.Instance.holder1[0]);
                        foreach (var item in GameController.Instance.holder2)
                        {
                            if (item)
                                item.GetComponent<Collider2D>().enabled = true;
                        }

                        foreach (var item in GameController.Instance.holder1)
                        {
                            if (item)
                                item.GetComponent<Collider2D>().enabled = false;
                        }
                        GameController.Instance.blocker1.SetActive(false);
                        GameController.Instance.blocker2.SetActive(true);
                        GetComponent<Collider2D>().enabled = false;
                        GameController.Instance.gamePlay = true;
                    }
                   // }
                    //else
                    //{
                    //    GetComponent<Collider2D>().enabled = false;
                    //    GameController.Instance.gamePlay = true;
                    //}
                    GameController.Instance.selecteddraging = null;
                }
                else
                {

                    border.color = Color.red;
                    border.gameObject.SetActive(true);
                    StartCoroutine(WrongAnswerAnimation());
                    //wrong answer;
                }
            }
            else
            {
               // GameController.Instance.no++;
                transform.position = lastpos;
                GameController.Instance.selecteddraging = null;
            }
                //if (obj != null)
                //{

                //    transform.position = obj.transform.position;

                //    // transform.position = lastpos;
                //    // gameObject.SetActive(false);
                //}
                //else
                // 

        }

        IEnumerator WrongAnswerAnimation()
        {
            GameController.Instance.wrongAnswer_animtion.SetActive(true);

            yield return new WaitForSeconds(2);
         //   GameController.Instance.question[GameController.Instance.no].SetActive(true);
            border.gameObject.SetActive(false);

            // transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.w\;
            GameController.Instance.wrongAnswer_animtion.SetActive(false);
          //  transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
            transform.position = lastpos;
            GameController.Instance.gamePlay = true;
            GameController.Instance.selecteddraging = null;
        }
        private void Update()
        {
            if (Input.GetMouseButton(0) && clicked)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                pos.x = pos.x - no / 2.75f;
                transform.position = pos;
            }


        }

    }
}