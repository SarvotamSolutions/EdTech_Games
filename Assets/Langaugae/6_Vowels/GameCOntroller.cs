using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Laguage.vowels
{
    public class GameCOntroller : GameControllerforAll
    {
        public Color darkwhite, selecteddrop, neartodrop;
        public int blankno;
        public bool ending;
        public bool allleter;
        public bool middle;
        public int maxreloding;

        public override void GameStart()
        {
            base.GameStart();
            char[] letername = allCharacter[AllAnswerNo[reloding - 1]].sameLetter[letterno].Name.ToCharArray();
            Debug.Log(letername.Length);
            lettersound = allCharacter[AllAnswerNo[reloding - 1]].sameLetter[letterno].Sound;
            if (!allleter)
            {
                blankno = Random.Range(0, letername.Length);
                if (!ending && !middle)
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
                }
            }
            else
            {
                blankno = 0;
            }

            Boarder.color = Color.white;
            for (int i = 0; i < droping_place.Length; i++)
            {
                droping_place[i].GetComponentInChildren<TextMeshPro>().color = darkwhite;
                if (!allleter)
                {
                    if (i != blankno)
                    {
                        droping_place[i].color = new Color(1, 1, 1, 1);
                        droping_place[i].GetComponentInChildren<TextMeshPro>().text = letername[i].ToString();
                        droping_place[i].sprite = Normal_answer[0];
                    }
                    else
                    {
                        droping_place[i].sprite = Normal_answer[1];
                        question_text = droping_place[i].GetComponentInChildren<TextMeshPro>();
                        question_text.text = "?";
                    }
                }
                else
                {
                    droping_place[i].color = selecteddrop;
                    droping_place[i].sprite = Normal_answer[1];
                    question_text = droping_place[i].GetComponentInChildren<TextMeshPro>();
                    question_text.text = "?";
                }
            }
            question_text = droping_place[blankno].GetComponentInChildren<TextMeshPro>();
            droping_place[blankno].color = sellect_answer_color;

        }
        public override void WrongAnswer()
        {

            gamePlay = false;
            base.WrongAnswer();
            selectedoption.background.color = wrong_answer_color;
            droping_place[blankno].GetComponentInChildren<TextMeshPro>().color = Color.white;
            droping_place[blankno].color = wrong_answer_color;
            droping_place[blankno].sprite = wrong_answer[0];
            StartCoroutine(WaitWrongAnimtion());
        }
        public override void CurrectAnswer()
        {
            gamePlay = false;
            selectedoption.background.color = currect_answer_color;
            if (!allleter)
            {
                for (int i = 0; i < droping_place.Length; i++)
                {
                    droping_place[i].GetComponentInChildren<TextMeshPro>().color = Color.white;
                    droping_place[i].sprite = currect_answer[0];
                }
                StartCoroutine(WaitForCurrectanimtion());
            }
            if (allleter)
            {
                droping_place[blankno].GetComponentInChildren<TextMeshPro>().color = Color.white;
                droping_place[blankno].sprite = currect_answer[0];

                if (blankno == 2)
                {
                    StartCoroutine(WaitForCurrectanimtion());
                }
                else
                {
                    CurrectAnimtionCompleted();
                }
            }
        }
        protected override void CurrectAnimtionCompleted()
        {
            base.CurrectAnimtionCompleted();
            if (reloding > maxloding)
                return;
            selectedoption.background.color = Color.white;
            selectedoption.transform.position = selectedoption.lastpos;
            selectedoption.background.enabled = true;
            selectedoption.text.enabled = true;
            selectedoption.Border.color = darkwhite;
            if (allleter)
            {
                droping_place[blankno].GetComponentInChildren<TextMeshPro>().text = selectedoption.no;
                droping_place[blankno].sprite = currect_answer[0];

                blankno++;
                for (int i = blankno; i < 3; i++)
                {
                    droping_place[i].transform.GetComponentInChildren<TextMeshPro>().text = "?";
                }

                if (blankno == 3)
                {
                    GameStart();
                }
                else
                {
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
            if (Vector3.Distance(droping_place[blankno].transform.position, selectedoption.transform.position) < distangedrage)
            {
                droping_place[blankno].color = neartodrop;
                return true;
            }
            droping_place[blankno].color = selecteddrop;
            return false;
        }
        public override void ResetingDrage()
        {
            selectedoption.background.color = Color.white;

            if (allleter)
            {
                droping_place[blankno].sprite = Normal_answer[1];
                question_text = droping_place[blankno].GetComponentInChildren<TextMeshPro>();
                question_text.color = darkwhite;
                question_text.text = "?";
            }
            else
            {
                for (int i = 0; i < droping_place.Length; i++)
                {
                    droping_place[i].color = Color.white;
                    droping_place[i].sprite = Normal_answer[0];
                    droping_place[i].GetComponentInChildren<TextMeshPro>().color = darkwhite;
                }
            }
            selectedoption.background.enabled = true;
            selectedoption.text.enabled = true;
            droping_place[blankno].sprite = Normal_answer[1];
            question_text.text = "?";
            droping_place[blankno].color = selecteddrop;
            base.ResetingDrage();
            gamePlay = true;

        }

        public void LetterAudioPlay()
        {
            GetComponent<AudioSource>().PlayOneShot(lettersound);
        }
    }
}