using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Laguage.Reading_sentences
{
    public class GameController : GameControllerforAll
    {
        int no;
        string[] allcharacterarry;
        public Color selectcolor,nonselectcolor;
        public Sprite wrong, currect, defalt;
        public SpriteRenderer droping_place_spriteRender,Draging_holder_spriteRender;
        public SpriteRenderer question;
        public Image background;
        public Sprite[] DropingPlaceSprites;
        public Sprite[] OptionHolder;
        public Sprite[] allbackground;
        public Color[] alltextcolor;
        public Color[] allQuestionHodercolor;
        private AudioSource audio;
        protected override void Start()
        {
            audio = GetComponent<AudioSource>();
            //base.Start();
            GameSet();
        }
        public void audioplay(AudioClip clip)
        {
            audio.PlayOneShot(clip);
        }
        void GameSet()
        {
            if (reloding > allCharacter.Length-1)
            {
                gamePlay = false;
                StartCoroutine(LevelCompleted());
                return;
            }

            foreach (var item in alloption)
            {
                item.background.sprite = defalt;
                item.gameObject.SetActive(false);
                item.GetComponent<Collider2D>().enabled = true;
            }
            foreach (var item in droping_place)
            {
                item.color = nonselectcolor;
                item.gameObject.SetActive(false);
            }
            droping_place[0].color = selectcolor;
            allcharacterarry = allCharacter[reloding].Letter.Split(" ");
            lettersound = allCharacter[reloding].lettersound;
            droping_place_spriteRender.sprite = DropingPlaceSprites[reloding];
            Draging_holder_spriteRender.sprite = OptionHolder[reloding];
            question.color = allQuestionHodercolor[reloding];
            background.sprite = allbackground[reloding];
            for (int i = 0; i < allcharacterarry.Length; i++)
            {

                Debug.Log(i + " "+ allcharacterarry[i] + " getting insdie the for loop");
                alloption[i].text.text = allcharacterarry[i];
                alloption[i].gameObject.SetActive(true);
                alloption[i].no = allcharacterarry[i];
                alloption[i].text.color = alltextcolor[reloding];
                droping_place[i].gameObject.SetActive(true);
            }
            Icon.sprite = allCharacter[reloding].letterSprite;
            gamePlay = true;
         
        }
        public override bool Neartodestination()
        {
            selectedoption.background.sprite = defalt;
            if (Vector3.Distance(selectedoption.transform.position, droping_place[no].transform.position) < distangedrage)
            {

                selectedoption.transform.position = droping_place[no].transform.position;
                if(selectedoption.no == allcharacterarry[no])//currect answer
                {
                    selectedoption.text.color = Color.white;
                    selectedoption.background.sprite = currect;
                    no++;
                    droping_place[no].color = selectcolor;
                    droping_place[no-1].color = nonselectcolor;
                    selectedoption.GetComponent<Collider2D>().enabled = false;
                    if(no == allcharacterarry.Length)
                    {
                        Boarder.color = currect_answer_color;
                        gamePlay = false;
                        no = 0;
                        reloding++;
                        GetComponent<AudioSource>().PlayOneShot(lettersound);
                        StartCoroutine(WaitForCurrectanimtion());
                        
                    }
                  //  ;
                }
                else
                {

                    Boarder.color = wrong_answer_color;
                    gamePlay = false;
                    selectedoption.background.sprite = wrong;
                    selectedoption.text.color = Color.white;
                    StartCoroutine(WaitWrongAnimtion());
                }
                return true;
            }
            
            return false;
        }

        
        public override void ResetingDrage()
        {
            Debug.Log("reseting");
            selectedoption.text.color = alltextcolor[reloding];
            selectedoption.background.sprite = defalt;
            //base.ResetingDrage();
            selectedoption.transform.position = selectedoption.lastpos;
            selectedoption = null;
            OptionNO.Clear();
            Boarder.color = Color.white;
            gamePlay = true;
        }
        protected override void CurrectAnimtionCompleted()
        {
          //  base.CurrectAnimtionCompleted();

            GameSet();
            Boarder.color = Color.white;
        }
    }
}