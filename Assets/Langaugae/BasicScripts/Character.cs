using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Laguage
{
    [CreateAssetMenu(menuName = "character")]
    public class Character : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public string letter;
        public Sprite relatedsdprite;
        public AudioClip Sound;

    }
   
}