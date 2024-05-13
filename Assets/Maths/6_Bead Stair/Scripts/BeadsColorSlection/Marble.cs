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
            if (GameController.Instance.level != activatelevel)
                return;
            if (!selcted)
            {
                selcted = true;
                rendring.sprite = Selecting;
            }


        }
        private void OnMouseUpAsButton()
        {
            if (completed || !GameController.Instance.gamePlay)
                return;
            if (thiscolor == GameController.Instance.selectedcollor && thiscolor == GameController.Instance.allmarble[GameController.Instance.level].thiscolor)
            {
                Debug.Log("XXx");
                completed = true;
                rendring.sprite = ColorMarble;
                GameController.Instance.level++;
                if (GameController.Instance.allmarble.Length > GameController.Instance.level)
                {
                    Debug.Log(GameController.Instance.level);
                    GameController.Instance.Hinttext.color = GameController.Instance.allmarble[GameController.Instance.level].color;
                  // Hinttext.text = "<color=white>Color the " + (GameController.instace.level + 1) + " bead " + "</color>" + allmarble[level].thiscolor;
                    GameController.Instance.Hinttext.text = "<color=white>Color the " + (GameController.Instance.level + 1) + " bead</color> " + GameController.Instance.allmarble[GameController.Instance.level].thiscolor;
                }
                else
                    StartCoroutine(LevelComplted());
            }
            else
            {
                StartCoroutine(WrongAnswerAnimation());
            }


        }
        IEnumerator WrongAnswerAnimation()
        {
            GameController.Instance.gamePlay = false;
            //   DrawCanvas.SetActive(false);

            GameController.Instance.wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            GameController.Instance.wrongAnswer_animtion.SetActive(false);
            GameController.Instance.gamePlay = true;
        }
        IEnumerator LevelComplted()
        {
            GameController.Instance.gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }
    }
}