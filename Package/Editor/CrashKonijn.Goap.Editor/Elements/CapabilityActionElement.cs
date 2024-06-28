﻿using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEditor;
using UnityEngine.UIElements;

namespace CrashKonijn.Goap.Editor.Elements
{
    public class CapabilityActionElement : VisualElement, IFoldable
    {
        public Foldout Foldout { get; private set; }
        public ClassRefField Action { get; private set; }
        public IntegerField BaseCostField { get; private set; }
        public ClassRefField Target { get; private set; }

        public FloatField InRangeField { get; set; }

        public EnumField MoveModeField { get; set; }
        public ActionPropertiesElement Properties { get; private set; }

        
        public CapabilityActionElement(SerializedObject serializedObject, SerializedProperty serializedProperty,
            CapabilityConfigScriptable scriptable, GeneratorScriptable generator, BehaviourAction item)
        {
            this.Foldout = new Foldout
            {
                value = false
            };
            this.Add(this.Foldout);

            var card = new Card((card) =>
            {
                var action = new LabeledField<ClassRefField>("Action", new ClassRefField());
                this.Action = action.Field;
                card.Add(action);

                var target = new LabeledField<ClassRefField>("Target", new ClassRefField());
                this.Target = target.Field;
                card.Add(target);

                var baseCost = new LabeledField<IntegerField>("Base Cost");
                this.BaseCostField = baseCost.Field;
                card.Add(baseCost);

                var inRange = new LabeledField<FloatField>("In Range");
                this.InRangeField = inRange.Field;
                card.Add(inRange);

                var moveMode = new LabeledField<EnumField>("Move Mode", new EnumField(ActionMoveMode.MoveBeforePerforming));
                this.MoveModeField = moveMode.Field;
                card.Add(moveMode);

                card.Add(new Label("Effects"));
                var effects = new EffectList(serializedProperty, scriptable, generator, item.effects);
                card.Add(effects);

                card.Add(new Label("Conditions"));
                var conditions = new ConditionList(serializedProperty, scriptable, generator, item.conditions);
                card.Add(conditions);
                
                var properties = new ActionPropertiesElement(serializedObject);
                this.Properties = properties;
                card.Add(properties);
            });
            
            this.Foldout.Add(card);
            
            this.Foldout.RegisterValueChangedCallback(evt => 
            {
                // Optional: You might need to trigger a layout update on the root
                // this.parent.parent.MarkDirtyRepaint();
            });
        }
    }
}