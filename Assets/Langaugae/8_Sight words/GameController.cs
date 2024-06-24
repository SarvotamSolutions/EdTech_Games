using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Laguage.sightWords
{
    public class GameController : GameControllerforAll
    {
        public GameObject[] allworlds;
        public Sprite currectanswerbutton, wronganswerbutton, defaltanswerbutton;

        public override void GameStart()
        {
            base.GameStart();
            Icon.sprite = allCharacter[reloding - 1].letterSprite;

        }
        public override bool Neartodestination()
        {
            if (Vector3.Distance(selectedoption.transform.position, droping_place[reloding-1].transform.position) < distangedrage)
            {
                gamePlay = false;
                if (selectedoption.no == allCharacter[reloding-1].Letter)
                {
                    selectedoption.background.sprite = currectanswerbutton;
                    selectedoption.transform.position = droping_place[reloding - 1].transform.position;
                    StartCoroutine(WaitForCurrectanimtion());
                    CurrectAnswer();
                    //currect answer
                }
                else
                {
                    selectedoption.background.sprite = wronganswerbutton;
                    selectedoption.transform.position = droping_place[reloding - 1].transform.position;
                    StartCoroutine(WaitWrongAnimtion());
                    WrongAnswer();
                   //wrong answer
                }

                return true;

            }


            return false;
        }

        public override void ResetingDrage()
        {
            selectedoption.background.sprite = defaltanswerbutton;
            base.ResetingDrage();

            gamePlay = true;
        }
        protected override void CurrectAnimtionCompleted()
        {
            gamePlay = true;
            selectedoption.gameObject.SetActive(false);
            GameStart();
            allworlds[reloding - 1].SetActive(true);
            allworlds[reloding - 2].SetActive(false);
            base.CurrectAnimtionCompleted();

        }

        public override void CurrectAnswer()
        {
            
            base.CurrectAnswer();

           
        }
    }

    



}