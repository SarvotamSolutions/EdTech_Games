using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Maths.TeenBeads.Caluclation
{
    public class Draging : MonoBehaviour
    {
        public AudioSource sound;
        public AudioClip pickup, drop;
        private bool clicked;
        public int no;
        private bool canChnagepos;
        private Vector3 lastpos;
        private void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay || GameController.Instance.totorial.totorialplaying)
                return;
            sound.clip = pickup;
            sound.Play();
            clicked = true;
           
        }
        private void Start()
        {
            lastpos = transform.position;
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
            //    gameObject.SetActive(false);
            }
            transform.position = lastpos;

        }
        private void Update()
        {
            if (GameController.Instance.Completed)
                return;
            if (Input.GetMouseButton(0) && clicked)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }


        }
    }
}