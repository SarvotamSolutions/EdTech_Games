using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.Money.MoneyQuiz
{
    public class DragObj : MonoBehaviour
    {
        private bool clicked;
        public int moneyValue;
        private bool canChnagepos;
        private Vector3 lastpos;
        public Vector3 permantlastpos;
        private void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay)
                return;
            clicked = true;
           // lastpos = transform.position;
            
        }
        private void Start()
        {
            permantlastpos = transform.position;
            lastpos = transform.position;
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
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                transform.position = lastpos;
            }
        }
        private void Update()
        {
            if (Input.GetMouseButton(0) && clicked)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }


        }
    }
}
