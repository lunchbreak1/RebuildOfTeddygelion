using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PosterManager : MonoBehaviour
{
    private int maxPosters = 10;
    private TextMeshProUGUI TextMeshProUGUI;
    private WheelchairController wheelchairController;

    // Start is called before the first frame update
    void Start()
    {
        maxPosters = FindObjectsOfType<Poster>().Length;
        TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        wheelchairController = FindObjectOfType<WheelchairController>();

        if(maxPosters == 0)
        {
            Debug.LogWarning("Can't find posters");
        }

        if (wheelchairController == null)
        {
            Debug.LogWarning("Can't find wheelchair");
        }

        if (TextMeshProUGUI == null)
        {
            Debug.LogWarning("Can't find textmesh");
        }
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI.text = (wheelchairController.posters > 0) ? "Posters: " + wheelchairController.posters + " / " + maxPosters : ""; 
    }
}
