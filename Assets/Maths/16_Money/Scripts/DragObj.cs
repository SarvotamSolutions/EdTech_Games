using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.Money.AddMoney
{
    public class DragObj : MonoBehaviour
    {
        private bool clicked;
        public float Moneyvlaue;
        private bool canChnagepos;
        private AudioSource sound;
        public AudioClip pickup, drop;
        private Vector3 lastpos;

        private void Start()
        {
            sound = GetComponent<AudioSource>();
            lastpos = transform.position;
        }
        private void OnMouseDown()
        {
            if (!Gamecontroll.Instance.gamePlay || Gamecontroll.Instance.totorial.totorialplaying)
                return;
            sound.clip = pickup;
            sound.Play();
            clicked = true;
        }
        public bool droped;
        public float speed;
        private void OnMouseUp()
        {
            if (!Gamecontroll.Instance.gamePlay || Gamecontroll.Instance.totorial.totorialplaying)
                return;

            sound.clip = drop;
            sound.Play();
            clicked = false;
            if (Gamecontroll.Instance.Neartodestination(this.gameObject))
            {
                Gamecontroll.Instance.totalmoneyadded += Moneyvlaue;
                Gamecontroll.Instance.totalMoneyaddedtext.text = "$ " + Gamecontroll.Instance.totalmoneyadded.ToString("0.00");
                droped = true;
            }

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
                    Gamecontroll.Instance.addmoneyimage.SetActive(false);
                    Gamecontroll.Instance.Priceplace.color = Gamecontroll.Instance.darkyellow;

                }
                else if (!Gamecontroll.Instance.Neartodestination(this.gameObject) && !Gamecontroll.Instance.addmoneyimage.activeInHierarchy)
                {
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
