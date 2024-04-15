using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.TeenBeads.drop10to20
{
    public class Drager : MonoBehaviour
    {
        private bool clicked;
        public int no;
        private bool canChnagepos;
        public Vector3 lastpos;


        private void Start()
        {
            //   lastpos = transform.position;
            transform.parent.SetSiblingIndex(Random.Range(0, transform.parent.parent.childCount));
        }
        private void OnMouseDown()
        {
            clicked = true;
            lastpos = transform.position;
        }

        private void OnMouseUp()
        {
            clicked = false;
            GameObject obj = GameController.instance.Neartodestination(this.gameObject);

            if (obj != null)
            {

                transform.position = obj.transform.position;
              
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