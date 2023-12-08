using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{

    [SerializeField]
    Camera _playerCamera;
    [SerializeField]
    LayerMask _interactableMask;
    [SerializeField]
    float _interactableRange;


    StarterAssetsInputs _inputs;

    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (_inputs.interact)
        {
            Ray cameraRay = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            Debug.DrawRay(cameraRay.origin, cameraRay.GetPoint(_interactableRange), Color.green);
            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, _interactableRange, _interactableMask))
            {
                hitInfo.transform.gameObject.TryGetComponent<IInteractable>(out IInteractable interactableObject);
                interactableObject.OnInteract();
            }
        }
    }
}
