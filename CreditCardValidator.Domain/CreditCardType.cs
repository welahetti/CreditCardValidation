using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Domain
{
    
        /// <summary>
        /// Credit card type.
        /// </summary>
        public enum CreditCardType
        {
            /// <summary>
            /// Unknown issuers.
            /// </summary>
            [Description("Unknown Issuer")]
            Unknown,

            /// <summary>
            /// American Express cards.
            /// </summary>
            [Description("American Express")]
            AMEX,

            /// <summary>
            /// Discover Card cards.
            /// </summary>
            [Description("Discover Card")]
            Discover,

            /// <summary>
            /// MasterCard cards.
            /// </summary>
            [Description("MasterCard")]
            MasterCard,

            /// <summary>
            /// Visa cards.
            /// </summary>
            [Description("Visa")]
            VISA,

            
        }
    }

