using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUROPORO
{
    public class GoKartStrikerController : MonoBehaviour
    {
        private GoKartController m_Controller;

        [Header("Visuals")]
        [SerializeField] private Transform m_VisualWheelFL;
        [SerializeField] private Transform m_VisualWheelFR;
        [SerializeField] private Transform m_VisualWheelRL;
        [SerializeField] private Transform m_VisualWheelRR;
        [SerializeField] private Transform m_VisualSteeringWheel;
        //[SerializeField] private Transform m_VisualSuspensionFL;
        //[SerializeField] private Transform m_VisualSuspensionFR;
        //[SerializeField] private Transform m_VisualSuspensionRear;
        [SerializeField] private Transform m_VisualGearSmall;
        [SerializeField] private Transform m_VisualGearBig;
        [SerializeField] private Transform m_VisualEngine;
        //[SerializeField] private Transform m_VisualExhausPipeLeft;
        //[SerializeField] private Transform m_VisualExhausPipeRight;

        private void Awake()
        {
            m_Controller = GetComponent<GoKartController>();
        }

        private void LateUpdate()
        {
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            if (m_VisualWheelFL != null) { UpdateSingleWheel(m_Controller.wheelColliderFL, m_VisualWheelFL); }
            if (m_VisualWheelFR != null) { UpdateSingleWheel(m_Controller.wheelColliderFR, m_VisualWheelFR); }
            if (m_VisualWheelRL != null) { UpdateSingleWheel(m_Controller.wheelColliderRL, m_VisualWheelRL); }
            if (m_VisualWheelRR != null) { UpdateSingleWheel(m_Controller.wheelColliderRR, m_VisualWheelRR); }

            if (m_VisualSteeringWheel != null)
            {
                m_VisualSteeringWheel.localEulerAngles = new Vector3(
                    m_VisualSteeringWheel.localEulerAngles.x,
                    m_VisualSteeringWheel.localEulerAngles.y,
                    -m_Controller.currentSteeringAngle);
            }

            if (m_VisualGearSmall != null) { m_VisualGearSmall.Rotate(new Vector3(0, Mathf.Abs(m_Controller.inputAcceleration) * m_Controller.accelerationForce + 1080, 0) * Time.deltaTime); }
            if (m_VisualGearBig != null) { m_VisualGearBig.Rotate(new Vector3(0, Mathf.Abs(m_Controller.inputAcceleration) * m_Controller.accelerationForce + 1080, 0) * Time.deltaTime); }

            if (m_VisualEngine != null)
            {
                float temp = Mathf.Sin(Time.time * m_Controller.inputAcceleration);
                m_VisualEngine.localEulerAngles = new Vector3(89.98f + temp, temp, temp);
            }
        }

        private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
        {
            Vector3 pos;
            Quaternion rot;
            wheelCollider.GetWorldPose(out pos, out rot);
            wheelTransform.rotation = rot;
            wheelTransform.position = pos;
        }
    }
}
