using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Laguage
{
    [CreateAssetMenu(menuName = "leterConins")]
    public class AllCharacter : ScriptableObject
    {
        public Character[] sameLetter;
        public string Letter;
        public Sprite letterSprite;
        public Character[] RelatedCharacter;
        public Color hintcolor;
        public AudioClip lettersound;
    }
}
