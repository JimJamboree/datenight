using System;

namespace Wolf3D.ReadyPlayerMe.AvatarSDK
{
    [Serializable]
    public class UnsupportedExtensionException : Exception
    {
        public UnsupportedExtensionException(string extension) : base($"Exceptions.UnsupportedExtensionException: Unsupported model extension { extension }. Only .gltf and .glb formats are supported") { }
    }
}