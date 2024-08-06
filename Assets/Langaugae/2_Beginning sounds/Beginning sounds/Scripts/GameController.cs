using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.SceneManagement;

namespace Laguage.beginning_sounds.matchtheSound
{
    public class GameController : Singleton<GameController>
    {
        public Totorial totorialcheck;
        public Color red, blue, green, vilot;
        public LineRenderer selectedline;
        public AllCharacter[] alltypeofCharacter;
        public SpriteRenderer[] iconOption;
        public TextMeshPro[] alltext;
        public drager[] alldrager;
        List<int> allselectedno = new List<int>();
        List<int> currentno = new List<int>();
        public Material currectanswer;
        public Material WrongAnswer;
        public Material SelectMaterail;
        public Sprite currectanswerarrow, wronganswerarrow;
        public Sprite Currectanswer, wronganswer, Selectanswer;
        public int totalanswered;
        public int relodtime;

        private int[] allrandomno;


        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        void Start()
        {
            gamePlay = true;
            GameStart();
        }
        void GameStart()
        {
            int j = 0;

            totalanswered = 0;
            currentno.Clear();
            relodtime++;
            foreach (var item in iconOption)
            {
                int randomno = Random.Range(0, alltypeofCharacter.Length);

                for (int i = 0; i < allselectedno.Count; i++)
                {
                    if (randomno == allselectedno[i] && allselectedno.Count < 26)
                    {
                        randomno = Random.Range(0, alltypeofCharacter.Length);
                        i = -1;
                    }
                }
                for (int i = 0; i < currentno.Count; i++)
                {
                    if (randomno == currentno[i] && allselectedno.Count >= 26)
                    {
                        randomno = Random.Range(0, alltypeofCharacter.Length);
                        i = -1;
                    }
                }
                allselectedno.Add(randomno);
                currentno.Add(randomno);
                int randomnno = Random.Range(0, alltypeofCharacter[randomno].sameLetter.Length);
                item.sprite = alltypeofCharacter[randomno].sameLetter[randomnno].Icon;
                alldrager[j].wordsSound = alltypeofCharacter[randomno].sameLetter[randomnno].Sound;

                item.transform.SetSiblingIndex(Random.Range(0, item.transform.parent.childCount));
                j++;
            }
            for (int i = 0; i < alltext.Length; i++)
            {

                alldrager[i].Answeroption = iconOption[i].gameObject;
                alltext[i].text = alltypeofCharacter[currentno[i]].Letter;
                alltext[i].color = vilot;
                alldrager[i].lettersound = alltypeofCharacter[currentno[i]].lettersound;
                alldrager[i].Answeroption.transform.parent.SetSiblingIndex(Random.Range(0, 4));
            }
            gamePlay = true;
        }
        void Update()
        {
            if (gamePlay == false)
                return;
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            if (selectedline)
            {
                selectedline.SetPosition(1, pos);
            }

        }
        IEnumerator LevelCompleted()
        {
            gamePlay = false;
            gameCompleted_animation.SetActive(true);
            gameCompleted_animation.GetComponent<AudioSource>().PlayDelayed(1);
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene(0);
        }
        IEnumerator ComplteAnimation()
        {
            Party_pop.SetActive(true);
            Party_pop.GetComponent<AudioSource>().PlayDelayed(1);
            yield return new WaitForSeconds(4);
            Party_pop.SetActive(false);
            foreach (var item in alldrager)
            {
                item.NotAnswerd();
                item.Reseting();
            }
            GameStart();
        }
        public void ScenecChange()
        {
            gamePlay = false;
            if (relodtime < 7)
            {
                StartCoroutine(ComplteAnimation());
            }
            else
                StartCoroutine(LevelCompleted());
        }
    }
}