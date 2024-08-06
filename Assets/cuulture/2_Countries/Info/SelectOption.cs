using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace culture.Solorsystem.slectsolorsystem
{
    public class SelectOption : GameControllerforAll
    {
        public ButtonClick[] allbuttonclicks;
        int answerno = 0;
        public Sprite curectnaswersprite, wronganswer, defaltanswer;
        int curectnaswer;

        private void Start()
        {
            Gameset();
        }
        int n;
        public void Selecticon(int no)
        {
            if (!gamePlay)
                return;

            gamePlay = false;
            if (no == answerno)
            {
                allbuttonclicks[no].GetComponent<SpriteRenderer>().sprite = curectnaswersprite;
                StartCoroutine(WaitForCurrectanimtion());
            }
            else
            {
                n = no;
                allbuttonclicks[no].GetComponent<SpriteRenderer>().sprite = wronganswer;
                StartCoroutine(WaitWrongAnimtion());
            }

        }
        protected override void CurrectAnimtionCompleted()
        {
            allbuttonclicks[answerno].GetComponent<SpriteRenderer>().sprite = defaltanswer;
            Gameset();
            gamePlay = true;
        }

        public override void ResetingDrage()
        {
            allbuttonclicks[n].GetComponent<SpriteRenderer>().sprite = defaltanswer;
            gamePlay = true;
        }
        public void Gameset()
        {
            if (AllAnswerNo.Count >= allbuttonclicks[0].allicon.Length)
            {
                StartCoroutine(LevelCompleted());
                return;
            }

            OptionNO.Clear();
            answerno = Random.Range(0, allbuttonclicks.Length);
            curectnaswer = Random.Range(0, allbuttonclicks[0].allicon.Length);
            for (int i = 0; i < AllAnswerNo.Count; i++)
            {
                if (curectnaswer == AllAnswerNo[i])
                {
                    curectnaswer = Random.Range(0, allbuttonclicks[0].allicon.Length);

                    i = -1;
                }

            }
            AllAnswerNo.Add(curectnaswer);
            for (int i = 0; i < allbuttonclicks.Length; i++)
            {
                int no = Random.Range(0, allbuttonclicks[0].allicon.Length);
                if (i == answerno)
                {


                    allbuttonclicks[0].icon.sprite = allbuttonclicks[0].allicon[curectnaswer];
                    allbuttonclicks[i].nametext.text = allbuttonclicks[0].allname[curectnaswer];
                    OptionNO.Add(curectnaswer);
                }
                else
                {
                    while (no == curectnaswer)
                    {
                        no = Random.Range(0, allbuttonclicks[0].allicon.Length);
                    }
                    for (int j = 0; j < OptionNO.Count; j++)
                    {
                        if (no == OptionNO[j] || no == curectnaswer)
                        {
                            no = Random.Range(0, allbuttonclicks[0].allicon.Length);
                            j = -1;

                        }

                    }
                    OptionNO.Add(no);
                    allbuttonclicks[i].nametext.text = allbuttonclicks[0].allname[no];
                }
            }
        }
    }
}