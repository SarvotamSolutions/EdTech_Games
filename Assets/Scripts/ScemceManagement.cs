using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class ScemceManagement : MonoBehaviour
{
    public Data newdata = new Data();
    public Scrollbar slide;
    private void Start()
    {

        Input.multiTouchEnabled = false;
        Application.targetFrameRate = -60;
        if (PlayerPrefs.HasKey("slideValue"))
        {
            if (slide)
                slide.value = PlayerPrefs.GetFloat("slideValue");
        }
        else
            slide.value = 1;
        Debug.Log(newdata.value);
    }

    float avgFrameRate;
    public Text text;
    private void Update()
    {
        avgFrameRate = Time.frameCount / Time.time;
        //text.text = avgFrameRate.ToString();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("slideValue", 1);
    }
    public void LoadScence(int index)
    {



        if (index >= 0)
        {
            if (slide)
            {
                PlayerPrefs.SetFloat("slideValue", slide.value);
                newdata.value = slide.value;
            }
            SceneManager.LoadScene(index);
        }
        else
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
