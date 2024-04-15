using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maths.Number1to10.numberWithObject
{
    public class ObjectDraging : MonoBehaviour
    {
        public GameObject dropingobj;
        public SpriteRenderer Normalsprite;
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                GameManager.Instance.Question.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.ClickedSprite[GameManager.Instance.no];
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }

            if (Input.GetMouseButtonUp(0))
            {
                GameManager.Instance.Question.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.unclickedsprite[GameManager.Instance.no];
                float distancex = Mathf.Abs(transform.position.x - dropingobj.transform.position.x);
                float distancey = Mathf.Abs(transform.position.y - dropingobj.transform.position.y);

                if (distancex < 6 && distancey < 4)
                {
                    GameManager.Instance.ObjectDroped();
                }

                Destroy(this.gameObject);
            }
        }


    }
}