using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

	void OnTriggerEnter2D ()
	{
		Debug.Log("YOU WON!");
		Score.CurrentScore += 100;
		CarSpawner.ClearList();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
