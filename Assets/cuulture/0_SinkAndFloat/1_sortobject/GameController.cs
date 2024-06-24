using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace culture.sinkfloat1
{

    public class GameController : GameControllerforAll
    {
        public bool sinking;
        public Transform partent;
        public int reseting;
        public bool experiment;
        public GameObject firstdroptext, seconddroptext;
        protected override void Start()
        {

            foreach (var item in alloption)
            {
                item.lastpos = item.transform.position;
            }
            GameSet();
        }
        public void GameSet()
        {
            if (reloding >= 4)
            {
                StartCoroutine(LevelCompleted());
                return;
            }
            for (int i = 0; i < alloption.Length; i++)
            {
                int randomchar = Random.Range(0, 2);
                int randomno = Random.Range(0, allCharacter[randomchar].sameLetter.Length);

                for (int j   = 0; j < AllAnswerNo.Count; j++)
                {
                    if(AllAnswerNo[j] == int.Parse(allCharacter[randomchar].sameLetter[randomno].letter))
                    {
                        randomchar = Random.Range(0, 2);
                        randomno = Random.Range(0, allCharacter[randomchar].sameLetter.Length);
                        j = -1;
                    }
                }

                AllAnswerNo.Add(int.Parse(allCharacter[randomchar].sameLetter[randomno].letter));
                
                alloption[i].GetComponent<BoxCollider2D>().enabled = true;
                alloption[i].no = allCharacter[randomchar].Letter;
                alloption[i].text.text = allCharacter[randomchar].sameLetter[randomno].name;
                alloption[i].transform.position = alloption[i].lastpos;
                // AllAnswerNo.Add()
                 alloption[i].Icon.sprite = allCharacter[randomchar].sameLetter[randomno].Icon;
            }

        }

        public void GameComplete()
        {
            gamePlay = false;
            StartCoroutine(LevelCompleted());
        }
        public override void CurrectAnswer()
        {

            base.CurrectAnswer();
            selectedoption.Border.color = currect_answer_color;
            foreach (var item in alloption)
            {
                item.StopAllCoroutines();
            }
            StartCoroutine(WaitForCurrectanimtion());
        }

        protected override void CurrectAnimtionCompleted()
        {
            base.CurrectAnimtionCompleted();
            if (!experiment)
            {
                selectedoption.GetComponent<Collider2D>().enabled = false;
                selectedoption = null;
                gamePlay = true;
                if (partent.childCount == 0)
                {
                    gamePlay = false;
                    StartCoroutine(LevelCompleted());
                }
            }
            else
            {
                GameSet();
            }

        }
        public override void WrongAnswer()
        {
            // base.WrongAnswer();
            selectedoption.Border.color = wrong_answer_color;
            StartCoroutine(WaitWrongAnimtion());
        }

        public override void ResetingDrage()
        {
            selectedoption.transform.parent = partent;
            selectedoption.transform.SetAsLastSibling();
            if (selectedoption.TryGetComponent<Drag>(out Drag drag))
            {
                selectedoption.Border.color = drag.defaltcolr;
            }
          //   = selectedoption.TryGetComponent<Drag>().defaltcolr;
            selectedoption = null;
            // base.ResetingDrage();
        }
    }
}