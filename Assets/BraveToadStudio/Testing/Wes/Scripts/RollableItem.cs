using UnityEngine;
using System.Collections;

public class RollableItem : MonoBehaviour {

    private GameObject objectLayer;

    void OnCollisionEnter(Collision other) {
                Transform[] objs = other.gameObject.transform.parent.transform.GetComponentsInChildren<Transform>();
                Debug.Log(other.gameObject.transform.parent.name);
                foreach (Transform t in objs) {
                    if (t.gameObject.tag.Equals("ObjectLayer")) {
                        objectLayer = t.gameObject;
                    }
                }

                GetComponent<Collider>().enabled = false;
                transform.position = objectLayer.transform.position;
                transform.rotation = objectLayer.transform.rotation;
                transform.parent = objectLayer.transform;
    }
}