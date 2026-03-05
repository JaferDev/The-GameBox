using UnityEngine;
using System.IO.Ports; //For SerialPort
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI;

public class Arduino : MonoBehaviour
{
    readonly SerialPort serial = new("COM7", 9600); //Create new instacne of SerialPort
    private List<float> lastPositions; //List of recent positions
    readonly private int noOfPositions = 50 ; //Number of positions contained in lastPositions
    private bool canShoot;

    //References to unity GameObjects and shooting
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bulletPrefabs;
    [SerializeField] private float reloadTime = 1.5f;
    [SerializeField] private float reloadTimer = 0;
    [SerializeField] GameObject enemyDeathEffect;
    [SerializeField] Slider reloadSlider;

    private void Start()
    {
        //Plays when scene is loaded

        FindFirstObjectByType<AudioManager>().Play("MainOST");
        
        //Keeps an initial noOfPositions values for lastPositions
        lastPositions = new List<float>(); 
        for (int i = 0; i < noOfPositions; i++)
        {
            lastPositions.Add(0);
        }

        //Open serial monitor and keep readtimeout as 15 milliseconds
        serial.Open();
        serial.ReadTimeout = 15;
    }

    private void Update()
    {
        if (!serial.IsOpen) return; //No operations to do when serial port is closed

        //Run every frame (15 milliseconds approx)
        reloadTimer += Time.deltaTime;
        if (reloadTimer < reloadSlider.maxValue) reloadSlider.value = reloadTimer;
        if (reloadTimer < reloadTime) canShoot = false;
        else canShoot = true;

        ReadSerial();
    }

    private void Sliding(float distanceData)
    {
        /*
            Changes position of player to position of stabilized slider input
        */
        float stablePosition = StabilizePosition(distanceData);
        transform.position = new Vector3(stablePosition, -2.1f, 0);
    }

    private float StabilizePosition(float distance)
    {
        /*
            Stablizes position by taking an average of the last noOfPositions positions
            Returns: Average of last noOfPositions positions (float)
        */
        lastPositions.Add(distance);
        lastPositions.Remove(lastPositions[0]);
        return lastPositions.Sum() / noOfPositions;
    }

    private void ReadSerial()
    {
        /*
            Reads from serial monitor and calls the Sliding and Shoot function depending on the line being read
        */
        try
        {
            string value = serial.ReadLine(); //Read the information
            if (value.Contains("Slide: "))
            {
                float distanceData = float.Parse(value[7..]); //Parse the substring from 7th index onwards
                Sliding(distanceData);
            }
            if (value.Contains("Shot: "))
            {
                int bulletIndex = int.Parse(value[6..]); //Parse the substring from 6th index onwards
                Shoot(bulletIndex);
            }
        }
        catch (TimeoutException) {} //In case of TimeOutException (very low chance)
        catch (Exception) {}
    }

    private void Shoot(int bulletIndex)
    {
        /*
            Takes int bulletIndex and shoots the bulletIndex-th bullet from bulletPrefabs
        */

        if (!canShoot) return;

        reloadTimer = 0;
        GameObject bullet = Instantiate(bulletPrefabs[bulletIndex], firePoint.position, firePoint.rotation);
        Bullet bulletComp = bullet.GetComponent<Bullet>();
        bulletComp.deathEffect = enemyDeathEffect;
    }
}