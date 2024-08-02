using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

namespace Maths.Money.AddMoney
{

    public class Gamecontroll : Singleton<Gamecontroll>
    {
        public GameObject FirstScreen;
        public GameObject secondScreen;
        public GameObject Secondscrreenbackground;
        public Totorial totorial;
        
        public Color yellow, darkyellow,green,red,PriceColor;
       // public Color flickinganimtion;
        public GameObject dropcoin;
        public GameObject dropcoinfliker;

        public float totalmoneyadded;
        public TextMeshPro totalMoneyaddedtext;
        public float MoneyneedtoBuy;
        public TextMeshPro moneyneedto_buy_text;
        public SpriteRenderer CashInput,Priceplace;
        public SpriteRenderer Sellingtoy;
        public Sprite cureectanswer, wronganswer, normalanswer;

        public Button Done;

        private int relod;
        public GameObject addmoneyimage;
        public int selectedtoy;

        public SpriteRenderer[] alltoys;
        public float[] allprice;
        public Sprite selctedtosimage,notselected;
        IEnumerator Checkingplayerslectedtoys;

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
            totalMoneyaddedtext.color = PriceColor;
            gamePlay = true;
            Done.interactable = true;

        }

 

        public void SelectToys(int id)
        {
            if (Checkingplayerslectedtoys != null)
                StopCoroutine(Checkingplayerslectedtoys);

            Checkingplayerslectedtoys = LoadForNextStep();

            foreach (var item in alltoys)
            {
                item.sprite = notselected;
           //     item.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            }
            Sellingtoy.sprite = alltoys[id].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            selectedtoy = id;
            alltoys[id].sprite = selctedtosimage;
           // alltoys[id].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            MoneyneedtoBuy = allprice[id];
            StartCoroutine(Checkingplayerslectedtoys);

        }
        IEnumerator LoadForNextStep()
        {
            yield return new WaitForSeconds(2);

            FirstScreen.SetActive(false);
            secondScreen.SetActive(true);

            Done.gameObject.SetActive(true);
            Secondscrreenbackground.SetActive(true);
            alltoys[selectedtoy].GetComponent<BoxCollider2D>().enabled = false;
            foreach (var item in alltoys)
            {
                item.sprite = notselected;
                //item.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
            }
            alltoys[selectedtoy].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
            alltoys[selectedtoy].transform.GetChild(1).gameObject.SetActive(true);
            //   MoneyneedtoBuy += add;
            moneyneedto_buy_text.text = "$ " + MoneyneedtoBuy.ToString("0.00");
            totorial.directionWindow();
            
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


            return false;
        }

        public void NextButton()
        {
            if (totorial.totorialplaying)
                return;
            gamePlay = false;
            Done.interactable = false;
            if(totalMoneyaddedtext.text == moneyneedto_buy_text.text)
            {
                Priceplace.color = green;
                totalMoneyaddedtext.color = Color.white;
                CashInput.sprite = cureectanswer;
                if (relod >= 9)
                {
                    StartCoroutine(LevelCompleted());
                    return;
                }
                Done.gameObject.SetActive(false);
               
                StartCoroutine(waitForRelod());
            }
            else
            {
                CashInput.sprite = wronganswer;
                totalMoneyaddedtext.color = Color.white;

                StartCoroutine(WrongAnswerAnimation());
               // resetbutton.SetActive(true);
            }
        }

        IEnumerator waitForRelod()
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            Secondscrreenbackground.SetActive(false);
            Party_pop.SetActive(false);
            relod++;
            Priceplace.color = yellow;
            totalMoneyaddedtext.color = PriceColor;
            CashInput.sprite = normalanswer;
            totalmoneyadded = 0;
            totalMoneyaddedtext.text = "$ " +totalmoneyadded.ToString("0.00");
            FirstScreen.SetActive(true);
            secondScreen.SetActive(false);
            //MoneyneedtoBuy = Random.Range(1, 10);
            //int no = Random.Range(0, 50);
            //float add = .05f * no;
            //MoneyneedtoBuy += add;
            //moneyneedto_buy_text.text = "$ "+ MoneyneedtoBuy.ToString("0.00");
            gamePlay = true;
            Done.interactable = true;

        }

        public void ResetButton()
        {
            CashInput.sprite = normalanswer;
            addmoneyimage.SetActive(true);
            Priceplace.color = yellow;
            totalmoneyadded = 0;
            totalMoneyaddedtext.text = "$ "+ totalmoneyadded.ToString("0.00");
           // resetbutton.SetActive(false);
        }
    }
}