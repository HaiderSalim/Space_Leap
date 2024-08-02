using UnityEngine;

public class Rotatator : MonoBehaviour {
    [SerializeField] Vector3 rotation;
    [SerializeField] Transform meshObject;
    public float rotationSpeed = 0;
    [SerializeField] bool randomize;
    [HideInInspector] public float OG_rotationSpeed = 0;
    
    public bool Randomize 
    
    {
        get {
            return randomize;
        }
    }
    
    [SerializeField] float maxSpeed;
    [SerializeField] float minSpeed;

    // Use this for initialization
    void Start () 
    {
        meshObject = transform;
        OG_rotationSpeed = rotationSpeed;

        if(meshObject == null) 
        
        {
            meshObject = transform.Find("planet"); 
            if (meshObject == null)
                meshObject = transform.Find("w2"); 
        }
        
        
        if(randomize) 
        
        {
            rotation = new Vector3(RandFloat(), RandFloat(), RandFloat());
            rotationSpeed = Random.Range(minSpeed,maxSpeed);
        }
    }
    
    float RandFloat() 
    
    {
        return Random.Range(0f,1.01f);
    }
    
    // Update is called once per frame
    void FixedUpdate() 
    
    {
        if(meshObject != null)
            meshObject.Rotate(rotation, rotationSpeed * Time.deltaTime);
    }
}
