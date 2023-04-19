using System;
using System.Collections.Generic;
using SA.Android.App;
using SA.Android.Gallery.Internal;
using SA.Android.Manifest;

namespace SA.Android.Gallery
{
    /// <summary>
    /// Picker object for picking media from the device storage
    /// </summary>
    public class AN_MediaPicker
    {
        readonly List<AN_MediaType> m_Types;

        /// <summary>
        /// Create new instance of the picker with predefined picker types
        /// </summary>
        /// <param name="types"></param>
        public AN_MediaPicker(params AN_MediaType[] types)
        {
            m_Types = new List<AN_MediaType>(types);
        }

        /// <summary>
        /// Max thumbnail size that will be transferred to the Unity side.
        /// The thumbnail will be resized before it sent.
        /// The default value is 512.
        /// </summary>
        public int MaxSize { get; set; } = 512;

        /// <summary>
        /// Defines if multiple images picker is allowed.
        /// The default value is <c>false</c>
        /// </summary>
        public bool AllowMultiSelect { get; set; }

        /// <summary>
        /// Starts pick media from a gallery flow.
        /// </summary>
        /// <param name="callback"></param>
        public void Show(Action<AN_GalleryPickResult> callback)
        {
            AN_PermissionsUtility.TryToResolvePermission(
                new[] { AMM_ManifestPermission.READ_EXTERNAL_STORAGE, AMM_ManifestPermission.WRITE_EXTERNAL_STORAGE },
                (granted) =>
                {
                    var type = AN_GalleryChooseType.PICK_PICTURE;
                    if (m_Types.Contains(AN_MediaType.Image) && m_Types.Contains(AN_MediaType.Video))
                    {
                        type = AN_GalleryChooseType.PICK_PICTURE_OR_VIDEO;
                    }
                    else
                    {
                        if (m_Types.Contains(AN_MediaType.Video)) type = AN_GalleryChooseType.PICK_VIDEO;
                    }

                    AN_GalleryInternal.PickImageFromGallery(type, AllowMultiSelect, callback);
                });
        }
    }
}
