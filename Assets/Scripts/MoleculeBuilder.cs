using UnityEngine;


public class MoleculeBuilder : MonoBehaviour
{
    public Rigidbody mainAtom;
    public Rigidbody[] otherAtoms;
    public LineRenderer lineRendererPrefab;
    Rigidbody currentSelected;

    Camera cam;
    void Start()
    {
        cam = Camera.main;

        for (int i = 0; i < otherAtoms.Length; i++)
        {
            otherAtoms[i].linearVelocity = Random.insideUnitCircle * 0.1f;
            otherAtoms[i].linearDamping = 0.5f;
        }

        mainAtom.linearDamping = 1;
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 20))
            {
                SelectAtom(hit.collider, hit.point);
            }

            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnClickUp();
        }

        CheckIfAtomClose();
    }

    void SelectAtom(Collider col, Vector3 hitPos)
    {
        if (!currentSelected)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();

            if (rb == mainAtom)
            {
                currentSelected = rb;
            }
            else
            {
                for (int i = 0; i < otherAtoms.Length; i++)
                {
                    if (rb == otherAtoms[i])
                        currentSelected = rb;
                }
            }
            return;
        }

        if (currentSelected.isKinematic)
        {
            currentSelected = null;
            return;
        }

        hitPos.y = currentSelected.transform.position.y;

        currentSelected.linearVelocity = hitPos - currentSelected.transform.position;
    }

    void OnClickUp()
    {
        currentSelected = null;
    }

    void CheckIfAtomClose()
    {
        bool flag = true;
        for (int i = 0; i < otherAtoms.Length; i++)
        {
            if (!otherAtoms[i].isKinematic)
            {
                flag = false;
                break;
            }
        }

        if (flag)
        {
            Completed();
            return;
        }

        SphereCollider mainCol = mainAtom.GetComponent<SphereCollider>();

        for (int i = 0; i < otherAtoms.Length; i++)
        {
            if (!otherAtoms[i].isKinematic)
            {
                SphereCollider col = otherAtoms[i].GetComponent<SphereCollider>();
                float totalMax = col.radius + mainCol.radius + 0.05f;
                if (Vector3.Distance(col.transform.position, mainCol.transform.position) <= totalMax)
                {
                    col.enabled = false;
                    otherAtoms[i].isKinematic = true;
                    col.transform.parent = mainCol.transform;
                    otherAtoms[i].linearVelocity = Vector3.zero;

                    LineRenderer l = Instantiate(lineRendererPrefab, mainAtom.transform);
                    l.transform.position = mainCol.transform.position;
                    l.transform.rotation = mainCol.transform.rotation;
                    l.positionCount = 2;
                    l.useWorldSpace = false;
                    l.SetPosition(0, Vector3.zero);
                    l.SetPosition(1, col.transform.localPosition);
                }
            }
        }
    }

    void Completed()
    {
        FindAnyObjectByType<TaskUI>().TaskComplete();
        enabled = false;
    }
}
