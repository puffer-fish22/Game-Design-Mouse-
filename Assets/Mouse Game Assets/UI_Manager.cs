using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public TMPro.TMP_Text scoreText;
    private int _score = 0;

   private AudioSource source;
    public AudioClip Coin_Sound1;
public AudioClip Rat_Sound1;
public AudioClip Rat_Sound2;


void Start(){
	scoreText.text = "Money: " + 0;
	source = GetComponent<AudioSource>();
	}


    public void AddScore(int _points)
    {
	_score += _points;
	//scoreText.text = "Money: " + _score.ToString();
	//playerScore += _score;
        scoreText.text = "Money: " + _score.ToString();
	if(_score == 2){
source.PlayOneShot(Rat_Sound1);
	}
    }

private void OnTriggerEnter(Collider other)
    {
        
	if(other.tag == "Coin"){
	AddScore(2);
	source.PlayOneShot(Coin_Sound1);
	}
	if(other.tag == "Object"){
	AddScore(1);
	source.PlayOneShot(Coin_Sound1);
	}
	if(other.tag == "Cheese"){
	AddScore(5);
	source.PlayOneShot(Coin_Sound1);
	source.PlayOneShot(Rat_Sound2);
	}
	//Destroy(other.gameObject);
	//other.simulated = true;
	}



}