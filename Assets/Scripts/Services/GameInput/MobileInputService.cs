﻿using UnityEngine;

namespace Services.GameInput
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}