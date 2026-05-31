using UnityEngine;
using TMPro;
public class Customer : MonoBehaviour
{
    public enum OrderType { Popcorn, Drink, Both }
    public OrderType myOrder;

    [Header("UI")]
    public TextMeshProUGUI orderText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myOrder = (OrderType)Random.Range(0, 3);
        UpdateOrderUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateOrderUI()
    {
        if (orderText != null)
        {
            orderText.text = "" + myOrder.ToString();
            Debug.Log("UI Updated to: " + myOrder.ToString());
        }
    }
}
