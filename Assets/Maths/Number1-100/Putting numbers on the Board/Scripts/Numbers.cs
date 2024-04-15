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
       
        private void Awake()
        {
            no =int.Parse(gameObject.name);
            if(competed)
                transform.GetChild(0).GetComponent<TextMeshPro>().text = no.ToString();
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
