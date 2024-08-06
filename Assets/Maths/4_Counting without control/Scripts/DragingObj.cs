using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
namespace Maths.Number1to10
{
    public class DragingObj : MonoBehaviour
    {
        private AudioSource sound;
        public AudioClip pickup, drop;
        public int answer;
        public bool currectAnswer;
        public Transform parent;
        [SerializeField] private SpriteRenderer Background;
        private void Start()
        {
            sound = GetComponent<AudioSource>();
            lastpos = transform.position;
        }
        void CurrectAnswer()
        {
            GameManager.Instance.gamePlay = false;
            Background.sprite = GameManager.Instance.CurrectAnswer;
            GameManager.Instance.CurrectanswerOBj.SetActive(true);
            StartCoroutine(LevelCompleted());
        }

        IEnumerator LevelCompleted()
        {
            yield return new WaitForSeconds(GameManager.Instance.currectanswerInterverl);
            Background.sprite = GameManager.Instance.IncompleteAnswer;
            transform.position = lastpos;
            Background.color = GameManager.Instance.noseclting;
            GameManager.Instance.arrow.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.IcompletArrow;
            GameManager.Instance.DropingParent.GetChild(0).GetComponent<DragingObj>().Background.color = GameManager.Instance.noseclting;
            GameManager.Instance.DropingParent.GetChild(0).transform.parent = GameManager.Instance.OptionParent;
            GameManager.Instance.GameReset();
        }
        void WrongAnswer()
        {
            StartCoroutine(WrongAnserAnimation());
        }
      
        IEnumerator WrongAnserAnimation()
        {
            GameManager.Instance.gamePlay = false;
            Background.sprite = GameManager.Instance.WrongAnswer;
            GameManager.Instance.arrow.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.wrongArrow;
            yield return new WaitForSeconds(.5f);
            GameManager.Instance.WrongAnswer_animation.SetActive(true);
            yield return new WaitForSeconds(GameManager.Instance.wronganswerInterval);
            transform.position = lastpos;
            GameManager.Instance.gamePlay = true;
            GameManager.Instance.WrongAnswer_animation.SetActive(false);
            Background.color = GameManager.Instance.noseclting;
            GameManager.Instance.arrow.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.IcompletArrow;
            Background.sprite = GameManager.Instance.IncompleteAnswer;
            GameManager.Instance.DropingParent.GetChild(0).GetComponent<DragingObj>().Background.color = GameManager.Instance.noseclting;
            GameManager.Instance.DropingParent.GetChild(0).transform.parent = GameManager.Instance.OptionParent;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            parent = collision.transform;
        }

        public void OrderSetting()
        {
            Background.sortingOrder = 4;
            GetComponent<SpriteRenderer>().sortingOrder = 5;
        }


        private bool clicked;
        public int no;
        private bool canChnagepos;
        private Vector3 lastpos;
        private void OnMouseDown()
        {
            if (!GameManager.Instance.gamePlay || GameManager.Instance.totorialcheck.totorialplaying )
                return;
            lastpos = transform.position;
            sound.clip = pickup;
            sound.Play();
            OrderSetting();
            clicked = true;
            Background.color = GameManager.Instance.incomlete;
        }

        private void OnMouseUp()
        {
            if (!GameManager.Instance.gamePlay || GameManager.Instance.totorialcheck.totorialplaying || !clicked)
                return;
            sound.clip = drop;
            sound.Play();
            Background.sortingOrder = 3;
            GetComponent<SpriteRenderer>().sortingOrder = 4;
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
                parent = GameManager.Instance.OptionParent;
            }
        }
        private void Update()
        {
            if (!GameManager.Instance.gamePlay)
                return;
            if (Input.GetMouseButton(0) && clicked)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }
        }
    }
}