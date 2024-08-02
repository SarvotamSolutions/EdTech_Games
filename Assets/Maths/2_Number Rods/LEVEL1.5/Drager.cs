using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Maths.NumberRoads.Building_Number_rods
{
    public class Drager : MonoBehaviour
    {
        private AudioSource sound;
        public AudioClip pickup, drop;
        private bool clicked;
        public int no;
        private bool canChnagepos;
        public Vector3 lastpos;
        

        private void Start()
        {
            sound = GetComponent<AudioSource>();
            lastpos = transform.position;
            //transform.parent.SetSiblingIndex(Random.Range(0, transform.parent.parent.childCount));
        }
        private void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay|| GameController.Instance.totrial.totorialplaying)
                return;
            sound.clip = pickup;
            sound.Play();
            transform.GetChild(0).gameObject.SetActive(false);
            //   if (EventSystem.current.IsPointerOverGameObject()) return;
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
            clicked = true;
          //  lastpos = transform.position;
        }

        private void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay || GameController.Instance.totrial.totorialplaying)
                return;
            clicked = false;

            sound.clip = drop;
            sound.Play();
            if (GameController.Instance.Neartodestination(this.gameObject))
            {

                transform.position = GameController.Instance.question[GameController.Instance.no].transform.position;

                if (no == GameController.Instance.no + 1)
                {
                    GetComponent<BoxCollider2D>().enabled = false;

                    // transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    for (int i = GameController.Instance.question[GameController.Instance.no].transform.childCount-1; i>= 0; i--)
                    {
                        GameController.Instance.question[GameController.Instance.no].transform.GetChild(i).gameObject.SetActive(false);
                    }
                    
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
                        for (int i = GameController.Instance.question[GameController.Instance.no].transform.childCount - 1; i >= 0; i--)
                        {
                            GameController.Instance.question[GameController.Instance.no].transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = GameController.Instance.selectedrod;
                        }
                    }
                    else
                    {

                        GameController.Instance.TextHolder.SetActive(true);
                        if (GameController.Instance.totrial.directionno < GameController.Instance.totrial.directionpanel.Length)
                        {
                           GameController.Instance.totrial.directionWindow();
                        }
                        GameController.Instance.Hodler.SetActive(false);
                        if(GameController.Instance.no>0)
                            GameController.Instance.alltext[GameController.Instance.no-1].transform.parent.GetComponent<SpriteRenderer>().sprite = GameController.Instance.curerctanswer;
                        GameController.Instance.alltext[GameController.Instance.no].text ="?";
                        GameController.Instance.alltext[GameController.Instance.no].transform.parent.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
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
                   // transform.GetChild(0).gameObject.SetActive(true);
                  //  transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
                    GameController.Instance.question[GameController.Instance.no].SetActive(false);
                    StartCoroutine(WrongAnswerAnimation());
                }
                //  transform.position = obj.transform.position;

                // transform.position = lastpos;
                // gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
                //transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
                transform.position = lastpos;
            }

        }

        IEnumerator WrongAnswerAnimation()
        {
            GameController.Instance.wrongAnswer_animtion.SetActive(true);
            
            yield return new WaitForSeconds(2);
            GameController.Instance.question[GameController.Instance.no].SetActive(true);
            transform.GetChild(0).gameObject.SetActive(true);

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