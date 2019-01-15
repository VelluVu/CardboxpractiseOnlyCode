using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumpling : MonoBehaviour {

    public AudioSource hitSoundPlayer;
    public List<AudioClip> hitSounds = new List<AudioClip>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hole"))
        {
            other.GetComponent<ScoreCounter>().HitsTheToilet();
            Destroy(gameObject);
        }         
    }
    private void OnCollisionEnter(Collision collision)
    {
        hitSoundPlayer.clip = hitSounds[Random.Range(0, hitSounds.Count)];
        hitSoundPlayer.Play();
    }

}
