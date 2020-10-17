using System;
using System.IO;
using UnityEngine;
using System.Collections;
using Siccity.GLTFUtility;
using UnityEngine.Networking;

namespace Wolf3D.ReadyPlayerMe.AvatarSDK
{
    /// <summary>
    ///     Loads avatar models from URL and instantates to the current scene.
    /// </summary>
    public class AvatarLoader
    {
        // Attach EyeRotator component to the avatar if true.
        public bool UseEyeRotator { get; set; }

        // Attach VoiceHandler component to the avatar if true.
        public bool UseVoiceToAnimation { get; set; }

        // Used as a callback method for loaging avatar model.
        public Action<GameObject> OnAvatarLoaded;

        // Import progress of the model
        public float ImportProgress { get; private set; }

        // Download progress of the model
        public float DownloadProgress {
            get {
                return downloadCompleted ? 0 : request.downloadProgress;
            }
        }

        // Save destination of the avatar models under Application.persistentDataPath
        public string SaveFolder { get; set; } = "Avatars";

        // Avatar download timeout
        public int Timeout { get; set; } = 20;

        private bool downloadCompleted;
        private UnityWebRequest request;

        /// <summary>
        ///     Starts avatar loading operation.
        /// </summary>
        /// <param name="url">URL of the GLB model of the avatar</param>
        public void LoadAvatar(string url)
        {
            AvatarUri uri = new AvatarUri(url, SaveFolder);
            LoadAvatarAsync(uri).Run();
        }

        // Makes web request for downloading avatar model and imports the model.
        private IEnumerator LoadAvatarAsync(AvatarUri uri)
        {
            if (!File.Exists(uri.ModelPath))
            {
                using (request = new UnityWebRequest(uri.AbsoluteUrl))
                {
                    downloadCompleted = false;
                    request.downloadHandler = new DownloadHandlerFile(uri.ModelPath);
                    request.timeout = Timeout;

                    yield return request.SendWebRequest();

                    downloadCompleted = true;
                }
            }

            // Wait until file write to local is completed
            yield return new WaitUntil(() =>
            {
                return (new FileInfo(uri.ModelPath).Length > 0);
            });

            // Import avatar
            switch (uri.Extension)
            {
                case ".glb":
                    Importer.ImportGLBAsync(uri.ModelPath, new ImportSettings(), OnFinishAsync, OnImportProgress);
                    break;
                case ".gltf":
                    Importer.ImportGLTFAsync(uri.ModelPath, new ImportSettings(), OnFinishAsync, OnImportProgress);
                    break;
            }
        }
    
        // Once avatar import is compleded avatar component and other additional components
        // are attached to the avatar game object and it is sent to callback method.
        private void OnFinishAsync(GameObject avatar, AnimationClip[] animationClips)
        {
            ImportProgress = 0;

            Avatar avatarComponent = avatar.AddComponent<Avatar>();
            avatar.name = "Wolf3D.Avatar";
            avatarComponent.SetAnimationClips(animationClips);

            if (UseEyeRotator) avatar.AddComponent<EyeRotator>();
            if (UseVoiceToAnimation) avatar.AddComponent<VoiceHandler>();

            OnAvatarLoaded?.Invoke(avatar);
        }

        // Model importer progress callback
        private void OnImportProgress(float progress)
        {
            ImportProgress = progress;
        }
    }
}
