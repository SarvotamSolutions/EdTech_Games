using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maths.Times.DragDrop
{
    public class Draging : MonoBehaviour
    {
        private bool clicked;
        public AudioSource sound;
        public AudioClip pickup, drop;
        public int no;
        private bool canChnagepos;
        private Vector3 lastpos;

        private void Start()
        {
            //sound = GetComponent<AudioSource>();
            lastpos = transform.position;
        }
        private void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay || GameController.Instance.totorial.totorialplaying)
                return;
            sound.clip = pickup;
            sound.Play();   
            clicked = true;
    
        }

        private void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay || GameController.Instance.totorial.totorialplaying)
                return;
            sound.clip = drop;
            sound.Play();
            clicked = false;
            if (GameController.Instance.Neartodestination(this.gameObject))
            {
                transform.position = lastpos;
                gameObject.SetActive(false);
            }
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
