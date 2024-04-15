using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace Laguage.Trachingexasise
{
    public class GameManager : MonoBehaviour
    {
        [System.Serializable]
        public class ColorsSelection
        {
            public Gradient color;
            public GameObject selectedcolor;
            public GameObject notselected;
       //     public SpriteRenderer changesprite;
        }

        public bool COMPLETED;
        public bool clicked;
        public static GameManager instace;
        public int activeobj;
        public WordsHandling[] NumberAndLetters;
        public GameObject LevelComplted;
        public GameObject StartPannel;
        public Color incomplletedCollor, CompletedCollor;
        public bool alrdyCalled;
        public bool ColorSelection;
        public ColorsSelection[] colors;
        public GameObject partypop;
      //  public Gradient[] allcolors;
        public int selectedcolor;
        private void Awake()
        {
            if (instace == null)
                instace = this;
        }


        public void SelectingColor(int color)
        {

            foreach (var item in colors)
            {
                item.notselected.SetActive(true);
                item.selectedcolor.SetActive(false);
                
            }
            colors[color].selectedcolor.SetActive(true);
            colors[color].notselected.SetActive(false);
            selectedcolor = color;
        }

        private void Start()
        {

     

        }
        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (clicked && Input.GetMouseButtonUp(0) && !alrdyCalled)
            {
                clicked = false;
                alrdyCalled = true;
                Debug.Log("onmouseUP");
                NumberAndLetters[activeobj].line[NumberAndLetters[activeobj].line[0].finalline].FinishedTheLineDraging();
              //  FinishedTheLineDraging();
            }
        }
        public void ResetBUtton()
        {
            foreach (var item in NumberAndLetters[activeobj].line)
            {
                item.completed = false;
            }
           

            NumberAndLetters[activeobj].Finsihed = false;
            NumberAndLetters[activeobj].FinsihNO.SetActive(false);
            foreach (var item in NumberAndLetters[activeobj].line)
            {
                item.Entered = false;
                item.Finished = false;
                item.line.positionCount = 0;
                item.line.gameObject.SetActive(false);
                item.gameObject.SetActive(false);
            }
            NumberAndLetters[activeobj].line[0].line.gameObject.SetActive(true);
            NumberAndLetters[activeobj].line[0].gameObject.SetActive(true);

        }
        public void ChatracterSlect(int index)
        {
            activeobj = index;
            for (int i = 0; i < NumberAndLetters.Length; i++)
            {
                NumberAndLetters[i].obj.SetActive(false);

            }
            NumberAndLetters[activeobj].obj.SetActive(true);
       
        }

        public void StartGame()
        {
            for (int i = 0; i < NumberAndLetters.Length; i++)
            {
                NumberAndLetters[i].obj.SetActive(false);

            }
            NumberAndLetters[activeobj].obj.SetActive(true);
            StartPannel.SetActive(false);

        }
        public bool AllLevelCompleted()
        {

            foreach (var item in NumberAndLetters)
            {
                if (!item.Finsihed)
                    return false;
            }

            return true; ;

        }
        public void LoadMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
    [System.Serializable]
    public class WordsHandling
    {
        public string name;
        public GameObject obj;
        public bool Finsihed;
        public TouchPoint[] line;

        public GameObject FinsihNO;
        public Image buttonImage;


    }
}