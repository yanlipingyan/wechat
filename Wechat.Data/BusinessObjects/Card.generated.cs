using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

using Wechat.Data.Base;

namespace Wechat.Data.BusinessObjects
{
    [JsonObject(MemberSerialization.OptIn)]
    public partial class Card : BusinessBase<int>
    {
        #region Declarations

		private string _guid = String.Empty;
		private string _cardId = null;
		private string _cardType = String.Empty;
		private string _logoUrl = String.Empty;
		private string _codeType = String.Empty;
		private string _brandName = String.Empty;
		private string _title = String.Empty;
		private string _subTitle = null;
		private string _color = String.Empty;
		private string _notice = String.Empty;
		private string _description = String.Empty;
		private int _quantity = default(Int32);
		private int _type = default(Int32);
		private uint _beginTimestamp = default(UInt32);
		private uint _endTimestamp = default(UInt32);
		private int _fixedTerm = default(Int32);
		private int _fixedBeginTerm = default(Int32);
		private int _useCustomCode = default(Int32);
		private int _bindOpenid = default(Int32);
		private string _servicePhone = null;
		private string _locationIdList = null;
		private string _source = null;
		private string _customUrlName = null;
		private string _customUrl = null;
		private string _customUrlSubTitle = null;
		private string _promotionUrlName = null;
		private string _promotionUrl = null;
		private string _promotionUrlSubTitle = null;
		private int _getLimit = default(Int32);
		private int _canShare = default(Int32);
		private int _canGiveFriend = default(Int32);
		private int _state = default(Int32);
		private System.DateTime _dateTime = new DateTime();
		
		
		
		#endregion

        #region Constructors

        public Card() { }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(_guid);
			sb.Append(_cardId);
			sb.Append(_cardType);
			sb.Append(_logoUrl);
			sb.Append(_codeType);
			sb.Append(_brandName);
			sb.Append(_title);
			sb.Append(_subTitle);
			sb.Append(_color);
			sb.Append(_notice);
			sb.Append(_description);
			sb.Append(_quantity);
			sb.Append(_type);
			sb.Append(_beginTimestamp);
			sb.Append(_endTimestamp);
			sb.Append(_fixedTerm);
			sb.Append(_fixedBeginTerm);
			sb.Append(_useCustomCode);
			sb.Append(_bindOpenid);
			sb.Append(_servicePhone);
			sb.Append(_locationIdList);
			sb.Append(_source);
			sb.Append(_customUrlName);
			sb.Append(_customUrl);
			sb.Append(_customUrlSubTitle);
			sb.Append(_promotionUrlName);
			sb.Append(_promotionUrl);
			sb.Append(_promotionUrlSubTitle);
			sb.Append(_getLimit);
			sb.Append(_canShare);
			sb.Append(_canGiveFriend);
			sb.Append(_state);
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
		public virtual string CardType
        {
            get { return _cardType; }
			set
			{
				OnCardTypeChanging();
				_cardType = value;
				OnCardTypeChanged();
			}
        }
		partial void OnCardTypeChanging();
		partial void OnCardTypeChanged();
		
        [JsonProperty]
		public virtual string LogoUrl
        {
            get { return _logoUrl; }
			set
			{
				OnLogoUrlChanging();
				_logoUrl = value;
				OnLogoUrlChanged();
			}
        }
		partial void OnLogoUrlChanging();
		partial void OnLogoUrlChanged();
		
        [JsonProperty]
		public virtual string CodeType
        {
            get { return _codeType; }
			set
			{
				OnCodeTypeChanging();
				_codeType = value;
				OnCodeTypeChanged();
			}
        }
		partial void OnCodeTypeChanging();
		partial void OnCodeTypeChanged();
		
        [JsonProperty]
		public virtual string BrandName
        {
            get { return _brandName; }
			set
			{
				OnBrandNameChanging();
				_brandName = value;
				OnBrandNameChanged();
			}
        }
		partial void OnBrandNameChanging();
		partial void OnBrandNameChanged();
		
