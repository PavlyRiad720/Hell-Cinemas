using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

public class DrinkDispenser : MonoBehaviour
{
    [Header("UI References")]
    public Slider progressSlider;
    public TextMeshProUGUI timeText;

    [Header("Settings")]
    public float timeToMakeDrink = 3.0f;
    public float interactionDistance = 10f;

    private bool isMakingDrink = false;
    private bool canInteract = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (progressSlider != null)
        {
            progressSlider.maxValue = timeToMakeDrink;
            UpdateUI(0);
        }
    }

    private void Update()
    {
        if (canInteract && !isMakingDrink && Input.GetKeyDown(KeyCode.E))
        {
            StartMakingDrink();
        }
    }

    public void StartMakingDrink()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (!isMakingDrink && distance <= interactionDistance)
        {
            StartCoroutine(MakeDrinkRoutine());
        }
    }

    IEnumerator MakeDrinkRoutine()
    {
        isMakingDrink = true;
        float currentTime = timeToMakeDrink;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateUI(currentTime);
            yield return null;
        }

        FinishDrink();
    }

    void UpdateUI(float timeLeft)
    {
        float clampedTime = Mathf.Max(0, timeLeft);

        if (progressSlider != null)
        {
            progressSlider.value = timeToMakeDrink - clampedTime;
        }

        if (timeText != null)
        {
            timeText.text = clampedTime.ToString("F1") + "s";
        }
    }

    void FinishDrink()
    {
        isMakingDrink = false;
        UpdateUI(0);
    }
}
