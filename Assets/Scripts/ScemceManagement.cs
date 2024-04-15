using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class ScemceManagement : MonoBehaviour
{

    private void Start()
    {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("XXXX");
        }
    }
    public void LoadScence(int index)
    {
        if (index >= 0)
            SceneManager.LoadScene(index);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
