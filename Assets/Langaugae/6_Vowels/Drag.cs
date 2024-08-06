using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Laguage.vowels
{
    public class Drag : DragerForall
    {
        public GameCOntroller controler;


        protected override void OnMouseDown()
        {
            if (!GameCOntroller.Instance.gamePlay)
                return;
            sound.clip = pickup;
            sound.Play();
            base.OnMouseDown();
        }
        protected override void Update()
        {
            base.Update();

            if (GameCOntroller.Instance.selectedoption && GameCOntroller.Instance.Neartodestination())
            {

            }
        }
        protected override void OnMouseUp()
        {
            if (!GameCOntroller.Instance.gamePlay || !clicked)
                return;

            base.OnMouseUp();


            if (GameCOntroller.Instance.Neartodestination())
            {
                sound.clip = drop;
                sound.Play();
                controler.question_text.text = text.text;
                letterSoundClip.clip = controler.lettersound;
                if (controler.allleter)
                {
                    if (controler.blankno == 2)
                        letterSoundClip.PlayDelayed(.2f);

                }
                else
                    letterSoundClip.PlayDelayed(.2f);
                background.enabled = false;
                text.enabled = false;

                char[] letes = GameCOntroller.Instance.selectedCharacter.sameLetter[controler.letterno].Name.ToCharArray();

                Debug.Log(letes[controler.blankno]);
                transform.position = GameCOntroller.Instance.droping_place[controler.blankno].transform.position;
                if (no == letes[controler.blankno].ToString())
                {
                    Border.color = GameCOntroller.Instance.currect_answer_color;
                    controler.CurrectAnswer();

                }
                else
                {
                    Border.color = GameCOntroller.Instance.wrong_answer_color;
                    GameCOntroller.Instance.WrongAnswer();
                }
            }
            else
            {
                transform.position = lastpos;
                Border.color = controler.darkwhite;
            }
        }
    }
}