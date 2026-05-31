using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopcornPopper : MonoBehaviour
{
    [Header("Inventory")]
    public PopcornMerchandiser merchandiser;

    [Header("UI References")]
    public Slider amountOfPopcornSlider;
    public TextMeshProUGUI timerText;

    [Header("Settings")]
    public float totalCookTime = 60f;
    public int popcornAmount = 10;
    public float interactionDistance = 6f;

    private float elapsedTime = 0f;
    private bool isCooking = false;
    private bool isFinished = false;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        amountOfPopcornSlider.minValue = 0;
        amountOfPopcornSlider.maxValue = popcornAmount;
        amountOfPopcornSlider.value = 0;

        timerText.text = "Press E to Pop";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckForInteraction();
        }

        if (isCooking)
        {
            elapsedTime += Time.deltaTime;

            float currentBatch = (elapsedTime / totalCookTime) * popcornAmount;
            amountOfPopcornSlider.value = currentBatch;

            float timeLeft = Mathf.Max(0, totalCookTime - elapsedTime);

            if (timerText)
            {
                timerText.text = totalCookTime.ToString("F0") + "s";
            }
            
            if (elapsedTime >= totalCookTime)
            {
                isCooking = false;
                isFinished = true;
                timerText.text = "Press E to Collect";
            }
        }
    }

    void CheckForInteraction()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance)
        {
            if (!isCooking && !isFinished)
            {
                isCooking = true;
                elapsedTime = 0;
            }
            else if (elapsedTime >= totalCookTime && merchandiser != null && merchandiser.CanFitBatch(popcornAmount))
            {
                CollectPopcorn();
            }
            else
            {
                if (timerText)
                {
                    timerText.text = "Merchandiser Full";
                }
            }
        }
    }

    void CollectPopcorn()
    {
        isFinished = false;
        elapsedTime = 0;
        amountOfPopcornSlider.value = 0;
        timerText.text = "Press E to Start";

        if (merchandiser != null)
        {
            merchandiser.AddStock(popcornAmount);
        }
    }
}
