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
        public Sprite ObectPlacedBGSprite;
        public Sprite QuestionBGSprite;
        public Sprite[] objecticon;
        public Sprite[] droingplace;
        public bool bird;
        public Sprite birdsprite;
        public Color questionTextColor;
        public string HeadingText;
    }


    public class GameController : Singleton<GameController>
    {
        public Totorial totorial;
        public obj[] allobj;
        public GameObject papercontins;
        public GameObject dustbin;
        public SpriteRenderer ObjectPlacedBG;
        public SpriteRenderer QuestionBG;
        public TextMeshPro[] numberText;
        public int[] numbers;
        public Sprite OpenDustbinSprite;
        public Sprite closeDustBinSprite;
        public TextMeshPro Heading;
        public Button NextButton;
        private int reloding;
        int randomNo;

        public Color currectanswer, wronganswer, normalanswer;

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
            numberText[2].transform.GetComponent<TextMeshPro>().text = "?";
            numberText[2].transform.GetComponent<TextMeshPro>().color = allobj[randomNo].questionTextColor;
            numberText[2].transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
            gamePlay = true;
            NextButton.interactable = true;

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
            Heading.text = allobj[getno].HeadingText;
            randomNo = getno;
            ObjectPlacedBG.sprite = allobj[getno].ObectPlacedBGSprite;
            QuestionBG.sprite = allobj[getno].QuestionBGSprite;
            if (getno == 2)
            {
                ObjectPlacedBG.sortingOrder = 2;
                numberText[0].color = allobj[getno].questionTextColor;
                numberText[1].color = allobj[getno].questionTextColor;
                numberText[2].color = allobj[getno].questionTextColor;
            }
            else
            {
                ObjectPlacedBG.sortingOrder = 0;
                numberText[0].color = allobj[getno].questionTextColor;
                numberText[1].color = allobj[getno].questionTextColor;
                numberText[2].color = allobj[getno].questionTextColor;
            }
            numberText[2].transform.parent.GetComponent<SpriteRenderer>().enabled = true;

            numbers[2] = numbers[0] - numbers[1];
            TextChange();
            gamePlay = true;
            NextButton.interactable = true;

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
            if (Vector3.Distance(obj.transform.position, dustbin.transform.position) < 4)
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
            if (totorial.totorialplaying)
                return;

            gamePlay = false;
            NextButton.interactable = false;
            if (papercontins.transform.childCount == numbers[2])
            {
                numberText[2].text = papercontins.transform.childCount.ToString();
                numberText[2].transform.parent.GetComponent<SpriteRenderer>().color = currectanswer;
                numberText[2].transform.GetComponent<TextMeshPro>().color = Color.white;
                numberText[2].transform.parent.GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(waitForLoad());
                Debug.Log("currect Answer");
            }
            else
            {
                numberText[2].text = papercontins.transform.childCount.ToString();
                numberText[2].transform.parent.GetComponent<SpriteRenderer>().color = wronganswer;
                numberText[2].transform.GetComponent<TextMeshPro>().color = Color.white;
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

                numberText[2].transform.parent.GetComponent<SpriteRenderer>().color = normalanswer;
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