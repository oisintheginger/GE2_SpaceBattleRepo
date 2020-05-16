using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : SteerBehaviour
{
    [Header("Depth and Scale")]
    public float FrontDetectorDistance, SideDetectorDepth, DetectorScaler, DetectorAngle;

    [Header("Detector Updates/Second")]
    public float FrontDetectorUpdate, SideDetectorUpdate;

    public ForceType myForceType = ForceType.Normal;

    [Header("Layermask")]
    public LayerMask lMask = -1;

    public struct DetectRayHitInfo
    {
        public Vector3 detectPoint;
        public Vector3 detectNormal;
        public bool hit;
        public DetectorType detectorType;

        public enum DetectorType
        {
            Forward ,
            Side
        };

        public DetectRayHitInfo(Vector3 detectPoint, Vector3 detectNormal, bool hit, DetectorType detectorType)
        {
            this.detectPoint = detectPoint;
            this.detectNormal = detectNormal;
            this.hit = hit;
            this.detectorType = detectorType;
        }
    }

    public enum ForceType
    {
        Normal,
        Incident,
        UpAxis,
        Brake
    };

    DetectRayHitInfo[] DrHInfo = new DetectRayHitInfo[5];


    public override Vector3 CalculateForce()
    {
        force = Vector3.zero;
        for(int i =0; i< DrHInfo.Length;i++)
        {
            
            if(DrHInfo[i].hit)
            {

                force += CalculateObstacleAvoidForce(DrHInfo[i]);
            }
        }
        return force;
    }

    private void Update()
    {
        UpdateDetection(0, Quaternion.identity, this.FrontDetectorDistance, DetectRayHitInfo.DetectorType.Forward);

        UpdateDetection(1, Quaternion.AngleAxis(DetectorAngle, Vector3.up), SideDetectorDepth, DetectRayHitInfo.DetectorType.Side);
        UpdateDetection(2, Quaternion.AngleAxis(-DetectorAngle, Vector3.up), SideDetectorDepth, DetectRayHitInfo.DetectorType.Side);
        UpdateDetection(3, Quaternion.AngleAxis(DetectorAngle, Vector3.right), SideDetectorDepth, DetectRayHitInfo.DetectorType.Side);
        UpdateDetection(4, Quaternion.AngleAxis(-DetectorAngle, Vector3.right), SideDetectorDepth, DetectRayHitInfo.DetectorType.Side);


    }
    System.Collections.IEnumerator UpdateFrontFeelers()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 0.5f));
        while (true)
        {
            UpdateDetection(0, Quaternion.identity, this.FrontDetectorDistance, DetectRayHitInfo.DetectorType.Forward);
            
            yield return new WaitForSeconds(1.0f / FrontDetectorUpdate);
        }
    }

    System.Collections.IEnumerator UpdateSideFeelers()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 0.5f));
        float angle = 70;
        while (true)
        {
            // Left feeler
            UpdateDetection(1, Quaternion.AngleAxis(angle, Vector3.up), SideDetectorDepth, DetectRayHitInfo.DetectorType.Side);
            // Right feeler
            UpdateDetection(2, Quaternion.AngleAxis(-angle, Vector3.up), SideDetectorDepth, DetectRayHitInfo.DetectorType.Side);
            // Up feeler
            UpdateDetection(3, Quaternion.AngleAxis(angle, Vector3.right), SideDetectorDepth, DetectRayHitInfo.DetectorType.Side);
            // Down feeler
            UpdateDetection(4, Quaternion.AngleAxis(-angle, Vector3.right), SideDetectorDepth, DetectRayHitInfo.DetectorType.Side);

            yield return new WaitForSeconds(1.0f / SideDetectorUpdate);
        }
    }

    void UpdateDetection(int DetectIndex, Quaternion localRotation, float baseDepth, DetectRayHitInfo.DetectorType detectType)
    {
        Vector3 raydirection = localRotation * transform.rotation * Vector3.forward;
        float depth = baseDepth + ((boid.velocity.magnitude / boid.maxSpeed) * baseDepth);
        RaycastHit detectInfo;
        bool collided = Physics.Raycast(transform.position, raydirection, out detectInfo, depth, lMask.value);
        Vector3 feelerEnd = collided ? detectInfo.point : (transform.position + raydirection * depth);
        DrHInfo[DetectIndex] = new  DetectRayHitInfo(feelerEnd, detectInfo.normal, collided, detectType);
    }


    public Vector3 CalculateObstacleAvoidForce(DetectRayHitInfo DetectorInfo)
    {
        Vector3 force = Vector3.zero;

        Vector3 fromTarget = transform.position - DetectorInfo.detectPoint;
        float dist = Vector3.Distance(transform.position, DetectorInfo.detectPoint);

        switch (myForceType)
        {
            case ForceType.Normal:
                force = DetectorInfo.detectNormal * (FrontDetectorDistance * DetectorScaler / dist);
                break;
            case ForceType.Incident:
                fromTarget.Normalize();
                force -= Vector3.Reflect(fromTarget, DetectorInfo.detectNormal) * (FrontDetectorDistance / dist);
                break;
            case ForceType.UpAxis:
                force += Vector3.up * (FrontDetectorDistance * DetectorScaler / dist);
                break;
            case ForceType.Brake:
                force += fromTarget * (FrontDetectorDistance / dist);
                break;
        }
        return force;
    }



    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled)
        {
            foreach (DetectRayHitInfo DetectorRay in DrHInfo)
            {
                Gizmos.color = Color.gray;
                
                Gizmos.DrawLine(transform.position, DetectorRay.detectPoint);
                
                if (DetectorRay.hit)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(DetectorRay.detectPoint, DetectorRay.detectPoint + (DetectorRay.detectNormal * 5));
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(DetectorRay.detectPoint, DetectorRay.detectPoint + force);
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(this.transform.position, this.transform.position + force);
                }
            }
        }
    }
}
