using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Maths.TeenBeads.Caluclation
{
    public class GameController : Singleton<GameController>
    {
        public Totorial totorial;
        public bool Completed;
        public int question1, quesiton2,Answer;
        public TextMeshPro Question1Text, AnswerText,questiion2Text;
        public Sprite[] alloptionsptite;
        public List<Answer> answerOption = new List<Answer>();
        public GameObject[] Option;
        public Sprite currctanswer, wronganswer,norml;
        public SpriteRenderer inputfieldtext;
        public List<int> answerQuestionno;
        public GameObject AnswerObject;
        public GameObject PartyBlast;

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        private void Start()
        {
            question1 = Random.Range(1, 10);
            quesiton2 = Random.Range(1, 10);
            Answer = question1 + quesiton2;
            int answerno_opetion = Random.Range(0, 4);
            SettingGame(answerno_opetion);
            Question1Text.text = question1.ToString();
            AnswerText.text = Answer.ToString();
        }

        public bool Neartodestination(GameObject obj)
        {
            if (Vector3.Distance(obj.transform.position, AnswerObject.transform.position) < 1)
            {

                SelctButton(answerOption[obj.GetComponent<Draging>().no].Getno(), obj.transform.parent.GetComponent<SpriteRenderer>());
                return true;
            }
            else
            {
                return false;
            }


        }

        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        void RelodingLevel()
        {

            if (answerQuestionno.Count >= 10)
            {
                StartCoroutine(LevelCompleted());
                return;
            }
            quesiton2 = Random.Range(1, 11);
            foreach (var item in answerQuestionno)
            {
                if (quesiton2 == item)
                {
                    RelodingLevel();
                    return;
                }
            }


            for (int i = 0; i < Option.Length; i++)
            {
                Option[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
           
            answerOption.Clear();
            for (int i = 0; i < selctedno.Count; i++)
            {
                selctedno[i] = 0;
            }
         
            inputfieldtext.sprite = norml;
            questiion2Text.text = "";
            question1 = Random.Range(1, 10);
            Answer = question1 + quesiton2;
            int answerno_opetion = Random.Range(0, 4);
            SettingGame(answerno_opetion);
            Question1Text.text = question1.ToString();
            AnswerText.text = Answer.ToString();
            gamePlay = true;

        }
        public List<int> selctedno = new List<int> { 0, 0, 0, 0 };
        void SettingGame(int answer)
        {
            selctedno[answer] = quesiton2;
            for (int i = 0; i < selctedno.Count; i++)
            {
                int no = Random.Range(1, 11);
                for (int j = 0; j < selctedno.Count; j++)
                {
                    if(selctedno[j] == no)
                    {
                        j = -1;
                        no = Random.Range(1, 11);
                    }
                }
                if (i != answer)
                {
                    selctedno[i] = no;
                }
            }
            Answer ans = new Answer();
            for (int i = 0; i < selctedno.Count; i++)
            {
                float width = 82 * (selctedno[i] - 1);
                ans = new Answer();
                ans.addingno(selctedno[i], alloptionsptite[selctedno[i] - 1]);
                answerOption.Add(ans);
            }

            answerQuestionno.Add(quesiton2);
            for (int i = 0; i < Option.Length; i++)
            {
                Option[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = answerOption[i].GetImage();
            }
        }
        public void SelctButton(int no, SpriteRenderer parentimage)
        {
            gamePlay = false;
            for (int i = 0; i < Option.Length; i++)
            {
                Option[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
            questiion2Text.text = no.ToString();
            if (no == quesiton2)
            {
                PartyBlast.SetActive(true);
                Completed = true;
                inputfieldtext.sprite = currctanswer;
                StartCoroutine(waitForLevelLoad());
            }
            else
            {
                inputfieldtext.sprite = wronganswer;
                StartCoroutine(WrongAnswerAnimation());
            }
        }
        IEnumerator WrongAnswerAnimation()
        {
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            wrongAnswer_animtion.SetActive(false);
            questiion2Text.text = "";
            AnswerObject.GetComponent<SpriteRenderer>().sprite = norml;
            swapOption();
            foreach (var item in Option)
            {
                item.GetComponent<SpriteRenderer>().color = Color.white;

            }
            for (int i = 0; i < Option.Length; i++)
            {
                Option[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = answerOption[i].GetImage();
            }
            gamePlay = true;
        }

        public void swapOption()
        {
            for (int i = 0; i < answerOption.Count; i++)
            {
                Answer ans = answerOption[i];
                int switchto = Random.Range(0, answerOption.Count);
                Debug.Log("Sweitching" +ans.Getno()+" ccc " + answerOption[switchto].Getno());
                answerOption[i] = answerOption[switchto];
                answerOption[switchto] = ans;
            }
        }
        IEnumerator waitForLevelLoad()
        {
            yield return new WaitForSeconds(2);
            Completed = false;
            PartyBlast.SetActive(false);
            RelodingLevel();
        }
    
    }
    [System.Serializable]
    public class Answer 
    {
        private int no;
        private Sprite image;
        private float width;
        public void addingno (int nubler,Sprite sprite)
        {
            no = nubler;
            image = sprite;
        }
        public Sprite GetImage()
        {
            return image;
        }
        public int Getno()
        {
            return no;
        }
        public float GetWidth()
        {
            return width;
        }
    }
}