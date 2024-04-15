using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maths.graterAndLesser
{
    public class Dragin : MonoBehaviour
    {
        private bool clicked;
        private bool canChnagepos;
        public Vector3 lastpos;
        public bool grater;

        private void Start()
        {
            lastpos = transform.position;
        }
        private void OnMouseDown()
        {
            if (!GameController.instance.GraterorLessselct)
                return;

            clicked = true;
            lastpos = transform.position;
        }

        private void OnMouseUp()
        {


            clicked = false;
            if (Vector3.Distance(transform.position, GameController.instance.dropingobj.transform.position) < 1)
            {

                if (grater && GameController.instance.Numbers[0] > GameController.instance.Numbers[1])
                {
                    GameController.instance.GraterorLessselct = false;
                    canChnagepos = true;
                    transform.position = GameController.instance.dropingobj.transform.position;
                    GameController.instance.FinalCheck(this.GetComponent<SpriteRenderer>());

                }
                else
                if (!grater && GameController.instance.Numbers[0] < GameController.instance.Numbers[1])
                {
                    GameController.instance.GraterorLessselct = false;
                    canChnagepos = true;
                    transform.position = GameController.instance.dropingobj.transform.position;
                    GameController.instance.FinalCheck(this.GetComponent<SpriteRenderer>());
                }
                else
                {
                    transform.position = lastpos;
                }
                //gameObject.SetActive(false);
                //transform.position = lastpos;
            }
            else
            {
                transform.position = lastpos;
            }

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