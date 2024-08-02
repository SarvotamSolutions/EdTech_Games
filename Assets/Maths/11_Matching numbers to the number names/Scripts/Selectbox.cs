using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
namespace Maths.matchingNumbers
{
    public class Selectbox : MonoBehaviour
    {
        public AudioSource sound;
        public AudioClip pickup, drop;
        public LineRenderer line;
        public GameObject Answeroption;
        private bool answered;
        public GameObject arrow;
        public Color alpanull;
        private SpriteRenderer maintexture;

        public float distancecheck=1f;
        public TextMeshPro text;
        public Color Right, Wrong, Normal;

        private void Start()
        {
            //sound = GetComponent<AudioSource>();
            line = transform.GetChild(2).GetComponent<LineRenderer>();
            maintexture = GetComponent<SpriteRenderer>();
        }
        private void OnMouseUpAsButton()
        {


        }
        private void OnMouseDown()
        {
            if (answered || !GameController.Instance.gamePlay || GameController.Instance.totorialcheck.totorialplaying)
                return;
           
            sound.clip = pickup;
            
            sound.Play();
            
            if (GameController.Instance.textmatch)
            
            {
            
                transform.GetComponentInChildren<TextMeshPro>().color = GameController.Instance.selectedanswer_colors;


            }
            else
            
            {
            
                transform.GetComponent<SpriteRenderer>().sprite = GameController.Instance.SelectOption;
            }




            SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();

            transform.GetChild(0).GetComponent<SpriteRenderer>().color = GameController.Instance.selectedanswer_colors;

            arowrender.color = GameController.Instance.selectedanswer_colors;

            //      Color tmp = arowrender.color;

            //      tmp.a = 1f;

            //      arowrender.color = tmp;

            GameController.Instance.selected_line = line;
        }
        private void Update()
        {
            
            arrow.transform.position = new Vector3(line.GetPosition(1).x + .1f, line.GetPosition(1).y);

             arrow.transform.LookAt(line.GetPosition(0));
            //selection state  
            if (GameController.Instance.textmatch && GameController.Instance.selected_line == line)

            {

                for (int i = 0; i < Answeroption.transform.parent.childCount; i++)

                {

                    Transform tempobj = Answeroption.transform.parent.GetChild(i).transform;


                    if (tempobj.GetChild(0).gameObject.activeInHierarchy && Vector3.Distance(line.GetPosition(1), tempobj.transform.position) <= distancecheck)
                    {
                        tempobj.GetComponentInChildren<TextMeshPro>().color = GameController.Instance.selectedanswer_colors;
                        //.color = GameController.Instance.selectedanswer_colors;
                    }
                    else if (tempobj.GetComponentInChildren<TextMeshPro>().color == GameController.Instance.selectedanswer_colors)
                    {



                        if (tempobj.GetChild(0).gameObject.activeInHierarchy &&

                            Vector3.Distance(line.GetPosition(1),


                            tempobj.transform.position) <= distancecheck)
                        {



                            tempobj.GetComponentInChildren<TextMeshPro>().color = GameController.Instance.selectedanswer_colors;

                            // .color = GameController.Instance.selectedanswer_colors;
                        }


                        else if (tempobj.GetComponentInChildren<TextMeshPro>().color == GameController.Instance.selectedanswer_colors)

                        {

                            tempobj.GetComponentInChildren<TextMeshPro>().color = Color.white;

                            //  tempobj.GetComponent<SpriteRenderer>().sprite =GameController.Instance.DefaltAnwer;

                            //  Debug.Log(Answeroption.transform.parent.GetChild(i).GetComponentInChildren<TextMeshPro>().color);
                        }

                    }

                }

            }
            else if (!GameController.Instance.textmatch && GameController.Instance.selected_line == line)
            {

                for (int i = 0; i < Answeroption.transform.parent.childCount; i++)
                {
                    Transform tempobj = Answeroption.transform.parent.GetChild(i).transform;

                    if (tempobj.GetChild(0).gameObject.activeInHierarchy &&
                        Vector3.Distance(line.GetPosition(1),
                        tempobj.transform.position) <= distancecheck)

                    {

                        tempobj.GetComponent<SpriteRenderer>().sprite = GameController.Instance.SelectAnswer;
                    }
                    else if (tempobj.GetComponent<SpriteRenderer>().sprite == GameController.Instance.SelectAnswer)
                    {

                        tempobj.GetComponent<SpriteRenderer>().sprite = GameController.Instance.DefaltOption;
                        tempobj.transform.GetChild(1).GetComponent<TextMeshPro>().color = Normal;

                        //  Debug.Log(Answeroption.transform.parent.GetChild(i).GetComponentInChildren<TextMeshPro>().color);
                    }
                }
            }



            //Droping state
            if (Input.GetMouseButtonUp(0) && !answered && GameController.Instance.selected_line == line)
            {
                
                Debug.Log(Vector3.Distance(line.GetPosition(1), Answeroption.transform.transform.position));
                
               
                
                if (Answeroption.transform.GetChild(0).gameObject.activeInHierarchy &&
                    Vector3.Distance(line.GetPosition(1), Answeroption.transform.transform.position) <= distancecheck)
                {

                    sound.clip = drop;

                    sound.Play();
                    Debug.Log("The answer is currect so the option will show in green now");

                    line.material = GameController.Instance.currectmateral;

                    // arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().color = GameController.Instance.currectanswer_Colors;

                    answered = true;

                    if (GameController.Instance.textmatch)
                    {

                        Debug.Log("Coming inside");
                        text.color = GameController.Instance.currectanswer_Colors;
                        transform.GetChild(0).GetComponent<SpriteRenderer>().color = GameController.Instance.currectanswer_Colors;
                        transform.GetComponentInChildren<TextMeshPro>().color = GameController.Instance.currectanswer_Colors;
                        Answeroption.transform.GetComponentInChildren<TextMeshPro>().color = GameController.Instance.currectanswer_Colors;
                    }

                    transform.GetChild(0).GetComponent<SpriteRenderer>().color = GameController.Instance.currectanswer_Colors;

                    GameController.Instance.selected_line = null;
                    
                    line.SetPosition(1, Answeroption.transform.GetChild(0).transform.position);
                    
                    Answeroption.transform.GetChild(0).gameObject.SetActive(false);
                    
                    maintexture.sprite = GameController.Instance.currectanswer;

                    // this.transform.GetChild(1).GetComponent<TextMeshPro>().color = Color.white;
                    // Answeroption.GetComponentInChildren<TextMeshPro>().color = Color.white;                    
                    if (SceneManager.GetActiveScene().name == "Matching Letter")
                    {
                        this.transform.GetChild(1).GetComponent<TextMeshPro>().color = Right;
                        }
                    else
                        this.transform.GetChild(1).GetComponent<TextMeshPro>().color = Color.white;
                    
                    Answeroption.GetComponentInChildren<TextMeshPro>().color = Right;
                    

                    Answeroption.GetComponent<SpriteRenderer>().sprite = GameController.Instance.currectAnswerOption;

                    
                    GameController.Instance.totalanswered++;

                    if (GameController.Instance.totalanswered >= GameController.Instance.allAnsweroption.Length)
                    {
                        GameController.Instance.ScenecChange();
                    }
                    
                    Debug.Log("returing the script so the wrong option will not show");
                    return;
                }

                for (int i = 0; i < Answeroption.transform.parent.childCount; i++)
                {

                  
                    if (Answeroption.transform.parent.GetChild(i).transform.GetChild(0).gameObject.activeInHierarchy &&
                        Vector3.Distance(line.GetPosition(1), Answeroption.transform.parent.GetChild(i).transform.position) <= distancecheck)
                    {
                        sound.clip = drop;

                        sound.Play();
                        Debug.Log("wrong asnwer option is playing " + Vector3.Distance(line.GetPosition(1), Answeroption.transform.parent.GetChild(i).transform.GetChild(0).transform.position));
                        
                        GameController.Instance.gamePlay = false;
                        line.SetPosition(1, Answeroption.transform.parent.GetChild(i).transform.GetChild(0).transform.position);
                        
                        Answeroption.transform.parent.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
                        
                        GetComponent<SpriteRenderer>().sprite = GameController.Instance.wronganswer;
                        
                        Answeroption.transform.parent.GetChild(i).GetComponent<SpriteRenderer>().sprite = GameController.Instance.wrongansweroption;
                        Answeroption.transform.parent.GetChild(i).transform.GetChild(1).GetComponent<TextMeshPro>().color = Wrong;
                        
                        line.material = GameController.Instance.wrongmatrial;
                        
                        
                        this.transform.GetChild(1).GetComponent<TextMeshPro>().color = Color.white;///////////////////////
                     

                        transform.GetChild(0).GetComponent<SpriteRenderer>().color = GameController.Instance.wronganswer_colors;
                        
                        StartCoroutine(waitForReset(Answeroption.transform.parent.GetChild(i).gameObject));


                        return;
                    }

                }
                
                GameController.Instance.selected_line = null;
                
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = alpanull;
                
                if (GameController.Instance.textmatch)
                {
                
                    transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
                    
                    Answeroption.transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
                }
                
                GetComponent<SpriteRenderer>().sprite = GameController.Instance.DefaltAnwer;
                
                Color tmp = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                
                tmp.a = 0f;
                
                arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                
                line.SetPosition(1, line.GetPosition(0));
            }

        }

