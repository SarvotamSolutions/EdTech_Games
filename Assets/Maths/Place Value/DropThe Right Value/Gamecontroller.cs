using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


namespace Maths.placeValue.slectOption
{
    public class Gamecontroller : Singleton<Gamecontroller>
    {
    
        public TextMeshPro questiontext;
        public int number;
        public SpriteRenderer dropbox;
        public TextMeshPro[] options;
        public int answerno;
        public int[] allanswer;

        public Sprite normalsprite;
        public Sprite currectanswer;
        public Sprite wronganswer;
        private int reloding;
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        private void Start()
        {
          

            number = Random.Range(10, 99);
            answerno = Random.Range(0, 4);
            options[answerno].transform.GetComponentInParent<DragDroper>().no = number;
            questiontext.text = number.ToString();
            allanswer[answerno] = number;
            options[answerno].text = (number / 10) + " tens and " + (number % 10) + " ones";
            for (int i = 0; i < allanswer.Length; i++)
            {
                if(answerno == i)
                {

                }
                else
                {
                    
                    allanswer[i] = Random.Range(10, 99);
                    options[i].transform.GetComponentInParent<DragDroper>().no = allanswer[i];
                    options[i].text = (allanswer[i] / 10) + " tens and " + (allanswer[i] % 10) + " ones";
                }


            }
        }

        public void Restart(DragDroper obj)
        {
            StartCoroutine(WairforRelode(obj));

        }

        public  IEnumerator WairforRelode(DragDroper obj)
        {
           
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            wrongAnswer_animtion.SetActive(false);
            obj.GetComponent<SpriteRenderer>().sprite = normalsprite;
            obj.transform.position = obj.lastpos;
            gamePlay = true;
        }
        public void resetgame(DragDroper obj)
        {

            
            StartCoroutine(waitforreset(obj));

        }
        IEnumerator LevelCompleted()
        {
            yield return new WaitForSeconds(.4f);
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        IEnumerator waitforreset(DragDroper obj)
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            obj.GetComponent<SpriteRenderer>().sprite = normalsprite;
            obj.transform.position = obj.lastpos;
            Party_pop.SetActive(false);
            reloding++;

            if (reloding > 9)
            {
                StartCoroutine(LevelCompleted());

                yield return null;
            }
            number = Random.Range(10, 99);
            answerno = Random.Range(0, 4);
            questiontext.text = number.ToString();
            allanswer[answerno] = number;
            options[answerno].transform.GetComponentInParent<DragDroper>().no = number;
            options[answerno].text = (number / 10) + " tens and " + (number % 10) + " ones";
            for (int i = 0; i < allanswer.Length; i++)
            {
                if (answerno == i)
                {

                }
                else
                {

                    allanswer[i] = Random.Range(10, 99);
                    options[i].transform.GetComponentInParent<DragDroper>().no = allanswer[i];
                    options[i].text = (allanswer[i] / 10) + " tens and " + (allanswer[i] % 10) + " ones";
                }


            }
            gamePlay = true;

        }

        public bool Neartodestination(GameObject obj)
        {

            if (Vector3.Distance(obj.transform.position, dropbox.transform.position) < 1)
            {


                if (obj)
                {
                   
                    return true;
                }
            }

            return false;
        }

        }

    }