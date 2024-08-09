using Human_Body_Part.Puzzle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Human_Body_Part.Puzzle
{
    public class GetClicked : MonoBehaviour
    {
        public GameController controller;
        public int image_id;
        public int location_id;
        public Vector3 correct_Position;
        public Vector3 this_position;
        public bool Corrected;

        private void Start()
        {
            this_position = transform.position;
        }
        private void OnMouseEnter()
        {
            Debug.Log("enteredobject" + this.gameObject.name);
            if (controller.clicked)
            {
                Debug.Log("clicked");
                OnMouseDown(); 
            }
        }

        private void OnMouseDown()
        {

            if (controller.totorial.totorialplaying)
                return;
            controller.clicked = true;
            controller.firstClickedObject = this.gameObject;
            controller.isFirstClicked = true;
        }
        private void OnMouseUp()
        {
            controller.clicked = false;
            controller.secondClickedObject = this.gameObject;
            controller.isFirstClicked = false;
           

            controller.SwitchingData();
        }
    }

}