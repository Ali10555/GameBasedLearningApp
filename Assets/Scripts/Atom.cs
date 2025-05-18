using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using TMPro;

public class Atom : MonoBehaviour
{
    public int maxProton = 1;
    public int maxElectron = 1;
    public NeutronData[] neutronsSockets;
    public ElectronData[] electronsSockets;
    public UnityEvent OnCompletetionEvent;
    TextMeshPro nucleasText;
    void Start()
    {
        nucleasText = GetComponentInChildren<TextMeshPro>();

        if (nucleasText)
            nucleasText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public ElectronData IsInsideAnOrbit(Vector3 pos)
    {
        ElectronData nextEmpty = null;
        for (int i = 0; i < electronsSockets.Length; i++)
        {
            if (electronsSockets[i].socket.childCount == 0)
            {
                nextEmpty = electronsSockets[i];
                break;
            }
        }

        if (nextEmpty != null && nextEmpty.IsInsideOrbit(transform.position, pos))
        {
            return nextEmpty;
        }

        return null;
    }

    public int HowManyElectrons()
    {
        int total = 0;

        for (int i = 0; i < electronsSockets.Length; i++)
        {
            if (electronsSockets[i].socket.childCount > 0)
                total++;
        }

        return total;
    }

    public void AddElectron(ElectronData e,GameObject g)
    {
        g.transform.parent = e.socket;
        StartCoroutine(OnComplete());
    }

    public void AddProton(NeutronData n, GameObject g)
    {
        g.transform.parent = n.socket;
        if (nucleasText)
            nucleasText.text = HowManyProton().ToString();
        StartCoroutine(OnComplete());
    }

    public NeutronData IsNearNucleas(Vector3 pos)
    {
        for (int i = 0; i < neutronsSockets.Length; i++)
        {
            float dis = Vector3.Distance(pos, neutronsSockets[i].socket.position);

            if (dis < 0.2f && neutronsSockets[i].socket.childCount == 0)
                return neutronsSockets[i];
        }

        return null;
    }

    public int HowManyProton()
    {
        int total = 0;

        for (int i = 0; i < neutronsSockets.Length; i++)
        {
            if (neutronsSockets[i].socket.childCount > 0)
                total++;
        }

        return total;
    }

    IEnumerator OnComplete()
    {
        if (HowManyElectrons() < maxElectron || HowManyProton() < maxProton)
            yield break;

        FindAnyObjectByType<TaskUI>().TaskComplete();

        yield return new WaitForSeconds(3);

        OnCompletetionEvent?.Invoke();
        gameObject.SetActive(false);
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
