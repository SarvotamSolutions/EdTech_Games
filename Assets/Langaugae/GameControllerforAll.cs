using DesignPatterns.Singleton;
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

    [Space(15)]
    [Header("Options")]
    public DragerForall[] alloption;
    public DragerForall selectedoption;

    [Space(15)]
    public GameObject droping_place;
    public Character[] allCharacter;
    public string letter;
    protected List<int> allno = new List<int>();

    public int reloding;
    [Space(15)]
    [Header("Animation")]
    public GameObject gameCompleted_animation;
    public GameObject wrongAnswer_animtion;
    public GameObject Party_pop;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        GameStart();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    public virtual void GameStart()
    {
        int no = 0;
        reloding++;
        if (reloding > 10)
        {
            StartCoroutine(LevelCompleted());
            return;
        }

        int answeroption = Random.Range(0, alloption.Length);
        int i = 0;
        foreach (var option in alloption)
        {

            no = Random.Range(0, allCharacter.Length);
            for (int j = 0; j < allno.Count; j++)
            {
                if (no == allno[j])
                {
                    no = Random.Range(0, allCharacter.Length);
                    j = -1;
                }
            }
            option.no = allCharacter[no].letter;
            if(i== answeroption)
            {
                letter = allCharacter[no].letter;
                Icon.sprite = Icon ? allCharacter[no].Icon:null;
            }

            i++;
            allno.Add(no);
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

        if (Vector3.Distance(selectedoption.transform.position, droping_place.transform.position) < 2.5f)
        {

            selectedoption.transform.position = droping_place.transform.position;
            return true;
        }

        selectedoption = null;
        return false;
    }

    protected IEnumerator LevelCompleted()
    {
        gameCompleted_animation.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);

    }

    protected IEnumerator WaitWrongAnimtion()
    {
        wrongAnswer_animtion.SetActive(true);
        yield return new WaitForSeconds(2);
        wrongAnswer_animtion.SetActive(false);
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
        allno.Clear();
    }
}
