using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
namespace Maths.Number1to100.Putting_numbers_Board
{
    public class Numbers : MonoBehaviour
    {
        public int no;
        public bool competed;

        private void Start()
        {
          
            if (GameController.instace.randomNo)
                competed = Random.Range(0, 2) == 1 ? true : false;
            if (no == 1)
                competed = false;
            no =int.Parse(gameObject.name);
            if (competed)
            {
                GetComponent<SpriteRenderer>().sprite = GameController.instace.currectanswer;
                transform.GetChild(0).GetComponent<TextMeshPro>().text = no.ToString();
            }
            else
                transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
        }
        private void OnMouseUpAsButton()
        {
            if (competed || IsmouseOverUI() )
                return;
            SelectedthisNumber();
        }
        public void SelectedthisNumber()
        {
            foreach (var item in GameController.instace.allnumber)
            {
                if (!item.competed)
                    item.GetComponent<SpriteRenderer>().sprite = GameController.instace.notselcted;
            }
            GetComponent<SpriteRenderer>().sprite = GameController.instace.selectedsprire;
            GameController.instace.selectedno = this;
            GameController.instace.Ai.textResult = transform.GetChild(0).GetComponent<TextMeshPro>();
            GameController.instace.Ai_recognizer.Recognigingnumber = no.ToString();
            GameController.instace.Ai_recognizer.Changerecogniger();
        }
        public bool IsmouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
        

    }
}
