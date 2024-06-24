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
        public GameObject colorwindow;
        public ColorsSelection[] colors;
        public GameObject partypop;
        public GameObject wordholder;
        public GameObject resetbutton;
        public Sprite[] ranbowColorSprite, NormalSprite;
        
      //  public Gradient[] allcolors;
        public int selectedcolor;

        [Space(14)]
        public bool multipleColor;
        public int numberofcolors;

        private void Awake()
        {
            if (instace == null)
                instace = this;
        }

        public List<int> lastcoloid = new List<int>();
        public void SelectingColor(int color)
        {
            foreach (var item in lastcoloid)
            {
                if (item == color)
                    return;
            }
            NumberAndLetters[activeobj].line[0].gameObject.SetActive(true);

            
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
        public int lineno;
        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            //if (clicked && Input.GetMouseButtonUp(0) && !alrdyCalled)
            //{
            //    clicked = false;
            //    alrdyCalled = true;
            //    Debug.Log("onmouseUP");
            //    NumberAndLetters[activeobj].line[lineno].FinishedTheLineDraging();
            //  //  FinishedTheLineDraging();
            //}
            //if(Input.touchCount > 1)
            //{
            //    NumberAndLetters[activeobj].line[lineno].FinishedTheLineDraging();
            //}
        }
        public void ResetBUtton()
        {
            lastcoloid.Clear();
            foreach (var item in NumberAndLetters[activeobj].line)
            {
                item.completed = false;
            }
            if (ColorSelection)
                colorwindow.SetActive(true);
            if (multipleColor)
            {
                numberofcolors = 0;
                NumberAndLetters[activeobj].FinsihNO.GetComponent<SpriteRenderer>().sortingOrder = 0;
                NumberAndLetters[activeobj].FinsihNO.GetComponent<SpriteRenderer>().sprite = NormalSprite[activeobj];
            }
            NumberAndLetters[activeobj].Finsihed = false;
            NumberAndLetters[activeobj].buttonImage.color = incomplletedCollor;
            NumberAndLetters[activeobj].FinsihNO.SetActive(false);
            foreach (var item in NumberAndLetters[activeobj].line)
            {
                item.Entered = false;
                item.Finished = false;
                item.line.positionCount = 0;
                item.line.gameObject.SetActive(false);
                item.gameObject.SetActive(false);
                item.pointno = 0;
                for (int i = 1; i < item.CheckPoints.Count; i++)
                {
                    item.CheckPoints[i].gameObject.SetActive(false);
                }
            }
            NumberAndLetters[activeobj].line[0].line.gameObject.SetActive(true);
            NumberAndLetters[activeobj].line[0].gameObject.SetActive(true);

        }
        public void ChatracterSlect(int index)
        {
            if(!NumberAndLetters[activeobj].Finsihed)
                ResetBUtton();
            activeobj = index;
            for (int i = 0; i < NumberAndLetters.Length; i++)
            {
                NumberAndLetters[i].obj.SetActive(false);

            }
            NumberAndLetters[activeobj].obj.SetActive(true);
           // numberofcolors = 0;
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