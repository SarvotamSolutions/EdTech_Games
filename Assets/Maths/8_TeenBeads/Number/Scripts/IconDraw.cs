using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Maths.TeenBeads.Number
{
    public class IconDraw : MonoBehaviour
    {
        public AudioSource sound;
        public AudioClip pickup, drop;
        private bool clicked;
        public GameObject[] droppos;
        public Vector3 lastpos;
        public bool increser;
        [SerializeField] private Sprite currectanswer;
        [SerializeField] private Sprite wronganswer;
        private void Start()
        {
            if (!increser)
            {
                transform.SetSiblingIndex(Random.Range(0, 5));
            }
            lastpos = transform.position;
        }
        private void OnMouseDown()
        {
            if (GameController.instance.totorialcheck.totorialplaying)
                return;
            sound.clip = pickup;
            sound.Play();
            clicked = true;
            lastpos = transform.position;
        }

        private void OnMouseUp()
        {
            if (!clicked)
                return;
            sound.clip = drop;
            sound.Play();
            clicked = false;
            for (int i = 0; i < droppos.Length; i++)
            {
                if (Vector3.Distance(transform.position, droppos[i].transform.position) < 5)
                {
                    if (i == 0)
                    {
                        if (increser)
                        {
                            transform.parent = GameController.instance.ObjectDroping.transform.GetChild(0);

                            GameController.instance.Number2++;
                            GameController.instance.Number2text.text = GameController.instance.Number2.ToString();
                        }
                        else
                        {
                            GameController.instance.stagetwoDrop++;
                            droppos[i].GetComponentInChildren<TextMeshPro>().text = transform.GetComponentInChildren<TextMeshPro>().text;

                            if (GameController.instance.stagetwoDrop == 3)
                            {
                                GameController.instance.Relod();
                            }
                            droppos[i].GetComponent<SpriteRenderer>().sprite = currectanswer;

                            transform.position = lastpos;
                            gameObject.SetActive(false);
                        }
                    }
                }
            }
            transform.position = lastpos;
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