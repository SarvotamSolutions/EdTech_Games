using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace culture.countries.draganddrop
{
    public class GameController : GameControllerforAll
    {
        public Sprite currectanswer, wronganswer, defaltanswer;
        public int temp_int;

        protected override void Start()
        {
           
        }
        public override void CurrectAnswer()
        {
            if (currectanswer)
            {
                selectedoption.background.sprite = currectanswer;
                selectedoption.text.color = currect_answer_color;
            }
            else
            {
                selectedoption.Border.color = currect_answer_color;
                selectedoption.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            }
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
            {
                selectedoption.background.sprite = wronganswer;
                selectedoption.text.color = wrong_answer_color;
            }
            else
            {
                selectedoption.Border.color = wrong_answer_color;
                selectedoption.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            }

            base.WrongAnswer();

            StartCoroutine(WaitWrongAnimtion());
        }
        public override void ResetingDrage()
        {
            if (currectanswer)
            {
                selectedoption.background.sprite = defaltanswer;
                selectedoption.text.color = sellect_answer_color;
            }
            else
                selectedoption.Border.color = Color.white;
            droping_place[temp_int].color = Color.white;
            selectedoption.transform.position = selectedoption.lastpos;
            selectedoption.transform.localScale = new Vector3(1,1,1);

            gamePlay = true;
        }
    }
}