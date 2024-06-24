using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Maths.TeenBeads.Number
{
    public class IconDraw : MonoBehaviour
    {

        private bool clicked;
        private bool canChnagepos;
        public GameObject droppos;
        private Vector3 lastpos;
        public bool increser;

        private void Start()
        {
            if (!increser)
            {
                transform.SetSiblingIndex(Random.Range(0, 5));
            }
        }
        private void OnMouseDown()
        {

            clicked = true;
            lastpos = transform.position;
        }

        private void OnMouseUp()
        {
            if (!droppos)
                droppos = GameController.instance.ObjectDroping;
            clicked = false;
            if (Vector3.Distance(transform.position,droppos.transform.position) < 2)
            {
                if (increser)
                {
                    canChnagepos = true;
                    GameController.instance.Number2++;
                    GameController.instance.Number2text.text = GameController.instance.Number2.ToString();
                }
                else
                {
                    GameController.instance.stagetwoDrop++;
                    droppos.GetComponentInChildren<TextMeshPro>().text = transform.GetComponentInChildren<TextMeshPro>().text;
                    droppos.GetComponent<SpriteRenderer>().sprite = transform.GetComponent<SpriteRenderer>().sprite;
                    if (GameController.instance.stagetwoDrop == 3)
                    {
                        GameController.instance.Relod();
                    }
                }
                gameObject.SetActive(false);
                transform.position = lastpos;
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