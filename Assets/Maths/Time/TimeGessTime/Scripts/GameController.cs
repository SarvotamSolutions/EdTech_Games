using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.Times.Selecttime
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        public GameObject[] Option;
        public int HourCout;
        public int MinitCount;
        public GameObject Smallneedle, MinitNeedle;

        public Sprite currectanswer;
        public Sprite wrongAnswer;
        public Sprite NormalAnswer;

        public string time;
        public int reloding;

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }
        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            Relod();
        }

        public void Relod()
        {
            reloding++;
            if (reloding > 11)
            {
                StartCoroutine(LevelCompleted());
                return;
            }

            HourCout = Random.Range(1, 13);
            MinitCount = Random.Range(0, 4);
            float rotateangle = HourCout * 30;
            float rotateAngleofminit = MinitCount * 90;
            Debug.Log(HourCout * 30);
            Debug.Log(rotateangle);
            Debug.Log(-rotateangle);

            Smallneedle.transform.Rotate(Vector3.forward, -rotateangle);
            MinitNeedle.transform.Rotate(Vector3.forward, -rotateAngleofminit);
            int ans = Random.Range(0, 4);
            for (int i = 0; i < Option.Length; i++)
            {
                if (i == ans)
                {
                    Option[ans].transform.GetChild(0).GetComponent<TextMeshPro>().text = HourCout.ToString("00") + ":" + (MinitCount * 15 % 60).ToString("00");
                    Option[ans].GetComponent<TimeSelect>().hour = HourCout;
                    Option[ans].GetComponent<TimeSelect>().Minits = MinitCount;
                }
                else
                {
                    int randHour = Random.Range(1, 13);
                    int randMinit = Random.Range(0, 4);
                    Option[i].GetComponent<TimeSelect>().hour = randHour;
                    Option[i].GetComponent<TimeSelect>().Minits = randMinit;
                    Option[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = randHour.ToString("00") + ":" + (randMinit * 15 % 60).ToString("00");
                }

            }

        }

        public IEnumerator WaitforRelod()
        {
            Party_pop.SetActive(true);
            //Smallneedle.transform.Rotate(Vector3.forward, 0f);
            //MinitNeedle.transform.Rotate(Vector3.forward, 0f);
            yield return new WaitForSeconds(3f);
            Party_pop.SetActive(false);
            Smallneedle.transform.localRotation = Quaternion.Euler(0, 0, 0);
            MinitNeedle.transform.localRotation = Quaternion.Euler(0, 0, 0);

            foreach (var item in Option)
            {
                item.GetComponent<SpriteRenderer>().sprite = NormalAnswer;
            }
            Relod();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public class Time
        {
            public int Hour;
            public int Minit;

        }
    }
}
