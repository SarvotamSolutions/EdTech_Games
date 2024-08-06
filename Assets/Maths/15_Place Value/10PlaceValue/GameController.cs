using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maths.placeHolder.value
{
    public class GameController : Singleton<GameController>
    {
        public Totorial totorial;
        public GameObject dropLoction;
        public GameObject parent;
        public GameObject insasiteobj;
        public Image checkobj;
        public TextMeshPro text;
        public Sprite currectplace;
        public Sprite currectanswer, wronganswer, normlsprite;
        public int noadd;
        public bool thousand;
        [Space(10)]
        public GameObject[] Allbox;
        public bool randomno;
        public int thousandcount, hundredcount, tencount, onecount;
        public int totalpoint;
        public Button DoneButton;


        public int no;
        public TextMeshPro droptext;
        private List<int> addno = new List<int>();
        public float currectanswerinteval;

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
            for (int k = 0; k < ansercheck.Length; k++)
            {
                ansercheck[k].color = Color.white;
                ansercheck[k].transform.GetChild(10).gameObject.SetActive(true);
                ansercheck[k].gameObject.transform.GetChild(10).GetComponent<TextMeshPro>().color = new Color(0.3176471f, 0.1803922f, 0.4901961f, 1f);
            }
            wrongAnswer_animtion.SetActive(false);

            gamePlay = true;
            DoneButton.interactable = true;
        }

        public void loadPoint()
        {
            totalpoint = thousandcount * 1000 +
                    hundredcount * 100 +
                   tencount * 10 +
                   onecount;

            text.text = totalpoint.ToString();
        }


        public bool Neartodestination(GameObject obj, GameObject droploction)
        {
            if (Vector3.Distance(obj.transform.position, droploction.transform.position) < 1)
            {
                text.text = (parent.transform.childCount * noadd).ToString();
                GameObject objects = Instantiate(insasiteobj, parent.transform);
                if (thousand)
                {
                    objects.transform.localPosition = new Vector3(dropLoction.transform.position.x + .2f * (parent.transform.childCount - 1), dropLoction.transform.position.y - .2f * (parent.transform.childCount - 1), dropLoction.transform.position.z + .1f * (parent.transform.childCount - 1));

                }
                return true;
            }
            return false;
        }
        public int minimumno;
        public int maxno;

        void Start()
        {
            no = Random.Range(minimumno, maxno);
            addno.Add(no);
            if (randomno)
            {
                droptext.text = "Make " + no.ToString();
            }
        }
        public SpriteRenderer[] ansercheck;
        public void totalcheck()
        {
            if (totorial.totorialplaying)
                return;
            gamePlay = false;
            DoneButton.interactable = false;
            if (no == totalpoint)
            {
                for (int k = 0; k < ansercheck.Length; k++)
                {
                    ansercheck[k].color = new Color(0.4078431f, 0.682353f, 0.1960784f, 1);
                    ansercheck[k].transform.GetChild(10).gameObject.SetActive(true);
                    ansercheck[k].gameObject.transform.GetChild(10).GetComponent<TextMeshPro>().color = Color.white;
                }
                if (addno.Count >= 10)
                {
                    StartCoroutine(LevelCompleted(true));
                    return;
                }
                StartCoroutine(Relod());
            }
            else
            {
                for (int k = 0; k < ansercheck.Length; k++)
                {
                    ansercheck[k].color = new Color(0.8823529f, 0.2745098f, 0.3137255f, 1);
                    ansercheck[k].transform.GetChild(10).gameObject.SetActive(true);
                    ansercheck[k].gameObject.transform.GetChild(10).GetComponent<TextMeshPro>().color = Color.white;
                }
                StartCoroutine(WrongAnswerAnimation());
            }
        }

        public void RandomNOcheck()
        {
            if (parent.transform.childCount == no + 1)
            {
                checkobj.sprite = currectanswer;
                if (addno.Count >= 10)
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
                    item.transform.GetChild(10).gameObject.SetActive(true);
                }
            }
            text.text = "0";
        }
        public IEnumerator Relod()
        {
            Party_pop.SetActive(true);
            yield return new WaitForSeconds(currectanswerinteval);
            Party_pop.SetActive(false);
            for (int k = 0; k < ansercheck.Length; k++)
            {
                ansercheck[k].color = Color.white;
                ansercheck[k].transform.GetChild(10).gameObject.SetActive(true);
                ansercheck[k].gameObject.transform.GetChild(10).GetComponent<TextMeshPro>().color = new Color(0.3176471f, 0.1803922f, 0.4901961f, 1f);
            }
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
                item.transform.GetChild(10).gameObject.SetActive(true);
            }
            no = Random.Range(minimumno, maxno);
            text.text = "0";
            droptext.text = "Make " + no.ToString();
            addno.Add(no);
            gamePlay = true;
            DoneButton.interactable = true;
        }

        public void NextButton(bool final)
        {
            if (parent.transform.childCount == 11)
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
    }
}
