using System;
using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using SA.Android.Gallery.Internal;
using StansAssets.Foundation.Async;
using UnityEngine;
using UnityEngine.Networking;

namespace SA.Android.Gallery
{
    /// <summary>
    /// Picked image from gallery representation
    /// </summary>
    [Serializable]
    public class AN_Media
    {
        [SerializeField] string m_path = null;
        [SerializeField] AN_MediaType m_type = AN_MediaType.Image;

        const string k_URLFilePathPrefix = "file://";

        /// <summary>
        /// The picked media path
        /// </summary>
        public string Path => m_path;

        public void GetThumbnailAsync([NotNull] Action<Texture2D> imageLoadCallback) {
            if (imageLoadCallback == null) {
                throw new ArgumentNullException(nameof(imageLoadCallback));
            }

            if (string.IsNullOrEmpty(m_path)) {
                imageLoadCallback.Invoke(null);
            }
            else {
                switch (m_type) {
                    case AN_MediaType.Video:
                        AN_GalleryInternal.GetVideoThumbnailAtPosition(m_path, 0, result => {
                            var media = result.Media.FirstOrDefault();
                            if (media == null) {
                                imageLoadCallback.Invoke(null);
                            }
                            else {
                                media.GetThumbnailAsync(imageLoadCallback);
                            }
                        });
                        break;
                    case AN_MediaType.Image:
                        CoroutineUtility.Start(LoadIntoTexture(m_path, imageLoadCallback));
                        break;
                }
            }
        }

        static IEnumerator LoadIntoTexture(string absolutePath, Action<Texture2D> callback) {
            var adjustedPath = k_URLFilePathPrefix + absolutePath;
            using (var request = UnityWebRequestTexture.GetTexture(adjustedPath)) {
                yield return request.SendWebRequest();
                if (!request.isNetworkError && !request.isHttpError) {
                    callback.Invoke(DownloadHandlerTexture.GetContent(request));
                }
                else {
                    callback.Invoke(null);
                }
            }
        }

        public AN_MediaType Type => m_type;
    }
}
