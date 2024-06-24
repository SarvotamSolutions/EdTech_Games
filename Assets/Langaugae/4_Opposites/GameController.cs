using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Laguage.Oposite.draganddrop
{
    public class GameController : GameControllerforAll
    {
        List<int> alllettersaved = new List<int>();
        public Transform OptionParent;
        public SpriteRenderer outline;
        public override void GameStart()
        {
            StartGame();
            OpstionSet();

        }

        void StartGame()
        {
            reloding++;
            if (reloding > allCharacter.Length)
            {
                StartCoroutine(LevelCompleted());
                return;
            }
            int no = 0;



            int answeroption = Random.Range(0, alloption.Length);
            int AnswerNumber = Random.Range(0, allCharacter.Length);

            for (int i = 0; i < alllettersaved.Count; i++)
            {
                if (AnswerNumber == alllettersaved[i])
                {
                    AnswerNumber = Random.Range(0, allCharacter.Length);
                    i = -1;
                }

            }
            alllettersaved.Add(AnswerNumber);
            //allno.Add(no);
            // allno.Add(no);
            for (int i = 0; i < alloption.Length; i++)
            {
                if (i == answeroption)
                {
                    //no = Random.Range(0, allcharacter.Length);

                    //for (int j = 0; j < allno.Count; j++)
                    //{
                    //    if (no == allno[j])
                    //    {
                    //        no = Random.Range(0, allcharacter.Length);
                    //        j = -1;
                    //    }
                    //}
                    // int rotaionno = Random.Range(1, alldirectionanswer.Length);
                    Drag drag = alloption[i].GetComponent<Drag>();
                  //  drag.rotionno = 0;
                  //  drag.background.transform.localRotation = Quaternion.Euler(0, 0, 0);
                   // drag.Border.transform.localRotation = Quaternion.Euler(0, 0, 0);
                 //   drag.Icon.transform.position = drag.background.GetChild(0).transform.position;
                    Icon.sprite = allCharacter[AnswerNumber].sameLetter[Random.Range(0, allCharacter[AnswerNumber].sameLetter.Length)].Icon;
                    Boarder.color = allCharacter[AnswerNumber].hintcolor;
                    letter = allCharacter[AnswerNumber].Letter;

                    //  alloption[i].GetComponent<SpriteRenderer>().sprite = alldirectionanswer[0];
                    // alloption[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = border[0];
                    alloption[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allCharacter[AnswerNumber].letterSprite;
                    drag.no = allCharacter[AnswerNumber].Letter;
                    drag.Icon_Colors = allCharacter[AnswerNumber].hintcolor;
                    alloption[i].Border.color = allCharacter[AnswerNumber].hintcolor;
                    OptionNO.Add(AnswerNumber);
                }
                else
                {
                   // int rotaionno = Random.Range(1, alldirectionanswer.Length);
                    // alloption[i].GetComponent<SpriteRenderer>().sprite = alldirectionanswer[rotaionno];
                    // alloption[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = border[rotaionno];
                    Drag drag = alloption[i].GetComponent<Drag>();
                   // drag.rotionno = rotaionno;
                  //  drag.background.transform.localRotation = Quaternion.Euler(0, 0, rotaionno * 90);
                  //  drag.Border.transform.localRotation = Quaternion.Euler(0, 0, rotaionno * 90);
                  //  drag.Icon.transform.position = drag.background.GetChild(0).transform.position;
                    no = Random.Range(0, allCharacter.Length);
                    for (int j = 0; j < OptionNO.Count; j++)
                    {
                        if (no == OptionNO[j] || no == AnswerNumber)
                        {
                            no = Random.Range(0, allCharacter.Length);
                            j = -1;
                        }


                    }
                    while (no == AnswerNumber)
                    {
                        Debug.Log("sameName");
                        no = Random.Range(0, allCharacter.Length);
                        //j = -1;
                    }
                    Debug.Log(no + "option" + AnswerNumber);
                    alloption[i].Border.color = allCharacter[no].hintcolor;
                    alloption[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allCharacter[no].letterSprite;
                    drag.no = allCharacter[no].Letter;
                    drag.Icon_Colors = allCharacter[no].hintcolor;
                    OptionNO.Add(no);
                }



            }
            //foreach (var item in alloption)
            //{
            //    no = Random.Range(0, allcharacter.Length);
            //    for (int i = 0; i < allno.Count; i++)
            //    {
            //        if(no == allno[i])
            //        {
            //            no = Random.Range(0, allcharacter.Length);
            //            i = -1;
            //        }
            //    }
            //    item.GetChild(0).GetComponent<SpriteRenderer>().sprite = allcharacter[no].relatedsdprite;
            //    item.GetComponent<Drager>().no = allcharacter[no].letter;
            //    allno.Add(no);

            //}


            gamePlay = true;
        }

        public void OpstionSet()
        {
            for (int i = 0; i < OptionNO.Count; i++)
            {
                alloption[i].Icon.sprite = allCharacter[OptionNO[i]].letterSprite;
            }
        }
        public override void CurrectAnswer()
        {

            outline.color = currect_answer_color;
            droping_place[0].color = currect_answer_color;
            StartCoroutine(RelodingThe_Level());
            base.CurrectAnswer();
        }
        //protected override void CurrectAnswer()
        //{
        //    dropingoutline.color = currect_answer_color;
        //    droping_place.color = currect_answer_color;
        //    StartCoroutine(RelodingThe_Level());
        //}
        IEnumerator RelodingThe_Level()
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);

            Party_pop.SetActive(false);
            Drag drage = droping_place[0].transform.GetComponentInChildren<Drag>();
            Debug.Log("RELODING");
            selectedoption.Border.color = drage.Icon_Colors;
            drage.transform.parent = OptionParent;
            drage.transform.localScale = Vector3.one;
            drage.transform.position = drage.lastpos;
            droping_place[0].color = Color.white;
            outline.color = new Color(1, 1, 1, .75f);
            
            OptionNO.Clear();
            StartGame();

        }

        IEnumerator WaitForReseting()
        {
            Drag drage = droping_place[0].transform.GetComponentInChildren<Drag>();
            drage.Border.color = wrong_answer_color;
            Debug.Log("WRONG ANSWER");
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            selectedoption.Border.color = drage.Icon_Colors;
            gamePlay = true;
            wrongAnswer_animtion.SetActive(false);

            //  drage.GetComponent<SpriteRenderer>().sprite = alldirectionanswer[drage.rotionno];
            //  drage.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = border[drage.rotionno];

            drage.transform.parent = OptionParent;
            drage.transform.position = drage.lastpos;
            drage.transform.localScale = Vector3.one;
            //   drage.Border.color = Color.white;
            //  droping_place[0].color = Color.white;
            outline.color = new Color(1, 1, 1, .75f);
            gamePlay = true;
        }
        public override void WrongAnswer()
        {
            base.WrongAnswer();
            Debug.Log(selectedoption.Border.color);
            outline.color = wrong_answer_color;
            // droping_place[0].color = wrong_answer_color;

            StartCoroutine(WaitForReseting());
        }

    }

}