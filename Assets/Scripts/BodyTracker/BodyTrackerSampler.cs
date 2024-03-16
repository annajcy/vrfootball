using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.Serialization;

public class BodyTrackerSampler : MonoBehaviour
{
    public float soleHeight = 0.022f;
    public List<Transform> bonesList = new List<Transform>(new Transform[(int)BodyTrackerRole.ROLE_NUM]);
    public float[] skeletonLens = new float[11];

    private Dictionary<int, Quaternion> rotationDict = new Dictionary<int, Quaternion>();
    private BodyTrackerResult bodyTrackerResult;
    private double displayTime;
    private Vector3 hipJointPosition;
    private Quaternion jointRotation;
    private BodyTrackerJoint[] joints;

    [HideInInspector] public int leftTouchGroundAction;
    [HideInInspector] public int rightTouchGroundAction;
    [HideInInspector] public int leftToeTouchGroundAction;
    [HideInInspector] public int rightToeTouchGroundAction;

    private void Awake()
    {
        InitBodyTrackingResult();
        InitJoints();
        Update();
    }

    void Update()
    {
        if (UpdateBodyTrackingResult() != 0) return;
        UpdateBonesTransform();
        UpdateFeetActions();
    }

    private void InitBodyTrackingResult()
    {
        bodyTrackerResult = new BodyTrackerResult
        {
            trackingdata = new BodyTrackerTransform[24]
        };
    }

    private void InitJoints()
    {
        joints = new BodyTrackerJoint[bonesList.Count];
        for (int i = 0; i < bonesList.Count; i++)
        {
            if (bonesList[i] == null) continue;
            rotationDict.Add(i, bonesList[i].rotation);
            var bodyTrackerJoint = bonesList[i].GetComponent<BodyTrackerJoint>();
            if (bodyTrackerJoint == null)
            {
                bodyTrackerJoint = bonesList[i].gameObject.AddComponent<BodyTrackerJoint>();
                bodyTrackerJoint.bodyTrackerRole = (BodyTrackerRole)i;
            }
            joints[i] = bodyTrackerJoint;
        }
    }

    private int UpdateBodyTrackingResult()
    {
        displayTime = PXR_System.GetPredictedDisplayTime();
        var state = PXR_Input.GetBodyTrackingPose(displayTime, ref bodyTrackerResult);
        return state;
    }

    private void UpdateBonesTransform()
    {
        hipJointPosition = GetPosition(bodyTrackerResult.trackingdata[0]);
        bonesList[0].localPosition = hipJointPosition;
        for (int i = 0; i < bonesList.Count; i++)
        {
            if (bonesList[i] == null) continue;
            jointRotation = GetQuaternion(bodyTrackerResult.trackingdata[i]);
            bonesList[i].rotation = jointRotation * rotationDict[i];
            joints[i].trackingData = bodyTrackerResult.trackingdata[i];
        }
    }

    private void UpdateFeetActions()
    {
        leftTouchGroundAction = (int)bodyTrackerResult.trackingdata[7].Action;
        rightTouchGroundAction = (int)bodyTrackerResult.trackingdata[8].Action;
        leftToeTouchGroundAction = (int)bodyTrackerResult.trackingdata[10].Action;
        rightToeTouchGroundAction = (int)bodyTrackerResult.trackingdata[11].Action;
    }


    public void UpdateBonesLength(float scale = 1)
    {
        bonesList[0].localScale = Vector3.one * scale;
        skeletonLens[0] = 0.2f * scale;
        skeletonLens[1] = 0.169f * scale;
        FindBonesLength();
        SetBonesLength();
        Update();
    }

    public void SetBonesLength()
    {
        BodyTrackingBoneLength boneLength = new BodyTrackingBoneLength
        {
            headLen = 100 * skeletonLens[0],
            neckLen = 100 * skeletonLens[1], //6.1f;
            torsoLen = 100 * skeletonLens[2], //37.1f;
            hipLen = 100 * skeletonLens[3], //9.1f;
            upperLegLen = 100 * skeletonLens[4], //34.1f;
            lowerLegLen = 100 * skeletonLens[5], //40.1f;
            footLen = 100 * skeletonLens[6], //14.1f;
            shoulderLen = 100 * skeletonLens[7], //27.1f;
            upperArmLen = 100 * skeletonLens[8], //20.1f;
            lowerArmLen = 100 * skeletonLens[9], //22.1f;
            handLen = 100 * skeletonLens[10]
        };

        int result = PXR_Input.SetBodyTrackingBoneLength(boneLength);

        Debug.Log($"BodyTrackerSampler.SetBonesLength: boneLength = {boneLength}, result = {result}");
    }

    private static Vector3 GetPosition(BodyTrackerTransform bodyTrackerTransform)
    {
        return new(
            (float)bodyTrackerTransform.localpose.PosX,
            (float)bodyTrackerTransform.localpose.PosY,
            (float)bodyTrackerTransform.localpose.PosZ);
    }

