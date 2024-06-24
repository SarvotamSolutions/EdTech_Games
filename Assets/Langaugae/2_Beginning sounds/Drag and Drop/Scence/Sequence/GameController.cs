using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laguage.Sequence
{
    public class GameController : GameControllerforAll
    {
        public Sprite SelectedDrop;
        public Sprite defaltspritre, wrongsprite, currectsprite;

        private void Start()
        {

        }

        public override bool Neartodestination()
        {
            if (Vector3.Distance(selectedoption.transform.position, droping_place[reloding].transform.position) <distangedrage )
            {
                selectedoption.transform.position = droping_place[reloding].transform.position;
                return true;
            }

            return false;
        }
        public override void CurrectAnswer()
        {
            droping_place[reloding - 1].enabled = false;
            StartCoroutine(WaitForCurrectanimtion());
            selectedoption.GetComponent<Collider2D>().enabled = false;
          
            if (defaltspritre)
            {
                selectedoption.background.sprite = currectsprite;

            }
            
            if (reloding > droping_place.Length-1)
            {
                StartCoroutine(LevelCompleted());
            }
            else
            {
                droping_place[reloding].sprite = SelectedDrop;
            }
            
            base.CurrectAnswer();
        }
        public override void WrongAnswer()
        {
            if (defaltspritre)
            {
                selectedoption.background.sprite = wrongsprite;

            }

            StartCoroutine(WaitWrongAnimtion());
        }
        public override void ResetingDrage()
        {
            if (defaltspritre)
            {
                selectedoption.background.sprite = defaltspritre;

            }
            selectedoption.Border.color = Color.white;
            gamePlay = true;
            base.ResetingDrage();

        }
    }

}