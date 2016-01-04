using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

using Wechat.Data.Base;

namespace Wechat.Data.BusinessObjects
{
    [JsonObject(MemberSerialization.OptIn)]
    public partial class CashCard : BusinessBase<string>
    {
        #region Declarations

		private int _leastCost = default(Int32);
		private int _reduceCost = default(Int32);
		
		
		
		#endregion

        #region Constructors

        public CashCard() { }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(_leastCost);
			sb.Append(_reduceCost);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [JsonProperty]
		public virtual int LeastCost
        {
            get { return _leastCost; }
			set
			{
				OnLeastCostChanging();
				_leastCost = value;
				OnLeastCostChanged();
			}
        }
		partial void OnLeastCostChanging();
		partial void OnLeastCostChanged();
		
        [JsonProperty]
		public virtual int ReduceCost
        {
            get { return _reduceCost; }
			set
			{
				OnReduceCostChanging();
				_reduceCost = value;
				OnReduceCostChanged();
			}
        }
		partial void OnReduceCostChanging();
		partial void OnReduceCostChanged();
		
        #endregion
    }
}
