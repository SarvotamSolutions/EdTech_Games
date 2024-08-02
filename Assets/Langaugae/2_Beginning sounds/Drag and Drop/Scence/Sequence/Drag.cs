using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laguage.Sequence
{
    public class Drag : DragerForall
    {
        public Color NormalText;
        public override void Start()
        {
            base.Start();

            transform.SetSiblingIndex(Random.Range(0, transform.parent.childCount));
        }
        protected override void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay)
                return;
            lastpos = transform.position;
            base.OnMouseDown();
            Border.color = GameController.Instance.sellect_answer_color;
        }
        protected override void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay || !clicked)
                return;
            base.OnMouseUp();

            if (GameController.Instance.Neartodestination())
            {

                GameController.Instance.gamePlay = false;
                if (no == GameController.Instance.reloding.ToString())
                {
                    Border.color = GameController.Instance.currect_answer_color;
                    text.color = Color.white;
                    GameController.Ins.DroppingPlaceBG.sprite = GameController.Ins.trueDroppingPlace;
                    GameController.Instance.reloding++;
                    GameController.Instance.CurrectAnswer();
                }
                else
                {
                    Border.color = GameController.Instance.wrong_answer_color;
                    text.color = Color.white;
                    GameController.Ins.DroppingPlaceBG.sprite = GameController.Ins.falseDroppingPlace;

                    GameController.Instance.WrongAnswer();
                }
                
            }
            else
            {
                text.color = NormalText;
                Border.color = Color.white;
                transform.position = lastpos;
            }
           
        }
    }
}