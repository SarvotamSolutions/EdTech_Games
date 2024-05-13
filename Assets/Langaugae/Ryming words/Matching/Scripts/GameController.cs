using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Laguage.Ryming_words.Matching
{
    public class GameController : GameControllerforAll
    {
       

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
                droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allCharacter[OptionNO[i]].RelatedCharacter[0].Icon;
                droping_place[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = allCharacter[OptionNO[i]].RelatedCharacter[0].name;
                alloption[i].Icon.sprite = allCharacter[OptionNO[i]].sameLetter[0].Icon;
                alloption[i].text.text = allCharacter[OptionNO[i]].sameLetter[0].name;
                alloption[i].GetComponent<Drager>().Answeroption = droping_place[i].gameObject;
            }


            for (int i = 0; i < droping_place.Length; i++)
            {

                droping_place[i].transform.SetSiblingIndex(Random.Range(0, droping_place[i].transform.parent.childCount));

            }
           

        }

        protected override void Update()
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
            StartCoroutine(LevelCompleted());
        }
    
        public IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
    }
}
