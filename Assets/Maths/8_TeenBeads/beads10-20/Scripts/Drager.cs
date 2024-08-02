using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.TeenBeads.drop10to20
{
    public class Drager : MonoBehaviour
    {
        private AudioSource sound;
        public AudioClip pickup, drop;
        private bool clicked;
        public int no;
        private bool canChnagepos;
        public Vector3 lastpos;
        bool firsttime = false;

        private void Start()
        {
            sound = GetComponent<AudioSource>();
            firsttime = true;
            //   lastpos = transform.position;
            transform.parent.SetSiblingIndex(Random.Range(0, transform.parent.parent.childCount));
            
        }
        private void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay || GameController.Instance.totorialcheck.totorialplaying)
                return;
            sound.clip = pickup;
            sound.Play();
            if (firsttime)
            lastpos = transform.position;
            clicked = true;
            firsttime = false;
            
        }

        private void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay || GameController.Instance.totorialcheck.totorialplaying || clicked == false)
                return;
            sound.clip = drop;
            sound.Play();
            clicked = false;
            GameObject obj = GameController.Instance.Neartodestination(this.gameObject);

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