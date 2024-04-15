using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.placeValue.slectOption
{
    public class DragDroper : MonoBehaviour
    {
        private bool clicked;
        public int no;
        private bool canChnagepos;
        public Vector3 lastpos;
        private void OnMouseDown()
        {
            clicked = true;
            lastpos = transform.position;
        }

        private void OnMouseUp()
        {
            clicked = false;
            Gamecontroller.instace.dropbox.color = Color.white;
            if (Gamecontroller.instace.Neartodestination(this.gameObject))
            {

                
                transform.position = Gamecontroller.instace.dropbox.transform.position;

                if(Gamecontroller.instace.number == no)
                {
                    GetComponent<SpriteRenderer>().sprite = Gamecontroller.instace.currectanswer;
                    Gamecontroller.instace.resetgame(this);
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = Gamecontroller.instace.wronganswer;
                    StartCoroutine(Gamecontroller.instace.WairforRelode(this));
                }
                // gameObject.SetActive(false);

            }else
            {
                transform.position = lastpos;
            }
            //if (GameController.instance.Neartodestination(this.gameObject))
            //{
            //    transform.position = lastpos;
            //    gameObject.SetActive(false);
            //}
           

        }
        private void Update()
        {
            if (Input.GetMouseButton(0) && clicked)
            {
                if (Gamecontroller.instace.Neartodestination(this.gameObject))
                {
                    Gamecontroller.instace.dropbox.color = Color.black;
                }
                else
                {
                    Gamecontroller.instace.dropbox.color = Color.white;
                }

               Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }


        }
    }
}
