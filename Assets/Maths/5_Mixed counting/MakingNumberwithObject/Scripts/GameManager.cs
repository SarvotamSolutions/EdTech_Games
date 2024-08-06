 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Maths.Number1to10.numberWithObject
{
    public class GameManager : Singleton<GameManager>
    {


        [SerializeField] private GameObject upperVertical, midleVertical, LowerVertical;
        [SerializeField] private Sprite NormalQuestuion, CurrectQuestion, WrongQuestion;
        [SerializeField] private Sprite NormalArrow, currectarrow, wrongarrow;
        [SerializeField] private SpriteRenderer Arrow;
        private bool randomno;
        public bool WithControll;
        public ObjectSlection Question;
        public Totorial totorialcheck;
        public GameObject[] AllFlower;
        public Sprite[] unclickedsprite;
        public Sprite[] normal_sprite;
        public Sprite[] ClickedSprite;
        public Sprite[] allbackground;
        public Image background;
        public GameObject resetbutton;
        int DropedID = 0;

        public int IconspriteID;
        public int selectediconid;
        public List<int> AnsweredQuestion = new List<int>();

        public int Questionno;
        public TextMeshPro text;
        public float currectanswerInterval, wronganswerInterval;
        [Header("Animation Delay")]
        [SerializeField] private float delay_correctAnswer=3;
        [SerializeField] private float delay_wrongtAnswer=4;
        [SerializeField] private float delay_completedAnswer =2;

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        private void Start()
        {
            QuestoinCreation();
        }


        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(delay_completedAnswer);
            SceneManager.LoadScene(0);
        }

        public void QuestoinCreation()
        {
            if (AnsweredQuestion.Count >= 10)
            {
                StartCoroutine(LevelCompleted());
                return;
            }

            IconspriteID = Random.Range(0, normal_sprite.Length);
            Question.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = normal_sprite[IconspriteID];
            background.sprite = allbackground[IconspriteID];
            Question.GetComponent<SpriteRenderer>().sprite = unclickedsprite[IconspriteID];      
            Questionno++;

            if(WithControll)
            {
                for (int i = 0; i < Questionno; i++)
                {
                    ObjectDroped();
                }
                
            }
            else
            {
                foreach (var item in AllFlower)
                {
                    item.GetComponent<SpriteRenderer>().sprite = normal_sprite[IconspriteID];
                }
            }
            if(randomno)
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
            gamePlay = true;
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
            yield return new WaitForSeconds(wronganswerInterval);
            donebutton.interactable = true;
            resetbutton.SetActive(true);
            Reseting();
            wrongAnswer_animtion.SetActive(false);
        }
        IEnumerator GoingNextlevel()
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(currectanswerInterval);
            Party_pop.SetActive(false);
            donebutton.interactable = true;
            resetbutton.SetActive(true);
            Reseting();
            QuestoinCreation();
        }
        public Sprite emty;
        public Button donebutton;
        public void Conform()
        {
            if (totorialcheck.totorialplaying || gamePlay == false)
                return;

            resetbutton.SetActive(false);
            gamePlay = false;
            donebutton.interactable = false;
            int id = 0;
            if (WithControll)
            {
                id = selectediconid;
            }
            else
            {
                id = DropedID;
            }
            if (id == Questionno)
            {
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
        }

        public void Reseting()
        {
            if(totorialcheck.totorialplaying)
                return;
            selectediconid = 0;
            text.transform.parent.GetComponent<SpriteRenderer>().sprite = NormalQuestuion;
            Arrow.sprite = NormalArrow;
            upperVertical.SetActive(false);
            LowerVertical.SetActive(false);
            DropedID = 0;
            if (WithControll)
            {
                foreach (var item in AllFlower)
                {
                    item.GetComponent<SpriteRenderer>().sprite = emty;

                }
                for (int i = 0; i < Questionno; i++)
                {
                    ObjectDroped();
                }
                DropedID = 0;
            }
            else
            {
                for (int i = 0; i < AllFlower.Length; i++)
                {
                    AllFlower[i].SetActive(false);
                }
            }
            gamePlay = true;
        }
    }
}