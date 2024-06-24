using System.Collections;

using System.Collections.Generic;
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
        public TouchPoint[] maintouchpoint;
        public Transform[] points;
        Coroutine draw_line;
        public bool singleline;
        public float distancecheck = 1f;
        public List<Transform> CheckPoints = new List<Transform>();
        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;


            GameManager.instace.clicked = true;//Mouse is presed


            DragingStarted();
        }
        public int pointno;

        private void Start()
        {
            distancecheck = .75f;
        }
        private void OnMouseDrag()
        {
            if (pointno == CheckPoints.Count || line.positionCount==0)
                return;
            //if(Connection[pointno] == false)
            //{
            //    FinishLine();
            //}

            if (Vector3.Distance(line.GetPosition(line.positionCount - 1), CheckPoints[pointno].transform.position) < distancecheck)
            {
                
                pointno++;
                if(pointno == CheckPoints.Count)
                {
                    FinishLine();
                    
                    if (index < GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].line.Length)
                    {
                        //Open Nextline;

                        GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].line[index].gameObject.SetActive(true);
                        GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].line[index - 1].gameObject.SetActive(false);
                    }
                    else
                    {

                        if (GameManager.instace.numberofcolors == 2)
                        {
                            Finished = true;
                            GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].Finsihed = true;
                            Debug.Log("All Line is Completed");
                            if (!GameManager.instace.AllLevelCompleted())
                            {
                                completed = true;
                                //Going to next No
                                StartCoroutine(Leteris_Completed());
                            }
                            else
                            {

                                GameManager.instace.LevelComplted.SetActive(true);
                                StartCoroutine(GoingTomenu());
                            }

                        }
                        else
                        {
                            Debug.Log("multipoint");

                            Multiplecolor();
                        }
                        //Finished World
                    }

                    return;
                }
                CheckPoints[pointno].gameObject.SetActive(true);

            }
        }

        void DragingStarted()
        {
            GameManager.instace.lastcoloid.Add(GameManager.instace.selectedcolor);
            Vector3 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Position.z = 0;
            if (GameManager.instace.ColorSelection)
            {
                GameManager.instace.colorwindow.SetActive(false);
                line.colorGradient = GameManager.instace.colors[GameManager.instace.selectedcolor].color;
            }
            if (Vector3.Distance(Position, startingPoint.transform.position) <= distancecheck)
                Entered = true;

            if(Entered)
                StartLine();
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
        private void OnMouseUp()
        {
            if (line.positionCount == 0)
                return;

            CheckFinishline();

        }

        private void OnMouseExit()
        {
            //check its near to finsihline;
            if (line.positionCount == 0)
                return;

            CheckFinishline();
          
        }
        void CheckFinishline()
        {
         
            if (!CheckPoints[CheckPoints.Count - 1].gameObject.activeInHierarchy)
            {

            }
               
            if (Vector3.Distance(CheckPoints[CheckPoints.Count - 1].transform.position, line.GetPosition(line.positionCount - 1)) < distancecheck &&
                CheckPoints[CheckPoints.Count - 1].gameObject.activeInHierarchy)
            {
                if (index < GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].line.Length)
                {

                    //Open Nextline;
                    GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].line[index].gameObject.SetActive(true);
                    GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].line[index - 1].gameObject.SetActive(false);
                    if (maintouchpoint.Length > 1)
                    {
                        Debug.Log("next activateonspot");

                    }
                    else
                    {
                    }

                }
                else
                {

                    if (GameManager.instace.numberofcolors == 2)
                    {


                        Finished = true;
                        GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].Finsihed = true;
                        Debug.Log("All Line is Completed");
                        if (!GameManager.instace.AllLevelCompleted())
                        {
                            completed = true;

                            //Going to next No
                            StartCoroutine(Leteris_Completed());
                        }
                        else
                        {
                            StopCoroutine(Leteris_Completed());
                            GameManager.instace.LevelComplted.SetActive(true);
                            StartCoroutine(GoingTomenu());
                        }
                    }
                    else
                    {
                        Debug.Log("multipoint");

                        Multiplecolor();
                    }
                    //else
                    //{
                    //    if(GameManager.instace.multipleColor)
                    //    GameManager.instace.numberofcolors++;
                    //    GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].line[0].gameObject.SetActive(true);
                    //    if(index>1)
                    //        GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].line[index - 1].gameObject.SetActive(false);
                    //}
                    //Finished World
                }

            }
            else
            {
                Debug.Log("point reset");
                pointno = 0;
                foreach (var item in CheckPoints)
                {
                    item.gameObject.SetActive(false);
                }
                CheckPoints[0].gameObject.SetActive(true);
                line.positionCount = 0;
                line.gameObject.SetActive(false);

            }
            FinishLine();
            Entered = false;
        }


        IEnumerator GoingTomenu()
        {
            WordsHandling word = GameManager.instace.NumberAndLetters[GameManager.instace.activeobj];
            foreach (var item in word.line)
            {

                item.line.gameObject.SetActive(false);
            }
           
            if (GameManager.instace.ColorSelection)
                word.FinsihNO.GetComponent<SpriteRenderer>().color = GameManager.instace.colors[GameManager.instace.selectedcolor].color.Evaluate(0);
            Debug.Log("LINE DEACTIAVED");
            word.FinsihNO.SetActive(true);
            yield return new WaitForSeconds(2);
            GameManager.instace.LoadMenu();
        }
        void MultipleLevelComplted(out WordsHandling wrods)
        {
            WordsHandling word = GameManager.instace.NumberAndLetters[GameManager.instace.activeobj];
            wrods = word;
            foreach (var item in word.line)
            {
                
                item.line.gameObject.SetActive(false);//deactivaling the all linerender
                foreach (var check in item.CheckPoints)
                {
                    check.gameObject.SetActive(false);
                }
                item.CheckPoints[0].gameObject.SetActive(true);
                item.line.positionCount = 0;
            }
            if (GameManager.instace.ColorSelection)
            {
                word.FinsihNO.GetComponent<SpriteRenderer>().color = GameManager.instace.colors[GameManager.instace.selectedcolor].color.Evaluate(0);
                GameManager.instace.colorwindow.SetActive(true);
            }
           
            word.FinsihNO.SetActive(true);
            
        }

        public void Multiplecolor()
        {
            //GameManager.instace.resetbutton.SetActive(false);
            WordsHandling word = null;
            MultipleLevelComplted(out word);
          //  GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].line[0].gameObject.SetActive(true);
            
            GameManager.instace.numberofcolors++;
            foreach (var item in GameManager.instace.NumberAndLetters[GameManager.instace.activeobj].line)
            {
                item.pointno = 0;
            }
            word.line[word.line.Length - 1].gameObject.SetActive(false);

        }
        IEnumerator Leteris_Completed()
        {

            GameManager.instace.resetbutton.SetActive(false);
            WordsHandling word = null;
            MultipleLevelComplted(out word);
            GameManager.instace.wordholder.SetActive(false);
            foreach (var item in GameManager.instace.NumberAndLetters)
            {

                if (item.Finsihed)
                {
                    item.buttonImage.color = GameManager.instace.CompletedCollor;
                   
                }
                else
                {
                    item.buttonImage.color = GameManager.instace.incomplletedCollor;
                }

           

            }// reseting the all Ui button Colors

            word.FinsihNO.GetComponent<SpriteRenderer>().sortingOrder = 3;
            GameManager.instace.lastcoloid.Clear();

           
            if (GameManager.instace.ColorSelection && GameManager.instace.multipleColor)
            {
                word.FinsihNO.GetComponent<SpriteRenderer>().color = Color.white;
                word.FinsihNO.GetComponent<SpriteRenderer>().sortingOrder = 2;
                word.FinsihNO.GetComponent<SpriteRenderer>().sprite = GameManager.instace.ranbowColorSprite[GameManager.instace.activeobj];
                GameManager.instace.colorwindow.SetActive(true);

            }

                word.FinsihNO.SetActive(true);

            GameManager.instace.partypop.SetActive(true);

            yield return new WaitForSeconds(2f);
            GameManager.instace.wordholder.SetActive(true);
            GameManager.instace.partypop.SetActive(false);
            //   word.obj.transform.DOScale(new Vector3(1f, 1f, 1f), .1F);
            Debug.Log(("Waiting"));
            word.obj.SetActive(false);

            GameManager.instace.activeobj++;
            if(GameManager.instace.multipleColor)
            GameManager.instace.numberofcolors = 0;
            GameManager.instace.activeobj = GameManager.instace.activeobj % GameManager.instace.NumberAndLetters.Length;
            word = GameManager.instace.NumberAndLetters[GameManager.instace.activeobj];
            word.obj.SetActive(true);
            GameManager.instace.resetbutton.SetActive(true);
            Debug.Log(GameManager.instace.activeobj +"index" +index);
            if(GameManager.instace.activeobj>0 && index>0)
               GameManager.instace.NumberAndLetters[GameManager.instace.activeobj-1].line[index - 1].gameObject.SetActive(false);
           
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
            {
                word.FinsihNO.GetComponent<SpriteRenderer>().color = GameManager.instace.colors[GameManager.instace.selectedcolor].color.Evaluate(0);
                GameManager.instace.colorwindow.SetActive(true);
            }

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

    }
}