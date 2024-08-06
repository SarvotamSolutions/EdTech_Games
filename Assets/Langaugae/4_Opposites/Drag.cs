using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Laguage.Oposite.draganddrop
{
    public class Drag : DragerForall
    {
        public Color Icon_Colors;

        protected override void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay)
                return;
            base.OnMouseDown();
        }
        protected override void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay)
                return;

            base.OnMouseUp();
            foreach (var item in GameController.Instance.droping_place)
            {
                item.color = Color.white;
            }

            clicked = false;
            if (GameController.Instance.Neartodestination())
            {
                GameController.Instance.gamePlay = false;
                GameController.Instance.GetComponent<AudioSource>().clip = GameController.Instance.lettersound;
                GameController.Instance.GetComponent<AudioSource>().PlayDelayed(.3f);

                transform.parent = GameController.Instance.droping_place[0].transform;

                if (no == GameController.Instance.letter)
                {
                    Border.color = Icon_Colors;
                    GameController.Instance.CurrectAnswer();
                }
                else
                {
                    Border.color = Icon_Colors;
                    Debug.Log("bORDER");

                    GameController.Instance.WrongAnswer();
                }
            }
            else
            {
                Border.color = Icon_Colors;
                transform.position = lastpos;
            }

        }
    }
}