using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    int reloding;
    public bool gameplay;
    public Sprite[] selectspritrecolor, nonselectinspritregcolor;
    public Image[] allpencinl;
    public Transform[] basepos;
    public ScratchCard.ScratchCardMaskUGUI[] allcountries;
    public Vector2 brushsize;
    public GameObject backhold;
    int no;
    bool clicked;
    public float[] fillamount;
    public GameObject gameCompleted_animation;
    public GameObject wrongAnswer_animtion;
    public GameObject Party_pop;
    public void SelectingPencil(int id)
    {

        if (!gameplay)
            return;

        allpencinl[no].sprite = nonselectinspritregcolor[no];
        allcountries[no].brushScale = Vector2.zero;
        allcountries[no].transform.parent.DOMove(basepos[no].position, .1f);
        //  allcountries[id].transform.parent.SetAsLastSibling();
        allcountries[no].transform.parent.DOScale(1, .1f);
        if (allcountries[no].GetRevealProgress() >= fillamount[no])
        {
            allcountries[no].enabled = false;
        }
        no = id;
        gameplay = false;
        for (int i = 0; i < allpencinl.Length; i++)
        {
            
        }
        backhold.SetActive(true);
        backhold.transform.SetSiblingIndex(backhold.transform.parent.childCount - 1);
        allcountries[id].brushScale =brushsize;
        allcountries[id].transform.parent.DOMove(Vector3.zero,.5f);
        allcountries[id].transform.parent.SetAsLastSibling();
        allcountries[id].transform.parent.DOScale(2,.5f);
        allpencinl[id].sprite = selectspritrecolor[id];
        clicked = true;
    }
    private void Update()
    {
        if (!clicked)
            return;
        Debug.Log(allcountries[no].GetRevealProgress());
        if (Input.GetMouseButtonUp(0) && allcountries[no].GetRevealProgress() >fillamount[no] && clicked)      
        {
            clicked = false;
            StartCoroutine(WaitForCurrectanimtion());
            
        }
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
      //  gamePlay = true;
        CurrectAnimtionCompleted();
        //ResetingDrage();
    }
    protected virtual void CurrectAnimtionCompleted()
    {
        backhold.SetActive(false);
        reloding++;
        allcountries[no].transform.parent.DOMove(basepos[no].position, .1f);
        allpencinl[no].gameObject.SetActive(false);
        //  allcountries[id].transform.parent.SetAsLastSibling();
        allcountries[no].transform.parent.DOScale(1, .1f);
        allcountries[no].gameObject.SetActive(false);
        if (reloding >= allpencinl.Length)
        {
            StartCoroutine(LevelCompleted());
        }
        gameplay = true;
        clicked = false;
    }
    



}
