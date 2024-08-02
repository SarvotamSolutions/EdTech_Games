using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.Addision.AddisitonwithColors
{
    public class GameCondtroller : Singleton<GameCondtroller>
    {
        public Totorial totorial;
        public ParticleSystem Particalblast;

        public ColorSkeckPen[] all_CollerSelection;
        public GameObject[] DragOption;
        public AllCollors selectedcollor;

        public Sprite[] allBeadsWithColors,allbeadswithoutColors;

        public int number1, number2;

        public SpriteRenderer withoutcolors1, withoutcolor2;
        public SpriteRenderer withcolor1, withcolor2;
        public List<int> alloptionno = new List<int>();

        public GameObject stage1, stage2;
        public int stage1Colored;

        [Space(15)]
        public SpriteRenderer firstBridestage2;
        public SpriteRenderer Secondbridestage2;
        public SpriteRenderer currectanswerimage;
        public TextMeshPro currentanswertext;
        public GameObject inputfild1;
        public GameObject inputfile2;
        public Sprite currectAnswer,NormalInput;
        public GameObject dropplace;

        [Space(15)]
        public GameObject stage3;
        public TextMeshPro Stgethree_number1_text, Stgethree_number2_text;
        public SpriteRenderer stagethree_bread1, stagethree_bread2;
        public GameObject[] Calculationtotall;
        public int[] allanswer;
        public int Total;
        public Sprite Currectanswer, WrongAnswer, normalAnswer;

        public int reloding;

        public BeadsSelecting bead1, bead2;

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        public float currectanswerinteval;
        public float wronganswerinterval;

        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }
       
        private void Start()
        {
           
            StartingGame();
        }
        void GameStart()
        {

        }
        public void ResetingGame()
        {
            stage1Colored = 0;
            stage3.SetActive(false);
            stage1.SetActive(true);
            for (int i = 0; i < alloptionno.Count; i++)
            {
                alloptionno[i] = -1;

            }
            StartingGame();
        }
        private void StartingGame()
        {
            reloding++;

            if (reloding > 10)
            {
                StartCoroutine(LevelCompleted());
                return;
            }
            number1 = Random.Range(0, allBeadsWithColors.Length);
            number2 = Random.Range(0, allBeadsWithColors.Length);
            while (number2 == number1)
            {
                number2 = Random.Range(0, allBeadsWithColors.Length);
            }
           
          // Particalblast.shape.position = new Vector3(0, number1 / 2f, 0);
            Total = (number1 + 1) + (number2 + 1);
            int option1answer = Random.Range(0, DragOption.Length);
            int option2answer = Random.Range(0, DragOption.Length);

            while (option1answer == option2answer)
            {
                option2answer = Random.Range(0, DragOption.Length);
            }
            alloptionno[option1answer] = number1;
            alloptionno[option2answer] = number2;

            for (int i = 0; i < DragOption.Length; i++)
            {
                if (i == option1answer || i == option2answer)
                {
                    DragOption[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = (alloptionno[i] + 1).ToString();
                    DragOption[i].GetComponent<Drag>().no = alloptionno[i];
                }
                else
                {
                    int no = Random.Range(0, 10);
                    for (int j = 0; j < alloptionno.Count; j++)
                    {
                        if (no == alloptionno[j])
                        {
                            no = Random.Range(0, 10);
                            j = -1;
                        }
                    }
                    alloptionno[i] = no;
                    DragOption[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = (alloptionno[i] + 1).ToString();
                    DragOption[i].GetComponent<Drag>().no = alloptionno[i];
                }
            }


            withcolor1.sprite = allBeadsWithColors[number1];
            withcolor2.sprite = allBeadsWithColors[number2];
            withoutcolors1.sprite = allbeadswithoutColors[number1];
            withoutcolor2.sprite = allbeadswithoutColors[number2];
            withoutcolors1.GetComponent<BeadsSelecting>().color = (AllCollors)number1;
            withoutcolor2.GetComponent<BeadsSelecting>().color = (AllCollors)number2;
            gamePlay = true;
        }
        public void SetStage2()
        {
            stage1.SetActive(false);
            bead1.clicked = false;
            bead2.clicked = false;
            firstBridestage2.sprite = allBeadsWithColors[number1];
            Secondbridestage2.sprite = allBeadsWithColors[number2];
            inputfild1.GetComponent<SpriteRenderer>().sprite = NormalInput;
            inputfile2.GetComponent<SpriteRenderer>().sprite = NormalInput;
            inputfile2.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            inputfild1.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            foreach (var item in DragOption)
            {
                item.SetActive(true);
            }
            stage2.SetActive(true);


        }

        public bool Neartodestination2(GameObject obj)
        {
            if (Vector3.Distance(obj.transform.position, dropplace.transform.position) < 1)
            {
                gamePlay = false;
                //obj.transform.position = dropplace.transform.position;
             //   obj.SetActive(false);
                return true;
            }
                return false;
        }
        public IEnumerator Wairrelod(GameObject obj)
        {
            obj.SetActive(false);
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(currectanswerinteval);
            currentanswertext.text = "";
            //GetComponent<SpriteRenderer>().enabled = true;
            currectanswerimage.sprite = NormalInput;
            obj.transform.position = obj.GetComponent<SelectAnswer>().lastpos;
            obj.SetActive(true);
            Party_pop.SetActive(false);
            ResetingGame();
        }

        public IEnumerator WrongAnimtaion(GameObject obj)
        {
            obj.SetActive(false);
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(wronganswerinterval);
            currentanswertext.text = "";
            //GetComponent<SpriteRenderer>().enabled = true;
            currectanswerimage.sprite = NormalInput;
            obj.transform.GetComponent<SpriteRenderer>().sprite = normalAnswer;
            wrongAnswer_animtion.SetActive(false);
            obj.SetActive(true);
            obj.transform.position = obj.GetComponent<SelectAnswer>().lastpos;
            gamePlay = true;



            
        }

        public bool Neartodestination(GameObject obj)
        {
            
            if(Vector3.Distance(obj.transform.position,inputfild1.transform.position)<1 && obj.GetComponent<Drag>().no == number1)
            {
               
                float ypos = (float)number1 / 2f;
                Particalblast.gameObject.transform.position = withoutcolors1.transform.position;
                Vector3 pos = new Vector3(0, ypos, 0);
                var ph = Particalblast.shape;
                ph.position = pos;
                ph.scale = new Vector3(1, number1, 1);
                Particalblast.Play();
                inputfild1.transform.GetChild(0).GetComponent<TextMeshPro>().text = (number1 + 1).ToString();
                inputfild1.GetComponent<SpriteRenderer>().sprite = currectAnswer;
                return true;
            }
            else if (Vector3.Distance(obj.transform.position, inputfild1.transform.position) < 1 && obj.GetComponent<Drag>().no != number1)
            {
                gamePlay = false;
                float ypos = (float)number1 / 2f;
                Particalblast.gameObject.transform.position = withoutcolors1.transform.position;
                Vector3 pos = new Vector3(0, ypos, 0);
                var ph = Particalblast.shape;
                ph.position = pos;
                ph.scale = new Vector3(1, number1, 1);
                Particalblast.Play();
                inputfild1.transform.GetChild(0).GetComponent<TextMeshPro>().text = (obj.GetComponent<Drag>().no + 1).ToString();
                inputfild1.GetComponent<SpriteRenderer>().sprite = WrongAnswer;
                StartCoroutine(WrongAnswerAnimation(inputfild1.GetComponent<SpriteRenderer>()));

            }


            if (Vector3.Distance(obj.transform.position, inputfile2.transform.position) < 1 && obj.GetComponent<Drag>().no == number2)
            {

                float ypos = (float)number2 / 2f;
                Particalblast.gameObject.transform.position = withoutcolor2.transform.position;
                Vector3 pos = new Vector3(0, ypos, 0);
                var ph = Particalblast.shape;
                ph.position = pos;
                ph.scale = new Vector3(1, number2, 1);
                inputfile2.transform.GetChild(0).GetComponent<TextMeshPro>().text = (number2 + 1).ToString();
                inputfile2.GetComponent<SpriteRenderer>().sprite = currectAnswer;
                return true;
            }
            else if (Vector3.Distance(obj.transform.position, inputfile2.transform.position) < 1 && obj.GetComponent<Drag>().no != number2)
            {
                gamePlay = false;
                float ypos = (float)number2 / 2f;
                Particalblast.gameObject.transform.position = withoutcolor2.transform.position;
                Vector3 pos = new Vector3(0, ypos, 0);
                var ph = Particalblast.shape;
                ph.position = pos;
                ph.scale = new Vector3(1, number2, 1);
                inputfile2.transform.GetChild(0).GetComponent<TextMeshPro>().text = (obj.GetComponent<Drag>().no + 1).ToString();
                inputfile2.GetComponent<SpriteRenderer>().sprite = WrongAnswer;
                StartCoroutine(WrongAnswerAnimation(inputfile2.GetComponent<SpriteRenderer>()));
            }

            return false;
        }

        IEnumerator WrongAnswerAnimation(SpriteRenderer input)
        {
            gamePlay = false;
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            input.sprite = NormalInput;
            input.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            wrongAnswer_animtion.SetActive(false);
            gamePlay = true;
        }
        public bool checkForThirdStage()
        {
            bool setstargeone = false;
            bool SetStagetwo = false;
            if(inputfild1.transform.GetChild(0).GetComponent<TextMeshPro>().text == (number1+1).ToString())
            {
                setstargeone = true;
            }
            if (inputfile2.transform.GetChild(0).GetComponent<TextMeshPro>().text == (number2 + 1).ToString())
            {
                SetStagetwo = true;
            }

            if (setstargeone && SetStagetwo)
            {
                stage2.gameObject.SetActive(false);
                foreach (var item in Calculationtotall)
                {
                    item.GetComponent<SpriteRenderer>().sprite = normalAnswer;
                }

                Stgethree_number1_text.text = (number1 + 1).ToString();
                Stgethree_number2_text.text = (number2 + 1).ToString();
                stagethree_bread1.sprite = allBeadsWithColors[number1];
                stagethree_bread2.sprite = allBeadsWithColors[number2];
                int ansoption = Random.Range(0, Calculationtotall.Length);
                allanswer[ansoption] = Total;
                for (int i = 0; i < Calculationtotall.Length; i++)
                {
                    if(i== ansoption)
                    {
                        Calculationtotall[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = Total.ToString();
                        Calculationtotall[i].GetComponent<SelectAnswer>().no = Total;
                    }
                    else
                    {
                        int no = Random.Range(4, 20);
                        for (int j = 0; j < allanswer.Length; j++)
                        {
                            if(allanswer[j] == no)
                            {
                                no = Random.Range(4, 20);
                                j = -1;
                            }

                        }
                        Calculationtotall[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = no.ToString();
                        Calculationtotall[i].GetComponent<SelectAnswer>().no = no;
                        allanswer[i] = no;
                    }
                }
                stage3.gameObject.SetActive(true);
                return true;
            }
            return false;
        }
        public void ResetColler()
        {

            for (int i = 0; i < all_CollerSelection.Length; i++)
            {

                all_CollerSelection[i].Selectedobj.SetActive(false);

            }

        }
    }
}