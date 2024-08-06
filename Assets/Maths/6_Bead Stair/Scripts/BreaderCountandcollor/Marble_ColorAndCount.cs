using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Maths.BeadStair.ColorandCount
{
    public class Marble_ColorAndCount : MonoBehaviour
    {
        public Sprite ColorMarble;
        public AllCollors thiscolor;
        public Sprite None, Selecting;
        public int activatelevel;
        private SpriteRenderer rendring;
        public Color colors;
        private bool selcted;
        private bool completed;
        private void Start()
        {
            rendring = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (GameController_ColorAndCount.instace.level != activatelevel)
                return;
            if (!selcted)
            {
                selcted = true;
                rendring.sprite = Selecting;
            }
        }
        private void OnMouseUpAsButton()
        {
            if (completed)
                return;
            if (thiscolor == GameController_ColorAndCount.instace.selectedcollor && thiscolor == GameController_ColorAndCount.instace.allmarble[GameController_ColorAndCount.instace.level].thiscolor)
            {
                completed = true;
                rendring.sprite = ColorMarble;
                GameController_ColorAndCount.instace.selectedcollorDone = GameController_ColorAndCount.instace.selectedcollor;
            }
        }
    }
}