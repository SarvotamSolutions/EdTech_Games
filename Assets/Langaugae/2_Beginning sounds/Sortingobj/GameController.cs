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
        public Transform option_Parent;
        public Sprite currectbutton, wrongbutton;
        public Sprite[] Normalbutton;
        protected override void Start()
        {
            GameDataSet();
        }
        public void GameDataSet()
        {
            reloding++;
            for (int i = 0; i < Question.Length; i++)
            {
                Question[i] = Random.Range(0, allCharacter.Length);
                for (int j = 0; j < AllAnswerNo.Count; j++)
                {
                    if (Question[i] == AllAnswerNo[j] && AllAnswerNo.Count < 26)
                    {
                        Question[i] = Random.Range(0, allCharacter.Length);
                        j = -1;
                    }

                }

                for (int j = 0; j < Question.Length; j++)
                {
                    if (Question[i] == Question[j] && i != j && AllAnswerNo.Count >= 26)
                    {
                        Question[j] = Random.Range(0, allCharacter.Length);
                        allquestion[j].text = allCharacter[Question[i]].Letter;
                        j = -1;
                    }
                }
                AllAnswerNo.Add(Question[i]);
                allquestion[i].text = allCharacter[Question[i]].Letter;

            }
            foreach (var item in alloption)
            {
                item.GetComponent<BoxCollider2D>().enabled = true;
            }
            int k = 0;

            for (int i = 0; i < Question.Length; i++)
            {
                for (int j = 0; j < allCharacter[Question[i]].sameLetter.Length; j++)
                {
                    alloption[k].Icon.sprite = allCharacter[Question[i]].sameLetter[j].Icon;
                    alloption[k].pickup = allCharacter[Question[i]].sameLetter[j].Sound;
                    alloption[k].no = i.ToString();
                    alloption[k].transform.SetSiblingIndex(Random.Range(0, option_Parent.childCount));
                    k++;
                }

            }

            gamePlay = true;
        }
        IEnumerator StepComplete()
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            Party_pop.SetActive(false);
            for (int i = 0; i < droping_place.Length; i++)
            {
                for (int j = droping_place[i].transform.childCount - 1; j > 0; j--)
                {
                    GameObject temp = droping_place[i].transform.GetChild(j).transform.GetChild(0).gameObject;
                    droping_place[i].transform.GetChild(j).transform.GetChild(0).SetParent(option_Parent);
                    temp.transform.position = Vector3.zero;
                    droping_place[i].transform.GetChild(j).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                }
                droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Normalbutton[i];
            }
            GameDataSet();
        }
        public int maxholder;
        public override bool Neartodestination()
        {

            if (Vector3.Distance(selectedoption.transform.position, droping_place[int.Parse(selectedoption.no)].transform.position) < distangedrage)
            {

                //selectedoption.transform.parent = droping_place[int.Parse(selectedoption.no)].transform;
                for (int c = 1; c < droping_place[int.Parse(selectedoption.no)].transform.childCount; c++)
                {
                    if (droping_place[int.Parse(selectedoption.no)].transform.GetChild(c).transform.childCount == 0)
                    {

                        selectedoption.transform.parent = droping_place[int.Parse(selectedoption.no)].transform.GetChild(c).transform;
                        droping_place[int.Parse(selectedoption.no)].transform.GetChild(c).transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                        selectedoption.transform.localPosition = Vector3.zero;

                        break;
                    }
                }
                if (selectedoption.transform.parent.transform.parent.transform.GetChild(4).transform.childCount != 0)
                {
                    selectedoption.transform.parent.transform.parent.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currectbutton;

                }

                selectedoption.GetComponent<BoxCollider2D>().enabled = false;

                if (option_Parent.childCount == 0)
                {
                    gamePlay = false;
                    if (AllAnswerNo.Count >= allCharacter.Length)
                    {
                        StartCoroutine(LevelCompleted());
                        return true;
                    }
                    StartCoroutine(StepComplete());

                }
                return true;
            }
            else
            {

                for (int i = 0; i < droping_place.Length; i++)
                {
                    if (Vector3.Distance(selectedoption.transform.position, droping_place[i].transform.position) < 2.5f &&
                        droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite != currectbutton)
                    {
                        droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = wrongbutton;
                        gamePlay = false;
                        StartCoroutine(WaitforAnimation(i));
                        return true;
                    }
                }

                selectedoption.transform.position = selectedoption.lastpos;
            }

            return false;
        }
        public override void WrongAnswer()
        {

        }
        IEnumerator WaitforAnimation(int i)
        {

            selectedoption.transform.parent = droping_place[i].transform;
            selectedoption.transform.position = selectedoption.lastpos;
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            wrongAnswer_animtion.SetActive(false);
            droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Normalbutton[i];
            selectedoption.transform.parent = option_Parent;
            gamePlay = true;
        }
    }
}
