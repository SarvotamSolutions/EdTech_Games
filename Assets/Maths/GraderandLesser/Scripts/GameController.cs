using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum mode 
{
    AI,Drag
}


namespace Maths.graterAndLesser
{
    

    public class GameController : Singleton<GameController>
    {

        [Header("Ai")]
        public mode gamemode;
        public ExampleGestureHandler AiDrawtext;
        public GestureRecognizer.Recognizer Ai_recognizer;

        [Space(10)]
        public bool objects;
        public int objectid;

        [Space(10)]
        public TextMeshPro[] input_text;
        public int[] Numbers;
        public SpriteRenderer[] inputs;

        public Answer[] allsprite;

        public Sprite currectAnswer, WrongAnswer, normalAnswer;

        public SpriteRenderer firstbeads;
        public SpriteRenderer secondbeads;

        public GameObject graterlessparent, optioncontins;
        public List<int> allno= new List<int>();
        public int numbertimes;

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        private void Awake()
        {

            Numbers[0] = Random.Range(0, 9);
            Numbers[1] = Random.Range(0, 9);
           
            while (Numbers[0] == Numbers[1])
            {
                Numbers[1] = Random.Range(0, 9);
            }
            if (gamemode == mode.Drag)
            {
                int answeroption1 = Random.Range(0, 4);
                int answeroption2 = Random.Range(0, 4);

                while (answeroption1 == answeroption2)
                {
                    answeroption2 = Random.Range(0, 4);
                }
                for (int i = 0; i < 4; i++)
                {
                    if (answeroption2 == i)
                    {
                        allno.Add(Numbers[1] + 1);
                    }
                    else if (answeroption1 == i)
                    {
                        allno.Add(Numbers[0] + 1);

                    }
                    else
                    {
                        int no = Random.Range(0, 10);
                        for (int j = 0; j < allno.Count; j++)
                        {
                            if (no == allno[j] || no == Numbers[0] || no == Numbers[1])
                            {
                                no = Random.Range(1, 11);
                                j = -1;
                            }
                        }
                        allno.Add(no);
                    }
                }
            }
           


            if (objects)
            {
                objectid = Random.Range(0, allsprite.Length);
                for (int i = 0; i < Numbers[0] + 1; i++)
                {
                    firstbeads.transform.GetChild(i).gameObject.SetActive(true);
                    firstbeads.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = allsprite[objectid].beads;

                }
                for (int i = 0; i < Numbers[1] + 1; i++)
                {
                    secondbeads.transform.GetChild(i).gameObject.SetActive(true);
                    secondbeads.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = allsprite[objectid].beads;

                }
            }
            else
            {
                firstbeads.sprite = allsprite[Numbers[0]].beads;
                secondbeads.sprite = allsprite[Numbers[1]].beads;
            }
            Ai_recognizer.Recognigingnumber = (Numbers[0] + 1).ToString();
            Ai_recognizer.Changerecogniger();
        }
        private void Start()
        {
           
            
        }
        public int no;

        public void Answering()
        {

            no++;
            inputs[no].sortingOrder = 2;
            input_text[no].sortingOrder = 2;
            Debug.Log("no" + no);
            if (objects)
            {
                for (int i = 0; i < Numbers[1] + 1; i++)
                {

                    secondbeads.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 2;

                }
            }
            else
            {
                secondbeads.sortingOrder = 2;
            }
            input_text[no-1].text = (Numbers[no - 1]+1).ToString();
            inputs[no - 1].sprite = currectAnswer;
            inputs[no - 1].color = Color.white;
            inputs[no].color = Color.blue;
            AiDrawtext.textResult = input_text[no];
        }

