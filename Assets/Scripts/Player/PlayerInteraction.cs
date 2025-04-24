using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float Distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<PlayerCamera>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * Distance);
        RaycastHit hitInfo; //proměnná, která uloží informace ohledně kolize
        if (Physics.Raycast(ray, out hitInfo, Distance,mask))
        {
            if(hitInfo.collider.GetComponent<InteractableMain>() != null) 
            {
                InteractableMain interactable = hitInfo.collider.GetComponent<InteractableMain>();
                playerUI.UpdateText(interactable.promptText);
                if(inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
