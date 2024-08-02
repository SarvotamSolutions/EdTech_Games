using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
public class ButtonClick : MonoBehaviour
{
    public GameObject nextwindow,currectnWindow;
    public GameObject LevleComplet;
    public UnityEvent Events;
    public SpriteRenderer icon;
    public TextMeshPro infotext, nametext;
    int no;
    public bool iconset;
    public Sprite[] allicon;
    public string[] allinfo;
    public string[] allname;
    public Transform inforWindow;
    public bool selection;
    public GameObject addmoneyimage;
    private void Start()
    {
        if (selection)
        {
            StartCoroutine(addmoneyanimation());


        }
        
    }

    IEnumerator addmoneyanimation()
    {
       // dropcoinfliker.GetComponent<SpriteRenderer>().DOFade(0f, 1);
        addmoneyimage.transform.DOMoveY(addmoneyimage.transform.position.y + .1f, 1);
        yield return new WaitForSeconds(1);
   //     dropcoinfliker.GetComponent<SpriteRenderer>().DOFade(1f, 1);
        addmoneyimage.transform.DOMoveY(addmoneyimage.transform.position.y - .1f, 1);
        yield return new WaitForSeconds(1);
        StartCoroutine(addmoneyanimation());
    }
    public void OnMouseUpAsButton()
    {
        Events.Invoke();

    }
    public void NextButton()
    {
        if (iconset)
        {
            no++;
          
            if(no>= allicon.Length)
                StartCoroutine(LevelComplete());
            else
            {
                icon.sprite = allicon[no];
                infotext.text = allinfo[no];
                nametext.text = allname[no];
            }
        }
        else
        {
            if (nextwindow != null)
            {
                nextwindow.SetActive(true);
                currectnWindow.SetActive(false);

            }
            else
            {
                StartCoroutine(LevelComplete());
                //gamecompleted
            }
        }
    }

    public void ShowInfo()
    {
        inforWindow.DOScaleY(1, .5f);
    }
    IEnumerator LevelComplete()
    {
        LevleComplet.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
