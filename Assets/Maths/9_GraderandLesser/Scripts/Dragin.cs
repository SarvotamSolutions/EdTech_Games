using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Maths.graterAndLesser
{
    public class Dragin : MonoBehaviour
    {
        public AudioSource sound;
        public AudioClip pickup, drop;
        private bool clicked;
        public Vector3 lastpos;
        public bool grater ,lesser;

        public int id;
        private void Start()
        {
            //sound = GetComponent<AudioSource>();
            lastpos = transform.position;
            TextUpdate();
        }
        private void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay || GameController.Instance.totorialcheck.totorialplaying)
                return;
            sound.clip = pickup;
            sound.Play();
            //if (!GameController.instance.GraterorLessselct)
            //    return;
            GameController.Instance.drager = this.gameObject;
            clicked = true;
            lastpos = transform.position;
        }

        private void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay || GameController.Instance.totorialcheck.totorialplaying)
                return;

            clicked = false;
            sound.clip = drop;
            sound.Play();
            if (Vector3.Distance(transform.position, GameController.Instance.inputs[GameController.Instance.no].transform.position) < 3)
            {

              
                if (grater || lesser)
                {
                    GameController.Instance.gamePlay = false;

                    //  transform.position = GameController.instance.dropingobj.transform.position;
                    //GameController.instance.GraterorLessselct = false;
                    if (grater && GameController.Instance.Numbers[0] > GameController.Instance.Numbers[1])
                    {
                        GameController.Instance.inputs[GameController.Instance.no].color = Color.white;
                        GameController.Instance.inputs[GameController.Instance.no].sprite = GameController.Instance.currectAnswer;
                        GameController.Instance.input_text[GameController.Instance.no].text = ">";
                        //GameController.instance.Answering();
                      //  canChnagepos = true;

                        GameController.Instance.FinalCheck(this.GetComponent<SpriteRenderer>());

                    }
                    else
                    if (!grater && GameController.Instance.Numbers[0] < GameController.Instance.Numbers[1])
                    {
                        GameController.Instance.inputs[GameController.Instance.no].color = Color.white;
                        GameController.Instance.inputs[GameController.Instance.no].sprite = GameController.Instance.currectAnswer;
                        GameController.Instance.input_text[GameController.Instance.no].text = "<";
                        //  GameController.instance.Answering();
                        //  transform.position = GameController.instance.dropingobj.transform.position;
                        GameController.Instance.FinalCheck(this.GetComponent<SpriteRenderer>());
                    }
                    else
                    {
                        GameController.Instance.input_text[GameController.Instance.no].text = transform.GetChild(0).GetComponent<TextMeshPro>().text;
                        GameController.Instance.WrongAnimation();
                    }
                }
                else
                {
                    Debug.Log(GameController.Instance.Numbers[GameController.Instance.no]);
                    Debug.Log(GameController.Instance.allno[id]);
                    if (GameController.Instance.allno[id] == GameController.Instance.Numbers[GameController.Instance.no]+1)
                    {
                        GameController.Instance.Answering();
                        if(GameController.Instance.no >= 2)
                        {
                            GameController.Instance.graterlessparent.SetActive(true);
                            GameController.Instance.optioncontins.SetActive(false);
                        }
                        transform.gameObject.SetActive(false);
                     //   transform.position = GameController.instance.inputs[GameController.instance.no].transform.position;
                       
                    }
                    else
                    {

                          GameController.Instance.gamePlay = false;
                        GameController.Instance.input_text[GameController.Instance.no].text = GameController.Instance.allno[id].ToString();
                        GameController.Instance.WrongAnimation();
                        //wrong answer
                    }
                    //normal answer

                   



                }
                //gameObject.SetActive(false);
                //transform.position = lastpos;
                //transform.position = lastpos;
                transform.gameObject.SetActive(false);

            }
            else
            {
            }
                transform.position = lastpos;

        }
        public void TextUpdate()
        {
            if (!grater && !lesser)
            {
                GetComponentInChildren<TextMeshPro>().text = GameController.Instance.allno[id].ToString();
            }
        }
        private void OnEnable()
        {
            
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