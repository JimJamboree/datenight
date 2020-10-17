using UnityEngine;

namespace Wolf3D.ReadyPlayerMe.AvatarSDK
{
    /// <summary>
    ///     Avatar component is attached to the loaded avatar model by <see cref="AvatarLoader"/>
    ///     once the script starts running sets references of avatar parts.
    /// </summary>
    public class Avatar : MonoBehaviour
    {
        //Head Mesh
        private const string HeadObjectName = "Wolf3D_Head";
        public SkinnedMeshRenderer HeadMesh { get; private set; }

        //Eye Meshes and Bone Transforms
        private const string LeftEyeObjectName = "EyeLeft";
        public SkinnedMeshRenderer LeftEyeMesh { get; private set; }
        public Transform LeftEyeBone { get; private set; }
        private const string LeftEyeBoneName = "Hips/Spine/Neck/Head/LeftEye";

        private const string RightEyeObjectName = "EyeRight";
        public SkinnedMeshRenderer RightEyeMesh { get; private set; }
        private const string RightEyeBoneName = "Hips/Spine/Neck/Head/RightEye";
        public Transform RightEyeBone { get; private set; }

        //Hand object name and Transform
        private const string HandsObjectName = "Wolf3D_Hands";
        public SkinnedMeshRenderer HandsMesh { get; private set; }

        //Shirt object
        private const string ShirtObjectName = "Wolf3D_Shirt";
        public SkinnedMeshRenderer ShirtMesh { get; private set; }

        //Hair object
        private const string HairObjectName = "Wolf3D_Hair";
        public SkinnedMeshRenderer HairMesh { get; private set; }

        //Teeth object
        private const string TeethObjectName = "Wolf3D_Teeth";
        public SkinnedMeshRenderer TeethMesh { get; private set; }

        //Beard object
        private const string BeardObjectName = "Wolf3D_Beard";
        public SkinnedMeshRenderer BeardMesh { get; private set; }

        //Glasses object
        private const string GlassesObjectName = "Wolf3D_Glasses";
        public SkinnedMeshRenderer GlassesMesh { get; private set; }

        //Postfix to remove from names for correction
        private const string Wolf3DPostfix = "Wolf3D_";

        //Main texture and normal map property IDs
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private static readonly int BumpMap = Shader.PropertyToID("_BumpMap");

        //
        private Renderer[] renderers = new Renderer[0];
        private AnimationClip[] animationClips = new AnimationClip[0];

        /// <summary>
        ///     Assigns the references of avatar parts.
        /// </summary>
        private void Start()
        {
            InitializeAvatarProperties();

            #if UNITY_EDITOR
                SetAvatarAssetNames();
            #endif
        }

        /// <summary>
        ///     Set animation clips of the avatar that are coming from GLTFUtility.
        /// </summary>
        /// <param name="animationClips">Array of avatar animation clips</param>
        public void SetAnimationClips(AnimationClip[] animationClips)
        {
            this.animationClips = animationClips;
        }

        /// <summary>
        ///     Set references of avatar head mesh and other assets.
        /// </summary>
        private void InitializeAvatarProperties()
        {
            HeadMesh = transform.Find(HeadObjectName)?.GetComponent<SkinnedMeshRenderer>();

            LeftEyeBone = transform.Find(LeftEyeBoneName)?.transform;
            LeftEyeMesh = transform.Find(LeftEyeObjectName)?.GetComponent<SkinnedMeshRenderer>();

            RightEyeBone = transform.Find(RightEyeBoneName)?.transform;
            RightEyeMesh = transform.Find(RightEyeObjectName)?.GetComponent<SkinnedMeshRenderer>();

            HairMesh    = transform.Find(HairObjectName)?.GetComponent<SkinnedMeshRenderer>();
            HandsMesh   = transform.Find(HandsObjectName)?.GetComponent<SkinnedMeshRenderer>();
            ShirtMesh   = transform.Find(ShirtObjectName)?.GetComponent<SkinnedMeshRenderer>();
            TeethMesh   = transform.Find(TeethObjectName)?.GetComponent<SkinnedMeshRenderer>();
            BeardMesh   = transform.Find(BeardObjectName)?.GetComponent<SkinnedMeshRenderer>();
            GlassesMesh = transform.Find(GlassesObjectName)?.GetComponent<SkinnedMeshRenderer>();
        }

