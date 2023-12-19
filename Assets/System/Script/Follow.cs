using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public GameObject _target;
    public Vector3 offset;
    
    public float minDistance;
    public float maxDistance;
    
    bool eDown;
    bool qDown;
    
    public float cooldownTime = 1f;
    public float LcooldownTime = 1f;
    
    private float lastInputTime;
    private float LlastInputTime;
    
    int isRot = 0;

    public float rotationSpeed = 90f;
    
    Vector3 newRotation;
    Vector3 CnewRotation;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        
        eDown = Input.GetButton("Rot");
        qDown = Input.GetButton("LRot");
        
        Rotating();
    }
    
    void Rotating() {
        if(qDown == true && LCanRotate()) {
            LRotateObjectAroundYAxis(_target);
        }
        
        
        if(eDown == true && CanRotate()) {
            RotateObjectAroundYAxis(_target);
        }
        
    }
    
    void RotateObjectAroundYAxis(GameObject externalObject)
    {
        if (externalObject != null)
        {
            
            lastInputTime = Time.time;
            
            if (isRot == 0) {
                offset = new Vector3(-8, 3.5f, 0);
                
                newRotation = new Vector3(0, 90, 0);
                externalObject.transform.rotation = Quaternion.Euler(newRotation);
                
                CnewRotation = new Vector3(15, 90, 0);
                transform.rotation = Quaternion.Euler(CnewRotation);
                
                isRot = 1;
            }
            
            else if (isRot == 1) {
                offset = new Vector3(0, 3.5f, 8);
                
                newRotation = new Vector3(0, 180, 0);
                externalObject.transform.rotation = Quaternion.Euler(newRotation);
                
                CnewRotation = new Vector3(15, 180, 0);
                transform.rotation = Quaternion.Euler(CnewRotation);
                
                isRot = 2;
            }
            
            else if (isRot == 2) {
                offset = new Vector3(8, 3.5f, 0);
                
                newRotation = new Vector3(0, 270, 0);
                externalObject.transform.rotation = Quaternion.Euler(newRotation);
                
                CnewRotation = new Vector3(15, 270, 0);
                transform.rotation = Quaternion.Euler(CnewRotation);
                            
                isRot = 3;
            }
            
            else if (isRot == 3) {
                offset = new Vector3(0, 3.5f, -8);
                
                newRotation = new Vector3(0, 0, 0);
                externalObject.transform.rotation = Quaternion.Euler(newRotation);
                
                CnewRotation = new Vector3(15, 0, 0);
                transform.rotation = Quaternion.Euler(CnewRotation);
                
                isRot = 0;
            }
            
        }
    }
    
    void LRotateObjectAroundYAxis(GameObject externalObject)
    {
        if (externalObject != null)
        {
            // 현재 오브젝트의 회전 값을 가져와서 y축에 90도를 더합니다.
            Vector3 newRotation = externalObject.transform.rotation.eulerAngles + new Vector3(0, -90, 0);

            // 새로운 회전 값을 적용합니다.
            externalObject.transform.rotation = Quaternion.Euler(newRotation);
            
            LlastInputTime = Time.time;
                        
            if (isRot == 3) {
                offset = new Vector3(8, 3.5f, 0);
                
                newRotation = new Vector3(0, 270, 0);
                externalObject.transform.rotation = Quaternion.Euler(newRotation);
                
                CnewRotation = new Vector3(15, 270, 0);
                transform.rotation = Quaternion.Euler(CnewRotation);
                            
                isRot = 2;
                
            }
            
            else if (isRot == 2) {
                offset = new Vector3(0, 3.5f, 8);
                
                newRotation = new Vector3(0, 180, 0);
                externalObject.transform.rotation = Quaternion.Euler(newRotation);
                
                CnewRotation = new Vector3(15, 180, 0);
                transform.rotation = Quaternion.Euler(CnewRotation);
                            
                isRot = 1;
            }
            
            else if (isRot == 1) {
                offset = new Vector3(-8, 3.5f, 0);
                
                newRotation = new Vector3(0, 90, 0);
                externalObject.transform.rotation = Quaternion.Euler(newRotation);
                
                Vector3 CnewRotation = new Vector3(15, 90, 0);
                transform.rotation = Quaternion.Euler(CnewRotation);
                            
                isRot = 0;
            }
            
            else if (isRot == 0) {
                offset = new Vector3(0, 3.5f, -8);
                
                newRotation = new Vector3(0, 0, 0);
                externalObject.transform.rotation = Quaternion.Euler(newRotation);
                
                CnewRotation = new Vector3(15, 0, 0);
                transform.rotation = Quaternion.Euler(CnewRotation);
                            
                isRot = 3;
                
            }
            
        }
    }
    
    // 쿨다운 중인지 확인하는 함수
    bool CanRotate()
    {
        // 현재 시간과 마지막 입력 시간을 비교하여 쿨다운 중인지 확인
        return Time.time - lastInputTime >= cooldownTime;
    }
    
    bool LCanRotate()
    {
        // 현재 시간과 마지막 입력 시간을 비교하여 쿨다운 중인지 확인
        return Time.time - LlastInputTime >= LcooldownTime;
    }
    
    
}
