using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace Maths.NumberRoads
{
    public class Draging : MonoBehaviour
    {
        public AudioSource sound;
        public AudioClip pickup, drop;
        private bool clicked;
        public int no;
        private bool canChnagepos;
        public Vector3 lastpos;


        private void Start()
        {
           // sound = GetComponent<AudioSource>();
            lastpos = transform.position;
            if(transform.GetChild(0).GetComponent<TextMeshPro>())
            transform.GetChild(0).GetComponent<TextMeshPro>().text = no.ToString();
          //  transform.parent.SetSiblingIndex(Random.Range(0, transform.parent.parent.childCount));
        }
        private void OnMouseDown()
        {
            if (GameController.instace.totorialcheck.totorialplaying || GameController.instace.gameplay == false)
                return;
            sound.clip = pickup;
            sound.Play();
            clicked = true;
            //   lastpos = transform.position;
        }

        private void OnMouseUp()
        {
            if (GameController.instace.totorialcheck.totorialplaying || !clicked )
                return;
            clicked = false;
            sound.clip = drop;
            sound.Play();
            if (GameController.instace.Neartodestination(this.gameObject))
            {

                transform.position = GameController.instace.dropplace[GameController.instace.no-1].transform.position;
                if (no == GameController.instace.no)//Cuurrect answer
                {
                  //GetComponent<BoxCollider2D>().enabled = false;
                  //transform.parent = GameController.instace.allnumber[GameController.instace.no].transform;
                    GameController.instace.Draw_canvas.SetActive(false);
                    GameController.instace.dropplace[GameController.instace.no - 1].GetComponent<SpriteRenderer>().sprite = GameController.instace.dropedCurrectanswer;
                    GameController.instace.dropplace[GameController.instace.no - 1].GetComponentInChildren<TextMeshPro>().text = GetComponentInChildren<TextMeshPro>().text;

                  //transform.GetComponent<SpriteRenderer>().sprite = GameController.instace.dropedCurrectanswer;
                    if (no < 10)
                    {
                        GameController.instace.ChangetoColorfiller();
                    }
                    else
                    {
                        GameController.instace.StartCoroutine(GameController.instace.LevelCompleted());
                    }

                    Destroy(this.gameObject);

                }
                else
                {
                   
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
            GameController.instace.gameplay = false;
            SpriteRenderer tempsprite = transform.GetComponent<SpriteRenderer>();
            TextMeshPro temptext = transform.GetComponentInChildren<TextMeshPro>();
            //transform.gameObject.SetActive(false);
            tempsprite.enabled = false;
            GameController.instace.dropplace[GameController.instace.no - 1].GetComponent<SpriteRenderer>().sprite = GameController.instace.dropedWrongAnswer;
            GameController.instace.dropplace[GameController.instace.no - 1].GetComponentInChildren<TextMeshPro>().text = temptext.text;
            temptext.enabled = false;
            GameController.instace.wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            GameController.instace.gameplay = true;
            temptext.enabled = true;
            GameController.instace.dropplace[GameController.instace.no - 1].GetComponentInChildren<TextMeshPro>().text = "";
            tempsprite.enabled = true;
            GameController.instace.dropplace[GameController.instace.no - 1].GetComponent<SpriteRenderer>().sprite = GameController.instace.DropedNormalAnswer;
            GameController.instace.wrongAnswer_animtion.SetActive(false);
            transform.position = lastpos;
        }
        private void Update()
        {
            if (GameController.instace.totorialcheck.totorialplaying)
                return;
            if (Input.GetMouseButton(0) && clicked)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }


        }
    }
}