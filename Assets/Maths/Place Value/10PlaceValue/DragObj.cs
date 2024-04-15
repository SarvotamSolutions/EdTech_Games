using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.placeHolder.value
{
    public class DragObj : MonoBehaviour
    {
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
            clicked = true;
            lastpos = transform.position;
        }

        private void OnMouseUp()
        {
            clicked = false;
            transform.position = lastpos;
            if (enterted)
            {
                switch (no) 
                {
                    case 1000:

                        if(GameController.instance.thousandcount == 10)
                        {
                            return;
                        }
                        droploction.transform.GetChild(GameController.instance.thousandcount).gameObject.SetActive(true);
                        GameController.instance.thousandcount++;
                        break;

                    case 100:
                        if (GameController.instance.hundredcount == 10)
                        {
                            return;
                        }
                        droploction.transform.GetChild(GameController.instance.hundredcount).gameObject.SetActive(true);
                        GameController.instance.hundredcount++;
                        break;

                    case 10:
                        if (GameController.instance.tencount == 10)
                        {
                            return;
                        }
                        droploction.transform.GetChild(GameController.instance.tencount).gameObject.SetActive(true);
                        GameController.instance.tencount++;
                        break;

                    case 1:
                        if (GameController.instance.onecount == 10)
                        {
                            return;
                        }
                        droploction.transform.GetChild(GameController.instance.onecount).gameObject.SetActive(true);
                        GameController.instance.onecount++;
                        break;
                }
                GameController.instance.loadPoint();
               

            }
            //    this.gameObject.SetActive(false);

            //    if (GameCondtroller.instace.checkForThirdStage())
            //    {

            //    }
            //}
            //else
            //{

          
            //}

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