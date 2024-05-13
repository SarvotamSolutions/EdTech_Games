using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Laguage.beginning_sounds.Idintfyletter
{
    public class GameController : GameControllerforAll 
    {
        public SpriteRenderer arrow;
        public Sprite currectanswer, wronganswer, defaltanswer;
        public override void GameStart()
        {
            base.GameStart();
            foreach (var item in alloption)
            {
                item.text.text = item.no;
            }
            gamePlay = true;
        }
        public override bool Neartodestination()
        {
            if (base.Neartodestination())
            {
                SpriteRenderer selection = selectedoption.GetComponent<SpriteRenderer>();
                if (letter == selectedoption.no)
                {
                    selection.sprite = currectanswer;
                    // arrow.color = currect_answer_color;
                    foreach (var item in droping_place)
                    {
                        item.color = currect_answer_color;
                    }
                    Party_pop.SetActive(true);
                    StartCoroutine(WaitCurrectAnswer());
                }
                else
                {
                    gamePlay = false;
                    foreach (var item in droping_place)
                    {
                        item.color = wrong_answer_color;
                    }
                    selection.sprite = wronganswer;
                    //arrow.color = wrong_answer_color;
                    StartCoroutine(WaitWrongAnimtion());
                }
                return true;
            }
            return false;
            //return base.Neartodestination();

        }

        
        IEnumerator WaitCurrectAnswer()
        {
            gamePlay = false;
            yield return new WaitForSeconds(3);
            Party_pop.SetActive(false);
            CurrectAnswer();
        }

        public override void CurrectAnswer()
        {
            
            foreach (var item in droping_place)
            {
                item.color =Color.white;
            }
            ResetingDrage();
            GameStart();
        }

        public override void ResetingDrage()
        {
            selectedoption.GetComponent<SpriteRenderer>().sprite = defaltanswer;
            base.ResetingDrage();

            arrow.color = Color.white;
            gamePlay = true;


        }
    }
}