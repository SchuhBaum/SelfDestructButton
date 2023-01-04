using System.Collections.Generic;
using OptionalUI;
using UnityEngine;

namespace SelfDestructButton
{
    public class MainModOptions : OptionInterface
    {
        internal static InGameTranslator? inGameTranslator = null;

        private static Vector2 marginX = new();
        private static Vector2 pos = new();
        private static readonly float spacing = 20f;

        private static readonly List<float> boxEndPositions = new();

        private static readonly int numberOfCheckboxes = 3;
        private static readonly float checkBoxSize = 24f;
        private static readonly List<OpCheckBox> checkBoxes = new();
        private static readonly List<OpLabel> checkBoxesTextLabels = new();

        private static readonly float fontHeight = 20f;
        private static readonly List<OpLabel> textLabels = new();

        private static float CheckBoxWithSpacing => checkBoxSize + 0.25f * spacing;

        public MainModOptions() : base(MainMod.instance)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Tabs = new OpTab[1];
            Tabs[0] = new OpTab("Options");
            InitializeMarginAndPos();

            // Title
            AddNewLine();
            AddTextLabel("Self-Destruct Button Mod", bigText: true);
            DrawTextLabels(ref Tabs[0]);

            // Subtitle
            AddNewLine(0.5f);
            AddTextLabel("Version " + MainMod.instance?.Version, FLabelAlignment.Left);
            AddTextLabel("by SchuhBaum", FLabelAlignment.Right);
            DrawTextLabels(ref Tabs[0]);

            AddNewLine();

            // Description
            AddBox();
            AddNewLine(2.5f); // add some space for word wrapping and new lines
            AddTextLabel("Description:\n\nHold the '" + (inGameTranslator?.Translate("Pick up / Eat") ?? "Pick up / Eat") + "' button to self-destruct when being stun-grabbed by a creature.\n\nHelpful when using the 'JollyCoop Mod' where being stun-grabbed does not trigger the game-over screen. Can be combined with the 'Auto Destruction Mod' to create an explosion when self-destructing.", FLabelAlignment.Left);

            DrawTextLabels(ref Tabs[0]);
            AddNewLine(2.5f);
            DrawBox(ref Tabs[0]);

            // Content //
            AddNewLine(2f);
            AddBox();

            AddCheckBox("Option_DropBugBite", "Dropwig Bite", "When enabled, Dropwigs attacks will only grab the player. Except when they are bleeding out and want to surprise-attack the player.", defaultBool: false);
            AddCheckBox("Option_EmergencyThrow", "Emergency Throw", "When enabled, the player cannot throw objects after being stun grabbed by a creature.", defaultBool: false);
            AddCheckBox("Option_LizardBite", "Lizard Bite", "When enabled, lizard bites will only grab the player.", defaultBool: false);

            AddCheckBox("Option_LowHealthInstaKill", "Low Health Insta-Kill", "When enabled and a creature's health is below 50 percent, the creature insta-kills the player instead of grabbing him.", defaultBool: false);
            AddCheckBox("Option_RedLizards", "Red Lizards", "When enabled, red lizards are always deadly even when the Lizard Bite option is enabled.", defaultBool: true);
            AddCheckBox("Option_SpiderBite", "Spider Bite", "When enabled, big spider charged attacks will only grab the player.", defaultBool: false);

            DrawCheckBoxes(ref Tabs[0]);
            DrawBox(ref Tabs[0]);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        public override void ConfigOnChange()
        {
            base.ConfigOnChange();
            MainMod.Option_DropBugBite = bool.Parse(config["Option_DropBugBite"]);
            MainMod.Option_EmergencyThrow = bool.Parse(config["Option_EmergencyThrow"]);
            MainMod.Option_LizardBite = bool.Parse(config["Option_LizardBite"]);

            MainMod.Option_LowHealthInstaKill = bool.Parse(config["Option_LowHealthInstaKill"]);
            MainMod.Option_RedLizards = bool.Parse(config["Option_RedLizards"]);
            MainMod.Option_SpiderBite = bool.Parse(config["Option_SpiderBite"]);

            Debug.Log("SelfDestructButton: Option_DropBugBite " + MainMod.Option_DropBugBite);
            Debug.Log("SelfDestructButton: Option_EmergencyThrow " + MainMod.Option_EmergencyThrow);
            Debug.Log("SelfDestructButton: Option_LizardBite " + MainMod.Option_LizardBite);

            Debug.Log("SelfDestructButton: Option_LowHealthInstaKill " + MainMod.Option_LowHealthInstaKill);
            Debug.Log("SelfDestructButton: Option_RedLizards " + MainMod.Option_RedLizards);
            Debug.Log("SelfDestructButton: Option_SpiderBite " + MainMod.Option_SpiderBite);
        }

