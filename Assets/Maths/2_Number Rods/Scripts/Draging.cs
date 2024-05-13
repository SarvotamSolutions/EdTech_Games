using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace Maths.NumberRoads
{
    public class Draging : MonoBehaviour
    {
        private bool clicked;
        public int no;
        private bool canChnagepos;
        public Vector3 lastpos;


        private void Start()
        {
            lastpos = transform.position;
            if(transform.GetChild(0).GetComponent<TextMeshPro>())
            transform.GetChild(0).GetComponent<TextMeshPro>().text = no.ToString();
          //  transform.parent.SetSiblingIndex(Random.Range(0, transform.parent.parent.childCount));
        }
        private void OnMouseDown()
        {
          
            clicked = true;
            //   lastpos = transform.position;
        }

        private void OnMouseUp()
        {
            clicked = false;

            if (GameController.instace.Neartodestination(this.gameObject))
            {

                transform.position = GameController.instace.dropplace[GameController.instace.no-1].transform.position;
                if (no == GameController.instace.no)
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                    transform.parent = GameController.instace.allnumber[GameController.instace.no].transform;
                    GameController.instace.Draw_canvas.SetActive(false);
                    transform.GetComponent<SpriteRenderer>().sprite = GameController.instace.dropedCurrectanswer;
                    if (no < 10)
                    {
                        GameController.instace.ChangetoColorfiller();
                    }
                    else
                    {
                        StartCoroutine(GameController.instace.LevelCompleted());
                    }

                }
                else
                {
                    transform.GetComponent<SpriteRenderer>().sprite = GameController.instace.dropedWrongAnswer;
                    StartCoroutine(WrongAnimation());

                    //wrong answer
                }
                // transform.position = lastpos;
                // gameObject.SetActive(false);
            }
            else
                transform.position = lastpos;

        }

        IEnumerator WrongAnimation()
        {
            GameController.instace.wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            GameController.instace.wrongAnswer_animtion.SetActive(false);
            transform.GetComponent<SpriteRenderer>().sprite = GameController.instace.DropedNormalAnswer;
            transform.position = lastpos;
        }
        private void Update()
        {
            if (Input.GetMouseButton(0) && clicked)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }


        }
    }
}