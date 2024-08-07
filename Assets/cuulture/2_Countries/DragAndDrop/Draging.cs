using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace culture.countries.draganddrop
{
    public class Draging : DragerForall
    {

        public GameController controller;


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
            lastpos = transform.position;
        }
        protected override void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay || !clicked)
                return;
            base.OnMouseUp();
            GameController.Instance.gamePlay = false;
            if(Border)
            Border.color = Color.white;
            if (GameController.Instance.Neartodestination(int.Parse(no)))
            {
                GetComponent<BoxCollider2D>().enabled = false;
                transform.position = GameController.Instance.droping_place[int.Parse(no)].transform.position;
                GameController.Instance.droping_place[int.Parse(no)].GetComponent<SpriteRenderer>().color = Color.black;
                GameController.Instance.CurrectAnswer();
            }
            else
            {
                for (int i = 0; i < GameController.Instance.droping_place.Length; i++)
                {
                    if (GameController.Instance.Neartodestination(i))
                    {
                        transform.position = GameController.Instance.droping_place[i].transform.position;
                        GameController.Instance.droping_place[i].GetComponent<SpriteRenderer>().color = Color.black;
                        controller.temp_int = i;
                        GameController.Instance.WrongAnswer();
                        return;
                    }
                }
                GameController.Instance.gamePlay = true;
                transform.position = lastpos;
            }
        }
    }
}