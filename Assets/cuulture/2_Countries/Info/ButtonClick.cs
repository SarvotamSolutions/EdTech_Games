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
    public GameObject[] allclickhere;
    public Transform inforWindow;
    public bool selection;
    public GameObject addmoneyimage;
    private void Start()
    {
        //if (selection)
        //{
        //    StartCoroutine(addmoneyanimation());


        //}
       // gameObject.SetActive(false);
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
            inforWindow.DOScaleY(0, .5f);
            if (no>= allicon.Length)
                StartCoroutine(LevelComplete());
            else
            {
                allclickhere[no].SetActive(true);
                allclickhere[no - 1].SetActive(false);
                icon.sprite = allicon[no];
                infotext.text = allinfo[no];
                nametext.text = allname[no];
                Debug.Log("xxx");
                Debug.Log(nametext.transform.parent.GetComponent<SpriteRenderer>().size.x);
                float nos =(float)allname[no].Length;
                if (nos > 10)
                    nos = nos / 1.75f;
                
                nametext.transform.parent.GetComponent<SpriteRenderer>().size = new Vector2(nos, 2.5f);
                this.gameObject.SetActive(false);
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
        this.gameObject.SetActive(true);
    }
    IEnumerator LevelComplete()
    {
        LevleComplet.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
