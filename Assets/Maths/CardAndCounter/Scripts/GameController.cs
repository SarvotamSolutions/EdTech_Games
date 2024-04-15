using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.CountAndCards.count
{
    public class GameController : MonoBehaviour
    {
        public static GameController instace;
        public int no;
        public List<int> allno = new List<int>();
        public Transform droper,Holder;
        public Sprite currectanswer, wronganswer, Normal;
        public Sprite currectanswerBox, wronganswerBOx, NormalBox;
        public Button nextbutton;
        public TextMeshPro QuestionText;
        public SpriteRenderer BOx;

        [Space(10)]
        public GameObject OddevenDropbox;
        public GameObject OddevenBox;
        public GameObject oddevenButtonPanel;
        public Sprite CurrectAnswerButton, WrongAnswerButton, NormalAnswerButton;

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        private void Start()
        {
            instace = this;
            no = Random.Range(1, 11);
            QuestionText.text = no.ToString();
            allno.Add(no);
        }

        public void Answer()
        {
            nextbutton.transform.gameObject.SetActive(false);

            if(no == droper.childCount - 1)
            {
                BOx.sprite = currectanswerBox;
                //currect answer
                for (int i = 0; i < droper.childCount-1; i++)
                {
                    droper.GetChild(i).GetComponent<Image>().sprite = currectanswer;

                }
                StartCoroutine(Reseting(true));
            }
            else
            {
                wrongAnswer_animtion.SetActive(true);
                BOx.sprite = wronganswerBOx;
                for (int i = 0; i < droper.childCount - 1; i++)
                {
                    droper.GetChild(i).GetComponent<Image>().sprite = wronganswer;

                }
                StartCoroutine(Reseting(false));
                //wrong answer
            }
          
        }
        IEnumerator Reseting(bool currectanswer)
        {
           
            droper.GetChild(droper.childCount - 1).gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);
            BOx.sprite = NormalBox;
            wrongAnswer_animtion.SetActive(false);
            droper.GetChild(droper.childCount - 1).gameObject.SetActive(true);
            if (currectanswer)
            {
                OddevenOptionOppen();
                //RelodingLevel();
            }
            else
            {
                nextbutton.gameObject.SetActive(true);
                for (int i = droper.childCount - 2; i >= 0; i--)
                {
                   droper.GetChild(i).GetComponent<Image>().sprite = Normal;
                   droper.GetChild(i).transform.parent = Holder;

                }
            }
        }

        public void OddevenOptionOppen()
        {
            nextbutton.gameObject.SetActive(false);
            BOx.gameObject.SetActive(false);
            droper.gameObject.SetActive(false);
            Holder.gameObject.SetActive(false);


            for (int i = droper.childCount-2; i >=0; i--)
            {
                droper.GetChild(i).transform.parent = OddevenDropbox.transform;
            }
            OddevenDropbox.SetActive(true);
            OddevenBox.SetActive(true);
            OddevenBox.transform.GetChild(0).GetComponent<TextMeshPro>().text = no.ToString();
            oddevenButtonPanel.SetActive(true);

        }

        public void OddButton(Image rendere)
        {
            int nos = no % 2;
            Debug.Log(nos);
            if (nos == 1)
            {
                rendere.sprite = CurrectAnswerButton;
                StartCoroutine(ODdevenReset(true,rendere));
            }
            else
            {
                rendere.sprite = WrongAnswerButton;
                StartCoroutine(ODdevenReset(false, rendere));
            }
        }
        public void EvenButton(Image rendere)
        {
            int nos = no % 2;
            Debug.Log(nos);
            if (nos == 0)
            {
                rendere.sprite = CurrectAnswerButton;
                StartCoroutine(ODdevenReset(true,rendere));
            }
            else
            {
                rendere.sprite = WrongAnswerButton;
                StartCoroutine(ODdevenReset(false, rendere));
            }
        }
        IEnumerator ODdevenReset(bool currectAnswer,Image image)
        {
            if (!currectAnswer)
            {

            }
           
            if (currectAnswer)          
            {
                Party_pop.SetActive(true);
                yield return new WaitForSeconds(3);
                Party_pop.SetActive(false);
                image.sprite = NormalAnswerButton;
                OddevenBox.SetActive(false);
                OddevenDropbox.SetActive(false);
                oddevenButtonPanel.SetActive(false);
                for (int i = OddevenDropbox.transform.childCount-1; i >=0; i--)
                {
                    OddevenDropbox.transform.GetChild(i).GetComponent<Image>().sprite = Normal;
                    OddevenDropbox.transform.GetChild(i).transform.parent = Holder;
                }

                Holder.gameObject.SetActive(true);
                nextbutton.gameObject.SetActive(true);
                droper.gameObject.SetActive(true);
                BOx.gameObject.SetActive(true);
                RelodingLevel();
            }
            else
            {
                wrongAnswer_animtion.SetActive(true);
                yield return new WaitForSeconds(2);
                image.sprite = NormalAnswerButton;
                wrongAnswer_animtion.SetActive(false);
            }

        }
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        public void RelodingLevel()
        {

          
            if (allno.Count >= 10)
            {
                StartCoroutine(LevelCompleted());
                return;
            }
            no = Random.Range(1, 11);        
            foreach (var item in allno)
            {
                if(item == no)
                {
                    RelodingLevel();
                    return;

                }

            }
            QuestionText.text = no.ToString();
            allno.Add(no);
        }
    }
}
