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
    public Totorial totorialcheck;
    public GameObject nextwindow, currectnWindow;
    public GameObject LevleComplet;
    public UnityEvent Events;
    public SpriteRenderer icon;
    public TextMeshPro infotext, nametext;
    public Image BackgroundImage;
    int no;
    public bool iconset;
    public Sprite[] allicon;
    public Sprite[] Background;
    [TextArea(5, 20)]
    public string[] allinfo;
    public string[] allname;
    public AudioClip[] allsounds;
    public AudioClip[] allnames;
    public GameObject[] allColider;
    public Transform inforWindow;
    public bool selection;
    public GameObject addmoneyimage;
    public GameObject nextButton;
    public bool soundplay;
    public bool SolorSystem;
    public GameObject background_solorSystem;
    public AudioSource sound;
    public int EndWait = 2;
    private void Start()
    {
        if (selection)
        {
            StartCoroutine(addmoneyanimation());


        }



    }


    private void Update()
    {
        if (SolorSystem)
        {
            if (!totorialcheck.totorialplaying)
            {
                SolorSystem = false;
                Debug.Log("checkingtime");
                nametext.transform.parent.gameObject.SetActive(true);
                icon.gameObject.SetActive(true);

                background_solorSystem.SetActive(false);
                this.GetComponent<SpriteRenderer>().enabled = true;
                this.GetComponent<BoxCollider2D>().enabled = true;
                NextButton();
            }

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

            if (no >= allicon.Length)
            {
                if (soundplay)
                {

                    sound.Stop();
                    //  sound.PlayOneShot(allsounds[no]);
                }
                StartCoroutine(LevelComplete());
            }
            else
            {


                if (allname[no - 1] == allname[no])
                {

                    infotext.text = allinfo[no];
                    if (soundplay)
                    {

                        sound.Stop();
                        sound.PlayOneShot(allsounds[no]);
                    }
                }
                else
                {

                    if (nametext.transform.parent.GetComponent<BoxCollider2D>())
                    {
                        nametext.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                        addmoneyimage.SetActive(true);
                    }
                    // this.gameObject.transform.DOScaleY(0, 0.5f);
                    //inforWindow.DOScaleY(0, .5f);
                    inforWindow.gameObject.SetActive(false);
                    if (allColider.Length >= no)
                    {
                        allColider[no - 1].gameObject.SetActive(false);
                        allColider[no].gameObject.SetActive(true);
                    }
                    if (BackgroundImage)
                        BackgroundImage.sprite = Background[no];
                    icon.sprite = allicon[no];
                    infotext.text = allinfo[no];
                    nametext.text = allname[no];
                    nextButton.SetActive(false);
                    if (sound)
                    {
                        sound.Stop();
                        sound.PlayOneShot(allnames[no]);
                    }
                }

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
        if (nametext.transform.parent.GetComponent<BoxCollider2D>())
        {
            nametext.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            addmoneyimage.SetActive(false);
        }
        nextButton.transform.DOScaleY(1, .5f);
        // inforWindow.DOScaleY(1, .5f);
        inforWindow.gameObject.SetActive(true);
        nextButton.SetActive(true);
        infotext.text = allinfo[no];
        if (soundplay)
        {

            sound.Stop();
            sound.PlayOneShot(allsounds[no]);
        }
    }
    IEnumerator LevelComplete()
    {
        LevleComplet.SetActive(true);
        yield return new WaitForSeconds(EndWait);
        SceneManager.LoadScene(0);
    }
}
