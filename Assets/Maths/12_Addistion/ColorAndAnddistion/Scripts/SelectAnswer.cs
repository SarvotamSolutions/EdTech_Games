using Maths.Substraction.DragingObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.Addision.AddisitonwithColors
{
    public class SelectAnswer : MonoBehaviour
    {
        public int no;

        public AudioSource sound;
        public AudioClip pickup, drop;
        private bool clicked;
        public float Moneyvlaue;
        private bool canChnagepos;
        public Vector3 lastpos;
        public bool droped;
        public float speed;

        private void Start()
        {
            lastpos = transform.position;
        }
        private void OnMouseDown()
        {
            if (!GameCondtroller.Instance.gamePlay)
                return;
            sound.clip = pickup;
            sound.Play();
            clicked = true;
        }
        private void OnMouseUp()
        {
            if (!GameCondtroller.Instance.gamePlay)
                return;
            sound.clip = drop;
            sound.Play();
            clicked = false;
            if (GameCondtroller.Instance.Neartodestination2(this.gameObject))
            {
                GameCondtroller.Instance.currentanswertext.text = no.ToString();
                GameCondtroller.Instance.gamePlay = false;
                if (no == GameCondtroller.Instance.Total)
                {
                    GameCondtroller.Instance.currectanswerimage.sprite = GameCondtroller.Instance.currectAnswer;
                    GameCondtroller.Instance.StartCoroutine(GameCondtroller.Instance.Wairrelod(this.gameObject));
                }
                else
                {
                    GameCondtroller.Instance.currectanswerimage.sprite = GameCondtroller.Instance.WrongAnswer;
                    GameCondtroller.Instance.StartCoroutine(GameCondtroller.Instance.WrongAnimtaion(this.gameObject));
                }
                droped = true;
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