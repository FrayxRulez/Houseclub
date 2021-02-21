using System;
using System.Text.Json.Serialization;

namespace Clubhouse.Models
{
    public class User
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("photo_url")]
        public Uri PhotoUrl { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }


        //	@Override
        //public int describeContents(){
        //	return 0;
        //}

        //@Override
        //public void writeToParcel(Parcel dest, int flags){
        //	dest.writeInt(this.userId);
        //	dest.writeString(this.name);
        //	dest.writeString(this.photoUrl);
        //	dest.writeString(this.username);
        //}

        //public void readFromParcel(Parcel source){
        //	this.userId=source.readInt();
        //	this.name=source.readString();
        //	this.photoUrl=source.readString();
        //	this.username=source.readString();
        //}

        //public User(){
        //}

        //protected User(Parcel in){
        //	this.userId=in.readInt();
        //	this.name=in.readString();
        //	this.photoUrl=in.readString();
        //	this.username=in.readString();
        //}

        //public static final Creator<User> CREATOR=new Creator<User>(){
        //	@Override
        //	public User createFromParcel(Parcel source){
        //		return new User(source);
        //	}

        //	@Override
        //	public User[] newArray(int size){
        //		return new User[size];
        //	}
        //};
    }
}
