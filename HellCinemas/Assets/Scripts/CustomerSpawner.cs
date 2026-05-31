using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public float spawnY = -20f;
    public float spawnRate = 4f;
    public float startDelay = 90f;

    [Header("Capacity Settings")]
    public int maxCustomers = 10;

    public float[] spawnXPoints = { -30f, -15f, 0f, 15f, 30f };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(startDelay);
        InvokeRepeating(nameof(TrySpawn), 0f, spawnRate);
    }

    void TrySpawn()
    {
        int currentCustomerCount = GameObject.FindGameObjectsWithTag("Customer").Length;

        if (currentCustomerCount < maxCustomers)
        {
            Spawn();
        }
        else
        {
            Debug.Log("Max Customers");
        }
    }

    void Spawn()
    {
        float chosenX = spawnXPoints[Random.Range(0, spawnXPoints.Length)];
        Instantiate(customerPrefab, new Vector3(chosenX, spawnY, 0), Quaternion.identity);
    }
}
