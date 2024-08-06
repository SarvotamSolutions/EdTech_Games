using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Maths.CountAndCards.count
{
    public class Buttons : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
    {
        private AudioSource sound;
        public AudioClip pickup, drop;
        public Transform parent;
        public Transform DropingParent;
        public Transform DraginParent;
        public void OnBeginDrag(PointerEventData eventData)
        {

            if (!GameController.Instance.gamePlay)
                return;
            sound.clip = pickup;
            sound.Play();

            transform.parent = parent;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!GameController.Instance.gamePlay)
                return;
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            transform.position = pos;
        }
        
        public void OnDrop(PointerEventData eventData)
        {

            if (!GameController.Instance.gamePlay)
                return;

            sound.clip = drop;
            sound.Play();
            Debug.Log(Vector3.Distance(transform.position, DropingParent.transform.GetChild(DropingParent.childCount - 1).transform.position));
            if (!GameController.Instance.hint)
            {
                if (Vector3.Distance(transform.position, DropingParent.transform.GetChild(DropingParent.childCount - 1).transform.position) < 90.01f)
                {
                    transform.parent = DropingParent;
                    transform.SetAsFirstSibling();
                    if (GameController.Instance.no == DropingParent.childCount - 1)
                    {
                        DropingParent.GetChild(DropingParent.childCount - 1).gameObject.SetActive(false);
                        Debug.Log("level completed");
                    }
                }
                else
                {
                    transform.parent = DraginParent;
                }
            }
            else
            {
                if (Mathf.Abs(transform.position.x - DropingParent.transform.position.x) < GameController.Instance.distance.x &&
                    Mathf.Abs(transform.position.y - DropingParent.transform.position.x) < GameController.Instance.distance.y)
                {
                    GameController.Instance.distance.y +=.5f;
                    transform.parent = DropingParent;
                    transform.SetAsFirstSibling();
                }
                else
                {
                    transform.parent = DraginParent;
                }
            }
        }

        void Start()
        {
            sound = GetComponent<AudioSource>();
        }
    }

}