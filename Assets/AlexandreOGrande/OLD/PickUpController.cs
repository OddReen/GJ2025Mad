using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    StarterAssetsInputs inputHandler;

    [SerializeField] bool hasPickedUp;
    [SerializeField] Transform pickUpTransform;
    [SerializeField] float distanceToPickUp;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] GameObject pickedUpObject;
    [SerializeField] Material invisible;

    public string currentObjectHolding = "";

    private void Awake()
    {
        inputHandler = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        StartCoroutine(IsPickingUp());

        if (hasPickedUp)
        {
            pickedUpObject.transform.position = pickUpTransform.position;
        }

        if (Input.GetKeyDown(KeyCode.E) && !hasPickedUp)
        {
            PickUp();
        }
        else if (Input.GetKeyDown(KeyCode.E) && hasPickedUp)
        {
            Drop();
        }
    }
    private void PickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distanceToPickUp, ~playerLayer))
        {
            if (hit.collider.CompareTag("Pickable"))
            {
                pickedUpObject = hit.collider.gameObject;
                Rigidbody rb = pickedUpObject.GetComponent<Rigidbody>();

                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeRotation;

                pickedUpObject.transform.SetParent(pickUpTransform);

                pickedUpObject.transform.localPosition = Vector3.zero;
                pickedUpObject.transform.localRotation = Quaternion.identity;

                currentObjectHolding = pickedUpObject.GetComponent<PickableObjectsID>().objectId;

                pickedUpObject.GetComponent<Collider>().enabled = false;
                hasPickedUp = true;
            }
        }
    }

    private void Drop()
    {
        //if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distanceToPickUp, ~playerLayer))
        //{
        //    if (hit.collider.CompareTag("Pillar"))
        //    {
        //        if (hit.collider.transform.childCount == 0)
        //        {
        //            //hit.collider.GetComponent<CosmosPillar>().PutObjectOnTop(pickedUpObject);

        //            pickedUpObject.GetComponent<Collider>().enabled = true;
        //            hasPickedUp = false;
        //            StopCoroutine(coroutine);
        //            Destroy(ghostObject);
        //            pickedUpObject = null;
        //        }
        //        return;
        //    }
        //    pickedUpObject.transform.parent = null;
        //    pickedUpObject.transform.position = hit.point;
        //    pickedUpObject.GetComponent<Collider>().enabled = true;
        //    pickedUpObject.transform.rotation = Quaternion.Euler(-90, 0, 0); // MAD CODE HERE!!!!!!!!!
        //    hasPickedUp = false;
        //    StopCoroutine(coroutine);
        //    Destroy(ghostObject);
        //    pickedUpObject = null;
        //}

        pickedUpObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        pickedUpObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        pickedUpObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        pickedUpObject.GetComponent<Rigidbody>().useGravity = true;
        pickedUpObject.transform.SetParent(null);
        pickedUpObject.transform.position = pickUpTransform.position;
        pickedUpObject.transform.rotation = Quaternion.Euler(-90, pickUpTransform.rotation.y, pickUpTransform.rotation.z);

        pickedUpObject.GetComponent<Collider>().enabled = true;
        currentObjectHolding = "";
        hasPickedUp = false;

    }
    private IEnumerator IsPickingUp()
    {
        yield return new WaitForEndOfFrame();
        //inputHandler.isPickingUp = false;
    }
}
