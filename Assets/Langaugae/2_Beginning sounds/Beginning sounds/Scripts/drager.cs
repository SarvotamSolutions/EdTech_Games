using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Laguage.beginning_sounds.matchtheSound
{
    public class drager : MonoBehaviour
    {
        public LineRenderer line;
        public GameObject Answeroption;
        private bool answered;
        public GameObject arrow;
        public Color alpanull;
        public SpriteRenderer fromArrow;
        private SpriteRenderer statiing_box;
        public Sprite Slecting;
        public AudioClip lettersound;
        public AudioClip wordsSound;
        public AudioClip dropsound;
        public AudioSource sound;
        private void OnMouseDown()
        {
            if (answered || !GameController.Instance.gamePlay || GameController.Instance.totorialcheck.totorialplaying)
                return;
            sound.PlayOneShot(lettersound);
            GameController.Instance.selectedline = line;
            transform.GetComponent<SpriteRenderer>().sprite = Slecting;
            SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();
       
        }

        // Start is called before the first frame update
        void Start()
        {
            statiing_box=GetComponent<SpriteRenderer>();
         
        }
        SpriteRenderer border;
        // Update is called once per frame
        void Update()
        {

            if (!GameController.Instance.gamePlay || GameController.Instance.totorialcheck.totorialplaying)
                return;
            arrow.transform.position = new Vector3(line.GetPosition(1).x + .1f, line.GetPosition(1).y);
         
            

            if (GameController.Instance.selectedline == line && !answered )
            {
                arrow.transform.LookAt(line.GetPosition(0),Vector3.up);
                for (int i = 0; i < GameController.Instance.iconOption.Length; i++)
                {
                    Transform tempobj = GameController.Instance.iconOption[i].transform;
                    
                    if (Vector3.Distance(line.GetPosition(1),
                        tempobj.transform.position) <= 2  && tempobj.GetChild(0).gameObject.activeInHierarchy)
                    {
                       
                        tempobj.GetChild(1).GetComponent<SpriteRenderer>().color = GameController.Instance.blue;

                    }
                    else if (tempobj.GetChild(1).GetComponent<SpriteRenderer>().color == GameController.Instance.blue)
                    {


                        tempobj.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
                        //  Debug.Log(Answeroption.transform.parent.GetChild(i).GetComponentInChildren<TextMeshPro>().color);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0) && !answered && GameController.Instance.selectedline == line)
            {
                //if (Vector3.Distance(line.GetPosition(1), Answeroption.transform.position) > 1)
                //{
                //    GameController.Instance.selectedline = null;
                //    Color tmp = arrow.GetComponent<SpriteRenderer>().color;
                //    tmp.a = 0f;
                //    arrow.GetComponent<SpriteRenderer>().color = tmp;
                //    line.SetPosition(1, line.GetPosition(0));
                //}
                //else
                fromArrow.color = GameController.Instance.vilot;
                transform.GetComponent<SpriteRenderer>().sprite = GameController.Instance.Selectanswer;
                if (Vector3.Distance(line.GetPosition(1), Answeroption.transform.position) <= 2)
                {
                    sound.PlayOneShot(wordsSound);
                    transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
                    answered = true;
                    border = Answeroption.transform.GetChild(1).GetComponent<SpriteRenderer>();
                    border.color = GameController.Instance.red;
                    GameController.Instance.selectedline = null;
                    statiing_box.sprite = GameController.Instance.Currectanswer;
                    line.SetPosition(1, Answeroption.transform.GetChild(0).transform.position);
                    Answeroption.transform.GetChild(1).GetComponent<SpriteRenderer>().color 
                        = GameController.Instance.green;
                    line.material = GameController.Instance.currectanswer;
                  //  arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().color = GameController.Instance.green;
                    arrow.GetComponent<SpriteRenderer>().sprite = GameController.Instance.currectanswerarrow;
                   // fromArrow.color = GameController.Instance.green;
                    arrow.transform.position = line.GetPosition(1);
                    Answeroption.transform.GetChild(0).gameObject.SetActive(false);
                    //   Answeroption.transform.GetChild(0).gameObject.SetActive(false);
                    //GetComponent<SpriteRenderer>().sprite = GameController.instance.currectanswer;
                    //Answeroption.GetComponent<SpriteRenderer>().sprite = GameController.instance.currectAnswerOption;
                    GameController.Instance.totalanswered++;
                    if (GameController.Instance.totalanswered >= 4)
                    {
                        GameController.Instance.ScenecChange();
                    }
                    return;
                }

                for (int i = 0; i < GameController.Instance.iconOption.Length; i++)
                {


                    if (Vector3.Distance(line.GetPosition(1), GameController.Instance.iconOption[i].transform.position) <= 2 &&
                        GameController.Instance.iconOption[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color != GameController.Instance.green)
                    {
                        sound.PlayOneShot(wordsSound);
                        answered = true;
                        GameController.Instance.gamePlay = false;
                        GameController.Instance.selectedline = null;
                        statiing_box.sprite = GameController.Instance.wronganswer;
                        line.SetPosition(1, GameController.Instance.iconOption[i].transform.GetChild(0).transform.position);
                        arrow.transform.position = GameController.Instance.iconOption[i].transform.GetChild(0).transform.position;
                        border =GameController.Instance.iconOption[i].transform.GetChild(1).GetComponent<SpriteRenderer>();
                        border.color = GameController.Instance.red;
                        line.material = GameController.Instance.WrongAnswer;
                      //  fromArrow.color = GameController.Instance.red;
                      //  arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().color = GameController.Instance.red;
                        arrow.GetComponent<SpriteRenderer>().sprite = GameController.Instance.wronganswerarrow;
                        StartCoroutine(ResetingWrongAnswer());
                        break;
                    }

                }

                sound.PlayOneShot(dropsound);

                if(!answered)
                {
                    NotAnswerd();

                }

            }
         
          

         
        }
        public void NotAnswerd()
        {
            answered = false;
            line.SetPosition(1, line.GetPosition(0));
            SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();
            //Color tmp = arowrender.color;
            //tmp.a = 0f;
            //arowrender.color = tmp;
            GameController.Instance.selectedline = null;
        }
   
        IEnumerator ResetingWrongAnswer()
        {
            Color tempcolor = transform.GetChild(0).GetComponent<TextMeshPro>().color;
            transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
            GameController.Instance.wrongAnswer_animtion.SetActive(true);
            GameController.Instance.wrongAnswer_animtion.GetComponent<AudioSource>().PlayDelayed(1);
            yield return new WaitForSeconds(3);
            transform.GetChild(0).GetComponent<TextMeshPro>().color = tempcolor;
            GameController.Instance.wrongAnswer_animtion.SetActive(false);
            Reseting();
        }
        public void Reseting()
        {
            line.material = GameController.Instance.SelectMaterail;
            statiing_box.sprite = GameController.Instance.Selectanswer;
            // GameController.Instance.selectedline = null;
           
           
            border.color = Color.white;
           // fromArrow.color = GameController.Instance.vilot;
            Answeroption.transform.GetChild(0).gameObject.SetActive(true);
            line.SetPosition(1, line.GetPosition(0));
            answered = false;
            GameController.Instance.gamePlay = true;
        }
    }
}
