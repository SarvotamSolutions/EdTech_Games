using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using GestureRecognizer;

namespace Maths.Addision.BeadsCount
{
    public class GameController :Singleton<GameController>
    {

        [Header("Ai")]
        public ExampleGestureHandler AiDrawtext;
        public GestureRecognizer.Recognizer Ai_recognizer;
       // public GameObject DrawCanvas;

        [Space(10)]
        public GameObject particalbalst;
        public GameObject PartyBlast;

        public TextMeshPro[] inputText;


        public SpriteRenderer[] spriteinputs;
        public SpriteRenderer firstbeads;
        public SpriteRenderer secondbeads;

        public Answer[] allsprite;
        public static GameController instance;

        public int[] Numbers;

        public GameObject firstbutton;
        public GameObject SecondButton;
        public GameObject ThirdButton;

        public Sprite wrongAnswer,currectanswer,NormalInput;
        public GameObject[] allOptionAnswer;

        public List<int> alloption = new List<int>(3);

        private int reloding;

        public float currectanswerInterval;
        public float wronganswerInterval;
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
        IEnumerator WrongAnswerAnimation(SpriteRenderer input)
        {
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            input.sprite = NormalInput;
            AiDrawtext.textResult = input.transform.GetChild(0).GetComponent<TextMeshPro>();
            input.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            wrongAnswer_animtion.SetActive(false);
            gamePlay = true;
        }
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            Relod();

        }
        //public void ParticalEffect()
        //{
        //    if(AiDrawtext.checkNo())
        //    {
        //        particalbalst.transform.position =
        //    }

        //}
        public Color nonselctedcolor,selectedColor;
        int no;
        public void SetFirsttext()
        {
            if (AiDrawtext.no == (Numbers[no] + 1))
            {

                if (no < 2)
                {
                    //    particalbalst.transform.position = firstinput.transform.position;
                    //   particalbalst.GetComponent<ParticleSystem>().startColor = Color.green;
                    // particalbalst.GetComponent<ParticleSystem>().Play();

                   
                    spriteinputs[no].sprite = allsprite[Numbers[no]].box;
                    AiDrawtext.textResult = inputText[no + 1];
                   // firstbutton.gameObject.SetActive(false);
                   // SecondButton.gameObject.SetActive(true);
                    Ai_recognizer.Recognigingnumber = (Numbers[no + 1] + 1).ToString();
                    Ai_recognizer.Changerecogniger();
                    no++;
                  
                    if (no == 2)
                    {

                        AiDrawtext.transform.parent.gameObject.SetActive(false);
                        //AiDrawtext.GetComponent<DrawDetector>().enabled = false;
                        ThirdButton.gameObject.SetActive(true);
                        
                        spriteinputs[no-1].color = nonselctedcolor;
                        spriteinputs[no].color = selectedColor;
                        no = 0;
                        //spriteinputs[2].sprite = allsprite[Numbers[1]].box;
                        //AiDrawtext.textResult = null;
                        //SecondButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        spriteinputs[no].color = selectedColor;
                        spriteinputs[no - 1].color = nonselctedcolor;
                    }
                }
                else
                {
                   // particalbalst.transform.position = spriteinputs[no].transform.position;
                  //  particalbalst.GetComponent<ParticleSystem>().startColor = Color.green;
                    //particalbalst.GetComponent<ParticleSystem>().Play();
                    
                }
                
            }
            else
            {
                AiDrawtext.textResult = null;
                particalbalst.transform.position = spriteinputs[no].transform.position;
               // particalbalst.GetComponent<ParticleSystem>().startColor = Color.red;
              //  particalbalst.GetComponent<ParticleSystem>().Play();
                spriteinputs[no].sprite = wrongAnswer;
                StartCoroutine(WrongAnswerAnimation(spriteinputs[no]));
            }


        }

