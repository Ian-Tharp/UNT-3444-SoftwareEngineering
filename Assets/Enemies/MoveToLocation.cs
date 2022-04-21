using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLocation : MonoBehaviour
{
    private Transform locationToMove;
    private EnemyStats enemy;
    private Rigidbody2D rb;
    private Vector2 Movement;
    private GameObject TaggedLocation;

    public float Speed = 2.0f;
    public bool InPosition = false;

    public bool GetInPosition() {
        return InPosition;
    }

    void OnEnable() {
        StartCoroutine(StartRotating());
    }

    IEnumerator StartRotating() {
        yield return new WaitForSeconds(13.0f);
        InPosition = true;
    }

    void Start() {
        this.gameObject.GetComponent<DealMeleeDamage>().enabled = false;
        enemy = this.gameObject.GetComponent<EnemyStats>();

        //Setup rigidbody component to allow for change of rotation
        rb = this.GetComponent<Rigidbody2D>();
        GameObject temp = FindClosestLocation();
        Transform tempLoc = temp.GetComponent<Transform>();
        locationToMove = tempLoc;
        TaggedLocation = temp;
    }

    void DetermineSpeed() {
        if (enemy.GetEnemyType() == 2) {
            Speed = 2.15f;
        }
        else if (enemy.GetEnemyType() == 3) {
            Speed = 1.95f;
        }
    }

    void Update() {
        if (InPosition) {
            this.gameObject.GetComponent<RotateAroundObject>().enabled = true;
            GameObject temp = GameObject.FindGameObjectWithTag("Player");
            this.gameObject.GetComponent<RotateAroundObject>().SetPivot(temp);
            this.gameObject.GetComponent<MoveToLocation>().enabled = false;
        }
        else {
            //Calculate direction and angle for enemies to look at player
            Vector3 Direction = locationToMove.transform.position - this.transform.position;
            Quaternion Rotation = Quaternion.LookRotation(Vector3.forward, Direction);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Rotation, Time.deltaTime * Speed);
            //Normalize direction for movement to smooth transition into rotations
            Direction.Normalize();
            Movement = Direction;
        }
    }

    //Returns the GameObject w/tag "ShootingLocation" that is the closest to the enemy
    GameObject FindClosestLocation() {
        GameObject[] locations;
        locations = GameObject.FindGameObjectsWithTag("ShootingLocation");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 Position = transform.position;
        foreach (GameObject location in locations) {
            if (location == TaggedLocation) {
                this.gameObject.GetComponent<MoveTowardsPlayer>().enabled = true;
                GameObject temp = GameObject.FindGameObjectWithTag("Player");
                this.gameObject.GetComponent<RotateAroundObject>().SetPivot(temp);
            }
            Vector3 diff = location.transform.position - Position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = location;
                distance = curDistance;
            }
        }
        return closest;
    }

    //Update enemy movement
    void MoveEnemy(Vector2 Direction) {
        if (InPosition == false) {
            rb.MovePosition((Vector2)transform.position + (Direction * Speed * Time.deltaTime));
        }
        else {
            this.gameObject.GetComponent<MoveToLocation>().enabled = false;
        }
    }

    void FixedUpdate() {
        MoveEnemy(Movement);
    }
}
