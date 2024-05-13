using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Laguage.SortingImage 
{

    public class GameController : GameControllerforAll
    {

        public int[] Question;
        public TextMeshPro[] allquestion;
        public Sprite Droped;

        protected override void Start()
        {
            GameDataSet();
         //   base.Start();
        }
        public void GameDataSet()
        {
            for (int i = 0; i < Question.Length; i++)
            {
                Question[i] = Random.Range(0, allCharacter.Length);
                allquestion[i].text = allCharacter[Question[i]].Letter;
            }
            for (int i = 0; i < Question.Length; i++)
            {


                for (int j = 0; j < Question.Length; j++)
                {
                    if(Question[i] == Question[j] && i!= j)
                    {
                        Question[j] = Random.Range(0, allCharacter.Length);
                        allquestion[j].text = allCharacter[Question[i]].Letter;
                        j = -1;
                    }

                }
            }

            int k = 0;

            for (int i = 0; i < Question.Length; i++)
            {
                for (int j = 0; j < allCharacter[Question[i]].sameLetter.Length; j++)
                {
                    alloption[k].Icon.sprite = allCharacter[Question[i]].sameLetter[j].Icon;
                    alloption[k].no = i.ToString();
                    k++;
                    Debug.Log(k);
                }

            }

            gamePlay = true;
        }

        public override bool Neartodestination()
        {

            if (Vector3.Distance(selectedoption.transform.position, droping_place[int.Parse(selectedoption.no)].transform.position) < 2.6f)
            {
              //  gamePlay = false;
                Transform tempparent = selectedoption.transform.parent;
                selectedoption.transform.parent = droping_place[int.Parse(selectedoption.no)].transform;

                droping_place[int.Parse(selectedoption.no)].sprite = Droped;
                Debug.Log(tempparent.childCount);
                if(tempparent.childCount == 0)
                {
                    gamePlay = false;
                    Debug.Log("levelComplted");
                    StartCoroutine(LevelCompleted());
                }
                return true;
            }
            else
            {

                for (int i = 0; i < droping_place.Length; i++)
                {
                    if (Vector3.Distance(selectedoption.transform.position, droping_place[i].transform.position) < 2.5f)
                    {
                        gamePlay = false;
                        StartCoroutine(WaitforAnimation(i));
                        return true;
                        //StartCoroutine(WaitWrongAnimtion());
                    }
                }
     
                selectedoption.transform.position = selectedoption.lastpos;
            }

            Debug.Log("selection");
            return false;
            base.Neartodestination();
        }
        public override void WrongAnswer()
        {
           
        }
        IEnumerator WaitforAnimation(int i)
        {
            Transform tempparent = selectedoption.transform.parent;
            selectedoption.transform.parent = droping_place[i].transform;
            selectedoption.transform.position = selectedoption.lastpos;
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            wrongAnswer_animtion.SetActive(false);
            selectedoption.transform.parent = tempparent;
            gamePlay = true;
        }
    }
}
