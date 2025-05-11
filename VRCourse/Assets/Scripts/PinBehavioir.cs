using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class PinBehavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.AddScore(10);
            gameObject.SetActive(false);
        }
    }
}
