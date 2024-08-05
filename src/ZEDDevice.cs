using System;
using VL.Lib.Collections;
using VL.Core;
using VL.Core.CompilerServices;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace Devices.ZED.Enum
{
    [Serializable]
    public class ZEDDevice : DynamicEnumBase<ZEDDevice, DynamicZEDDevicesEnumDefinition>
    {
        public ZEDDevice(string value) : base(value)
        {
        }

        //this method needs to be imported in VL to set the default
        public static ZEDDevice CreateDefault()
        {
            //use method of base class if nothing special required
            return CreateDefaultBase();
        }

        public void AddEnumEntry(string entry, object tag)
        {
            DynamicZEDDevicesEnumDefinition.Instance.AddEntry(entry, tag);
        }

        public void RemoveEnumEntry(string entry)
        {
            DynamicZEDDevicesEnumDefinition.Instance.RemoveEntry(entry);
        }

        public void SetEntryTag(string entry, object tag)
        {
            DynamicZEDDevicesEnumDefinition.Instance.RemoveEntry(entry);
            DynamicZEDDevicesEnumDefinition.Instance.AddEntry(entry, tag);
        }
    }

    public class DynamicZEDDevicesEnumDefinition : DynamicEnumDefinitionBase<DynamicZEDDevicesEnumDefinition>
    {
        Dictionary<string, object> entries = new Dictionary<string, object>();
        Subject<object> trigger = new Subject<object>(); //Really just used as a trigger, the "object" is ignored



        //this is optional an can be used if any initialization before the call to GetEntries is needed
        protected override void Initialize()
        {
            AddEntry("Default", null);
            base.Initialize();
        }

        [CreateDefault]
        public static DynamicZEDDevicesEnumDefinition CreateDefault()
        {
            return Instance;
        }

        /// <summary>
        /// Adds an entry to the enum that can optionally have an object associated as its tag
        /// </summary>
        /// <param name="name">Name of the entry to add</param>
        /// <param name="tag">Optional: Object associated to the enum entry</param>
        public void AddEntry(string name, object? tag = null)
        {
            entries[name] = tag;
            trigger.OnNext("");
        }

        /// <summary>
        /// Removes the given entry from the enum
        /// </summary>
        /// <param name="name">Name of the entry to remove</param>
        public void RemoveEntry(string name)
        {
            entries.Remove(name);
            trigger.OnNext("");
        }

        /// <summary>
        /// Removes all entries from the enum
        /// </summary>
        public void ClearEntries()
        {
            entries.Clear();
            trigger.OnNext("");
        }

        protected override IReadOnlyDictionary<string, object> GetEntries()
        {
            return entries;
        }

        protected override IObservable<object> GetEntriesChangedObservable()
        {
            return trigger;
        }

        //add this to get a node that can access the Instance from everywhere
        //public static DynamicZEDDevicesEnumDefinition Instance => DynamicEnumDefinitionBase<DynamicZEDDevicesEnumDefinition>.Instance;
    }

}
