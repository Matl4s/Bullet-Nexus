using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoGUI : MonoBehaviour
{

    public PlayerShoot PlayerShoot;
    public TextMeshProUGUI text;

    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        text.text = $"{PlayerShoot.currentClip} / {PlayerShoot.clipSize} | {PlayerShoot.currentAmmo} / {PlayerShoot.maxAmmo}";
    }
}
