using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maths.Number1to10.numberWithObject
{
    public class ObjectDraging : MonoBehaviour
    {
        public AudioSource dropaudio;
        public GameObject dropingobj;
        public int no;
        public SpriteRenderer Normalsprite;
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                GameManager.Instance.Question.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.ClickedSprite[GameManager.Instance.IconspriteID];
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }

            if (Input.GetMouseButtonUp(0) && !GameManager.Instance.WithControll)
            {
                GameManager.Instance.Question.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.unclickedsprite[GameManager.Instance.IconspriteID];
                float distancex = Mathf.Abs(transform.position.x - dropingobj.transform.position.x);
                float distancey = Mathf.Abs(transform.position.y - dropingobj.transform.position.y);

                if (distancex < 6 && distancey < 4)
                {
                    GameManager.Instance.ObjectDroped();
                }

                Destroy(this.gameObject);
            }
            else if (Input.GetMouseButtonUp(0) && GameManager.Instance.WithControll)
            {
                GameManager.Instance.Question.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.unclickedsprite[GameManager.Instance.IconspriteID];
                float distancex = Mathf.Abs(transform.position.x - dropingobj.transform.position.x);
                float distancey = Mathf.Abs(transform.position.y - dropingobj.transform.position.y);
                if (distancex < 6 && distancey < 4 && GameManager.Instance.selectediconid < GameManager.Instance.Questionno)
                {
                    GameManager.Instance.AllFlower[GameManager.Instance.selectediconid].GetComponent<SpriteRenderer>().sprite = GameManager.Instance.normal_sprite[GameManager.Instance.IconspriteID];
                    GameManager.Instance.selectediconid++;
                }

                Destroy(this.gameObject);
            }
        }
    }
}