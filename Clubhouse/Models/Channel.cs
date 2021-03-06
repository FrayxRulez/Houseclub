using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Clubhouse.Models
{
    public class Channel
    {
        /*
		* "channels":[
	{
	"creator_user_profile_id":5468389,
	"channel_id":15215112,
	"channel":"xlJkYk6m",
	"topic":null,
	"is_private":false,
	"is_social_mode":false,
	"url":"https://www.joinclubhouse.com/room/xlJkYk6m",
	"feature_flags":[
	],
	"club":null,
	"club_name":null,
	"club_id":null,
	"welcome_for_user_profile":null,
	"num_other":0,
	"has_blocked_speakers":false,
	"is_explore_channel":false,
	"num_speakers":8,
	"num_all":184,
	"users":[
	{
	"user_id":877820863,
	"name":"Валентин Кашпур",
	"photo_url":"https://clubhouseprod.s3.amazonaws.com:443/877820863_2cd32360-0f8a-46c1-826e-ae88f66ebc36_thumbnail_250x250",
	"is_speaker":true,
	"is_moderator":true,
	"time_joined_as_speaker":"2021-02-19T00:52:31.484403+00:00",
	"is_followed_by_speaker":true,
	"is_invited_as_speaker":true
	},
	{
	"user_id":1808486887,
	"name":"Bogdan Kalashnikov",
	"photo_url":"https://clubhouseprod.s3.amazonaws.com:443/1808486887_b08a9768-71a5-4968-a5bd-f4998bea0a95_thumbnail_250x250",
	"is_speaker":true,
	"is_moderator":true,
	"time_joined_as_speaker":"2021-02-19T02:26:02.364976+00:00",
	"is_followed_by_speaker":true,
	"is_invited_as_speaker":true
	},
	{
	"user_id":261058534,
	"name":"Yulia Lis",
	"photo_url":"https://clubhouseprod.s3.amazonaws.com:443/261058534_a3a1f882-487b-450a-be69-6c5a268c6b38_thumbnail_250x250",
	"is_speaker":true,
	"is_moderator":true,
	"time_joined_as_speaker":"2021-02-19T02:30:29.539969+00:00",
	"is_followed_by_speaker":true,
	"is_invited_as_speaker":true
	}
	]
		* */

        [JsonPropertyName("creator_user_profile_id")]
        public ulong CreatorUserProfileId { get; set; }

        [JsonPropertyName("channel_id")]
        public int Id { get; set; }

        [JsonPropertyName("channel")]
        public String channel { get; set; }

        [JsonPropertyName("topic")]
        public String Topic { get; set; }

        [JsonPropertyName("is_private")]
        public bool IsPrivate { get; set; }

        [JsonPropertyName("is_social_mode")]
        public bool IsSocialMode { get; set; }

        [JsonPropertyName("url")]
        public String Url { get; set; }

        [JsonPropertyName("num_other")]
        public int NumOther { get; set; }

        [JsonPropertyName("has_blocked_speakers")]
        public bool HasBlockedSpeakers { get; set; }

        [JsonPropertyName("is_explore_channel")]
        public bool IsExploreChannel { get; set; }

        [JsonPropertyName("num_speakers")]
        public int NumSpeakers { get; set; }

        [JsonPropertyName("num_all")]
        public int NumAll { get; set; }

        [JsonPropertyName("users")]
        public List<ChannelUser> Users { get; set; }

        [JsonPropertyName("token")]
        public String Token { get; set; }

        [JsonPropertyName("is_handraise_enabled")]
        public bool IsHandraiseEnabled { get; set; }

        [JsonPropertyName("pubnub_token")]
        public String PubnubToken { get; set; }

        [JsonPropertyName("pubnub_heartbeat_value")]
        public int PubnubHeartbeatValue { get; set; }

        [JsonPropertyName("pubnub_heartbeat_interval")]
        public int PubnubHeartbeatInterval { get; set; }

        //[JsonPropertyName("club_id")]
        //public int ClubId { get; set; }

        [JsonPropertyName("club_name")]
        public string ClubName { get; set; }


        //    @Override
        //    public int describeContents()
        //    {
        //        return 0;
        //    }

        //    @Override
        //    public void writeToParcel(Parcel dest, int flags)
        //    {
        //        dest.writeInt(this.creatorUserProfileId);
        //        dest.writeInt(this.channelId);
        //        dest.writeString(this.channel);
        //        dest.writeString(this.topic);
        //        dest.writeByte(this.isPrivate ? (byte)1 : (byte)0);
        //        dest.writeByte(this.isSocialMode ? (byte)1 : (byte)0);
        //        dest.writeString(this.url);
        //        dest.writeInt(this.numOther);
        //        dest.writeByte(this.hasBlockedSpeakers ? (byte)1 : (byte)0);
        //        dest.writeByte(this.isExploreChannel ? (byte)1 : (byte)0);
        //        dest.writeInt(this.numSpeakers);
        //        dest.writeInt(this.numAll);
        //        dest.writeTypedList(this.users);
        //        dest.writeString(this.token);
        //        dest.writeByte(this.isHandraiseEnabled ? (byte)1 : (byte)0);
        //        dest.writeString(this.pubnubToken);
        //        dest.writeInt(this.pubnubHeartbeatValue);
        //        dest.writeInt(this.pubnubHeartbeatInterval);
        //    }

        //    public void readFromParcel(Parcel source)
        //    {
        //        this.creatorUserProfileId = source.readInt();
        //        this.channelId = source.readInt();
        //        this.channel = source.readString();
        //        this.topic = source.readString();
        //        this.isPrivate = source.readByte() != 0;
        //        this.isSocialMode = source.readByte() != 0;
        //        this.url = source.readString();
        //        this.numOther = source.readInt();
        //        this.hasBlockedSpeakers = source.readByte() != 0;
        //        this.isExploreChannel = source.readByte() != 0;
        //        this.numSpeakers = source.readInt();
        //        this.numAll = source.readInt();
        //        this.users = source.createTypedArrayList(ChannelUser.CREATOR);
        //        this.token = source.readString();
        //        this.isHandraiseEnabled = source.readByte() != 0;
        //        this.pubnubToken = source.readString();
        //        this.pubnubHeartbeatValue = source.readInt();
        //        this.pubnubHeartbeatInterval = source.readInt();
        //    }

        //    public Channel()
        //    {
        //    }

        //    protected Channel(Parcel in)
        //    {
        //        this.creatorUserProfileId =in.readInt();
        //        this.channelId =in.readInt();
        //        this.channel =in.readString();
        //        this.topic =in.readString();
        //        this.isPrivate =in.readByte() != 0;
        //        this.isSocialMode =in.readByte() != 0;
        //        this.url =in.readString();
        //        this.numOther =in.readInt();
        //        this.hasBlockedSpeakers =in.readByte() != 0;
        //        this.isExploreChannel =in.readByte() != 0;
        //        this.numSpeakers =in.readInt();
        //        this.numAll =in.readInt();
        //        this.users =in.createTypedArrayList(ChannelUser.CREATOR);
        //        this.token =in.readString();
        //        this.isHandraiseEnabled =in.readByte() != 0;
        //        this.pubnubToken =in.readString();
        //        this.pubnubHeartbeatValue =in.readInt();
        //        this.pubnubHeartbeatInterval =in.readInt();
        //    }

        //    public static final Creator<Channel> CREATOR= new Creator<Channel>(){
        //    @Override

        //    public Channel createFromParcel(Parcel source)
        //    {
        //        return new Channel(source);
        //    }

        //    @Override
        //        public Channel[] newArray(int size)
        //    {
        //        return new Channel[size];
        //    }
        //};
    }
}
