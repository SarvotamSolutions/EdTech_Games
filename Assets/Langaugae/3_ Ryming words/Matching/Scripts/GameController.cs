using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Laguage.Ryming_words.Matching
{
    public class GameController : GameControllerforAll
    {
        public Color normalcolor;//insde text color
        public int set;
        public void Start()
        {
            
            GameSet();
           
        }
        public void GameSet()
        {
            




            for (int i = 0; i < alloption.Length; i++)
            {
                int no = Random.Range(0, allCharacter.Length);

                for (int j = 0; j < OptionNO.Count; j++)
                {
                    if(no == OptionNO[j])
                    {
                        no = Random.Range(0, allCharacter.Length);
                        j = -1;
                    }
                }
                OptionNO.Add(no);

                droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allCharacter[OptionNO[OptionNO.Count - 1]].RelatedCharacter[0].Icon;
                droping_place[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = allCharacter[OptionNO[OptionNO.Count - 1]].RelatedCharacter[0].Name;
                alloption[i].Icon.sprite = allCharacter[OptionNO[OptionNO.Count-1]].sameLetter[0].Icon;
                alloption[i].text.text = allCharacter[OptionNO[OptionNO.Count-1]].sameLetter[0].Name;
                alloption[i].GetComponent<Drager>().Answeroption = droping_place[i].gameObject;
                alloption[i].drop = allCharacter[OptionNO[OptionNO.Count-1]].RelatedCharacter[0].Sound;
                alloption[i].lettersound = allCharacter[OptionNO[OptionNO.Count-1]].sameLetter[0].Sound;
            }


            for (int i = 0; i < droping_place.Length; i++)
            {

                droping_place[i].transform.SetSiblingIndex(Random.Range(0, droping_place[i].transform.parent.childCount));

            }
           

        }
        public IEnumerator reseting()
        {
            Party_pop.SetActive(true);
            Party_pop.GetComponent<AudioSource>().PlayDelayed(1);
            yield return new WaitForSeconds(CorrectAnswer_delayTime+1);
            Party_pop.SetActive(false);
            for (int i = 0; i < alloption.Length; i++)
            {
                Drager drager = alloption[i].GetComponent<Drager>();
                drager.answered = false;
                drager.text.color = normalcolor;
                droping_place[i].transform.GetChild(1).GetComponent<TextMeshPro>().color = normalcolor;

                //seting arrwo to invicisble
                SpriteRenderer arowrender = drager.arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();
                Color tmp = arowrender.color;
                tmp.a = 0f;
                arowrender.color = tmp;


                //seting border
                drager.Border.color = drager.tempcolr;
                drager.GetComponent<SpriteRenderer>().color = Color.white;
                droping_place[i].color = Color.white;
                droping_place[i].transform.GetChild(3).GetComponent<SpriteRenderer>().color = drager.tempColor2;
                // Answeroption.transform.GetChild(0).gameObject.SetActive(true);
                drager.line.SetPosition(1, drager.line.GetPosition(0));
                gamePlay = true;

            }

            GameSet();
        }
        protected  void Update()
        {
            // base.Update();
           
            if (gamePlay == false)
                return;
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            if (selected_Line)
            {

                selected_Line.SetPosition(1, pos);
            }

            
        }
        public override void CurrectAnswer()
        {
            set += 4;
            if (set >= maxloding)
            {

                StartCoroutine(LevelCompleted());
            }
            else
            {
                StartCoroutine(reseting());
              //  reseting();
             //   GameSet();
            }
        }
    
        public IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            gameCompleted_animation.GetComponent<AudioSource>().PlayDelayed(1);
            yield return new WaitForSeconds(levelcompletedelayTime+1);
            SceneManager.LoadScene(0);

        }
    }
}
