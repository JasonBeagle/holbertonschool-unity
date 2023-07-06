using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float speed;
	private int score = 0;
	public int health = 5;
	public Text scoreText;
	public Text healthText;
	public Text winLoseText;
	public Image winLoseBG;

	// Start is called before the first frame update
	void Start()
	{
		winLoseBG.enabled = false;
	}
	void Update()
	{
		if (health == 0)
		{
			winLoseBG.enabled = true;
			winLoseText.text = "Game Over!";
			winLoseText.color = Color.white;
			winLoseBG.color = Color.red;
			//Debug.Log("Game Over!");
			StartCoroutine(LoadScene(3));
		}
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			SceneManager.LoadScene ("menu");
		}
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		if (Input.GetKey("up") || Input.GetKey("w"))
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
		}
		if (Input.GetKey("right") || Input.GetKey("d"))
		{
			GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
		}
		if (Input.GetKey("down") || Input.GetKey("s"))
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);
		}
		if (Input.GetKey("left") || Input.GetKey("a"))
		{
			GetComponent<Rigidbody>().velocity = new Vector3(-speed, 0, 0);
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Pickup")
		{
			score += 1;
			SetScoreText();
			Destroy(other.gameObject);
			//Debug.Log("Score: " + score);
		}
		if (other.tag == "Trap")
		{
			health -= 1;
			SetHealthText ();
			//Debug.Log("Health: " + health);
		}
		if (other.tag == "Goal")
		{
			winLoseBG.enabled = true;
			winLoseText.text = "You Win!";
			winLoseText.color = Color.black;
			winLoseBG.color = Color.green;
			//Debug.Log("You win!");
			StartCoroutine(LoadScene(3));
		}
	}

	void SetScoreText()
	{
		scoreText.text = "Score: " + score;
	}

	void SetHealthText()
	{
		healthText.text = "Health: " + health;
	}

	IEnumerator LoadScene(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
