using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

using Wechat.Data.Base;

namespace Wechat.Data.BusinessObjects
{
    [JsonObject(MemberSerialization.OptIn)]
    public partial class GeneralcouponCard : BusinessBase<string>
    {
        #region Declarations

		private string _defaultDetail = String.Empty;
		
		
		
		#endregion

        #region Constructors

        public GeneralcouponCard() { }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(_defaultDetail);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [JsonProperty]
		public virtual string DefaultDetail
        {
            get { return _defaultDetail; }
			set
			{
				OnDefaultDetailChanging();
				_defaultDetail = value;
				OnDefaultDetailChanged();
			}
        }
		partial void OnDefaultDetailChanging();
		partial void OnDefaultDetailChanged();
		
        #endregion
    }
}
