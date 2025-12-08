using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TransitionType
{
    Wrap,
    Scene
}

public class Transition : MonoBehaviour
{
    [SerializeField] TransitionType transitionType;
    [SerializeField] string sceneNameToTransition;
    [SerializeField] Vector3 targetPosition;

    Transform destination;

    void Start()
    {
        destination = transform.GetChild(1);
    }

    internal void InitiateTransition(Transform toTransform)
    {
        switch (transitionType)
        {
            case TransitionType.Wrap:
                toTransform.position = new Vector3(
                    destination.position.x,
                    destination.position.y,
                    toTransform.position.z);
                break;
            case TransitionType.Scene:
                GameSceneManager.instance.InitSwitchScene(sceneNameToTransition, targetPosition);
                break;
        }
    }
}
