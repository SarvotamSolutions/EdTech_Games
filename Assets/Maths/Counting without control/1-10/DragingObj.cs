using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
namespace Maths.Number1to10
{
    public class DragingObj : MonoBehaviour
    {
        public int answer;
        public bool currectAnswer;
        public Transform parent;
        [SerializeField] private SpriteRenderer Background;
        private void Start()
        {
            
        }
        void CurrectAnswer()
        {
         
            Background.color = GameManager.Instance.currect;
            GameManager.Instance.arrow.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.currectArrow;
            GameManager.Instance.QuestionGameObject.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.CurrectAnswer;
            GameManager.Instance.CurrectanswerOBj.SetActive(true);
            StartCoroutine(LevelCompleted());
        }

        IEnumerator LevelCompleted()
        {
            yield return new WaitForSeconds(3f);
            GameManager.Instance.GameReset();
        }
        void WrongAnswer()
        {

            StartCoroutine(WrongAnserAnimation());
 
        }
      
        IEnumerator WrongAnserAnimation()
        {
            Background.color = GameManager.Instance.wrong;
            GameManager.Instance.arrow.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.wrongArrow;
            GameManager.Instance.QuestionGameObject.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.WrongAnswer;
            yield return new WaitForSeconds(.5f);
            GameManager.Instance.WrongAnswer_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            GameManager.Instance.WrongAnswer_animation.SetActive(false);
            Background.color = GameManager.Instance.noseclting;
            GameManager.Instance.arrow.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.IcompletArrow;
            GameManager.Instance.QuestionGameObject.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.IncompleteAnswer;
            GameManager.Instance.DropingParent.GetChild(0).GetComponent<DragingObj>().Background.color = GameManager.Instance.noseclting;
            GameManager.Instance.DropingParent.GetChild(0).transform.parent = GameManager.Instance.OptionParent;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            parent = collision.transform;
        }





        private bool clicked;
        public int no;
        private bool canChnagepos;
        private Vector3 lastpos;
        private void OnMouseDown()
        {
            //if (EventSystem.current.IsPointerOverGameObject()) return;
            clicked = true;
            Background.color = GameManager.Instance.incomlete;
            lastpos = transform.position;
        }

        private void OnMouseUp()
        {
            clicked = false;
            if (GameManager.Instance.Neartodestination(this.gameObject))
            {

                transform.parent = null;
                transform.position = GameManager.Instance.DropingParent.position;
                if (currectAnswer)
                {
                    CurrectAnswer();
                }
                else
                {
                    WrongAnswer();

                }
                parent = GameManager.Instance.DropingParent;
                transform.parent = parent;
            }
            else
            {
                transform.position = lastpos;
                Background.color = GameManager.Instance.noseclting;
                GameManager.Instance.QuestionGameObject.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.IncompleteAnswer;
                parent = GameManager.Instance.OptionParent;
            }
            GameManager.Instance.DropingParent.GetComponent<SpriteRenderer>().color = Color.white;
            //if (GameController.instance.Neartodestination(this.gameObject))
            //{
            //    transform.position = lastpos;
            //    gameObject.SetActive(false);
            //}


        }
        private void Update()
        {
         
            if (Input.GetMouseButton(0) && clicked)
            {
                //if (GameManager.Instance.Neartodestination(this.gameObject))
                //{
                //    GameManager.Instance.DropingParent.GetComponent<SpriteRenderer>().color = Color.black;
                //}
                //else
                //{
                //    GameManager.Instance.DropingParent.GetComponent<SpriteRenderer>().color = Color.white;
                //}
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }


        }
    }
}