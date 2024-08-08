using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class ButtonClick : MonoBehaviour
{
    public GameObject nextwindow,currectnWindow;
    public GameObject LevleComplet;
    public UnityEvent Events;
    public SpriteRenderer icon;
    public TextMeshPro infotext, nametext;
    public Image BackgroundImage;
    int no;
    public bool iconset;
    public Sprite[] allicon;
    public Sprite[] Background;
    [TextArea(5,20)]
    public string[] allinfo;
    public string[] allname;
    public GameObject[] allColider;
    public Transform inforWindow;
    public bool selection;
    public GameObject addmoneyimage;
    public GameObject nextButton;

    private void Start()
    {
        if (selection)
        {
            StartCoroutine(addmoneyanimation());


        }
        
    }

    IEnumerator addmoneyanimation()
    {
        addmoneyimage.transform.DOMoveY(addmoneyimage.transform.position.y + .1f, 1);
        yield return new WaitForSeconds(1);
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
               // this.gameObject.transform.DOScaleY(0, 0.5f);
                //inforWindow.DOScaleY(0, .5f);
                inforWindow.gameObject.SetActive(false);
                if(allColider.Length >= no)
                {
                    allColider[no-1].gameObject.SetActive(false);
                    allColider[no].gameObject.SetActive(true);
                }
                BackgroundImage.sprite = Background[no];
                icon.sprite = allicon[no];
                infotext.text = allinfo[no];
                nametext.text = allname[no];
                nextButton.SetActive(false);
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
            }
        }
    }

    public void ShowInfo()
    {
        
        nextButton.transform.DOScaleY(1, .5f);
        // inforWindow.DOScaleY(1, .5f);
        inforWindow.gameObject.SetActive(true);
        nextButton.SetActive(true);
        infotext.text = allinfo[no];
    }
    IEnumerator LevelComplete()
    {
        LevleComplet.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
