
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Laguage;
using TMPro;
using UnityEngine.SceneManagement;

public class GameControllerforAll : Singleton<GameControllerforAll>
{
    [Header("Color selection")]
    public Color currect_answer_color;
    public Color wrong_answer_color;
    public Color sellect_answer_color;

    [Space(15)]
    [Header("Question")]
    public bool TextQuestion;
    public TextMeshPro question_text;
    public SpriteRenderer Icon;
    public SpriteRenderer Boarder;
   
    public int letterno;

    [Space(15)]
    [Header("Options")]
    public DragerForall[] alloption;
    public DragerForall selectedoption;

    [Space(15)]
    public SpriteRenderer[] droping_place;
    public AllCharacter[] allCharacter;
    public AllCharacter selectedCharacter;
    public Character SelectedCharcter;
    public string letter;
    protected List<int> OptionNO = new List<int>();
    public List<int> AllAnswerNo = new List<int>();
    public int reloding;
    public float distangedrage=2.5f;
    [Space(15)]
    [Header("Animation")]
    public LineRenderer selected_Line;
 
    public GameObject gameCompleted_animation;
    public GameObject wrongAnswer_animtion;
    public GameObject Party_pop;
    // Start is called before the first frame update
    public int maxloding;
    protected virtual void Start()
    {
        if(maxloding==0)
        {
            maxloding = allCharacter.Length;
        }
        GameStart();
    }

    public virtual void GameStart()
    {
        int no = 0;

        reloding++;
        if (reloding > allCharacter.Length)
        {
            gamePlay = false;
            StartCoroutine(LevelCompleted());
            return;
        }
        int answerno = Random.Range(0, allCharacter.Length);
        for (int j = 0; j < AllAnswerNo.Count; j++)
        {
            if(AllAnswerNo[j]== answerno)
            {
                answerno = Random.Range(0, allCharacter.Length);
                j = -1;
            }
        }
        int answeroption = Random.Range(0, alloption.Length);
        Debug.Log(answerno);
        int i = 0;
        foreach (var option in alloption)
        {

            no = Random.Range(0, allCharacter.Length);
            if (no == answerno)
            {
                no = Random.Range(0, allCharacter.Length);
            }
            for (int j = 0; j < OptionNO.Count; j++)
            {
                if (no == OptionNO[j])
                {
                    no = Random.Range(0, allCharacter.Length);
                    if (no == answerno)
                    {
                        no = Random.Range(0, allCharacter.Length);
                    }
                    j = -1;
                }
            }
          
            if(i== answeroption)
            {
                selectedCharacter = allCharacter[answerno];
                letter = allCharacter[answerno].Letter;
                letterno = Random.Range(0, allCharacter[answerno].sameLetter.Length);
                Icon.sprite = Icon ? allCharacter[answerno].sameLetter[letterno].Icon:null;
                option.no = allCharacter[answerno].Letter;
                AllAnswerNo.Add(answerno);
                OptionNO.Add(answerno);
            }
            else
            {
                option.no = allCharacter[no].Letter;
                OptionNO.Add(no);
            }

            i++;
           
        }
      
        //for (int i = 0; i < alloption.Length; i++)
        //{
        //    no = Random.Range(0, allCharacter.Length);
        //    for (int j = 0; j < allno.Count; j++)
        //    {
        //        if (no == allno[j])
        //        {
        //            no = Random.Range(0, allCharacter.Length);
        //            j = -1;
        //        }
        //    }
        //    if (i == answeroption)
        //    {

        //        question_text.text = allCharacter[no].letter;
        //        //question_sprite.sprite = allcharacter[no].Icon;
        //        letter = allCharacter[no].letter;
        //        //  alloption[i].GetChild(0).GetComponent<SpriteRenderer>().sprite = allcharacter[no].relatedsdprite;
        //        //   alloption[i].GetComponent<Drager>().no = allcharacter[no].letter;
        //    }
        //    else
        //    {


        //    }
        //    alloption[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allCharacter[no].Icon;
        //    alloption[i].GetComponent<Drager>().no = allCharacter[no].letter;
        //    allno.Add(no);


        //}

    }
    public virtual bool Neartodestination()
    {


        /// Debug.Log(Vector3.Distance(selectedoption.transform.position, droping_place[0].transform.position));
        if (Vector3.Distance(selectedoption.transform.position, droping_place[0].transform.position) < distangedrage)
        {

            selectedoption.transform.position = droping_place[0].transform.position;
            return true;
        }

        selectedoption = null;
        return false;
    }
    public virtual bool NeartodestinationsamePos()
    {
        if (Vector3.Distance(selectedoption.transform.position, droping_place[0].transform.position) < distangedrage)
        {

           // selectedoption.transform.position = droping_place[0].transform.position;
            return true;
        }

        selectedoption = null;
        return false;
    }

    public virtual bool Neartodestination(int id)
    {


        /// Debug.Log(Vector3.Distance(selectedoption.transform.position, droping_place[0].transform.position));
        if (Vector3.Distance(selectedoption.transform.position, droping_place[id].transform.position) < distangedrage)
        {

            selectedoption.transform.position = droping_place[0].transform.position;
            //selectedoption = null;
            return true;
        }

      //  
        return false;
    }
  
    
    
    protected IEnumerator LevelCompleted()
    {
        gameCompleted_animation.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);

    }
    protected IEnumerator WaitForCurrectanimtion()
    {
        Party_pop.SetActive(true);
        Debug.Log("partypos");
        yield return new WaitForSeconds(3);
        Party_pop.SetActive(false);
        gamePlay = true;
        CurrectAnimtionCompleted();
        //ResetingDrage();
    }
    protected virtual void CurrectAnimtionCompleted()
    {

    }
    protected IEnumerator WaitWrongAnimtion()
    {
        wrongAnswer_animtion.SetActive(true);
        yield return new WaitForSeconds(2);
        wrongAnswer_animtion.SetActive(false);
        gamePlay = true;
        ResetingDrage();
    }

    public virtual void ResetingDrage()
    {
        if(Boarder)
        {
            Boarder.color = Color.white;
            
        }
        selectedoption.transform.position = selectedoption.lastpos;
        selectedoption = null;
        OptionNO.Clear();
    }

    public virtual void CurrectAnswer() { }

    public virtual void WrongAnswer() { }
}
