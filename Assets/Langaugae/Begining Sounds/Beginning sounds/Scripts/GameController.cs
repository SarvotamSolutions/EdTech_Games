using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DesignPatterns.Singleton;
using UnityEngine.SceneManagement;

 namespace Laguage.beginning_sounds.matchtheSound
{
    public class GameController : Singleton<GameController>
    {

        
        public LineRenderer selectedline;
        public Character[] alltypeofCharacter;
        public SpriteRenderer[] iconOption;
        public TextMeshPro[] alltext;
        List<int> allselectedno = new List<int>();
        public Material currectanswer;
        public Material WrongAnswer;
        public Sprite currectanswerarrow,wronganswerarrow;
        public int totalanswered;
        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;

        // Start is called before the first frame update
        void Start()
        {

            GameStart();
        }
        void GameStart()
        {
           
            foreach (var item in iconOption)
            {
                int randomno = Random.Range(0, alltypeofCharacter.Length);

                for (int i = 0; i < allselectedno.Count; i++)
                {
                    if (randomno == allselectedno[i])
                    {
                        randomno = Random.Range(0, alltypeofCharacter.Length);
                        i = -1;
                    }

                }
                allselectedno.Add(randomno);
                item.sprite = alltypeofCharacter[allselectedno[allselectedno.Count - 1]].Icon;
                item.transform.SetSiblingIndex(Random.Range(0, item.transform.parent.childCount));
            }
            for (int i = 0; i < alltext.Length; i++)
            {
                alltext[i].transform.parent.GetComponent<drager>().Answeroption = iconOption[i].gameObject; 
                alltext[i].text = alltypeofCharacter[allselectedno[i]].letter;
            }
        }
        void Update()
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            if (selectedline)
                selectedline.SetPosition(1, pos);

        }
        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
        public void ScenecChange()
        {
            StartCoroutine(LevelCompleted());
        }
    }
}

