
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laguage.beginning_sounds.DragandDrop
{ 

    public class GameController : GameControllerforAll
    {
        public SpriteRenderer arrow;
        public bool letters;
        public SpriteRenderer CheckAnswerSprite;
        public Sprite currectanswer, wrongAnswer, normalAnswer;

        protected override void Start()
        {
            base.Start();
           // GameStart();
        }
        
      
        public override void GameStart()
        {
         //   base.GameStart();
            int no = 0;
            reloding++;
            if (reloding > allCharacter.Length)
            {
                StartCoroutine(LevelCompleted());
                return;
            }
            int answerno = Random.Range(0, allCharacter.Length);
            for (int j = 0; j < AllAnswerNo.Count; j++)
            {
                if (AllAnswerNo[j] == answerno)
                {
                    answerno = Random.Range(0, allCharacter.Length);
                    j = -1;
                }
            }
            int answeroption = Random.Range(0, alloption.Length);
            int i = 0;
            foreach (var option in alloption)
            {

                no = Random.Range(0, allCharacter.Length);
                if (no == answerno)
                {
                    no = Random.Range(0, allCharacter.Length);
                }
                for (int j = 0; j < OptionNO.Count; j++)
                {
                    if (no == OptionNO[j])
                    {
                        no = Random.Range(0, allCharacter.Length);
                        if (no == answerno)
                        {
                            no = Random.Range(0, allCharacter.Length);
                        }
                        j = -1;
                    }
                }

                if (i == answeroption)
                {
                    letter = allCharacter[answerno].Letter;
                   // Icon.sprite = Icon ? allCharacter[answerno].sameLetter[Random.Range(0, allCharacter[answerno].sameLetter.Length)].Icon : null;
                    option.no = allCharacter[answerno].Letter;
                    AllAnswerNo.Add(answerno);
                    OptionNO.Add(answerno);
                    int randno = Random.Range(0, allCharacter[answerno].sameLetter.Length);
                    alloption[i].Icon.sprite = allCharacter[answerno].sameLetter[randno].Icon;
                    if (letters)
                    {
                        question_text.text = allCharacter[answerno].Letter;
                    }
                    else
                    {
                        Debug.Log("XX");
                        question_text.text = allCharacter[answerno].sameLetter[randno].name;
                    }
                }
                else
                {
                    option.no = allCharacter[no].Letter;
                    OptionNO.Add(no);
                    alloption[i].Icon.sprite = allCharacter[no].sameLetter[Random.Range(0, allCharacter[no].sameLetter.Length)].Icon;
                }

                i++;

            }

         
            //   int no = 0;
            //   reloding++;
            //   if (reloding > allCharacter.Length)
            //   {
            //       StartCoroutine(LevelCompleted());
            //       return;
            //   }

            ////   int answer = Random.Ran
            //   int answeroption = Random.Range(0, alloption.Length);
            //   // allno.Add(no);
            //   for (int i = 0; i < alloption.Length; i++)
            //   {
            //       no = Random.Range(0, allCharacter.Length);
            //       for (int j = 0; j < OptionNO.Count; j++)
            //       {
            //           if (no == OptionNO[j])
            //           {
            //               no = Random.Range(0, allCharacter.Length);
            //               j = -1;

            //           }
            //       }
            //       if (i == answeroption)
            //       {

            //           question_text.text = allCharacter[no].Letter;
            //           //question_sprite.sprite = allcharacter[no].Icon;
            //           letter = allCharacter[no].Letter;
            //           AllAnswerNo.Add(no);
            //           //  alloption[i].GetChild(0).GetComponent<SpriteRenderer>().sprite = allcharacter[no].relatedsdprite;
            //           //   alloption[i].GetComponent<Drager>().no = allcharacter[no].letter;
            //       }
            //       else
            //       {


            //       }
            //       alloption[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allCharacter[no].sameLetter[Random.Range(0,2)].Icon;
            //       alloption[i].GetComponent<Drager>().no = allCharacter[no].Letter;
            //       OptionNO.Add(no);



            //   }
            gamePlay = true;
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
                    return true;

                }
                else
                {
                    gamePlay = false;
                    arrow.color = wrong_answer_color;
                    CheckAnswerSprite.sprite = wrongAnswer;
                    selectedoption.transform.GetChild(1).GetComponent<SpriteRenderer>().color = wrong_answer_color;
                    StartCoroutine(WaitWrongAnimtion());
                   // wrong asnwer
                    return true;
                }
            }
            else
            {

            }

            return false;
        }
        public void CurrectAnswer()
        {
           
            StartCoroutine(RelodingThe_Level());
          
         
        }

        IEnumerator RelodingThe_Level()
        {
            gamePlay = false;
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