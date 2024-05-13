using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Maths.Substraction.Caluculation
{

    public class Draging : MonoBehaviour
    {
        private bool clicked;
        public int no;
        private bool canChnagepos;
        private Vector3 lastpos;

        private void Start()
        {
            lastpos = transform.position;
        }
        private void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay)
                return;
            clicked = true;
           // lastpos = transform.position;
        }
        public void Textchange()
        {
            transform.GetChild(0).GetComponent<TextMeshPro>().text = no.ToString();
        }

        private void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay)
                return;
            clicked = false;
            if (GameController.Instance.Neartodestination(this.gameObject))
            {
                transform.position = lastpos;
                Debug.Log(GameController.Instance.no);
                if (GameController.Instance.no < 3)
                {
                    Debug.Log(GameController.Instance.number[GameController.Instance.no-1]);
                    if (no-1 == GameController.Instance.number[GameController.Instance.no-1])
                    {
                        this.gameObject.SetActive(false);
                    }
                    
                     transform.position = lastpos;
                  
                }
                else
                {
                    Debug.Log(GameController.Instance.number[GameController.Instance.no - 1]);
                    if (no == GameController.Instance.number[GameController.Instance.no])
                    {
                        this.gameObject.SetActive(false);
                    }
                    {
                        transform.position = lastpos;
                    }
                }
               

            
            }
            else
            {

                transform.position = lastpos;
            }

        }
        private void Update()
        {
            if (Input.GetMouseButton(0) && clicked)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }


        }
    }
}