using UnityEngine;
using TMPro;


public class PopcornMerchandiser : MonoBehaviour
{
    public TextMeshProUGUI inventoryText;
    public int maxCapacity = 50;

    public float interactionDistance = 10f;

    private int currentStock = 0;
    private bool playerIsCarrying = false;

    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (player == null) return;

            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance <= interactionDistance)
            {
                if (currentStock > 0 && !playerIsCarrying)
                {
                    GrabOnePopcornBag();
                }
            }
            else if (currentStock <= 0)
            {
                inventoryText.text = "Out of Stock";
            }
        }
    }

    public bool CanFitBatch(int amount)
    {
        return (currentStock + amount) <= maxCapacity;
    }

    public void AddStock(int amount)
    {
        currentStock += amount;
        UpdateUI();
    }

    public void GrabOnePopcornBag()
    {
        currentStock--;
        playerIsCarrying = true;
        UpdateUI();
    }

    public void PopcornBagSold()
    {
        playerIsCarrying = false;
    }

    void UpdateUI()
    {
        if (inventoryText != null)
        {
            inventoryText.text = $"{currentStock}";
        }
    }
}
