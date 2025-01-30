using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfDonuts { get; private set; }
    private int maxDonuts = 6;

    public Text donutText;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickUpItem();
        }
    }

    private void TryPickUpItem()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            Interactable interactable = hitCollider.GetComponent<Interactable>();
            if (interactable != null && interactable.CanBePickedUp())
            {
                if (interactable.itemName == "Donut")
                {
                    if (NumberOfDonuts < maxDonuts)
                    {
                        NumberOfDonuts++;
                        Destroy(hitCollider.gameObject);
                        Debug.Log("Picked up a Donut! Total: " + NumberOfDonuts);
                    }
                    else
                    {
                        Debug.Log("You've collected all the Donuts!");
                    }

                    UpdateUI();
                }
                break;
            }

        }
    }

    private void UpdateUI()
    {
        if (donutText != null)
        {
            donutText.text = "Donuts: " + NumberOfDonuts + "/" + maxDonuts;
        }
    }
}
