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
// 	Generated: 02/23/2021 14:07:21
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
        /// Localized resource similar to "Cancel"
        /// </summary>
        public static string Cancel
        {
            get
            {
                return Resource.GetString("Cancel");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Start a room"
        /// </summary>
        public static string CreateChannel
        {
            get
            {
                return Resource.GetString("CreateChannel");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Follow"
        /// </summary>
        public static string Follow
        {
            get
            {
                return Resource.GetString("Follow");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "followers"
        /// </summary>
        public static string FollowersCount
        {
            get
            {
                return Resource.GetString("FollowersCount");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Following"
        /// </summary>
        public static string Following
        {
            get
            {
                return Resource.GetString("Following");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "following"
        /// </summary>
        public static string FollowingCount
        {
            get
            {
                return Resource.GetString("FollowingCount");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Join"
        /// </summary>
        public static string Join
        {
            get
            {
                return Resource.GetString("Join");
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
        /// Localized resource similar to "OK"
        /// </summary>
        public static string OK
        {
            get
            {
                return Resource.GetString("OK");
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
        /// Localized resource similar to "Do you want to accept the invite from {0}?"
        /// </summary>
        public static string RoomJoinAsSpeakerMessage
        {
            get
            {
                return Resource.GetString("RoomJoinAsSpeakerMessage");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Join as speaker"
        /// </summary>
        public static string RoomJoinAsSpeakerTitle
        {
            get
            {
                return Resource.GetString("RoomJoinAsSpeakerTitle");
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
        /// Localized resource similar to "Unfollow"
        /// </summary>
        public static string Unfollow
        {
            get
            {
                return Resource.GetString("Unfollow");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Followed by **{0}**"
        /// </summary>
        public static string UserFollowedBy
        {
            get
            {
                return Resource.GetString("UserFollowedBy");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Followed by **{0}** and **{1}**"
        /// </summary>
        public static string UserFollowedByAnd
        {
            get
            {
                return Resource.GetString("UserFollowedByAnd");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Follows you"
        /// </summary>
        public static string UserFollowsYou
        {
            get
            {
                return Resource.GetString("UserFollowsYou");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Joined {0}
        ///Nominated by **{1}**"
        /// </summary>
        public static string UserInvitedBy
        {
            get
            {
                return Resource.GetString("UserInvitedBy");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Joined {0}"
        /// </summary>
        public static string UserJoined
        {
            get
            {
                return Resource.GetString("UserJoined");
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
        /// Localized resource similar to "Member of"
        /// </summary>
        public static string UserMemberOf
        {
            get
            {
                return Resource.GetString("UserMemberOf");
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
