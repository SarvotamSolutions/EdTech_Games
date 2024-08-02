using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class ScemceManagement : MonoBehaviour
{
    public float ontotorialsound = .1f;
    public float ontotorialexitsound = .3f;
    public Scrollbar matchsSlide,langslide;
    public Image maths, lang,culture;
    public Sprite selectedmaths, selectedlang, nonselectedmaths, nonselectedlang,cultureselect,culturenonselect;
    public GameObject mathsscreen, englishscreen,cultuurescrren;
    public bool menu;
    public Button backbutton;


    private void Start()
    {
        if (backbutton)
        {
            backbutton.interactable = false;
           
        }
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = -60;
        if (menu)
        {

            if (PlayerPrefs.GetInt("activity") == 0)
            {


                mathsselect();

            }
            else if (PlayerPrefs.GetInt("activity") == 1)
            {
                langselect();
            }
            else
            {
                cultureselects();
            }
            if (PlayerPrefs.HasKey("slideValuemathcs"))
            {

                matchsSlide.value = PlayerPrefs.GetFloat("slideValuemathcs");
            }
            else
                matchsSlide.value = 1;

            if (PlayerPrefs.HasKey("slideValuelanguage"))
            {
                float value = PlayerPrefs.GetFloat("slideValuelanguage");
                Debug.Log("value set" + PlayerPrefs.GetFloat("slideValuelanguage"));
                langslide.value = value;
                Debug.Log(langslide.value);

            }
            else
            {
                Debug.Log("XXXXX");
                langslide.value = 1;
            }


        }
        else
        {
           // backgroundsound.clip = audioClips[Random.Range(0, audioClips.Length)];
        }


    }

    float avgFrameRate;
    public Text text;
    private void Update()
    {
        avgFrameRate = Time.frameCount / Time.time;
        if (text)
        {
           // backgroundsound.volume = totorial.totorialplaying ? ontotorialsound : ontotorialexitsound;
            text.text = avgFrameRate.ToString();
            if (SoundManager.instance.totorial && !SoundManager.instance.totorial.totorialplaying && backbutton.interactable == false)
            {
                SoundManager.instance.backgroundsound.volume = ontotorialexitsound;
                backbutton.interactable = true;
                
               
              //  backgroundsound.Play();
            }
            else
            if (SoundManager.instance.totorial== null)
            {
                SoundManager.instance.totorial = GameObject.Find("totorial").GetComponent<Totorial>();
                SoundManager.instance.backgroundsound.Play();
                SoundManager.instance.backgroundsound.volume = ontotorialsound;
            }

            if (Input.GetKeyUp(KeyCode.Escape))
                SceneManager.LoadScene(0);
        }

       
        
    }

    public void mathsselect()
    {
        maths.sprite = selectedmaths;
        lang.sprite = nonselectedlang;
        culture.sprite = culturenonselect;
        mathsscreen.SetActive(true);
        PlayerPrefs.SetInt("activity", 0);
        cultuurescrren.SetActive(false);
        englishscreen.SetActive(false);
    }
    public void langselect()
    {
        lang.sprite = selectedlang;
        maths.sprite = nonselectedmaths;
        culture.sprite = culturenonselect;
        englishscreen.SetActive(true);
        PlayerPrefs.SetInt("activity", 1);
        cultuurescrren.SetActive(false);
        mathsscreen.SetActive(false);
    }
    public void cultureselects()
    {
        lang.sprite = nonselectedlang;
        maths.sprite = nonselectedmaths;
        culture.sprite = cultureselect;
        cultuurescrren.SetActive(true);
        PlayerPrefs.SetInt("activity", 2);
        englishscreen.SetActive(false);
        mathsscreen.SetActive(false);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("slideValuemathcs", 1);
        PlayerPrefs.SetFloat("slideValuelanguage", 1);
    }
    public void LoadScence(int index)
    {



        if (index >= 0)
        {
            if (menu)
            {
               // SoundManager.instance.backgroundsound.Play();
                PlayerPrefs.SetFloat("slideValuemathcs", matchsSlide.value);
                Debug.Log(langslide.value);
                PlayerPrefs.SetFloat("slideValuelanguage",langslide.value);
               // newdata.value = matchsSlide.value;
            }
           
                
            SceneManager.LoadScene(index);
        }
        else
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
