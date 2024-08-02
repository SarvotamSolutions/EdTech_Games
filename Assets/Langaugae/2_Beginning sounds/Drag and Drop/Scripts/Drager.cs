using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Laguage.beginning_sounds.DragandDrop
{
    public class Drager : DragerForall
    {
        public override void Start()
        {
            base.Start();
        }

        protected override void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay)
                return;
            base.OnMouseDown();
        }
        protected override void OnMouseUp()
        {

            if (!GameController.Instance.gamePlay || !clicked )
                return;
           
            base.OnMouseUp();
           // transform.position = lastpos;
            if(GameController.Instance.Neartodestination())
            {
                sound.PlayOneShot(drop);

            }
            else
            {
                //sound.PlayOneShot(drop);
                Border.color = Color.white;
                transform.position = lastpos;
            }
        }
     
    }
}