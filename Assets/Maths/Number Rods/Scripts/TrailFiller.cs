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
 
        private void Update()
        {
        
        }

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
            // Assuming your square box has equal sides, perimeter = 4 * sideLength
            // You might need to adjust this according to how you create your square box
            // You can also consider using collider bounds if available.
            return 4 * transform.localScale.x;
        }
        public Sprite filld;
        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Debug.Log("Down");
        }
        bool dragonetimecall;
        private void OnMouseDrag()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
           
            float trailLength = GetTrailLength();
            if (GameController.instace.red)
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
            else
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

            if(GameController.instace.red != red && isFilled)
            {
                StartCoroutine(GameController.instace.WrongAnswerAnimation(this));
            }
            else if (GameController.instace.red == red && isFilled && !dragonetimecall)
            {
                Debug.Log("CCC");
                dragonetimecall = true;
                GameController.instace.filledno++;

                Debug.Log(GameController.instace.no);
                if(GameController.instace.all_color_holder[GameController.instace.no-1].transform.childCount == GameController.instace.filledno)
                {

                    if (GameController.instace.no <= 9)
                    {
                        if (GameController.instace.Draw_canvas)
                        {
                            GameController.instace.Draw_canvas.gameObject.SetActive(true);
                          //  GameController.instace.ai.textResult = GameController.instace.allnumber[GameController.instace.no - 1];
                            //GameController.instace.Draw_canvas.interactable = true;
                            GameController.instace.filledno = 0;
                            foreach (var item in GameController.instace.allnumber)
                            {
                                item.transform.parent.GetComponent<SpriteRenderer>().sprite = GameController.instace.inputNot_selected;
                            }
                            GameController.instace.allnumber[GameController.instace.no-1].transform.parent.GetComponent<SpriteRenderer>().sprite = GameController.instace.inpuselected;

                        }
                        else
                        {
                            Debug.Log("filling");
                            GameController.instace.filledno = 0;
                            GameController.instace.ChangetoColorfiller();

                        }
                    }
                    else
                    {
                        StartCoroutine(GameController.instace.LevelCompleted());
                    }
                }
            }
   
         
        }
    }
}