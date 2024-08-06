using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.Times.DragDrop
{
    public class GameController : Singleton<GameController>
    {

        public Totorial totorial;
        public GameObject[] ClockTimer;
        public GameObject[] OPtionTime;

        public Sprite currectAnswer,selctedclock,wrongAnswer;
        public Sprite CurrectAnswerArrow, selctanswerarrow,wronganswerArrow;
        public Sprite WrongAnswer;
        public SpriteRenderer ClockSprite;
        public Sprite NormalClock, wrongClock, Currectclock;
        private List<int> allno = new List<int>();
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(5.6f);
            SceneManager.LoadScene(0);
        }
        IEnumerator WrongAnswerAnimation()
        {
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            ClockTimer[selctedno].transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            ClockTimer[selctedno].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = selctanswerarrow;
            ClockTimer[selctedno].transform.GetComponent<SpriteRenderer>().sprite = selctedclock;
            ClockSprite.sprite = NormalClock;
            wrongAnswer_animtion.SetActive(false);
            gamePlay = true;
        }
   
        private void Start()
        {
            for (int i = 0; i < OPtionTime.Length; i++)
            {

                int no = Random.Range(0, OPtionTime.Length);
                no++;
                for (int j = 0; j < allno.Count; j++)
                {
                    if(allno[j]== no)
                    {
                        no = Random.Range(0, OPtionTime.Length);
                        no++;
                        j = -1;
                    }
                }
                OPtionTime[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = no.ToString();
                OPtionTime[i].GetComponent<Draging>().no = no;
                allno.Add(no);
            }
        }
        private int selctedno;
        public bool Neartodestination(GameObject obj)
        {
            if (Vector3.Distance(obj.transform.position, ClockTimer[selctedno].transform.position) < 1)
            {
                gamePlay = false;
                if (ClockTimer[selctedno].name == obj.GetComponent<Draging>().no.ToString())
                {
                    ClockTimer[selctedno].GetComponent<SpriteRenderer>().sprite = currectAnswer;
                    ClockTimer[selctedno].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = CurrectAnswerArrow;
                    Draging draging = obj.GetComponent<Draging>();
                    ClockTimer[selctedno].transform.GetChild(0).GetComponent<TextMeshPro>().text = draging.no.ToString();

                    selctedno++;
                    if (selctedno < 12)
                    {
                        ClockTimer[selctedno].GetComponent<SpriteRenderer>().sprite = selctedclock;
                        ClockTimer[selctedno].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = selctanswerarrow;
                    }
                    else
                    {
                        ClockSprite.sprite = Currectclock;
                        StartCoroutine(LevelCompleted());
                    }
                    gamePlay = true;

                    return true;
                }
                else
                {
                    ClockSprite.sprite = wrongClock;
                    ClockTimer[selctedno].GetComponent<SpriteRenderer>().sprite = wrongAnswer;
                    ClockTimer[selctedno].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = wronganswerArrow;
                    ClockTimer[selctedno].transform.GetChild(0).GetComponent<TextMeshPro>().text = obj.GetComponent<Draging>().no.ToString();
                    StartCoroutine(WrongAnswerAnimation());
                }
            }
            return false;
        }
    }
}