        public void Settext()
        {
            gamePlay = false;
           
            if (no >= 2)
            {
                Debug.Log("ttttt");
                if(AiDrawtext.textResult.text =="<"&& Numbers[0] < Numbers[1])
                {
                    Debug.Log("Currect answer");
                    FinalCheck(inputs[2]);
                    inputs[2].sprite = currectAnswer;
                    inputs[2].color = Color.white;
                    //inputs[0].color = Color.blue;
                     AiDrawtext.textResult = null;
                    gamePlay = true;
                }
                else if (AiDrawtext.textResult.text == ">" && Numbers[0] > Numbers[1])
                {
                    Debug.Log("Currect answer");
                    FinalCheck(inputs[no]);
                    inputs[2].sprite = currectAnswer;

                    inputs[2].color = Color.white;
                    // inputs[0].color = Color.blue;
                    AiDrawtext.textResult = null;
                    gamePlay = true;
                }
                else
                {
                    AiDrawtext.textResult = null;
                    inputs[no].sprite = WrongAnswer;
                    StartCoroutine(WrongAnswerAnimation());
                }
                //inputs[no-1].sprite = currectAnswer;
                //inputs[no - 1].color = Color.white;
                ////     DrawCanvas.gameObject.SetActive(false);
                //AiDrawtext.textResult = null;

                    //GraterorLessselct = true;
                    //no = 0;
                    //    SecondButton.gameObject.SetActive(false);
            }
            else
            {

                if (AiDrawtext.no == (Numbers[no] + 1))
                {
                    Answering();

                   
                    if (no < 2)
                    {
                        Ai_recognizer.Recognigingnumber = (Numbers[no] + 1).ToString();
                        Ai_recognizer.Changerecogniger();
                    }
                    else
                    {
                        if (Numbers[0] < Numbers[1])
                        {
                            Ai_recognizer.Recognigingnumber = "<";
                            Ai_recognizer.Changerecogniger();
                        }
                        else if (Numbers[0] > Numbers[1])
                        {
                            Ai_recognizer.Recognigingnumber = ">";
                            Ai_recognizer.Changerecogniger();
                        }
                    }
                    gamePlay = true;

                    //firstbutton.gameObject.SetActive(false);
                    //SecondButton.gameObject.SetActive(true);
                }
                else
                {
                    AiDrawtext.textResult = null;
                    inputs[no].sprite = WrongAnswer;
                    StartCoroutine(WrongAnswerAnimation());
                }
            }
           
        

        }
        public void FinalButton()
        {
            //if (AiDrawtext.no == (Number2 + 1))
            //{
                
            //}
            //else
            //{
            //    secondInput.sprite = WrongAnswer;
            //    AiDrawtext.textResult = null;
            //    StartCoroutine(WrongAnswerAnimation());

            //}
        }

        public void WrongAnimation()
        {
            StartCoroutine(WrongAnswerAnimation());
        }
        public GameObject drager;
        IEnumerator WrongAnswerAnimation()
        {
            inputs[no].color = Color.white;
            inputs[no].sprite = WrongAnswer;
           
            wrongAnswer_animtion.SetActive(true);
            Debug.Log("Coming");

            yield return new WaitForSeconds(2);
            Debug.Log("not coming");
            inputs[no].sprite = normalAnswer;
            inputs[no].color = Color.blue;
            //inputs[0].sprite = inputs[0].sprite == currectAnswer ? currectAnswer : normalAnswer;
            input_text[no].text = "";
            //Firsttext.text = inputs[0].sprite == currectAnswer ? Firsttext.text : "";
            //SecondText.text = "";
            AiDrawtext.textResult = input_text[no];
            if (drager)
                drager.SetActive(true);
            //AiDrawtext.textResult = Firsttext.text == "" ? Firsttext : SecondText;
          //  inputs[1].sprite =normalAnswer;
            wrongAnswer_animtion.SetActive(false);
            gamePlay = true;
        }


