using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace culture.Magnetinc
{
    public class Drag : DragerForall
    {
        public GameObject[] points;
        public SpriteRenderer spritre;
        public Color defaltcolor;

        public override void Start()
        {
            base.Start();
            StartCoroutine(flipanimtion());
        }
        protected override void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay || !clicked)
                return;
            base.OnMouseUp();

            if (GameController.Instance.Neartodestination())
            {
                Border.color = GameController.Instance.currect_answer_color;
                StopAllCoroutines();
                transform.GetComponent<BoxCollider2D>().enabled = false;
                if (no == "float")
                {
                    StartCoroutine(AnimtionPlayer(2));
                }
                else
                {
                    StartCoroutine(AnimtionPlayer(1));
                }
            }
            else
            {
                Border.color = defaltcolor;
                transform.localScale = new Vector3(2, 2, 2);
                transform.position = lastpos;
            }
        }

        IEnumerator flipanimtion()
        {
            spritre.DOFade(0, .5f);
            yield return new WaitForSeconds(.5f);
            spritre.DOFade(1, .5f);
            yield return new WaitForSeconds(.5f);
            StartCoroutine(flipanimtion());
        }
        IEnumerator AnimtionPlayer(int no)
        {
            GameController.Instance.gamePlay = false;

            transform.DOMove(points[no].transform.position, .5f);

            yield return new WaitForSeconds(.5f);

            transform.parent = GameController.Instance.droping_place[no].transform;

            transform.SetSiblingIndex(GameController.Instance.droping_place[no].transform.childCount - 2);

            GameController.Instance.reloding++;

            if (GameController.Instance.reloding == GameController.Instance.alloption.Length)
            {
                GameController.Instance.CurrectAnswer();

            }
            else
                GameController.Instance.alloption[GameController.Instance.reloding].gameObject.SetActive(true);
            GameController.Instance.gamePlay = true;
        }
        protected override void OnMouseDown()
        {
            if (!GameController.Instance.gamePlay)
                return;
            base.OnMouseDown();
            Border.color = GameController.Instance.sellect_answer_color;
            lastpos = transform.position;
            transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }
    }
}