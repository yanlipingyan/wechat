using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

using Wechat.Data.Base;

namespace Wechat.Data.BusinessObjects
{
    [JsonObject(MemberSerialization.OptIn)]
    public partial class Getcard : BusinessBase<int>
    {
        #region Declarations

		private string _guid = String.Empty;
		private string _cardId = String.Empty;
		private string _cardCode = String.Empty;
		private string _openId = String.Empty;
		private int _isGiveByFriend = default(Int32);
		private string _friendUserOpenId = null;
		private int _outerId = default(Int32);
		private int _isConsume = default(Int32);
		private System.DateTime _dateTime = new DateTime();
		
		
		
		#endregion

        #region Constructors

        public Getcard() { }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(_guid);
			sb.Append(_cardId);
			sb.Append(_cardCode);
			sb.Append(_openId);
			sb.Append(_isGiveByFriend);
			sb.Append(_friendUserOpenId);
			sb.Append(_outerId);
			sb.Append(_isConsume);
			sb.Append(_dateTime);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [JsonProperty]
		public virtual string Guid
        {
            get { return _guid; }
			set
			{
				OnGuidChanging();
				_guid = value;
				OnGuidChanged();
			}
        }
		partial void OnGuidChanging();
		partial void OnGuidChanged();
		
        [JsonProperty]
		public virtual string CardId
        {
            get { return _cardId; }
			set
			{
				OnCardIdChanging();
				_cardId = value;
				OnCardIdChanged();
			}
        }
		partial void OnCardIdChanging();
		partial void OnCardIdChanged();
		
        [JsonProperty]
		public virtual string CardCode
        {
            get { return _cardCode; }
			set
			{
				OnCardCodeChanging();
				_cardCode = value;
				OnCardCodeChanged();
			}
        }
		partial void OnCardCodeChanging();
		partial void OnCardCodeChanged();
		
        [JsonProperty]
		public virtual string OpenId
        {
            get { return _openId; }
			set
			{
				OnOpenIdChanging();
				_openId = value;
				OnOpenIdChanged();
			}
        }
		partial void OnOpenIdChanging();
		partial void OnOpenIdChanged();
		
        [JsonProperty]
		public virtual int IsGiveByFriend
        {
            get { return _isGiveByFriend; }
			set
			{
				OnIsGiveByFriendChanging();
				_isGiveByFriend = value;
				OnIsGiveByFriendChanged();
			}
        }
		partial void OnIsGiveByFriendChanging();
		partial void OnIsGiveByFriendChanged();
		
        [JsonProperty]
		public virtual string FriendUserOpenId
        {
            get { return _friendUserOpenId; }
			set
			{
				OnFriendUserOpenIdChanging();
				_friendUserOpenId = value;
				OnFriendUserOpenIdChanged();
			}
        }
		partial void OnFriendUserOpenIdChanging();
		partial void OnFriendUserOpenIdChanged();
		
        [JsonProperty]
		public virtual int OuterId
        {
            get { return _outerId; }
			set
			{
				OnOuterIdChanging();
				_outerId = value;
				OnOuterIdChanged();
			}
        }
		partial void OnOuterIdChanging();
		partial void OnOuterIdChanged();
		
        [JsonProperty]
		public virtual int IsConsume
        {
            get { return _isConsume; }
			set
			{
				OnIsConsumeChanging();
				_isConsume = value;
				OnIsConsumeChanged();
			}
        }
		partial void OnIsConsumeChanging();
		partial void OnIsConsumeChanged();
		
        [JsonProperty]
		public virtual System.DateTime DateTime
        {
            get { return _dateTime; }
			set
			{
				OnDateTimeChanging();
				_dateTime = value;
				OnDateTimeChanged();
			}
        }
		partial void OnDateTimeChanging();
		partial void OnDateTimeChanged();
		
        #endregion
    }
}
