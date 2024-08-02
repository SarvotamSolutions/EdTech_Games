using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    public AudioClip[] audioClips;
    public AudioSource backgroundsound;

    public Totorial totorial;
    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        backgroundsound = GetComponent<AudioSource>();
        backgroundsound.clip = audioClips[Random.Range(0, audioClips.Length)];
    }
    public void StopMusic()
    {
        Destroy(this.gameObject);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex ==0)
        {
            
            backgroundsound.Stop();
        }
      
    }

}