        [JsonProperty]
		public virtual string Title
        {
            get { return _title; }
			set
			{
				OnTitleChanging();
				_title = value;
				OnTitleChanged();
			}
        }
		partial void OnTitleChanging();
		partial void OnTitleChanged();
		
        [JsonProperty]
		public virtual string SubTitle
        {
            get { return _subTitle; }
			set
			{
				OnSubTitleChanging();
				_subTitle = value;
				OnSubTitleChanged();
			}
        }
		partial void OnSubTitleChanging();
		partial void OnSubTitleChanged();
		
        [JsonProperty]
		public virtual string Color
        {
            get { return _color; }
			set
			{
				OnColorChanging();
				_color = value;
				OnColorChanged();
			}
        }
		partial void OnColorChanging();
		partial void OnColorChanged();
		
        [JsonProperty]
		public virtual string Notice
        {
            get { return _notice; }
			set
			{
				OnNoticeChanging();
				_notice = value;
				OnNoticeChanged();
			}
        }
		partial void OnNoticeChanging();
		partial void OnNoticeChanged();
		
        [JsonProperty]
		public virtual string Description
        {
            get { return _description; }
			set
			{
				OnDescriptionChanging();
				_description = value;
				OnDescriptionChanged();
			}
        }
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		
        [JsonProperty]
		public virtual int Quantity
        {
            get { return _quantity; }
			set
			{
				OnQuantityChanging();
				_quantity = value;
				OnQuantityChanged();
			}
        }
		partial void OnQuantityChanging();
		partial void OnQuantityChanged();
		
        [JsonProperty]
		public virtual int Type
        {
            get { return _type; }
			set
			{
				OnTypeChanging();
				_type = value;
				OnTypeChanged();
			}
        }
		partial void OnTypeChanging();
		partial void OnTypeChanged();
		
        [JsonProperty]
		public virtual uint BeginTimestamp
        {
            get { return _beginTimestamp; }
			set
			{
				OnBeginTimestampChanging();
				_beginTimestamp = value;
				OnBeginTimestampChanged();
			}
        }
		partial void OnBeginTimestampChanging();
		partial void OnBeginTimestampChanged();
		
        [JsonProperty]
		public virtual uint EndTimestamp
        {
            get { return _endTimestamp; }
			set
			{
				OnEndTimestampChanging();
				_endTimestamp = value;
				OnEndTimestampChanged();
			}
        }
		partial void OnEndTimestampChanging();
		partial void OnEndTimestampChanged();
		
        [JsonProperty]
		public virtual int FixedTerm
        {
            get { return _fixedTerm; }
			set
			{
				OnFixedTermChanging();
				_fixedTerm = value;
				OnFixedTermChanged();
			}
        }
		partial void OnFixedTermChanging();
		partial void OnFixedTermChanged();
		
        [JsonProperty]
		public virtual int FixedBeginTerm
        {
            get { return _fixedBeginTerm; }
			set
			{
				OnFixedBeginTermChanging();
				_fixedBeginTerm = value;
				OnFixedBeginTermChanged();
			}
        }
		partial void OnFixedBeginTermChanging();
		partial void OnFixedBeginTermChanged();
		
        [JsonProperty]
		public virtual int UseCustomCode
        {
            get { return _useCustomCode; }
			set
			{
				OnUseCustomCodeChanging();
				_useCustomCode = value;
				OnUseCustomCodeChanged();
			}
        }
		partial void OnUseCustomCodeChanging();
		partial void OnUseCustomCodeChanged();
		
        [JsonProperty]
		public virtual int BindOpenid
        {
            get { return _bindOpenid; }
			set
			{
				OnBindOpenidChanging();
				_bindOpenid = value;
				OnBindOpenidChanged();
			}
        }
		partial void OnBindOpenidChanging();
		partial void OnBindOpenidChanged();
		
        [JsonProperty]
		public virtual string ServicePhone
        {
            get { return _servicePhone; }
			set
			{
				OnServicePhoneChanging();
				_servicePhone = value;
				OnServicePhoneChanged();
			}
        }
		partial void OnServicePhoneChanging();
		partial void OnServicePhoneChanged();
		
