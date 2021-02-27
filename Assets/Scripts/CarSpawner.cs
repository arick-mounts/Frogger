using UnityEngine;
using System.Collections.Generic;

public class CarSpawner : MonoBehaviour {

	public static float spawnDelay = .3f;

	public GameObject car;

	public Transform[] spawnPoints;

	public static List<GameObject> cars = new List<GameObject>();

	float nextTimeToSpawn = 0f;

	void Update ()
	{
		if (nextTimeToSpawn <= Time.time)
		{
			SpawnCar();
			nextTimeToSpawn = Time.time + spawnDelay;
		}
		
	}

	public static void ClearList()
    {
		for (int i = cars.Count-1; i >= 0; i--) {
			cars[i].GetComponent<Car>().Destroy();
		}
		
	}

	void SpawnCar ()
	{
		int randomIndex = Random.Range(0, spawnPoints.Length);
		Transform spawnPoint = spawnPoints[randomIndex];

		GameObject spawn = Instantiate(car, spawnPoint.position, spawnPoint.rotation);
		cars.Add(spawn);
		
	}
	public  void SpawnCar(float x, float y, float facing, float speed)
	{
		
		GameObject spawn = Instantiate(car, new Vector3(x,y,0), new Quaternion(0,0,facing, 0));
		car.GetComponent<Car>().speed = speed;
		cars.Add(spawn);

	}

}
