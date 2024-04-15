using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Maths.CountAndCards.count
{
    public class Buttons : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
    {
        public Transform parent;
        public Transform DropingParent;
        public Transform DraginParent;
        public void OnBeginDrag(PointerEventData eventData)
        {

            transform.parent = parent;
        }

        public void OnDrag(PointerEventData eventData)
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            transform.position = pos;
        }

        public void OnDrop(PointerEventData eventData)
        {

            Debug.Log(Vector3.Distance(transform.position, DropingParent.transform.GetChild(DropingParent.childCount - 1).transform.position));
            if (Vector3.Distance(transform.position, DropingParent.transform.GetChild(DropingParent.childCount - 1).transform.position) < 90.01f)
            {
                transform.parent = DropingParent;
                transform.SetAsFirstSibling();
                if(GameController.instace.no== DropingParent.childCount - 1)
                {
                    DropingParent.GetChild(DropingParent.childCount - 1).gameObject.SetActive(false);
                    Debug.Log("level completed");
                }
                //     Debug.Log(GameController.instance.numbers);
            }
            else
            {

                transform.parent = DraginParent;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}