using System.Collections;
using System.Collections.Generic;
using DesignPatterns.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Laguage.beginning_sounds.match_images
{
    public class GameController : Singleton<GameController>
    {
        public Color currect, wrong, selected;

        public Transform optionparent;
        public Character[] allcharacter;
        public Transform[] alloption;
        public SpriteRenderer droping_place;
        public Sprite dropingselected, dropingnotselected;
        public SpriteRenderer dropingoutline;
        public string lettter;
        public SpriteRenderer question_sprite;
        List<int> allno = new List<int>();
        public int reloding;
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        private void Start()
        {
            StartGame();
        }
        void StartGame()
        {
            reloding++;
            if (reloding > 10)
            {
                StartCoroutine(LevelCompleted());
                return;
            }
            int no = 0;



            int answeroption = Random.Range(0, alloption.Length);
           // allno.Add(no);
            for (int i = 0; i < alloption.Length; i++)
            {
                if(i == answeroption)
                {
                    no = Random.Range(0, allcharacter.Length);
                    for (int j = 0; j < allno.Count; j++)
                    {
                        if (no == allno[j])
                        {
                            no = Random.Range(0, allcharacter.Length);
                            j = -1;
                        }
                    }
                    question_sprite.sprite = allcharacter[no].Icon;
                    lettter = allcharacter[no].letter;
                    alloption[i].GetChild(0).GetComponent<SpriteRenderer>().sprite = allcharacter[no].relatedsdprite;
                    alloption[i].GetComponent<Drager>().no = allcharacter[no].letter;
                }
                else
                {
                    no = Random.Range(0, allcharacter.Length);
                    for (int j  = 0; j < allno.Count; j++)
                    {
                        if (no == allno[j])
                        {
                            no = Random.Range(0, allcharacter.Length);
                            j = -1;
                        }
                    }
                    alloption[i].GetChild(0).GetComponent<SpriteRenderer>().sprite = allcharacter[no].relatedsdprite;
                    alloption[i].GetComponent<Drager>().no = allcharacter[no].letter;
                }
                allno.Add(no);


            }
            //foreach (var item in alloption)
            //{
            //    no = Random.Range(0, allcharacter.Length);
            //    for (int i = 0; i < allno.Count; i++)
            //    {
            //        if(no == allno[i])
            //        {
            //            no = Random.Range(0, allcharacter.Length);
            //            i = -1;
            //        }
            //    }
            //    item.GetChild(0).GetComponent<SpriteRenderer>().sprite = allcharacter[no].relatedsdprite;
            //    item.GetComponent<Drager>().no = allcharacter[no].letter;
            //    allno.Add(no);

            //}
            
           

        }


        public bool Neartodestination(GameObject obj)
        {

            if (Vector3.Distance(obj.transform.position, droping_place.transform.position) < 2.5f)
            {
                droping_place.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = dropingselected;
               // droping_place.color = Color.black;
                return true;
            }
            droping_place.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = dropingnotselected;
            return false;
        }
        public void CurrectAnswer()
        {
            dropingoutline.color = currect;
            droping_place.color = currect;
            StartCoroutine(RelodingThe_Level());
        }
        IEnumerator RelodingThe_Level()
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            Party_pop.SetActive(false);
            Drager drage = droping_place.transform.GetComponentInChildren<Drager>();
            drage.transform.parent = optionparent;
            drage.transform.position = drage.lastpos;
            droping_place.color = Color.white;
            dropingoutline.color = Color.white;
            allno.Clear();
            StartGame();

        }
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        IEnumerator WaitForReseting()
        {
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            wrongAnswer_animtion.SetActive(false);
            Drager drage = droping_place.transform.GetComponentInChildren<Drager>();
            drage.transform.parent = optionparent;
            drage.transform.position = drage.lastpos;
            droping_place.color = Color.white;
            dropingoutline.color = Color.white;
        }
        public void WrongAnswer()
        {
            dropingoutline.color = wrong;
            droping_place.color = wrong;


            StartCoroutine(WaitForReseting());
        }

    }
}
