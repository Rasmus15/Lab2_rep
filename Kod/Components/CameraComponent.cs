using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Components
{
    class CameraComponent : IComponent
    {
        #region Properties
        public float FOV { get; set; }
        public float NearPlane { get; set; }
        public float FarPlane { get; set; }
        public float Ratio { get; set; }
        public Matrix View { get; set; }
        public Matrix Proj { get; set; }
        public Vector3 CameraOffset {get; set; }
        public Vector3 LookAt { get; set; }
        #endregion

        public CameraComponent(float fov, float nearPlane, float farPlane, float ratio, Vector3 cameraOffset, Vector3 lookat)
        {
            FOV = fov;
            NearPlane = nearPlane;
            FarPlane = farPlane;
            Ratio = ratio;
            LookAt = lookat;

            View = Matrix.Identity;
            CameraOffset = cameraOffset;
        }
    }
}
