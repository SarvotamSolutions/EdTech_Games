using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.Number1to10{
    public class GameManager : Singleton<GameManager>
    {
    
        public bool random;
        public bool withController;
        public Sprite[] ShowingIcon;
        public SpriteRenderer IconShowingSprite;
        public int Question;
        public List<int> Answered = new List<int>();
        public Sprite[] Allanswer;
        public Sprite WrongAnswer, CurrectAnswer, IncompleteAnswer;
        public Sprite wrongArrow, currectArrow, IcompletArrow;
        public Color wrong, currect, incomlete, noseclting;
        public GameObject QuestionGameObject;
        public GameObject[] answerObject;
        public GameObject answerPrefab;
        public Transform OptionParent;
        public Transform DropingParent;
        public GameObject arrow;
        public GameObject CurrectanswerOBj;
        public GameObject WrongAnswer_animation,GameCompleted_animation;
        private void Awake()
        {
            
            GameStart();
        }

        IEnumerator GameCompleted()
        {
            GameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }
        void GameStart()
        {
            gamePlay = true;
           if(random)
               Question = Random.Range(0, Allanswer.Length);
            if (Answered.Count >= Allanswer.Length)
            {
                Debug.Log("Go to Next level");
                StartCoroutine(GameCompleted());
                return;
            }

            if (withController)
            {
                IconShowingSprite.sprite = ShowingIcon[Question];
            }
            for (int i = 0; i < Answered.Count; i++)
            {

                if(Question == Answered[i])
                {
                    i = -1;
                    Question = Random.Range(0, Allanswer.Length);

                }

            }
            Answered.Add(Question);
            QuestionGameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = "" + (Question + 1);
            int currectansweroption = Random.Range(0, 4);
            int[] tempsavedAnswer = { 0, 0, 0, 0 };
            tempsavedAnswer[currectansweroption] = Question;
            for (int i = 0; i < 4; i++)
            {
                GameObject obj = answerObject[i];
                DragingObj draginobj = obj.GetComponent<DragingObj>();

                if (i == currectansweroption)
                {

                    obj.GetComponent<SpriteRenderer>().sprite = Allanswer[Question];
                    draginobj.answer = Question;
                    draginobj.currectAnswer = true;
                }
                else
                {

                    int no = Random.Range(0, Allanswer.Length);
                    for (int j = 0; j < tempsavedAnswer.Length; j++)
                    {

                        if (no == tempsavedAnswer[j])
                        {
                            no = Random.Range(0, Allanswer.Length);
                            j = -1;
                        }

                    }
                    tempsavedAnswer[i] = no;
                    draginobj.answer = no;
                    obj.GetComponent<SpriteRenderer>().sprite = Allanswer[no];
                    draginobj.currectAnswer = false;
                }

              //  obj.GetComponent<DragingObj>().PostionSet();

            }
        }
        public void GameReset()
        {

            Question++;
            CurrectanswerOBj.SetActive(false);
            arrow.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.IcompletArrow;
            QuestionGameObject.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.IncompleteAnswer;
            for (int i = 0; i < DropingParent.childCount; i++)
            {
                //Destroy(DropingParent.GetChild(i).gameObject);
                DropingParent.GetChild(i).parent = OptionParent;

            }
            for (int i = 0; i < OptionParent.childCount; i++)
            {
              //  Destroy(OptionParent.GetChild(i).gameObject);
            }
            GameStart();

        }

        public bool Neartodestination(GameObject obj)
        {
            if (Vector3.Distance(obj.transform.position, DropingParent.transform.position) < 5)
            {
              
                return true;
            }

            return false;
        }

    } 

}
