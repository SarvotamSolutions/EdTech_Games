using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Maths.BeadStair.NumberSelction
{
    public class Drager : MonoBehaviour
    {
        private AudioSource sound;
        public AudioClip pickup, drop;
        private bool clicked;
        public int no;
        private bool canChnagepos;
        public Vector3 lastpos;

        private void Start()
        {
            sound = GetComponent<AudioSource>();
            lastpos = transform.position;
            if(!GameController.Instance.spritechnagr)
            Noset();
        }
        void Noset()
        {
            no = Random.Range(1, 11);
            foreach (var item in GameController.Instance.allno)
            {
                if(item == no)
                {
                    Noset();
                    return;
                }
            }
            GameController.Instance.allno.Add(no);
            transform.GetComponentInChildren<TextMeshPro>().text = no.ToString();
        }
        private void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay|| GameController.Instance.totorialcheck.totorialplaying)
                return;
            sound.clip = pickup;
            sound.Play();
            GameController.Instance.Selected = this;
            clicked = true;
            
        }

        private void OnMouseUp()
        {
            clicked = false;
            if (!GameController.Instance.gamePlay || GameController.Instance.totorialcheck.totorialplaying)
                return;
            sound.clip = drop;
            sound.Play();

            if (GameController.Instance.NeartoDestination())
            {

                if (!GameController.Instance.spritechnagr)
                    transform.position = lastpos;
              
                  
             //   gameObject.SetActive(false);
            }
            else
            transform.position = lastpos;
           // GameController.Instance.Selected = null;
        }
        private void Update()
        {
            //if (GameController.Instance.Completed)
            //    return;
            if (Input.GetMouseButton(0) && clicked)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }


        }
    }
}