using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Food : MonoBehaviour
{
    [Header("Available States")]
    [SerializeField]
    protected bool Cookable = true;
    [SerializeField]
    protected bool Freezable = true;
    [SerializeField]
    protected bool Cuttable = true;

    [Space(5)]

    [Header("Cooking State Values")]

    [SerializeField]
    protected bool isCooking = false;
    protected bool isBurnt = false;
    protected bool isCooked = false;

    [SerializeField]
    protected bool onPan;

    [SerializeField]
    [Range(0f, 1f)]
    protected float temperatureValue = 0.0f;

    [SerializeField]
    [Range(0f, 1f)]
    protected float cookingValue = 0.0f;

    [Header("Seasoning State Values")]

    public bool usesButter = true;

    protected float xScale;
    protected float yScale;
    protected float zScale;
    protected Collider oth;

    [SerializeField]
    [Range(0f, 1f)]
    protected float butterValue = 0f;

    [Header("Cleanliness State Values")]

    [SerializeField]

    protected bool hasTouchedFloor = false;

    [SerializeField]
    [Range(0f, 1f)]
    protected float cleanlinessValue = 0.0f;
    
    [Space(10)]

    [Header("Combine Food Items States")]

    [SerializeField]
    protected bool onAssembler = false;
    [SerializeField]
    protected bool canCombine = false;
    [SerializeField]
    protected bool isCombined;
    [SerializeField]
    protected bool isParent = false;
    [SerializeField]
    protected bool canBeParent = true;
    [SerializeField]
    protected bool hasParent = false;

    [Space(10)]

    [Header("Cutting")]

    public Material capMaterial;

    protected void WakeUp()
    {
        Rigidbody rB = GetComponent<Rigidbody>();
        if (rB != null)
            rB.WakeUp();
    }

    protected virtual void Awake()
    {
        if (canCombine)
        {
            BoxCollider bc = gameObject.GetComponent<BoxCollider>();
            if (bc != null)
            {
                MeshCollider mc = gameObject.GetComponent<MeshCollider>();
                if (mc)
                {
                    if (bc.isTrigger == false)
                        bc.isTrigger = true;
                }
                else
                {
                    BoxCollider b = gameObject.AddComponent<BoxCollider>();
                    b.isTrigger = true;
                }

            }
            else
            {
                BoxCollider b = gameObject.AddComponent<BoxCollider>();
                b.isTrigger = true;
            }
        }
    }

    protected virtual void LateUpdate()
    {
        CookingUpdate();
    }

    protected virtual void CookingUpdate()
    {

    }

    public virtual void OnTriggerEnter(Collider other)
    {
        Cookware temp = other.GetComponent<Cookware>();
        if (temp != null)
        {
            onPan = true;
            oth = other;
            if (temp.GetHeatValue() > 0 || temperatureValue > 0)
            {
                isCooking = true;
            }
        }

        if (onAssembler && !isCombined)
        {
            Food f = other.GetComponent<Food>();
            FixedJoint fixedJoint = other.GetComponent<FixedJoint>();
            FixedJoint fixedJoint1 = GetComponent<FixedJoint>();
            // colliding object is food
            if (f != null && fixedJoint == null && fixedJoint1 == null)
            {
                //items can be combined
                if (f.canCombine && canCombine)
                {
                    //Debug.Log("1" + f.name + " " + f.canCombine + " - " + gameObject.name + " " + canCombine);
                    //collinding object is not a parent already
                    if (!f.isParent)
                    {
                        //Debug.Log("1-1" + f.name + " " + f.isParent);
                        //other can be parent and gameobject is not parent
                        if (f.canBeParent && !isParent)
                        {
                            if (f.transform.parent != transform)
                            {
                                //Debug.Log("1-1-1" + f.name + " " + f.canBeParent + " - " + gameObject.name + " " + isParent);
                                transform.parent = f.transform;
                                FixedJoint fj = f.gameObject.AddComponent<FixedJoint>();
                                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                                fj.connectedBody = rb;
                                fj.enableCollision = false;
                                fj.breakForce = 900f;

                                f.isParent = true;
                                f.hasParent = true;
                                canBeParent = false;

                                Destroy(gameObject.GetComponent<Throwable>());
                                Destroy(gameObject.GetComponent<Interactable>());
                                Destroy(gameObject.GetComponent<VelocityEstimator>());
                            }
                        }
                        //gameobject is already parent or f cannot be parent
                        else
                        {
                            if (f.hasParent && !isParent)
                            {
                                //Debug.Log("1-2" + f.name + " " + f.isParent);
                                transform.parent = f.transform;
                                FixedJoint fj = f.gameObject.AddComponent<FixedJoint>();
                                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                                fj.connectedBody = rb;
                                fj.enableCollision = false;
                                fj.breakForce = 900f;

                                canBeParent = false;
                                hasParent = true;

                                Destroy(f.GetComponent<Throwable>());
                                Destroy(f.GetComponent<Interactable>());
                                Destroy(f.GetComponent<VelocityEstimator>());
                            }
                            else 
                            {
                                //Debug.Log("1-3" + f.name + " " + f.isParent);
                                f.transform.parent = transform;
                                FixedJoint fj = gameObject.AddComponent<FixedJoint>();
                                Rigidbody rb = f.gameObject.GetComponent<Rigidbody>();
                                fj.connectedBody = rb;
                                fj.enableCollision = false;
                                fj.breakForce = 900f;

                                f.canBeParent = false;
                                f.hasParent = true;

                                Destroy(f.GetComponent<Throwable>());
                                Destroy(f.GetComponent<Interactable>());
                                Destroy(f.GetComponent<VelocityEstimator>());
                            }
                        }
                    }
                    // colliding object is already a parent
                    else
                    {
                        //Debug.Log("2" + f.name + " " + f.canCombine + " - " + gameObject.name + " " + canCombine);
                        //gameobject is not already parent
                        if (!isParent)
                        {
                            //Debug.Log("2-1" + f.name + " " + f.isParent);
                            transform.parent = f.transform;
                            FixedJoint fj = f.gameObject.AddComponent<FixedJoint>();
                            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                            fj.connectedBody = rb;
                            fj.enableCollision = false;
                            fj.breakForce = 900f;

                            f.isParent = true;
                            gameObject.GetComponent<Food>().hasParent = true;

                            Destroy(gameObject.GetComponent<Throwable>());
                            Destroy(gameObject.GetComponent<Interactable>());
                            Destroy(gameObject.GetComponent<VelocityEstimator>());
                        }
                    }
                }
            }
        }
    }

    private void OnJointBreak(float breakForce)
    {
        Destroy(GetComponent<FixedJoint>());
        if (transform.parent != null)
            transform.parent = null;
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pot") || other.CompareTag("Pan"))
        {
            WakeUp();
            if (usesButter)
            {
                if (temperatureValue > 0 && butterValue > 0)
                {
                    cookingValue += temperatureValue * .0002f;
                    butterValue -= temperatureValue * .00006f;
                }
            }
            else
            {
                if (temperatureValue > 0)
                {
                    cookingValue += temperatureValue * .0002f;
                }
            }
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Cookware>() != null)
        {
            isCooking = false;
            onPan = false;
        }
    }

    public float GetCleanlinessValue()
    {
        return cleanlinessValue;
    }

    public void SetCleanlinessValue(float v)
    {
        cleanlinessValue = v;
    }

    public float GetTemperatureValue()
    {
        return temperatureValue;
    }

    public void SetTemperatureValue(float v)
    {
        temperatureValue = v;
    }

    public float GetButterValue()
    {
        return butterValue;
    }

    public void SetButterValue(float v)
    {
        butterValue = v;
    }

    public float GetCookingValue()
    {
        return cookingValue;
    }

    public void SetCookingValue(float f)
    {
        cookingValue = f;
    }

    public void SetHasTouchedFloor(bool b)
    {
        hasTouchedFloor = b;
    }

    public void SetOnAssembler(bool b)
    {
        onAssembler = b;
    }

    public bool GetOnAssembler()
    {
        return onAssembler;
    }

    public bool GetCanCombine()
    {
        return canCombine;
    }

    public void SetCanCombine(bool b)
    {
        canCombine = b;
    }

    public bool GetIsCombined()
    {
        return isCombined;
    }

    public void SetIsCombined(bool b)
    {
        isCombined = b;
    }

    public bool GetIsParent()
    {
        return isParent;
    }

    public void SetIsParent(bool b)
    {
        isParent = b;
    }

    public bool GetCanBeParent()
    {
        return canBeParent;
    }

    public void SetCanBeParent(bool b)
    {
        canBeParent = b;
    }

    public bool GetHasParent()
    {
        return hasParent;
    }

    public void SetHasParent(bool b)
    {
        hasParent = b;
    }

    public bool GetCookableState()
    {
        return Cookable;
    }

    public void SetCookableState(bool b)
    {
         Cookable = b;
    }

    public void SetFreezableState(bool b)
    {
        Freezable = b;
    }

    public bool GetFreezableState()
    {
        return Freezable;
    }

    public void SetCuttableState(bool b)
    {
        Cuttable = b;
    }

    public bool GetCuttableState()
    {
        return Cuttable;
    }

    public void SetisCookingState(bool b)
    {
        isCooking = b;
    }

    public bool GetisCookingState()
    {
        return isCooking;
    }

    public void SetonPanState(bool b)
    {
        onPan = b;
    }

    public bool GetOnPanState()
    {
        return onPan;
    }

    public void SetUsesButterState(bool b)
    {
        usesButter = b;
    }

    public bool GetUsesButterState()
    {
        return usesButter;
    }

    public bool GetHasTouchedFloor()
    {
        return hasTouchedFloor;
    }


}