        IEnumerator waitForReset(GameObject obj)// 
        {
            GameController.Instance.gamePlay = false;

            SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();

            GameController.Instance.selected_line = null;

            Color tempcolor = obj.transform.GetComponentInChildren<TextMeshPro>().color;

            if (GameController.Instance.textmatch)
            {
                transform.GetComponentInChildren<TextMeshPro>().color = GameController.Instance.wronganswer_colors;
                obj.transform.GetComponentInChildren<TextMeshPro>().color = GameController.Instance.wronganswer_colors;
            }
            else
            {
                //obj.transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
            }

            //    arowrender.color = GameController.Instance.wronganswer_colors;
            GameController.Instance.wrongAnswer_animtion.SetActive(true);
            GameController.Instance.wrongAnswer_animtion.GetComponent<AudioSource>().PlayDelayed(1);
            yield return new WaitForSeconds(3);

            GameController.Instance.wrongAnswer_animtion.SetActive(false);

            line.material = GameController.Instance.normalmateral;

            obj.transform.GetChild(0).gameObject.SetActive(true);

            transform.GetChild(0).GetComponent<SpriteRenderer>().color = alpanull;

            if (GameController.Instance.textmatch)
            {
                transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
                obj.transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
            }
            else
            {
                obj.transform.GetComponentInChildren<TextMeshPro>().color = tempcolor;
            }

            
            GetComponent<SpriteRenderer>().sprite = GameController.Instance.DefaltAnwer;
            
            this.transform.GetChild(1).GetComponent<TextMeshPro>().color = alpanull;
            
            obj.GetComponent<SpriteRenderer>().sprite = GameController.Instance.DefaltOption;
            obj.transform.GetChild(1).GetComponent<TextMeshPro>().color = Normal;


            Color tmp = arowrender.color;

            tmp.a = 0f;

            arowrender.color = tmp;

            line.SetPosition(1, line.GetPosition(0));

            GameController.Instance.gamePlay = true;

            GameController.Instance.gamePlay = true;
        }

        public void Reseting()
        {
            
            line.material = GameController.Instance.normalmateral;
            
            if (GameController.Instance.textmatch)
            
            {
            
                transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
                
                Answeroption.transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
            }

            
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = alpanull;
            
            transform.GetChild(1).GetComponent<TextMeshPro>().color = alpanull;
            
            transform.GetComponent<SpriteRenderer>().sprite = GameController.Instance.DefaltAnwer;
            
            Answeroption.transform.GetChild(1).GetComponent<TextMeshPro>().color = Normal;
            Answeroption.GetComponent<SpriteRenderer>().sprite = GameController.Instance.DefaltOption;

            answered = false;
            
            SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();
            
            Color tmp = arowrender.color;
            
            tmp.a = 0f;
            
            arowrender.color = tmp;
            
            GameController.Instance.selected_line = null;
            
            Answeroption.transform.GetChild(0).gameObject.SetActive(true);
            
            line.SetPosition(1, line.GetPosition(0));
        }
    }
}