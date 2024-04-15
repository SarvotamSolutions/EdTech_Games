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
            if(GameCondtroller.instace.selectedcollor == color)
            {
                clicked = true;

                float ypos = (float)GameCondtroller.instace.number1 / 2f;
                GameCondtroller.instace.Particalblast.gameObject.transform.position = transform.position;
                Vector3 pos = new Vector3(0, ypos, 0);
                var ph = GameCondtroller.instace.Particalblast.shape;
                ph.position = pos;
                ph.scale = new Vector3(1, GameCondtroller.instace.number1, 1);
                GameCondtroller.instace.Particalblast.Play();
                GetComponent<SpriteRenderer>().sprite = GameCondtroller.instace.allBeadsWithColors[(int)color];
                GameCondtroller.instace.stage1Colored++;
                if (GameCondtroller.instace.stage1Colored >= 2)
                {
                    GameCondtroller.instace.SetStage2();
                }
            }
        }

    }
}