﻿using DTC.Models.Base;
using DTC.Models.F16;
using DTC.Models.FA18;
using DTC.Models.AH64;
using DTC.Models.A10CII;
using System;
using System.Collections.Generic;

namespace DTC.Models.Presets
{
	public class Aircraft
	{
		public string Name {
			get
			{
				if (Model == AircraftModel.F16C)
				{
					return "F-16C";
				}
				else if (Model == AircraftModel.FA18C)
				{
					return "F/A-18C";
				}
                else if (Model == AircraftModel.AH64D)
                {
                    return "AH-64D";
                }
                else if (Model == AircraftModel.A10CII)
                {
                    return "A-10CII";
                }
                throw new Exception();
			}
		}

		public List<Preset> Presets { get; set; }
		public AircraftModel Model { get; set; }

		public Aircraft(AircraftModel model)
		{
			Presets = new List<Preset>();
			Model = model;
		}

		public string GetAircraftModelName()
		{
			return Enum.GetName(typeof(AircraftModel), Model);
		}

		public Type GetAircraftConfigurationType()
		{
			if (Model == AircraftModel.F16C)
			{
				return typeof(F16Configuration);
			}
			else if (Model == AircraftModel.FA18C)
			{
				return typeof(FA18Configuration);
			}
			else if (Model == AircraftModel.AH64D)
			{
				return typeof(AH64Configuration);
			}
            else if (Model == AircraftModel.A10CII)
            {
                return typeof(A10CIIConfiguration);
            }
            throw new Exception();
		}

		public Preset CreatePreset(string name, IConfiguration cfg = null)
		{
			if (Model == AircraftModel.F16C)
			{
				if (cfg == null)
				{
					cfg = new F16Configuration();
				}
				var p = new Preset(name, cfg);
				Presets.Add(p);
				return p;
			}
			else if (Model == AircraftModel.FA18C)
			{
				if (cfg == null)
				{
					cfg = new FA18Configuration();
				}
				var p = new Preset(name, cfg);
				Presets.Add(p);
				return p;
			}
			else if (Model == AircraftModel.AH64D)
			{
				if (cfg == null)
				{
				    cfg = new AH64Configuration();
				}
				var p = new Preset(name, cfg);
				Presets.Add(p);
				return p;
			    }
            else if (Model == AircraftModel.A10CII)
            {
                if (cfg == null)
                {
                    cfg = new A10CIIConfiguration();
                }
                var p = new Preset(name, cfg);
                Presets.Add(p);
                return p;
            }
            else
			{
				throw new Exception();
			}
		}

		internal Preset ClonePreset(Preset preset)
		{
			var p = preset.Clone();
			Presets.Add(p);
			PresetsStore.PresetChanged(this, p);
			return p;
		}

		internal void DeletePreset(Preset preset)
		{
			Presets.Remove(preset);
			FileStorage.DeletePreset(this, preset);
		}
	}
}
