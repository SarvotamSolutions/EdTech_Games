using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace culture.sinkfloat1
{
    public class Draging : DragerForall
    {
        public GameController controller;
        public override void Start()
        {
            base.Start();
            lastpos = transform.position;
        }
        protected override void OnMouseUp()
        {
            base.OnMouseUp();

            if (GameController.Instance.NeartodestinationsamePos())
            {
                transform.GetComponent<Collider2D>().enabled = false;
                controller.reseting++;
                if(no == "sink")
                {

                    transform.DOMoveY(controller.firstdroptext.transform.position.y, 1);
                    
                }
                else
                {
                    transform.DOMoveY(controller.seconddroptext.transform.position.y, 1);

                    StartCoroutine(floatanimation(1, false));
                }
                if (controller.reseting >= 4)
                {
                    controller.reseting = 0;
                    GameController.Instance.reloding++;
                   // StopAllCoroutines();
                    controller.CurrectAnswer();
                }
            }
            else
            {
                transform.position = lastpos;
            }
        }
        IEnumerator floatanimation(float time,bool down)
        {
            yield return new WaitForSeconds(time);
            if (down)
            {
                transform.DOMoveY(transform.position.y + .1f, .5f);
                down = false;
                StartCoroutine(floatanimation(.5f, down));
            }
            else
            {
                transform.DOMoveY(transform.position.y - .1f, .5f);
                down = true;
                StartCoroutine(floatanimation(.5f, down));
            }
        }
        protected override void OnMouseDown()
        {
            base.OnMouseDown();
        }


    }
}