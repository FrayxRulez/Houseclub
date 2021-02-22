//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// --------------------------------------------------------------------------------------------------
// <auto-generatedInfo>
// 	This code was generated by ResW File Code Generator (http://bit.ly/reswcodegen)
// 	ResW File Code Generator was written by Christian Resma Helle
// 	and is under GNU General Public License version 2 (GPLv2)
// 
// 	This code contains a helper class exposing property representations
// 	of the string resources defined in the specified .ResW file
// 
// 	Generated: 02/22/2021 15:42:26
// </auto-generatedInfo>
// --------------------------------------------------------------------------------------------------
namespace Clubhouse.Strings
{
    using Windows.ApplicationModel.Resources;
    
    
    public sealed partial class Resources
    {
        
        private static ResourceLoader resourceLoader;
        
        /// <summary>
        /// Get or set ResourceLoader implementation
        /// </summary>
        public static ResourceLoader Resource
        {
            get
            {
                if ((resourceLoader == null))
                {
                    Resources.Initialize();
                }
                return resourceLoader;
            }
            set
            {
                resourceLoader = value;
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Next"
        /// </summary>
        public static string Next
        {
            get
            {
                return Resource.GetString("Next");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Followed by the speakers"
        /// </summary>
        public static string RoomFollowedByTheSpeakers
        {
            get
            {
                return Resource.GetString("RoomFollowedByTheSpeakers");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "?? Leave quietly"
        /// </summary>
        public static string RoomLeaveChannel
        {
            get
            {
                return Resource.GetString("RoomLeaveChannel");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Others in the room"
        /// </summary>
        public static string RoomOthersInTheRoom
        {
            get
            {
                return Resource.GetString("RoomOthersInTheRoom");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Enter the code we just texted you."
        /// </summary>
        public static string SentSmsCode
        {
            get
            {
                return Resource.GetString("SentSmsCode");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Please confirm your country code and enter your phone number."
        /// </summary>
        public static string StartText
        {
            get
            {
                return Resource.GetString("StartText");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Online"
        /// </summary>
        public static string UserLastActiveOnline
        {
            get
            {
                return Resource.GetString("UserLastActiveOnline");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Verification code"
        /// </summary>
        public static string YourCode
        {
            get
            {
                return Resource.GetString("YourCode");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Enter your phone #"
        /// </summary>
        public static string YourPhone
        {
            get
            {
                return Resource.GetString("YourPhone");
            }
        }
        
        public static void Initialize()
        {
            string executingAssemblyName;
            executingAssemblyName = Windows.UI.Xaml.Application.Current.GetType().AssemblyQualifiedName;
            string[] executingAssemblySplit;
            executingAssemblySplit = executingAssemblyName.Split(',');
            executingAssemblyName = executingAssemblySplit[1];
            string currentAssemblyName;
            currentAssemblyName = typeof(Resources).AssemblyQualifiedName;
            string[] currentAssemblySplit;
            currentAssemblySplit = currentAssemblyName.Split(',');
            currentAssemblyName = currentAssemblySplit[1];
            if (executingAssemblyName.Equals(currentAssemblyName))
            {
                resourceLoader = ResourceLoader.GetForCurrentView("Resources");
            }
            else
            {
                resourceLoader = ResourceLoader.GetForCurrentView(currentAssemblyName + "/Resources");
            }
        }
    }
}