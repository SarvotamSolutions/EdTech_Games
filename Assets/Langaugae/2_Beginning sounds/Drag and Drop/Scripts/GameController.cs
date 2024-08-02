
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
        public Color QuestionColor;
        
        protected override void Start()
        {
         //  base.Start();
            GameStart();
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
            }// check answer is not added the same as prvious one

            OptionNO.Add(answerno);
            AllAnswerNo.Add(answerno);
            int answeroption = Random.Range(0, alloption.Length);
            int i = 0;
            foreach (var option in alloption)
            {

                no = Random.Range(0, allCharacter.Length);
                //if (no == answerno)
                //{
                //    no = Random.Range(0, allCharacter.Length);
                //}
                for (int j = 0; j < OptionNO.Count; j++)
                {
                    if (no == OptionNO[j])
                    {
                        no = Random.Range(0, allCharacter.Length);
                        //if (no == answerno)
                        //{
                        //    no = Random.Range(0, allCharacter.Length);
                        //}
                        j = -1;
                    }
                }


                if (i == answeroption)
                {
                    letter = allCharacter[answerno].Letter;
                   // Icon.sprite = Icon ? allCharacter[answerno].sameLetter[Random.Range(0, allCharacter[answerno].sameLetter.Length)].Icon : null;
                    option.no = allCharacter[answerno].Letter;
                  
                    
                    int randno = Random.Range(0, allCharacter[answerno].sameLetter.Length);
                    alloption[i].Icon.sprite = allCharacter[answerno].sameLetter[randno].Icon;
                    if (letters)
                    {
                        question_text.text = allCharacter[answerno].Letter;
                        lettersound = allCharacter[answerno].lettersound;
                    }
                    else
                    {
                        Debug.Log("XX");
                        question_text.text = allCharacter[answerno].sameLetter[randno].Name;
                        lettersound = allCharacter[answerno].sameLetter[randno].Sound;
                    }
                    alloption[i].pickup = allCharacter[answerno].sameLetter[randno].Sound; 
                }
                else
                {

                    option.no = allCharacter[no].Letter;
                    OptionNO.Add(no);
                    int randno = Random.Range(0, Random.Range(0, allCharacter[no].sameLetter.Length));
                    alloption[i].Icon.sprite = allCharacter[no].sameLetter[randno].Icon;
                    alloption[i].pickup = allCharacter[no].sameLetter[randno].Sound;
                }

                i++;

            }
          
         
          
            gamePlay = true;
        }


        public override bool Neartodestination()
        {

            if (base.Neartodestination())
            {
                Drager drag = selectedoption.GetComponent<Drager>();
                GetComponent<AudioSource>().clip = lettersound;
                GetComponent<AudioSource>().PlayDelayed(.3f);
                if(drag.no == letter)
                {
                    
                    //currect answer
                    for(int k = 0; k < droping_place.Length;k++)
                    {
                        droping_place[k].gameObject.SetActive(false);
                    }
                    question_text.color = Color.white;
                    arrow.color = currect_answer_color;
                    question_text.color = Color.white;
                    CheckAnswerSprite.sprite = currectanswer;
                    selectedoption.transform.GetChild(1).GetComponent<SpriteRenderer>().color = currect_answer_color;
                    CurrectAnswer();
                    return true;

                }
                else
                {
                    // wrong asnwer
                    for (int k = 0; k < droping_place.Length; k++)
                    {
                        droping_place[k].gameObject.SetActive(false);
                    }
                    question_text.color = Color.white;

                    gamePlay = false;
                    arrow.color = wrong_answer_color;
                    question_text.color = Color.white;
                    CheckAnswerSprite.sprite = wrongAnswer;
                    selectedoption.transform.GetChild(1).GetComponent<SpriteRenderer>().color = wrong_answer_color;
                    StartCoroutine(WaitWrongAnimtion());
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
            Party_pop.GetComponent<AudioSource>().PlayDelayed(1);
            yield return new WaitForSeconds(CorrectAnswer_delayTime +1f);
            Party_pop.SetActive(false);
            ResetingDrage();
            GameStart();


        }
        public override void ResetingDrage()
        {
            arrow.color = Color.white;
            question_text.color = QuestionColor;
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