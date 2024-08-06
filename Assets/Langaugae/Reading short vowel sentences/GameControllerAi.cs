using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GestureRecognizer;
using TMPro;
using UnityEngine.UI;

namespace Laguage.ai
{
    public class GameControllerAi : GameControllerforAll
    {
        public Sprite SelectedSprite;
        public Color ResetColor;
        public Recognizer recognizer; 
        public ExampleGestureHandler AItextcheck;
        public char[] allchar;
        public TextMeshPro[] alltext;
        int no;
        public Sprite Normal, Wrong, Right,NormalDis;
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
            droping_place[no].sprite = SelectedSprite;
            AItextcheck.textResult = alltext[no];
            allchar = allCharacter[reloding].Letter.ToCharArray();
            recognizer.Recognigingnumber = allCharacter[reloding].Letter[no].ToString();
            recognizer.Changerecogniger();
            Icon.sprite = allCharacter[reloding].letterSprite;
            lettersound = allCharacter[reloding].sameLetter[0].Sound;
        }
        public void CheckAnswer()
        {


            if(allchar[no].ToString() == AItextcheck.notext)
            {
                droping_place[no].sprite = Right;
                droping_place[no].gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
                no++;
                    
                if (no >= allchar.Length)
                {
                    AItextcheck.textResult = null;
                    AItextcheck.transform.parent.gameObject.SetActive(false);
                    letterSoundPlay();
                    StartCoroutine(WaitForCurrectanimtion());
                }
                else
                {
                    GameSet();
                }
            }
            else
            {
                droping_place[no].sprite = Wrong;
                droping_place[no].gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
            }
        }

        protected override void CurrectAnimtionCompleted()
        {
            base.CurrectAnimtionCompleted();
            reloding++;
            no = 0;
            AItextcheck.textResult = alltext[no];
            foreach (var item in alltext)
            {
                item.text = "?";

            }
            for(int k = 0;k < droping_place.Length;k++)
            {
                if(k == 0)
                {
                    droping_place[k].sprite = Normal;
                    droping_place[k].gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().color = ResetColor;
                }
                else
                {
                    droping_place[k].sprite = NormalDis;
                    droping_place[k].gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().color = ResetColor;
                }

            }
            AItextcheck.transform.parent.gameObject.SetActive(true);
            GameSet();
        }
    }
}