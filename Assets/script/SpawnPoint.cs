using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void Start()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.transform.position = transform.position;
        }
    }
}