        public void FinalCheck(SpriteRenderer sprite)
        {
          
            //sprite.sprite = currectAnswer;
            if (!objects)
            {
                inputs[0].sprite = allsprite[Numbers[0]].box;
                inputs[1].sprite = allsprite[Numbers[1]].box;
            }
            else
            {
                inputs[0].sprite = currectAnswer;
                inputs[1].sprite = currectAnswer;
                inputs[2].sprite = currectAnswer;
            }
            numbertimes++;
            if (numbertimes < 10)
            {
                StartCoroutine(Relodlevel(sprite));
            }
            else
            {
                StartCoroutine(LevelCompleted());
            }

        }
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        IEnumerator Relodlevel(SpriteRenderer obj)
        {
            no = 0;
            Party_pop.SetActive(true);
            AiDrawtext.transform.parent.transform.gameObject.SetActive(false);
            yield return new WaitForSeconds(3);
            Party_pop.SetActive(false);
         
          
            inputs[0].color = Color.blue;
            foreach (var item in input_text)
            {
             
                item.sortingOrder = 0;
                item.text = "";
            }
            foreach (var item in inputs)
            {
                item.sortingOrder = 0;
                item.sprite = normalAnswer;
            }


            inputs[0].sortingOrder = 2;
            input_text[0].sortingOrder = 2;

            
          //  inputs[no].color = Color.white;
            AiDrawtext.transform.parent.transform.gameObject.SetActive(true);
            Numbers[0] = Random.Range(0, 9);
            Numbers[1] = Random.Range(0, 9);
            while (Numbers[0] == Numbers[1])
            {
                Numbers[1] = Random.Range(0, 9);
            }

            if (gamemode == mode.Drag)
            {
                allno.Clear(); 
                optioncontins.SetActive(true);
                graterlessparent.SetActive(false);


                int answeroption1 = Random.Range(0, 4);
                int answeroption2 = Random.Range(0, 4);

                while (answeroption1 == answeroption2)
                {
                    answeroption2 = Random.Range(0, 4);
                }
                for (int i = 0; i < 4; i++)
                {
                    if (answeroption2 == i)
                    {
                        allno.Add(Numbers[1] + 1);
                    }
                    else if (answeroption1 == i)
                    {
                        allno.Add(Numbers[0] + 1);

                    }
                    else
                    {
                        int no = Random.Range(0, 10);
                        for (int j = 0; j < allno.Count; j++)
                        {
                            if (no == allno[j] || no == Numbers[0] || no == Numbers[1])
                            {
                                no = Random.Range(1, 11);
                                j = -1;
                            }
                        }
                        allno.Add(no);
                    }
                }

                for (int i = 0; i < optioncontins.transform.childCount; i++)
                {
                    optioncontins.transform.GetChild(i).gameObject.SetActive(true);
                    optioncontins.transform.GetChild(i).GetComponent<Dragin>().TextUpdate();

                }
                for (int i = 0; i < graterlessparent.transform.childCount; i++)
                {
                    graterlessparent.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            AiDrawtext.textResult = input_text[no];
          //  AiDrawtext.textResult = Firsttext;
            Ai_recognizer.Recognigingnumber = (Numbers[0] + 1).ToString();
            Ai_recognizer.Changerecogniger();


            if (objects)
            {
                for (int i = 0; i < Numbers[1] + 1; i++)
                {

                    secondbeads.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 0;

                }

                for (int i = 0; i < firstbeads.transform.childCount; i++)
                {
                    firstbeads.transform.GetChild(i).gameObject.SetActive(false);
                    secondbeads.transform.GetChild(i).gameObject.SetActive(false);
                }

                objectid = Random.Range(0, allsprite.Length);
                for (int i = 0; i < Numbers[0] + 1; i++)
                {
                    firstbeads.transform.GetChild(i).gameObject.SetActive(true);
                    firstbeads.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = allsprite[objectid].beads;

                }
                for (int i = 0; i < Numbers[1] + 1; i++)
                {
                    secondbeads.transform.GetChild(i).gameObject.SetActive(true);
                    secondbeads.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = allsprite[objectid].beads;

                }
            }
            else
            {

                firstbeads.sprite = allsprite[Numbers[0]].beads;
                secondbeads.sprite = allsprite[Numbers[1]].beads;
                secondbeads.sortingOrder = 0;
            }
            gamePlay = true;
            
        //    inputs[0].sprite = normalAnswer;
        //    inputs[1].sprite = normalAnswer;
        //    inputs[2].sprite = normalAnswer;
        //  //  SecondText.GetComponent<TextMeshPro>().text = "";
        //  //  Firsttext.GetComponent<TextMeshPro>().text = "";
        //    firstbutton.SetActive(true);
          
        //   // DrawCanvas.SetActive(true);
        ////    obj.sprite = inp;
        ////    obj.transform.position = obj.GetComponent<Dragin>().lastpos;

        }




    }
}
[System.Serializable]
public class Answer
{
    public Sprite beads;
    public Sprite box;
}