﻿using System.Collections.Generic;
using System.Linq;
using CrashKonijn.Goap.Configs;
using CrashKonijn.Goap.Configs.Interfaces;
using UnityEngine;

namespace CrashKonijn.Goap.Scriptables
{
    [CreateAssetMenu(menuName = "Goap/GoapSetConfig")]
    public class GoapSetConfigScriptable : ScriptableObject, IGoapSetConfig
    {
        public List<ActionConfigScriptable> actions = new List<ActionConfigScriptable>();
        public List<GoalConfigScriptable> goals = new List<GoalConfigScriptable>();

        public List<TargetSensorConfigScriptable> targetSensors = new List<TargetSensorConfigScriptable>();
        public List<WorldSensorConfigScriptable> worldSensors = new List<WorldSensorConfigScriptable>();

        public string Name => this.name;
        public List<IActionConfig> Actions => this.actions.Cast<IActionConfig>().ToList();
        public List<IGoalConfig> Goals => this.goals.Cast<IGoalConfig>().ToList();
        public List<ITargetSensorConfig> TargetSensors => this.targetSensors.Cast<ITargetSensorConfig>().ToList();
        public List<IWorldSensorConfig> WorldSensors => this.worldSensors.Cast<IWorldSensorConfig>().ToList();
    }
}