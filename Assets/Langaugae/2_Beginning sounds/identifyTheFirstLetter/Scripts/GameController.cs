using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Laguage.beginning_sounds.Idintfyletter
{
    public class GameController : GameControllerforAll 
    {

        public Color TextColor;
        public Sprite currectanswer, wronganswer, defaltanswer;
        public override void GameStart()
        {
            base.GameStart();
            foreach (var item in alloption)
            {
                item.text.text = item.no;
            }

            //gamePlay = true;
        }
        public override bool Neartodestination()
        {
            if (base.Neartodestination())
            {
                SpriteRenderer selection = selectedoption.GetComponent<SpriteRenderer>();
                if (letter == selectedoption.no)
                {
                    selection.sprite = currectanswer;
                    selection.gameObject.transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
                    for(int k = 0; k < droping_place.Length;k++)
                    {
                        droping_place[k].gameObject.SetActive(false);
                    }
                    // arrow.color = currect_answer_color;
                    foreach (var item in droping_place)
                    {
                        item.color = currect_answer_color;
                    }
                    Party_pop.SetActive(true);
                    Party_pop.GetComponent<AudioSource>().PlayDelayed(1);
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
                    selection.gameObject.transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
                    for (int k = 0; k < droping_place.Length; k++)
                    {
                        droping_place[k].gameObject.SetActive(false);
                    }
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
            yield return new WaitForSeconds(4);
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
            selectedoption.gameObject.transform.GetComponentInChildren<TextMeshPro>().color = TextColor;
            for (int k = 0; k < droping_place.Length; k++)
            {
                droping_place[k].gameObject.SetActive(true);
            }
            base.ResetingDrage();

            //arrow.color = Color.white;
            //////////////////////////////////////////////////////////////////////Here
            gamePlay = true;


        }
    }
}