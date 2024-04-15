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
            clicked = true;
            lastpos = transform.position;
        }
        public bool droped;
        public float speed;
        private void OnMouseUp()
        {
            clicked = false;
            if (Gamecontroll.instace.Neartodestination(this.gameObject))
            {
                Gamecontroll.instace.totalmoneyadded += Moneyvlaue;
                Gamecontroll.instace.totalMoneyaddedtext.text = Gamecontroll.instace.totalmoneyadded.ToString("0.00") + " $";
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
                if (Gamecontroll.instace.Neartodestination(this.gameObject) && Gamecontroll.instace.addmoneyimage.activeInHierarchy)
                {
                    Debug.Log("XXX");
                    Gamecontroll.instace.addmoneyimage.SetActive(false);
                    Gamecontroll.instace.Priceplace.color = Gamecontroll.instace.darkyellow;

                }
                else if (!Gamecontroll.instace.Neartodestination(this.gameObject) && !Gamecontroll.instace.addmoneyimage.activeInHierarchy)
                {
                    Debug.Log("YYY");
                    Gamecontroll.instace.addmoneyimage.SetActive(true);
                    Gamecontroll.instace.Priceplace.color = Gamecontroll.instace.yellow;
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
            if (!Gamecontroll.instace.addmoneyimage.activeInHierarchy)
            {
                Gamecontroll.instace.addmoneyimage.SetActive(true);
                Gamecontroll.instace.Priceplace.color = Gamecontroll.instace.yellow;
            }
            StopAllCoroutines();
        }
    }
}
