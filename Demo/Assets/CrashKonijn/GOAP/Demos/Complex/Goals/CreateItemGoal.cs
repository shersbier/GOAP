﻿using CrashKonijn.Goap.Demos.Complex.Interfaces;
using CrashKonijn.Goap.Runtime;

namespace CrashKonijn.Goap.Demos.Complex.Goals
{
    public class CreateItemGoal<THoldable> : GoalBase
        where THoldable : ICreatable
    {
    }
}