
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Laguage;
using TMPro;
using UnityEngine.SceneManagement;

public class GameControllerforAll : Singleton<GameControllerforAll>
{
    public Totorial totorial;

    [Header("Color selection")]
    public Color currect_answer_color;
    public Color wrong_answer_color;
    public Color sellect_answer_color;

    [Header("Sprite selection")]
    public Sprite[] currect_answer;
    public Sprite[] wrong_answer;
    public Sprite[] Normal_answer;


    [Header("Delay Time")]
    [SerializeField] protected float CorrectAnswer_delayTime = 2;
    [SerializeField] protected float WrongAnswer_delayTime = 2;
    public float levelcompletedelayTime;

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
    public float distangedrage = 2.5f;
    public AudioClip lettersound;

    [Space(15)]
    [Header("Animation")]
    public LineRenderer selected_Line;

    public GameObject gameCompleted_animation;
    public GameObject wrongAnswer_animtion;
    public GameObject Party_pop;
    public int maxloding;

    protected virtual void Start()
    {
        totorial = GameObject.Find("totorial").GetComponent<Totorial>();
        if (maxloding == 0)
        {
            maxloding = allCharacter.Length;
        }
        GameStart();
    }


    public virtual void GameStart()
    {
        int no = 0;

        reloding++;
        if (reloding > maxloding)
        {
            gamePlay = false;
            StartCoroutine(LevelCompleted());
            return;
        }
        int answerno = Random.Range(0, allCharacter.Length);
        for (int j = 0; j < AllAnswerNo.Count; j++)
        {
            if (AllAnswerNo[j] == answerno)
            {
                answerno = Random.Range(0, allCharacter.Length);
                j = -1;
            }
        }
        int answeroption = Random.Range(0, alloption.Length);
        Debug.Log(answerno);
        OptionNO.Add(answerno);

        int i = 0;
        foreach (var option in alloption)
        {
            no = Random.Range(0, allCharacter.Length);
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

            if (i == answeroption)
            {
                selectedCharacter = allCharacter[answerno];
                letter = allCharacter[answerno].Letter;
                letterno = Random.Range(0, allCharacter[answerno].sameLetter.Length);
                Icon.sprite = Icon ? allCharacter[answerno].sameLetter[letterno].Icon : null;
                option.no = allCharacter[answerno].Letter;
                AllAnswerNo.Add(answerno);
                lettersound = allCharacter[answerno].sameLetter[letterno].Sound;
                option.pickup = allCharacter[answerno].lettersound;
            }
            else
            {
                option.no = allCharacter[no].Letter;
                option.pickup = allCharacter[no].lettersound;
                OptionNO.Add(no);
            }

            i++;

        }
    }
    public virtual bool Neartodestination()
    {
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
            return true;
        }

        selectedoption = null;
        return false;
    }
    public virtual bool Neartodestination(int id)
    {
        if (Vector3.Distance(selectedoption.transform.position, droping_place[id].transform.position) < distangedrage)
        {

            selectedoption.transform.position = droping_place[0].transform.position;
            return true;
        }
        return false;
    }
    protected IEnumerator LevelCompleted()
    {
        gameCompleted_animation.SetActive(true);
        gameCompleted_animation.GetComponent<AudioSource>().PlayDelayed(1);
        yield return new WaitForSeconds(levelcompletedelayTime + 1);
        SceneManager.LoadScene(0);
    }
    protected IEnumerator WaitForCurrectanimtion()
    {
        Party_pop.SetActive(true);
        Party_pop.GetComponent<AudioSource>().PlayDelayed(1f);
        Debug.Log("partypos");
        yield return new WaitForSeconds(CorrectAnswer_delayTime + 1);
        Party_pop.SetActive(false);
        gamePlay = true;
        CurrectAnimtionCompleted();
        if (reloding > maxloding)
        {
            StartCoroutine(LevelCompleted());
        }
    }
    protected virtual void CurrectAnimtionCompleted()
    {

    }
    protected IEnumerator WaitWrongAnimtion()
    {
        wrongAnswer_animtion.SetActive(true);
        wrongAnswer_animtion.GetComponent<AudioSource>().PlayDelayed(1f);
        yield return new WaitForSeconds(WrongAnswer_delayTime + 1);
        wrongAnswer_animtion.SetActive(false);
        gamePlay = true;
        ResetingDrage();
    }

    public virtual void ResetingDrage()
    {
        for (int k = 0; k < droping_place.Length; k++)
        {
            droping_place[k].gameObject.SetActive(true);
        }
        if (Boarder)
        {
            Boarder.color = Color.white;

        }
        selectedoption.transform.position = selectedoption.lastpos;
        selectedoption = null;
        OptionNO.Clear();
    }

    public virtual void CurrectAnswer() { }

    public virtual void WrongAnswer() { }

    public void letterSoundPlay()
    {
        if (gamePlay)
        {
            GetComponent<AudioSource>().PlayOneShot(lettersound);
        }
    }
}
