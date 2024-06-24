using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace culture.Magnetinc
{
    public class GameController : GameControllerforAll
    {
        protected override void Start()
        {
            GameSet();
        }
        int lagreset;
        public void GameSet()
        {

            for (int i = 0; i < alloption.Length; i++)
            {
                int randomchar = Random.Range(0, 2);
                int randomno = Random.Range(0, allCharacter[randomchar].sameLetter.Length);

                for (int j = 0; j < AllAnswerNo.Count; j++)
                {
                    if (AllAnswerNo[j] == int.Parse(allCharacter[randomchar].sameLetter[randomno].letter))
                    {
                        randomchar = Random.Range(0, 2);
                        randomno = Random.Range(0, allCharacter[randomchar].sameLetter.Length);
                       // lagreset++;
                        //if (lagreset > 20)
                        //{
                         
                        //    break;

                        //}
                        j = -1;
                    }
                }
                lagreset = 0;
                AllAnswerNo.Add(int.Parse(allCharacter[randomchar].sameLetter[randomno].letter));
                alloption[i].no = allCharacter[randomchar].Letter;
                alloption[i].text.text = allCharacter[randomchar].sameLetter[randomno].name;
                // AllAnswerNo.Add()
                alloption[i].Icon.sprite = allCharacter[randomchar].sameLetter[randomno].Icon;
            }


        }
        public override bool Neartodestination()
        {

            return base.Neartodestination();

        }

        public override void CurrectAnswer()
        {
            Debug.Log("XXX");
            StartCoroutine(LevelCompleted());
            base.CurrectAnswer();
        }
    }
}
