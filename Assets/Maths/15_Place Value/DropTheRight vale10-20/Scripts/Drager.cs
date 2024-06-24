using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.placeValue.Dropbeads
{
    public class Drager : MonoBehaviour
    {
        private bool clicked;
        public int no;
        private bool canChnagepos;
        public  Vector3 lastpos;

        private void Start()
        {
            lastpos = transform.position;
        }
        private void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay)
                return;
            clicked = true;
            
        }

        private void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay)
                return;
            clicked = false;
            if (GameController.Instance.Neartodestination(this.gameObject))
            {

               // transform.position = lastpos;
               // gameObject.SetActive(false);
            }
            else
                transform.position = lastpos;

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