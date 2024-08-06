using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Maths.NumberRoads
{
    public class TrailFiller : MonoBehaviour
    {
        public TrailRenderer trailRenderer;
        public float fillThreshold = 0.95f; // Adjust this threshold according to your requirement
        public bool isFilled = false;
        public bool red;

        private float GetTrailLength()
        {
            if (trailRenderer == null)
                return 0f;

            return trailRenderer.time * CalculateTrailSpeed();
        }

        private float CalculateTrailSpeed()
        {
            if (trailRenderer == null || trailRenderer.time <= 0f)
                return 0f;

            return trailRenderer.positionCount / trailRenderer.time;
        }

        private float CalculateBoxPerimeter()
        {
            return 4 * transform.localScale.x;
        }
        public Sprite filld;
        private void OnMouseDown()
        {

        }
        bool dragonetimecall;
        private void OnMouseDrag()
        {
            if (GameController.instace.totorialcheck.totorialplaying || !GameController.instace.gameplay)
                return;
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            float trailLength = GetTrailLength();
            if (GameController.instace.red)//checking the colling road is red
            {
                trailRenderer.colorGradient = GameController.instace.currectcolor;
                trailRenderer.transform.position = pos;
                if (!isFilled && trailLength >= CalculateBoxPerimeter() * fillThreshold)
                {

                    isFilled = true;
                    trailRenderer.gameObject.SetActive(false);
                    GetComponent<SpriteRenderer>().sprite = GameController.instace.redwin;
                    GetComponent<Collider2D>().enabled = false;
                    // You can trigger any other action here when the box is filled
                }
            }
            else// the coloring road is bluue
            {
                trailRenderer.colorGradient = GameController.instace.Wrongcolor;
                trailRenderer.transform.position = pos;
                if (!isFilled && trailLength >= CalculateBoxPerimeter() * fillThreshold)
                {
                    isFilled = true;
                    trailRenderer.gameObject.SetActive(false);
                    GetComponent<SpriteRenderer>().sprite = GameController.instace.bluwin;
                    GetComponent<Collider2D>().enabled = false;
                    // You can trigger any other action here when the box is filled
                }
            }

            if (GameController.instace.red != red && isFilled)
            {
                StartCoroutine(GameController.instace.WrongAnswerAnimation(this));
            }
            else if (GameController.instace.red == red && isFilled && !dragonetimecall)// cuurrect ANSWER
            {
                dragonetimecall = true;
                GameController.instace.filledno++;

                if (GameController.instace.all_color_holder[GameController.instace.no - 1].transform.childCount == GameController.instace.filledno)
                {

                    if (GameController.instace.no <= 9)
                    {
                        if (GameController.instace.Draw_canvas)
                        {
                            GameController.instace.Draw_canvas.gameObject.SetActive(true);
                            if (GameController.instace.totorialcheck.directionno < GameController.instace.totorialcheck.directionpanel.Length)
                            {
                                GameController.instace.totorialcheck.directionWindow();
                            }
                            GameController.instace.filledno = 0;
                            for (int i = GameController.instace.no; i < GameController.instace.allnumber.Length; i++)
                            {
                                GameController.instace.allnumber[i].transform.parent.GetComponent<SpriteRenderer>().color = GameController.instace.notselect;
                            }


                            GameController.instace.allnumber[GameController.instace.no - 1].transform.parent.GetComponent<SpriteRenderer>().color = GameController.instace.Selct;

                        }
                        else
                        {
                            GameController.instace.filledno = 0;
                            GameController.instace.ChangetoColorfiller();

                        }
                    }
                    else
                    {
                        if (GameController.instace.onlycolor)
                            StartCoroutine(GameController.instace.LevelCompleted());
                        else
                        {
                            GameController.instace.Draw_canvas.gameObject.SetActive(true);
                            GameController.instace.filledno = 0;
                            GameController.instace.allnumber[GameController.instace.no - 1].transform.parent.GetComponent<SpriteRenderer>().color = GameController.instace.Selct;
                        }
                    }
                }
            }
        }
    }
}