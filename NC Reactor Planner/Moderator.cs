﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace NC_Reactor_Planner
{
    public class Moderator : Block
    {

        public bool Active { get; private set; }
        public ModeratorTypes ModeratorType { get; private set; }
        public double FluxFactor { get; private set; }
        public double EfficiencyFactor { get; private set; }

        public Moderator(string displayName, ModeratorTypes type, Bitmap texture, Point3D position, double fluxFactor, double efficiencyFactor) : base(displayName, BlockTypes.Moderator, texture, position)
        {
            FluxFactor = fluxFactor;
            EfficiencyFactor = efficiencyFactor;
            Active = false;
            ModeratorType = type;
        }

        public Moderator(Moderator parent, Point3D position) : this(parent.DisplayName, parent.ModeratorType, parent.Texture, position, parent.FluxFactor, parent.EfficiencyFactor)
        {
            ModeratorType = parent.ModeratorType;
        }

        public void UpdateStats()
        {
            Active = FindAdjacentFuelCells() > 0;
        }

        public int FindAdjacentFuelCells()
        {
            int adjCells = 0;
            foreach (Vector3D o in Reactor.sixAdjOffsets)
                if (Reactor.BlockAt(Position + o) is FuelCell fuelCell)
                    if(fuelCell.Active)
                        adjCells++;
            return adjCells;
        }

        public override string GetToolTip()
        {
            string toolTip = DisplayName + " Moderator\r\n";
            if (Position != Palette.dummyPosition)
            {
                toolTip += string.Format("at: X: {0} Y: {1} Z: {2}\r\n", Position.X, Position.Y, Position.Z);
                if (!Active)
                    toolTip += "Inactive!\r\n";
            }
            toolTip += string.Format("Flux Factor: {0}\r\n", FluxFactor);
            toolTip += string.Format("Efficiency Factor: {0}\r\n", EfficiencyFactor);
            return toolTip;
        }

        public override Block Copy(Point3D newPosition)
        {
            return new Moderator(this, newPosition);
        }

        public override void ReloadValuesFromConfig()
        {
            FluxFactor = Configuration.Moderators[DisplayName].FluxFactor;
            EfficiencyFactor = Configuration.Moderators[DisplayName].EfficiencyFactor;
        }

    }

    public enum ModeratorTypes
    {
        Beryllium,
        Graphite,
        //NotAModerator,
    }
}
