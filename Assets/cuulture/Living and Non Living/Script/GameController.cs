using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace culture.LivingandNonLiving.DragandDrop
{
    public class GameController : GameControllerforAll
    {
        public bool resetingOption;
        public int[] Question;

        public TextMeshPro[] allquestion;
        public Sprite Droped;
        public Transform option_Parent;
        public Sprite currectbutton, wrongbutton, Normalbutton;
        public Color PreviousText;
        public int childCounter;
        protected override void Start()
        {
            GameDataSet();
            //   base.Start();
        }
        public void GameDataSet()
        {
            foreach (var item in alloption)
            {
                item.GetComponent<BoxCollider2D>().enabled = true;
            }
            int k = 0;

            for (int i = 0; i < Question.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    alloption[k].Icon.sprite = allCharacter[Question[i]].sameLetter[j].Icon;
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

            yield return new WaitForSeconds(CorrectAnswer_delayTime);
            Party_pop.SetActive(false);
            for (int i = 0; i < droping_place.Length; i++)
            {
                for (int j = droping_place[i].transform.childCount - 1; j > 0; j--)
                {
                    if(droping_place[i].transform.GetChild(j).transform.GetComponent<BoxCollider2D>() != null)
                    { 
                    droping_place[i].transform.GetChild(j).transform.GetComponent<BoxCollider2D>().enabled = true;
                    }
                    droping_place[i].transform.GetChild(j).SetParent(option_Parent);
                }
            }
            Rearrnge();
        }
        public int maxholder;
        public bool singleloader;
        public override bool Neartodestination()
        {
            if (droping_place.Length == 1)
            {
                if (Vector3.Distance(selectedoption.transform.position, droping_place[int.Parse(selectedoption.no)].transform.position) < distangedrage)
                {
                    selectedoption.transform.parent = droping_place[int.Parse(selectedoption.no)].transform;
                    if (selectedoption.transform.parent.childCount >= maxholder)
                    {
                        selectedoption.transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite = currectbutton;

                    }
                    droping_place[int.Parse(selectedoption.no)].sprite = Droped;
                    selectedoption.GetComponent<BoxCollider2D>().enabled = false;
                    if (option_Parent.childCount == 0)
                    {
                        gamePlay = false;
                        if (reloding >= allCharacter.Length)
                        {
                            StartCoroutine(LevelCompleted());
                        }
                        StartCoroutine(StepComplete());

                    }
                    return true;
                }
                else
                    selectedoption.transform.position = selectedoption.lastpos;
                return false;
            }
            else
            {

                if (Vector3.Distance(selectedoption.transform.position, droping_place[int.Parse(selectedoption.no)].transform.position) < distangedrage && droping_place[int.Parse(selectedoption.no)].gameObject.activeInHierarchy)
                {
                    selectedoption.transform.parent = droping_place[int.Parse(selectedoption.no)].transform;
                    if (selectedoption.transform.parent.childCount >= maxholder)
                    {
                        selectedoption.transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite = currectbutton;
                        selectedoption.transform.parent.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.white;

                        if (singleloader)
                        {
                            StartCoroutine(WaitForCurrectanimtion());
                        }

                    }
                    droping_place[int.Parse(selectedoption.no)].sprite = Droped;
                    selectedoption.GetComponent<BoxCollider2D>().enabled = false;
                    if (option_Parent.childCount == 0)
                    {
                        gamePlay = false;
                        if (AllAnswerNo.Count >= allCharacter.Length)
                        {
                            StartCoroutine(LevelCompleted());
                        }
                        StartCoroutine(StepComplete());
                    }
                    return true;
                }
                else
                {

                    for (int i = 0; i < droping_place.Length; i++)
                    {
                        if (Vector3.Distance(selectedoption.transform.position, droping_place[i].transform.position) < distangedrage &&
                            droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite != currectbutton)
                        {
                            droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = wrongbutton;
                            droping_place[i].transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.white;

                            gamePlay = false;
                            StartCoroutine(WaitforAnimation(i));
                            return true;
                        }
                    }

                    selectedoption.transform.position = selectedoption.lastpos;
                }
            }

            return false;
            base.Neartodestination();
        }

        protected override void CurrectAnimtionCompleted()
        {
            base.CurrectAnimtionCompleted();

            if (resetingOption)
            {
                if (reloding == droping_place.Length)
                {

                    StartCoroutine(LevelCompleted());
                    return;
                }
                for (int i = droping_place[reloding - 1].transform.childCount - 1; i > 0; i--)
                {
                    droping_place[reloding - 1].transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
                    droping_place[reloding - 1].transform.GetChild(i).parent = option_Parent;
                }
                int k = 0;

                for (int j = 0; j < alloption.Length; j++)
                {
                    if (j < allCharacter[Question[reloding]].sameLetter.Length)
                    {
                        alloption[k].Icon.sprite = allCharacter[Question[reloding]].sameLetter[j].Icon;
                        alloption[k].no = reloding.ToString();
                        alloption[k].transform.SetSiblingIndex(Random.Range(0, option_Parent.childCount));
                    }
                    else
                    {

                    }
                    k++;
                }


            }
            droping_place[reloding - 1].gameObject.SetActive(false);
            droping_place[reloding].gameObject.SetActive(true);

            reloding++;


        }
        public override void WrongAnswer()
        {

        }
        IEnumerator WaitforAnimation(int i)
        {

            selectedoption.transform.parent = droping_place[i].transform;
            selectedoption.transform.position = selectedoption.lastpos;
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(WrongAnswer_delayTime);
            wrongAnswer_animtion.SetActive(false);
            droping_place[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Normalbutton;
            droping_place[i].transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro>().color = PreviousText;
            selectedoption.transform.parent = option_Parent;
            gamePlay = true;
        }

        public void Rearrnge()
        {
            reloding++;

            if(reloding >= allCharacter.Length)
            {
                StartCoroutine(LevelCompleted());
            return;
            }


            for (int d = 0; d < droping_place.Length; d++)
            {
                if (droping_place[d].transform.childCount == childCounter)
                {
                    for (int s = 1; s < droping_place[d].transform.childCount; s++)
                    {
                        droping_place[d].transform.GetChild(s).transform.parent = option_Parent;
                    }

                }
            }

            int k = 0;

            for (int i = 0; i < Question.Length; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    alloption[k].Icon.sprite = allCharacter[Question[i]].sameLetter[j].Icon;
                    alloption[k].no = i.ToString();
                    alloption[k].transform.SetSiblingIndex(Random.Range(0, option_Parent.childCount));
                    k++;
                }

            }
            gamePlay = true;
        }
    }
}