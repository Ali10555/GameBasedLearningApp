using UnityEngine;

public class Atom : MonoBehaviour
{
    public NeutronData[] neutronsSockets;
    public ElectronData[] electronsSockets;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public ElectronData IsInsideAnOrbit(Vector3 pos)
    {
        for (int i = 0; i < electronsSockets.Length; i++)
        {
            if (electronsSockets[i].IsInsideOrbit(transform.position, pos)) 
                return electronsSockets[i];
        }
        return null;
    }

    [System.Serializable]
    public class NeutronData
    {
        public Transform socket;
    }

    [System.Serializable]
    public class ElectronData
    {
        public Transform socket;

        public bool IsInsideOrbit(Vector3 centerObject,Vector3 targetPosition)
        {
            // Project target position onto the XZ plane relative to the center object
            Vector3 flatCenter = new Vector3(centerObject.x, 0f, centerObject.z);
            Vector3 flatTarget = new Vector3(targetPosition.x, 0f, targetPosition.z);

            // Calculate the horizontal distance (ignoring height)
            float distance = Vector3.Distance(flatCenter, flatTarget);

            float dis = Vector3.Distance(socket.position, centerObject);
            float innerRadius = dis - 0.1f;
            float outerRadius = dis + 0.1f;

            // Check if the distance is within the donut's radii
            return distance >= innerRadius && distance <= outerRadius;
        }

    }
}
