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
            if (!GameCondtroller.Instance.gamePlay)
                return;
            clicked = true;
           
        }
        public bool droped;
        public float speed;
        private void OnMouseUp()
        {
            if (!GameCondtroller.Instance.gamePlay)
                return;
            clicked = false;
            if (GameCondtroller.Instance.Neartodestination2(this.gameObject))
            {
                GameCondtroller.Instance.gamePlay = false;
                if (no == GameCondtroller.Instance.Total)
                {
                    
                    GetComponent<SpriteRenderer>().sprite = GameCondtroller.Instance.currectAnswer;
                    StartCoroutine(Wairrelod());
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = GameCondtroller.Instance.WrongAnswer;
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
            GameCondtroller.Instance.Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            transform.position = lastpos;
            GameCondtroller.Instance.Party_pop.SetActive(false);
            GameCondtroller.Instance.ResetingGame();
        } 
        IEnumerator WrongAnimtaion()
        {
            GameCondtroller.Instance.wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            transform.position = lastpos;
            GetComponent<SpriteRenderer>().sprite = GameCondtroller.Instance.normalAnswer;
            GameCondtroller.Instance.wrongAnswer_animtion.SetActive(false);
            GameCondtroller.Instance.gamePlay = true;
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