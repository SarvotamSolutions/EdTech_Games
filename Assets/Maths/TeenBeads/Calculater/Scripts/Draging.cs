using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.TeenBeads.Caluclation
{
    public class Draging : MonoBehaviour
    {
        private bool clicked;
        public int no;
        private bool canChnagepos;
        private Vector3 lastpos;
        private void OnMouseDown()
        {
            clicked = true;
            lastpos = transform.position;
        }

        private void OnMouseUp()
        {
            clicked = false;
            if (GameController.instance.Neartodestination(this.gameObject))
            {

                
                transform.position = lastpos;
            //    gameObject.SetActive(false);
            }
            transform.position = lastpos;

        }
        private void Update()
        {
            if (GameController.instance.Completed)
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