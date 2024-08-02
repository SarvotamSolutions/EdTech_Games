using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Maths.Number1to100.dragandrop
{
    public class Drag : MonoBehaviour
    {
        public AudioSource sound;

        public AudioClip pickup, drop;
        private bool clicked;
        public int no;
        private bool canChnagepos;
        private Vector3 lastpos;
        private void Start()
        {
            if(!sound)
            sound = GetComponent<AudioSource>();
            lastpos = transform.position;
        }
        private void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay || GameController.Instance.totorialcheck.totorialplaying)
                return;
            sound.clip = pickup;
            sound.Play();
            clicked = true;
          
        }

        private void OnMouseUp()
        {
           
            if (!GameController.Instance.gamePlay || GameController.Instance.totorialcheck.totorialplaying)
                return;
            sound.clip = drop;
            sound.Play();

            clicked = false;
            Debug.Log("XXXx");
            if (GameController.Instance.Neartodestination(this.gameObject))
            {
                transform.position = lastpos;
              
                this.gameObject.SetActive(false);
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