    private static Quaternion GetQuaternion(BodyTrackerTransform bodyTrackerTransform)
    {
        return new(
            (float)bodyTrackerTransform.localpose.RotQx,
            (float)bodyTrackerTransform.localpose.RotQy,
            (float)bodyTrackerTransform.localpose.RotQz,
            (float)bodyTrackerTransform.localpose.RotQw);
    }

    [ContextMenu("AutoBindAvatarBones")]
    public void FindBonesReference()
    {
        bonesList[0] = GameObject.Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips").transform;
        bonesList[1] = GameObject.Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/UpLeg_L").transform;
        bonesList[2] = GameObject.Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/UpLeg_R").transform;
        bonesList[3] = GameObject.Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/Spine1").transform;
        bonesList[4] = GameObject.Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/UpLeg_L/Leg_L")
            .transform;
        bonesList[5] = GameObject.Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/UpLeg_R/Leg_R")
            .transform;
        bonesList[6] = GameObject.Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/Spine1/Spine2")
            .transform;
        bonesList[7] = GameObject.Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/UpLeg_L/Leg_L/Foot_L")
            .transform;
        bonesList[8] = GameObject.Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/UpLeg_R/Leg_R/Foot_R")
            .transform;
        bonesList[9] = GameObject.Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/Spine1/Spine2/Chest")
            .transform;
        bonesList[10] = GameObject
            .Find(this.name +
                  "/RIG/DeformationSystem/Root/GlobalScale/Hips/UpLeg_L/Leg_L/Foot_L/Foot_rotate_L/HeelToes_L/Toes_rotate_L/Toes_L")
            .transform;
        bonesList[11] = GameObject
            .Find(this.name +
                  "/RIG/DeformationSystem/Root/GlobalScale/Hips/UpLeg_R/Leg_R/Foot_R/Foot_rotate_R/HeelToes_R/Toes_rotate_R/Toes_R")
            .transform;
        bonesList[12] = GameObject
            .Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/Spine1/Spine2/Chest/Neck").transform;
        bonesList[13] = GameObject
            .Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/Spine1/Spine2/Chest/Shoulder_L").transform;
        bonesList[14] = GameObject
            .Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/Spine1/Spine2/Chest/Shoulder_R").transform;
        bonesList[15] = GameObject
            .Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/Spine1/Spine2/Chest/Neck/Head").transform;
        bonesList[16] = GameObject
            .Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/Spine1/Spine2/Chest/Shoulder_L/Arm_L")
            .transform;
        bonesList[17] = GameObject
            .Find(this.name + "/RIG/DeformationSystem/Root/GlobalScale/Hips/Spine1/Spine2/Chest/Shoulder_R/Arm_R")
            .transform;
        bonesList[18] = GameObject
            .Find(this.name +
                  "/RIG/DeformationSystem/Root/GlobalScale/Hips/Spine1/Spine2/Chest/Shoulder_L/Arm_L/Arm_L_twist/ForeArm_L")
            .transform;
        bonesList[19] = GameObject
            .Find(this.name +
                  "/RIG/DeformationSystem/Root/GlobalScale/Hips/Spine1/Spine2/Chest/Shoulder_R/Arm_R/Arm_R_twist/ForeArm_R")
            .transform;
        bonesList[20] = GameObject
            .Find(this.name +
                  "/RIG/DeformationSystem/Root/GlobalScale/Hips/Spine1/Spine2/Chest/Shoulder_L/Arm_L/Arm_L_twist/ForeArm_L/ForeArm_L_twist01/ForeArm_L_twist02/Hand_L")
            .transform;
        bonesList[21] = GameObject
            .Find(this.name +
                  "/RIG/DeformationSystem/Root/GlobalScale/Hips/Spine1/Spine2/Chest/Shoulder_R/Arm_R/Arm_R_twist/ForeArm_R/ForeArm_R_twist01/ForeArm_R_twist02/Hand_R")
            .transform;
    }


    [ContextMenu("AutoFindAvatarBonesLength")]
    public void FindBonesLength()
    {
        skeletonLens[0] = 0.2f; //HeadLen
        skeletonLens[1] = (bonesList[12].position - bonesList[15].position).magnitude; //NeckLen
        skeletonLens[2] =
            (bonesList[12].position - (bonesList[0].position + bonesList[3].position) * 0.5f).magnitude; //TorsoLen
        skeletonLens[3] = ((bonesList[0].position + bonesList[3].position) * 0.5f -
                           (bonesList[1].position + bonesList[2].position) * 0.5f).magnitude; //HipLen
        skeletonLens[4] = (bonesList[1].position - bonesList[4].position).magnitude; //UpperLegLen
        skeletonLens[5] = (bonesList[4].position - bonesList[7].position).magnitude; //LowerLegLen
        skeletonLens[6] = (bonesList[7].position - bonesList[10].position).magnitude; //FootLen
        skeletonLens[7] = (bonesList[16].position - bonesList[17].position).magnitude; //ShoulderLen
        skeletonLens[8] = (bonesList[16].position - bonesList[18].position).magnitude; //UpperArmLen
        skeletonLens[9] = (bonesList[18].position - bonesList[20].position).magnitude; //LowerArmLen
        skeletonLens[10] = 0.169f; //HandLen
    }
}