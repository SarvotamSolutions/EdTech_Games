using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GestureRecognizer;
using TMPro;
namespace Laguage.ai
{
    public class GameControllerAi : GameControllerforAll
    {
        public Recognizer recognizer; 
        public ExampleGestureHandler AItextcheck;
        public char[] allchar;
        public TextMeshPro[] alltext;
        int no;
        private void Start()
        {
            GameSet();
        }
        void GameSet()
        {
            if (reloding >= allCharacter.Length)
            {
                gamePlay = false;
                AItextcheck.transform.parent.gameObject.SetActive(false);
                StartCoroutine(LevelCompleted());
                return;
            }
            droping_place[no].color = sellect_answer_color;
            AItextcheck.textResult = alltext[no];
            allchar = allCharacter[reloding].Letter.ToCharArray();
            recognizer.Recognigingnumber = allCharacter[reloding].Letter[no].ToString();
            recognizer.Changerecogniger();
            Icon.sprite = allCharacter[reloding].letterSprite;

        }
        public void CheckAnswer()
        {


            if(allchar[no].ToString() == AItextcheck.notext)
            {
                Boarder.color = currect_answer_color;
                droping_place[no].color = currect_answer_color;
                no++;
                if (no < alltext.Length)
                {
                  //  droping_place[no].color = sellect_answer_color;
                   
                }
                    
                if (no >= allchar.Length)
                {
                    AItextcheck.textResult = null;
                    AItextcheck.transform.parent.gameObject.SetActive(false);
                    StartCoroutine(WaitForCurrectanimtion());
                 
                }
                else
                {
                    GameSet();
                }




            }
            else
            {
                droping_place[no].color = wrong_answer_color;
            }
        }

        protected override void CurrectAnimtionCompleted()
        {
            base.CurrectAnimtionCompleted();
            reloding++;
            no = 0;
            Boarder.color = Color.white;
            AItextcheck.textResult = alltext[no];
            foreach (var item in alltext)
            {
                item.text = "?";

            }
            foreach (var item in droping_place)
            {
                item.color = Color.white;

            }
            AItextcheck.transform.parent.gameObject.SetActive(true);
            GameSet();
        }
    }

}