        public GameObject canvaspanel;
        IEnumerator WaitRelod()
        {
            AiDrawtext.textResult = inputText[0];
            canvaspanel.SetActive(false);
            PartyBlast.gameObject.SetActive(true);
            yield return new WaitForSeconds(6);
            canvaspanel.SetActive(true);
            PartyBlast.gameObject.SetActive(false);
            for (int i = 0; i < alloption.Count; i++)
            {
                alloption[i] = 0;
            }

            foreach (var item in allOptionAnswer)
            {
                item.SetActive(true);
            }
            foreach (var item in spriteinputs)
            {
                item.sprite = NormalInput;
            }
            spriteinputs[0].color = selectedColor;
            spriteinputs[2].color = nonselctedcolor;
            ThirdButton.gameObject.SetActive(false);
            firstbutton.SetActive(true);
           
           // DrawCanvas.SetActive(true);
            Relod();

        }
        public void Relod()
        {

            if(reloding >= 10)
            {
                StartCoroutine(LevelCompleted());
                return;
            }

            foreach (var item in inputText)
            {
                item.text = "";

            }
          
            reloding++;
            Numbers[0] = Random.Range(0, 9);
            Numbers[1] = Random.Range(0, 9);
            while(Numbers[0] == Numbers[1])
            {
                Numbers[1] = Random.Range(0, 9);
            }
            int no1 = Numbers[0] + 1;
            int no2 = Numbers[1] + 1;
            Ai_recognizer.Recognigingnumber = no1.ToString();
            Ai_recognizer.Changerecogniger();
            Numbers[2] = no1 + no2;
            firstbeads.sprite = allsprite[Numbers[0]].beads;
            secondbeads.sprite = allsprite[Numbers[1]].beads;
            int answeroption = Random.Range(0, 3);
            alloption[answeroption] = Numbers[2];
            for (int i = 0; i < alloption.Count; i++)
            {
                if (i == answeroption)
                {
                    allOptionAnswer[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = Numbers[2].ToString();
                    allOptionAnswer[i].GetComponent<Draging>().no = Numbers[2];
                }
                else
                {
                    int no = Random.Range(5, 19);
                    for (int j = 0; j < alloption.Count; j++)
                    {
                        if (no == alloption[i])
                        {
                            no = Random.Range(5, 19);
                            j = -1;
                        }

                    }
                    allOptionAnswer[i].GetComponent<Draging>().no = no;
                    allOptionAnswer[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = no.ToString();
                    alloption[i] = no;


                }
                gamePlay = true;
            }
        }
        public bool Neartodestination(GameObject obj)
        {

            if (Vector3.Distance(obj.transform.position, spriteinputs[2].transform.position) < 1)
            {
                gamePlay = false;

                if(obj.GetComponent<Draging>().no == Numbers[2])
                {
                    AiDrawtext.transform.parent.gameObject.SetActive(true);
                  //  AiDrawtext.GetComponent<DrawDetector>().enabled = true;
                    particalbalst.transform.position = spriteinputs[2].transform.position;
                    particalbalst.GetComponent<ParticleSystem>().startColor = Color.green;
                    particalbalst.GetComponent<ParticleSystem>().Play();
                    spriteinputs[2].sprite = currectanswer;
                    spriteinputs[2].transform.GetChild(0).GetComponent<TextMeshPro>().text = Numbers[2].ToString();
                    spriteinputs[2].color = Color.white;
                    StartCoroutine(WaitRelod());
                }
                else
                {
                    particalbalst.transform.position = spriteinputs[2].transform.position;
                    particalbalst.GetComponent<ParticleSystem>().startColor = Color.red;
                    particalbalst.GetComponent<ParticleSystem>().Play();
                    spriteinputs[2].sprite = wrongAnswer;
                    spriteinputs[2].transform.GetChild(0).GetComponent<TextMeshPro>().text =obj.GetComponent<Draging>().no.ToString();
                    StartCoroutine(WrongAnswerAnimation(spriteinputs[2]));
                    return false;
                }
                return true;
            }

            return false;
        }
    }
}