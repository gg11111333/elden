using UnityEngine;

namespace SG{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;

        public PlayerManager player;

        public Camera cameraObject;
        [SerializeField] Transform cameraPivotTransform;

        [Header("Camera Settings")]
        private float cameraSmoothSpeed = 1;
        [SerializeField] float LeftAndRightRotationSpeed = 220;
        [SerializeField] float upAndDownRotationSpeed = 220;
        [SerializeField] float minimumPivot = -30;
        [SerializeField] float maximumPivot = 60;
        [SerializeField] float cameraCollisionOffset = 0.2f;
        [SerializeField] LayerMask collideWithLayers;


        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        private Vector3 cameraObjectPosition;
        [SerializeField] float LeftAndRightLookAngle;
        [SerializeField] float upAndDownLookAngle;
        private float cameraZPosition;
        private float targetCameraZPosition;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            cameraZPosition = cameraObject.transform.localPosition.z;    
        }

        public void HandleAllCameraActions()
        {
            if(player != null)
            {
                HandleFollowTarget();
                HandleRotations();
                HandlerCollisions();
            }
        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
            
        }
        

        private void HandleRotations()
        {
            LeftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * LeftAndRightRotationSpeed) * Time.deltaTime;
            upAndDownLookAngle -= (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;

            cameraRotation.y = LeftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotation;
        
        
        }
    
         private void HandlerCollisions()
        {
            targetCameraZPosition = cameraZPosition;
            RaycastHit hit;
            Vector3 director = cameraObject.transform.position - cameraPivotTransform.position;
            director.Normalize();

            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionOffset, director, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
            {
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionOffset);
            }
            if (Mathf.Abs(targetCameraZPosition) < cameraCollisionOffset)
            {
                targetCameraZPosition = -cameraCollisionOffset;
            }
            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, cameraCollisionOffset);
            cameraObject.transform.localPosition = cameraObjectPosition;
        }
    
    
    
    
    
    
    
    }

}   