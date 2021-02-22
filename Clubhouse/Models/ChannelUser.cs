using System.Text.Json.Serialization;

namespace Clubhouse.Models
{
    public class ChannelUser : User
    {
        [JsonPropertyName("is_speaker")]
        public bool IsSpeaker { get; set; }

        [JsonPropertyName("is_moderator")]
        public bool IsModerator { get; set; }

        [JsonPropertyName("is_followed_by_speaker")]
        public bool IsFollowedBySpeaker { get; set; }

        [JsonPropertyName("is_invited_as_speaker")]
        public bool IsInvitedAsSpeaker { get; set; }

        [JsonPropertyName("is_new")]
        public bool IsNew { get; set; }

        [JsonPropertyName("time_joined_as_speaker")]
        public string TimeJoinedAsSpeaker { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        public bool isMuted { get; set; }


    //@Override
    //public int describeContents(){
    //	return 0;
    //}

    //@Override
    //public void writeToParcel(Parcel dest, int flags){
    //	super.writeToParcel(dest, flags);
    //	dest.writeByte(this.isSpeaker ? (byte) 1 : (byte) 0);
    //	dest.writeByte(this.isModerator ? (byte) 1 : (byte) 0);
    //	dest.writeByte(this.isFollowedBySpeaker ? (byte) 1 : (byte) 0);
    //	dest.writeByte(this.isInvitedAsSpeaker ? (byte) 1 : (byte) 0);
    //	dest.writeByte(this.isNew ? (byte) 1 : (byte) 0);
    //	dest.writeString(this.timeJoinedAsSpeaker);
    //	dest.writeString(this.firstName);
    //}

    //public void readFromParcel(Parcel source){
    //	super.readFromParcel(source);
    //	this.isSpeaker=source.readByte()!=0;
    //	this.isModerator=source.readByte()!=0;
    //	this.isFollowedBySpeaker=source.readByte()!=0;
    //	this.isInvitedAsSpeaker=source.readByte()!=0;
    //	this.isNew=source.readByte()!=0;
    //	this.timeJoinedAsSpeaker=source.readString();
    //	this.firstName=source.readString();
    //}

    //public ChannelUser(){
    //}

    //protected ChannelUser(Parcel in){
    //	super(in);
    //	this.isSpeaker=in.readByte()!=0;
    //	this.isModerator=in.readByte()!=0;
    //	this.isFollowedBySpeaker=in.readByte()!=0;
    //	this.isInvitedAsSpeaker=in.readByte()!=0;
    //	this.isNew=in.readByte()!=0;
    //	this.timeJoinedAsSpeaker=in.readString();
    //	this.firstName=in.readString();
    //}

    //public static final Creator<ChannelUser> CREATOR=new Creator<ChannelUser>(){
    //	@Override
    //	public ChannelUser createFromParcel(Parcel source){
    //		return new ChannelUser(source);
    //	}

    //	@Override
    //	public ChannelUser[] newArray(int size){
    //		return new ChannelUser[size];
    //	}
    //};
    }
}
