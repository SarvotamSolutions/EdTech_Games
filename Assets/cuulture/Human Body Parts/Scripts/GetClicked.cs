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
        private void OnMouseUp()
        {
            if(controller.isFirstClicked == false)
            {
                controller.firstClickedObject = this.gameObject;
                controller.isFirstClicked = true;
            }
            else
            {
                controller.secondClickedObject = this.gameObject;
                controller.isFirstClicked = false;
            }

            controller.SwitchingData();
        }
    }

}