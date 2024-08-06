using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.Times.Selecttime
{
    public class GameController : Singleton<GameController>
    {
        public Totorial totorial;
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
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(0);
        }
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

                    while(randHour == HourCout && randMinit == MinitCount)// ITS SAME AS ANSWER
                    {
                        randHour = Random.Range(1, 13);
                        randMinit = Random.Range(0, 4);
                    }
                    Option[i].GetComponent<TimeSelect>().hour = randHour;
                    Option[i].GetComponent<TimeSelect>().Minits = randMinit;
                    Option[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = randHour.ToString("00") + ":" + (randMinit * 15 % 60).ToString("00");
                }

            }
            gamePlay = true;

        }

        public IEnumerator WaitforRelod()
        {
            Party_pop.SetActive(true);

            yield return new WaitForSeconds(5f);
            Party_pop.SetActive(false);
            Smallneedle.transform.localRotation = Quaternion.Euler(0, 0, 0);
            MinitNeedle.transform.localRotation = Quaternion.Euler(0, 0, 0);

            foreach (var item in Option)
            {
                item.GetComponent<SpriteRenderer>().sprite = NormalAnswer;
                item.GetComponentInChildren<TextMeshPro>().color = new Color(0.2745098f, 0.4745098f, 0.6117647f, 1);
            }
            Relod();
        }

        public class Time
        {
            public int Hour;
            public int Minit;
        }
    }
}