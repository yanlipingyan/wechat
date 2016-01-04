using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

using Wechat.Data.Base;

namespace Wechat.Data.BusinessObjects
{
    [JsonObject(MemberSerialization.OptIn)]
    public partial class DiscountCard : BusinessBase<string>
    {
        #region Declarations

		private int _discount = default(Int32);
		
		
		
		#endregion

        #region Constructors

        public DiscountCard() { }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(_discount);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [JsonProperty]
		public virtual int Discount
        {
            get { return _discount; }
			set
			{
				OnDiscountChanging();
				_discount = value;
				OnDiscountChanged();
			}
        }
		partial void OnDiscountChanging();
		partial void OnDiscountChanged();
		
        #endregion
    }
}
