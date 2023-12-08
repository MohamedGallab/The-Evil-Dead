using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ShootingController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera _aimCamera;

    [SerializeField]
    float _normalSensitivity = 1.0f;
    [SerializeField]
    float _aimSensitivity = 0.75f;

    StarterAssetsInputs _inputs;
    ThirdPersonController _thirdPersonController;

    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputs.aim)
        {
            _aimCamera.gameObject.SetActive(true);
            _thirdPersonController.Sensitivity = _aimSensitivity;
        }
        else
        {
            _aimCamera.gameObject.SetActive(false);
            _thirdPersonController.Sensitivity = _normalSensitivity;
        }
    }
}
