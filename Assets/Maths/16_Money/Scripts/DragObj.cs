using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.Money.AddMoney
{
    public class DragObj : MonoBehaviour
    {
        private bool clicked;
        public  float Moneyvlaue;
        private bool canChnagepos;
        private Vector3 lastpos;

        private void Start()
        {
            lastpos = transform.position;
        }
        private void OnMouseDown()
        {
            if (!Gamecontroll.Instance.gamePlay)
                return;
            clicked = true;
          //  lastpos = transform.position;
        }
        public bool droped;
        public float speed;
        private void OnMouseUp()
        {
            if (!Gamecontroll.Instance.gamePlay)
                return;
            clicked = false;
            if (Gamecontroll.Instance.Neartodestination(this.gameObject))
            {
                Gamecontroll.Instance.totalmoneyadded += Moneyvlaue;
                Gamecontroll.Instance.totalMoneyaddedtext.text = "$ "+ Gamecontroll.Instance.totalmoneyadded.ToString("0.00") ;
                droped = true;
              //  transform.position += Vector3.down *19;
            }
            //this.gameObject.SetActive(false);

            //if (GameCondtroller.instace.checkForThirdStage())
            //{

            //}

            else
            {

                transform.position = lastpos;
            }

        }


        private void Update()
        {
            if (clicked)
            {
                if (Gamecontroll.Instance.Neartodestination(this.gameObject) && Gamecontroll.Instance.addmoneyimage.activeInHierarchy)
                {
                    Debug.Log("XXX");
                    Gamecontroll.Instance.addmoneyimage.SetActive(false);
                    Gamecontroll.Instance.Priceplace.color = Gamecontroll.Instance.darkyellow;

                }
                else if (!Gamecontroll.Instance.Neartodestination(this.gameObject) && !Gamecontroll.Instance.addmoneyimage.activeInHierarchy)
                {
                    Debug.Log("YYY");
                    Gamecontroll.Instance.addmoneyimage.SetActive(true);
                    Gamecontroll.Instance.Priceplace.color = Gamecontroll.Instance.yellow;
                }
            }
            if (Input.GetMouseButton(0) && clicked)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }

            if (droped)
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
                StartCoroutine(StopDroping());
            }
        }
        IEnumerator StopDroping()
        {
            yield return new WaitForSeconds(.5f);
            droped = false;
            transform.position = lastpos;
            if (!Gamecontroll.Instance.addmoneyimage.activeInHierarchy)
            {
                Gamecontroll.Instance.addmoneyimage.SetActive(true);
                Gamecontroll.Instance.Priceplace.color = Gamecontroll.Instance.yellow;
            }
            StopAllCoroutines();
        }
    }
}
