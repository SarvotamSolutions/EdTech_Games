using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace culture.countries.draganddrop
{
    public class GameController : GameControllerforAll
    {
        public Sprite currectanswer, wronganswer, defaltanswer;

        protected override void Start()
        {
           
        }
        public override void CurrectAnswer()
        {
            if (currectanswer)
                selectedoption.background.sprite = currectanswer;
            else
                selectedoption.Border.color = currect_answer_color;
            base.CurrectAnswer();

            StartCoroutine(WaitForCurrectanimtion());
        }
        protected override void CurrectAnimtionCompleted()
        {
            base.CurrectAnimtionCompleted();
            reloding++;
            if (reloding >= droping_place.Length)
                StartCoroutine(LevelCompleted());
            gamePlay = true;
        }
        public override void WrongAnswer()
        {
            if (currectanswer)
                selectedoption.background.sprite = wronganswer;
            else
                selectedoption.Border.color = wrong_answer_color;
            base.WrongAnswer();

            StartCoroutine(WaitWrongAnimtion());
        }
        public override void ResetingDrage()
        {
            if (currectanswer)
                selectedoption.background.sprite = defaltanswer;
            else
                selectedoption.Border.color = Color.white;
            selectedoption.transform.position = selectedoption.lastpos;
            gamePlay = true;
        }
    }
}