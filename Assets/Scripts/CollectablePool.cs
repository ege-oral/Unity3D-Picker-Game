using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePool : MonoBehaviour
{
    private int counter = 0;

    private void OnTriggerEnter(Collider other) {
        counter += 1;
    }

    private void Update() {
        print(counter);
    }
}
