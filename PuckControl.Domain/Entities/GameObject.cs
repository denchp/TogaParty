﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;

namespace PuckControl.Domain.Entities
{
    public class GameObject
    {
        public event EventHandler ObjectMotion;
        public event EventHandler StatusChanged;

        public ModelMetadata Model { get; set; }
        public Int32 Mass { get; set; }
        public Int32 MotionSmoothingSteps { get; set; }
        public Vector3D Position
        {
            get
            {
                Vector3D averagedPosition = new Vector3D(0, 0, 0);
                Int32 Steps = 0;
                if ((Steps = Positions.Count()) == 0)
                    return averagedPosition;

                var localPositions = new Vector3D[10];
                Positions.CopyTo(localPositions, 0);
                foreach (var vector in localPositions)
                {
                    averagedPosition += (vector * (Array.IndexOf(localPositions, vector) + 1));
                }

                if (Steps > 1)
                    averagedPosition = averagedPosition / (Steps * (Steps + 1) / 2);

                return averagedPosition;
            }
            set
            {
                while (Positions.Count() >= MotionSmoothingSteps)
                    Positions.RemoveAt(0);

                Positions.Add(value);
                if (ObjectMotion != null)
                    ObjectMotion(this, new EventArgs());
            }
        }
        private List<Vector3D> Positions { get; set; }
        public Vector3D Motion { get; set; }
        private Vector3D _rotation;
        public Vector3D Rotation
        {
            get
            { return _rotation; }

            set
            {
                _rotation = value;
                if (ObjectMotion != null)
                    ObjectMotion(this, new EventArgs());
            }
        }
        public String ObjectType { get; set; }
        private bool _active;
        public bool Active
        {
            get
            {
                return _active;
            }

            set
            {
                if (value != _active)
                {
                    _active = value;
                    if (StatusChanged != null)
                        StatusChanged(this, new EventArgs());
                }
            }
        }
        public Rect3D Bounds { get; set; }
        public bool TrackCollisions { get; set; }
        public bool ControlledObject { get; set; }
        public bool ApplyPhysics { get; set; }
        public bool AnimateMovement { get; set; }
        public TimeSpan AnimateDuration { get; set; }

        public Vector3D Scale { get; set; }

        public GameObject()
        {
            Scale = new Vector3D(1, 1, 1);
            Positions = new List<Vector3D>();
            Model = new ModelMetadata();
            MotionSmoothingSteps = 1;
            ApplyPhysics = true;
            Rotation = new Vector3D(0, 0, 0);
            ControlledObject = false;
        }
    }
}
