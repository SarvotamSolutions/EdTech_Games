using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Maths.NumberRoads.Making_10_with_Number_roads
{
    public class Draging : MonoBehaviour
    {
        private AudioSource sound;
        public AudioClip pickup, drop;
        private bool clicked;
        public int no;
        private SpriteRenderer border;
        private bool canChnagepos;
        public Vector3 lastpos;
        public bool Leftside;

        private void Start()
        {
            sound = GetComponent<AudioSource>();
            border = transform.GetChild(0).GetComponent<SpriteRenderer>();
            lastpos = transform.position;
        }
        private void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay || GameController.Instance.totrial.totorialplaying)
                return;

            sound.clip = pickup;
            sound.Play();
            clicked = true;
            GameController.Instance.selecteddraging = this;
        }

        private void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay || GameController.Instance.totrial.totorialplaying)
                return;


            sound.clip = drop;
            sound.Play();
            clicked = false;
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

                    if (GameController.Instance.firstanswer)
                    {
                        SpriteRenderer nextholdsprite = GameController.Instance.dropplace[GameController.Instance.no].transform.GetChild(0).GetComponent<SpriteRenderer>();
                        nextholdsprite.sprite = GameController.Instance.completeholder;
                        GameController.Instance.totrial.directionWindow();
                        GameController.Instance.firstanswer = false;
                        GameController.Instance.no++;
                        GameController.Instance.draginno = 10 - (GameController.Instance.no);
                        nextholdsprite = GameController.Instance.dropplace[GameController.Instance.no].transform.GetChild(0).GetComponent<SpriteRenderer>();
                        nextholdsprite.sprite = GameController.Instance.selctholder;
                        nextholdsprite.sortingOrder = 2;
                        GameController.Instance.holder2.Remove(GameController.Instance.holder2[0]);
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

                        GetComponent<Collider2D>().enabled = false;
                        GameController.Instance.blocker1.SetActive(true);
                        GameController.Instance.blocker2.SetActive(false);
                        GameController.Instance.gamePlay = true;

                    }
                    else
                    {
                        GameController.Instance.totrial.directionWindow();
                        if (no == 10)
                        {
                            GameController.Instance.draginno = 9;
                            Debug.Log("XXX");
                            SpriteRenderer nextholdsprite = GameController.Instance.dropplace[GameController.Instance.no].transform.GetChild(0).GetComponent<SpriteRenderer>();
                            nextholdsprite.sprite = GameController.Instance.completeholder;

                            GameController.Instance.no++;
                            nextholdsprite = GameController.Instance.dropplace[GameController.Instance.no].transform.GetChild(0).GetComponent<SpriteRenderer>();
                            nextholdsprite.sprite = GameController.Instance.selctholder;
                            nextholdsprite.sortingOrder = 2;
                            GameController.Instance.holder1.Remove(GameController.Instance.holder2[0]);
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

                            GetComponent<Collider2D>().enabled = false;
                            GameController.Instance.gamePlay = true;
                        }
                        else
                        {
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
                    }
                    GameController.Instance.selecteddraging = null;
                }
                else
                {

                    border.gameObject.SetActive(true);
                    StartCoroutine(WrongAnswerAnimation());
                }
            }
            else
            {
                transform.position = lastpos;
                GameController.Instance.selecteddraging = null;
            }
        }

        IEnumerator WrongAnswerAnimation()
        {
            GameController.Instance.wrongAnswer_animtion.SetActive(true);
            SpriteRenderer nextholdsprite = GameController.Instance.dropplace[GameController.Instance.no].transform.GetChild(0).GetComponent<SpriteRenderer>();
            Sprite tempsprite = nextholdsprite.sprite;
            nextholdsprite.sprite = GameController.Instance.wronganswer;
            yield return new WaitForSeconds(2);
            nextholdsprite.sprite = tempsprite;
            GameController.Instance.wrongAnswer_animtion.SetActive(false);
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