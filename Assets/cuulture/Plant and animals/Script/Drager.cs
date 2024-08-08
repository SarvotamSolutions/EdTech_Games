using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace culture.PlantsandAnimal.DragandDrop
{

    public class Drager : DragerForall
    {
        public int dropid;
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

        }

        protected override void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay || !clicked)
                return;
            base.OnMouseUp();
            if (GameController.Instance.Neartodestination())
            {

            }
        }
    }
}