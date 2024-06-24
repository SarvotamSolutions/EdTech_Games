using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laguage.Reading_sentences
{
    public class GameController : GameControllerforAll
    {
        int no;
        string[] allcharacterarry;
        public Color selectcolor,nonselectcolor;
        public Sprite wrong, currect, defalt;
        protected override void Start()
        {
            //base.Start();
            GameSet();
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
           
            for (int i = 0; i < allcharacterarry.Length; i++)
            {
                Debug.Log(i + " "+ allcharacterarry[i]);
                alloption[i].text.text = allcharacterarry[i];
                alloption[i].gameObject.SetActive(true);
                alloption[i].no = allcharacterarry[i];
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
                if(selectedoption.no == allcharacterarry[no])
                {
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
                        StartCoroutine(WaitForCurrectanimtion());
                        
                    }
                  //  ;
                }
                else
                {
                    Boarder.color = wrong_answer_color;
                    gamePlay = false;
                    selectedoption.background.sprite = wrong;
                    StartCoroutine(WaitWrongAnimtion());
                }
                return true;
            }
            
            return false;
        }
        public override void ResetingDrage()
        {
            selectedoption.background.sprite = defalt;
            base.ResetingDrage();
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