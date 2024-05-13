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

        public bool WithControll;
        public bool randomno;
        public ObjectSlection Question;
        [SerializeField] private GameObject upperVertical, midleVertical, LowerVertical;
        public GameObject[] AllFlower;
        [SerializeField] private Sprite NormalQuestuion, CurrectQuestion, WrongQuestion;
        [SerializeField] private Sprite NormalArrow, currectarrow, wrongarrow;
        [SerializeField] private SpriteRenderer Arrow;

        public Sprite[] unclickedsprite;
        public Sprite[] normal_sprite;
        public Sprite[] ClickedSprite;
        public Sprite[] allbackground;
        public Image background;

        int DropedID = 0;

        public int IconspriteID;
        public int selectediconid;
        public List<int> AnsweredQuestion = new List<int>();

        public int Questionno;
        public TextMeshPro text;

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
            IconspriteID = Random.Range(0, normal_sprite.Length);
            Question.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = normal_sprite[IconspriteID];
            background.sprite = allbackground[IconspriteID];
            Question.GetComponent<SpriteRenderer>().sprite = unclickedsprite[IconspriteID];
        
            Questionno++;
            if(WithControll)
            {

            //    DropedID = Questionno-1;
                for (int i = 0; i < Questionno; i++)
                {
                    ObjectDroped();
                   // DropedID++;
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
        public Sprite emty;
        public void Conform()
        {
            gamePlay = false;
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
            selectediconid = 0;
        
          
            text.transform.parent.GetComponent<SpriteRenderer>().sprite = NormalQuestuion;
            Arrow.sprite = NormalArrow;
            upperVertical.SetActive(false);
            LowerVertical.SetActive(false);
            //midleVertical.SetActive(false);
            DropedID = 0;
            if (WithControll)
            {
                foreach (var item in AllFlower)
                {
                    item.GetComponent<SpriteRenderer>().sprite = emty;

                }
                Debug.Log(Questionno);

                 //  DropedID = Questionno-1;
                 //
                for (int i = 0; i < Questionno; i++)
                {
                    ObjectDroped();
                    // DropedID++;
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
          //  DropedID = 0;
        }//reseting Button

    }

}