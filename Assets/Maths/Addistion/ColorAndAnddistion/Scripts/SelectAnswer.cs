using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.Addision.AddisitonwithColors
{
    public class SelectAnswer : MonoBehaviour
    {
        public int no;
        private bool clicked;
        public float Moneyvlaue;
        private bool canChnagepos;
        private Vector3 lastpos;

        private void Start()
        {
            lastpos = transform.position;
        }
        private void OnMouseDown()
        {
            clicked = true;
           
        }
        public bool droped;
        public float speed;
        private void OnMouseUp()
        {
            clicked = false;
            if (GameCondtroller.instace.Neartodestination2(this.gameObject))
            {
                if (no == GameCondtroller.instace.Total)
                {
                    
                    GetComponent<SpriteRenderer>().sprite = GameCondtroller.instace.currectAnswer;
                    StartCoroutine(Wairrelod());
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = GameCondtroller.instace.WrongAnswer;
                    StartCoroutine(WrongAnimtaion());
                }
                //   GameCondtroller.instace.totalmoneyadded += Moneyvlaue;
                //  GameCondtroller.instace.totalMoneyaddedtext.text = Gamecontroll.instace.totalmoneyadded.ToString("0.00") + " $";
                droped = true;
                //  transform.position += Vector3.down *19;
            }
            //this.gameObject.SetActive(false);

            //if (GameCondtroller.instace.checkForThirdStage())
            //{

            //}

            else
            {

                transform.position = lastpos;
            }

        }

        public void OnMouseUpAsButton()
        {
           


        }

        IEnumerator Wairrelod()
        {
            GameCondtroller.instace.Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            transform.position = lastpos;
            GameCondtroller.instace.Party_pop.SetActive(false);
            GameCondtroller.instace.ResetingGame();
        } 
        IEnumerator WrongAnimtaion()
        {
            GameCondtroller.instace.wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            transform.position = lastpos;
            GetComponent<SpriteRenderer>().sprite = GameCondtroller.instace.normalAnswer;
            GameCondtroller.instace.wrongAnswer_animtion.SetActive(false);
        }
        // Start is called before the first frame update
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