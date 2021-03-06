﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace NC_Reactor_Planner
{
    public class Block
    {
        private bool _valid;
        public string DisplayName { get; private set; }
        public Bitmap Texture { get; set; }
        public Vector3 Position { get; private set; }
        public BlockTypes BlockType { get; private set; }
        public virtual bool Valid { get => true; protected set => _valid = value; }

        public Block()
        {
            DisplayName = "Air";
            Texture = Palette.Textures["Air"];
            Position = Palette.dummyPosition;
        }

        public Block(string displayName, BlockTypes blockType, Bitmap texture, Vector3 position)
        {
            DisplayName = displayName;
            Texture = texture;
            Position = position;
            BlockType = blockType;
        }

        public virtual string GetToolTip()
        {
            string toolTip = DisplayName + "\r\n";
            return toolTip;
        }

        public virtual void ReloadValuesFromConfig()
        {
        }

        public virtual Block Copy(Vector3 newPosition)
        {
            return new Block(DisplayName, BlockType, Texture, newPosition);
        }

        public virtual Dictionary<string, int> GetResourceCosts()
        {
            return new Dictionary<string, int>();
        }
    }
}
