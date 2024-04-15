using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.Number1to10.numberWithObject
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public ObjectSlection Question;
        [SerializeField] private GameObject upperVertical, midleVertical, LowerVertical;
        [SerializeField] private GameObject[] AllFlower;
        [SerializeField] private Sprite NormalQuestuion, CurrectQuestion, WrongQuestion;
        [SerializeField] private Sprite NormalArrow, currectarrow, wrongarrow;
        [SerializeField] private SpriteRenderer Arrow;

        public Sprite[] unclickedsprite;
        public Sprite[] normal_sprite;
        public Sprite[] ClickedSprite;

        int DropedID = 0;

        public int no;
        public List<int> AnsweredQuestion = new List<int>();

        public int Questionno;
        public TextMeshPro text;

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            QuestoinCreation();
        }
        IEnumerator LevelCompleted()
        {
            yield return new WaitForSeconds(.4f);
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        // Update is called once per frame
        public void QuestoinCreation()
        {
            if (AnsweredQuestion.Count >= 10)
            {
                StartCoroutine(LevelCompleted());
                return;
            }
            no = Random.Range(0, 2);
            Question.GetComponent<SpriteRenderer>().sprite = unclickedsprite[no];
            foreach (var item in AllFlower)
            {
                item.GetComponent<SpriteRenderer>().sprite = normal_sprite[no];
            }
            Questionno = Random.Range(1, 11);
            foreach (var item in AnsweredQuestion)
            {
                if (Questionno == item)
                {
                    QuestoinCreation();
                    return;
                }
            }
            AnsweredQuestion.Add(Questionno);
            text.text = Questionno.ToString();

        }
        public void ObjectDroped()
        {
            if (DropedID > 9)
                return;

            if (DropedID < 4)
                midleVertical.SetActive(true);
            else if (DropedID < 8)
                upperVertical.SetActive(true);
            else
                LowerVertical.SetActive(true);

            AllFlower[DropedID].SetActive(true);

            DropedID++;

        }
        IEnumerator ResetingGame()
        {
            yield return new WaitForSeconds(.5f);
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            Reseting();
            wrongAnswer_animtion.SetActive(false);
        }
        IEnumerator GoingNextlevel()
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            Party_pop.SetActive(false);
            Reseting();
            QuestoinCreation();
        }
        public void Conform()
        {
            if ((DropedID) == Questionno)
            {

                //currect Answer
                text.transform.parent.GetComponent<SpriteRenderer>().sprite = CurrectQuestion;
                Arrow.sprite = currectarrow;
                StartCoroutine(GoingNextlevel());

            }
            else
            {
                text.transform.parent.GetComponent<SpriteRenderer>().sprite = WrongQuestion;
                Arrow.sprite = wrongarrow;
                StartCoroutine(ResetingGame());

            }
        }//conform Button
        public void Reseting()
        {
            text.transform.parent.GetComponent<SpriteRenderer>().sprite = NormalQuestuion;
            Arrow.sprite = NormalArrow;
            upperVertical.SetActive(false);
            LowerVertical.SetActive(false);

            for (int i = 0; i < AllFlower.Length; i++)
            {
                AllFlower[i].SetActive(false);
            }
            DropedID = 0;
        }//reseting Button

    }

}