        #region Set Names
        /// <summary>
        ///     Name avatar assets for make them easier to view in profiler.
        ///     Naming is 'Wolf3D.Avatar_<Type>_<Name>'
        /// </summary>
        private void SetAvatarAssetNames()
        {
            if(renderers.Length == 0)
            {
                renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            }

            foreach (var renderer in renderers)
            {
                string assetName = renderer.name.Replace(Wolf3DPostfix, "");

                renderer.name = $"Wolf3D.Avatar_Renderer_{assetName}";
                renderer.material.name = $"Wolf3D.Avatar_Material_{assetName}";
                SetTextureName(renderer, assetName, MainTex);
                SetTextureName(renderer, assetName, BumpMap);
                SetMeshName(renderer, assetName);
            }

            foreach (var clip in animationClips)
            {
                clip.name = $"Wolf3D.Avatar_AnimationClip_{clip.name}";
            }
        }

        /// <summary>
        ///     Set a name for the texture for finding it in the Profiler.
        /// </summary>
        /// <param name="renderer">Renderer to find the texture in.</param>
        /// <param name="assetName">Name of the asset.</param>
        /// <param name="propertyID">Property ID of the texture.</param>
        private void SetTextureName(Renderer renderer, string assetName, int propertyID)
        {
            if (renderer.material.HasProperty(propertyID))
            {
                var texture = renderer.material.GetTexture(propertyID);

                if (texture != null)
                {
                    texture.name = $"Wolf3D.Avatar_MainTex_{assetName}";
                }
            }
        }

        /// <summary>
        ///     Set a name for the mesh for finding it in the Profiler.
        /// </summary>
        /// <param name="renderer">Renderer to find the mesh in.</param>
        /// <param name="assetName">Name of the asset.</param>
        private void SetMeshName(Renderer renderer, string assetName)
        {
            if(renderer is SkinnedMeshRenderer)
            {
                (renderer as SkinnedMeshRenderer).sharedMesh.name = $"Wolf3D.Avatar_SkinnedMesh_{assetName}";
            }
            else if(renderer is MeshRenderer)
            {
                MeshFilter meshFilter = renderer.GetComponent<MeshFilter>();

                if(meshFilter != null)
                {
                    meshFilter.mesh.name = $"Wolf3D.Avatar_Mesh_{assetName}";
                }
            }
        }
        #endregion

        #region Destroy Assets
        private void OnDestroy()
        {
            if (renderers == null)
            {
                renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            }

            foreach (var renderer in renderers)
            {
                DestroyTexture(renderer, MainTex);
                DestroyTexture(renderer, BumpMap);
                DestroyMesh(renderer);
                Destroy(renderer.material);
            }

            renderers = null;

            foreach (var clip in animationClips)
            {
                Destroy(clip);
            }

            animationClips = null;
        }

        /// <summary>
        ///     Destroy the texture instance.
        /// </summary>
        /// <param name="renderer">Renderer to find the texture in.</param>
        /// <param name="propertyID">Property ID of the texture.</param>
        private void DestroyTexture(Renderer renderer, int propertyID)
        {
            if (renderer.material.HasProperty(propertyID))
            {
                Destroy(renderer.material.GetTexture(propertyID));
            }
        }

        /// <summary>
        ///     Destroy the mesh instance.
        /// </summary>
        /// <param name="renderer">Renderer to find the mesh in.</param>
        private void DestroyMesh(Renderer renderer)
        {
            if (renderer is SkinnedMeshRenderer)
            {
                Destroy((renderer as SkinnedMeshRenderer).sharedMesh);
            }
            else if (renderer is MeshRenderer)
            {
                MeshFilter meshFilter = renderer.GetComponent<MeshFilter>();

                if (meshFilter != null)
                {
                    Destroy(meshFilter.mesh);
                }
            }
        }
        #endregion
    }
}
