using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Laguage.sightWords
{
    public class Drag : DragerForall
    {
        public override void Start()
        {
            transform.SetSiblingIndex(Random.Range(0, transform.parent.childCount));
            base.Start();

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

            if (!GameController.Instance.Neartodestination())
            {
                transform.position = lastpos;
            }
        }
    }
}