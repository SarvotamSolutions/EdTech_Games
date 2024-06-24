using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace culture.sinkfloat1
{

    public class Drag : DragerForall
    {
        public GameController controler;

        public Color defaltcolr;
        protected override void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay)
                return;
            base.OnMouseDown();
            lastpos = transform.position;
            Border.color = GameController.Instance.sellect_answer_color;
        }
        protected override void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay || !clicked)
                return;
            base.OnMouseUp();
            if (GameController.Instance.Neartodestination(0))
            {
                GameController.Instance.gamePlay = false;
                transform.parent = GameController.Instance.droping_place[0].transform;
                transform.position = GameController.Instance.droping_place[0].transform.position;

                if(no == "sink")
                {
                    controler.firstdroptext.SetActive(false);
                    GameController.Instance.CurrectAnswer();
                }
                else
                {
                    GameController.Instance.WrongAnswer();
                }
            }
            else if (GameController.Instance.Neartodestination(1))
            {
                GameController.Instance.gamePlay = false;
                transform.parent = GameController.Instance.droping_place[1].transform;
                transform.position = GameController.Instance.droping_place[1].transform.position;

                if (no == "float")
                {
                    controler.seconddroptext.SetActive(false);
                    GameController.Instance.CurrectAnswer();
                }
                else
                {
                    GameController.Instance.WrongAnswer();
                }
            }
            else
            {
                transform.position = lastpos;
                Border.color = defaltcolr;
                GameController.Instance.selectedoption = null;
            }



        }

      
    }
}
