using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_Manager : MonoBehaviour
{
    public GameObject[] entities;
    public GameObject[] pickups;
    public Car_Cont player;
    public int spawn_timer;
    public GameObject pattern1;
    public GameObject pattern2;
    public GameObject pattern3;
    // Start is called before the first frame update
    void Start()
    {
        spawn_timer = 0;
        entities = GameObject.FindGameObjectsWithTag("Car");
        pickups = GameObject.FindGameObjectsWithTag("Coin");
    }

    public void Begin()
    {
        foreach (GameObject vehicle in entities)
        {
            if (vehicle != null)
            {
                StartCoroutine(movetoX(vehicle.transform, new Vector3(-13, vehicle.transform.position.y, vehicle.transform.position.z)));
                Movement_Track movement = (Movement_Track)vehicle.GetComponent(typeof(Movement_Track));
                movement.moving = true;
            }

        }
        foreach (GameObject pickup in pickups)
        {
            if (pickup != null)
            {
                StartCoroutine(movetoX(pickup.transform, new Vector3(-13, pickup.transform.position.y, pickup.transform.position.z)));
                Movement_Track movement = (Movement_Track)pickup.GetComponent(typeof(Movement_Track));
                movement.moving = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Car_Cont.start)
        {
            foreach (GameObject vehicle in entities)
            {
                if (vehicle != null && vehicle.transform.position.x <= -13)
                {
                    Destroy(vehicle);
                    spawn_timer++;
                }
            }
            foreach (GameObject pickup in pickups)
            {
                if (pickup != null && pickup.transform.position.x <= -13)
                {
                    Destroy(pickup);
                }
            }
            if (spawn_timer == 19) { spawn_timer = 0; }
            if (spawn_timer == 12)
            {
                spawn_timer++;
                Instantiate(pattern1, new Vector3(22, 2.8f, -25.23f), Quaternion.identity);
                Instantiate(pattern2, new Vector3(49.41f, 2.8f, -25.23f), Quaternion.identity);
                Instantiate(pattern3, new Vector3(67.41f, 2.8f, -25.23f), Quaternion.identity);

                entities = GameObject.FindGameObjectsWithTag("Car");
                foreach (GameObject vehicle in entities)
                {
                    Movement_Track movement = (Movement_Track)vehicle.GetComponent(typeof(Movement_Track));
                    if (vehicle != null && movement.moving == false)
                    {
                        movement.moving = true;
                        StartCoroutine(movetoX(vehicle.transform, new Vector3(-13, vehicle.transform.position.y, vehicle.transform.position.z)));
                    }
                }

                pickups = GameObject.FindGameObjectsWithTag("Coin");
                foreach (GameObject pickup in pickups)
                {
                    Movement_Track movement = (Movement_Track)pickup.GetComponent(typeof(Movement_Track));
                    if (pickup != null && movement.moving == false)
                    {
                        movement.moving = true;
                        StartCoroutine(movetoX(pickup.transform, new Vector3(-13, pickup.transform.position.y, pickup.transform.position.z)));
                    }
                }
            }
        }
    }
    IEnumerator movetoX(Transform fromposition, Vector3 toposition)
    {   
            while (fromposition!= null && fromposition.position.x > toposition.x)
            {
            if (player.collided)
            {
                break;
            }
            fromposition.position = new Vector3(fromposition.position.x - 3.2f *Time.deltaTime - player.coins*.002f, fromposition.position.y, fromposition.position.z);
            yield return null;
            }
        yield return null;
    }
}
