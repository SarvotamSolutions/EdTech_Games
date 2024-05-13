using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maths.Addision.AddisitonwithColors
{
    public class Drag : MonoBehaviour
    {
        private bool clicked;
        public int no;
        private bool canChnagepos;
        private Vector3 lastpos;

        private void Start()
        {
            lastpos = transform.position;
        }
        private void OnMouseDown()
        {
            if (!GameCondtroller.Instance.gamePlay)
                return;
            clicked = true;
           
        }

        private void OnMouseUp()
        {
            if (!GameCondtroller.Instance.gamePlay)
                return;
            clicked = false;
            if (GameCondtroller.Instance.Neartodestination(this.gameObject))
            {
                transform.position = lastpos;
                this.gameObject.SetActive(false);

                if (GameCondtroller.Instance.checkForThirdStage())
                {

                }
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
