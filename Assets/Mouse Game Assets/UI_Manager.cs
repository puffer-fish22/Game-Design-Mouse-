using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public TMPro.TMP_Text scoreText;
    private int _score = 0;

void Start(){
	scoreText.text = "Score: " + 0;
	}


    public void AddScore(int _points)
    {
	_score += _points;
	//scoreText.text = "Score: " + _score.ToString();
	//playerScore += _score;
        scoreText.text = "Score: " + _score.ToString();
    }

private void OnTriggerEnter(Collider other)
    {
        
	if(other.tag == "Coin"){
	AddScore(1);
	Destroy(other.gameObject);
	}

	}

}