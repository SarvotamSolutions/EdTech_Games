using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Maths.Money.AddMoney
{

    public class Gamecontroll : Singleton<Gamecontroll>
    {
        
        public Color yellow, darkyellow,green,red;
       // public Color flickinganimtion;
        public GameObject dropcoin;
        public GameObject dropcoinfliker;

        public float totalmoneyadded;
        public TextMeshPro totalMoneyaddedtext;
        public float MoneyneedtoBuy;
        public TextMeshPro moneyneedto_buy_text;
        public SpriteRenderer pricetag,Priceplace;
        public Sprite cureectanswer, wronganswer, normalanswer;

        private int relod;
        public GameObject addmoneyimage;
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
            Priceplace.color = red;
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            Priceplace.color = yellow;
            ResetButton();
            wrongAnswer_animtion.SetActive(false);
            gamePlay = true;
        }

     
        private void Start()
        {
            StartCoroutine(addmoneyanimation());
            MoneyneedtoBuy = Random.Range(1, 10);
            int no = Random.Range(0, 50);
            float add = .05f * no;
            MoneyneedtoBuy += add;
            moneyneedto_buy_text.text = "$ "+ MoneyneedtoBuy.ToString("0.00") ;
        }
        IEnumerator addmoneyanimation()
        {
            dropcoinfliker.GetComponent<SpriteRenderer>().DOFade(0f, 1);
            addmoneyimage.transform.DOMoveY(addmoneyimage.transform.position.y + .1f, 1);
            yield return new WaitForSeconds(1);
            dropcoinfliker.GetComponent<SpriteRenderer>().DOFade(1f, 1);
            addmoneyimage.transform.DOMoveY(addmoneyimage.transform.position.y - .1f, 1);
            yield return new WaitForSeconds(1);
            StartCoroutine(addmoneyanimation());
        }
        public bool Neartodestination(GameObject objects)
        {
            if (Vector3.Distance(objects.transform.position, dropcoin.transform.position) < 2)
            {
               
                return true;

            }

            Debug.Log("XXXxx");
            return false;
        }

        public void NextButton()
        {
            gamePlay = false;
            if(totalMoneyaddedtext.text == moneyneedto_buy_text.text)
            {
                Priceplace.color = green;
                pricetag.sprite = cureectanswer;
                if (relod >= 10)
                {
                    StartCoroutine(LevelCompleted());
                    return;
                }
                StartCoroutine(waitForRelod());
            }
            else
            {
                pricetag.sprite = wronganswer;
                StartCoroutine(WrongAnswerAnimation());
               // resetbutton.SetActive(true);
            }
        }

        IEnumerator waitForRelod()
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            Party_pop.SetActive(false);
            relod++;
            Priceplace.color = yellow;
            pricetag.sprite = normalanswer;
            totalmoneyadded = 0;
            MoneyneedtoBuy = Random.Range(1, 10);
            int no = Random.Range(0, 50);
            float add = .05f * no;
            MoneyneedtoBuy += add;
            totalMoneyaddedtext.text = "$ " +totalmoneyadded.ToString("0.00");
            moneyneedto_buy_text.text = "$ "+ MoneyneedtoBuy.ToString("0.00");
            gamePlay = true;
        }

        public void ResetButton()
        {
            pricetag.sprite = normalanswer;
            addmoneyimage.SetActive(true);
            Priceplace.color = yellow;
            totalmoneyadded = 0;
            totalMoneyaddedtext.text = "$ "+ totalmoneyadded.ToString("0.00");
           // resetbutton.SetActive(false);
        }
    }
}