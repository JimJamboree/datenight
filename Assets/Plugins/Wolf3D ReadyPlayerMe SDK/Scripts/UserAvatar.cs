using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolf3D.ReadyPlayerMe.AvatarSDK;
using Avatar = Wolf3D.ReadyPlayerMe.AvatarSDK.Avatar;

namespace Wolf3D.ReadyPlayerMe.AvatarSDK
{
    public class UserAvatar : MonoBehaviour
    {
        #pragma warning disable 0649
        public string avatarUrl;
        [SerializeField] private GameObject avatarClamp;
        [SerializeField] private bool disableHair;
        [SerializeField] private bool disableEyes;
        [SerializeField] private bool disableHands;
        [SerializeField] private bool disableShirt;
        [SerializeField] private bool disableTeeth;
        [SerializeField] private bool disableBeard;
        [SerializeField] private bool disableGlasses;
#pragma warning restore 0649

        private void Start()
        {
            AvatarLoader avatarLoader = new AvatarLoader();
            avatarLoader.UseEyeRotator = true;
            avatarLoader.UseVoiceToAnimation = true;

            avatarLoader.OnAvatarLoaded = OnAvatarLoaded;
            avatarLoader.LoadAvatar(avatarUrl);

#if UNITY_2018
            Light light = FindObjectOfType<Light>();
            light.intensity = 0.7f;
#endif
        }

        // Callback that runs when avatar is loaded.
        private void OnAvatarLoaded(GameObject avatarObject)
        {
            Debug.Log("AvatarLoader.OnFinishAsync: Avatar Loaded");

            AfterAvatarLoaded(avatarObject).Run();
        }

        // Callback that runs one frame after avatar is loaded, used for waiting avatar component to initialize.
        private IEnumerator AfterAvatarLoaded(GameObject avatarObject)
        {
            yield return new WaitForEndOfFrame();

            Avatar avatar = avatarObject.GetComponent<Avatar>();
            avatar.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            avatar.gameObject.transform.position = avatarClamp.transform.position;
            avatar.gameObject.transform.rotation = avatarClamp.transform.rotation;

            avatar.HairMesh?.gameObject.SetActive(!disableHair);
            avatar.HandsMesh?.gameObject.SetActive(!disableHands);
            avatar.ShirtMesh?.gameObject.SetActive(!disableShirt);
            avatar.TeethMesh?.gameObject.SetActive(!disableTeeth);
            avatar.BeardMesh?.gameObject.SetActive(!disableBeard);
            avatar.LeftEyeMesh?.gameObject.SetActive(!disableEyes);
            avatar.RightEyeMesh?.gameObject.SetActive(!disableEyes);
            avatar.GlassesMesh?.gameObject.SetActive(!disableGlasses);
        }

        public void SetAvatarAndWake(string newAvatar)
        {
            avatarUrl = newAvatar;
            this.gameObject.SetActive(true);
        }
    }
}
