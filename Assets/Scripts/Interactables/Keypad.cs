using UnityEngine;

public class Keypad : InteractableMain
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Zde se bude psát co vlastně ta interakce má vůbec dělat

    protected override void Interact()
    {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }
}