        // ----------------- //
        // private functions //
        // ----------------- //

        private static void InitializeMarginAndPos()
        {
            marginX = new Vector2(50f, 550f);
            pos = new Vector2(50f, 600f);
        }

        private static void AddNewLine(float spacingModifier = 1f)
        {
            pos.x = marginX.x; // left margin
            pos.y -= spacingModifier * spacing;
        }

        private static void AddBox()
        {
            marginX += new Vector2(spacing, -spacing);
            boxEndPositions.Add(pos.y);
            AddNewLine();
        }

        private static void DrawBox(ref OpTab tab)
        {
            marginX += new Vector2(-spacing, spacing);
            AddNewLine();

            float boxWidth = marginX.y - marginX.x;
            int lastIndex = boxEndPositions.Count - 1;
            tab.AddItems(new OpRect(pos, new Vector2(boxWidth, boxEndPositions[lastIndex] - pos.y)));
            boxEndPositions.RemoveAt(lastIndex);
        }

        private void AddCheckBox(string key, string text, string description, bool? defaultBool = null)
        {
            OpCheckBox opCheckBox = new(new Vector2(), key, defaultBool: defaultBool ?? false)
            {
                description = description
            };

            checkBoxes.Add(opCheckBox);
            checkBoxesTextLabels.Add(new OpLabel(new Vector2(), new Vector2(), text, FLabelAlignment.Left));
        }

        private void DrawCheckBoxes(ref OpTab tab) // changes pos.y but not pos.x
        {
            if (checkBoxes.Count != checkBoxesTextLabels.Count)
            {
                return;
            }

            float width = marginX.y - marginX.x;
            float elementWidth = (width - (numberOfCheckboxes - 1) * 0.5f * spacing) / numberOfCheckboxes;
            pos.y -= checkBoxSize;
            float _posX = pos.x;

            for (int index = 0; index < checkBoxes.Count; ++index)
            {
                OpCheckBox checkBox = checkBoxes[index];
                checkBox.pos = new Vector2(_posX, pos.y);
                tab.AddItems(checkBox);
                _posX += CheckBoxWithSpacing;

                OpLabel checkBoxLabel = checkBoxesTextLabels[index];
                checkBoxLabel.pos = new Vector2(_posX, pos.y + 2f);
                checkBoxLabel.size = new Vector2(elementWidth - CheckBoxWithSpacing, fontHeight);
                tab.AddItems(checkBoxLabel);

                if (index < checkBoxes.Count - 1)
                {
                    if ((index + 1) % numberOfCheckboxes == 0)
                    {
                        AddNewLine();
                        pos.y -= checkBoxSize;
                        _posX = pos.x;
                    }
                    else
                    {
                        _posX += elementWidth - CheckBoxWithSpacing + 0.5f * spacing;
                    }
                }
            }

            checkBoxes.Clear();
            checkBoxesTextLabels.Clear();
        }

        private static void AddTextLabel(string text, FLabelAlignment alignment = FLabelAlignment.Center, bool bigText = false)
        {
            float textHeight = (bigText ? 2f : 1f) * fontHeight;
            if (textLabels.Count == 0)
            {
                pos.y -= textHeight;
            }

            OpLabel textLabel = new(new Vector2(), new Vector2(20f, textHeight), text, alignment, bigText) // minimal size.x = 20f
            {
                autoWrap = true
            };
            textLabels.Add(textLabel);
        }

        private static void DrawTextLabels(ref OpTab tab)
        {
            if (textLabels.Count == 0)
            {
                return;
            }

            float width = (marginX.y - marginX.x) / textLabels.Count;
            foreach (OpLabel textLabel in textLabels)
            {
                textLabel.pos = pos;
                textLabel.size += new Vector2(width - 20f, 0.0f);
                tab.AddItems(textLabel);
                pos.x += width;
            }

            pos.x = marginX.x;
            textLabels.Clear();
        }
    }
}