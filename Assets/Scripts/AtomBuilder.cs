using UnityEngine;

public class AtomBuilder : MonoBehaviour
{
    public GameObject protonPrefab;
    public GameObject electronPrefab;
    public GameObject protonInWorld;
    public GameObject electronInWorld;
    Camera cam;
    bool isElectron;
    GameObject selected;
    Atom currentAtom;
    void Start()
    {
        cam = Camera.main;
    }

    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            currentAtom = FindAnyObjectByType<Atom>();

            if (currentAtom == null)
                return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit hit,20))
            {
                SelectElectronOrProton(hit.collider,hit.point);
            }
            HoveringOnAnAtom(hit.point);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnClickUp();
        }
    }

    void SelectElectronOrProton(Collider col,Vector3 pos)
    {
        if (selected)
        {
            pos.y = electronInWorld.transform.position.y;
            selected.transform.position = Vector3.Lerp(selected.transform.position,pos,Time.deltaTime * 7);
            return;
        }

        if(col.gameObject == protonInWorld)
        {
            isElectron = false;
            selected = Instantiate(protonInWorld, col.transform.position,Quaternion.identity);
        }
        else if(col.gameObject == electronInWorld)
        {
            isElectron = true;
            selected = Instantiate(electronPrefab, col.transform.position,Quaternion.identity);
        }
    }


    void HoveringOnAnAtom(Vector3 pos)
    {
        if (selected)
        {
            if (isElectron)
            {
                Atom.ElectronData e = currentAtom.IsInsideAnOrbit(pos);
                if (e != null)
                    selected.transform.position = e.socket.position;
            }
            else
            {
                Atom.NeutronData n = currentAtom.IsNearNucleas(pos);
                if (n != null)
                    selected.transform.position = n.socket.position;
            }
        }
    }


    void OnClickUp()
    {
        if (selected)
        {
            if (isElectron)
            {
                Atom.ElectronData e = currentAtom.IsInsideAnOrbit(selected.transform.position);
                if (e != null)
                {
                    selected.transform.position = e.socket.position;
                    currentAtom.AddElectron(e, selected);
                    selected = null;
                }
            }
            else
            {
                Atom.NeutronData n = currentAtom.IsNearNucleas(selected.transform.position);
                if (n != null)
                {
                    selected.transform.position = n.socket.position;
                    currentAtom.AddProton(n, selected);
                    selected = null;
                }
            }

            if (selected)
                Destroy(selected);
        }
    }
}
