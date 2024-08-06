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
        public Sprite currectanswer, wronganswer, defaltanswer;
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
            for (int i = 0; i < alloption.Length; i++)
            {
                if (i == answeroption)
                {
                    Drag drag = alloption[i].GetComponent<Drag>();
                    int Randomno = Random.Range(0, allCharacter[AnswerNumber].sameLetter.Length);
                    drag.pickup = allCharacter[AnswerNumber].RelatedCharacter[0].Sound;

                    Icon.sprite = allCharacter[AnswerNumber].sameLetter[Randomno].Icon;
                    Boarder.color = allCharacter[AnswerNumber].hintcolor;
                    letter = allCharacter[AnswerNumber].Letter;
                    lettersound = allCharacter[AnswerNumber].sameLetter[Randomno].Sound;
                    alloption[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allCharacter[AnswerNumber].letterSprite;
                    drag.no = allCharacter[AnswerNumber].Letter;
                    drag.Icon_Colors = allCharacter[AnswerNumber].hintcolor;
                    alloption[i].Border.color = allCharacter[AnswerNumber].hintcolor;
                    OptionNO.Add(AnswerNumber);
                }
                else
                {
                    Drag drag = alloption[i].GetComponent<Drag>();
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
                    }
                    Debug.Log(no + "option" + AnswerNumber);
                    drag.pickup = allCharacter[no].RelatedCharacter[0].Sound;
                    alloption[i].Border.color = allCharacter[no].hintcolor;
                    alloption[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allCharacter[no].letterSprite;
                    drag.no = allCharacter[no].Letter;
                    drag.Icon_Colors = allCharacter[no].hintcolor;
                    OptionNO.Add(no);
                }
            }
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

            outline.sprite = currectanswer;
            droping_place[0].color = currect_answer_color;
            StartCoroutine(RelodingThe_Level());
            base.CurrectAnswer();
        }
        IEnumerator RelodingThe_Level()
        {
            Party_pop.SetActive(true);
            Party_pop.GetComponent<AudioSource>().PlayDelayed(1);
            yield return new WaitForSeconds(CorrectAnswer_delayTime + 1);
            outline.sprite = defaltanswer;
            Party_pop.SetActive(false);
            Drag drage = droping_place[0].transform.GetComponentInChildren<Drag>();
            Debug.Log("RELODING");
            selectedoption.Border.color = drage.Icon_Colors;
            drage.transform.parent = OptionParent;
            drage.transform.localScale = Vector3.one;
            drage.transform.position = drage.lastpos;
            droping_place[0].color = Color.white;
            outline.color = new Color(1, 1, 1, 1f);

            OptionNO.Clear();
            StartGame();

        }

        IEnumerator WaitForReseting()
        {
            Drag drage = droping_place[0].transform.GetComponentInChildren<Drag>();
            drage.Border.color = wrong_answer_color;
            Debug.Log("WRONG ANSWER");
            wrongAnswer_animtion.SetActive(true);
            wrongAnswer_animtion.GetComponent<AudioSource>().PlayDelayed(1);
            yield return new WaitForSeconds(WrongAnswer_delayTime + 1);
            outline.sprite = defaltanswer;
            selectedoption.Border.color = drage.Icon_Colors;
            gamePlay = true;
            wrongAnswer_animtion.SetActive(false);
            drage.transform.parent = OptionParent;
            drage.transform.position = drage.lastpos;
            drage.transform.localScale = Vector3.one;
            outline.color = new Color(1, 1, 1, 1f);
            gamePlay = true;
        }
        public override void WrongAnswer()
        {
            base.WrongAnswer();
            Debug.Log(selectedoption.Border.color);
            outline.sprite = wronganswer;
            StartCoroutine(WaitForReseting());
        }
    }
}