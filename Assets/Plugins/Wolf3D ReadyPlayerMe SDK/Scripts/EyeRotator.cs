using UnityEngine;

namespace Wolf3D.ReadyPlayerMe.AvatarSDK
{
    [RequireComponent(typeof(Avatar)), DisallowMultipleComponent]
    public class EyeRotator : MonoBehaviour
    {
        private const int VerticalMargin = 3;
        private const int HorizontalMargin = 5;

        private Avatar avatar;

        void Start()
        {
            avatar = GetComponent<Avatar>();

            InvokeRepeating("RotateEyes", 1, 3);
        }

        private void RotateEyes()
        {
            float vertical = Random.Range(-VerticalMargin, VerticalMargin);
            float horizontal = Random.Range(-HorizontalMargin, HorizontalMargin);

            Quaternion rotation = Quaternion.Euler(-90 + horizontal, 0, 180 + vertical);

            avatar.LeftEyeBone.localRotation = rotation;
            avatar.RightEyeBone.localRotation = rotation;
        }
    }
}
