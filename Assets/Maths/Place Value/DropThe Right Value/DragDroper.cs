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
        private void Start()
        {
            lastpos = transform.position;
        }
        private void OnMouseDown()

        {
            if (!Gamecontroller.Instance.gamePlay)
                return;
            clicked = true;
            
        }

        private void OnMouseUp()
        {
            if (!Gamecontroller.Instance.gamePlay)
                return;
          
            clicked = false;
            Gamecontroller.Instance.dropbox.color = Color.white;
            if (Gamecontroller.Instance.Neartodestination(this.gameObject))
            {
                Gamecontroller.Instance.gamePlay = false;

                transform.position = Gamecontroller.Instance.dropbox.transform.position;

                if(Gamecontroller.Instance.number == no)
                {
                    GetComponent<SpriteRenderer>().sprite = Gamecontroller.Instance.currectanswer;
                    Gamecontroller.Instance.resetgame(this);
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = Gamecontroller.Instance.wronganswer;
                    StartCoroutine(Gamecontroller.Instance.WairforRelode(this));
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
                if (Gamecontroller.Instance.Neartodestination(this.gameObject))
                {
                    Gamecontroller.Instance.dropbox.color = Color.black;
                }
                else
                {
                    Gamecontroller.Instance.dropbox.color = Color.white;
                }

               Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }


        }
    }
}
