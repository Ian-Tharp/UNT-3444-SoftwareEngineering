using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundObject : MonoBehaviour
{
    [SerializeField]
    private GameObject pivotObject;

    private GameObject player;
    public float RotationSpeed = 1.0f;
    public float Speed = 2.0f;

    void Start() {
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        player = temp;
        RotationSpeed = Random.Range(20,30);
    }

    void OnEnable() {
        StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject() {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    public void SetPivot(GameObject pivot) {
        pivotObject = pivot;
    }

    void Update() {
        Vector3 Direction = player.transform.position - this.transform.position;
        Quaternion Rotation = Quaternion.LookRotation(Vector3.forward, Direction);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Rotation, Time.deltaTime * Speed);
        transform.RotateAround(pivotObject.transform.position, new Vector3(0,0,1), RotationSpeed * Time.deltaTime);    
    }

    void FixedUpdate() {
        this.gameObject.transform.localScale += new Vector3(1,1,1);
    }
}
