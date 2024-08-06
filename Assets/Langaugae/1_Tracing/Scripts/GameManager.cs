using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace Laguage.Trachingexasise
{
    public class GameManager : MonoBehaviour// all tracing game
    {
        public static GameManager instace;

        [System.Serializable]
        public class ColorsSelection
        {
            public Gradient color;
            public GameObject selectedcolor;
            public GameObject notselected;

        }
        public Totorial totrialcheck;

        [Space(10)]
        public GameObject LevelComplted;
        public GameObject StartPannel;
        public GameObject colorwindow;
        public GameObject partypop;
        public GameObject wordholder;


        public Sprite selectedimage, notanswered, currectanswered;
        public Sprite[] ranbowColorSprite, NormalSprite;

        public WordsHandling[] NumberAndLetters;

        public ColorsSelection[] colors;
        [Space(10)]
        public bool COMPLETED;
        public bool clicked;
        public bool alrdyCalled;
        public bool ColorSelection;
        public bool multipleColor;
        [Space(10)]
        public float crurectanswerinteval = 3;
        [Space(10)]
        public int activeobj;
        public int selectedcolor;
        public int numberofcolors;

        public Color incomplletedCollor, CompletedCollor;

        public List<int> lastcoloid = new List<int>();


        private void Awake()
        {
            if (instace == null)
                instace = this;
        }

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
            StartCoroutine(waitfortotoralcomplete());
        }
        IEnumerator waitfortotoralcomplete()
        {
            yield return new WaitUntil(() => !totrialcheck.totorialplaying);
            StartGame();
        }
        public int lineno;
        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
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
            NumberAndLetters[activeobj].buttonImage.sprite = notanswered;
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
            if (totrialcheck.totorialplaying)
                return;

            if (!NumberAndLetters[activeobj].Finsihed)
                ResetBUtton();

            activeobj = index;
            if (!NumberAndLetters[activeobj].Finsihed)
                NumberAndLetters[activeobj].buttonImage.sprite = selectedimage;
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