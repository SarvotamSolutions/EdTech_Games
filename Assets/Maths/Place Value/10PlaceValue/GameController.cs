using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.placeHolder.value
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        public GameObject dropLoction;
        public GameObject parent;
        public GameObject insasiteobj;
        public Image checkobj;
        public TextMeshPro text;
        public Sprite currectplace;
        public Sprite currectanswer,wronganswer,normlsprite;
        public int noadd;
        public bool thousand;
        [Space(10)]
        public GameObject[] Allbox;
        public bool randomno;
        public int thousandcount,hundredcount,tencount,onecount;
        public int totalpoint;


        public int no;
        public TextMeshPro droptext;
        private List<int> addno = new List<int>();

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        IEnumerator LevelCompleted(bool final)
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            if (!final)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(0);
        }
        IEnumerator WrongAnswerAnimation()
        {
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            Reseting();
            ansercheck.color = Color.white;
            wrongAnswer_animtion.SetActive(false);
        }

        public void loadPoint()
        {
           totalpoint = thousandcount * 1000 +
                   hundredcount * 100 +
                  tencount * 10 +
                  onecount;

            text.text = totalpoint.ToString();
        }

        private void Awake()
        {
            instance = this;
        }

        public bool Neartodestination(GameObject obj,GameObject droploction)
        {
            if (Vector3.Distance(obj.transform.position, droploction.transform.position) < 1)
            {
                text.text = (parent.transform.childCount * noadd).ToString();
                GameObject objects = Instantiate(insasiteobj, parent.transform);
                if (thousand)
                {
                    objects.transform.localPosition = new Vector3(dropLoction.transform.position.x + .2f*( parent.transform.childCount-1), dropLoction.transform.position.y - .2f * (parent.transform.childCount-1), dropLoction.transform.position.z + .1f * (parent.transform.childCount - 1));

                }
                return true;
            }


            return false;
        }
        // Start is called before the first frame update
        void Start()
        {
            no = Random.Range(1, 9999);
            addno.Add(no);

            if (randomno)
            {
                droptext.text = no.ToString();
            }
        }
        public SpriteRenderer ansercheck;
        public void totalcheck()
        {
            if(no == totalpoint)
            {
                ansercheck.color =Color.green;
                if (addno.Count >= 10)
                {
                    StartCoroutine(LevelCompleted(true));
                    return;
                }
                StartCoroutine(Relod());
            }
            else
            {
                ansercheck.color = Color.red;
                StartCoroutine(WrongAnswerAnimation());
            }
        }

        public void RandomNOcheck()
        {
            if(parent.transform.childCount == no + 1)
            {
                checkobj.sprite = currectanswer;

                if(addno.Count >= 10)
                {
                    StartCoroutine(LevelCompleted(true));
                    return;
                }
                StartCoroutine(Relod());
            }
            else
            {
                checkobj.sprite = wronganswer;
                for (int i = parent.transform.childCount - 1; i > 0; i--)
                {
                    Destroy(parent.transform.GetChild(i).gameObject);

                }
                text.text = "0";
            }
        }
       public void Reseting()
        {
            tencount = 0;
            hundredcount = 0;
            onecount = 0;
            thousandcount = 0;
            foreach (var item in Allbox)
            {

                for (int i = 0; i < item.transform.childCount; i++)
                {
                    item.transform.GetChild(i).gameObject.SetActive(false);

                }
            }
            text.text = "0";
        }
        public IEnumerator Relod()
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(3);
            Party_pop.SetActive(false);
            ansercheck.color = Color.white;
            tencount = 0;
            hundredcount = 0;
            onecount = 0;
            thousandcount = 0;
            foreach (var item in Allbox)
            {
               
                for (int i = 0; i < item.transform.childCount; i++)
                {
                    item.transform.GetChild(i).gameObject.SetActive(false);

                }
            }
            no = Random.Range(1, 9999);
            //for (int i = 0; i < addno.Count; i++)
            //{
            //    if(addno[i] == no)
            //    {
            //        no = Random.Range(1, 9999);
            //        i = -1;
            //    }

            //}
            text.text = "0";
            droptext.text = no.ToString();
            addno.Add(no);
            //for (int i = parent.transform.childCount-1; i >0; i--)
            //{
            //    Destroy(parent.transform.GetChild(i).gameObject);

            //}
            //checkobj.sprite = normlsprite;

        }

        public void NextButton(bool final)
        {
            if(parent.transform.childCount == 11)
            {
                checkobj.sprite = currectanswer;
                for (int i = 0; i < parent.transform.childCount; i++)
                {
                    parent.transform.GetChild(i).gameObject.SetActive(false);
                }
                parent.GetComponent<SpriteRenderer>().sprite = currectplace;
                StartCoroutine(LevelCompleted(final));
            }
            else
            {
                checkobj.sprite = wronganswer;
                StartCoroutine(WrongAnswerAnimation());
            }
        }

        IEnumerator waitforloadScence()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(0);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
