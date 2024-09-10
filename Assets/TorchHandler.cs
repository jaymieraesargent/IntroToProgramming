using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchHandler : MonoBehaviour
{
    public GameObject torch;
    public Light light;
    public float power = 1;
    public bool isOn;
    private void Awake()
    {        
        torch.SetActive(false);
    }
    public void PickUp()
    {
        torch.SetActive(true);
        isOn = true;
    }
    private void Update()
    {
        if (isOn && power > 0)
        {
            power -= 0.05f * Time.deltaTime;
            light.intensity = Mathf.Clamp01(power);
        }        
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            light.enabled = isOn;
        }
    }
}
