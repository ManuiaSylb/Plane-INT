using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    private Vector3 InitPos;
    public Vector3 PlaneDirection;
    private float reset_speed = 2f;
    private float x_limit = 0.5f;
    private float y_limit = 0.5f;

    void Start()
    {
        InitPos = transform.localPosition;
        PlaneDirection = Vector3.zero;
    }

    void Update()
    {
        // Appliquer les limites sur la position
        Vector3 LimitedVec = transform.localPosition;

        // Limiter le mouvement sur l'axe X et Y
        LimitedVec.x = Mathf.Clamp(LimitedVec.x, InitPos.x - x_limit, InitPos.x + x_limit);
        LimitedVec.y = Mathf.Clamp(LimitedVec.y, InitPos.y - y_limit, InitPos.y + y_limit);

        // Appliquer la position limitée
        transform.localPosition = LimitedVec;

        // Réinitialiser la position en douceur
        transform.localPosition = Vector3.Lerp(transform.localPosition, InitPos, Time.deltaTime * reset_speed);

        // Calculer la direction par rapport à la position initiale
        PlaneDirection = transform.localPosition - InitPos;
    }
}
