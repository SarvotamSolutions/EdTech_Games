using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class ScemceManagement : MonoBehaviour
{

    public Scrollbar matchsSlide,langslide,cultureslide;
    public Image maths, lang,culture;
    public Sprite selectedmaths, selectedlang, nonselectedmaths, nonselectedlang,cultureselect,culturenonselect;
    public GameObject mathsscreen, englishscreen,cultuurescrren;
    public bool menu;
    private void Start()
    {

        Input.multiTouchEnabled = false;
        Application.targetFrameRate = -60;
        if (menu)
        {

            if (PlayerPrefs.GetInt("activity") == 0)
            {


                mathsselect();

            }
            else if(PlayerPrefs.GetInt("activity") == 1)
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


           // cultureselects();
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


            if (PlayerPrefs.HasKey("slideValueculture"))
            {
               
            
                cultureslide.value = PlayerPrefs.GetFloat("slideValueculture");
               

            }
            else
            {
                Debug.Log("XXXXX");
                cultureslide.value = 1;
            }

        }



    }

    float avgFrameRate;
    public Text text;
    private void Update()
    {
        avgFrameRate = Time.frameCount / Time.time;
        if(text)
        text.text = avgFrameRate.ToString();
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
        PlayerPrefs.SetFloat("slideValueculture", 1);
    }
    public void LoadScence(int index)
    {



        if (index >= 0)
        {
            if (menu)
            {
                PlayerPrefs.SetFloat("slideValuemathcs", matchsSlide.value);
                Debug.Log(langslide.value);
                PlayerPrefs.SetFloat("slideValuelanguage",langslide.value);
                PlayerPrefs.SetFloat("slideValueculture",cultureslide.value);
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
