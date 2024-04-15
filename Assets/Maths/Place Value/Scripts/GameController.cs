using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


namespace Maths.placeHolder.selectNO
{
    public class GameController : MonoBehaviour
    {
        public Sprite[] iconsprite;
        public SpriteRenderer icon;
        public GameObject[] AnswerButton;
        public Sprite currectanswerButton;
        public Sprite normalanswer;
        public Sprite wronganswerButton;
        public int no;
        private List<int> allno = new List<int>();

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

        public void Start()
        {
            no = Random.Range(0, 3);
            icon.sprite = iconsprite[no];

            allno.Add(no);

        }

        public void Answer(int answerNO)
        {
            if(answerNO == no)
            {
                AnswerButton[no].GetComponent<Image>().sprite = currectanswerButton;
                if (allno.Count >= 3)
                {
                    StartCoroutine(LevelCompleted());
                     return;
                }
                StartCoroutine(WaitForrelod());
            }
            else
            {
                AnswerButton[answerNO].GetComponent<Image>().sprite = wronganswerButton;
                StartCoroutine(WrongAnswerAnimation());
            }
        }

        IEnumerator WaitForrelod()
        {
            yield return new WaitForSeconds(1f);

            foreach (var item in AnswerButton)
            {
                item.GetComponent<Image>().sprite = normalanswer;
            }
            no = Random.Range(0, 3);


         
            for (int i = 0; i < allno.Count; i++)
            {
                if(allno[i] == no)
                {
                    i = -1;
                    no = Random.Range(0, 3);
                }

            }

            icon.sprite = iconsprite[no];

            allno.Add(no);

        }
    }
}
