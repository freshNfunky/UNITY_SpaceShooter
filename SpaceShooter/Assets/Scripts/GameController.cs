﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private int score; 
	private bool gameOver;
	private bool restart;

	void Start ()
	{
		score = 0;
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text ="";
		UpdateScore ();
		StartCoroutine(SpawnWaves());
	}

	void Update ()
	{
		if (restart)
		{
			if(Input.GetKeyDown(KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}

	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards[Random.Range ( 0, hazards.Length)];
				Vector3 spawnPosition = new Vector3(Random.Range (-spawnValues.x,spawnValues.x),spawnValues.y,spawnValues.z);
				Quaternion spawnRotate = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotate);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}

	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue ;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		gameOver = true;
		gameOverText.text = "Game Over!";
	}
}
