using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maths.Addision.AddisitonwithColors
{
    public class BeadsSelecting : MonoBehaviour
    {
        public AllCollors color;
        public bool clicked;
        private void OnMouseUpAsButton()
        {
            if (clicked)
                return;
            if(GameCondtroller.Instance.selectedcollor == color)
            {
                clicked = true;

                float ypos = (float)GameCondtroller.Instance.number1 / 2f;
                GameCondtroller.Instance.Particalblast.gameObject.transform.position = transform.position;
                Vector3 pos = new Vector3(0, ypos, 0);
                var ph = GameCondtroller.Instance.Particalblast.shape;
                ph.position = pos;
                ph.scale = new Vector3(1, GameCondtroller.Instance.number1, 1);
                GameCondtroller.Instance.Particalblast.Play();
                GetComponent<SpriteRenderer>().sprite = GameCondtroller.Instance.allBeadsWithColors[(int)color];
                GameCondtroller.Instance.stage1Colored++;
                if (GameCondtroller.Instance.stage1Colored >= 2)
                {
                    GameCondtroller.Instance.SetStage2();
                }
            }
        }

    }
}