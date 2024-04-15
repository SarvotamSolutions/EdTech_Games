using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace Laguage.Trachingexasise
{

    public class TouchPoint : MonoBehaviour
    {
        public int index;
        public bool Entered;
        public bool Finished;
        public GameObject startingPoint, EndingPoint;
        public LineRenderer line;
        public bool completed;
       
        Coroutine draw_line;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
          
        }
        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (completed)
                return;

            if(GameManager.instace.ColorSelection)
                line.colorGradient = GameManager.instace.colors[GameManager.instace.selectedcolor].color;
            Debug.Log(index);
            Entered = true;
            GameManager.instace.clicked = true;

            DragingStarted();


        }
        private void OnMouseEnter()
        {
            if (GameManager.instace.clicked)
            {
            
                OnMouseDown();
                
            }
           
        }
        private void FixedUpdate()
        {
           
        }

        void DragingStarted()
        {
            StartLine();

            Vector3 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Position.z = 0;
            if (Vector3.Distance(Position, startingPoint.transform.position) <= 1f)
                Entered = true;
        }


        private void OnMouseUp()
        {
            if (GameManager.instace.alrdyCalled)
            {

                FinishedTheLineDraging();
            }

        }
       // public TouchPoint nextpoint;
        private void OnMouseExit()
        {
          
            Vector3 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Position.z = 0;
            if (Vector3.Distance(Position, EndingPoint.transform.position) <= 1f)
            {
               

            }
            else
            {
              

             
            }
            FinishedTheLineDraging();

        }
        public int finalline;

       public void FinishedTheLineDraging()
        {
            if (completed)
                return;
            Vector3 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Position.z = 0;
            if (Vector3.Distance(Position, EndingPoint.transform.position) <=1.5f)
            {
                Finished = true;
                if(finalline==0)
                    GameManager.instace.clicked = false;
         
            }
            else
            {
                Debug.Log("EXITING");
                  GameManager.instace.ResetBUtton();
                
                GameManager.instace.clicked = false;
            }
            FinishLine();
            if (!Entered || !Finished)
            {
                Entered = false;
                Finished = false;
                line.positionCount = 0;
                line.gameObject.SetActive(false);
            }
            else
            {
                if (index < GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].line.Length)
                {


                    GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].line[index].gameObject.SetActive(true);
                    GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].line[index - 1].gameObject.SetActive(false);
                }
                else
                {
                    GameManager.instace.alrdyCalled = false;
                    GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].Finsihed = true;
                    if (!GameManager.instace.AllLevelCompleted())
                    {
                        completed = true;
                        //Going to next No
                        StartCoroutine(GoingToNextWord());
                    }
                    else
                    {

                        GameManager.instace.LevelComplted.SetActive(true);
                        StartCoroutine(GoingTomenu());
                    }
                }
            }
        }
        IEnumerator GoingTomenu()
        {
            yield return new WaitForSeconds(2);
            GameManager.instace.LoadMenu();
        }
        IEnumerator GoingToNextWord()
        {

            WordsHandling word = GameManager.instace.NumberAndLetters[GameManager.instace.activeobj];
            foreach (var item in GameManager.instace.NumberAndLetters)
            {
                if (item.Finsihed)
                {
                    item.buttonImage.color = GameManager.instace.CompletedCollor;
                    //button color will be blue;
                }
                else
                {
                    item.buttonImage.color = GameManager.instace.incomplletedCollor;
                }

            }
         //   word.obj.transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), .5F);

            foreach (var item in word.line)
            {

                item.line.gameObject.SetActive(false);
            }
            Debug.Log("LINE DEACTIAVED");
            if (GameManager.instace.ColorSelection)
                word.FinsihNO.GetComponent<SpriteRenderer>().color = GameManager.instace.colors[GameManager.instace.selectedcolor].color.Evaluate(0);
            word.FinsihNO.SetActive(true);

            GameManager.instace.partypop.SetActive(true);
       
            yield return new WaitForSeconds(2f);
            GameManager.instace.partypop.SetActive(false);
            //   word.obj.transform.DOScale(new Vector3(1f, 1f, 1f), .1F);
            Debug.Log(("Waiting"));
            word.obj.SetActive(false);

            GameManager.instace.activeobj++;
            GameManager.instace.activeobj = GameManager.instace.activeobj % GameManager.instace.NumberAndLetters.Length;
            word = GameManager.instace.NumberAndLetters[GameManager.instace.activeobj];
            word.obj.SetActive(true);


        }
        void StartLine()
        {
            if (draw_line != null)
                StopCoroutine(draw_line);


            draw_line = StartCoroutine(DrawLine());

        }
        void FinishLine()
        {
            if (draw_line != null)
                StopCoroutine(draw_line);
        }

        IEnumerator DrawLine()
        {
            line.transform.gameObject.SetActive(true);
            while (true)
            {
                Vector3 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Position.z = 0;
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, Position);

                yield return null;
            }
        }

    }
}