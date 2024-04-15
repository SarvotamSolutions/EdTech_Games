using DesignPatterns.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laguage.beginning_sounds.DragandDrop
{ 

    public class GameController : GameControllerforAll
    {
        public SpriteRenderer arrow;

        public SpriteRenderer CheckAnswerSprite;
        public Sprite currectanswer, wrongAnswer, normalAnswer;


        public SpriteRenderer[] alloption;
        public void Start()
        {
            GameStart();
        }

        void GameStart()
        {
         
            int no = 0;
            reloding++;
            if (reloding > 10)
            {
                StartCoroutine(LevelCompleted());
                return;
            }


            int answeroption = Random.Range(0, alloption.Length);
            // allno.Add(no);
            for (int i = 0; i < alloption.Length; i++)
            {
                no = Random.Range(0, allCharacter.Length);
                for (int j = 0; j < allno.Count; j++)
                {
                    if (no == allno[j])
                    {
                        no = Random.Range(0, allCharacter.Length);
                        j = -1;
                    }
                }
                if (i == answeroption)
                {
                  
                    question_text.text = allCharacter[no].letter;
                    //question_sprite.sprite = allcharacter[no].Icon;
                    letter = allCharacter[no].letter;
                  //  alloption[i].GetChild(0).GetComponent<SpriteRenderer>().sprite = allcharacter[no].relatedsdprite;
                 //   alloption[i].GetComponent<Drager>().no = allcharacter[no].letter;
                }
                else
                {
                  
                   
                }
                alloption[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allCharacter[no].Icon;
                alloption[i].GetComponent<Drager>().no = allCharacter[no].letter;
                allno.Add(no);


            }
        }


        public override bool Neartodestination()
        {

            if (base.Neartodestination())
            {
                Drager drag = selectedoption.GetComponent<Drager>();

                if(drag.no == letter)
                {
                    arrow.color = currect_answer_color;
                    CheckAnswerSprite.sprite = currectanswer;
                    selectedoption.transform.GetChild(1).GetComponent<SpriteRenderer>().color = currect_answer_color;
                    //currect answer
                    CurrectAnswer();

                }
                else
                {
                    arrow.color = wrong_answer_color;
                    CheckAnswerSprite.sprite = wrongAnswer;
                    selectedoption.transform.GetChild(1).GetComponent<SpriteRenderer>().color = wrong_answer_color;
                    StartCoroutine(WaitWrongAnimtion());
                    //wrong asnwer

                }
            }
            else
            {

            }
            return base.Neartodestination();
        }
        public void CurrectAnswer()
        {
           
            StartCoroutine(RelodingThe_Level());
          
         
        }

        IEnumerator RelodingThe_Level()
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(2);
            Party_pop.SetActive(false);
            ResetingDrage();
            GameStart();


        }
        public override void ResetingDrage()
        {
            arrow.color = Color.white;
            CheckAnswerSprite.sprite = normalAnswer;
            foreach (var item in alloption)
            {
                item.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
                item.transform.position = item.GetComponent<Drager>().lastpos;
            }
            base.ResetingDrage();
        }
      
        //protected override IEnumerator WrongAnimtion()
        //{


        //    CheckAnswerSprite.sprite = normalAnswer;
        //   
        //    arrow.color = Color.white;
        //}



    }

}