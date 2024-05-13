using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
namespace Maths.Substraction.DragingObject
{
    [System.Serializable]
    public class obj
    {

        public Sprite background;
        public Sprite[] objecticon;
        public Sprite[] droingplace;
        public bool bird;
        public Sprite birdsprite;
    }


    public class GameController : Singleton<GameController>
    {
        public obj[] allobj;
        public GameObject papercontins;
        public GameObject dustbin;

        public TextMeshPro[] numberText;
        public int[] numbers;
        public Sprite OpenDustbinSprite;
        public Sprite closeDustBinSprite;
        private int reloding;

        public Sprite currectanswer, wronganswer, normalanswer;

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        IEnumerator WrongAnswerAnimation()
        {
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            wrongAnswer_animtion.SetActive(false);
            gamePlay = true;
        }
        public Image bg;
      
        public void Start()
        {
            Relod();
        }
        public int getno;
        void Relod()
        {
            
            reloding++;
            getno = Random.Range(0, allobj.Length);
            OpenDustbinSprite = allobj[getno].droingplace[0];
            closeDustBinSprite = allobj[getno].droingplace[1];
            dustbin.GetComponent<SpriteRenderer>().sprite = closeDustBinSprite;
            numbers[0] = Random.Range(5, 11);
            numbers[1] = Random.Range(1, 10);
            while (numbers[0] < numbers[1])
            {
                numbers[1] = Random.Range(1, 7);
            }
            int no = 0;
            for (int i = papercontins.transform.childCount - 1; i >= 0; i--)
            {
                if (no < numbers[0])
                {
                    papercontins.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = allobj[getno].objecticon[Random.Range(0, allobj[getno].objecticon.Length)];

                }
                else
                {
                    papercontins.transform.GetChild(i).gameObject.SetActive(false);
                    papercontins.transform.GetChild(i).transform.SetParent(dustbin.transform);
                }
                no++;


            }
            bg.sprite = allobj[getno].background;
            numbers[2] = numbers[0] - numbers[1];
            TextChange();
            gamePlay = true;
        }
        void TextChange()
        {
            for (int i = 0; i < numberText.Length - 1; i++)
            {
                numberText[i].text = numbers[i].ToString();
            }
        }
        public bool Neartodestination(GameObject obj)
        {
            if (Vector3.Distance(obj.transform.position, dustbin.transform.position) < 2)
            {
                if (allobj[getno].bird)
                {
                    obj.GetComponent<SpriteRenderer>().sprite = allobj[getno].birdsprite;
                    
                }

                return true;

            }
            if (allobj[getno].bird)
            {
                obj.GetComponent<SpriteRenderer>().sprite = allobj[getno].objecticon[0];

            }
            return false;
        }
        public void GameCompleted()
        {
            gamePlay = false;
            if (papercontins.transform.childCount == numbers[2])
            {
                numberText[2].text = papercontins.transform.childCount.ToString();
                for (int i = 0; i < numberText.Length; i++)
                {

                    numberText[i].transform.parent.GetComponent<SpriteRenderer>().sprite = currectanswer;
                }
                numberText[2].transform.parent.GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(waitForLoad());
                Debug.Log("currect Answer");
            }
            else
            {
                numberText[2].text = papercontins.transform.childCount.ToString();
                for (int i = 0; i < numberText.Length; i++)
                {

                    numberText[i].transform.parent.GetComponent<SpriteRenderer>().sprite = wronganswer;
                }
                numberText[2].transform.parent.GetComponent<SpriteRenderer>().enabled = true;
                for (int i = dustbin.transform.childCount - 1; i >= 0; i--)
                {
                    dustbin.transform.GetChild(i).gameObject.SetActive(true);
                    dustbin.transform.GetChild(i).transform.parent = papercontins.transform;

                }
                int no = 0;
                for (int i = papercontins.transform.childCount - 1; i >= 0; i--)
                {
                    if (no < numbers[0])
                    {
                        papercontins.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = allobj[getno].objecticon[Random.Range(0, allobj[getno].objecticon.Length)];

                    }
                    else
                    {
                        papercontins.transform.GetChild(i).gameObject.SetActive(false);
                        papercontins.transform.GetChild(i).transform.SetParent(dustbin.transform);
                    }
                    no++;


                }
                StartCoroutine(WrongAnswerAnimation());
            }
        }
        IEnumerator waitForLoad()
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            Party_pop.SetActive(false);
            if (reloding < 10)
            {
                numberText[2].transform.parent.GetComponent<SpriteRenderer>().enabled = false;
                numberText[2].text = "?";
                for (int i = 0; i < numberText.Length; i++)
                {

                    numberText[i].transform.parent.GetComponent<SpriteRenderer>().sprite = normalanswer;
                }
                for (int i = dustbin.transform.childCount - 1; i >= 0; i--)
                {
                    dustbin.transform.GetChild(i).gameObject.SetActive(true);
                    dustbin.transform.GetChild(i).transform.parent = papercontins.transform;

                }

                Relod();
            }
            else
            {
                StartCoroutine(LevelCompleted());
            }
        }
    }
}