        [JsonProperty]
		public virtual string LocationIdList
        {
            get { return _locationIdList; }
			set
			{
				OnLocationIdListChanging();
				_locationIdList = value;
				OnLocationIdListChanged();
			}
        }
		partial void OnLocationIdListChanging();
		partial void OnLocationIdListChanged();
		
        [JsonProperty]
		public virtual string Source
        {
            get { return _source; }
			set
			{
				OnSourceChanging();
				_source = value;
				OnSourceChanged();
			}
        }
		partial void OnSourceChanging();
		partial void OnSourceChanged();
		
        [JsonProperty]
		public virtual string CustomUrlName
        {
            get { return _customUrlName; }
			set
			{
				OnCustomUrlNameChanging();
				_customUrlName = value;
				OnCustomUrlNameChanged();
			}
        }
		partial void OnCustomUrlNameChanging();
		partial void OnCustomUrlNameChanged();
		
        [JsonProperty]
		public virtual string CustomUrl
        {
            get { return _customUrl; }
			set
			{
				OnCustomUrlChanging();
				_customUrl = value;
				OnCustomUrlChanged();
			}
        }
		partial void OnCustomUrlChanging();
		partial void OnCustomUrlChanged();
		
        [JsonProperty]
		public virtual string CustomUrlSubTitle
        {
            get { return _customUrlSubTitle; }
			set
			{
				OnCustomUrlSubTitleChanging();
				_customUrlSubTitle = value;
				OnCustomUrlSubTitleChanged();
			}
        }
		partial void OnCustomUrlSubTitleChanging();
		partial void OnCustomUrlSubTitleChanged();
		
        [JsonProperty]
		public virtual string PromotionUrlName
        {
            get { return _promotionUrlName; }
			set
			{
				OnPromotionUrlNameChanging();
				_promotionUrlName = value;
				OnPromotionUrlNameChanged();
			}
        }
		partial void OnPromotionUrlNameChanging();
		partial void OnPromotionUrlNameChanged();
		
        [JsonProperty]
		public virtual string PromotionUrl
        {
            get { return _promotionUrl; }
			set
			{
				OnPromotionUrlChanging();
				_promotionUrl = value;
				OnPromotionUrlChanged();
			}
        }
		partial void OnPromotionUrlChanging();
		partial void OnPromotionUrlChanged();
		
        [JsonProperty]
		public virtual string PromotionUrlSubTitle
        {
            get { return _promotionUrlSubTitle; }
			set
			{
				OnPromotionUrlSubTitleChanging();
				_promotionUrlSubTitle = value;
				OnPromotionUrlSubTitleChanged();
			}
        }
		partial void OnPromotionUrlSubTitleChanging();
		partial void OnPromotionUrlSubTitleChanged();
		
        [JsonProperty]
		public virtual int GetLimit
        {
            get { return _getLimit; }
			set
			{
				OnGetLimitChanging();
				_getLimit = value;
				OnGetLimitChanged();
			}
        }
		partial void OnGetLimitChanging();
		partial void OnGetLimitChanged();
		
        [JsonProperty]
		public virtual int CanShare
        {
            get { return _canShare; }
			set
			{
				OnCanShareChanging();
				_canShare = value;
				OnCanShareChanged();
			}
        }
		partial void OnCanShareChanging();
		partial void OnCanShareChanged();
		
        [JsonProperty]
		public virtual int CanGiveFriend
        {
            get { return _canGiveFriend; }
			set
			{
				OnCanGiveFriendChanging();
				_canGiveFriend = value;
				OnCanGiveFriendChanged();
			}
        }
		partial void OnCanGiveFriendChanging();
		partial void OnCanGiveFriendChanged();
		
        [JsonProperty]
		public virtual int State
        {
            get { return _state; }
			set
			{
				OnStateChanging();
				_state = value;
				OnStateChanged();
			}
        }
		partial void OnStateChanging();
		partial void OnStateChanged();
		
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
