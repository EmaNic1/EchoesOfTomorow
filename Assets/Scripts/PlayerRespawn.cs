using System;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{

    [SerializeField] Vector3 respawnPointPosition;
    [SerializeField] string respawnPointScene;

    internal void StartResoawn()
    {
        GameSceneManager.instance.Respawn(respawnPointPosition, respawnPointScene);
    }
}
