using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Maths.NumberRoads.Building_Number_rods
{
    public class Drager : MonoBehaviour
    {
        private bool clicked;
        public int no;
        private bool canChnagepos;
        public Vector3 lastpos;


        private void Start()
        {
               lastpos = transform.position;
            //transform.parent.SetSiblingIndex(Random.Range(0, transform.parent.parent.childCount));
        }
        private void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay)
                return;
            //   if (EventSystem.current.IsPointerOverGameObject()) return;
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
            clicked = true;
          //  lastpos = transform.position;
        }

        private void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay)
                return;
            clicked = false;


            if (GameController.Instance.Neartodestination(this.gameObject))
            {

                transform.position = GameController.Instance.question[GameController.Instance.no].transform.position;

                if (no == GameController.Instance.no + 1)
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    GameController.Instance.question[GameController.Instance.no].transform.GetChild(0).gameObject.SetActive(false);
                    this.transform.parent = GameController.Instance.question[GameController.Instance.no].transform;
                    if (!GameController.Instance.Ai)
                    {
                        //currect answer
                    
                       
                        GameController.Instance.no++;
                        
                        if (no == 10)
                            StartCoroutine(GameController.Instance.LevelCompleted());
                        else
                        {
                            GameController.Instance.question[GameController.Instance.no].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;
                        }
                    }
                    else
                    {
                      
                        GameController.Instance.Hodler.SetActive(false);
                        GameController.Instance.alltext[GameController.Instance.no].transform.parent.gameObject.SetActive(true);
                        GameController.Instance.drawCanvas.SetActive(true);
                        GameController.Instance.ai.textResult = GameController.Instance.alltext[GameController.Instance.no];
                        GameController.Instance.aireconniger.Recognigingnumber = no.ToString();
                        GameController.Instance.aireconniger.Changerecogniger();
                    }
                }
                else
                {
                    GameController.Instance.gamePlay = false;
                    //  GameController.Instance.WrongAnswer();
                    //wrong answer
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
                    GameController.Instance.question[GameController.Instance.no].SetActive(false);
                    StartCoroutine(WrongAnswerAnimation());
                }
                //  transform.position = obj.transform.position;

                // transform.position = lastpos;
                // gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
                transform.position = lastpos;
            }

        }

        IEnumerator WrongAnswerAnimation()
        {
            GameController.Instance.wrongAnswer_animtion.SetActive(true);
            
            yield return new WaitForSeconds(2);
            GameController.Instance.question[GameController.Instance.no].SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);

           // transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.w\;
            GameController.Instance.wrongAnswer_animtion.SetActive(false);
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
            transform.position = lastpos;
            GameController.Instance.gamePlay = true;
        }
        private void Update()
        {
            if (Input.GetMouseButton(0) && clicked)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                pos.x = pos.x - no/2.75f;
                transform.position = pos;
            }


        }
    }
}