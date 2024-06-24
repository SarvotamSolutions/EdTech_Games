using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Laguage.Reading_sentences
{
    public class Drag : DragerForall
    {
        public Sprite selct;
        public override void Start()
        {
          
            base.Start();
            transform.SetSiblingIndex(Random.Range(0, transform.parent.childCount));

        }

        protected override void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay)
                return;
            base.OnMouseDown();
            background.sprite = selct;
            GameController.Instance.Boarder.color = GameController.Instance.sellect_answer_color;
            lastpos = transform.position;

        }


        protected override void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay || !clicked)
                return;
            base.OnMouseUp();
            GameController.Instance.Boarder.color = Color.white;
            if (GameController.Instance.Neartodestination())
            {

            }
            else
            {
                
                transform.position = lastpos;
            }
        }
    }
}