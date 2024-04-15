using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Maths.BeadStair.ColorSlection
{
    public class Marble : MonoBehaviour
    {
        public Sprite ColorMarble;
        public AllCollors thiscolor;
        public Sprite None, Selecting;
        public int activatelevel;
        private SpriteRenderer rendring;
        public Color color;
        private bool selcted;
        private bool completed;
        private void Start()
        {
            rendring = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (GameController.instace.level != activatelevel)
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
            if (thiscolor == GameController.instace.selectedcollor && thiscolor == GameController.instace.allmarble[GameController.instace.level].thiscolor)
            {
                Debug.Log("XXx");
                completed = true;
                rendring.sprite = ColorMarble;
                GameController.instace.level++;
                if (GameController.instace.allmarble.Length > GameController.instace.level)
                {
                    Debug.Log(GameController.instace.level);
                    GameController.instace.Hinttext.color = GameController.instace.allmarble[GameController.instace.level].color;
                  // Hinttext.text = "<color=white>Color the " + (GameController.instace.level + 1) + " bead " + "</color>" + allmarble[level].thiscolor;
                    GameController.instace.Hinttext.text = "<color=white>Color the " + (GameController.instace.level + 1) + " bead</color> " + GameController.instace.allmarble[GameController.instace.level].thiscolor;
                }
                else
                    StartCoroutine(LevelComplted());
            }


        }
        IEnumerator LevelComplted()
        {
            GameController.instace.gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }
    }
}