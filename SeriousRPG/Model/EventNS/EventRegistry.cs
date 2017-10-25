/*
Copyright 2017 Enkhbold Nyamsuren (http://www.bcogs.net , http://www.bcogs.info/)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeriousRPG.Model.EventNS 
{
    public static class EventRegistry
    {
        #region Fields

        private static Dictionary<int, IEvent> registry = new Dictionary<int, IEvent>();
        private static Dictionary<Type, List<IEvent>> registryByType = new Dictionary<Type, List<IEvent>>();

        #endregion Fields

        #region Constructors

        static EventRegistry() {}

        #endregion Constructors

        #region Methods

        public static IEvent GetEvent(int eventId) {
            if (!HasEvent(eventId)) { 
                // [TODO] error msg
                return null;
            }

            return EventRegistry.registry[eventId];
        }

        public static List<IEvent> GetEventsByType(Type type) {
            if (!EventRegistry.registryByType.ContainsKey(type)) { 
                // [TODO] warning msg
                List<IEvent> newList = new List<IEvent>();
                EventRegistry.registryByType.Add(type, newList);
                return newList;
            }

            return EventRegistry.registryByType[type];
        }

        public static bool AddEvent(IEvent srEvent) {
            if (HasEvent(srEvent)) {
                // [TODO] error msg
                return false;
            }

            EventRegistry.registry.Add(srEvent.Id, srEvent);

            // [SC] register the event by its type
            if (EventRegistry.registryByType.ContainsKey(srEvent.GetType())) {
                EventRegistry.registryByType[srEvent.GetType()].Add(srEvent);
            }
            else {
                List<IEvent> newList = new List<IEvent>();
                newList.Add(srEvent);
                EventRegistry.registryByType.Add(srEvent.GetType(), newList);
            }

            return true;
        }

        public static bool RemoveEvent(int eventId) {
            if (!HasEvent(eventId)) {
                // [TODO] error msg
                return false;
            }

            IEvent srEvent = EventRegistry.registry[eventId];

            EventRegistry.registryByType[srEvent.GetType()].Remove(srEvent);

            EventRegistry.registry.Remove(eventId);

            return true;
        }

        public static bool RemoveEvent(IEvent srEvent) {
            return RemoveEvent(srEvent.Id);
        }

        public static bool HasEvent(IEvent srEvent) {
            return EventRegistry.HasEvent(srEvent.Id);
        }

        public static bool HasEvent(int eventId) {
            return EventRegistry.registry.ContainsKey(eventId);
        }

        #endregion Methods
    }
}
