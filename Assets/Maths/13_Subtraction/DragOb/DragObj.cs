using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Maths.Substraction.DragingObject
{
    public class DragObj : MonoBehaviour
    {
        private bool clicked;
        public int no;
        public AudioSource sound;
        public AudioClip pickup, drop;
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
            //sound = GetComponent<AudioSource>();
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
                if (GameController.Instance.allobj[GameController.Instance.getno].bird)
                {
                    GameController.Instance.dustbin.GetComponent<SpriteRenderer>().sprite = GameController.Instance.closeDustBinSprite;
                   
                    transform.parent = GameController.Instance.dustbin.transform;
                    transform.DOMoveX(transform.position.x + 10,.25f);
                    transform.DOMoveY(transform.position.y + 10, .25f);
                    StartCoroutine(BirdMove());
                }
                else
                {
                    GameController.Instance.dustbin.GetComponent<SpriteRenderer>().sprite = GameController.Instance.closeDustBinSprite;
                    transform.parent = GameController.Instance.dustbin.transform;

                    gameObject.SetActive(false);
                    transform.position = lastpos;
                }
            }
            transform.position = lastpos;

        }
        IEnumerator BirdMove()
        {
            yield return new WaitForSeconds(.25f);
           

            transform.position = lastpos;
            gameObject.SetActive(false);

        }
        private void Update()
        {
            if (Input.GetMouseButton(0) && clicked)
            {
                if (GameController.Instance.Neartodestination(this.gameObject))
                {
                    GameController.Instance.dustbin.GetComponent<SpriteRenderer>().sprite = GameController.Instance.OpenDustbinSprite;
                }
                else
                {
                    GameController.Instance.dustbin.GetComponent<SpriteRenderer>().sprite = GameController.Instance.closeDustBinSprite;
                }
                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }


        }
    }
}