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
       // public AllCharacter[] allcharacter;
       // public Transform[] alloption;
     //   public SpriteRenderer droping_place;
        public Sprite dropingselected, dropingnotselected;
        public SpriteRenderer dropingoutline;
     //   public string lettter;
        public SpriteRenderer question_sprite;
    //    List<int> allno = new List<int>();//option no
        List<int> alllettersaved = new List<int>();
        public Sprite[] alldirectionanswer,border;

     //   public int reloding;
     //   [Space(10)]
     ///   public GameObject gameCompleted_animation;
     //   public GameObject wrongAnswer_animtion;
     //   public GameObject Party_pop;
        private void Start()
        {
            StartGame();
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
                if(AnswerNumber == alllettersaved[i])
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
                if(i == answeroption)
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
                    Drager drag = alloption[i].GetComponent<Drager>();
                    drag.rotionno = 0;
                    if (rotate)
                    {
                        drag.background.transform.localRotation = Quaternion.Euler(0, 0, 0);

                        drag.Border.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                    
                    int randomno = Random.Range(0, allCharacter[AnswerNumber].sameLetter.Length);
                    question_sprite.sprite = allCharacter[AnswerNumber].sameLetter[randomno].Icon;

                    letter = allCharacter[AnswerNumber].Letter;

                    //  alloption[i].GetComponent<SpriteRenderer>().sprite = alldirectionanswer[0];
                    // alloption[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = border[0];
                    drag.no = allCharacter[AnswerNumber].Letter;
                    if (alloption[i].Icon)
                    {
                        drag.Icon.transform.position = drag.background.transform.GetChild(0).transform.position;
                        alloption[i].Icon.sprite = allCharacter[AnswerNumber].letterSprite;
                    }
                    if (alloption[i].text)
                    {
                        alloption[i].text.text = allCharacter[AnswerNumber].sameLetter[randomno].name;
                        drag.no = allCharacter[AnswerNumber].sameLetter[randomno].name;
                        letter = allCharacter[AnswerNumber].sameLetter[randomno].name;
                    }
                        OptionNO.Add(AnswerNumber);
                }
                else
                {
                    Drager drag = alloption[i].GetComponent<Drager>();
                    int rotaionno = 0;
                    if (rotate)
                    {
                        rotaionno = Random.Range(1, alldirectionanswer.Length);
                        drag.rotionno = rotaionno;
                    }
                   // alloption[i].GetComponent<SpriteRenderer>().sprite = alldirectionanswer[rotaionno];
                   // alloption[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = border[rotaionno];
                    if (rotate)
                    {
                        drag.background.transform.localRotation = Quaternion.Euler(0, 0, rotaionno * 90);
                        drag.Border.transform.localRotation = Quaternion.Euler(0, 0, rotaionno * 90);
                    }
                   
                    no = Random.Range(0, allCharacter.Length);
                    for (int j  = 0; j < OptionNO.Count; j++)
                    {
                        if (no == OptionNO[j]  || no == AnswerNumber)
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
                    Debug.Log(no + "option" +AnswerNumber);
                    if (drag.Icon)
                    {
                        drag.Icon.transform.position = drag.background.transform.GetChild(0).transform.position;
                        alloption[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allCharacter[no].letterSprite;
                    }
                    drag.no = allCharacter[no].Letter;
                    if (alloption[i].text)
                    {
                        alloption[i].text.text = allCharacter[no].sameLetter[0].name;
                        drag.no = allCharacter[no].sameLetter[0].name;
                    }
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


        public bool Neartodestination(GameObject obj)
        {
          
            if (Vector3.Distance(obj.transform.position, droping_place[0].transform.position) < 2.5f)
            {
                droping_place[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = dropingselected;
               // droping_place.color = Color.black;
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
            dropingoutline.color = currect_answer_color;
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
            if (icon_Colors_Change)
                selectedoption.background.color = sellect_answer_color;
            Party_pop.SetActive(false);
            Drager drage = droping_place[0].transform.GetComponentInChildren<Drager>();
            Debug.Log("RELODING");
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
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        IEnumerator WaitForReseting()
        {
            Drager drage = droping_place[0].transform.GetComponentInChildren<Drager>();
            drage.Border.color = wrong_answer_color;
            Debug.Log("WRONG ANSWER");
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            if (icon_Colors_Change)
                selectedoption.background.color = sellect_answer_color;
            selectedoption.Border.color = Color.white;
            gamePlay = true;
            wrongAnswer_animtion.SetActive(false);

          //  drage.GetComponent<SpriteRenderer>().sprite = alldirectionanswer[drage.rotionno];
          //  drage.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = border[drage.rotionno];

            drage.transform.parent = optionparent;
            drage.transform.position = drage.lastpos;
            drage.transform.localScale = Vector3.one;
         //   drage.Border.color = Color.white;
            //  droping_place[0].color = Color.white;
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
            // droping_place[0].color = wrong_answer_color;

            StartCoroutine(WaitForReseting());
        }


    }
}
