using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

using Wechat.Data.Base;

namespace Wechat.Data.BusinessObjects
{
    [JsonObject(MemberSerialization.OptIn)]
    public partial class GrouponCard : BusinessBase<string>
    {
        #region Declarations

		private string _dealDetail = String.Empty;
		
		
		
		#endregion

        #region Constructors

        public GrouponCard() { }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(_dealDetail);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [JsonProperty]
		public virtual string DealDetail
        {
            get { return _dealDetail; }
			set
			{
				OnDealDetailChanging();
				_dealDetail = value;
				OnDealDetailChanged();
			}
        }
		partial void OnDealDetailChanging();
		partial void OnDealDetailChanged();
		
        #endregion
    }
}
