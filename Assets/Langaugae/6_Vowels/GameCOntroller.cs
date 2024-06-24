using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Laguage.vowels
{ 
    public class GameCOntroller : GameControllerforAll
    {
        public Color darkwhite,selecteddrop,neartodrop;
        public int blankno;
        public bool ending;
        public bool allleter;
        public bool middle;
        public bool starting;

        public override void GameStart()
        {
            base.GameStart();
            char[] letername = allCharacter[AllAnswerNo[reloding-1]].sameLetter[letterno].name.ToCharArray();
            Debug.Log(letername.Length);
            if (!allleter) { 
                blankno = Random.Range(0, letername.Length);
                if (!ending && !middle && !starting)
                {
                    while (blankno == 1)
                    {
                        blankno = Random.Range(0, letername.Length);

                    }
                }
                else if (ending)
                {
                    blankno = 2;

                }
                else
                if (middle)
                {
                    blankno = 1;
                }else if (starting)
                {
                    blankno = 0;
                }
            }
            else
            {
                blankno=0;
            }
            Debug.Log(blankno);
            Boarder.color = Color.white;
            for (int i = 0; i < droping_place.Length; i++)
            {
                if (!allleter)
                {
                    if (i != blankno)
                    {
                        droping_place[i].color = new Color(1, 1, 1, 1);
                        droping_place[i].GetComponentInChildren<TextMeshPro>().text = letername[i].ToString();
                        droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    else
                    {

                        droping_place[i].color = selecteddrop;
                        droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = sellect_answer_color;
                        question_text = droping_place[i].GetComponentInChildren<TextMeshPro>();
                        question_text.text = "?";
                    }
                }
                else
                {
                        droping_place[i].color = selecteddrop;
                    droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                    question_text = droping_place[i].GetComponentInChildren<TextMeshPro>();
                        question_text.text = "?";
                    
                }
                droping_place[i].GetComponentInChildren<TextMeshPro>().color = Color.black;
            }
            question_text = droping_place[blankno].GetComponentInChildren<TextMeshPro>();
            droping_place[blankno].transform.GetChild(0).GetComponent<SpriteRenderer>().color = sellect_answer_color;
           
        }
        public override void WrongAnswer()
        {
         
            gamePlay = false;
            base.WrongAnswer();
            Boarder.color = wrong_answer_color;
            selectedoption.background.color = wrong_answer_color;
            for (int i = 0; i < droping_place.Length; i++)
            {
                droping_place[i].color = wrong_answer_color;
                droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = wrong_answer_color;
            }

            StartCoroutine(WaitWrongAnimtion());
        }
        public GameObject keybordpaneel;
        public override void CurrectAnswer()
        {
            keybordpaneel.SetActive(false);
            gamePlay = false;
            droping_place[blankno].GetComponentInChildren<TextMeshPro>().text = selectedoption.no;

          //  droping_place[blankno].color = currect_answer_color;
            droping_place[blankno].transform.GetChild(0).GetComponent<SpriteRenderer>().color = currect_answer_color;
            
            selectedoption.background.color = currect_answer_color;
            if (!allleter)
            {
                Boarder.color = currect_answer_color;
                for (int i = 0; i < droping_place.Length; i++)
                {
                    droping_place[i].GetComponentInChildren<TextMeshPro>().color = Color.white;
                    droping_place[i].color = currect_answer_color;
                    droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = currect_answer_color;

                }
                StartCoroutine(WaitForCurrectanimtion());
            }
            if (allleter)
            {
                if (blankno < 2)
                {

                    CurrectAnimtionCompleted();
                }
                else
                {
                    Boarder.color = currect_answer_color;
                    for (int i = 0; i < droping_place.Length; i++)
                    {
                        droping_place[i].GetComponentInChildren<TextMeshPro>().color = Color.white;
                        droping_place[i].color = currect_answer_color;
                        droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = currect_answer_color;

                    }

                    StartCoroutine(WaitForCurrectanimtion());
                }

            }
            Debug.Log("Currect answer");

           

           
        }
        protected override void CurrectAnimtionCompleted()
        {
            //set c
           keybordpaneel.SetActive(true);
            base.CurrectAnimtionCompleted();
            selectedoption.background.color = Color.white;
            selectedoption.transform.position = selectedoption.lastpos;
            selectedoption.Border.color = darkwhite;
            if (allleter)
            {
                droping_place[blankno].GetComponentInChildren<TextMeshPro>().text = selectedoption.no;
                droping_place[blankno].color = currect_answer_color;
                droping_place[blankno].transform.GetChild(0).GetComponent<SpriteRenderer>().color = currect_answer_color;
              
                blankno++;
                for (int i = blankno; i < 3; i++)
                {
                  
                    droping_place[i].transform.GetComponentInChildren<TextMeshPro>().text = "?";
                }

                if(blankno == 3)
                {
                    GameStart();
                }
                else
                {
                    droping_place[blankno].transform.GetChild(0).GetComponent<SpriteRenderer>().color = sellect_answer_color;
                    question_text = droping_place[blankno].transform.GetComponentInChildren<TextMeshPro>();
                }
            }
            else
            {
               
                question_text.text = "?";
                GameStart();
            }
           
            gamePlay = true;
        }
        public override bool Neartodestination()
        {
            if (Vector3.Distance(droping_place[blankno].transform.position, selectedoption.transform.position)<distangedrage)
            {
                droping_place[blankno].color = neartodrop;
               // selectedoption.transform.position = droping_place[blankno].transform.position;
                return true;
            }
            droping_place[blankno].color = selecteddrop;
            return false;
        }
        public override void ResetingDrage()
        {
            selectedoption.background.color = Color.white;

            if(allleter)
            {
                for (int i = blankno; i < 3; i++)
                {
                    droping_place[i].color = selecteddrop;
                    droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                    question_text = droping_place[i].GetComponentInChildren<TextMeshPro>();
                    question_text.text = "?";
                }
                for (int i =0; i <blankno; i++)
                {
                    droping_place[i].color =currect_answer_color;
                    droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = currect_answer_color;
                }
               
            }
            else
            {
                for (int i = 0; i < droping_place.Length; i++)
                {
                    droping_place[i].color = Color.white;
                    droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            droping_place[blankno].transform.GetChild(0).GetComponent<SpriteRenderer>().color = sellect_answer_color;
            selectedoption.Border.color = darkwhite;
            question_text.text = "?";
            droping_place[blankno].color = selecteddrop;
            base.ResetingDrage();
            gamePlay = true;

        }

    }
}
