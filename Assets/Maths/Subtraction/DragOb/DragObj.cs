using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Maths.Substraction.DragingObject
{
    public class DragObj : MonoBehaviour
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

                GameController.instance.dustbin.GetComponent<SpriteRenderer>().sprite = GameController.instance.closeDustBinSprite;
                transform.parent = GameController.instance.dustbin.transform;

                gameObject.SetActive(false);
                transform.position = lastpos;
            }
            transform.position = lastpos;

        }
        private void Update()
        {
            if (Input.GetMouseButton(0) && clicked)
            {
                if (GameController.instance.Neartodestination(this.gameObject))
                {
                    GameController.instance.dustbin.GetComponent<SpriteRenderer>().sprite = GameController.instance.OpenDustbinSprite;
                }
                else
                {
                    GameController.instance.dustbin.GetComponent<SpriteRenderer>().sprite = GameController.instance.closeDustBinSprite;
                }
                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }


        }
    }
}