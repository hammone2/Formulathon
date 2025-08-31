using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float restartDelay = 1f;
    public GameObject completeLevelUI;
    public Transform roadSpawner;
    public Transform roadSpawnSignal;
    public Transform trackObjectPool;
    public Transform activeRaceTrackPool;
    public Transform carSpawners;
    public Transform carPool;
    public Transform activeCarPool;
    public bool gameOver = false;
    [SerializeField] private GameObject recentRoad;

    public float worldSpeedMAX = 75f;
    public float worldSpeedCURRENT = 0f;

    public float worldSpeedACC = 45f;

    public PlayerController player;
    public FollowPlayer playerCam;

    private void Awake()
    {
        instance = this;
    }

    public void FixedUpdate()
    {
        if (recentRoad.transform.position.z <= roadSpawnSignal.position.z)
        {
            int newRoadSegment = Random.Range(0, trackObjectPool.childCount);
            recentRoad = trackObjectPool.GetChild(newRoadSegment).gameObject;

            recentRoad.transform.parent = activeRaceTrackPool;
            recentRoad.transform.position = roadSpawner.position;
            recentRoad.SetActive(true);
        }
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }

    public IEnumerator Respawn()
    {
        while (worldSpeedCURRENT != 0f)
        {
            worldSpeedCURRENT = Mathf.MoveTowards(worldSpeedCURRENT, 0f, worldSpeedACC * Time.deltaTime);

            yield return null;
        }

        for (int i = activeCarPool.childCount -1; i >= 0; i--) //despawn the cars so the player can spawn back safely
        {
            GameObject car = activeCarPool.GetChild(i).gameObject;
            car.SetActive(false);
            car.transform.parent = carPool;
            car.transform.position = carPool.position;
        }

        Invoke("RespawnSequence", restartDelay);
    }

    private void RespawnSequence()
    {
        player.Respawn();
        StartCoroutine(Accelerate());
    }

    private IEnumerator Accelerate()
    {
        while (worldSpeedCURRENT != worldSpeedMAX)
        {
            worldSpeedCURRENT = Mathf.MoveTowards(worldSpeedCURRENT, worldSpeedMAX, worldSpeedACC * Time.deltaTime);
            
            yield return null;
        }

    }

    public IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(3f);
        StartGame();
    }

    public void StartGame()
    {
        SpawnCars();
        StartCoroutine(Accelerate());
        player.enabled = true;
    }

    public void EndGame()
    {
        if (gameOver == false)
        {
            gameOver = true;
            Debug.Log("GAME OVER!");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SpawnCars()
    {
        StartCoroutine(CarSpawnRoutine(Random.Range(0.1f, 1f)));
    }

    IEnumerator CarSpawnRoutine(float timer)
    {
        int numberToSpawn = Random.Range(1, 5);

        List<Transform> pointsUsed = new List<Transform>();
        for(int i = 0; i < numberToSpawn; i++)
        {
            if (carPool.childCount == 0)
            {
                Debug.LogWarning("No cars left in the pool!");
                continue;
            }

            int spawnPointIndex = Random.Range(0, carSpawners.childCount);
            Transform spawnPoint =  carSpawners.GetChild(spawnPointIndex).gameObject.transform;

            if (pointsUsed.Contains(spawnPoint))
                continue;

            //spawn the car
            int newCarIndex = Random.Range(0, carPool.childCount);
            GameObject newCar = carPool.GetChild(newCarIndex).gameObject;
            if (newCar == null)
                continue;
            newCar.transform.parent = activeCarPool;
            newCar.transform.position = spawnPoint.position;
            newCar.SetActive(true);
        }

        yield return new WaitForSeconds(timer);

        SpawnCars();
    }
}
