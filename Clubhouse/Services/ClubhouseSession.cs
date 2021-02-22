using Clubhouse.Models;
using System;
using Windows.Storage;

namespace Clubhouse.Services
{
    public class ClubhouseSession
    {

        public static string deviceID;
        public static ulong userID;
        public static string userToken;
        public static bool isWaitlisted;
        public static User self;

        public static void load()
        {
            var prefs = ApplicationData.Current.LocalSettings.CreateContainer("session", ApplicationDataCreateDisposition.Always);
            deviceID = GetValueOrDefault<string>(prefs, "device_id", null);
            userID = GetValueOrDefault<ulong>(prefs, "user_id", 0);
            userToken = GetValueOrDefault<string>(prefs, "user_token", null);
            isWaitlisted = GetValueOrDefault(prefs, "waitlisted", false);
            if (deviceID == null)
            {
                deviceID = Guid.NewGuid().ToString().ToUpperInvariant();
                write();
            }
        }

        public static void write()
        {
            var prefs = ApplicationData.Current.LocalSettings.CreateContainer("session", ApplicationDataCreateDisposition.Always);
            AddOrUpdateValue(prefs, "device_id", deviceID);
            AddOrUpdateValue(prefs, "user_id", userID);
            AddOrUpdateValue(prefs, "user_token", userToken);
            AddOrUpdateValue(prefs, "waitlisted", isWaitlisted);
        }

        public static void clear()
        {
            ApplicationData.Current.LocalSettings.DeleteContainer("session");
            load();
        }

        public static bool isLoggedIn()
        {
            return userToken != null;
        }

        //private static SharedPreferences prefs()
        //{
        //    return App.applicationContext.getSharedPreferences("session", Context.MODE_PRIVATE);
        //}

        protected static bool AddOrUpdateValue(ApplicationDataContainer container, string key, Object value)
        {
            bool valueChanged = false;

            if (container.Values.ContainsKey(key))
            {
                if (container.Values[key] != value)
                {
                    container.Values[key] = value;
                    valueChanged = true;
                }
            }
            else
            {
                container.Values.Add(key, value);
                valueChanged = true;
            }

            return valueChanged;
        }

        protected static valueType GetValueOrDefault<valueType>(ApplicationDataContainer container, string key, valueType defaultValue)
        {
            valueType value;

            if (container.Values.ContainsKey(key))
            {
                value = (valueType)container.Values[key];
            }
            else
            {
                value = defaultValue;
            }

            return value;
        }

        protected static void RemoveValue(ApplicationDataContainer container, string key)
        {
            container.Values.Remove(key);
        }
    }
}
