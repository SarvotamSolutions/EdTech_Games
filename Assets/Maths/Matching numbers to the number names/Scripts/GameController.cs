using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


namespace Maths.matchingNumbers
{
    public class GameController : Singleton<GameController>
    {
    
        public LineRenderer selected_line;

        public GameObject[] allAnsweroption;
        public Selectbox[] allstringOption;

        public NumberwithText[] alltext;
        public int[] selctedOption;

        public int totalanswered;

        public bool placevalue;
        public Sprite currectanswer, currectAnswerOption,selectanswer,selectOption,wronganswer,wrongansweroption;
        public bool mulitipleloop;

        public Material wrongmatrial, currectmateral, normalmateral;
        public bool textmatch;
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
        public Color currectanswer_Colors, wronganswer_colors, selectedanswer_colors;
        public int addno;
        public int relidingtime;
        
        // Start is called before the first frame update
        void Start()
        {
            GameStart();
         
        }
        public List<int> answertext = new List<int>();
        void Reseting()
        {
            relidingtime++;
            if (relidingtime > 5)
            {
                StartCoroutine(LevelCompleted());
                return;
            }
            totalanswered = 0;
            for (int i = 0; i < selctedOption.Length; i++)
            {
                answertext.Add(selctedOption[i]);
            }
            foreach (var item in allstringOption)
            {
                item.Reseting();
            }

            GameStart();
        }
        void GameStart()
        {
            bool loaded = false;
            for (int i = 0; i < selctedOption.Length; i++)
            {

                if (!placevalue)
                {
                    int no = Random.Range(0, alltext.Length);

                    
                    for (int j = 0; j < selctedOption.Length; j++)
                    {

                        if (answertext.Count < 25)
                        {
                            Debug.Log(answertext.Count);
                            for (int k  = 0; k < answertext.Count; k++)
                            {
                                if (no == answertext[k])
                                {
                                    no = Random.Range(0, alltext.Length);
                                    k = -1;
                                }
                            }
                            if (no == selctedOption[j] - addno)
                            {



                                no = Random.Range(0, alltext.Length);
                                j = -1;


                            }
                        }
                        else
                        {
                            int reminno =0;
                            if (!loaded)
                            {
                                for (int k = 0; k < answertext.Count; k++)
                                {
                                    if (no == answertext[k])
                                    {
                                        no = Random.Range(0, alltext.Length);
                                        k = -1;
                                        Debug.Log("checking");

                                    }
                                    else
                                    {
                                        loaded = true;
                                        Debug.Log("breaking");
                                        reminno = no;
                                        //  break;
                                    }
                                }

                            }
                            Debug.Log(reminno);
                            if (no == selctedOption[j] - addno)
                            {



                                no = Random.Range(0, alltext.Length);
                                j = -1;

                            }

                        }
                        

                    }


                    selctedOption[i] = no + addno;
                    allstringOption[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = alltext[no].nowithstring;
                }
                else
                {
                    int no = Random.Range(11, 99);
                    for (int j = 0; j < selctedOption.Length; j++)
                    {
                        if (no == selctedOption[j] - addno)
                        {
                            no = Random.Range(11, 99);
                            j = -1;
                        }
                    }
                    selctedOption[i] = no + addno;
                    allstringOption[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = "" + (selctedOption[i] / 10) + " tens and " + (selctedOption[i] % 10) + " ones ";
                    //    allstringOption[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = alltext[no].nowithstring;
                }
            }
            Switch();
            for (int i = 0; i < allAnsweroption.Length; i++)
            {
                if (placevalue)
                {

                    allAnsweroption[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = (selctedOption[i]).ToString();
                }
                else
                    allAnsweroption[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = alltext[selctedOption[i]].no.ToString();
            }

            if (!placevalue)
            {
                for (int i = 0; i < allstringOption.Length; i++)
                {
                    for (int j = 0; j < alltext.Length; j++)
                    {
                        if (allstringOption[i].transform.GetChild(1).GetComponent<TextMeshPro>().text == alltext[j].nowithstring)
                        {

                            for (int k = 0; k < allAnsweroption.Length; k++)
                            {
                                Debug.Log(allAnsweroption[k].transform.GetChild(1).GetComponent<TextMeshPro>().text);
                                if (alltext[j].no.ToString() == allAnsweroption[k].transform.GetChild(1).GetComponent<TextMeshPro>().text)
                                {
                                    allstringOption[i].GetComponent<Selectbox>().Answeroption = allAnsweroption[k];
                                }
                            }
                        }
                    }

                }
            }
            else
            {
                for (int i = 0; i < allstringOption.Length; i++)
                {


                    for (int k = 0; k < selctedOption.Length; k++)
                    {
                        Debug.Log(allstringOption[i].transform.GetChild(1).GetComponent<TextMeshPro>().text + "   ." + "" + (selctedOption[k] / 10) + " tens and " + (selctedOption[i] % 10) + " ones ");
                        if (allstringOption[i].transform.GetChild(1).GetComponent<TextMeshPro>().text == "" + (selctedOption[k] / 10) + " tens and " + (selctedOption[k] % 10) + " ones ")
                        {

                            allstringOption[i].GetComponent<Selectbox>().Answeroption = allAnsweroption[k];
                            //k = 0;
                        }
                    }

                }
            }

        }
        public void ScenecChange()
        {
            if(!mulitipleloop)
                StartCoroutine(LevelCompleted());
            else
            {
                Reseting();
               
            }
        }
        void Switch()
        {
            for (int i = 0; i < 5; i++)
            {
                int tempno = selctedOption[i]-addno;

                int no = Random.Range(0, selctedOption.Length);
           
                selctedOption[i] = selctedOption[no];
                selctedOption[no] = tempno +addno;

            }



          
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            if (selected_line) 
                selected_line.SetPosition(1, pos);

        }

        public void currectAnswer() { }
        public void WrongAnswer() { }

    }
    [System.Serializable]
    public class NumberwithText 
    {
        public string no;
        public string nowithstring;
    
    }


}
