using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour 
{

    [SerializeField] GameObject asteroidPrefab;


    /// <summary>
    /// Awake is called before Start
    /// </summary>
	void Awake()
    {
        // initialize screen utils
        ScreenUtils.Initialize();





        int count = 0;
        while (count < 4)
        {
            Instantiate(asteroidPrefab, Vector3.zero, Quaternion.identity);

            count++;
        }


    }
}
