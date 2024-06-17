using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCameraEnabler : MonoBehaviour
{
    private CinemachineInputProvider cinemachineInputProvider;

    private void Start()
    {
        cinemachineInputProvider =GetComponent<CinemachineInputProvider>();
        cinemachineInputProvider.enabled = false;
    }
    public void AllowCameraInput(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            cinemachineInputProvider.enabled = true;
        }

        if (_context.canceled)
        {
            cinemachineInputProvider.enabled = false;
        }
    }
}
