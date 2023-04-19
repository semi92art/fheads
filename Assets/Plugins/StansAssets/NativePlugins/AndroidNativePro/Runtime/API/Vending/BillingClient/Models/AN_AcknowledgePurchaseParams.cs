using System;
using UnityEngine;

namespace SA.Android.Vending.BillingClient
{
    /// <summary>
    /// Parameters to acknowledge a purchase.
    /// <see cref="AN_BillingClient.AcknowledgePurchase(AN_AcknowledgePurchaseParams, AN_iAcknowledgePurchaseResponseListener)"/>
    /// </summary>
    [Serializable]
    public class AN_AcknowledgePurchaseParams
    {
        /// <summary>
        /// Helps construct <see cref="AN_AcknowledgePurchaseParams"/> that are used to acknowledge a purchase.
        /// </summary>
        [Serializable]
        public class Builder
        {
            [SerializeField]
            string m_PurchaseToken = default;

            internal string PurchaseToken => m_PurchaseToken;

            internal Builder() { }

            /// <summary>
            /// Returns <see cref="AN_AcknowledgePurchaseParams"/> reference to initiate a purchase flow.
            /// </summary>
            /// <returns><see cref="AN_AcknowledgePurchaseParams"/> reference to initiate a purchase flow.</returns>
            public AN_AcknowledgePurchaseParams Build()
            {
                return new AN_AcknowledgePurchaseParams(this);
            }

            /// <summary>
            /// Specify developer payload be sent back with the purchase information.
            /// </summary>
            /// <param name="developerPayload">Developer Payload string.</param>
            /// <returns><see cref="AN_AcknowledgePurchaseParams"/> reference.</returns>
            [Obsolete("Removed from google API.")]
            public Builder SetDeveloperPayload(string developerPayload)
            {
                return this;
            }

            /// <summary>
            /// Specify the token that identifies the purchase to be acknowledged.
            /// </summary>
            /// <param name="purchaseToken">Purchase Token string.</param>
            /// <returns><see cref="AN_AcknowledgePurchaseParams"/> reference.</returns>
            public Builder SetPurchaseToken(string purchaseToken)
            {
                m_PurchaseToken = purchaseToken;
                return this;
            }
        }

        [SerializeField]
        Builder m_Builder = default;

        AN_AcknowledgePurchaseParams(Builder builder)
        {
            m_Builder = builder;
        }

        /// <summary>
        /// Constructs a new <see cref="Builder"/> instance.
        /// </summary>
        /// <returns>a new <see cref="Builder"/> instance.</returns>
        public static Builder NewBuilder()
        {
            return new Builder();
        }

        /// <summary>
        /// Returns developer data associated with the purchase to be acknowledged.
        /// </summary>
        [Obsolete("Property was removed from the google API.")]
        public string DeveloperPayload => string.Empty;

        /// <summary>
        /// Returns token that identifies the purchase to be acknowledged
        /// </summary>
        public string PurchaseToken => m_Builder.PurchaseToken;
    }
}
