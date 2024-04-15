using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
namespace Maths.Substraction.DragingObject
{

    public class GameController : MonoBehaviour
    {
        public static GameController instance;
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
        }

        private void Awake()
        {
            instance = this;
        }
        public void Start()
        {
            Relod();
        }
        void Relod()
        {
            reloding++;
            numbers[0] = Random.Range(1, 11);
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

                }
                else
                {
                    papercontins.transform.GetChild(i).gameObject.SetActive(false);
                    papercontins.transform.GetChild(i).transform.SetParent(dustbin.transform);
                }
                no++;


            }
            numbers[2] = numbers[0] - numbers[1];
            TextChange();
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


                return true;

            }

            return false;
        }
        public void GameCompleted()
        {
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