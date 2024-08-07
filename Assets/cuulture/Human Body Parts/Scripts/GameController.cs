using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Human_Body_Part.Puzzle
{
    public class GameController : GameControllerforAll
    {
        public GameObject[] all_Object;
        public GameObject firstClickedObject, secondClickedObject;
        public GameObject ObjectSuccessImage;
        public bool isFirstClicked;
        Vector3 Temp1;
        Vector3 Temp2;
        public int checking_count;
        public SpriteRenderer Object_Name_BG;
        public SpriteRenderer Object_Image;
        public Sprite ObjetNameSuccessImage;


        protected override void Start()
        {
            
        }

        public void SwitchingData()
        {
            if (secondClickedObject != null)
            {
                if (firstClickedObject.GetComponent<GetClicked>().image_id == secondClickedObject.GetComponent<GetClicked>().image_id)
                {
                    int Temp1 = firstClickedObject.transform.GetSiblingIndex();
                    int Temp2 = secondClickedObject.transform.GetSiblingIndex();
                    firstClickedObject.transform.SetSiblingIndex(Temp2);
                    secondClickedObject.transform.SetSiblingIndex(Temp1);

                    Checking();
                    firstClickedObject = null; secondClickedObject = null;
                }
                else
                {
                    firstClickedObject = null; secondClickedObject = null;
                }
            }
        }

        public void Checking()
        {
            int index_FirstObject = Array.IndexOf(all_Object, firstClickedObject.gameObject, 0, all_Object.Length);
            int index_SecondObject = Array.IndexOf(all_Object, secondClickedObject.gameObject, 0, all_Object.Length);
            GameObject Temp = all_Object[index_FirstObject];
            all_Object[index_FirstObject] = all_Object[index_SecondObject];
            all_Object[index_SecondObject] = Temp;

            if (firstClickedObject.GetComponent<GetClicked>().location_id == index_SecondObject)
            {
                firstClickedObject.GetComponent<GetClicked>().Corrected = true;
            }
            else
            {
                firstClickedObject.GetComponent<GetClicked>().Corrected = false;
            }

            if (secondClickedObject.GetComponent<GetClicked>().location_id == index_FirstObject)
            {
                secondClickedObject.GetComponent<GetClicked>().Corrected = true;
            }
            else
            {
                secondClickedObject.GetComponent<GetClicked>().Corrected = false;
            }

            for (int c = 0; c < all_Object.Length; c++)
            {
                if (all_Object[c].GetComponent<GetClicked>().Corrected == true)
                {
                    checking_count++;
                    if (checking_count == all_Object.Length - 1)
                    {
                        GameOver();
                    }
                }
                else
                {
                    checking_count = 0;
                    break;
                }
            }
        }

        void GameOver()
        {
            Object_Image.enabled = false;
            Object_Name_BG.color = Color.white;
            Object_Name_BG.sprite = ObjetNameSuccessImage;
            for (int o = 0; o < all_Object.Length; o++)
            {
                all_Object[o].gameObject.SetActive(false);
            }
            ObjectSuccessImage.SetActive(true);
            StartCoroutine(LevelCompleted());
        }
    }
}