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
            //GameController.Instance.droping_place.sortingOrder = 2;
            base.OnMouseUp();
            foreach (var item in GameController.Instance.droping_place)
            {
                item.color = Color.white;
            }
            //   GameController.Instance.droping_place.color =Color.white;

            clicked = false;
            if (GameController.Instance.Neartodestination())
            {
                GameController.Instance.gamePlay = false;
                GameController.Instance.GetComponent<AudioSource>().clip = GameController.Instance.lettersound;
                GameController.Instance.GetComponent<AudioSource>().PlayDelayed(.3f);
            //    transform.position = GameController.Instance.droping_place[0].transform.GetChild(rotionno + 1).transform.position;
                transform.parent = GameController.Instance.droping_place[0].transform;
               // transform.localScale = new Vector3(.9f, .9f, .9f);
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
                // gameObject.SetActive(false);
            }
            else
            {
                Border.color = Icon_Colors;
                // transform.GetComponent<SpriteRenderer>().sprite = controler.alldirectionanswer[rotionno];
                //  transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = controler.border[rotionno];
                transform.position = lastpos;
            }
        
        }
    }
}