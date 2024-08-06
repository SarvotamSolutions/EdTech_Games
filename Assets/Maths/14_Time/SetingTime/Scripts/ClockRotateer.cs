using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.Times.ClockTimeSet
{
    public class ClockRotateer : MonoBehaviour
    {

        public GameObject pointer;
        bool waitfor_rotate;
        public AudioSource ClockTikTok;


        public GameObject obj;
        bool activate;
        public bool hour;

        public int time;
        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnMouseDown()
        {
            activate = true;
            ClockTikTok = GetComponent<AudioSource>();
        }
        public float distcgegap;
        Vector3 touchpos;
        // Update is called once per frame
        void Update()
        {
            if (GameController.instace.totorial.totorialplaying || GameController.instace.GAMEPLAY == false)
                return;
            if (Input.GetMouseButton(0) && activate)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;

                obj.transform.position = pos;
                float distance = pointer.transform.localPosition.x - obj.transform.localPosition.x;
                float distancey = pointer.transform.localPosition.y - obj.transform.localPosition.y;
                if (distancey > -9 && distancey < 9)
                {
                    if (distance > .3 || distance < -.3)
                    {
                        if (pointer.transform.localPosition.x > obj.transform.localPosition.x)
                        {

                            if (!waitfor_rotate)
                            {
                                waitfor_rotate = true;

                                StartCoroutine(RotatePointer());
                            }
                        }
                        else if (pointer.transform.localPosition.x < obj.transform.localPosition.x)
                        {
                            if (!waitfor_rotate)
                            {
                                waitfor_rotate = true;
                                StartCoroutine(RotatePointerDecress());
                            }
                        }
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                activate = false;
            }
        }
        IEnumerator RotatePointer()
        {
            activate = false;
            yield return new WaitForSeconds(.005f);
            ClockTikTok.Play();

            activate = true;
            Vector3 rotate = new Vector3(0, 0, transform.localEulerAngles.z + 6f);
            time = time - 1;
            if (time < 0)
            {
                time = 59;
            }
            if (hour)
            {
                GameController.instace.Hour = time / 5;
            }
            else
            {
                GameController.instace.minit = time;
            }
            foreach (var item in GameController.instace.alltext)
            {
                item.color = Color.black;
            }

            if (GameController.instace.minit % 5 == 0)
            {
                GameController.instace.alltext[GameController.instace.minit / 5].color = Color.blue;
            }
            GameController.instace.alltext[GameController.instace.Hour].color = Color.blue;
            Quaternion temp = Quaternion.Euler(rotate);
            transform.localEulerAngles = new Vector3(0, 0, temp.eulerAngles.z);
            float angle = temp.eulerAngles.z + .5f;
            waitfor_rotate = false;
        }
        IEnumerator RotatePointerDecress()
        {

            yield return new WaitForSeconds(.005f);
            ClockTikTok.Play();

            Vector3 rotate = new Vector3(0, 0, transform.localEulerAngles.z - 6f);
            time = time + 1;
            time = time % 60;
            if (hour)
            {
                GameController.instace.Hour = time / 5;
            }
            else
            {
                GameController.instace.minit = time;
            }
            foreach (var item in GameController.instace.alltext)
            {
                item.color = Color.black;
            }
            if (GameController.instace.minit % 5 == 0)
            {
                GameController.instace.alltext[GameController.instace.minit / 5].color = Color.blue;
            }
            GameController.instace.alltext[GameController.instace.Hour].color = Color.blue;
            Quaternion temp = Quaternion.Euler(rotate);
            transform.localEulerAngles = new Vector3(0, 0, temp.eulerAngles.z);
            float angle = temp.eulerAngles.z + .5f;
            waitfor_rotate = false;
        }


    }
}