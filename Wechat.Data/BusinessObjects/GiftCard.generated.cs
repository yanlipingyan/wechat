using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

using Wechat.Data.Base;

namespace Wechat.Data.BusinessObjects
{
    [JsonObject(MemberSerialization.OptIn)]
    public partial class GiftCard : BusinessBase<string>
    {
        #region Declarations

		private string _gift = String.Empty;
		
		
		
		#endregion

        #region Constructors

        public GiftCard() { }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(_gift);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [JsonProperty]
		public virtual string Gift
        {
            get { return _gift; }
			set
			{
				OnGiftChanging();
				_gift = value;
				OnGiftChanged();
			}
        }
		partial void OnGiftChanging();
		partial void OnGiftChanged();
		
        #endregion
    }
}
