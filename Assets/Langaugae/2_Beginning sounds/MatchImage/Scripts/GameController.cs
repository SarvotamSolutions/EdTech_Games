using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Laguage.beginning_sounds.match_images
{
    public class GameController : GameControllerforAll
    {


        public Transform optionparent;
        public bool rotate;

        public Sprite dropingselected, dropingnotselected;
        public SpriteRenderer dropingoutline;

        public SpriteRenderer question_sprite;

        List<int> alllettersaved = new List<int>();
        public Sprite[] alldirectionanswer, border;

        private void Start()
        {
            StartGame();
        }
        void StartGame()
        {
            reloding++;
            if (reloding > allCharacter.Length)
            {
                StartCoroutine(LevelCompleted());// check so can game will complete
                return;
            }
            int no = 0;



            int answeroption = Random.Range(0, alloption.Length); // chosing the no of the answer option in the multiple option

            int AnswerNumber = Random.Range(0, allCharacter.Length);// chosing the number of the all character  so caracter can not be repeated

            for (int i = 0; i < alllettersaved.Count; i++)
            {
                if (AnswerNumber == alllettersaved[i])
                {
                    AnswerNumber = Random.Range(0, allCharacter.Length);// check the answer nuber is already spawned in prviouus if its spawned it will reset
                    i = -1;
                }

            }
            alllettersaved.Add(AnswerNumber);// adding the answered number so in fuutre can not be spawned again

            for (int i = 0; i < alloption.Length; i++)
            {
                if (i == answeroption)// checing this id is answeroption 
                {
                    // setting this option as an asnwer

                    Drager drag = alloption[i].GetComponent<Drager>();
                    drag.rotionno = 0;
                    if (rotate)
                    {
                        drag.background.transform.localRotation = Quaternion.Euler(0, 0, 0);

                        drag.Border.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        alloption[i].pickup = allCharacter[AnswerNumber].RelatedCharacter[0].Sound;
                    }
                    else
                    {
                        alloption[i].pickup = allCharacter[AnswerNumber].lettersound;
                    }

                    int randomno = Random.Range(0, allCharacter[AnswerNumber].sameLetter.Length);
                    question_sprite.sprite = allCharacter[AnswerNumber].sameLetter[randomno].Icon;
                    lettersound = allCharacter[AnswerNumber].sameLetter[randomno].Sound;
                    letter = allCharacter[AnswerNumber].Letter;

                    drag.no = allCharacter[AnswerNumber].Letter;
                    if (alloption[i].Icon)
                    {
                        drag.Icon.transform.position = drag.transform.GetChild(1).transform.position;
                        alloption[i].Icon.sprite = allCharacter[AnswerNumber].letterSprite;

                    }



                    if (alloption[i].text)
                    {

                        alloption[i].text.text = allCharacter[AnswerNumber].sameLetter[randomno].Name;
                        drag.no = allCharacter[AnswerNumber].sameLetter[randomno].Name;
                        letter = allCharacter[AnswerNumber].sameLetter[randomno].Name;
                    }
                    OptionNO.Add(AnswerNumber);
                }
                else// if its not an answeroption means all is wrong answer
                {
                    Drager drag = alloption[i].GetComponent<Drager>();
                    int rotaionno = 0;

                    no = Random.Range(0, allCharacter.Length);//selecting the random number of the all character
                                                              // option can not be repeated again
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

                        no = Random.Range(0, allCharacter.Length);

                    }

                    if (rotate)// some scence there option need to rotate in random
                    {
                        rotaionno = Random.Range(1, alldirectionanswer.Length);
                        drag.rotionno = rotaionno;
                        drag.Border.transform.localRotation = Quaternion.Euler(0, 0, rotaionno * 90);
                        alloption[i].pickup = allCharacter[no].RelatedCharacter[0].Sound;
                    }
                    else
                    {
                        alloption[i].pickup = allCharacter[no].lettersound;
                    }
                    // setting the icon image
                    if (drag.Icon)
                    {
                        if (!rotate)
                            drag.Icon.transform.position = drag.transform.GetChild(1).transform.position;
                        else
                        {
                            drag.Icon.transform.position = drag.Border.transform.GetChild(0).transform.position;
                        }

                        Debug.Log("setting the Icom image to to leeter sprite image from the all character");
                        alloption[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allCharacter[no].letterSprite;
                    }
                    drag.no = allCharacter[no].Letter;
                    Debug.Log("letter is " + drag.no);
                    if (alloption[i].text)
                    {
                        alloption[i].text.text = allCharacter[no].sameLetter[0].Name;
                        drag.no = allCharacter[no].sameLetter[0].Name;
                    }
                    OptionNO.Add(no);
                }
            }
            gamePlay = true;
        }

        public bool Neartodestination(GameObject obj)
        {

            if (Vector3.Distance(obj.transform.position, droping_place[0].transform.position) < 2.5f)
            {
                droping_place[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = dropingselected;
                return true;
            }
            dropingoutline.color = Color.white;
            droping_place[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = dropingnotselected;
            return false;
        }
        public bool icon_Colors_Change;

        public override void CurrectAnswer()
        {
            if (icon_Colors_Change)
                selectedoption.background.color = currect_answer_color;
            droping_place[1].sprite = currect_answer[1];
            StartCoroutine(RelodingThe_Level());
            base.CurrectAnswer();
        }
        IEnumerator RelodingThe_Level()
        {
            Party_pop.SetActive(true);
            Party_pop.GetComponent<AudioSource>().PlayDelayed(1);
            Color tempcolr = Color.white;
            if (selectedoption.text)
            {
                tempcolr = selectedoption.text.color;
                selectedoption.text.color = Color.white;
            }

            yield return new WaitForSeconds(CorrectAnswer_delayTime + 1);

            if (selectedoption.text)
            {
                selectedoption.text.color = tempcolr;
            }
            if (!rotate)
                selectedoption.Border.sprite = Normal_answer[0];
            droping_place[1].sprite = Normal_answer[1];
            droping_place[0].enabled = true;
            if (icon_Colors_Change)
            {
                selectedoption.background.color = sellect_answer_color;
                selectedoption.Icon.color = selectedoption.GetComponent<Drager>().imagecolor;
            }
            Party_pop.SetActive(false);
            Drager drage = droping_place[0].transform.GetComponentInChildren<Drager>();

            selectedoption.Border.color = Color.white;
            drage.transform.parent = optionparent;
            drage.transform.localScale = Vector3.one;
            drage.transform.position = drage.lastpos;
            droping_place[0].color = Color.white;
            dropingoutline.color = Color.white;
            OptionNO.Clear();
            StartGame();

        }
        IEnumerator LevelCompleted()
        {
            gamePlay = false;
            gameCompleted_animation.SetActive(true);
            gameCompleted_animation.GetComponent<AudioSource>().PlayDelayed(1);
            yield return new WaitForSeconds(levelcompletedelayTime + 1);
            SceneManager.LoadScene(0);

        }
        IEnumerator WaitForReseting()
        {
            Drager drage = droping_place[0].transform.GetComponentInChildren<Drager>();
            Color tempcolr = Color.white;
            if (selectedoption.text)
            {
                tempcolr = selectedoption.text.color;
                selectedoption.text.color = Color.white;
            }
            droping_place[1].sprite = wrong_answer[1];
            Debug.Log("WRONG ANSWER");
            wrongAnswer_animtion.SetActive(true);
            wrongAnswer_animtion.GetComponent<AudioSource>().PlayDelayed(1f);
            yield return new WaitForSeconds(WrongAnswer_delayTime + 1);

            if (selectedoption.text)
            {
                selectedoption.text.color = tempcolr;
            }
            if (!rotate)
                selectedoption.Border.sprite = Normal_answer[0];
            droping_place[1].sprite = Normal_answer[1];
            droping_place[0].enabled = true;
            if (icon_Colors_Change)
            {
                selectedoption.background.color = sellect_answer_color;

                selectedoption.Icon.color = selectedoption.GetComponent<Drager>().imagecolor;
            }
            selectedoption.Border.color = Color.white;
            gamePlay = true;
            wrongAnswer_animtion.SetActive(false);

            drage.transform.parent = optionparent;
            drage.transform.position = drage.lastpos;
            drage.transform.localScale = Vector3.one;
            dropingoutline.color = Color.white;
            gamePlay = true;
        }
        public override void WrongAnswer()
        {
            base.WrongAnswer();
            Debug.Log(selectedoption.Border.color);
            dropingoutline.color = wrong_answer_color;
            if (icon_Colors_Change)
                selectedoption.background.color = wrong_answer_color;

            StartCoroutine(WaitForReseting());
        }
    }
}