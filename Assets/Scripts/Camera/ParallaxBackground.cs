using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float length;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved=cam.transform.position.x*(1-parallaxEffect);
        float distanveToMove=cam.transform.position.x*parallaxEffect;

        transform.position = new Vector3(xPosition + distanveToMove, transform.position.y);

        if (distanceMoved > xPosition + length)
        {
            xPosition += length;
        }
        else if (distanceMoved < xPosition - length)
        {
            xPosition -= length;
        }
    }
}
