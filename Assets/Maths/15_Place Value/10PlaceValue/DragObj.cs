using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.placeHolder.value
{
    public class DragObj : MonoBehaviour
    {
        public AudioSource sound;
        public AudioClip pickup, drop;
        private bool clicked;
        public int no;
        private bool canChnagepos;
        private Vector3 lastpos;
        public GameObject droploction;
        public bool enterted;
        private void Start()
        {
            lastpos = transform.position;
        }
        private void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay || GameController.Instance.totorial.totorialplaying)
                return;
            sound.clip = pickup;
            sound.Play();
            clicked = true;
        }

        private void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay || GameController.Instance.totorial.totorialplaying)
                return;
            sound.clip = drop;
            sound.Play();
            clicked = false;
            transform.position = lastpos;
            if (enterted)
            {
                switch (no) 
                {
                    case 1000:

                        if(GameController.Instance.thousandcount == 10)
                        {
                            return;
                        }
                        droploction.transform.GetChild(GameController.Instance.thousandcount).gameObject.SetActive(true);
                        GameController.Instance.thousandcount++;
                        break;

                    case 100:
                        if (GameController.Instance.hundredcount == 10)
                        {
                            return;
                        }
                        droploction.transform.GetChild(GameController.Instance.hundredcount).gameObject.SetActive(true);
                        GameController.Instance.hundredcount++;
                        break;

                    case 10:
                        if (GameController.Instance.tencount == 10)
                        {
                            return;
                        }
                        droploction.transform.GetChild(GameController.Instance.tencount).gameObject.SetActive(true);
                        GameController.Instance.tencount++;
                        break;

                    case 1:
                        if (GameController.Instance.onecount == 10)
                        {
                            return;
                        }
                        droploction.transform.GetChild(GameController.Instance.onecount).gameObject.SetActive(true);
                        GameController.Instance.onecount++;
                        break;
                }
                GameController.Instance.loadPoint();
               

            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == droploction)
                enterted = true;
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == droploction)
                enterted = false;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && clicked )
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }
        }
    }
}