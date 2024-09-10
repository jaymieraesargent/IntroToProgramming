using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public TorchHandler torch;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Torch"))
        {
            torch.PickUp();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Power"))
        {
            torch.power = 1;
            Destroy(other.gameObject);
        }
    }
}
