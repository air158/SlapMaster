using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip[] HitMusicClips;
    public AudioSource HitMusic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void changeHitMusic(int hurt){
        if(hurt==10){
            HitMusic.clip=HitMusicClips[2];
        }
        else if(hurt==6){
            HitMusic.clip=HitMusicClips[1];
        }
        else{
            HitMusic.clip=HitMusicClips[0];
        }
        PlayHitMusic();
    }

    void PlayHitMusic(){
        if (HitMusic!=null&&!HitMusic.isPlaying)
        {
            HitMusic.Play();
        }
    }
    void StopHitMusic(){
        if (HitMusic!=null&&!HitMusic.isPlaying)
        {
            HitMusic.Stop();
        }
    }
    void setMusicVolume(float volume)
    {
        if (HitMusic != null && !HitMusic.isPlaying)
        {
            HitMusic.volume = volume;
        }
    }
}
