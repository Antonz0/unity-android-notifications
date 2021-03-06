﻿using System.Collections.Generic;
using UnifiedNotifications.Extensions;
using UnifiedNotifications.Extensions.Android;
using UnifiedNotifications.Extensions.iOS;
using UnifiedNotifications.Private.Extensions;

namespace UnifiedNotifications.Private
{
    public sealed class LocalNotification : ILocalNotification
    {
        List<INotificationExtension> extensions = new List<INotificationExtension>(4);

        public ExtensionT GetExtension<ExtensionT> (bool createIfNotExists)
            where ExtensionT : INotificationExtension
        {
            for (int i = 0; i < extensions.Count; i++)
            {
                if (extensions[i] is ExtensionT)
                {
                    return (ExtensionT)extensions[i];
                }
            }

            if (createIfNotExists)
            {
                if (typeof(ExtensionT) == typeof(INotificationExtensionAndroid))
                {
                    INotificationExtension extension = new NotificationExtensionAndroid();
                    extensions.Add(extension);
                    return (ExtensionT)extension;
                }
                else if (typeof(ExtensionT) == typeof(INotificationExtensionIOS))
                {
                    INotificationExtension extension = new NotificationExtensionIOS();
                    extensions.Add(extension);
                    return (ExtensionT)extension;
                }
            }

            return default(ExtensionT);
        }

        public string       title               { get; set; }
        public string       message             { get; set; }
        public bool         useSound            { get; set; }

        public string       customSoundName     { get; set; }
    }
}
