using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Totorial : MonoBehaviour
{

    public Button[] allbutton;
    public GameObject[] panels,directionpanel;
    public SpriteRenderer[] imagages;
    public Transform[] allpoints;
    public int[] timeholding, directiontimeholding;
    public SpriteRenderer hand;
    public AudioClip[] totorialsound,directionsound;
    public AudioSource sounds;
    public bool direction;
    bool totoralfinished;
    int totorialno;
    public int directionno;
    int pointno;
    public bool totorialplaying;
    public IEnumerator coritine;
    public bool repetdirection;
    private void Start()
    {
        //animaiton play
        if(direction)
            StartCoroutine(PlayAnimtion());

        StartCoroutine(GotoNextTotorial());


        foreach (var item in allbutton)
        {
            item.onClick.AddListener(() => SkipTotorial());
        }
    }
    private void Update()
    {
        if (hand && totoralfinished && hand.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0))
            hand.gameObject.SetActive(false);
            
    }

    public void SkipTotorial()
    {
        StopAllCoroutines();
        panels[totorialno].SetActive(false);
        sounds.Stop();
        totorialno++;
        if (totorialno < timeholding.Length)
        {
            StartCoroutine(GotoNextTotorial());
        }
        else
        {
            totorialplaying = false;
            totoralfinished = true;
            if (direction)
            {
                hand.gameObject.SetActive(true);
               // yield return new WaitForSeconds(.25f);
                sounds.clip = totorialsound[totorialno];
                sounds.Play();
                totorialplaying = false;
            }
        }
        
     

    
    }

    public void Extratotorialstop()
    {
        if (!totorialplaying)
            return;
        StopCoroutine(coritine);
        Debug.Log("coming here");
        sounds.Stop();
        directionpanel[directionno].SetActive(false);
        directionno++;
        totorialplaying = false;
        if (repetdirection)
        {
            repetdirection = false;
            directionWindow();
        }
    }
    public void directionWindow()
    {
        if (directionno >= directionpanel.Length)
            return;
        coritine = DirectionWindowOppen();
            StartCoroutine(coritine);
    }
    private IEnumerator DirectionWindowOppen()
    {

        
        Debug.Log("Direction" +directionno + " " + directionpanel.Length);
        totorialplaying = true;
       
        directionpanel[directionno].SetActive(true);
        sounds.clip = directionsound[directionno];
        sounds.Play();
        yield return new WaitForSeconds(directiontimeholding[directionno]);
        Debug.Log("Stoping");
        directionpanel[directionno].SetActive(false);
        directionno++;

        totorialplaying = false;
        if (repetdirection)
        {
            repetdirection = false;
            directionWindow();
        }
    }
    IEnumerator GotoNextTotorial()
    {
        totorialplaying = true;
        sounds.clip = totorialsound[totorialno];
        sounds.Play();
  
        panels[totorialno].SetActive(true);
       
        yield return new WaitForSeconds(timeholding[totorialno]);
        panels[totorialno].SetActive(false);
        totorialno++;
        
        if (totorialno < timeholding.Length)
        {
            StartCoroutine(GotoNextTotorial());
        }
        else
        {
            totorialplaying = false;
            totoralfinished = true;
            if (direction)
            {
                hand.gameObject.SetActive(true);
                yield return new WaitForSeconds(.25f);
                sounds.clip = totorialsound[totorialno];
                sounds.Play();
                totorialplaying = false;
            }
        }
    }

    IEnumerator PlayAnimtion()
    {
        if(pointno == 0)
        {
            hand.transform.position = allpoints[pointno].position;
        }
        hand.transform.DOMove(allpoints[pointno].position, 1f);
        yield return new WaitForSeconds(1f);
        Debug.Log(pointno);
        pointno = pointno >= allpoints.Length-1 ? pointno = 0:pointno+=1;
        Debug.Log(pointno);
        StartCoroutine(PlayAnimtion());
    }

}
