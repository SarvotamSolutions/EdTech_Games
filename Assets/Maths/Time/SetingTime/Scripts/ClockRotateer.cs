using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.Times.ClockTimeSet
{
    public class ClockRotateer : MonoBehaviour
    {

        public GameObject pointer;
        bool waitfor_rotate;

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
        }
        public float distcgegap;
        Vector3 touchpos;
        // Update is called once per frame
        void Update()
        {
              //  pointer.transform.LookAt(obj.transform.up);
            if (Input.GetMouseButton(0) && activate)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
////
             
                pos = new Vector3(Mathf.Clamp(pos.x, -7, 7), Mathf.Clamp(pos.y, -7,7), 0);
                pos.z = 0;
               
                obj.transform.position = pos;
                //  obj.transform.LookAt(pointer.gameObject.transform.forward);
                float distance = pointer.transform.localPosition.x - obj.transform.localPosition.x;
                float distancey = pointer.transform.localPosition.y - obj.transform.localPosition.y;
                Debug.Log(distancey);
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
            yield return new WaitForSeconds(.075f);
            activate = true;
            Vector3 rotate = new Vector3(0, 0, transform.localEulerAngles.z +6f);
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

            foreach (var item in GameController.instace.allminits)
            {
                item.color = Color.black;
            }
            GameController.instace.allminits[GameController.instace.minit].color = Color.white;
            GameController.instace.allminits[GameController.instace.minit].sprite = GameController.instace.blueicon;
            if (GameController.instace.minit % 5 == 0)
            {
                GameController.instace.alltext[GameController.instace.minit / 5].color = Color.blue;
            }
            GameController.instace.alltext[GameController.instace.Hour].color = Color.blue;
            Quaternion temp = Quaternion.Euler(rotate);
      //      pointer.transform.RotateAroundLocal(Vector3.forward, .1f);
              transform.localEulerAngles = new Vector3(0, 0, temp.eulerAngles.z);
            float angle = temp.eulerAngles.z + .5f;
            Debug.Log(angle);
            /// Debug.Log(pos.x + "_" + pointer.transform.position.x);
            waitfor_rotate = false;
            //     StartCoroutine(RotatePointer());
        }
        IEnumerator RotatePointerDecress()
        {
           
            yield return new WaitForSeconds(.1f);


            Vector3 rotate = new Vector3(0, 0, transform.localEulerAngles.z -6f);
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
            foreach (var item in GameController.instace.allminits)
            {
                item.color = Color.black;
            }
            GameController.instace.allminits[GameController.instace.minit].color = Color.white;
            GameController.instace.allminits[GameController.instace.minit].sprite = GameController.instace.blueicon;
            if (GameController.instace.minit % 5 == 0)
            {
                GameController.instace.alltext[GameController.instace.minit / 5].color = Color.blue;
            }
            GameController.instace.alltext[GameController.instace.Hour].color = Color.blue;
            Quaternion temp = Quaternion.Euler(rotate);
          //  pointer.transform.RotateAroundLocal(Vector3.forward, -.1f);
              transform.localEulerAngles = new Vector3(0, 0, temp.eulerAngles.z);
            float angle = temp.eulerAngles.z + .5f;
            /// Debug.Log(pos.x + "_" + pointer.transform.position.x);
            waitfor_rotate = false;
            //     StartCoroutine(RotatePointer());
        }


    }
}