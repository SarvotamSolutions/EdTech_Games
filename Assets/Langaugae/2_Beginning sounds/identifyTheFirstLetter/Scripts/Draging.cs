using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Laguage.beginning_sounds.Idintfyletter
{
    public class Draging : DragerForall
    {
        protected override void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay)
                return;
            foreach (var item in GameController.Instance.droping_place)
            {
                item.color = GameController.Instance.sellect_answer_color;
            }
            base.OnMouseDown();
        }

        protected override void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay || !clicked)
                return;
            base.OnMouseUp();
            if (GameControllerforAll.Instance.Neartodestination())
            {
                letterSoundClip.PlayOneShot(GameController.Instance.lettersound);
            }
            else
            {
                foreach (var item in GameController.Instance.droping_place)
                {
                    item.color = Color.white;
                }
                transform.position = lastpos;
            }
        }
    }
}